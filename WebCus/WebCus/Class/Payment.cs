using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using PQT.API;
using System.Reflection;
using System.IO;
using PQT.Common;
using System.Text;
using System.Collections;

namespace WebCus
{
    public class Payment
    {
        protected string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }
        //#region OnePay

        //public void Payment_One_Pay(string strhttpServer, long transaction_id, double amount, string pageReturn)
        //{
        //    amount = amount * 100;
        //    string SECURE_SECRET = Config.GetConfigValue("OnePay_SECURE_SECRET"); // "A3EFDFABA8653DF2342E8DAC29B51AF0";
        //    // Khoi tao lop thu vien va gan gia tri cac tham so gui sang cong thanh toan
        //    VPCRequest conn = new VPCRequest(Config.GetConfigValue("OnePay_virtualPaymentClientURL"));
        //    conn.SetSecureSecret(SECURE_SECRET);
        //    // Add the Digital Order Fields for the functionality you wish to use
        //    // Core Transaction Fields
        //    conn.AddDigitalOrderField("Title", "onepay paygate");
        //    conn.AddDigitalOrderField("vpc_Locale", Config.GetConfigValue("OnePay_vpc_Locale"));//Chon ngon ngu hien thi tren cong thanh toan (vn/en)
        //    conn.AddDigitalOrderField("vpc_Version", Config.GetConfigValue("OnePay_vpc_Version"));
        //    conn.AddDigitalOrderField("vpc_Command", Config.GetConfigValue("OnePay_vpc_Command"));
        //    conn.AddDigitalOrderField("vpc_Merchant", Config.GetConfigValue("OnePay_vpc_Merchant"));
        //    conn.AddDigitalOrderField("vpc_AccessCode", Config.GetConfigValue("OnePay_vpc_AccessCode"));
        //    conn.AddDigitalOrderField("vpc_MerchTxnRef", string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), transaction_id));
        //    conn.AddDigitalOrderField("vpc_OrderInfo", "MS" + transaction_id.ToString());
        //    conn.AddDigitalOrderField("vpc_Amount", amount.ToString("G"));
        //    conn.AddDigitalOrderField("vpc_Currency", Config.GetConfigValue("OnePay_vpc_Currency"));
        //    conn.AddDigitalOrderField("vpc_ReturnURL", strhttpServer + "/" + pageReturn);
        //    // Thong tin them ve khach hang. De trong neu khong co thong tin
        //    conn.AddDigitalOrderField("vpc_SHIP_Street01", "194 Tran Quang Khai");
        //    conn.AddDigitalOrderField("vpc_SHIP_Provice", "Hanoi");
        //    conn.AddDigitalOrderField("vpc_SHIP_City", "Hanoi");
        //    conn.AddDigitalOrderField("vpc_SHIP_Country", "Vietnam");
        //    conn.AddDigitalOrderField("vpc_Customer_Phone", "043966668");
        //    conn.AddDigitalOrderField("vpc_Customer_Email", "support@onepay.vn");
        //    conn.AddDigitalOrderField("vpc_Customer_Id", "onepay_paygate");
        //    // Dia chi IP cua khach hang
        //    conn.AddDigitalOrderField("vpc_TicketNo", GetUser_IP());
        //    // Chuyen huong trinh duyet sang cong thanh toan
        //    String url = conn.Create3PartyQueryString();
        //    HttpContext.Current.Response.Redirect(url);
        //    /* old vs 01
        //    string virtualPaymentClientURL = Config.GetConfigValue("OnePay_virtualPaymentClientURL");
        //    string vpc_AccessCode = Config.GetConfigValue("OnePay_vpc_AccessCode");
        //    string vpc_Command = Config.GetConfigValue("OnePay_vpc_Command");
        //    string vpc_Currency = Config.GetConfigValue("OnePay_vpc_Currency");
        //    string vpc_Locale = Config.GetConfigValue("OnePay_vpc_Locale");
        //    string vpc_MerchTxnRef = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), transaction_id);
        //    string vpc_Merchant = Config.GetConfigValue("OnePay_vpc_Merchant");
        //    string vpc_OrderInfo = Config.GetConfigValue("OnePay_vpc_OrderInfo");
        //    string vpc_ReturnURL = strhttpServer + "/" + pageReturn;// "/payment_complete_onepay.aspx";// + type_room_tour;// sau khi thanh toan tra ve trang chi dinh
        //    string vpc_TicketNo = Config.GetConfigValue("OnePay_vpc_TicketNo");
        //    string vpc_Version = Config.GetConfigValue("OnePay_vpc_Version");

