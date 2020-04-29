using OrderedJson.Code;
using OrderedJson.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedJson.Definer
{
    /// <summary>
    /// OJ中的通用类型转化
    /// </summary>
    public static class CommonDefiner
    {
        public static bool OJToBool(this object obj, OJContext context)
        {
            obj = obj.OJGetValue(context);
            if (obj is bool boolValue)
            {
                return boolValue;
            }
            if (obj is int intValue)
            {
                if (intValue == 0) return false;
                return true;
            }
            return false;
        }
        public static int OJToInt(this object obj, OJContext context)
        {
            obj = obj.OJGetValue(context);
            if (obj is int intValue)
            {
                return intValue;
            }
            if (obj is bool boolValue)
            {
                if (boolValue) return 1;
                return 0;
            }
            return 0;
        }

        public static object OJGetValue(this object obj, OJContext context)
        {
            if (obj is Block block)
            {
                obj = block.GetValue();
            }
            while (obj is IOJMethod method)
            {
                obj = method.Invoke(context);
            }
            return obj;
        }

        public static int OJCompareTo(this object obj1, OJContext context, object obj2)
        {
            obj1 = obj1.OJGetValue(context);
            obj2 = obj2.OJGetValue(context);

            if (obj1 is IComparable cmpValue1 && obj2 is IComparable cmpValue2)
            {
                try
                {
                    return cmpValue1.CompareTo(cmpValue2);
                }
                catch (Exception)
                {
                    throw new RuntimeException("比较操作作用于两个不同类型的元素了！");
                }
            }
            throw new RuntimeException("比较操作作用于不能进行比较的元素了！");
        }
    }
}
