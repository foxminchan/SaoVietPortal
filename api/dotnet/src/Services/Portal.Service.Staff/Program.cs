using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Staff Service").Centered().Color(Color.BlueViolet));

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(webBuilder =>
{
    webBuilder.Listen(IPAddress.Any, builder.Configuration.GetValue("ApiPort", 6004));
    webBuilder.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
});

var app = builder.Build();

app.Run();