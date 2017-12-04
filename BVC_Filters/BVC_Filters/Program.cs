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
            Dictionary<string, string> data = Data.GetData(File.ReadAllLines("..\\..\\data.txt"));
            foreach(var k in data)
            {
                Console.WriteLine(k.Key);
                Console.WriteLine(k.Value);
            }
            Console.ReadKey();
        }
    }
}
