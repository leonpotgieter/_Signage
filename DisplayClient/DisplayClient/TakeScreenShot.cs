using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace DisplayClient
{
    public partial class TakeScreenShot : Form
    {
        public static string filename;
        public TakeScreenShot()
        {
            InitializeComponent();
            this.Top = 1024;
            try
            {
                DumpScreen();
            }
            catch (Exception ex)
            {
            }
            this.Close();
        }

        public static void DumpScreen()
        {
            try
            {
                Bitmap screenBitmap = new Bitmap(320, 240);
                //240,180
                Bitmap compressedScreenBitmap = new Bitmap(320, 240);
                screenBitmap = ExtractScreenShot();
                compressedScreenBitmap = resizeImage((Image)screenBitmap, new Size(320, 240));
                //string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string saveToFileName = @"c:\tmp\screendump.jpg";
                compressedScreenBitmap.Save(saveToFileName, ImageFormat.Jpeg);
            }
            catch (Exception)
            {   
            }   
        }

        public static long GetSize()
        {
            long s = 2;
            try
            {
                //Bitmap screenBitmap = new Bitmap(640, 480);
                //Bitmap compressedScreenBitmap = new Bitmap(640, 480);
                //screenBitmap = ExtractScreenShot();
                ////compressedScreenBitmap = resizeImage((Image)screenBitmap, new Size(240, 180));
                //string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string saveToFileName = @"c:\tmp\screendump.jpg";
                //compressedScreenBitmap.Save(saveToFileName, ImageFormat.Jpeg);
                FileInfo f = new FileInfo(saveToFileName);
                s = f.Length;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return s;
        }

        public static Bitmap ExtractScreenShot()
        {
            Rectangle totalSize = Rectangle.Empty;

            //foreach (Screen allScreen in Screen.AllScreens)
             //   totalSize = Rectangle.Union(totalSize, allScreen.Bounds);
            
            //Added this for testing
            Rectangle r = new Rectangle(0,0,1024,755);
            totalSize = Rectangle.Union(totalSize, r);

            //Bitmap screenShotBMP = new Bitmap(
            //    totalSize.Width, totalSize.Height,
            //    PixelFormat.Format32bppArgb);

            Bitmap screenShotBMP = new Bitmap(
                1024, 755,
                PixelFormat.Format32bppArgb);

            Graphics screenShotGraphics = Graphics.FromImage(screenShotBMP);

            screenShotGraphics.CopyFromScreen(
                totalSize.Location.X,
                totalSize.Location.Y,
                0, 0, totalSize.Size,
                CopyPixelOperation.SourceCopy);

            return screenShotBMP;
        }

        private static Bitmap resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Bitmap)b;
        }

        private void TakeScreenShot_Load(object sender, EventArgs e)
        {

        }
    }
}
