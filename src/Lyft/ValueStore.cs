using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyft
{
    public class VersionedValue
    {
        public int Value
        {
            get;
        }
        public int Version
        {
            get;
        }
        public VersionedValue(int value, int version)
        {
            Value = value;
            Version = version;
        }
    }

    public class ValueStore
    {
        private Dictionary<string, List<VersionedValue>> store = new Dictionary<string, List<VersionedValue>>();

        public ValueStore()
        {
            Version = 0;
        }

        public int Version
        {
            get;
            private set;
        }

        public void Put(string key, int value)
        {
            Version++;
            if (store.ContainsKey(key))
            {
                store[key].Add(new VersionedValue(value, Version));
            }
            else
            {
                store.Add(key, new List<VersionedValue>() { new VersionedValue(value, Version) });
            }
        }

        public int? Get(string key, int version = -1)
        {
            if (store.ContainsKey(key))
            {
                if (version == -1)
                {
                    int len = store[key].Count;
                    return store[key][len - 1].Value;
                }
                else
                {
                    var list = store[key];
                    for (int i = 0; i < list.Count; i++)
                    {
                        if(list[i].Version > version && i > 0)
                        {
                            return list[i - 1].Value;
                        }
                    }

                    int len = store[key].Count;
                    return store[key][len - 1].Value;
                }
            }
            return null;
        }
    }
}
