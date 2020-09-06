using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HardcoreMotivator.BL.Models
{
    internal static class Helper
    {
        
        public static string GenerateTableName(string text)
        {
            text = text.Trim().Replace(" ", "_");

            foreach (var removedChar in RemovedSymbols)
            {
                text = text.Replace(removedChar, '\0');
            }

            var language = LanguageDefiner(text.ToCharArray());

            return language switch
            {
                TextLanguage.English => text,
                TextLanguage.Russian => ConvertRussianTextToTranslite(text),
                _ => Guid.NewGuid().ToString()
            };
        }
        private static TextLanguage LanguageDefiner(char[] text)
        {
            if (text.All(wordByte => wordByte < 128))
            {
                return TextLanguage.English;
            }
            return TextLanguage.Russian;
        }

        private static string ConvertRussianTextToTranslite(string text)
        {
            var translit = new List<string>();
            foreach (var letter in text.ToCharArray())
            {
                translit.Add( ConvertRussianCharToTranslit(letter));
            }
            return string.Join("", translit.ToArray());
        }

        private static string ConvertRussianCharToTranslit(char ch)
        {
            var charToString = ch.ToString();
            if (TranslitDictionary.ContainsKey(charToString))
            {
                return TranslitDictionary[charToString];
            }
            else
            {
                return "_";
            }
        }

        private static readonly char[] RemovedSymbols = new char[]
            {'!', '?', '#', '$', '%', '^', '&', '*', '(', ')', '=', '+', '.', ',', '\\', '"', '\'', ':', ';', '@', '№'};

        private static readonly Dictionary<string, string> TranslitDictionary = new Dictionary<string, string>()
        {
            {"а", "a"},
            {"б", "b"},
            {"в", "v"},
            {"г", "g"},
            {"д", "d"},
            {"е", "e"},
            {"ё", "yo"},
            {"ж", "zh"},
            {"з", "z"},
            {"и", "i"},
            {"й", "j"},
            {"к", "k"},
            {"л", "l"},
            {"м", "m"},
            {"н", "n"},
            {"о", "o"},
            {"п", "p"},
            {"р", "r"},
            {"с", "s"},
            {"т", "t"},
            {"у", "u"},
            {"ф", "f"},
            {"х", "h"},
            {"ц", "c"},
            {"ч", "ch"},
            {"ш", "sh"},
            {"щ", "sch"},
            {"ъ", "j"},
            {"ы", "i"},
            {"ь", "j"},
            {"э", "e"},
            {"ю", "yu"},
            {"я", "ya"},
            {"А", "A"},
            {"Б", "B"},
            {"В", "V"},
            {"Г", "G"},
            {"Д", "D"},
            {"Е", "E"},
            {"Ё", "Yo"},
            {"Ж", "Zh"},
            {"З", "Z"},
            {"И", "I"},
            {"Й", "J"},
            {"К", "K"},
            {"Л", "L"},
            {"М", "M"},
            {"Н", "N"},
            {"О", "O"},
            {"П", "P"},
            {"Р", "R"},
            {"С", "S"},
            {"Т", "T"},
            {"У", "U"},
            {"Ф", "F"},
            {"Х", "H"},
            {"Ц", "C"},
            {"Ч", "Ch"},
            {"Ш", "Sh"},
            {"Щ", "Sch"},
            {"Ъ", "J"},
            {"Ы", "I"},
            {"Ь", "J"},
            {"Э", "E"},
            {"Ю", "Yu"},
            {"Я", "Ya"},
        };
    }
}
