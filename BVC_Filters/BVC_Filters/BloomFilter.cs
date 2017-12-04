using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVC_Filters
{
    public class BloomFilter
    {
        private static byte[] Bloom_Filter { get; set; }

        public BloomFilter()
        {
            Bloom_Filter = new byte[1000];
            AddItem("test");
            AddItem("test2");
            AddItem("test3");
            AddItem("test4");
            AddItem("test5");

            Console.WriteLine(PossiblyExists("whatever"));
            Console.WriteLine(PossiblyExists("test"));
            Console.WriteLine(PossiblyExists("test2"));
            Console.WriteLine(PossiblyExists("test3"));
            Console.WriteLine(PossiblyExists("test4"));
            Console.WriteLine(PossiblyExists("test5"));
            Console.WriteLine(PossiblyExists("test6"));
            Console.ReadKey();
        }

        static void AddItem(string item)
        {
            int hash = Hash(item) & 0x7FFFFFFF; // strips signed bit
            byte bit = (byte)(1 << (hash & 7)); // you have 8 bits
            Bloom_Filter[hash % Bloom_Filter.Length] |= bit;
        }

        static bool PossiblyExists(string item)
        {
            int hash = Hash(item) & 0x7FFFFFFF;
            byte bit = (byte)(1 << (hash & 7)); // you have 8 bits;
            return (Bloom_Filter[hash % Bloom_Filter.Length] & bit) != 0;
        }

        private static int Hash(string item)
        {
            int result = 51;
            for (int i = 0; i < item.Length; i++)
            {
                unchecked
                {
                    result *= item[i];
                }
            }
            return result;
        }
    }
}
