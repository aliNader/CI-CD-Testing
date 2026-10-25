using MobileTests.Pages;

namespace MobileTests.Tests;

public class LoginTests : BaseTest
{
    // The demo app accepts demo / Password123. Override with environment variables if you change it.
    private static string User     => Environment.GetEnvironmentVariable("DEMO_USER") ?? "demo";
    private static string Password => Environment.GetEnvironmentVariable("DEMO_PASSWORD") ?? "Password123";

    [Fact]
    [Trait("Category", "Smoke")]
    public void Login_WithValidCredentials_ShowsHomeScreen() => RunTest(() =>
    {
        // Arrange: the app starts on the login screen
        var login = new LoginPage(Driver);
        Assert.True(login.IsLoaded(), "Login screen did not load");

        // Act
        login.Login(User, Password);

        // Assert: the home screen with the greeting appears
        var home = new HomePage(Driver);
        Assert.True(home.IsLoaded(), "Home screen did not appear after login");
        Assert.StartsWith("Hi,", home.GetGreeting());
    });
}
