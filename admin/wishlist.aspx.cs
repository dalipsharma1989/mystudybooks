using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_alert_msg.Text = "";
        ltr_scripts.Text = "";
        this.Title = CommonCode.SetPageTitle("WishLists");
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                search_Wishlist("", "", "", "", "", false);
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }
    }

    protected void btn_search_order_Click(object sender, EventArgs e)
    {

        search_Wishlist(textOrderID.Text, "false", textCustomerName.Text, textCreatedDate.Text, textAmount.Text, true);
    }

    private void search_Wishlist(String OrderID, String Status,  string CustomerName, String DateCreated,  String Amount, bool isSearch)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_get_search_Whishlist", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = (OrderID == "" ? (object)DBNull.Value : OrderID);
            com.Parameters.Add("@Status", SqlDbType.Bit).Value = (Status == "" ? (object)DBNull.Value : Status);
            com.Parameters.Add("@CustomerName", SqlDbType.VarChar, 200).Value = (CustomerName == "" ? (object)DBNull.Value : CustomerName);
            com.Parameters.Add("@DateCreated", SqlDbType.SmallDateTime).Value = (DateCreated == "" ? (object)DBNull.Value : DateCreated);
            com.Parameters.Add("@Amount", SqlDbType.Float).Value = (Amount == "" ? (object)DBNull.Value : Amount);
            com.Parameters.Add("@isSearch", SqlDbType.Bit).Value = isSearch;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyID"].ToString();
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
            SqlDataAdapter ad = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            rp_orders.DataSource = dt;
            rp_orders.DataBind();
            if (dt.Rows.Count < 1)
            {
                Control FooterTemplate = rp_orders.Controls[rp_orders.Controls.Count - 1].Controls[0];
                FooterTemplate.FindControl("trEmpty").Visible = true;
            }

        }
        catch (Exception ex)
        {

        }
    }
}