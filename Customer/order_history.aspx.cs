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
using System.Net.Mail;

public partial class _Default : System.Web.UI.Page
{
    public String SessionText = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_msg.Text = "";
        ltr_scripts.Text = "";
        this.Title = CommonCode.SetPageTitle("Order History");
        if (Session["CustID"] != null && Session["CustID"].ToString() != "guest")
        {
            SessionText = Session["CustID"].ToString();
            if (!IsPostBack)
            {
                load_my_orders();
            }
        }
        else
        {
            Response.Redirect("user_login.aspx", true);
        }
    }

    private void load_my_orders()
    {
        SqlCommand com = new SqlCommand("Web_dbo_get_my_orders_Ecommerce", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = Session["CustID"];
        com.Parameters.Add("@isAll", SqlDbType.Bit).Value = true;
        com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = 0;        
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
        SqlDataAdapter ad = new SqlDataAdapter(com);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            dt.Columns.Add("StatusClass");
            dt.Columns.Add(new DataColumn("OrderNo"));
            foreach (DataRow rr in dt.Rows)
            {
                if (rr["StatusRemark"].ToString() == "Completed")
                    rr["StatusClass"] = "status-completed";
                else if (rr["StatusRemark"].ToString() == "Closed")
                    rr["StatusClass"] = "status-closed";
                else if (rr["StatusRemark"].ToString() == "Cancelled")
                    rr["StatusClass"] = "status-cancelled";
                else if (rr["StatusRemark"].ToString() == "Processing")
                    rr["StatusClass"] = "status-processing";
                else if (rr["StatusRemark"].ToString() == "Pending" || rr["StatusRemark"].ToString() == "Order Placed")
                    rr["StatusClass"] = "status-pending";
                string encryptedValue = HttpUtility.UrlEncode(Encrypt(rr["Orderid"].ToString()));
                rr["OrderNo"] = encryptedValue;
            }
            rp_orders.DataSource = dt;
            rp_orders.DataBind();
        }
        else
        {
            CommonCode.show_alert("danger", "No Order yet", "You haven`t made any Order yet", ltr_msg);
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