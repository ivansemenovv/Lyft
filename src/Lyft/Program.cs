using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyft
{
    class Program
    {
        static void Main(string[] args)
        {
            // For stdin, you could use
            // new StreamReader(Console.OpenStandardInput(), Console.InputEncoding)

            using (var b = new StreamReader("InputFile.txt"))
            {
                string line;
                while ((line = b.ReadLine()) != null)
                    Console.WriteLine(line);
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
