using System;
using System.Collections.Generic;

namespace TabML.Core.MusicTheory
{
    public static class AlternationText
    {
        public const int NoAlternationIndex = 0;

        private static readonly Dictionary<AlternationTextType, string[]> AlternationTexts
            = new Dictionary<AlternationTextType, string[]>
            {
                {AlternationTextType.Arabic, new[] {"1", "2", "3", "4", "5", "6", "7", "8", "9"}},
                {AlternationTextType.RomanUpper, new[] { "Ⅰ","Ⅱ","Ⅲ","Ⅳ","Ⅴ","Ⅵ","Ⅶ","Ⅷ","Ⅸ"}},
                {AlternationTextType.RomanLower, new[] {"ⅰ","ⅱ","ⅲ","ⅳ","ⅴ","ⅵ","ⅶ","ⅷ","ⅸ"}},
            };

        private static readonly Dictionary<string, int> AlternationTextIndices =
            new Dictionary<string, int>
            {
                {"1", 1},
                {"2", 2},
                {"3", 3},
                {"4", 4},
                {"5", 5},
                {"6", 6},
                {"7", 7},
                {"8", 8},
                {"9", 9},
                {"I", 1 },
                {"II", 2},
                {"III", 3},
                {"IV", 4},
                {"V", 5},
                {"VI", 6},
                {"VII", 7},
                {"VIII", 8},
                {"IX", 9},
                {"i", 1},
                {"ii", 2},
                {"iii", 3},
                {"iv", 4},
                {"v", 5},
                {"vi", 6},
                {"vii", 7},
                {"viii", 8},
                {"ix", 9},
                {"Ⅰ", 1},
                {"Ⅱ", 2},
                {"Ⅲ", 3},
                {"Ⅳ", 4},
                {"Ⅴ", 5},
                {"Ⅵ", 6},
                {"Ⅶ", 7},
                {"Ⅷ", 8},
                {"Ⅸ", 9},
                {"ⅰ", 1},
                {"ⅱ", 2},
                {"ⅲ", 3},
                {"ⅳ", 4},
                {"ⅴ", 5},
                {"ⅵ", 6},
                {"ⅶ", 7},
                {"ⅷ", 8},
                {"ⅸ", 9}
            };

        private static readonly Dictionary<string, AlternationTextType> AlternationTextTypes =
            new Dictionary<string, AlternationTextType>
            {
                {"1", AlternationTextType.Arabic},
                {"2", AlternationTextType.Arabic},
                {"3", AlternationTextType.Arabic},
                {"4", AlternationTextType.Arabic},
                {"5", AlternationTextType.Arabic},
                {"6", AlternationTextType.Arabic},
                {"7", AlternationTextType.Arabic},
                {"8", AlternationTextType.Arabic},
                {"9", AlternationTextType.Arabic},
                {"I", AlternationTextType.RomanUpper},
                {"II", AlternationTextType.RomanUpper},
                {"III", AlternationTextType.RomanUpper},
                {"IV", AlternationTextType.RomanUpper},
                {"V", AlternationTextType.RomanUpper},
                {"VI", AlternationTextType.RomanUpper},
                {"VII", AlternationTextType.RomanUpper},
                {"VIII", AlternationTextType.RomanUpper},
                {"IX", AlternationTextType.RomanUpper},
                {"i", AlternationTextType.RomanLower},
                {"ii", AlternationTextType.RomanLower},
                {"iii", AlternationTextType.RomanLower},
                {"iv", AlternationTextType.RomanLower},
                {"v", AlternationTextType.RomanLower},
                {"vi", AlternationTextType.RomanLower},
                {"vii", AlternationTextType.RomanLower},
                {"viii", AlternationTextType.RomanLower},
                {"ix", AlternationTextType.RomanLower},
                {"Ⅰ", AlternationTextType.RomanUpper},
                {"Ⅱ", AlternationTextType.RomanUpper},
                {"Ⅲ", AlternationTextType.RomanUpper},
                {"Ⅳ", AlternationTextType.RomanUpper},
                {"Ⅴ", AlternationTextType.RomanUpper},
                {"Ⅵ", AlternationTextType.RomanUpper},
                {"Ⅶ", AlternationTextType.RomanUpper},
                {"Ⅷ", AlternationTextType.RomanUpper},
                {"Ⅸ", AlternationTextType.RomanUpper},
                {"ⅰ", AlternationTextType.RomanLower},
                {"ⅱ", AlternationTextType.RomanLower},
                {"ⅲ", AlternationTextType.RomanLower},
                {"ⅳ", AlternationTextType.RomanLower},
                {"ⅴ", AlternationTextType.RomanLower},
                {"ⅵ", AlternationTextType.RomanLower},
                {"ⅶ", AlternationTextType.RomanLower},
                {"ⅷ", AlternationTextType.RomanLower},
                {"ⅸ", AlternationTextType.RomanLower}
            };

        public static bool TryParse(string text, out int index, out AlternationTextType type)
        {
            if (!AlternationTextIndices.TryGetValue(text, out index))
            {
                type = AlternationTextType.Arabic;
                return false;
            }

            type = AlternationTextTypes[text];
            return true;
        }

        public static bool IsValid(string text)
        {
            int index;
            return AlternationTextIndices.TryGetValue(text, out index);
        }

        public static string GetAlternationText(AlternationTextType type, int index)
        {
            if (index < 1 || index > 9)
                throw new ArgumentOutOfRangeException(nameof(index));

            return AlternationTexts[type][index - 1];
        }
    }
}

