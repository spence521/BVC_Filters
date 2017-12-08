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
            Console.WriteLine("Generating Train"); 
            List<UInt64> list = RandomNoGen.RandomList(10000);
            var test_list = list.GetRange(0, 1000);
            Console.WriteLine("Generating Test");
            test_list.AddRange(RandomNoGen.RandomList(20000));

            Console.WriteLine("Making Filters");
            CuckooFilter cf = new CuckooFilter(100000/2, 4);
            int fill_count = 0;
            list.ForEach(x =>
            {
                if (!cf.IsFull)
                {
                    fill_count++;
                    cf.Insert(x);
                }
            });
            Console.WriteLine("Cuckoo Filter full after: " + fill_count);
            BloomFilter bf = new BloomFilter(100000, hash_size: 20);
            list.ForEach(x => bf.Insert(x));


            Console.WriteLine("Checking Filters. Should receive 1000 positives. Anything more than that is false");
            int counter_cf = 0;
            int counter_bf = 0;
            test_list.ForEach(x =>
            {
                if (cf.Lookup(x))
                    counter_cf++;
            });
            test_list.ForEach(x =>
            {
                if (bf.Lookup(x))
                    counter_bf++;
            });

            Console.WriteLine("Cuckoo filter positives: " + counter_cf);
            Console.WriteLine("Bloom filter positives: " + counter_bf);

            cf.Size();
            bf.Size();

            cf.LoadFactor();
            Console.ReadKey(); //Do not need this on linux machine inside the script
        }
    }
}
