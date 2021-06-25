using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["updated"]))
            {
                CommonCode.show_alert("success", "Password changed successfully", "", ph_msg);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string username = textusername.Text.Trim(), currentpassword = txtcurrentpassword.Text.Trim(), confirmpassword = txtconfirmpassword.Text.Trim(), errmsg = "";

        DAL dal = new DAL();
        dal.update_admin_login(username, currentpassword, confirmpassword, out errmsg);
        if (errmsg == "success")
        {
            Response.Redirect("change_password.aspx?updated=true", true);
        }
        else if (errmsg == "Invalid Current Password")
        {
            CommonCode.show_alert("danger", errmsg, "Please enter valid password", ph_msg);
        }
        else
        {
            CommonCode.show_alert("danger", "Error while udpating password", errmsg, ph_msg);
        }
    }
}