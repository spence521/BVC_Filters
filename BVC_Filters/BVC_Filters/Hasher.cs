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

        public static ulong Fingerprint(object obj)
        {
            char[] str = obj.ToString().ToCharArray();
            ulong hash = 5381;
            foreach (int c in str)
            {
                hash = (ulong)c + (hash << 6) + (hash << 16) - hash;
            }
            if (hash == 0)
                hash++;
            return hash;
        }

        public static int GetHash(object obj)
        {
            char[] str = obj.ToString().ToCharArray();
            int hash = 5381;
            foreach(int c in str)
            {
                hash = ((hash << 5) + hash) + c;
            }
            hash = Math.Abs(hash);
            return MapInt(hash);
        }   

        public static int MapInt(int hash)
        {
            int old_span = int.MaxValue;
            int new_span = int.MaxValue / 30;
            int num = hash / old_span;
            return num*new_span;
        }
    }
}