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

        public static UInt64 Int64Gen()
        {
            if (RandomGenerator == null)
                RandomGenerator = new Random();
            var buffer = new byte[sizeof(UInt64)];
            RandomGenerator.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public static List<UInt64> RandomList(long count) {
            List<UInt64> list = new List<UInt64>();
            while (count-- > 0)
            {
                list.Add(Int64Gen());
            }

            return list;
        }
    }
}
