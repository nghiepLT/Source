using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Reflection;
using PQT.API.File;
using NewsMng.DataDefine;
using NewsMng.BLC;
using UserMng.BLC;
//using ProductMng.BLC;
using System.IO;
using System.Data;
using PQT.DAC;
using System.IO.Compression;


namespace WebCus
{
    public class Global : System.Web.HttpApplication
    {
        public Global()
        {
            InitializeComponent();

        }
        private void InitializeComponent()
        {
            this.PostReleaseRequestState += new EventHandler(Global_PostReleaseRequestState);
        }
        private void Global_PostReleaseRequestState(object sender, EventArgs e)
        {
            string contentType = Response.ContentType; // Get the content type.

            // Compress only html and stylesheet documents.
            if (contentType == "text/html" || contentType == "text/css")
            {
                // Get the Accept-Encoding header value to know whether zipping is supported by the browser or not.
                string acceptEncoding = Request.Headers["Accept-Encoding"];

                if (!string.IsNullOrEmpty(acceptEncoding))
                {
                    // If gzip is supported then gzip it else if deflate compression is supported then compress in that technique.
                    if (acceptEncoding.Contains("gzip"))
                    {
                        // Compress and set Content-Encoding header for the browser to indicate that the document is zipped.
                        Response.Filter = new GZipStream(Response.Filter, CompressionMode.Compress);
                        Response.AppendHeader("Content-Encoding", "gzip");
                    }
                    else if (acceptEncoding.Contains("deflate"))
                    {
                        // Compress and set Content-Encoding header for the browser to indicate that the document is zipped.
                        Response.Filter = new DeflateStream(Response.Filter, CompressionMode.Compress);
                        Response.AppendHeader("Content-Encoding", "deflate");
                    }
                }
            }
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            //PropertyInfo p = typeof(System.Web.HttpRuntime).GetProperty("FileChangesMonitor", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            //object o = p.GetValue(null, null);
            //FieldInfo f = o.GetType().GetField("_dirMonSubdirs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            //object monitor = f.GetValue(o);
            //MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", BindingFlags.Instance | BindingFlags.NonPublic);
            //m.Invoke(monitor, new object[] { });

            //Application["HomNay"] = 0;
            //Application["HomQua"] = 0;
            //Application["TuanNay"] = 0;
            //Application["TuanTruoc"] = 0;
            //Application["ThangNay"] = 0;
            //Application["ThangTruoc"] = 0;
            //Application["TatCa"] = 0;
            //Application["Visitor_Online"] = 0;

           

        }

        protected void Session_Start(object sender, EventArgs e)
        {


            //try
            //{
            //    Session.Timeout = 10;
            //    Session["suer"] = Convert.ToInt32(Application["Visitor_Online"]) + 1;
            //    Application.Lock();

            //    Application["Visitor_Online"] = Session["suer"];
            //    Application.UnLock();


            //    News_BLC blc_news = new News_BLC();
            //    DataTable dtb = blc_news.RowsStatics();
            //    if (dtb.Rows.Count > 0)
            //    {
            //        Application["HomNay"] = long.Parse("0" + dtb.Rows[0]["HomNay"]).ToString("#,###");
            //        Application["HomQua"] = long.Parse("0" + dtb.Rows[0]["HomQua"]).ToString("#,###");
            //        Application["TuanNay"] = long.Parse("0" + dtb.Rows[0]["TuanNay"]).ToString("#,###");
            //        Application["TuanTruoc"] = long.Parse("0" + dtb.Rows[0]["TuanTruoc"]).ToString("#,###");
            //        Application["ThangNay"] = long.Parse("0" + dtb.Rows[0]["ThangNay"]).ToString("#,###");
            //        Application["ThangTruoc"] = long.Parse("0" + dtb.Rows[0]["ThangTruoc"]).ToString("#,###");
            //        Application["TatCa"] = long.Parse("0" + dtb.Rows[0]["TatCa"]).ToString("#,###");
            //    }
            //    dtb.Dispose();
            //    blc_news = null;
            //}
            //catch { MessageBox.Show("error"); }


        }
        //private void Process_Compression_Gzip(object sender)
        //{
        //    // Implement HTTP compression
        //    HttpApplication app = (HttpApplication)sender;

