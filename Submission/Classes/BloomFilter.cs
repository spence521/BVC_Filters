using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVC_Filters
{
    public class BloomFilter
    {
        private byte[] Filter { get; set; }
        private int hash_size;
        private int size;
        public BloomFilter(int filter_size, int hash_size = 10)
        {
            size = filter_size;
            this.hash_size = hash_size;
            Filter = new byte[filter_size + 1];
        }

        public void Insert(ulong item)
        {
            int[] hash_indices = Hasher.BloomHash(item, size, hash_size);

            for (int i = 0; i < hash_indices.Length; i++)
            {
                Filter[hash_indices[i]] = 1;
            }
        }

        public bool Lookup(ulong item)
        {
            int[] hash_indices = Hasher.BloomHash(item, size, hash_size);
            for (int i = 0; i < hash_indices.Length; i++)
            {
                if (Filter[hash_indices[i]] != 1)
                    return false;
            }
            return true;
        }

        public void Size()
        {
            Console.WriteLine("Size of bloom filter: " + size * sizeof(byte));
        }

    }
}
