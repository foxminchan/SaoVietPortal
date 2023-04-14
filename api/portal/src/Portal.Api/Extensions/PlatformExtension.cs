using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portal.Domain.ValueObjects;

namespace Portal.Api.Extensions;

public static class PlatformExtension
{
    public static JObject GetPlatform(this IConfiguration config, IWebHostEnvironment env)
        => JObject.Parse(JsonConvert.SerializeObject(config.PlatformModel(env)));

    private static Platform PlatformModel(this IConfiguration config, IWebHostEnvironment env)
    {
        var dto = new Platform
        {
            AppName = env.ApplicationName,

            BasePath = env.ContentRootPath,

            EnvironmentName = env.EnvironmentName,

            FrameworkDescription = RuntimeInformation.FrameworkDescription,

            EnvironmentVariables = config.AsEnumerable().ToDictionary(x => x.Key, x => x.Value),

            OsArchitecture = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) 
                ? RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) 
                    ? $"{nameof(OSPlatform.Linux)} or {nameof(OSPlatform.OSX)}"
                    : "Other OS"
                : nameof(OSPlatform.Windows),

            OsDescription = RuntimeInformation.OSDescription,

            ProcessArchitecture = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.Arm => nameof(Architecture.Arm),
                Architecture.Arm64 => nameof(Architecture.Arm64),
                Architecture.X86 => nameof(Architecture.X86),
                Architecture.X64 => nameof(Architecture.X64),
                _ => "Other Architecture"
            },
            
            HostName = Dns.GetHostName(),

            IpAddress = Dns.GetHostAddresses(Dns.GetHostName())
                .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                .Aggregate(" ", (a, b) => $"{a} {b}")
        };

        return dto;
    }
}