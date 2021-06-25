using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class admin_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ltr_msg.Text = "";
        //if (!string.IsNullOrEmpty(Request.QueryString["session_expired"]))
        //{
        //    CommonCode.show_alert("info", "Session Expired!", "It seems like your session has been expired due to security reasons. Please try login again.", ltr_msg);
        //}
        textUserName.Focus();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //string username = textUserName.Text.Trim(), password = textPassword.Text.Trim(), errmsg = "";

        //DAL dal = new DAL();
        //DataTable dt = new DataTable();
        //dt = dal.admin_login(username, out errmsg);
        //if (errmsg == "success")
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (password == dt.Rows[0]["password"].ToString())
        //        {
        //            Session["AdminUserName"] = username;
        //            Response.Redirect("adminhome.aspx", true);
        //        }
        //        else
        //        {
        //            CommonCode.show_alert("warning", "Incorrect Password", "Please provide a valid password", ltr_msg);
        //        }
        //    }
        //    else
        //        CommonCode.show_alert("warning", "Incorrect Username", "Please provide a valid Username", ltr_msg);
        //}
        //else
        //    CommonCode.show_alert("danger", "Error while loading admin details", errmsg, ltr_msg);

        Session["AdminUserName"] = ""; string errmsg = "";
        Session["LoginSessionID"] = "";
        try
        {

            DAL dl = new DAL();
            DataTable dt = new DataTable();
            string UserName = textUserName.Text;
            string UserPassword = textPassword.Text;

            dt = dl.Validate_Login(UserName, UserPassword, out errmsg);
            if (errmsg == "success")
            { 
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Userid"].ToString().ToLower() == UserName.ToString().ToLower() && dt.Rows[0]["UserPassword"].ToString().ToLower() == UserPassword.ToString().ToLower())
                    {
                        SetCompanyID_BranchIdInSession();
                        Session["AdminUserName"] = UserName;
                        Session["LoginSessionID"] = UserName;
                        Response.Redirect("adminhome.aspx",false);
                    }
                    else
                    {
                        CommonCode.show_alert("danger", "<i class='fa fa-frown-o'></i> Invalid Request", "Please Provide a Valid UserID and Password", ltr_msg);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string status = ex.Message;

            if (status == "UserNotExits")
            {
                CommonCode.show_alert("danger", "<i class='fa fa-frown-o'></i> Incorrect Username", "Please provide a valid Username", ltr_msg);
            }
            else if (status == "wrongpassword")
            {
                CommonCode.show_alert("danger", "<i class='fa fa-frown-o'></i> Incorrect Password", "Please provide a valid password", ltr_msg);
            }
            else
            {
                CommonCode.show_alert("danger", "<i class='fa fa-frown-o'></i> Invalid Request", "Please Provide a Valid UserID and Password", ltr_msg);
            }
        }

    }


    private void SetCompanyID_BranchIdInSession()
    {
        DataTable dt = new DataTable();
        DAL dl = new DAL();
        string errmsg = "";
        dt = dl.ICompanyID_BranchID(out errmsg);
        if (dt.Rows.Count > 0)
        {
            Session["iCompanyId"] = dt.Rows[0]["iCompanyId"].ToString();
            Session["iBranchID"] = dt.Rows[0]["iBranchID"].ToString();
            Session["FinancialPeriod"] = dt.Rows[0]["FinancialPeriod"].ToString();
        }
    }


}