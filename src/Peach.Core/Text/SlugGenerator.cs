using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Peach.Core.Text
{
    public class SlugGenerator : ISlugGenerator
    {
        public string Generate(string text)
        {
            if (String.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            text = text.ToLowerInvariant();

            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);
            text = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            text = Regex.Replace(text, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            text = Regex.Replace(text, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            text = text.Trim('-', '_');

            //Replace double occurences of - or _
            text = Regex.Replace(text, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return text;
        }
    }
}