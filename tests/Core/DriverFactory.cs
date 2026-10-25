using MobileTests.Config;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace MobileTests.Core;

public static class DriverFactory
{
    public static AndroidDriver Create()
    {
        var s = Settings.Appium;

        var options = new AppiumOptions
        {
            PlatformName = "Android",
            AutomationName = "UiAutomator2",
            DeviceName = s.DeviceName
        };

        if (!string.IsNullOrEmpty(s.Udid))
            options.AddAdditionalAppiumOption("udid", s.Udid);

        options.AddAdditionalAppiumOption("appPackage", s.AppPackage);
        options.AddAdditionalAppiumOption("appActivity", s.AppActivity);
        options.AddAdditionalAppiumOption("noReset", s.NoReset);
        options.AddAdditionalAppiumOption("autoGrantPermissions", true);
        options.AddAdditionalAppiumOption("newCommandTimeout", 120);

        // Emulators on CI machines are slow: give Appium more time to start its helpers.
        options.AddAdditionalAppiumOption("uiautomator2ServerInstallTimeout", 120000);
        options.AddAdditionalAppiumOption("uiautomator2ServerLaunchTimeout", 120000);
        options.AddAdditionalAppiumOption("adbExecTimeout", 120000);

        return new AndroidDriver(new Uri(s.ServerUrl), options);
    }
}