        //    amount = amount * 100;

        //    //amount = 50000000;

        //    int rows = 13;
        //    string seperator = "?";
        //    string SECURE_SECRET = Config.GetConfigValue("OnePay_SECURE_SECRET");// "A3EFDFABA8653DF2342E8DAC29B51AF0";
        //    string[,] MyArray =
        //        {
        //        {"AgainLink",virtualPaymentClientURL},
        //        {"Title","ASP VPC 3-Party" }  ,
        //        {"vpc_AccessCode",vpc_AccessCode},
        //        {"vpc_Amount",amount.ToString("G")},
        //        {"vpc_Command",vpc_Command},
        //        {"vpc_Currency",vpc_Currency} ,	
        //        {"vpc_Locale",vpc_Locale},  
        //        {"vpc_MerchTxnRef",vpc_MerchTxnRef},
        //        {"vpc_Merchant",vpc_Merchant},
        //        {"vpc_OrderInfo",vpc_OrderInfo},
        //        {"vpc_ReturnURL",vpc_ReturnURL}, 									
        //        {"vpc_TicketNo",vpc_TicketNo},
        //        {"vpc_Version",vpc_Version},
							
        //        };
        //    string redirectURL = virtualPaymentClientURL;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        //redirectURL = redirectURL + seperator + MyArray[i,0] + "=" + MyArray[i,1];
        //        redirectURL = redirectURL + seperator + HttpContext.Current.Server.UrlEncode(MyArray[i, 0]) + "=" + HttpContext.Current.Server.UrlEncode(MyArray[i, 1]);
        //        //redirectURL = redirectURL + seperator + HttpUtility.UrlEncode(MyArray[i,0].ToString(),System.Text.Encoding.Default) + "=" + HttpUtility.UrlEncode(MyArray[i,1].ToString(),System.Text.Encoding.GetEncoding("ISO-8859-1"));
        //        seperator = "&";

        //    }

        //    string md5HashData = SECURE_SECRET;
        //    for (int k = 0; k < rows; k++)
        //    {
        //        string tmp = MyArray[k, 1].ToString();
        //        if (tmp.Length > 0)
        //        {
        //            md5HashData = md5HashData + tmp;
        //        }
        //    }
        //    string doSecureHash = DoMD5(md5HashData);
        //    redirectURL = redirectURL.Replace("_", "%5F");
        //    redirectURL = redirectURL + seperator + "vpc_SecureHash=" + doSecureHash;

        //    HttpContext.Current.Response.Redirect(redirectURL);
        //     * */
        //}

        //public static string ToHexa(byte[] data)
        //{
        //    System.Text.StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < data.Length; i++)
        //        sb.AppendFormat("{0:X2}", data[i]);
        //    return sb.ToString();
        //}
        //public static string DoMD5(string SData)
        //{
        //    System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        //    System.Text.UTF8Encoding encode = new System.Text.UTF8Encoding();
        //    byte[] result1 = md5.ComputeHash(encode.GetBytes(SData));
        //    string sResult2 = ToHexa(result1);
        //    return sResult2;
        //}

        //#endregion
        #region OnePay

