using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net;
using System.IO;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_msg.Text = "";
        if (Session["pay_response"] != null)
        {
            string[] pay_response = (string[])Session["pay_response"];
            if (pay_response[0] == "success")
            {
                string encryptedValue = HttpUtility.UrlEncode(Encrypt(pay_response[3].ToString()));

                ltr_msg2.Text = "<a class='btn btn-lg btn-success' href='order_summary.aspx?orderid=" + encryptedValue + "'>View Order Summmary</a>";
            }
            CommonCode.show_alert(pay_response[0], pay_response[1], pay_response[2], false, ltr_msg);
            this.Title = CommonCode.SetPageTitle(pay_response[1]);
        }
        else
        {
            this.Title = CommonCode.SetPageTitle("Payment Unsuccessful");
            CommonCode.show_alert("error", "Error while Processing payment", "Session expired ! Please try again later.", ltr_msg);
        } 
    }

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
}