using Microsoft.Playwright;


namespace Gassignment.Pages;

public class WikipediaPage
{
    private readonly IPage _page;

    public WikipediaPage(IPage page) => _page = page;

    //navigation 

    public async Task GoTo(string slug) =>
        await _page.GotoAsync(Config.BaseUrl + slug);

    // task 1 get text under debug section

    public async Task<string> GetDebugText() =>
        await Utils.UITextExtract.GetSection(
            _page,
            Config.DebugId,   
            Config.DebugEnd
        );

    // task 2 find href elements

    public async Task<List<(string Text, string? Href)>> GetDevLinks()
    {
        var items   = _page.Locator("xpath=" + Config.DevToolsPath);
        int count   = await items.CountAsync();
        var results = new List<(string Text, string? Href)>();

        for (int i = 0; i < count; i++)
        {
            var li   = items.Nth(i);
            string txt  = (await li.Locator("a").First.TextContentAsync() ?? "").Trim();
            string? url = await li.Locator("a").First.GetAttributeAsync("href");
            results.Add((txt, url));
        }

        return results;
    }

    // task 3 validate switch to dark mode

    public async Task SetDark()
    {
        await _page.WaitForSelectorAsync(Config.NightToggle);
        await _page.ClickAsync(Config.NightToggle);
        await _page.WaitForFunctionAsync(
            "document.documentElement.classList.contains('" + Config.NightClass + "')",
            new PageWaitForFunctionOptions { Timeout = Config.DarkTimeout }
        );
    }

    public async Task<string> GetHtmlClass() =>
        await _page.EvalOnSelectorAsync<string>("html", "el => el.className");
}