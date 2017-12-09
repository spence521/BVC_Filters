using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVC_Filters
{
    public static class RandomNoGen
    {
        private static Random RandomGenerator { get; set; }

        public static int Int64Gen()
        {
            if (RandomGenerator == null)
                RandomGenerator = new Random();
            var buffer = new byte[sizeof(int)];
            RandomGenerator.NextBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static List<int> RandomList(long count) {
            List<int> list = new List<int>();
            while (count-- > 0)
            {
                list.Add(Int64Gen());
            }

            return list;
        }
    }
}
