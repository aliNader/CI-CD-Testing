using MobileTests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace MobileTests.Core;

public abstract class BasePage
{
    protected readonly AndroidDriver Driver;
    protected readonly WebDriverWait Wait;

    protected BasePage(AndroidDriver driver)
    {
        Driver = driver;
        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Settings.WaitSeconds));
    }

    protected IWebElement Find(By by) =>
        Wait.Until(d =>
        {
            var e = d.FindElements(by).FirstOrDefault();
            return e != null && e.Displayed ? e : null;
        })!;

    protected void Tap(By by) => Find(by).Click();

    protected void Type(By by, string text)
    {
        var el = Find(by);
        el.Click();           // Flutter fields need focus first
        el.SendKeys(text);
    }

    protected bool IsVisible(By by)
    {
        try { Find(by); return true; }
        catch (WebDriverTimeoutException) { return false; }
    }

    protected void HideKeyboard()
    {
        try { Driver.ExecuteScript("mobile: hideKeyboard"); }
        catch (Exception) { /* keyboard was not shown */ }
    }
}
