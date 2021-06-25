using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void load_offline_data()
    {
        if (File.Exists(Server.MapPath("/app_on.htm")))
        {

        }
        else if (File.Exists(Server.MapPath("/App_Offline.htm")))
        {

        }

        //string app_offline = Server.MapPath("/app_on.htm");
        //File.Copy(oldFileName, NewFileName);
        //File.Delete(oldFileName);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}