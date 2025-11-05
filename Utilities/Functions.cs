using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Elearning.Utilities
{
    public static class Functions
    {
        // Chuyển chuỗi có dấu -> không dấu, thay khoảng trắng bằng '-'
        public static string Slugify(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return "";
            value = value.Trim().ToLowerInvariant();

            // remove diacritics
            var normalized = value.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var ch in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(ch);
            }
            var cleaned = sb.ToString().Normalize(NormalizationForm.FormC);

            // replace invalid chars with '-'
            cleaned = Regex.Replace(cleaned, @"[^a-z0-9\s\-]", "");
            cleaned = Regex.Replace(cleaned, @"\s+", "-");
            cleaned = Regex.Replace(cleaned, @"-+", "-");

            // trim '-' from ends
            return cleaned.Trim('-');
        }

        // GiaoVien slug: prefix-id-name-slug
        public static string GiaoVienSlugGeneration(string prefix, long id, string? name)
        {
            var slugName = Slugify(name ?? $"gv-{id}");
            return $"{prefix}-{id}-{slugName}";
        }
    }
}