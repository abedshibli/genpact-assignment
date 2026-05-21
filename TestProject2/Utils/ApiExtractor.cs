using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gassignment.Utils;

public static class ApiExtract
{
    private static readonly HttpClient Client = MakeClient();

    private static HttpClient MakeClient()
    {
        var c = new HttpClient();
        c.DefaultRequestHeaders.Add("User-Agent", Config.UserAgent);
        return c;
    }

    public static async Task<string> GetDebugText(string title)
    {
        string url =
            Config.ApiUrl +
            "?action=query" +
            $"&titles={Uri.EscapeDataString(title)}" +
            "&prop=extracts" +
            "&explaintext=true" +
            "&format=json";

        for (int i = 0; i < Config.MaxRetries; i++)
        {
            var resp = await Client.GetAsync(url);

            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                return ParseSection(json, Config.DebugStart, Config.DebugEnd);
            }

            if ((int)resp.StatusCode == 429)
            {
                await Task.Delay(Config.RetryDelay * (i + 1));
                continue;
            }

            resp.EnsureSuccessStatusCode();
        }

        return string.Empty;
    }

    private static string ParseSection(string json, string start, string end)
    {
        using var doc = JsonDocument.Parse(json);

        var pages = doc.RootElement
            .GetProperty("query")
            .GetProperty("pages");

        foreach (var page in pages.EnumerateObject())
        {
            string txt = page.Value.GetProperty("extract").GetString() ?? "";

            int s = txt.IndexOf(start, StringComparison.OrdinalIgnoreCase);
            int e = txt.IndexOf(end,   StringComparison.OrdinalIgnoreCase);

            if (s == -1 || e == -1 || e <= s) return string.Empty;

            return txt[s..e]
                .Replace(start + " ===", "")
                .Trim();
        }

        return string.Empty;
    }
}