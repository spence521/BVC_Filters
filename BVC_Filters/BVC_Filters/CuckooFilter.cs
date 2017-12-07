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

        public HashCombo(ulong item)
        {
            Object = item;
            Fingerprint = Hasher.Fingerprint(item);
            Hash1 = Hasher.GetHash(item);
            Hash2 = Hash1 ^ Hasher.GetHash(Fingerprint);
        }
    }

    public class CuckooFilter
    {
        private static Random rand = new Random();
        private int replace_counter = 500;

        public ulong[] Filter { get; set; }
        public bool IsFull = false;

        public CuckooFilter()
        {
            Filter = new ulong[(int.MaxValue/30) + 1];
        }

        public void Insert(ulong item)
        {
            if (IsFull)
            {
                Console.WriteLine("Filter Full LOL");
                return;
            }

            HashCombo i = new HashCombo(item);
            if (Filter[i.Hash1] == 0)
                if (Filter[i.Hash2] == 0)
                    ReplaceAndPlace(i);
                else
                    Filter[i.Hash2] = i.Object;
            else
                Filter[i.Hash1] = i.Object;
        }

        private void ReplaceAndPlace(HashCombo item)
        {
            if(replace_counter-- < 1)
            {
                Console.WriteLine("Filter Full.");
                IsFull = true;
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
            Filter[location] = item.Object;
            Insert(original_obj);
        }
    }
}
