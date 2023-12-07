using Murmur;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bloom_Filter
{
    public class BloomFilter
    {
        private int m; // size of array
        private int k; // quantity of hash functions
        private byte[] arr;
        public BloomFilter(int n, double p = 0)
        {
            this.m = calculateSizeOfArray(n, p);
            this.k = calculateNumOfHashFumctions(n);
            arr = new byte[m];
            for (int i = 0; i < m; i++)
            {
                arr[i] = 0; // fill whole array with 0
            }
        }

        public void addString(string str)
        {
            for(int i = 0; i < k; i++)
            {
                int index = hashing(str, i) % m; // calculating modus so it will not go out of bounds
                if(index < 0) // for negetive numbers
                {
                    index += m;
                }
                arr[index] = 1;
            }
        }

        public bool testString(string str)
        {
            for (int i = 0;i < k; i++)
            {
                int index = hashing(str, i) % m; // calculating modus so it will not go out of bounds
                if (index < 0) // for negative numbers
                {
                    index += m;
                }
                if (arr[index] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private int calculateSizeOfArray(int n, double p)
        {
            return (int) Math.Round(- n * Math.Log(p, 2) / Math.Log(2));
        }

        private int calculateNumOfHashFumctions(int n)
        {
            return (int)((m/(double)n) * Math.Log(2));
        }

        private int hashing(string str, int seed)
        {
            HashAlgorithm murmur = MurmurHash.Create128((uint)seed);
            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            byte[] hashArr = murmur.ComputeHash(strBytes);
            return BitConverter.ToInt32(hashArr, 0);
        }

        public int M {  get { return m; } }
        public int K { get { return k; } }
    }
}
