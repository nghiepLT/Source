using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace PQT.DAC
{
    public enum ContentDetail : int
    {
        Name = 0,
        Description,
        SubContent,
        Address,
        Comment,
        Destination,
        SubDescription
    }

    public class TEntityContent:EntityObject
    {
        public global::System.Int32 ID
        {
            get;
            set;
        }
        public global::System.String Text
        {
            get;
            set;
        }

        public global::System.Int32 LangID
        {
            get;
            set;
        }
    }
    public class Utils
    {
        #region
        public static long TryParseLong(object p_value, int p_default)
        {
            try
            {
                if (p_value != null)
                {
                    long longValue = Convert.ToInt64(p_value);
                    return longValue;
                }
                return p_default;

            }
            catch (System.Exception ex)
            {
                return p_default;
            }
        }

        public static int TryParseInt(object p_value, int p_default)
        {
            try
            {
                if (p_value != null)
                {
                    int longValue = Convert.ToInt32(p_value);
                    return longValue;
                }
                return p_default;
            }
            catch (System.Exception ex)
            {
                return p_default;
            }
        }
        #endregion
    }

    public enum typeLike : int
    {
        LikeProduct = 0,
        LikeGallery = 1,
        LikeNews = 2
    }

}
