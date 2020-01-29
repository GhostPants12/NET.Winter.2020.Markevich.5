using System;

namespace NumbersExtension
{
    /// <summary>
    /// An application entry point.
    /// </summary>
    public static class NumbersExtension
    {
        public const double Epsilon = 1;
        private const int IntMaxBit = (sizeof(int) * 8) - 1;
        private const int IntMinBit = 0;

        /// <summary>Inserts the number into another.</summary>
        /// <param name="numberSource">The number source.</param>
        /// <param name="numberIn">The number in.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>numberSource with inserted numberIn.</returns>
        /// <exception cref="ArgumentOutOfRangeException">i - i is less than zero
        /// or
        /// j - j is less than zero
        /// or
        /// i - i is higher than number of bits in int
        /// or
        /// j - j is higher than number of bits in int.</exception>
        /// <exception cref="ArgumentException">i is higher than j.</exception>
        public static int InsertNumberIntoAnother(int numberSource, int numberIn, int i, int j)
        {
            if (i < IntMinBit || i > IntMaxBit)
            {
                throw new ArgumentOutOfRangeException(nameof(i), "i is out of range");
            }

            if (j < IntMinBit || j > IntMaxBit)
            {
                throw new ArgumentOutOfRangeException(nameof(j), "j is out of range");
            }

            if (i > j)
            {
                throw new ArgumentException("i is higher than j");
            }

            if (i == IntMinBit && j == IntMaxBit)
            {
                return numberIn;
            }

            if (j == IntMaxBit)
            {
                numberSource &= int.MaxValue;
            }

            int bitMaskForNumberIn, bitMaskForNumberSource;
            bitMaskForNumberIn = ~(int.MaxValue << (j + 1 - i)) & int.MaxValue;
            int insertValue = (numberIn & bitMaskForNumberIn) << i;
            if (numberSource >= 0)
            {
                bitMaskForNumberSource = (int.MaxValue >> (IntMaxBit - i)) | ((int.MaxValue << (j + 1)) & int.MaxValue);
            }
            else
            {
                bitMaskForNumberSource = (int.MaxValue >> (IntMaxBit - i)) | (((int.MaxValue << (j + 1)) & int.MaxValue) | (int.MaxValue << (IntMaxBit - 1)));
            }

            int putOutValue = numberSource & bitMaskForNumberSource;
            putOutValue |= insertValue;
            return putOutValue;
        }

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

        /// <summary>Finds the NTH root.</summary>
        /// <param name="value">The value.</param>
        /// <param name="rootPower">The root power.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <returns>Returns the root of the value.</returns>
        /// <exception cref="ArgumentException">Incorrect rootPower
        /// or
        /// number
        /// or accuracy.</exception>
        public static double FindNthRoot(double value, int rootPower, double accuracy)
        {
            if (rootPower <= 0)
            {
                throw new ArgumentOutOfRangeException("RootPower is less than zero");
            }

            if (value == 0 || ((value < 0) && (rootPower % 2 == 0)))
            {
                throw new ArgumentException("Incorrect number");
            }

            if (accuracy <= 0 || accuracy >= Epsilon)
            {
                throw new ArgumentOutOfRangeException("Accuracy is less than zero or higher than Epsilon");
            }

            double prev = value / rootPower;
            double current = (1.0 / rootPower) * (((rootPower - 1) * prev) + (value / Math.Pow(prev, rootPower - 1)));
            while (Math.Abs(current - prev) > accuracy)
            {
                prev = current;
                current = (1.0 / rootPower) * (((rootPower - 1) * prev) + (value / Math.Pow(prev, rootPower - 1)));
            }

            return current;
        }
    }
}