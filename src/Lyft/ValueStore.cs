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
                    var versionedValue = BinarySearchIterative(list, version);
                    if (versionedValue == null)
                        return null;
                    return versionedValue.Value;
                }
            }
            return null;
        }

        public VersionedValue BinarySearchIterative(List<VersionedValue> inputArray, int version)
        {
            int min = 0;
            int max = inputArray.Count - 1;
            while (min <= max)
            {
                if(max - min == 1)
                {
                    if (inputArray[max].Version <= version)
                        return inputArray[max];
                    return inputArray[min];
                }
                int mid = (min + max) / 2;
                if (version == inputArray[mid].Version)
                {
                    return inputArray[mid];
                }
                else if (version < inputArray[mid].Version)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return null;
        }
    }
}
