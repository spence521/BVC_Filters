using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BVC_Filters
{
    public class BloomFilter
    {
        private static byte[] Bloom_Filter { get; set; }

        public BloomFilter(Dictionary<string, string> dict)
        {
            Stopwatch a = new Stopwatch();
            //System.Threading.Thread.Sleep(2000);
            Dictionary<string, string> Train = new Dictionary<string, string>();
            Dictionary<string, string> Test = new Dictionary<string, string>();
            int i = 0;
            foreach (var item in dict)
            {
                if(i < 650)
                {
                    Train.Add(item.Key, item.Value);
                }
                else
                {
                    Test.Add(item.Key, item.Value);
                }
                i++;
            }

            Bloom_Filter = new byte[1000000];
            foreach (var item in Train)
            {
                AddItem(item.Value);
            }
            int false_positives_count = 0;

            a.Start();
            foreach (var item in Test)
            {
                if(PossiblyExists(item.Value))
                {
                    false_positives_count++;
                    //Console.WriteLine(item.Key);
                    //Console.WriteLine(item.Value);
                    //Console.WriteLine(item.Value.GetHashCode());
                }
            }
            int storage = Bloom_Filter.Count();//Bloom_Filter.Where(x => x == (byte)1).Count();
            //Console.WriteLine(PossiblyExists("whatever"));
            //Console.WriteLine(PossiblyExists("test"));
            //Console.WriteLine(PossiblyExists("test2"));
            //Console.WriteLine(PossiblyExists("test3"));
            //Console.WriteLine(PossiblyExists("test4"));
            //Console.WriteLine(PossiblyExists("test5"));
            //Console.WriteLine(PossiblyExists("test6"));
            Console.WriteLine("The amount of Space used is:\n\t" + storage);
            Console.WriteLine("The number of False Positives are:\n\t" + false_positives_count);
            a.Stop();
            Console.WriteLine("Our Bloom Filter Algorithm Takes a total of:\n\t" + a.ElapsedMilliseconds + " Milliseconds");
            Console.ReadKey();
        }

        static void AddItem(string item)
        {
            int hash = item.GetHashCode() & 0x7FFFFFFF; // strips signed bit
            //Console.WriteLine(hash);
            byte bit = (byte)(1 << (hash & 7)); // you have 8 bits
            Bloom_Filter[hash % Bloom_Filter.Length] |= bit;
        }

        static bool PossiblyExists(string item)
        {
            int hash = item.GetHashCode() & 0x7FFFFFFF;
            byte bit = (byte)(1 << (hash & 7)); // you have 8 bits;
            return (Bloom_Filter[hash % Bloom_Filter.Length] & bit) != 0;
        }

        //private static int Hash(string item)
        //{
        //    int result = 51;
        //    for (int i = 0; i < item.Length; i++)
        //    {
        //        unchecked
        //        {
        //            result *= item[i];
        //        }
        //    }
        //    return result;
        //}
    }
}
