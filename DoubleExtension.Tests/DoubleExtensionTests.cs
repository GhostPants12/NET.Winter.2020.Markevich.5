using System;
using DoubleExtension;
using NUnit.Framework;
using static DoubleExtensions.DoubleExtension;

namespace DoubleExtensions.Tests
{
    public class Tests
    {
        Transformer transformer = new Transformer();
        [TestCase(-255.255, ExpectedResult = "1100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(255.255, ExpectedResult = "0100000001101111111010000010100011110101110000101000111101011100")]
        [TestCase(4294967295.0, ExpectedResult = "0100000111101111111111111111111111111111111000000000000000000000")]
        [TestCase(double.MinValue, ExpectedResult = "1111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.MaxValue, ExpectedResult = "0111111111101111111111111111111111111111111111111111111111111111")]
        [TestCase(double.Epsilon, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000001")]
        [TestCase(double.NaN, ExpectedResult = "1111111111111000000000000000000000000000000000000000000000000000")]
        [TestCase(double.NegativeInfinity, ExpectedResult =
            "1111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(double.PositiveInfinity, ExpectedResult =
            "0111111111110000000000000000000000000000000000000000000000000000")]
        [TestCase(-0.0, ExpectedResult = "1000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(0.0, ExpectedResult = "0000000000000000000000000000000000000000000000000000000000000000")]
        [TestCase(3.14, ExpectedResult = "0100000000001001000111101011100001010001111010111000010100011111")]
        [TestCase(25.2525, ExpectedResult = "0100000000111001010000001010001111010111000010100011110101110001")]
        [TestCase(-0.24, ExpectedResult = "1011111111001110101110000101000111101011100001010001111010111000")]
        [TestCase(75257.23, ExpectedResult = "0100000011110010010111111001001110101110000101000111101011100001")]
        public static string ToBinary_WithAllValidParameters(double number) => number.ToBinary();

        [TestCase(double.NaN, ExpectedResult = "Not a number")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "Negative infinity")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "Positive infinity")]
        [TestCase(-0.0d, ExpectedResult = "zero")]
        [TestCase(0.0d, ExpectedResult = "zero")]
        [TestCase(0.1d, ExpectedResult = "zero point one")]
        [TestCase(-23.809d, ExpectedResult = "minus two three point eight zero nine")]
        [TestCase(-0.123456789d, ExpectedResult = "minus zero point one two three four five six seven eight nine")]
        [TestCase(1.23333e308d, ExpectedResult = "one point two three three three three E plus three zero eight")]
        [TestCase(double.Epsilon, ExpectedResult =
            "Epsilon")]
        [TestCase(double.MaxValue, ExpectedResult = "one point seven nine seven six nine three one three four eight six two three one five seven E plus three zero eight")]
        [TestCase(double.MinValue, ExpectedResult = "minus one point seven nine seven six nine three one three four eight six two three one five seven E plus three zero eight")]
        public string TransformerToWord_WithAllValidParameters(double value)
        {
            return transformer.TransformToWords(value);
        }
    }
}