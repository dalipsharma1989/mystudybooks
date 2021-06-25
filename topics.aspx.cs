using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["topicid"]))
                load_topic(Request.QueryString["topicid"]);
            else
                Response.Redirect("/", true);
        }
    }

    private void load_topic(string topicID)
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_topics(topicID, 3, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                rp_topic.DataSource = dt;
                rp_topic.DataBind();
            }
            else
                CommonCode.show_alert("warning", "No Topic found !", "May be topics was deleted or not created.", ph_msg);
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Topics", errmsg, ph_msg);
        }
    }
}