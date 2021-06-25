using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
public partial class CustomControls_footer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       load_parent_topics();
    }

    private void load_parent_topics()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_topics("0", 0, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                rp_parent_topic.DataSource = dt;
                rp_parent_topic.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Topics", errmsg, ph_msg);
        }
    }

    private void load_topics(string ParentID, Repeater rp_child_topics)
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_topics(ParentID, 2, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                rp_child_topics.DataSource = dt;
                rp_child_topics.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Topics", errmsg, ph_msg);
        }
    }

    protected void rp_parent_topic_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string ParentID = (e.Item.FindControl("hf_ParentID") as HiddenField).Value;
            Repeater rp_child_topics = e.Item.FindControl("rp_child_topics") as Repeater;
            load_topics(ParentID, rp_child_topics);
        }
    }
}