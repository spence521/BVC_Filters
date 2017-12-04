using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BVC_Filters
{
    public class Data
    {
        public Dictionary<string, string> Training_Data { get; private set; }
        public Dictionary<string, string> Test_Data { get; private set; }

        public Data(StreamReader r, StreamReader r2)
        {
            Training_Data = new Dictionary<string, string>();
            Test_Data = new Dictionary<string, string>();
            SetData(r, r2);
        }

        public void SetData(StreamReader reader, StreamReader reader_2 = null)
        {
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                int Sign;
                Dictionary<int, double> Vector = new Dictionary<int, double>();
                string[] splitstring = line.Split();
                if (splitstring.First().First() == '1') { Sign = 1; }
                else { Sign = -1; }
                foreach (var item in splitstring)
                {
                    if (item.Contains(":"))
                    {
                        string[] s = item.Split(':');
                        Vector.Add(Convert.ToInt32(s[0]), Convert.ToDouble(s[1]));
                    }
                }
                //Training_Data.Add(new Entry(Sign, Vector));
            }
            if (reader_2 != null)
            {
                reader_2.DiscardBufferedData();
                reader_2.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                string line2;
                while ((line2 = reader_2.ReadLine()) != null)
                {
                    int Sign;
                    Dictionary<int, double> Vector = new Dictionary<int, double>();
                    string[] splitstring = line2.Split();
                    if (splitstring.First().First() == '1') { Sign = 1; }
                    else { Sign = -1; }
                    foreach (var item in splitstring)
                    {
                        if (item.Contains(":"))
                        {
                            string[] s = item.Split(':');
                            Vector.Add(Convert.ToInt32(s[0]), Convert.ToDouble(s[1]));
                        }
                    }
                    //Test_Data.Add(new Entry(Sign, Vector));
                }
            }
        }
    }
}
