using Allure.NUnit.Attributes;
using NUnit.Framework;
using Gassignment.Pages;
using Gassignment.Utils;


namespace Gassignment;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
[AllureSuite("Wikipedia Tests")]
[AllureFeature("Playwright Wikipedia")]
public class WikiTest : WikiTestBase
{
    [Test]
    [AllureTag("word-count", "api", "ui")]
    [AllureSeverity(Allure.Net.Commons.SeverityLevel.critical)]
    [AllureDescription("Compares unique word count in the Debugging Features section between UI scrape and Wikipedia API.")]
    public async Task WordCount_Match()
    {
        var uiWords  = TXTNorm.Unique(await WikiPage.GetDebugText());
        var apiWords = TXTNorm.Unique(await ApiExtract.GetDebugText(Config.Slug));

        TestContext.Progress.WriteLine("UI unique word count:  " + uiWords.Count);
        TestContext.Progress.WriteLine("API unique word count: " + apiWords.Count);

        Assert.That(uiWords.Count, Is.EqualTo(apiWords.Count),
            "UI count (" + uiWords.Count + ") does not match API count (" + apiWords.Count + ")");
    }

    [Test]
    [AllureTag("links", "ui")]
    [AllureSeverity(Allure.Net.Commons.SeverityLevel.normal)]
    [AllureDescription("Verifies that every item in the Microsoft dev tools table cell has a hyperlink.")]
    public async Task DevTools_AreLinks()
    {
        var items   = await WikiPage.GetDevLinks();
        var noLinks = items.Where(i => string.IsNullOrEmpty(i.Href)).ToList();

        TestContext.Progress.WriteLine("Total items found:     " + items.Count);
        TestContext.Progress.WriteLine("Items without a link:  " + noLinks.Count);
        foreach (var item in noLinks)
            TestContext.Progress.WriteLine("No link → " + item.Text.Trim());

        Assert.That(noLinks, Is.Empty,
            "Not linked: " + string.Join(", ", noLinks.Select(i => i.Text)));
    }

    [Test]
    [AllureTag("dark-mode", "ui")]
    [AllureSeverity(Allure.Net.Commons.SeverityLevel.minor)]
    [AllureDescription("Clicks the night mode toggle and confirms the dark theme class is applied to the html element.")]
    public async Task DarkMode_Applied()
    {
        await WikiPage.SetDark();
        var cls = await WikiPage.GetHtmlClass();

        Assert.That(cls, Does.Contain(Config.NightClass),
            "Dark mode class was not applied to the html element");
    }
}