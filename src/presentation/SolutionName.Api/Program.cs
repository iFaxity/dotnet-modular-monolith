var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile(
        "appsettings.Local.json",
        optional: true,
        reloadOnChange: true
    );
}

builder.WebHost.ConfigureKestrel(options =>
{
    // Do not add the Server HTTP header.
    options.AddServerHeader = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseForwardedHeaders();
app.UseRouting();
app.UseExceptionHandler();

// Run the app
app.Run();
