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
        this.Title = CommonCode.SetPageTitle("Order");
        try
        { 
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, DateTime.Today.Day);
                var first = month.AddMonths(-1);
                textCreatedDate.Text = string.Format("{0:yyyy/MM/dd}", first);
                textToDate.Text = string.Format("{0:yyyy/MM/dd}", DateTime.Now);

                search_order("", "", "", "", "", 0, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), textToDate.Text);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../admin/");
        }
    }

    protected void btn_search_order_Click(object sender, EventArgs e)
    { 
        search_order(textOrderID.Text, "", textCustomerName.Text, textCreatedDate.Text, textAmount.Text, 1, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), textToDate.Text);
    }

    private void search_order(String OrderID, String Status, string CustomerName, String DateCreated, String Amount, int isSearch, string iCompanyID, string iBranchID, String ToDate)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_dbo_get_search_orders", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = (OrderID == "" ? (object)DBNull.Value : OrderID);
            com.Parameters.Add("@Status", SqlDbType.Bit).Value = (Status == "" ? (object)DBNull.Value : Status);
            com.Parameters.Add("@CustomerName", SqlDbType.VarChar, 200).Value = (CustomerName == "" ? (object)DBNull.Value : CustomerName);
            com.Parameters.Add("@DateCreated", SqlDbType.SmallDateTime).Value = (DateCreated == "" ? (object)DBNull.Value : DateCreated);
            com.Parameters.Add("@Amount", SqlDbType.Float).Value = (Amount == "" ? (object)DBNull.Value : Amount);
            com.Parameters.Add("@isSearch", SqlDbType.Int).Value = isSearch;
            com.Parameters.Add("@ToDate", SqlDbType.SmallDateTime).Value = (ToDate == "" ? (object)DBNull.Value : ToDate);
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            SqlDataAdapter ad = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            if (dt != null)
            {
                rp_orders.DataSource = dt;
                rp_orders.DataBind();
                if (dt.Rows.Count < 1)
                {
                    Control FooterTemplate = rp_orders.Controls[rp_orders.Controls.Count - 1].Controls[0];
                    FooterTemplate.FindControl("trEmpty").Visible = true;
                }
            }

            //if (dt.Rows.Count > 0)
            //{                 
            //    rp_orders.DataSource = dt;
            //    rp_orders.DataBind();
            //    if (dt.Rows.Count < 1)
            //    {
            //        Control FooterTemplate = rp_orders.Controls[rp_orders.Controls.Count - 1].Controls[0];
            //        FooterTemplate.FindControl("trEmpty").Visible = true;
            //    }
            //}  
        }
        catch (Exception ex)
        {

        }
    }
}