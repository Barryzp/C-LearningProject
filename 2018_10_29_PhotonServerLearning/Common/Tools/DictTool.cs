using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Tools
{
    public class DictTool
    {
        public static V GetDictValue<K, V>(Dictionary<K, V> dict, K key)
        {
            V aimValue;
            bool isGet = dict.TryGetValue(key, out aimValue);

            if(!isGet)
            {
                return default(V);
            }
            return aimValue;
        }
    }
}
