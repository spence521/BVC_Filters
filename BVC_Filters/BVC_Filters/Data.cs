using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace BVC_Filters
{
    public class Data
    {
        public Dictionary<string, string> Training_Data { get; private set; }
        public Dictionary<string, string> Test_Data { get; private set; }

        public Data(StreamReader r)
        {
            Training_Data = new Dictionary<string, string>();
            Test_Data = new Dictionary<string, string>();
            SetData(r);
        }

        public void SetData(StreamReader reader)
        {

            // DEV - MUC3 - 0001(NOSC)
            Regex header = new Regex(@".*-.*-[\d]+");
            //foreach(string line in reader.)
        }
    }
}
