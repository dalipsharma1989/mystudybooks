using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_alert_msg.Text = "";
        ltr_scripts.Text = ""; 
        this.Title = CommonCode.SetPageTitle("Order Detail");
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["orderid"]))
                {
                    load_order_summary();
                }
                else
                {
                    //display message
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../admin/");
        }
    }

    private void load_order_summary()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        
        ds = dal.get_Order_details(Request.QueryString["orderid"], "", false, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            if (ds.Tables.Count > 0)
            {
                //h3_orderid.InnerText = "Order ID : " + ds.Tables[0].Rows[0]["OrderID"] + "";
                //span_order_date.InnerText = "Order Date : " + string.Format("{0:dd MMM, yyyy}", ds.Tables[0].Rows[0]["OrderDate"]) + "";
                //h4_status.InnerText = "Status: " + ds.Tables[0].Rows[0]["Status"] + "";
                //td_total_amount.InnerHtml = "<span id='ttl-amt' class='ttl-amt pulsor'>" + ds.Tables[1].Rows[0]["SaleCurrency"] + " " + ds.Tables[0].Rows[0]["TotalAmount"] + "</span>";

                rp_order.DataSource = ds.Tables[0];
                rp_order.DataBind();
                Repeater1.DataSource = ds.Tables[0];
                Repeater1.DataBind();
                Repeater2.DataSource = ds.Tables[0];
                Repeater2.DataBind();
                // lbl_courier.Text = ds.Tables[0].Rows[0]["CourierType"].ToString();
                rp_order_details.DataSource = ds.Tables[1];
                rp_order_details.DataBind();

                rp_order_tot_details.DataSource = ds.Tables[0];
                rp_order_tot_details.DataBind();

                DataTable dt0 = ds.Tables[0];
                DataTable dt1 = ds.Tables[1];

                if (ds.Tables[1].Rows.Count < 1)
                {
                    Control FooterTemplate = rp_order_details.Controls[rp_order_details.Controls.Count - 1].Controls[0];
                    FooterTemplate.FindControl("trEmpty").Visible = true;
                }
                else
                {/*
                    Control FooterTemplate = rp_order_details.Controls[rp_order_details.Controls.Count - 1].Controls[0];
                    FooterTemplate.FindControl("shipping_amount_footer_text").*/
                    /*
                    FooterTemplate.FindControl("shipping-amount-footer-text").
                        DataSource = dt0["ShippingAmountText"];
                    FooterTemplate.FindControl("total-amount-footer-text").DataBind() = dt0["TotalAmount"];*/
                }
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Order Details", errmsg, ph_msg);
        }
    }

}