using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BVC_Filters
{
    public static class Hasher
    {
        public static int size { get; set; }

        public static string Fingerprint(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using(MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                MD5 hasher = MD5.Create();
                byte[] hash = hasher.ComputeHash(ms);
                return "" + (Int16)obj.GetHashCode();
                //return BitConverter.ToString(hash);
            }
        }

        public static int GetHash(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                MD5 hasher = MD5.Create();
                byte[] hash = hasher.ComputeHash(ms);
                Int32 hash_ = BitConverter.ToInt32(hash, 0);
                return MapInt((Int16)obj.GetHashCode());
                //return MapInt(hash_);
            }
        }

        public static int MapInt(Int32 hash)
        {
            float num = (float)Math.Abs(hash) / Int16.MaxValue * (size - 1);
            return (int)num;
        }
    }
}