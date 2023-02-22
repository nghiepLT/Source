using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using PQT.API;
using PQT.API.Connection;
using UserMng.DAC;
using UserMng.DataDefine;
using System.Data;
using PQT.DAC;
using PQT.Common;
using System.Data.Linq;

namespace UserMng.BLC
{
    public class Seo_BLC : XVNET_ModuleControl
    {
        UserMngDataDataContext da = null;
        public Seo_BLC()
        {
            if (da == null)
            {
                da = new UserMngDataDataContext();
            }
        }

#region Seo
        public DataTable ListSeoDescription(Int64 seoid)
        {
            string sql = string.Format("[p_TSeoDescriptionRows] {0}", seoid);
            ConnectSQL cn = new ConnectSQL();
            DataTable dt = cn.connect_dt(sql);
            return dt;
        }
        public DataTable RowsListSeoMnSeo(Int64 seoid, int intlang, int @intType, string @strUniquekey, string @strKeyOther)
        {
            //key = -1 get all;
            string sql = string.Format("[p_TSeoListDesRows] {0},{1},{2},'{3}','{4}'", seoid, intlang, @intType, @strUniquekey, @strKeyOther);
            ConnectSQL cn = new ConnectSQL();
            DataTable dt = cn.connect_dt(sql);
            return dt;
        }
        public TSeo GetTSeo_ByID(Int64 SeoID)
        {
            var list = da.TSeos.Where(z => z.SeoID == SeoID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        public TSeo GetTSeo_ByUniKeyMapID(Int64 MapID, string keyword)
        {
            var list = da.TSeos.Where(z => z.MapID == MapID && z.Uniquekey.ToLower().Trim() == keyword.ToLower().Trim()).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        public TSeo GetTSeo_ByKeyOther(string keyword)
        {
            var list = da.TSeos.Where(z => z.KeyOther.ToLower().Trim() == keyword.ToLower().Trim()).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        public Int64 CreateSEO(TSeo ent)
        {
            long pvalue = UpdateSEO(ent);
            if (pvalue == -1)
            {
                TSeo objEnt = null;
                objEnt = new TSeo();
                objEnt = ent;
                objEnt.Mota = "";
                da.TSeos.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.SeoID;
            }
            return pvalue;
        }

        public long UpdateSEO(TSeo ent)
        {
            TSeo entSeo = GetTSeo_ByUniKeyMapID((Int64)ent.MapID, (string)ent.Uniquekey);
            if (entSeo != null)
            {
                var objVlaue = entSeo;
                objVlaue = ent;
                objVlaue.Mota = "";
                da.TSeos.Context.SubmitChanges();
                return entSeo.SeoID;
            }
            return -1;
        }

        public bool DeleteSEO(Int64 MapID, string unikeySeo)
        {
            TSeo entSeo = GetTSeo_ByUniKeyMapID(MapID, unikeySeo);
            if (entSeo!=null)
            {
                DeleteSEODes(entSeo.SeoID);
                da.TSeos.DeleteOnSubmit(entSeo);
                da.SubmitChanges();
            }
            return true;
        }

        public Int64 CreateSEOList(TSeo ent)
        {
            TSeo objEnt = null;
            objEnt = new TSeo();
            objEnt = ent;
            da.TSeos.InsertOnSubmit(objEnt);
            da.SubmitChanges();
            return objEnt.SeoID;
        }

        public bool UpdateSEOList(long longseoid, string keyother,string mota)
        {
            TSeo entSeo = GetTSeo_ByID(longseoid);
            if (entSeo!=null)
            {
                var objVlaue = entSeo;
                objVlaue.KeyOther = keyother;
                objVlaue.SeoType = 2;
                objVlaue.MapID = 0;
                objVlaue.Status = 1;
                objVlaue.Uniquekey = "";
                objVlaue.Mota = mota;
                da.TSeos.Context.SubmitChanges();

                return true;
            }
            return false;
        }

        public bool DeleteSEOList(Int64 longseoid)
        {
            TSeo entSeo = GetTSeo_ByID(longseoid);
            if (entSeo != null)
            {
                DeleteSEODes(entSeo.SeoID);
                da.TSeos.DeleteOnSubmit(entSeo);
                da.SubmitChanges();
            }
            return true;
        }
#endregion
        
#region descrip

        public TSeoDescription GetTSeoDescription(Int64 SeoID, int langid)
        {
            var list = da.TSeoDescriptions.Where(z => z.SeoID == SeoID && z.LanguageID == langid).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        public bool CreateSEODescription(TSeoDescription ent)
        {
            try
            {
                if (UpdateCreateSEODescription(ent) == false)
                {
                    TSeoDescription objEnt = null;
                    objEnt = new TSeoDescription();
                    objEnt = ent;
                    da.TSeoDescriptions.InsertOnSubmit(objEnt);
                    da.SubmitChanges();

                    return true;
                }
                return true;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCreateSEODescription(TSeoDescription ent)
        {
            IList<TSeoDescription> list = da.TSeoDescriptions.Where(z => z.SeoID == ent.SeoID && z.LanguageID == ent.LanguageID).ToList();
            if (list.Count > 0)
            {
                var objVlaue = list.First();
                objVlaue.SeoTitle = ent.SeoTitle;
                objVlaue.SeoDescription = ent.SeoDescription;
                objVlaue.SeoKeyWord = ent.SeoKeyWord;
                da.TSeoDescriptions.Context.SubmitChanges();

                return true;
            }
            return false;
        }

        public bool DeleteSEODes(Int64 SeoID)
        {
            IList<TSeoDescription> list = da.TSeoDescriptions.Where(z => z.SeoID == SeoID).ToList();
            if (list.Count > 0)
            {
                foreach(TSeoDescription entDes in list )
                {
                    da.TSeoDescriptions.DeleteOnSubmit(entDes);
                    da.SubmitChanges();
                }
            }
            return true;
        }
#endregion
        

        

        //public TSeo GetTSeo_By_ref_type_Unikey(Int64 Ref_Type, string Uniquekey)
        //{
        //    var list = da.TSeos.Where(z => z.Ref_Type == Ref_Type && z.Uniquekey.ToLower() == Uniquekey.ToLower()).ToList();
        //    if (list.Count > 0)
        //    {
        //        return list.First();
        //    }
        //    else
        //        return null;
        //}

        //public bool DeleteSEO_ById_ref_UK(TSeo ent)
        //{
        //    IList<TSeo> list = da.TSeos.Where(z => z.ID_ref == ent.ID_ref && z.Uniquekey.ToLower() == ent.Uniquekey.ToLower()).ToList();
        //    if (list.Count > 0)
        //    {
        //        da.TSeos.DeleteOnSubmit(list.First());
        //        da.SubmitChanges();
        //    }
        //    return true;
        //}

    }
}