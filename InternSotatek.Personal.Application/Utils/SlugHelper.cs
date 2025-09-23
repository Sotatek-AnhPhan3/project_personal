using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Application.Utils
{
	public static class SlugHelper
	{
		public static string StringToSlug(string s)
		{
			if (string.IsNullOrEmpty(s))
				return string.Empty;

			string slug = s.ToLowerInvariant();
			slug = RemoveDiacritics(slug);
			slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
			slug = Regex.Replace(slug, @"\s+", "-");
			slug = Regex.Replace(slug, @"-+", "-");
			slug = slug.Trim('-');

			return slug;
		}
		private static string RemoveDiacritics(string text)
		{
			var normalized = text.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();
			foreach (var c in normalized)
			{
				if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
					sb.Append(c);
			}
			return sb.ToString().Normalize(NormalizationForm.FormC);
		}
	}
}
