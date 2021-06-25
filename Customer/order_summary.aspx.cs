using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    public static string ShippingAmountText = "",subtotl = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Order Summary");
        if (Session["CustID"] != null && Session["CustID"].ToString() != "guest")
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["orderid"]))
                {
                    String ord = Request.QueryString["orderid"];
                    String OID = "";
                    try
                    {
                        OID = Decrypt(HttpUtility.UrlDecode(ord));
                        load_order_summary(OID);
                    }
                    catch (FormatException ex)
                    {
                        OID = "";
                        Response.Redirect("user_login.aspx");
                    }
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    Session.RemoveAll();
                    System.Web.Security.FormsAuthentication.SignOut();
                    CommonCode.show_alert("danger", "Invalid Request", "", ltr_msg);
                    Response.Redirect("user_login.aspx");
                }
            }
        }
        else
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("user_login.aspx", true);
        }
    }


    private void load_order_summary(string OrderID)
    {
         

        String Address = "<address>" +
                           "<strong>{0}</strong><br>" +
                           "<abbr title='EmailID'>Address:</abbr><span>  {1} </span><br>" +
                           "<abbr title='EmailID'>Email:</abbr><span>  {2} </span><br>" +
                           "<abbr title='Phone'>Mobile No.:</abbr> <span> {3} </span><br>" +
                           "{4}-{5}<br>" +
                           "{6}, {7}" +
                           "</address>";
        String ShippingAddress = "<address>" +
                           "<strong>{0}</strong><br>" +
                           "<abbr title='EmailID'>Address:</abbr><span>  {1} </span><br>" +
                           "<abbr title='EmailID'>Email:</abbr><span>  {2} </span><br>" +
                           "<abbr title='Phone'>Mobile No.:</abbr> <span> {3} </span><br>" +
                           "{4}-{5}<br>" +
                           "{6}, {7}" +
                           "</address>";
        SqlCommand com = new SqlCommand("Web_dbo_get_my_orders_Ecommerce", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value =  Session["CustID"];
        com.Parameters.Add("@isAll", SqlDbType.Bit).Value = false;
        com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = OrderID;
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
        SqlDataAdapter ad = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        ad.Fill(ds);
        if (ds.Tables.Count > 0)
        {
            h3_orderid.InnerText = "Order ID : " + ds.Tables[0].Rows[0]["OrderID"] + "";
            string pubstatus;
            //pubstatus =  ds.Tables[0].Rows[0]["PubStatus"].ToString();
            //if (pubstatus == "")
            //{
            //    h2.InnerHtml = " ORDER STATUS : PENDING"; 
            //}
            //else
            //{
            //    h2.InnerHtml = " ORDER STATUS : " + ds.Tables[0].Rows[0]["PubStatus"] + "";
            //}
            div_ship_address.InnerHtml = string.Format(ShippingAddress, ds.Tables[0].Rows[0]["CustName"], ds.Tables[0].Rows[0]["ShipAddress"], ds.Tables[0].Rows[0]["ShipEmailID"],
                ds.Tables[0].Rows[0]["ShipPhone"], ds.Tables[0].Rows[0]["ShipCityName"], ds.Tables[0].Rows[0]["ShipPostalCode"],
                ds.Tables[0].Rows[0]["ShipStateName"], ds.Tables[0].Rows[0]["ShipCountryName"]);

            div_bill_address.InnerHtml = string.Format(Address, ds.Tables[0].Rows[0]["CustName"], ds.Tables[0].Rows[0]["BillingAddress"], ds.Tables[0].Rows[0]["EmailID"],
               ds.Tables[0].Rows[0]["BillingPhone"], ds.Tables[0].Rows[0]["BillCityName"], ds.Tables[0].Rows[0]["BillingPostalCode"],
               ds.Tables[0].Rows[0]["BillStateName"], ds.Tables[0].Rows[0]["BillCountryName"]);

            span_order_date.InnerText = "Order Date : " + string.Format("{0:dd MMM, yyyy}", ds.Tables[0].Rows[0]["OrderDate"]) + "";


            ds.Tables[0].Columns.Add("StatusClass");
            foreach (DataRow rr in ds.Tables[0].Rows)
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
            }
            //h4_status.Attributes["class"] = "label " + ds.Tables[0].Rows[0]["StatusClass"] + "";
            h4_status.InnerText = "Order Status: " + ds.Tables[0].Rows[0]["OrderStatus"] + "";
            td_total_amount.InnerHtml = "<span id='ttl-amt' class='ttl-amt pulsor'>  &nbsp;" + ds.Tables[1].Rows[0]["SaleCurrency"] + " " + string.Format(CommonCode.AmountFormat(), ds.Tables[0].Rows[0]["TotalNetAmount"].ToString()) + "</span>";           

            if (ds.Tables[2].Rows[0]["CardNum"].ToString() != "" && ds.Tables[2].Rows[0]["CardNum"].ToString() != null)
            {
                dv_payinfo.Visible = true;

                if(ds.Tables[2].Rows[0]["CardType"].ToString() == "COD")
                {
                    h_paymentInfo.InnerHtml = "Payment Method - Cash on Delivery.";
                }
                else
                {
                    h_paymentInfo.InnerHtml = "Payment Method - " + ds.Tables[2].Rows[0]["CardType"].ToString() + " ending with XXXX-" + ds.Tables[2].Rows[0]["CardNum"].ToString() + ".";
                }
                
            }
            else
            {
                dv_payinfo.Visible = false;
            }
            if(ds.Tables[0].Rows[0]["OrderStatus"].ToString() != "ORDER PLACED")
            {
                h4_invno.InnerHtml = "Invoice No:- " +  ds.Tables[0].Rows[0]["trnsdocno"].ToString();
            }
            if (ds.Tables[0].Rows[0]["TrnsGrNo"].ToString() != "" && ds.Tables[0].Rows[0]["TrnsGrNo"].ToString() != null)
            {
                h4_awbno.InnerHtml = "Airway No:- " + ds.Tables[0].Rows[0]["TrnsGrNo"].ToString();
                h4_Date.InnerHtml = "Airway Date:- " + string.Format("{0:dd MMM, yyyy}", ds.Tables[0].Rows[0]["TrnsGrDate"]) ;
                trackingUrl.HRef = ds.Tables[0].Rows[0]["TrackingURL"].ToString();
                trackingUrl.InnerHtml = "Track Order";
                trackingUrl.Target = "_blank";
            }

            ShippingAmountText = ds.Tables[0].Rows[0]["ShipCost"] + "";
            if (ShippingAmountText != "Free Shipping")
                ShippingAmountText = ds.Tables[1].Rows[0]["SaleCurrency"] + " " + ds.Tables[0].Rows[0]["ShipCost"] + "";

            subtotl =  ds.Tables[1].Rows[0]["SaleCurrency"] + " " + ds.Tables[0].Rows[0]["TotalAmount"] + "";
            rp_order_summary.DataSource = ds.Tables[1];
            rp_order_summary.DataBind();
        }


    }
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                Session["CustID"] = null;
                Response.Redirect("~/order_track.aspx");
            }
        }
        return cipherText;
    }
}