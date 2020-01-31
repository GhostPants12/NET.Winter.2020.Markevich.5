using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DoubleExtension
{
    public class Transformer
    {
        private readonly Dictionary<char, string> characterToWord = new Dictionary<char, string>
        {
            { '+', "plus" },
            { '-', "minus" },
            { '.', "point" },
            { '0', "zero" },
            { '1', "one" },
            { '2', "two" },
            { '3', "three" },
            { '4', "four" },
            { '5', "five" },
            { '6', "six" },
            { '7', "seven" },
            { '8', "eight" },
            { '9', "nine" },
            { 'E', "E" },
        };

        public string TransformToWords(double value)
        {
            if (value.ToString(CultureInfo.InvariantCulture) == double.Epsilon.ToString(CultureInfo.InvariantCulture))
            {
                return "Epsilon";
            }

            if (double.IsNaN(value))
            {
                return "Not a number";
            }

            if (double.IsPositiveInfinity(value))
            {
                return "Positive infinity";
            }

            if (double.IsNegativeInfinity(value))
            {
                return "Negative infinity";
            }

            if (value == 0)
            {
                return "zero";
            }

            string result = string.Empty;
            string valueAsString = value.ToString(CultureInfo.InvariantCulture);
            foreach (char ch in valueAsString)
            {
                foreach (KeyValuePair<char, string> keyValue in this.characterToWord)
                {
                    if (ch == keyValue.Key)
                    {
                        result += keyValue.Value + " ";
                    }
                }
            }

            return result.Substring(0, result.Length - 1);
        }
    }
}
