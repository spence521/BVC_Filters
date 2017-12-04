using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVC_Filters
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader Train;
            StreamReader Test;
            Data data;
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter a file argument.");
                return;
            }
            else if (args.Length > 1) //at least two arguments
            {
                Train = File.OpenText(args[0]);
                Test = File.OpenText(args[1]);
                data = new Data(Train, Test);
            }
        }
    }
}
