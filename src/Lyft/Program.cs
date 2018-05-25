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
            ValueStore store = new ValueStore();
            string line = string.Empty;
            string key = string.Empty;
            while((line = Console.ReadLine()) != "q")
            {
                var parts = line.Split();
                switch(parts[0])
                {
                    case "PUT":
                        key = parts[1];
                        var value = Int32.Parse(parts[2]);
                        store.Put(key, value);
                        Console.WriteLine("PUT(#{0}) {1} = {2}", store.Version, key, value);
                        break;
                    case "GET":
                        key = parts[1];
                        int version = -1;
                        if(parts.Length > 2)
                        {
                            version = Int32.Parse(parts[2]);
                        }

                        var v = store.Get(key, version);
                        if(v.HasValue)
                        {
                            if (version == -1)
                            {
                                Console.WriteLine("GET {0} = {1}", key, v);
                            }
                            else
                            {
                                Console.WriteLine("GET {0}(#{1}) = {2}", key, version, v);
                            }
                        }
                        else
                        {
                            if (version == -1)
                            {
                                Console.WriteLine("GET {0} = <NULL>", key);
                            }
                            else
                            {
                                Console.WriteLine("GET {0}(#{1}) = <NULL>", key, version);
                            }
                        }

                        break;
                    default: break;
                }
            }
            
        }
    }
}
