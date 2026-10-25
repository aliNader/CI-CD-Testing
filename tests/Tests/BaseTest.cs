using System.Runtime.CompilerServices;
using MobileTests.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace MobileTests.Tests;

public abstract class BaseTest : IDisposable
{
    protected readonly AndroidDriver Driver;

    // xUnit creates a new instance of the class for every test,
    // so the constructor is the setup and Dispose is the teardown.
    protected BaseTest()
    {
        Driver = DriverFactory.Create();
    }

    public void Dispose()
    {
        try { Driver.Quit(); }
        finally { Driver.Dispose(); }
        GC.SuppressFinalize(this);
    }

    // Wraps a test body so a screenshot is saved when an assertion fails.
    protected void RunTest(Action test, [CallerMemberName] string name = "")
    {
        try
        {
            test();
        }
        catch
        {
            SaveScreenshot(name);
            throw;
        }
    }

    private void SaveScreenshot(string name)
    {
        try
        {
            var dir = Path.Combine(AppContext.BaseDirectory, "screenshots");
            Directory.CreateDirectory(dir);
            Driver.GetScreenshot().SaveAsFile(Path.Combine(dir, name + ".png"));
        }
        catch (WebDriverException) { }
    }
}
