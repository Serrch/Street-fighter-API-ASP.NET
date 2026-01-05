using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SF_API.Utils
{
    public static class StringUtils
    {
        public static string Slugify(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.ToLowerInvariant();
            text = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in text)
            {
                var uc = Char.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            text = sb.ToString().Normalize(NormalizationForm.FormC);

            text = Regex.Replace(text, @"\s+", "_");

            text = Regex.Replace(text, @"[^a-z0-9_\-]", "");

            return text;
        }
    }
}
