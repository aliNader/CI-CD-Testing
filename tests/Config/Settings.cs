using Microsoft.Extensions.Configuration;

namespace MobileTests.Config;

public class AppiumSettings
{
    public string ServerUrl { get; set; } = "http://127.0.0.1:4723";
    public string DeviceName { get; set; } = "";
    public string Udid { get; set; } = "";
    public string AppPackage { get; set; } = "";
    public string AppActivity { get; set; } = "";
    public bool NoReset { get; set; }
}

public static class Settings
{
    // appsettings.json first, then environment variables override it.
    // Example: the variable Appium__Udid=emulator-5554 overrides Appium:Udid.
    private static readonly IConfigurationRoot Root = new ConfigurationBuilder()
        .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: false)
        .AddEnvironmentVariables()
        .Build();

    public static AppiumSettings Appium =>
        Root.GetSection("Appium").Get<AppiumSettings>() ?? new AppiumSettings();

    public static int WaitSeconds => Root.GetValue<int?>("WaitSeconds") ?? 30;
}
