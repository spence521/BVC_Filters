using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVC_Filters
{

    public class HashCombo
    {
        public object Object { get; set; }
        public string Fingerprint { get; set; }
        public int Hash1 { get; set; }
        public int Hash2 { get; set; }
    }

    [Serializable]
    public class CuckooFilter
    {
        public string[] Filter { get; set; }
        [NonSerialized]
        bool IsFull;

        public CuckooFilter(int size = 10000, List<object> data = null)
        {
            IsFull = false;
            Filter = new string[size];

            if (!(data is null))
            {
                foreach(object obj in data)
                {
                    Insert(obj);
                }
            }
        }

        public void Insert(Object obj)
        {
            HashCombo hash_combo = GetHashCombo(obj);
            if (!(Filter[hash_combo.Hash1] is null))
                if (!(Filter[hash_combo.Hash2] is null))
                    RelocateAndPlace(hash_combo, 500);
                else
                {
                    Console.WriteLine("Second Choice");
                    Filter[hash_combo.Hash2] = hash_combo.Fingerprint;
                }
            else
            {
                Console.WriteLine("First Choice");
                Filter[hash_combo.Hash1] = hash_combo.Fingerprint;
            }
        }

        public bool Lookup(object obj)
        {
            HashCombo h = GetHashCombo(obj);
            if (Filter[h.Hash1] == h.Fingerprint)
                return true;
            if (Filter[h.Hash2] == h.Fingerprint)
                return true;
            return false;
        }

        public void RelocateAndPlace(HashCombo obj, int trials)
        {
            Console.WriteLine("Relocating and placing");
            if (trials == 0)
                IsFull = true;
                return;
            int pos_1 = obj.Hash1;
            trials--;
            // Old Object
            HashCombo hash_combo = GetHashCombo(Filter[pos_1]);
            Filter[pos_1] = obj.Fingerprint;

            if (!(Filter[hash_combo.Hash1] is null))
                if (!(Filter[hash_combo.Hash2] is null))
                    RelocateAndPlace(hash_combo, trials);
                else
                    Filter[hash_combo.Hash2] = hash_combo.Fingerprint;
            else
                Filter[hash_combo.Hash1] = hash_combo.Fingerprint;
        }

        public HashCombo GetHashCombo(object obj)
        {
            HashCombo h = new HashCombo
            {
                Object = obj
            };
            h.Fingerprint = Hasher.Fingerprint(h.Object);
            h.Hash1 = Hasher.GetHash(h.Object);
            h.Hash2 = Hasher.MapInt(h.Hash1 ^ Hasher.GetHash(h.Fingerprint));
            //Console.WriteLine(h.Fingerprint + " " + h.Hash1 + " " + h.Hash2);
            return h;
        }

        public void Print()
        {
            foreach (string s in Filter)
                Console.WriteLine(s);
        }
    }
}
