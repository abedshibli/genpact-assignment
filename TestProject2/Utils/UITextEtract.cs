using Microsoft.Playwright;

namespace Gassignment.Utils;

public static class UITextExtract
{
    public static async Task<string> GetSection(IPage page, string startId, string endId)
    {
        var parts = await page.EvaluateAsync<string[]>($@"() => {{
            const startEl = document.querySelector('#{startId}');
            const endEl   = document.querySelector('#{endId}');

            if (!startEl || !endEl) return [];

            const start = startEl.closest('.mw-heading');
            const end   = endEl.closest('.mw-heading');

            if (!start || !end) return [];

            const out = [];
            let el = start.nextElementSibling;

            while (el && el !== end) {{
                if (el.matches('p, ul')) {{
                    const clone = el.cloneNode(true);
                    clone.querySelectorAll('sup, a').forEach(n => n.remove());
                    out.push(clone.textContent.trim());
                }}
                el = el.nextElementSibling;
            }}

            return out;
        }}");

        return string.Join(" ", parts);
    }
}