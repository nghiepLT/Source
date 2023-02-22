using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PQT.API.File;

namespace PQT.DAC
{
    public class MenuMng_BLC
    {
        FileManager fileMng = new FileManager();
        MenuDataDataContext da = null;
        public MenuMng_BLC()
        {
            if (da == null)
            {
                da = new MenuDataDataContext();
            }
        }

        public TMenu RowMenu(int id)
        {
            return da.TMenus.SingleOrDefault(z => z.Menu_ID == id);
        }

        public TMenu RowMenu_By_Key(string keyword)
        {
            return da.TMenus.FirstOrDefault(z => z.Keyword == keyword);
        }

        public TMenu RowMenu_By_Alias_URL(string alias_url)
        {
            return da.TMenus.FirstOrDefault(z => z.Alias_Url == alias_url);
        }

        public int AddMenu(TMenu ent)
        {
            TMenu check_exists = RowMenu(ent.Menu_ID);
            if (check_exists == null)
            {
                da.TMenus.InsertOnSubmit(ent);
                da.SubmitChanges();
                return ent.Menu_ID;
            }
            else
            {
                TMenu objEnt = check_exists;
                objEnt.Name = ent.Name;
                objEnt.Type = ent.Type;
                objEnt.Map_ID = ent.Map_ID;
                objEnt.Status = ent.Status;
                objEnt.Require_Login = ent.Require_Login;
                objEnt.Sort_Order = ent.Sort_Order;
                objEnt.Parent_ID = ent.Parent_ID;
                objEnt.Keyword = ent.Keyword;
                objEnt.Alias_Url = ent.Alias_Url;
                objEnt.Modify_User = ent.Modify_User;
                objEnt.Update_Date = DateTime.Now ;
                objEnt.Image = ent.Image;
                objEnt.Option_1 = ent.Option_1;
                objEnt.Option_2 = ent.Option_2;
                objEnt.Option_3 = ent.Option_3;
                objEnt.Option_4 = ent.Option_4;
                objEnt.Option_5 = ent.Option_5;
                objEnt.Option_6 = ent.Option_6;
                objEnt.Name_2 = ent.Name_2;
                objEnt.Name_3 = ent.Name_3;
                da.TMenus.Context.SubmitChanges();
                return objEnt.Menu_ID;
            }
        }
        public bool DeleteMenu(int id)
        {
            try
            {
                TMenu check_exists = RowMenu(id);
                if (check_exists != null)
                {
                    da.TMenus.DeleteOnSubmit(check_exists);
                    da.SubmitChanges();

                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Get list menu
        /// </summary>
        /// <param name="parent_id">-1 get all</param>
        /// <param name="menu_id">-1 get all</param>
        /// <param name="status">-1 get all</param>
        /// <param name="keyword">'' get all</param>
        /// <returns></returns>
        public DataTable RowsMenu(int parent_id, int menu_id, int status, string keyword)
        {
            string sql = string.Format("[p_TMenu_Rows] @parent_id={0}, @menu_id = {1}, @status={2}, @keyword=N'{3}'",
                parent_id, menu_id, status, keyword);
            return (new ConnectSQL()).connect_dt(sql);
        }
        public DataTable RowsMenu_by_parentID_pager(int pager,int pagesize,int parent_id, int menu_id, int status, string keyword,int LangID)
        {
            string sql = string.Format("[p_TMneuByParentID_pageSize_Rows] @intPage={0},@intPageSize={1},@intParentID={2}, @menu_id = {3}, @status={4}, @keyword=N'{5}',@intLangID={6}",
                pager, pagesize, parent_id, menu_id, status, keyword, LangID);
            return (new ConnectSQL()).connect_dt(sql);
        }


        /// <summary>
        /// Get list menu
        /// </summary>
        /// <param name="parent_id">-1 get all</param>
        /// <param name="menu_id">-1 get all</param>
        /// <param name="status">-1 get all</param>
        /// <param name="keyword">'' get all</param>
        /// <returns></returns>
        public DataTable RowsMenu(int parent_id, int menu_id, int status, string keyword, int lang_id)
        {
            string sql = string.Format("[p_TMenu_Rows] @parent_id={0}, @menu_id = {1}, @status={2}, @keyword=N'{3}', @lang_id={4}",
                parent_id, menu_id, status, keyword, lang_id);
            return (new ConnectSQL()).connect_dt(sql);
        }

    }
}

