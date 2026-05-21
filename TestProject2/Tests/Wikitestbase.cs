using Allure.NUnit;
using Allure.NUnit.Attributes;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Gassignment.Pages;


namespace Gassignment;

[AllureNUnit]
public class WikiTestBase : PageTest
{
    protected WikipediaPage WikiPage;

    [SetUp]
    public async Task SetUp()
    {
        WikiPage = new WikipediaPage(Page);
        await WikiPage.GoTo(Config.Slug);
    }

    [TearDown]
    public async Task TearDown() => await Page.CloseAsync();
}