        //    // Retrieve accepted encodings
        //    string encodings = app.Request.Headers.Get("Accept-Encoding");
        //    if (encodings != null)
        //    {
        //        // Check the browser accepts deflate or gzip (deflate takes preference)
        //        encodings = encodings.ToLower();
        //        if (encodings.Contains("deflate"))
        //        {
        //            app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
        //            app.Response.AppendHeader("Content-Encoding", "deflate");
        //        }
        //        else if (encodings.Contains("gzip"))
        //        {
        //            app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
        //            app.Response.AppendHeader("Content-Encoding", "gzip");
        //        }
        //    }
        //}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
           
            HttpContext context = HttpContext.Current;
            string currentLocation = context.Request.Path.ToLower();
            News_BLC blc_news = new News_BLC();
            NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();
           // ProductMng_BLC_NTX blc_product = new ProductMng_BLC_NTX();
            string About = "/gioi-thieu/";
            string NewsDetail = "/chi-tiet-tin/";
            string sanpham = "/san-pham/";
            string ChiTietSanPham = "/chi-tiet-san-pham/";
            string SearchProduct = "/tim-kiem/";
            string Searchtour = "/tour-option/";
            string HinhSanPham = "/hinh-san-pham/image/";
            string HinhSanPhamThumbnail = "/hinh-san-pham/thumbnail/";
            string HinhSanPhamBig = "/hinh-san-pham/image-detail/";
            string download = "/downloadfile/";
            string tourdetail = "/tour-detail/";
            string tourlist = "/tours/";
            string SendEnquiry = "/send-enquiry/";
           


