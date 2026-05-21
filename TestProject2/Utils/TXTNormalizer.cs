using System.Collections.Generic;
using System.Text.RegularExpressions;
 
namespace Gassignment.Utils;
 
public static class TXTNorm
{
    public static HashSet<string> Unique(string text)
    {
        var words = new HashSet<string>();
        foreach (Match m in Regex.Matches(text.ToLowerInvariant(), @"\b[a-z]+\b"))
            words.Add(m.Value);
        return words;
    }
}