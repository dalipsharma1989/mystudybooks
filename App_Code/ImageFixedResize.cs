using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
/// <summary>
/// Summary description for ImageFixedResize
/// </summary>
public class ImageFixedResize
{
    enum BackColor { Transparent,Dominant,White };

    public static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height, out String errmsg)
    {
        Bitmap bmPhoto = null;
        try
        {
            System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgPhoto);
            //Color DominantColor = getDominantColor(bmpPostedImage);
            //// if (DominantColor.IsEmpty)
            //DominantColor = Color.Transparent;
            Color DominantColor = Color.Transparent;
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            bmPhoto = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            bmPhoto.MakeTransparent(Color.Transparent);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(DominantColor);
            grPhoto.InterpolationMode =   InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,  new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();
            errmsg = "success";
        }
        catch (Exception ex)
        {
            bmPhoto = null;
            errmsg = ex.Message;
        }
        return bmPhoto;

    }

    public static Color getDominantColor(Bitmap bmp)
    {
        //Used for tally
        int r = 0;
        int g = 0;
        int b = 0;

        int total = 0;

        for (int x = 0; x < bmp.Width; x++)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                Color clr = bmp.GetPixel(x, y);

                r += clr.R;
                g += clr.G;
                b += clr.B;

                total++;
            }
        }

        //Calculate average
        r /= total;
        g /= total;
        b /= total;

        return Color.FromArgb(r, g, b);
    }
}