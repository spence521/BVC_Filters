using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BVC_Filters
{

    public class HashCombo
    {
        public ulong Object { get; set; }
        public ulong Fingerprint { get; set; }
        public int Hash1 { get; set; }
        public int Hash2 { get; set; }

        public HashCombo(ulong item, int upper_lim)
        {
            Object = item;
            //Fingerprint = Object;
            Fingerprint = Hasher.Fingerprint(item);
            Hash1 = Hasher.GetHash(item, upper_lim);
            Hash2 = Hasher.MapInt(Hash1 ^ Hasher.GetHash(Fingerprint, upper_lim), upper_lim);
        }
    }

    public class CuckooFilter
    {
        private static Random rand = new Random();
        private int replace_counter = 500;

        public ulong[] Filter { get; set; }
        public bool IsFull = false;
        private int size;
        public CuckooFilter(int size)
        {
            this.size = size;
            Filter = new ulong[size + 1];
        }

        public void Insert(ulong item)
        {
            HashCombo i = new HashCombo(item, size);
            if (Filter[i.Hash1] != 0)
                if (Filter[i.Hash2] != 0)
                    ReplaceAndPlace(i);
                else
                    Filter[i.Hash2] = i.Fingerprint;
            else
                Filter[i.Hash1] = i.Fingerprint;
        }

        public bool Lookup(ulong item)
        {
            HashCombo i = new HashCombo(item, size);
            if (Filter[i.Hash1] == i.Fingerprint || Filter[i.Hash2] == i.Fingerprint)
            {
                return true;
            }
            return false;
        }


        private void ReplaceAndPlace(HashCombo item)
        {
            if(replace_counter-- < 1)
            {
                replace_counter = 500;
                return;
            }

            int location = -1;
            int choice = rand.Next(0, 2);
            if (choice == 0)
                location = item.Hash1;
            else
                location = item.Hash2;

            ulong original_obj = Filter[location];
            Filter[location] = item.Fingerprint;
            Insert(original_obj);
        }
    }
}
