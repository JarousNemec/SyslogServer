using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SyslogServer;

Console.WriteLine("Initializing...");
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<Server>();
Console.WriteLine("Initialized!");

var app = builder.Build();

Console.WriteLine("Starting receiver...");
var server = app.Services.GetRequiredService<Server>();
server.Run();

_ = app.RunAsync();