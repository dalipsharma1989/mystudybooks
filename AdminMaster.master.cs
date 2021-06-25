using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserName"] == null)
        {
            Response.Redirect("login.aspx?session_expired=true", true);
        }
    }

    protected void lbl_logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("login.aspx");
    }

    protected void lb_site_offline_yes_Click(object sender, EventArgs e)
    {
        try
        {
            // 1.Prepare the name for renaming
            string newName = "app_offline.htm";

            //3.Copy the file with the new name
            File.Copy(Server.MapPath("~") + @"\app_offline_custom.htm", Server.MapPath("~") + @"\" + newName);

            Response.Redirect("\app_offline.htm", true);
        }
        catch (Exception)
        {
            CommonCode.show_alert("danger", "Error while switching site availability. Please contact to administrator", "Details - app_offline_custom.htm file not found.", ph_msg);
        }
    }
}
