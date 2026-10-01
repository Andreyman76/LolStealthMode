using System.Diagnostics;
using System.Text;

namespace LolStealthMode.Models;

internal class LolStealthManager
{
    public string RuleName { get; set; } = "lol_chat";
    public int Port { get; set; } = 5223;

    public bool IsStealthEnabled()
    {
        return SendNetshCommand($"advfirewall firewall show rule name=\"{RuleName}\"")
            .Contains(RuleName);
    }

    public void EnableStealth()
    {
        SendNetshCommand($"advfirewall firewall add rule name=\"{RuleName}\" dir=out remoteport={Port} protocol=TCP action=block");
    }

    public void DisableStealth()
    {
        SendNetshCommand($"advfirewall firewall delete rule name=\"{RuleName}\"");
    }

    private static string SendNetshCommand(string command)
    {
        var info = new ProcessStartInfo
        {
            FileName = "netsh",
            CreateNoWindow = true,
            Arguments = command,
            Verb = "runas",
            StandardOutputEncoding = Encoding.UTF8,
            RedirectStandardOutput = true,
        };

        using var p = Process.Start(info);

        if (p is null)
        {
            return string.Empty;
        }

        var response = p.StandardOutput.ReadToEnd();

        return response;
    }
}