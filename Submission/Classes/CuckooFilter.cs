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

        public HashCombo(ulong item, int upper_lim, bool finger_only=false)
        {
            Object = item;
            //Fingerprint = Object;
            Fingerprint = Hasher.Fingerprint(item);
            if (finger_only)
                Fingerprint = item;
            Hash1 = Hasher.GetHash(Fingerprint, upper_lim);
            int temp = Hash1 ^ 2*Hasher.GetHash(Fingerprint, upper_lim);
            Hash2 = Hasher.MapInt(temp, upper_lim);
        }
    }

    public class CuckooFilter
    {
        private static Random rand = new Random();
        private int replace_counter = 500;

        public ulong[] Filter { get; set; }
        private ulong[] Filter_backup { get; set; }

        private Dictionary<ulong, ulong> object_dictionary = new Dictionary<ulong, ulong>();

        public bool IsFull = false;
        private int size;


        public CuckooFilter(int size)
        {
            this.size = size;
            Filter = new ulong[size + 1];
            Filter_backup = Filter;
        }

        public void Insert(ulong item, bool finger_only=false)
        {
            if (!IsFull)
            {
                HashCombo i = new HashCombo(item, size, finger_only);
                if (Filter[i.Hash1] != 0)
                    if (Filter[i.Hash2] != 0)
                        ReplaceAndPlace(i);
                    else
                    {
                        Filter[i.Hash2] = i.Fingerprint;
                        replace_counter = 500;
                    }
                else
                {
                    Filter[i.Hash1] = i.Fingerprint;
                    replace_counter = 500;
                }
            }
        }

        private void ReplaceAndPlace(HashCombo item)
        {
            if (replace_counter == 500)
                Filter.CopyTo(Filter_backup, 0);
            if(replace_counter == 0)
            {
                Filter = Filter_backup;
                replace_counter = 500;
                IsFull = true;
                return;
            }
            replace_counter--;

            int location = -1;
            int choice = rand.Next(0, 2);
            if (choice == 0)
                location = item.Hash1;
            else
                location = item.Hash2;

            ulong original_obj = Filter[location];
            Filter[location] = item.Fingerprint;
            Insert(original_obj, finger_only:true);
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

        public void Size()
        {
            Console.WriteLine("Size of cuckoo filter: " + size * sizeof(ulong));
        }
    }
}
