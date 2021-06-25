using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.IO;

/// <summary>
/// Summary description for compressImage
/// </summary>
public class CompressImage
{
    public static Bitmap Resize_Image(Stream streamImage, int maxWidth, int maxHeight)
    {
        Bitmap originalImage = new Bitmap(streamImage);
        int newWidth = originalImage.Width;
        int newHeight = originalImage.Height;
        double aspectRatio = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height);
        if (aspectRatio <= 1 && originalImage.Width > maxWidth)
        {
            newWidth = maxWidth;
            newHeight = Convert.ToInt32(Math.Round(newWidth / aspectRatio));
        }
        else if (aspectRatio > 1 && originalImage.Height > maxHeight)
        {
            newHeight = maxHeight;
            newWidth = Convert.ToInt32(Math.Round(newHeight * aspectRatio));
        }
        return new Bitmap(originalImage, newWidth, newHeight);
    }  
}