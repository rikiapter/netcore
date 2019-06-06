using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Malam.Mastpen.API.Infrastructure
{

    public static class Utils
    {
        public static string ObjectToString(object o)
        {
            return ObjectToString(o, String.Empty);
        }

        public static int StringToInt(string s)
        {
            try
            {
                if (s == null)
                    return -1;
                else
                    return int.Parse(s);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static DateTime StringToDate(string s)
        {
            try
            {
                if (s == null)
                    return DateTime.MinValue;
                else
                    return DateTime.Parse(s);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime ObjectToNotNullDate(object o)
        {
            try
            {
                if (o == null || o == DBNull.Value)
                    return DateTime.MinValue;
                else
                    if (o is DateTime)
                {
                    return (DateTime)o;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime? ObjectToDate(object o)
        {
            try
            {
                if (o == null)
                    return null;
                else
                    if (o is DateTime)
                {
                    return (DateTime)o;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Decimal? ObjectToDecimal(object o)
        {
            try
            {
                if (o == null)
                    return null;
                else
                    if (o is decimal)
                {
                    return Convert.ToDecimal(o);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static string ObjectToString(object o, string sDefaultValue)
        {
            try
            {
                if (o == null)
                    return sDefaultValue;
                else if (o.GetType().IsArray)
                {
                    string s = "[";
                    foreach (object obj in (System.Array)o)
                    {
                        if (s.Length > 1)
                        {
                            s += ",";
                        }
                        s += ObjectToString(obj);
                    }
                    s += "]";
                    return s;
                }
                else if (o.GetType().Equals(typeof(DateTime)))
                {
                    return ((DateTime)o).ToShortDateString();
                }
                else
                    return o.ToString();
            }
            catch (Exception)
            {
                return sDefaultValue;
            }
        }

        //public static string ComplexObjectToString(object o, string sDefaultValue)
        //{
        //    try
        //    {
        //        if (o == null)
        //            return sDefaultValue;
        //        else
        //            return o.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        return sDefaultValue;
        //    }
        //}

        public static int ObjectToInt(object o)
        {
            try
            {
                if (o == null || o == DBNull.Value)
                    return -1;
                else
                    return Convert.ToInt32(o);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int? ObjectToNullableInt(object o)
        {
            try
            {
                if (o == null || o == DBNull.Value)
                    return null;
                else
                    return Convert.ToInt32(o);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static long ObjectToLong(object o)
        {
            try
            {
                if (o == null || o == DBNull.Value)
                    return -1;
                else
                    return Convert.ToInt64(o);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static long? ObjectToNullableLong(object o)
        {
            try
            {
                if (o == null || o == DBNull.Value)
                    return null;
                else
                    return Convert.ToInt64(o);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Boolean ObjectToBoolean(object o)
        {
            try
            {
                if (o == null || o == DBNull.Value)
                    return false;
                else
                    return Convert.ToBoolean(o);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean? ObjectToNullableBoolean(object o)
        {
            try
            {
                if (o == null || o == DBNull.Value)
                    return null;
                else
                    return Convert.ToBoolean(o);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int LongToInt(long num)
        {
            try
            {
                return Convert.ToInt32(num);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static float ObjectToFloat(object o)
        {
            try
            {
                if (o == null || o == DBNull.Value)
                    return 0;
                else
                    return Convert.ToSingle(o);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //public static string FormatLogDetails(Castle.DynamicProxy.IInvocation invocation)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("Class: " + invocation.TargetType.Name);
        //    sb.Append("  ");
        //    sb.Append("Function: " + invocation.Method.Name + "  ReturnType: " + invocation.Method.ReturnType.Name);

        //    if (invocation.Arguments.Length > 0)
        //    {
        //        sb.Append("  ");
        //        sb.Append("Parameters: ");
        //        for (int i = 0; i < invocation.Arguments.Length; i++)
        //        {
        //            if (i > 0)
        //                sb.Append(", ");
        //            sb.Append(Utils.ObjectToString(invocation.Arguments[i]));
        //        }
        //    }
        //    return sb.ToString();
        //}
    }
}
