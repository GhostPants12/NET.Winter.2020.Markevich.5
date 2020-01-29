using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DoubleExtension
{
    public static class DoubleExtension
    {
        private const int ByteSize = 8;
        private const int LongDoubleBits = 8 * ByteSize;

        /// <summary>
        /// Represents binary form of System.Double as a string.
        /// </summary>
        /// <param name="value">Number to represent.</param>
        /// <returns>Returns string containing binary form of a number.</returns>
        public static string ToBinary(this double value)
        {
            Union union = new Union(value);
            long bits = union.ToLong();

            return bits.ToBinary();
        }

        /// <summary>
        /// Represents binary form of long integer number as a string.
        /// </summary>
        /// <param name="value">Number to represent.</param>
        /// <returns>Returns string containing binary form of a number.</returns>
        private static string ToBinary(this long value)
        {
            StringBuilder result = new StringBuilder(LongDoubleBits);

            for (int i = 0; i < LongDoubleBits; i++)
            {
                if ((value & 1) == 1)
                {
                    result.Insert(0, "1");
                }
                else
                {
                    result.Insert(0, "0");
                }

                value >>= 1;
            }

            return result.ToString();
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Union
        {
            [FieldOffset(0)]
            private readonly double doubleValue;

            [FieldOffset(0)]
            private readonly long longValue;

            public Union(double value)
                : this()
            {
                this.doubleValue = value;
            }

            public static explicit operator long(Union obj) => obj.longValue;

            public long ToLong() => this.longValue;
        }
    }
}
