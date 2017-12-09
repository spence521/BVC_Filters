using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BVC_Filters
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating filter members randomly."); 
            List<int> to_add = RandomNoGen.RandomList(1000);
            var test_list = to_add.GetRange(0, 200);
            Console.WriteLine("Generating test elements.");
            test_list.AddRange(RandomNoGen.RandomList(2000));
            Console.WriteLine("");
            Console.WriteLine("");

            CuckooFilterRun(to_add, test_list);
            Console.WriteLine("");
            Console.WriteLine("");
            BloomFilterRun(to_add, test_list);
           
            Console.ReadKey(); //Do not need this on linux machine inside the script
        }

        static void CuckooFilterRun(List<int> filter_members, List<int> test_elements, int filter_size=1000, int bucket_size=4)
        {
            Console.WriteLine("Making cuckoo filter");
            CuckooFilter cf = new CuckooFilter(filter_size, bucket_size);
            int fill_count = 0;
            filter_members.ForEach(x =>
            {
                if (!cf.IsFull)
                {
                    fill_count++;
                    cf.Insert(x);
                }
            });
            Console.WriteLine("Cuckoo Filter full after: " + fill_count);
            Console.WriteLine("Checking Filters. Should receive 200 positives. Anything more than that is false");
            int counter_cf = 0;
            test_elements.ForEach(x =>
            {
                if (cf.Lookup(x))
                    counter_cf++;
            });

            cf.Size();
            cf.LoadFactor();
            Console.WriteLine("Cuckoo filter positives: " + counter_cf);
        }

        static void BloomFilterRun(List<int> filter_members, List<int> test_elements, int filter_size=100000, int hash_size=1000)
        {
            Console.WriteLine("Making bloom filter");
            BloomFilter bf = new BloomFilter(filter_size, hash_size: hash_size);
            filter_members.ForEach(x => bf.Insert(x));


            Console.WriteLine("Checking Filters. Should receive 200 positives. Anything more than that is false");
            int counter_bf = 0;
            test_elements.ForEach(x =>
            {
                if (bf.Lookup(x))
                    counter_bf++;
            });

            Console.WriteLine("Bloom filter positives: " + counter_bf);
            bf.Size();
        }
    }
}
