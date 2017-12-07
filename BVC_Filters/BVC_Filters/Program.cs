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
            List<UInt64> list = RandomNoGen.RandomList(10000);
            CuckooFilter cf = new CuckooFilter();
            list.ForEach(x => {
                Console.WriteLine(x);
                if(!cf.IsFull)
                    cf.Insert(x);
            });


            Console.ReadKey();
        }
    }
}
