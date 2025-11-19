using Azure.Provisioning;
using Azure.Provisioning.AppService;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureAppServiceEnvironment("aspire").ConfigureInfrastructure(infra =>
{
    var plan = infra.GetProvisionableResources()
        .OfType<AppServicePlan>()
        .Single();

    plan.Sku = new AppServiceSkuDescription
    {
        Name = "B1", // Basic tier, 1 core
    };
});


var sqlServer = builder
    .AddAzureSqlServer("sql")
    .RunAsContainer(container =>
    {
        // Configure SQL Server to run locally as a container
        container.WithLifetime(ContainerLifetime.Persistent);
        container.WithHostPort(1800);
    });


var db = sqlServer
    .AddDatabase("CleanArchitecture", "clean-architecture");
    //.WithDropDatabaseCommand();


var migrationService = builder.AddProject<MigrationService>("migrations")
    // 👇 Changed
    .PublishAsAzureAppServiceWebsite((infra, site) =>
    {
        // Needed for hosted service to run
        site.SiteConfig.IsAlwaysOn = true;

        // Dynamically set environment so we can enable seeding of data
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var envSetting = new AppServiceNameValuePair { Name = "ASPNETCORE_ENVIRONMENT", Value = environment };
        site.SiteConfig.AppSettings.Add(new BicepValue<AppServiceNameValuePair>(envSetting));
    })
    // 👆 Changed
    .WithReference(db)
    .WaitFor(sqlServer);

    .AddProject<WebApi>("api")
    .WithExternalHttpEndpoints()
    // No need to use builder.PublishAsAzureAppServiceWebsite() here, as that is the default due to builder.AddAzureAppServiceEnvironment()
    .WithReference(db)
    .WaitForCompletion(migrationService);

if (builder.ExecutionContext.IsPublishMode)
{
    var logAnalytics = builder.AddAzureLogAnalyticsWorkspace("log-analytics");
    var insights = builder.AddAzureApplicationInsights("insights", logAnalytics);
    api.WithReference(insights);
    migrationService.WithReference(insights);
}

builder.Build().Run();