            try
            {
                if (currentLocation.Equals("/listcase"))
                {
                    context.RewritePath("~/TableKeyWork.aspx");
                }
                else if (currentLocation.Equals("/yeucautuyendung"))
                {
                    context.RewritePath("~/tableYeyCauTuyenDung.aspx");
                }
                else if (currentLocation.Equals("/listaccept"))
                {
                    context.RewritePath("~/TableDuyet.aspx");
                }
                  else if (currentLocation.Equals("/inoutcheckhistory"))
                  {
                      context.RewritePath("~/inoutcheckhistory.aspx");
                  }
                  else if (currentLocation.Equals("/inoutchecker"))
                  {
                      context.RewritePath("~/inoutchecker.aspx");
                  }
                      
                else if (currentLocation.Equals("/updatechecker"))
                  {
                      context.RewritePath("~/updatechecker.aspx");
                  }                    
                  else if (currentLocation.Equals("/nhanvien"))
                  {
                      context.RewritePath("~/TableNhanVien.aspx");
                  }
                  else if (currentLocation.Equals("/statistics"))
                  {
                      context.RewritePath("~/ThongKeKeyWork.aspx");
                  }
                 else if (currentLocation.Equals("/account"))
                  {
                      context.RewritePath("/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=UserInfo");
                  }
                      
                           else if (currentLocation.Equals("/area"))
                  {
                      context.RewritePath("/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=Area");
                  }
                      
                           else if (currentLocation.Equals("/poin"))
                  {
                      context.RewritePath("/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=ValueofLevel");
                  }
                      
                                    else if (currentLocation.Equals("/idcasework"))
                  {
                      context.RewritePath("/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=MacongviecMng");
                  }
                      
                                             else if (currentLocation.Equals("/itinfo"))
                  {
                      context.RewritePath("/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=UserTableWork");
                  }
                      
                                                      else if (currentLocation.Equals("/poinarea"))
                  {
                      context.RewritePath("/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=PoinKmMng");
                  }
                      
                                                               else if (currentLocation.Equals("/userinf"))
                  {
                      context.RewritePath("/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=UserView");
                  }
                  else if (currentLocation.Equals("/login"))
                  {
                      context.RewritePath("~/login.aspx");
                  }
                  else if (currentLocation.Equals("/trans"))
                {
                    context.RewritePath("~/Transaction.aspx");
                }
               
                else if (currentLocation.Equals("/trang-chu"))
                {
                    context.RewritePath("~/Home.aspx");
                }
                else if (currentLocation.Contains(download) && currentLocation.Contains(".html"))
                {
                    string tmp = currentLocation.Replace(download, "");
                    string id = tmp.Substring(0, tmp.IndexOf('/'));
                    context.RewritePath(string.Format("~/CustomerDownload.aspx?FileID={0}", id));
                }
                else if (currentLocation.Equals("/lien-he"))
                {
                    context.RewritePath("~/Contact.aspx");
                }
                else if (currentLocation.Equals("/contact"))
                {
                    context.RewritePath("~/Contact.aspx");
                }
                else if (currentLocation.Equals("/tin-tuc"))
                {
                    context.RewritePath("~/NewsList.aspx?UK=News");
                }
                else if (currentLocation.Equals("/product"))
                {
                    context.RewritePath("~/Product.aspx?UK=Product");
                }
                else if (currentLocation.Equals("/hinh-anh"))
                {
                    context.RewritePath("~/NewsList.aspx?UK=Photos");
                }
                else if (currentLocation.Equals("/loc-nhung-suc-khoe"))
                {
                    context.RewritePath("~/NewsList.aspx?UK=Health");
                }
                else if (currentLocation.Equals("/chuyen-giao-con-giong"))
                {
                    context.RewritePath("~/Product.aspx?UK=ConGiong");

                }
                else if (currentLocation.Equals("/dang-ky"))
                {
                    context.RewritePath("~/Member/RegisAccount.aspx");
                }
                else if (currentLocation.Equals("/dang-nhap"))
                {
                    context.RewritePath("~/Member/MemberLogin.aspx");
                }
                else if (currentLocation.Equals("/quen-mat-khau"))
                {
                    context.RewritePath("~/Member/PasswordRecovery.aspx");
                }
                else if (currentLocation.Equals("/tai-khoan"))
                {
                    context.RewritePath("~/Member/EditCurrentProfile.aspx");
                }
                else if (currentLocation.Equals("/bookkhachsan"))
                {
                    context.RewritePath("~/SendHotel.aspx");
                }

                else if (currentLocation.Equals("/gio-hang"))
                {
                    context.RewritePath("~/CartProduct.aspx");
                }
                else if (currentLocation.Equals("/mua-hang"))
                {
                    context.RewritePath("~/Checkout.aspx");
                }
                /**/
                else if (currentLocation.Contains(tourlist) && currentLocation.Contains(".html"))
                {
                    string tmp = currentLocation.Replace(tourlist, "");
                    string id = tmp.Substring(0, tmp.IndexOf('/'));
                    context.RewritePath(string.Format("~/Tour.aspx?CateID={0}", id));
                }
                else if (currentLocation.Contains(About) && currentLocation.Contains(".html"))
                {
                    string id = Utility.getID_of_URL(currentLocation);
                    context.RewritePath(string.Format("~/About.aspx?UK=About01&CateID={0}", id));
                }

                else if (currentLocation.Contains(NewsDetail) && currentLocation.Contains(".html"))
                {
                    string id = Utility.getID_of_URL(currentLocation);
                    context.RewritePath(string.Format("~/NewsDetail.aspx?NewID={0}", id));
                }
                else if (currentLocation.Contains(sanpham) && currentLocation.Contains(".html"))
                {
                    string id = Utility.getID_of_URL(currentLocation);
                    context.RewritePath(string.Format("~/Product.aspx?UK=Product&CateID={0}", id));
                }
                else if (currentLocation.Contains(ChiTietSanPham) && currentLocation.Contains(".html"))
                {

                    string tmp = currentLocation.Replace(ChiTietSanPham, "").Replace(".html", "");
                    string id = tmp.Substring(tmp.LastIndexOf('-') + 1);
                    string lastid = tmp.Substring(tmp.LastIndexOf('-'));
                    tmp = tmp.Remove(tmp.Length - lastid.Length);
                    if (tmp.LastIndexOf('-') + 1 > 0)
                    {
                        string catID = tmp.Substring(tmp.LastIndexOf('-') + 1);

                        context.RewritePath(string.Format("~/ProductDetail.aspx?UK=Product&ProID={0}&CateID={1}", id, catID));
                    }
                    else
                    {
                        context.RewritePath(string.Format("~/ProductDetail.aspx?UK=Product&ProID={0}", id));
                    }
                }
                else if (currentLocation.Contains(HinhSanPham))
                {
                    string tmp = currentLocation.Replace(HinhSanPham, "");
                    string id = tmp.Substring(0, tmp.IndexOf('/'));

                    PQT.API.File.FileManager fileMng = new PQT.API.File.FileManager();
                    string img_path = fileMng.GetImageUrl(Utilis.TryParseLong(id), "ProductImagePath", ImageSizeType.Medium);

                    context.RewritePath(string.Format("~/{0}", img_path));
                }
                else if (currentLocation.Contains(HinhSanPhamBig))
                {
                    string tmp = currentLocation.Replace(HinhSanPhamBig, "");
                    string id = tmp.Substring(0, tmp.IndexOf('/'));

                    PQT.API.File.FileManager fileMng = new PQT.API.File.FileManager();
                    string img_path = fileMng.GetImageUrl(Utilis.TryParseLong(id), "ProductImagePath", ImageSizeType.Big);

                    context.RewritePath(string.Format("~/{0}", img_path));
                }
                else if (currentLocation.Contains(HinhSanPhamThumbnail))
                {
                    string tmp = currentLocation.Replace(HinhSanPhamThumbnail, "");
                    string id = tmp.Substring(0, tmp.IndexOf('/'));

                    PQT.API.File.FileManager fileMng = new PQT.API.File.FileManager();
                    string img_path = fileMng.GetImageUrl(Utilis.TryParseLong(id), "ProductImagePath", ImageSizeType.Small);

                    context.RewritePath(string.Format("~/{0}", img_path));
                }
                else if (currentLocation.Contains(SearchProduct) && currentLocation.Contains(".html"))
                {
                    string tmp = currentLocation.Replace(SearchProduct, "");
                    string id = tmp.Substring(0, tmp.IndexOf(".html"));
                    //string id = Utility.getID_of_URL(currentLocation);
                    context.RewritePath(string.Format("~/Product.aspx?UK=Product&key=1&SearchKey={0}", id));
                }
                else if (currentLocation.Contains(SendEnquiry) && currentLocation.Contains(".html"))
                {
                    string tmp = currentLocation.Replace(SendEnquiry, "");
                    string id = tmp.Substring(0, tmp.IndexOf('/'));
                    tmp = tmp.Substring(id.Length + 1);
                    if (tmp.IndexOf('/') > 0)
                    {
                        string catID = tmp.Substring(0, tmp.IndexOf('/'));
                        context.RewritePath(string.Format("~/TourEnquiry.aspx?CateID={0}&TourID={1}", id, catID));
                    }
                    else
                        context.RewritePath(string.Format("~/TourEnquiry.aspx?TourID={0}", id));
                }
                else if (currentLocation.Contains(Searchtour) && currentLocation.Contains(".html"))
                {
                    string tmp = currentLocation.Replace(Searchtour, "");
                    string id = tmp.Substring(0, tmp.IndexOf(".html"));
                    //string id = Utility.getID_of_URL(currentLocation);
                    context.RewritePath(string.Format("~/SearchTour.aspx?UK=Product&key=1&SearchKey={0}", id));
                }
                else if (currentLocation.Contains(tourdetail) && currentLocation.Contains(".html"))
                {
                    string tmp = currentLocation.Replace(tourdetail, "");
                    string id = tmp.Substring(0, tmp.IndexOf('/'));
                    tmp = tmp.Substring(id.Length + 1);
                    if (tmp.IndexOf('/') > 0)
                    {
                        string catID = tmp.Substring(0, tmp.IndexOf('/'));
                        context.RewritePath(string.Format("~/TourDetail.aspx?CateID={0}&TourID={1}", id, catID));
                    }
                    else
                        context.RewritePath(string.Format("~/TourDetail.aspx?TourID={0}", id));
                }
                else if (!currentLocation.EndsWith(".jpg") && !currentLocation.Contains("RenderModule") && !currentLocation.EndsWith(".js") && !currentLocation.EndsWith(".css") && !currentLocation.EndsWith(".png"))
                {
                    if (currentLocation.StartsWith("/pages/") || currentLocation.StartsWith("/blogs/"))
                    {
                        string keyword = currentLocation.Replace("/pages/", "").Replace("/blogs/", "").Replace(".html", "");
                        NewsCategoryEntity newcate = nBLC.RowNewsCategoryByUniqueKey(keyword);

                        if (currentLocation.StartsWith("/pages/"))
                        {
                            // TNewsCategory ent = blc_news.GetNewCategoryByUniqueKey_AND_KEYGROUP(keyword, "pages");
                            TNewsCategory ent = blc_news.GetNewCategoryByUniqueKey_AND_KeyWord(keyword, "pages");
                            if (ent != null)
                                context.RewritePath("~/content.aspx?id=" + ent.NewsCategoryID.ToString());
                            else context.RewritePath("~/content.aspx?id=" + newcate.NewsCategoryID.ToString());
                        }
                        else if (currentLocation.StartsWith("/blogs/"))
                        {
                            TNewsCategory ent = blc_news.GetNewCategoryByUniqueKey_AND_KeyWord(keyword, "blogs");
                            if (ent != null)
                                context.RewritePath("~/NewsList.aspx?CateID=" + ent.NewsCategoryID.ToString() + "&UK=" + keyword);
                            else context.RewritePath("~/NewsList.aspx?CateID=" + newcate.NewsCategoryID.ToString() + "&UK=" + keyword);
                            //TNewsCategory ent = blc_news.GetNewCategoryByUniqueKey_AND_KeyWord(keyword, "blogs");
                            //var catID = ent.NewsCategoryID.ToString();
                            //if (ent != null)
                            //{
                            //    context.RewritePath("~/NewsList.aspx?CateID=" + catID + "&UK=" + keyword);

                            //}
                            //else
                            //{
                            //    context.RewritePath("~/contact.aspx");

                            //    //TNewsCategory ents = blc_news.GetNewCategoryByUniqueKey_AND_KEYGROUP(keyword, "blogs");
                            //    //var cateid = ents.NewsCategoryID.ToString();
                            //    // var catesid = newcate.NewsCategoryID.ToString();
                            //    //if (ents != null)
                            //    //     context.RewritePath("~/NewsList.aspx?CateID=" + cateid + "&UK=" + keyword);
                            //    // else context.RewritePath("~/NewsList.aspx?CateID=" + catesid + "&UK=" + keyword);

                            //}
                        }

                    }
                    else if (currentLocation.StartsWith("/san-pham/"))
                    {
                        string keyword = currentLocation.Replace("/san-pham/", "").Replace(".html", "");
                      //  ProductCategoryEntity entCat = blc_product.RowProductCategoryByUniqueKey(keyword, 1);
                      //  if (entCat != null)
                      //  {
                       //     context.RewritePath(string.Format("~/Product.aspx?CateID={0}", entCat.CategoryID));
                       // }

                    }
                    else if (currentLocation.Equals("/thong-ke-tuyen-dung"))
                    {
                        context.RewritePath("~/Thongketuyendung.aspx");
                    }
                    else if (currentLocation.Equals("/thiet-lap-email"))
                    {
                        context.RewritePath("~/ThietLapGuiMail.aspx");
                    }
                }

            }

            catch (Exception ex)
            {
                // Response.Redirect(PQT.Common.Config.HTTPServer);

                Response.Redirect("/trang-chu");

            }

        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }


        protected void Session_End(object sender, EventArgs e)
        {



        }

        protected void Application_End(object sender, EventArgs e)
        {


            //Application.Lock();
            //Application["Visitor_Online"] = System.Convert.ToInt32(Session["suer"]) - 1;
            //Application.UnLock();


        }



    }
}