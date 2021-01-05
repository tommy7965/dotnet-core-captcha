// ====================================================================
// Creates the bitmap image.
// ====================================================================
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace dotnet_mvc.Services
{
    public class GenerateBitmapService
    {
        public Bitmap createCaptcha(int width, int height, String captchaText)
        {
            //First declare a bitmap and declare graphic from this bitmap
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            //And create a rectangle to delegete this image graphic 
            Rectangle rect = new Rectangle(0, 0, width, height);
            //And create a brush to make some drawings
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.DottedGrid, Color.White, Color.LightGray);
            g.FillRectangle(hatchBrush, rect);

            //here we make the text configurations
            GraphicsPath graphicPath = new GraphicsPath();
            //add this string to image with the rectangle delegate
            graphicPath.AddString(captchaText, FontFamily.GenericMonospace, (int)FontStyle.Bold, 70, rect, null);
            //And the brush that you will write the text
            hatchBrush = new HatchBrush(HatchStyle.Percent80, Color.Black, Color.White);
            g.FillPath(hatchBrush, graphicPath);
            // We are adding the dots to the image
            Random rnd = new Random();
            for (int i = 0; i < (int)(rect.Width * rect.Height / 150F); i++)
            {
                int x = rnd.Next(width);
                int y = rnd.Next(height);
                int w = rnd.Next(10);
                int h = rnd.Next(10);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }
            //Remove all of variables from the memory to save resource
            hatchBrush.Dispose();
            g.Dispose();
            //return the image to the related component
            return bitmap;
        }
    }
}

