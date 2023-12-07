using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bloom_Filter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = -1;
            double p = -1;
            while (n < 0) // check if user entered right number
            {
                Console.Write("Enter n: ");
                n = Convert.ToInt32(Console.ReadLine());
            }
            while(p < 0 || p > 1) // check if user entered right number
            {
                Console.Write("Enter probability: ");
                p = Convert.ToDouble(Console.ReadLine());
            }
            BloomFilter bloomFilter = new BloomFilter(n, p); // creating an object of class

            string file = "words.txt";
            string[] stringArr = File.ReadAllLines(file); // reading strings from file
            for (int i = 0; i < stringArr.Length; i++)
            {
                bloomFilter.addString(stringArr[i]); // adding them to filter
            }
            // testing on a big number of string that are not there
            Console.WriteLine($"Size of filter: {bloomFilter.M}\nNumber of hash functions: {bloomFilter.K}\nExperemental probability of error: {testBigNumber(bloomFilter), 0:F2}");

            string str = Console.ReadLine();  // added so console will not close immediately in the end 
        }

        public static double testBigNumber(BloomFilter bloomFilter)
        {
            int num = 1000000;
            int numOfFalse = 0;

            for(int i = 0; i < num; i++)
            {
                if (bloomFilter.testString("notinfilter" + i))
                {
                    numOfFalse++; // add if wrong classified
                }
            }

            return (double)numOfFalse / (double)num;
        }
    }
}
