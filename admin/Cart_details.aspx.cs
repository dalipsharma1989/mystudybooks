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
        this.Title = CommonCode.SetPageTitle("cart Detail");
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
            string erro = ex.Message;

            Response.Redirect("../admin/");
        } 
    }

    private void load_order_summary()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        ds = dal.get_Cart_details(Request.QueryString["orderid"], "0", false, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            if (ds.Tables.Count > 0)
            { 
                rp_order.DataSource = ds.Tables[0];
                rp_order.DataBind();

                rp_order_details.DataSource = ds.Tables[1];
                rp_order_details.DataBind();

                if (ds.Tables[1].Rows.Count < 1)
                {
                    Control FooterTemplate = rp_order_details.Controls[rp_order_details.Controls.Count - 1].Controls[0];
                    FooterTemplate.FindControl("trEmpty").Visible = true;
                }
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Order Details", errmsg, ph_msg);
        }
    }

}