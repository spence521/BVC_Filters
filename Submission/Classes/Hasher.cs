using System;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

namespace BVC_Filters
{
    public static class Hasher
    {
        public static int size { get; set; }

        public static int Fingerprint(object obj) 
        {
            char[] str = obj.ToString().ToCharArray();
            int hash = 5381;
            foreach (int c in str)
            {
                hash = (int)(c + (hash << 6) + (hash << 16) - hash);
            }
            if (hash == 0)
                hash++;

            return hash;
        }

        public static int GetHash(object obj, int upper_lim)
        {
            char[] str = obj.ToString().ToCharArray();
            int hash = 5381;
            foreach(int c in str)
            {
                hash = ((hash << 5) + hash) + c;
            }
            hash = Math.Abs(hash);
            hash = MapInt(hash, upper_lim);
            return hash;
        }   

        public static int MapInt(int hash, int upper_lim)
        {
            if(hash > upper_lim)
            {
                int old_span = int.MaxValue;
                int new_span = upper_lim;
                float num = (float)hash / old_span;
                int obj = (int)(num * new_span);

                while (obj > upper_lim)
                {
                    obj = obj - upper_lim;
                }
                return obj;
            }
            return hash;
        }

        public static int[] BloomHash(object obj, int upper_lim, int k = 10)
        {
            int[] hasharray = new int[k];
            string hash = Fingerprint(obj).ToString();
            int length = hash.Length;
            int first = Convert.ToInt32(hash.Substring(0, length / 2));
            int second = Convert.ToInt32(hash.Substring(length / 2));

            for (int i=0; i<k; i++)
            {
                int temp = first + (int)i * second;
                int hash_ = GetHash(temp, upper_lim);
                hasharray[i] = hash_;
            }
            return hasharray;
        }

    }
}