        public void Payment_One_Pay(string strhttpServer, long transaction_id, double amount, string pageReturn)
        {

            string virtualPaymentClientURL = Config.GetConfigValue("OnePay_virtualPaymentClientURL");
            string vpc_AccessCode = Config.GetConfigValue("OnePay_vpc_AccessCode");
            string vpc_Command = Config.GetConfigValue("OnePay_vpc_Command");
            string vpc_Currency = Config.GetConfigValue("OnePay_vpc_Currency");
            string vpc_Locale = Config.GetConfigValue("OnePay_vpc_Locale");
            string vpc_MerchTxnRef = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), transaction_id);
            string vpc_Merchant = Config.GetConfigValue("OnePay_vpc_Merchant");
            string vpc_OrderInfo = Config.GetConfigValue("OnePay_vpc_OrderInfo");
            string vpc_ReturnURL = strhttpServer + "/" + pageReturn;// "/payment_complete_onepay.aspx";// + type_room_tour;// sau khi thanh toan tra ve trang chi dinh
            string vpc_TicketNo = Config.GetConfigValue("OnePay_vpc_TicketNo");
            string vpc_Version = Config.GetConfigValue("OnePay_vpc_Version");

            amount = amount * 100;

            //amount = 50000000;

            int rows = 13;
            string seperator = "?";
            string SECURE_SECRET = Config.GetConfigValue("OnePay_SECURE_SECRET");// "A3EFDFABA8653DF2342E8DAC29B51AF0";
            string[,] MyArray =
			    {
			    {"AgainLink",virtualPaymentClientURL},
			    {"Title","ASP VPC 3-Party" }  ,
			    {"vpc_AccessCode",vpc_AccessCode},
			    {"vpc_Amount",amount.ToString("G")},
			    {"vpc_Command",vpc_Command},
                {"vpc_Currency",vpc_Currency} ,	
			    {"vpc_Locale",vpc_Locale},  
			    {"vpc_MerchTxnRef",vpc_MerchTxnRef},
			    {"vpc_Merchant",vpc_Merchant},
			    {"vpc_OrderInfo",vpc_OrderInfo},
			    {"vpc_ReturnURL",vpc_ReturnURL}, 									
			    {"vpc_TicketNo",vpc_TicketNo},
			    {"vpc_Version",vpc_Version},
							
			    };
            string redirectURL = virtualPaymentClientURL;
            for (int i = 0; i < rows; i++)
            {
                //redirectURL = redirectURL + seperator + MyArray[i,0] + "=" + MyArray[i,1];
                redirectURL = redirectURL + seperator + HttpContext.Current.Server.UrlEncode(MyArray[i, 0]) + "=" + HttpContext.Current.Server.UrlEncode(MyArray[i, 1]);
                //redirectURL = redirectURL + seperator + HttpUtility.UrlEncode(MyArray[i,0].ToString(),System.Text.Encoding.Default) + "=" + HttpUtility.UrlEncode(MyArray[i,1].ToString(),System.Text.Encoding.GetEncoding("ISO-8859-1"));
                seperator = "&";

            }

            string md5HashData = SECURE_SECRET;
            for (int k = 0; k < rows; k++)
            {
                string tmp = MyArray[k, 1].ToString();
                if (tmp.Length > 0)
                {
                    md5HashData = md5HashData + tmp;
                }
            }
            string doSecureHash = DoMD5(md5HashData);
            redirectURL = redirectURL.Replace("_", "%5F");
            redirectURL = redirectURL + seperator + "vpc_SecureHash=" + doSecureHash;

            HttpContext.Current.Response.Redirect(redirectURL);
        }

        public static string ToHexa(byte[] data)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.AppendFormat("{0:X2}", data[i]);
            return sb.ToString();
        }
        public static string DoMD5(string SData)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            System.Text.UTF8Encoding encode = new System.Text.UTF8Encoding();
            byte[] result1 = md5.ComputeHash(encode.GetBytes(SData));
            string sResult2 = ToHexa(result1);
            return sResult2;
        }

        #endregion

        #region Ngan_Luong

        public void Payment_NganLuong(long transaction_id, double Amount, string Buyer_Name, string Buyer_Email, string Buyer_Tel, string Buyer_Address,string backURL)
        {
            String merchant_site_code = Config.GetConfigValue("NganLuong_merchant_site_code");	//thay ma merchant site ma ban da dang ky vao day
            String secure_pass = Config.GetConfigValue("NganLuong_secure_pass");	//thay mat khau giao tiep giua website cua ban voi NganLuong.vn ma ban da dang ky vao day
            String nganluong_url = "http://sandbox.nganluong.vn/checkout.php";

            string return_url = backURL;//Config.GetConfigValue("HTTPServer") + "/payment_complete_nganluong.aspx";
            string receiver = Config.GetConfigValue("NganLuong_Email_Receiver");
            string transaction_info = "";
            string order_code = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), transaction_id);
            string price = (Amount).ToString("G0");
            string currency = "vnd";
            string quantity = "1";
            string tax = "0";
            string discount = "0";
            string fee_cal = "0";
            string fee_shipping = "0";
            string order_description = "";
            string buyer_info = string.Format("{0}*|*{1}*|*{2}*|*{3}", Buyer_Name, Buyer_Email, Buyer_Tel, Buyer_Address);// Session["User_Email"].ToString();
            string affiliate_code = "";

            string redirectURL_Ngan_Luong = buildCheckoutUrlNew(merchant_site_code, secure_pass, nganluong_url, return_url, receiver, transaction_info, order_code, price, currency, quantity, tax
                        , discount, fee_cal, fee_shipping, order_description, buyer_info, affiliate_code);
            HttpContext.Current.Response.Redirect(redirectURL_Ngan_Luong);

        }

        public String GetMD5Hash(String input)
        {

            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);

            bs = x.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {

                s.Append(b.ToString("x2").ToLower());

            }

            String md5String = s.ToString();

            return md5String;
        }

        public String buildCheckoutUrlNew(string merchant_site_code, string secure_pass, string nganluong_url, String return_url, String receiver, String transaction_info, String order_code, String price, String currency = "vnd", String quantity = "0", String tax = "0", String discount = "0", String fee_cal = "0", String fee_shipping = "0", String order_description = "", String buyer_info = "", String affiliate_code = "")
        {
            // Tao bien secure code
            String secure_code = "";

            secure_code += merchant_site_code;
            secure_code += " " + return_url.ToLower();
            secure_code += " " + receiver;
            secure_code += " " + transaction_info;
            secure_code += " " + order_code;
            secure_code += " " + price;
            secure_code += " " + currency;
            secure_code += " " + quantity;
            secure_code += " " + tax;
            secure_code += " " + discount;
            secure_code += " " + fee_cal;
            secure_code += " " + fee_shipping;
            secure_code += " " + order_description;
            secure_code += " " + buyer_info;
            secure_code += " " + affiliate_code;
            secure_code += " " + secure_pass;

            // Tao mang bam
            Hashtable ht = new Hashtable();

            ht.Add("merchant_site_code", merchant_site_code);
            ht.Add("return_url", return_url.ToLower());
            ht.Add("receiver", receiver);
            ht.Add("transaction_info", transaction_info);
            ht.Add("order_code", order_code);
            ht.Add("price", price);
            ht.Add("currency", currency);
            ht.Add("quantity", quantity);
            ht.Add("tax", tax);
            ht.Add("discount", discount);
            ht.Add("fee_cal", fee_cal);
            ht.Add("fee_shipping", fee_shipping);
            ht.Add("order_description", order_description);
            ht.Add("buyer_info", buyer_info);
            ht.Add("affiliate_code", affiliate_code);
            ht.Add("secure_code", this.GetMD5Hash(secure_code));

            // Tao url redirect
            String redirect_url = nganluong_url;

            if (redirect_url.IndexOf("?") == -1)
            {
                redirect_url += "?";
            }
            else if (redirect_url.Substring(redirect_url.Length - 1, 1) != "?" && redirect_url.IndexOf("&") == -1)
            {
                redirect_url += "&";
            }

            String url = "";

            // Duyet cac phan tu trong mong bam ht1 de tao redirect url
            IDictionaryEnumerator en = ht.GetEnumerator();

            while (en.MoveNext())
            {
                if (url == "")
                    url += en.Key.ToString() + "=" + HttpUtility.UrlEncode(en.Value.ToString());
                else
                    url += "&" + en.Key.ToString() + "=" + HttpUtility.UrlEncode(en.Value.ToString());
            }

            String rdu = redirect_url + url;

            return rdu;
        }



        #endregion

        #region BaoKim

        public void Payment_BaoKim(long transaction_id, double Amount, string url_detail, string url_success, string url_cancel)
        {
            //URL checkout của BaoKim.vn
            string baokim_url = "https://www.baokim.vn/payment/customize_payment/order/version11";

            //Mã merchant site
            string merchant_id = Config.GetConfigValue("BaoKim_merchant_id"); //"100001";	//Thay bằng mã merchant site bạn đã đăng ký trên BaoKim.vn

            //Mật khẩu bảo mật
            string secure_pass = Config.GetConfigValue("BaoKim_secure_pass"); //"DED01D1CFF3BE2767196FF0080F6DB6D5C";	//Thay bằng mật khẩu giao tiếp giữa website của bạn với BaoKim.vn

            //string url_success = Config.GetConfigValue("HTTPServer") + "/payment_bao_kim_success.aspx";
            //string url_cancel = Config.GetConfigValue("HTTPServer") + "/payment_bao_kim_cancel.aspx";

            string receiver = Config.GetConfigValue("BaoKim_Email_Receiver");
            string order_code = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), transaction_id);
            string price = (Amount).ToString("G0");
            string tax = "0";
            string fee_shipping = "0";
            string order_description = "";

            string url = createRequestUrl(merchant_id, secure_pass, baokim_url, order_code, receiver, price, fee_shipping, tax, order_description, url_success, url_cancel, url_detail);
            HttpContext.Current.Response.Redirect(url);

        }

        /**
         * Hàm thực hiện việc mã hóa, tạo khóa trên đường dẫn
         * @param messages xâu gốc
         * @return kết quả mã hóa
         * @throws Exception
        
        private string GetMD5Hash(String input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            String md5String = s.ToString();
            return md5String;
        } */

        /**
         * Hàm xây dựng url chuyển đến BaoKim.vn thực hiện thanh toán, trong đó có tham số mã hóa (còn gọi là public key)
         * @param $order_id				Mã đơn hàng
         * @param $business 			Email tài khoản người bán
         * @param $total_amount			Giá trị đơn hàng
         * @param $shipping_fee			Phí vận chuyển
         * @param $tax_fee				Thuế
         * @param $order_description	Mô tả đơn hàng
         * @param $url_success			Url trả về khi thanh toán thành công
         * @param $url_cancel			Url trả về khi hủy thanh toán
         * @param $url_detail			Url chi tiết đơn hàng
         * @return url cần tạo
         */
        public String createRequestUrl(string merchant_id, string secure_pass, string baokim_url, String order_id, String business, String total_amount, String shipping_fee, String tax_fee, String order_description, String url_success, String url_cancel, String url_detail)
        {
            Hashtable order_params = new Hashtable();
            order_params.Add("merchant_id", merchant_id);
            order_params.Add("order_id", order_id);
            order_params.Add("business", business);
            order_params.Add("total_amount", total_amount);
            order_params.Add("shipping_fee", shipping_fee);
            order_params.Add("tax_fee", tax_fee);
            order_params.Add("order_description", order_description);
            order_params.Add("url_success", url_success);
            order_params.Add("url_cancel", url_cancel);
            order_params.Add("url_detail", url_detail);

            //Sắp xếp các tham số theo key để tiến hành mã hóa
            ICollection keyCollection = order_params.Keys;
            string[] keys = new string[keyCollection.Count];
            keyCollection.CopyTo(keys, 0);
            Array.Sort(keys);

            //Mã hóa tạo checksum
            String str_combined = secure_pass;
            foreach (string key in keys)
            {
                Object value = order_params[key];
                str_combined += value.ToString();
            }
            String checksum = GetMD5Hash(str_combined).ToUpper();


            //Tạo url redirect
            String redirect_url = baokim_url;

            if (redirect_url.IndexOf("?") == -1)
            {
                redirect_url += "?";
            }
            else if (redirect_url.Substring(redirect_url.Length - 1, 1) != "?" && redirect_url.IndexOf("&") == -1)
            {
                redirect_url += "&";
            }

            String url_params = "";
            foreach (string key in keys)
            {
                Object value = order_params[key];
                if (url_params == "")
                    url_params += key.ToString() + "=" + HttpContext.Current.Server.UrlEncode(value.ToString());
                else
                    url_params += "&" + key.ToString() + "=" + HttpContext.Current.Server.UrlEncode(value.ToString());
            }
            url_params += "&checksum=" + checksum;

            return redirect_url + url_params;
        }


        #endregion


    }
}