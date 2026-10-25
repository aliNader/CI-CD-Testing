using MobileTests.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace MobileTests.Pages;

public class HomePage : BasePage
{
    public HomePage(AndroidDriver driver) : base(driver) { }

    // "Hi, <name>" changes per user, so match only the start.
    private static By Greeting =>
        By.XPath("//android.view.View[starts-with(@content-desc,'Hi,')]");

    public bool IsLoaded() => IsVisible(Greeting);

    public string GetGreeting() => Find(Greeting).GetAttribute("content-desc") ?? "";
}
