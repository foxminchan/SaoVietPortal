using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SaoViet.Portal.Infrastructure.System.Models;

namespace SaoViet.Portal.Infrastructure.System;

public static class PlatformExtension
{
    private const string UnsupportedOs = "This OS is not supported";

    public static JObject GetPlatform(this IConfiguration config, IWebHostEnvironment env)
        => JObject.Parse(JsonConvert.SerializeObject(config.PlatformModel(env)));

    public static JObject GetPlatformStatus(IWebHostEnvironment env)
        => JObject.Parse(JsonConvert.SerializeObject(ServerModel(env)));

    private static Platform PlatformModel(this IConfiguration config, IHostEnvironment env)
    {
        return new Platform
        {
            AppName = env.ApplicationName,

            BasePath = env.ContentRootPath,

            EnvironmentName = env.EnvironmentName,

            FrameworkDescription = RuntimeInformation.FrameworkDescription,

            EnvironmentVariables = config.AsEnumerable()
                .ToDictionary(x => x.Key, x => x.Value),

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
    }

    private static Server ServerModel(IHostEnvironment env)
        => new()
        {
            Name = env.EnvironmentName,
            Time = DateTime.Now,
            UpTime = DateTime.Now - Process.GetCurrentProcess().StartTime,
            CpuUsage = GetCpuUsage(),
            MemoryUsage = GetMemoryUsage(),
            DiskUsage = GetDiskUsage()
        };

    private static object GetCpuUsage()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            Thread.Sleep(1000);
            return $"{cpuCounter.NextValue()}%";
        }

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && !RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return UnsupportedOs;

        var cpuUsage = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = "-c \"top -bn2 | grep 'Cpu(s)'|tail -n 1 | awk '{print $2 + $4}'\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        cpuUsage.Start();

        return $"{cpuUsage.StandardOutput.ReadToEnd()}%";
    }

    private static object GetMemoryUsage()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var memoryCounter = new PerformanceCounter("Memory", "Available MBytes");
            memoryCounter.NextValue();
            Thread.Sleep(1000);
            return $"{memoryCounter.NextValue()}MB";
        }

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && !RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return UnsupportedOs;
        var memoryUsage = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = "-c \"free -m | awk 'NR==2{printf \"%.2f\", $3*100/$2 }'\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        memoryUsage.Start();
        return $"{memoryUsage.StandardOutput.ReadToEnd()}%";
    }

    private static object GetDiskUsage()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var drive = new DriveInfo(Directory.GetCurrentDirectory());
            var totalSize = drive.TotalSize;
            var totalFreeSpace = drive.TotalFreeSpace;
            var usedSpace = totalSize - totalFreeSpace;
            return $"{usedSpace} bytes ({(double)usedSpace / totalSize * 100}%)";
        }

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && !RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return UnsupportedOs;

        var diskUsage = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = "-c \"df -h | awk '$NF==\"/\"{printf \"%s\", $5}'\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        diskUsage.Start();
        return $"{diskUsage.StandardOutput.ReadToEnd()}";
    }
}