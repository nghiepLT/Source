using System;

namespace WebCus
{
    public class Protector
    {
        #region UShort
        public static ushort UShort(object value, ushort defaultValue)
        {
            if (value == null)
                return defaultValue;

            ushort.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static ushort UShort(object value)
        {
            return UShort(value, 0);
        }

        public static ushort? UShort(object value, bool nullAble)
        {
            if (!nullAble)
                return UShort(value);

            if (value == null)
                return null;

            ushort defaultValue = 0;
            if (!ushort.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region UInt
        public static uint UInt(object value, uint defaultValue)
        {
            if (value == null)
                return defaultValue;

            uint.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static uint UInt(object value)
        {
            return UInt(value, 0);
        }

        public static uint? UInt(object value, bool nullAble)
        {
            if (!nullAble)
                return UInt(value);

            if (value == null)
                return null;

            uint defaultValue = 0;
            if (!uint.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region ULong
        public static ulong ULong(object value, ulong defaultValue)
        {
            if (value == null)
                return defaultValue;

            ulong.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static ulong ULong(object value)
        {
            return ULong(value, 0);
        }

        public static ulong? ULong(object value, bool nullAble)
        {
            if (!nullAble)
                return ULong(value);

            if (value == null)
                return null;

            ulong defaultValue = 0;
            if (!ulong.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region Byte
        public static byte Byte(object value, byte defaultValue)
        {
            if (value == null)
                return defaultValue;

            byte.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static byte Byte(object value)
        {
            return Byte(value, 0);
        }

        public static byte? Byte(object value, bool nullAble)
        {
            if (!nullAble)
                return Byte(value);

            if (value == null)
                return null;

            byte defaultValue = 0;
            if (!byte.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region Short
        public static short Short(object value, short defaultValue)
        {
            if (value == null)
                return defaultValue;

            short.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static short Short(object value)
        {
            return Short(value, 0);
        }

        public static short? Short(object value, bool nullAble)
        {
            if (!nullAble)
                return Short(value);

            if (value == null)
                return null;

            short defaultValue = 0;
            if (!short.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region Int
        public static int Int(object value, int defaultValue)
        {
            if (value == null)
                return defaultValue;

            int.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static int Int(object value)
        {
            return Int(value, 0);
        }

        public static int? Int(object value, bool nullAble)
        {
            if (!nullAble)
                return Int(value);

            if (value == null)
                return null;

            int defaultValue = 0;
            if (!int.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region Long
        public static long Long(object value, long defaultValue)
        {
            if (value == null)
                return defaultValue;

            long.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static long Long(object value)
        {
            return Long(value, 0);
        }

        public static long? Long(object value, bool nullAble)
        {
            if (!nullAble)
                return Long(value);

            if (value == null)
                return null;

            long defaultValue = 0;
            if (!long.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region Double
        public static double Double(object value, double defaultValue)
        {
            if (value == null)
                return defaultValue;

            double.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static double Double(object value)
        {
            return Double(value, 0);
        }

        public static double? Double(object value, bool nullAble)
        {
            if (!nullAble)
                return Double(value);

            if (value == null)
                return null;

            double defaultValue = 0;
            if (!double.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region Decimal
        public static decimal Decimal(object value, decimal defaultValue)
        {
            if (value == null)
                return defaultValue;

            decimal.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static decimal Decimal(object value)
        {
            return Decimal(value, 0);
        }

        public static decimal? Decimal(object value, bool nullAble)
        {
            if (!nullAble)
                return Decimal(value);

            if (value == null)
                return null;

            decimal defaultValue = 0;
            if (!decimal.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region Bool
        public static bool Bool(object value, bool defaultValue)
        {
            if (value == null)
                return defaultValue;

            bool.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static bool Bool(object value)
        {
            return Bool(value, false);
        }

        public static bool? Bool(object value, bool defaultValue, bool nullAble)
        {
            if (!nullAble)
                return Bool(value);

            if (value == null)
                return null;

            if (!bool.TryParse(value.ToString(), out defaultValue))
                return null;

            return defaultValue;
        }
        #endregion

        #region DateTime
        public static System.DateTime DateTime(object value, DateTime defaultValue)
        {
            if (value == null)
                return defaultValue;

            System.DateTime.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static System.DateTime DateTime(object value)
        {
            return DateTime(value, new System.DateTime());
        }

        public static System.DateTime? DateTime(object value, bool nullAble)
        {
            if (!nullAble)
                return DateTime(value);

            if (value == null)
                return null;

            DateTime defaultValue = new DateTime();
            if (!System.DateTime.TryParse(value.ToString(), out defaultValue))
                return null;

            if (defaultValue == System.DateTime.MinValue)
                return null;

            return defaultValue;
        }
        #endregion

        #region String
        public static string String(object value, string defaultValue)
        {
            if (value == null)
                return defaultValue;

            return value.ToString().Trim();
        }

        public static string String(object value)
        {
            return String(value, string.Empty);
        }

        public static String String(object value, bool nullAble)
        {
            if (!nullAble)
                return String(value);

            if (value == null)
                return null;

            string defaultValue = value.ToString().Trim();
            if (defaultValue == string.Empty)
                return null;

            return defaultValue;
        }
        #endregion

        #region Obj
        public static Object Object(object value, Type type, bool nullAble)
        {
            if (value != null && value.GetType() == type)
                return value;

            if (!nullAble)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }
        #endregion

        #region Enum
        internal static bool EnumTryParse<T>(string strType, out T result)
        {
            string strTypeFixed = strType.Replace(' ', '_');
            if (Enum.IsDefined(typeof(T), strTypeFixed))
            {
                result = (T)Enum.Parse(typeof(T), strTypeFixed, true);
                return true;
            }
            else
            {
                foreach (string value in Enum.GetNames(typeof(T)))
                {
                    if (value.Equals(strTypeFixed, StringComparison.OrdinalIgnoreCase))
                    {
                        result = (T)Enum.Parse(typeof(T), value);
                        return true;
                    }
                }
                result = default(T);
                return false;
            }
        }

        public static T EnumType<T>(object value, T defaultValue)
        {
            if (value == null)
                return defaultValue;

            EnumTryParse<T>(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static T EnumType<T>(object value)
        {
            return EnumType<T>(value, default(T));
        }
        #endregion
    }
}
