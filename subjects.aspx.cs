using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            load_Subjects();
            this.Title = CommonCode.SetPageTitle("Browse by Subjects");
        }
    }

    private void load_Subjects()
    {
        String errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_subjects("0", 0, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                dl_subjects.DataSource = dt;
                dl_subjects.DataBind();
            }
            else
                CommonCode.show_alert("info", "No subjects found!", "", ltr_errmsg);
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Subjects", errmsg, ltr_errmsg);
        }
    }

}