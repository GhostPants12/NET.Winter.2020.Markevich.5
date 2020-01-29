using NUnit.Framework;
using System;
using System.Linq;
using static ArrayExtension.ArrayExtension;
using static ArrayGeneration.ArrayGenerator;

namespace ArrayExtension.Tests
{

    public class ArrayExtensionTests
    {
        private static int[] generatedArray = CreateArrayWithNumber(5, 10_000_000);

        #region FindBalanceIndexTests
        [TestCase(new int[10] { 10, 1, 2, 2, 1, 1, 1, 1, 1, 1 }, ExpectedResult = 1)]
        [TestCase(new int[5] { 1, 2, 3, 4, 6 }, ExpectedResult = 3)]
        [TestCase(new int[2] { 0, 0 }, ExpectedResult = null)]
        [TestCase(new int[3] { 0, 0, 0 }, ExpectedResult = 1)]
        [TestCase(new int[1] { 1 }, ExpectedResult = null)]
        [TestCase(new int[3] { 0,0,1 }, ExpectedResult = null)]
        [TestCase(new int[3] { int.MaxValue, int.MaxValue, int.MaxValue}, ExpectedResult =1 )]
        public int? FindBalanceIndex_WithAllValidParameters(int[] arr) =>
            FindBalanceIndex(arr);
        #endregion

        #region FindMaximumElementTests
        [TestCase(new int[5] { 1, 2, 3, 4, 5 }, ExpectedResult = 5)]
        [TestCase(new int[3] { 99999998, 99999999, 100000000 }, ExpectedResult = 100000000)]
        [TestCase(new int[10] { -1,2,-3,4,-5,6,-7,8,-9,10}, ExpectedResult =10)]
        public static int FindMaximumElement_WithAllValidParameters(int[] arr)
        {
            return FindMaximumElement(arr);
        }

        [Test]
        public static void FindMaximumElement_WithGeneratedArray()
        {
            int firstResult = FindMaximumElement(MixArray(CreateOrderedArray(0, 100000)));
            int secondResult = FindMaximumElement(MixArrayByRandomChanges(CreateOrderedArray(0, 100000, 10), 10000));
            int expectedResult = 100000;
            Assert.AreEqual(expectedResult,firstResult);
            Assert.AreEqual(expectedResult, secondResult);
        }
        #endregion

        #region FilterArrayByKeyTests
        [TestCase(new[] { 2212332, 1405644, -1236674 }, 0, ExpectedResult = new[] { 1405644 })]
        [TestCase(new[] { 53, 71, -24, 1001, 32, 1005 }, 2, ExpectedResult = new[] { -24, 32 })]
        [TestCase(new[] { -27, 173, 371132, 7556, 7243, 10017 }, 7, ExpectedResult = new[] { -27, 173, 371132, 7556, 7243, 10017 })]
        [TestCase(new[] { 7, 2, 5, 5, -1, -1, 2 }, 9, ExpectedResult = new int[0])]
        [TestCase(new[] { 15, 25, 60, 74, 189, int.MinValue, 32 }, 2, ExpectedResult = new[] { 25, int.MinValue, 32 })]
        public static int[] FilterArrayByKey_WithAllValidParameters(int[] arr, int key)
        {
            return FilterArrayByKey(arr, key);
        }

        [TestCase(new[] {101, 1551, 82028, 100, 1890, 1570}, ExpectedResult = new[] {101, 1551, 82028})]
        [TestCase(new[] {100, 200, 300, 400 }, ExpectedResult = new int[] { })]
        public static int[] FilterArrayByKey_WithAllValidParameters_Palindrome(int[] arr)
        {
            return FilterArrayByKey(arr);
        }
        [Test]
        public static void FilterArrayByKey_WithEmptyArray()=>Assert.Throws<ArgumentException>(() 
            => ArrayExtension.FilterArrayByKey(new int[0], 0));
        [Test]
        public static void FilterArrayByKey_WithNegativeKey() => Assert.Throws<ArgumentOutOfRangeException>(()
              => ArrayExtension.FilterArrayByKey(new int[] { 1, 2 }, -1));
        [Test]
        public static void FilterArrayByKey_KeyMoreThan9() => Assert.Throws<ArgumentOutOfRangeException>(()
              => ArrayExtension.FilterArrayByKey(new int[] { 1, 2 }, 100));
        [Test]
        public static void FilterArrayByKey_WithNullArray() => Assert.Throws<ArgumentNullException>(()
              => ArrayExtension.FilterArrayByKey(null, 0));
        //1 sec
        [Test]
        public static void FilterArrayByKey_WithAllValidParameters_BigArray()
        {
            int[] arr = new int[100_000_000];
            for (int i = 0; i < arr.Length; i ++)
            {
                arr[i] = 10;
            }
            for (int i=0;i<arr.Length;i+=20_000_000)
            {
                arr[i] = 8;
            }

            int[] result = FilterArrayByKey(arr);
            int[] resultForKey = FilterArrayByKey(arr, 8);
            int[] expected = { 8, 8, 8, 8, 8 };
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected,resultForKey);
        }

        //3 sec
        [Test]
        public static void FilterArrayByKey_WithGeneratedArray()
        {
            Assert.AreEqual(generatedArray, FilterArrayByKey(generatedArray, 5));
        }
        #endregion

        #region ArrayGeneratorTests
        //16 ms
        [Test]
        public static void TestArrayGenerator()
        {
            CreateArrayWithNumber(5, 10_000_000);
        }
        #endregion
    }
}