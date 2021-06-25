using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 

        try
        {
            this.Title = CommonCode.SetPageTitle("Notify Me Users");
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_notifyme_users();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }

    }


    private void load_notifyme_users(string EmailID="", string Phone = "", string ProductID = "", string iCompanyID = "")
    {
        String errmsg = "";
        DAL dal = new DAL();

        DataTable dt = new DataTable();
        dt = dal.get_NotifyMe_Users(EmailID,Phone,ProductID,Session["iCompanyID"].ToString(),out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                grd_notify_me_users.DataSource = dt;
                grd_notify_me_users.DataBind();
            }
            else
            {
                grd_notify_me_users.DataSource = null;
                grd_notify_me_users.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Notify me Details ", errmsg, ph_msg);
        }
    }


    protected void grd_notify_me_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_notify_me_users.PageIndex = e.NewPageIndex;
        load_notifyme_users();
    }

    protected void grd_notify_me_users_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete_record")
        {
            string RowID = e.CommandArgument.ToString();
            string errmsg = "";
            DAL dal = new DAL();
            dal.insert_update_delete_NotifyMe_Users(RowID,"",  "","", "", 2, Session["iCompanyID"].ToString(), out errmsg);

            if (errmsg == "success")
            {
                Response.Redirect("notifyme_users.aspx", true);
            }
            else
            {
                CommonCode.show_alert("danger", "Error while deleting user", errmsg, ph_msg);
            }

        }
    }
}