using System;

namespace NumbersExtension
{
    /// <summary>
    /// An application entry point.
    /// </summary>
    public static class NumbersExtension
    {
        /// <summary>Determines whether the specified value is palindrome.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is palindrome; otherwise, <c>false</c>.</returns>
        public static bool IsPalindrome(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be less than zero");
            }

            int valueCopy = value;
            int decimalPlaces = 1;
            int tenDivider;
            while (valueCopy / 10 != 0)
            {
                valueCopy /= 10;
                decimalPlaces++;
            }

            if (decimalPlaces == 1)
            {
                return true;
            }

            if (decimalPlaces == 2)
            {
                if (value / 10 == value % 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            tenDivider = (int)Math.Pow(10, decimalPlaces - 1);
            if (value / tenDivider == value % 10)
            {
                if ((value % tenDivider) / (tenDivider / 10) == 0)
                {
                    if ((value / 10) % 10 == 0)
                    {
                        return IsPalindrome(((value % tenDivider) / 10) + (tenDivider / 100) + 1);
                    }
                    else
                    {
                        return false;
                    }
                }

                return IsPalindrome((value % tenDivider) / 10);
            }
            else
            {
                return false;
            }
        }
    }
}