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
            Dictionary<string, string> data = Data.GetData(File.ReadAllLines("..\\..\\data.txt"));
            Hasher.size = 40000;
            CuckooFilter cf = new CuckooFilter(Hasher.size);
            int lim = 600;
            int count = 0;
            Stopwatch sw = null;
            foreach (var key in data)
            {
                if (lim-- > 0)
                {
                    cf.Insert(key.Value);
                }
                else
                {
                    if(sw == null)
                    {
                        sw = new Stopwatch();
                        sw.Start();
                    }
                    bool a = cf.Lookup(key.Value);
                    if (a)
                        count++;
                }
            }
            sw.Stop();
            Console.WriteLine("Time " + sw.ElapsedMilliseconds);
            Console.WriteLine("Count " + count);
            BinaryFormatter bf = new BinaryFormatter();
            long size = 0;
            using(Stream s = new MemoryStream())
            {
                bf.Serialize(s, cf.Filter);
                size = s.Length;
            }
            Console.WriteLine("Size " + size);
            Console.ReadKey();
        }
    }
}
