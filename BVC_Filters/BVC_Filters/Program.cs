using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVC_Filters
{
    class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data(File.OpenText("..\\..\\data.txt"));
            BloomFilter p = new BloomFilter();
            Console.ReadKey();
        }
    }
}
