using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.API;
using PQT.Controls;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using PQT.Common;
using NewsMng.BLC;
using NewsMng.DataDefine;
using UserMng.DataDefine;
using UserMng.BLC;
using PQT.DAC;
using System.Text.RegularExpressions;
//using ProductMng.BLC;
//using ProductMng.DataDefine;
using System.IO;
using System.Web.UI.HtmlControls;
namespace WebCus.ASCX
{
    public partial class MenuPage : PQT.API.CommonUserControl
    {
        MenuMng_BLC blc_menu = new MenuMng_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Bind_Menu();
        }
        private void Bind_Menu()
        {
            TMenu ent = blc_menu.RowMenu_By_Key("MainMenu");
            if (ent != null)
            {
                DataTable dt = blc_menu.RowsMenu(ent.Menu_ID, -1, 1, "", this.LangID);
                rpt_menu.DataSource = dt;
                rpt_menu.DataBind();
            }
                     
        }

        

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                int Menu_ID = Convert.ToInt32(dr["Menu_ID"]);
                Repeater rptSubMenu = e.Item.FindControl("rptSubMenu") as Repeater;
                HtmlGenericControl div_liparent = e.Item.FindControl("menu_parent") as HtmlGenericControl;
                DataTable dtChild = blc_menu.RowsMenu(Menu_ID, -1, 1, "", this.LangID);

                rptSubMenu.DataSource = dtChild;
                rptSubMenu.DataBind();

                Control menuSub = e.Item.FindControl("menuSub");
                if (menuSub != null && dtChild.Rows.Count == 0)
                    menuSub.Visible = false;

                if (dtChild.Rows.Count > 0)
                {
                    div_liparent.Attributes["class"] = "parent";
                   // div_liparent.Attributes["class"] = "dropdown"; 
                }

            }
        }

        protected void rptMenu_ItemDataBound_sub(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                int Parent_ID = Convert.ToInt32(dr["Menu_ID"]);
                Repeater rptSubMenu_sub = e.Item.FindControl("rptSubMenu_sub") as Repeater;
                HtmlGenericControl div_liparent_sub = e.Item.FindControl("menu_parent_sub") as HtmlGenericControl;
                DataTable dtChild_sub = blc_menu.RowsMenu(Parent_ID, -1, 1, "", this.LangID);

                rptSubMenu_sub.DataSource = dtChild_sub;
                rptSubMenu_sub.DataBind();

                Control menuSub_sub = e.Item.FindControl("menuSub_sub");
                if (menuSub_sub != null && dtChild_sub.Rows.Count == 0)
                    menuSub_sub.Visible = false;

                if (dtChild_sub.Rows.Count > 0)
                {
                    div_liparent_sub.Attributes["class"] = "parent-child";
                  //  div_liparent_sub.Attributes["class"] = "mega-menu-column";
                }

            }
        }
        
    }
}