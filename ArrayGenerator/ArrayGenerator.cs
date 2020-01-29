using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ArrayGeneration
{
    public static class ArrayGenerator
    {
        private const int MaxSize = int.MaxValue / 10;

        /// <summary>Creates the ordered array.</summary>
        /// <param name="firstValue">The first value.</param>
        /// <param name="lastValue">The last value.</param>
        /// <returns>Returns an array containing all numbers between firstValue and lastValue.</returns>
        /// <exception cref="ArgumentException">lastValue is less than firstValue</exception>
        public static int[] CreateOrderedArray(int firstValue, int lastValue)
        {
            if (lastValue < firstValue)
            {
                throw new ArgumentException("lastValue is less than firstValue");
            }

            return Enumerable.Range(firstValue, (lastValue - firstValue) + 1).ToArray();
        }

        /// <summary>Creates the ordered array.</summary>
        /// <param name="firstValue">The first value.</param>
        /// <param name="lastValue">The last value.</param>
        /// <param name="step">The step.</param>
        /// <returns>Returns an array containing all numbers between firstValue and lastValue in increments of step.</returns>
        /// <exception cref="ArgumentException">lastValue is less than firstValue</exception>
        /// <exception cref="ArgumentOutOfRangeException">step - Step cannot be less or equal to zero.</exception>
        public static int[] CreateOrderedArray(int firstValue, int lastValue, int step)
        {
            if (lastValue < firstValue)
            {
                throw new ArgumentException("lastValue is less than firstValue");
            }

            if (step <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(step), "Step cannot be less or equal to zero.");
            }

            int addedNumber = firstValue;
            List<int> outList = new List<int>();
            outList.Add(addedNumber);
            while (addedNumber < lastValue)
            {
                addedNumber += step;
                if (addedNumber > lastValue)
                {
                    addedNumber = lastValue;
                }

                outList.Add(addedNumber);
            }

            return outList.ToArray();
        }

        /// <summary>Creates the random ordered array.</summary>
        /// <param name="size">The size.</param>
        /// <returns>Returns increasing array with random numbers.</returns>
        /// <exception cref="ArgumentOutOfRangeException">size - Size cannot be less or equal to zero or more than MaxSize</exception>
        public static int[] CreateRandomOrderedArray(int size)
        {
            if (size <= 0 || size > MaxSize)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Size cannot be less or equal to zero or more than MaxSize");
            }

            int[] outArray = new int[size];
            Random rand = new Random();
            outArray[0] = rand.Next(0, 10);
            for (int i = 1; i < size; i++)
            {
                outArray[i] = rand.Next(outArray[i - 1], outArray[i - 1] + 10);
            }

            return outArray;
        }

        /// <summary>Creates the array with values that contain numbers.</summary>
        /// <param name="number">The number.</param>
        /// <param name="size">The size.</param>
        /// <returns>Returns the array with elements that contain number.</returns>
        /// <exception cref="ArgumentOutOfRangeException">size - Size cannot be less or equal to zero or more than MaxSize</exception>
        public static int[] CreateArrayWithNumber(int number, int size)
        {
            if (size <= 0 || size > MaxSize)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Size cannot be less or equal to zero or more than MaxSize");
            }

            int[] outArray = new int[size];
            for (int i = 0; i < size; i++)
            {
                outArray[i] = number + (i * 10);
            }

            return outArray;
        }

        /// <summary>Mixes the array.</summary>
        /// <param name="arr">The arr.</param>
        /// <returns>Returns the mixed array.</returns>
        /// <exception cref="ArgumentNullException">arr - Array cannot be null</exception>
        /// <exception cref="ArgumentException">Array should contain at least 2 elements.</exception>
        public static int[] MixArray(int[] arr)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr), "Array cannot be null");
            }

            if (arr.Length <= 1)
            {
                throw new ArgumentException("Array should contain at least 2 elements.");
            }

            int arraySize = arr.Length;
            int buf;
            int randomNumber;
            Random randomElementNumber = new Random();
            for (int i = arraySize - 1; i > 0; i--)
            {
                randomNumber = randomElementNumber.Next(0, i - 1);
                buf = arr[i];
                arr[i] = arr[randomNumber];
                arr[randomNumber] = buf;
            }

            return arr;
        }


        /// <summary>Mixes the array.</summary>
        /// <param name="arr">The arr.</param>
        /// <returns>Returns the mixed array.</returns>
        /// <exception cref="ArgumentNullException">arr - Array cannot be null</exception>
        /// <exception cref="ArgumentException">Array should contain at least 2 elements.</exception>
        public static int[] MixArrayByAnotherArray(int[] arr)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr), "Array cannot be null");
            }

            if (arr.Length <= 1)
            {
                throw new ArgumentException("Array should contain at least 2 elements.");
            }

            int[] bufArray = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                bufArray[i] = arr[i];
            }

            int randomNumber;
            int range = arr.Length - 1;
            Random randomElementNumber = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                randomNumber = randomElementNumber.Next(0, range);
                arr[i] = bufArray[randomNumber];
                for (int j = randomNumber; j < range - 1; j++)
                {
                    bufArray[j] = bufArray[j + 1];
                }

                range--;
            }

            arr[arr.Length - 1] = bufArray[arr.Length - 1];
            return arr;
        }

        /// <summary>Mixes the array by random changes.</summary>
        /// <param name="arr">The array.</param>
        /// <param name="changes">Number of changes.</param>
        /// <returns>Returns the mixed array.</returns>
        /// <exception cref="ArgumentNullException">arr - Array cannot be null</exception>
        /// <exception cref="ArgumentException">Array should contain at least 2 elements.</exception>
        /// <exception cref="ArgumentOutOfRangeException">changes - The number of changes should be 1 or more.</exception>
        public static int[] MixArrayByRandomChanges(int[] arr, int changes)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr), "Array cannot be null");
            }

            if (arr.Length <= 1)
            {
                throw new ArgumentException("Array should contain at least 2 elements.");
            }

            if (changes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(changes), "The number of changes should be 1 or more.");
            }

            int firstRandomNumber;
            int secondRandomNumber;
            int buf;
            Random randomElementNumber = new Random();
            for (int i = 0; i <= changes; i++)
            {
                firstRandomNumber = randomElementNumber.Next(0, arr.Length - 1);
                secondRandomNumber = randomElementNumber.Next(0, arr.Length - 1);
                buf = arr[firstRandomNumber];
                arr[firstRandomNumber] = arr[secondRandomNumber];
                arr[secondRandomNumber] = buf;
            }

            return arr;
        }
    }
}
