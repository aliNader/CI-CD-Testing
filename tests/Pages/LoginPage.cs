using MobileTests.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace MobileTests.Pages;

public class LoginPage : BasePage
{
    public LoginPage(AndroidDriver driver) : base(driver) { }

    // Flutter exposes Text widgets and button labels as content-desc -> Accessibility ID.
    private static By Title        => MobileBy.AccessibilityId("Welcome Back!");
    private static By SignInButton => MobileBy.AccessibilityId("Sign in");

    // Text fields have no usable id, so they are found by position.
    private static By UsernameField => By.XPath("(//android.widget.EditText)[1]");
    private static By PasswordField => By.XPath("(//android.widget.EditText)[2]");

    public bool IsLoaded() => IsVisible(Title);

    public LoginPage EnterUsername(string username)
    {
        Type(UsernameField, username);
        return this;
    }

    public LoginPage EnterPassword(string password)
    {
        Type(PasswordField, password);
        return this;
    }

    public void TapSignIn()
    {
        HideKeyboard();
        Tap(SignInButton);
    }

    public void Login(string username, string password)
    {
        EnterUsername(username).EnterPassword(password);
        TapSignIn();
    }
}
