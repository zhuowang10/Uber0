using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CacheServiceLib.Cache
{
    /*
     * CacheKey class provides formatted unique string for a list of sub items
     */

    public abstract class CacheKey
    {
        private BinaryFormatter _formatter;

        private BinaryFormatter Formatter
        {
            get
            {
                if (_formatter == null)
                {
                    _formatter = new BinaryFormatter();
                    _formatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;
                    _formatter.Context = new StreamingContext(StreamingContextStates.File);
                    _formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
                }
                return _formatter;
            }
        }

        protected abstract IList GetKeyItemList();

        internal string KeyValue
        {
            get
            {
                var items = GetKeyItemList();
                return SerializeKey(items);
            }
        }

        private string SerializeKey(IList items)
        {
            StringBuilder sbuilder = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                sbuilder.Append(SerializeObject(items[i]));
            }
            return sbuilder.ToString();
        }

        private string SerializeObject(object obj)
        {
            if (obj == null)
            {
                return "__NULL";
            }

            Type type = obj.GetType();
            if (type.IsPrimitive)
            {
                // Save some space
                return obj.ToString();
            }
            if (type.Equals(typeof(string)))
            {
                return (string)obj;
            }
            if (!type.IsSerializable)
            {
                throw new InvalidOperationException("Cache Key Param Not Serializable");
            }
            return BinSerialize(obj);
        }

        private string BinSerialize(object value)
        {
            string ret;
            using (MemoryStream stream = new MemoryStream())
            {
                this.Formatter.Serialize(stream, value);
                ret = Convert.ToBase64String(stream.ToArray());
            }
            return ret;
        }
    }
}
