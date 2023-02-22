using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WebCus
{
    public class SoftGenImage
    {
        public static string genStr;
        public SoftGenImage()
        {

        }
        public static string genString(int strLength)
        {
            string str = "";
            Random rand = new Random();
            //str += Convert.ToChar(rand.Next(69,93));
            for (int i = 0; i < strLength; i++)
            {
                str += Convert.ToChar(rand.Next(65, 90));
            }
            //System.Web.HttpContext.Current.Session["GenStr"] = str;
            genStr = str;
            return str;
        }
        public static string setGenString(string p_string)
        {
            genStr = p_string;
            return genStr;
        }
        public static Bitmap CreateImage(string sImageText)
        {
            int minNumberLine = 1; // số lượng line min int minNumberLine = 0;
            int maxNumberLine = 1; // số lượng line max
            int maxWidthLine = 1; // độ dày line max

            int minFontSize = 16; //int minFontSize = 15; 
            int maxFontSize = 16;
            int spaceLetter = 18; // khoảng cách các từ
            int maxSpaceLetter = 20; // khoảng cách các từ

            //string fontName = "Algerian";
            string fontName = "Tahoma";

            Bitmap bmpImage = new Bitmap(300, 200);

            int iWidth = 0;
            int iHeight = 0;


            Random rnd = new Random();

            // Create the Font object for the image text drawing.
            //Font MyFont = new Font(FontFamily.GenericSansSerif.Name, 250);

            Font MyFont = new Font(fontName, maxFontSize,
                               System.Drawing.FontStyle.Bold,
                               System.Drawing.GraphicsUnit.Pixel);

            // Create a graphics object to measure the text's width and height.

            Graphics MyGraphics = Graphics.FromImage(bmpImage);

            // This is where the bitmap size is determined.

            iWidth = (int)MyGraphics.MeasureString(sImageText, MyFont).Width + 20;
            iHeight = (int)MyGraphics.MeasureString(sImageText, MyFont).Height;
            int maxRand = iWidth;
            int lines = rnd.Next(minNumberLine, maxNumberLine);
            // Create the bmpImage again with the correct size for the text and font.

            bmpImage = new Bitmap(bmpImage, new Size(iWidth + rnd.Next(spaceLetter, maxSpaceLetter), iHeight));

            // Add the colors to the new bitmap.

            MyGraphics = Graphics.FromImage(bmpImage);
            MyGraphics.Clear(Color.FromArgb(255, 255, 255));//Mau nen backgroud
            MyGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            for (int i = 0; i < lines; i++)
            {
                MyGraphics.DrawLine(new Pen(Color.FromArgb(rnd.Next(0), rnd.Next(0), rnd.Next(0)), rnd.Next(maxWidthLine)),
                    new Point(rnd.Next(maxRand), rnd.Next(maxRand)),
                    new Point(rnd.Next(maxRand), rnd.Next(maxRand)));
            }

            MyGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;//TextRenderingHint.AntiAlias;
            PointF pointf = new PointF(0, 0);
            //MyGraphics.DrawString(sImageText, MyFont,
            //                    new SolidBrush(Color.Goldenrod), 0, 0);
            char[] temCharArray = sImageText.ToCharArray();
            int position = 0;
            foreach (char temChar in temCharArray)
            {
                MyFont = new Font(fontName, 17,
                               System.Drawing.FontStyle.Bold,
                               System.Drawing.GraphicsUnit.Pixel);
                MyGraphics.DrawString(temChar.ToString(), MyFont,
                                new SolidBrush(Color.FromArgb(0,0,0)), position, 0);
                position += spaceLetter;
            }
            //MyFont = new Font(fontName, rnd.Next(minFontSize, iHeight),
            //                   System.Drawing.FontStyle.Bold,
            //                   System.Drawing.GraphicsUnit.Pixel);
            //MyGraphics.DrawString(temChar.ToString(), MyFont,
            //                new SolidBrush(Color.FromArgb(0, 0, 0)), position, 0);
            //position += spaceLetter;
            //MyGraphics.DrawString(temChar.ToString(), MyFont,
            //                    new SolidBrush(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))), position, 0);
            //MyGraphics.DrawString(sImageText, MyFont,
            //                    new SolidBrush(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))), rnd.Next(-80, 80), rnd.Next(-80, 80));


            MyGraphics.Flush();
            return (bmpImage);
        }

        public bool ThumbnailCallback()
        {
            return true;
        }
    }
}
