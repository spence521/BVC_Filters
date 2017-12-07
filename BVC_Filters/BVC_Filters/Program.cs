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
            var test_list = list.GetRange(0, 100);
            Console.WriteLine("Generating Test");
            test_list.AddRange(RandomNoGen.RandomList(200000));

            Console.WriteLine("Making Filter");
            //CuckooFilter cf = new CuckooFilter();
            //list.ForEach(x => {
            //    cf.Insert(x);
            //});
            BloomFilter bf = new BloomFilter(100000, hash_size:5);
            list.ForEach(x => bf.Insert(x));


            Console.WriteLine("Checking Filter");
            int counter = 0;
            //test_list.ForEach(x =>
            //{
            //    if (cf.Lookup(x))
            //        counter++;
            //});
            test_list.ForEach(x =>
            {
                if (bf.Lookup(x))
                    counter++;
            });

            Console.WriteLine(counter);
            Console.ReadKey();
        }
    }
}
