namespace Gassignment;

public static class Config
{
    // web
    public const string Slug = "Playwright_(software)";
    public const string BaseUrl = "https://en.wikipedia.org/wiki/";

    // selectors
    public const string NightToggle = "#skin-client-pref-skin-theme-value-night";
    public const string NightClass  = "skin-theme-clientpref-night";

    // Sections
    public const string DebugId    = "Debugging_features";
    public const string DebugStart = "Debugging features";
    public const string DebugEnd   = "Reporters";

    // XPath
    public const string DevToolsPath =
        "//*[@id='mw-content-text']/div[2]/div[17]/table/tbody/tr[7]/td/div/ul/li";

    // API
    public const string ApiUrl      = "https://en.wikipedia.org/w/api.php";
    public const string UserAgent   = "MyApp/1.0 (contact@example.com)";
    public const int    MaxRetries  = 5;
    public const int    RetryDelay  = 2000;
    public const int    DarkTimeout = 5000;
}