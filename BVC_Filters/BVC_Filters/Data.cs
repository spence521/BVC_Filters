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
        public static Dictionary<string,string> GetData(string[] data)
        {
            Dictionary<string, string> Training_Data = new Dictionary<string, string>();
            Regex header = new Regex(@".*-.*-[\d]{4}");
            string curr_id = "";
            List<string> content = new List<string>();
            foreach(string line in data)
            {
                if (header.IsMatch(line))
                {
                    if (string.IsNullOrEmpty(curr_id))
                        curr_id = header.Match(line).Value;
                    else
                    {
                        Training_Data.Add(curr_id, string.Join(" ", content.ToArray()));
                        curr_id = header.Match(line).Value;
                    }
                    content = new List<string>();
                } else
                {
                    content.Add(line);
                }
            }

            return Training_Data;
        }
    }
}
