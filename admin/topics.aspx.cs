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
        this.Title = CommonCode.SetPageTitle("Footer Topics"); 
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_parent_topics();
                load_topics();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }
    }


    private void load_parent_topics()
    {
        string errmsg = "", ParentTopicID = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_topics("0", 0, out errmsg);
        if (errmsg == "success")
        {
            dd_ChildOf.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                dd_ChildOf.Attributes["style"] = "background: #FFFFFF;";
                dd_ChildOf.Items.Add(new ListItem("Select Parent Topic", "Nil"));
                foreach (DataRow rr in dt.Rows)
                    dd_ChildOf.Items.Add(new ListItem(rr["Name"] + "", rr["TopicID"] + ""));

                if (!string.IsNullOrEmpty(Request.QueryString["parentopicid"]))
                {
                    cb_isParent.Checked = true;
                    div_not_parent.Visible = !cb_isParent.Checked;
                    cb_isParent.Enabled = false;

                    btn_save_edit.Text = "Update";
                    btn_save_edit.Attributes["class"] = "btn btn-sm btn-primary";
                    ParentTopicID = Request.QueryString["parentopicid"];
                    DataRow[] rr = dt.Select("TopicID = " + ParentTopicID);
                    if (rr.Length > 0)
                    {
                        hf_TopicID.Value = rr[0]["TopicID"].ToString();
                        textName.Text = rr[0]["Name"].ToString();
                    }
                    else
                    {
                        CommonCode.show_alert("warning", "Record not found !", "Parent Topic you are looking for isn`t available or recently deleted.", false, ph_msg);
                    }
                }
            }
            else
            {
                dd_ChildOf.Items.Add(new ListItem("No Parent Topic found", "Nil"));
                dd_ChildOf.Attributes["style"] = "background: #FFCDD2;";
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Topics", errmsg, ph_msg);
        }
    }

    private void load_topics()
    {
        string errmsg = "", TopicID = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_topics("0", 1, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["topicid"]))
                { 
                    cb_isParent.Enabled = false;
                    btn_save_edit.Text = "Update";
                    btn_save_edit.Attributes["class"] = "btn btn-sm btn-primary";
                    TopicID = Request.QueryString["topicid"];
                    DataRow[] rr = dt.Select("TopicID = " + TopicID);
                    if (rr.Length > 0)
                    {
                        hf_TopicID.Value = rr[0]["TopicID"].ToString();
                        textName.Text = rr[0]["Name"].ToString();
                        dd_ChildOf.SelectedIndex = dd_ChildOf.Items.IndexOf(dd_ChildOf.Items.FindByValue(rr[0]["ParentTopicID"].ToString()));
                        textDescription.Text = rr[0]["TopicContent"].ToString().Replace("<br>", Environment.NewLine);
                    }
                    else
                    {
                        CommonCode.show_alert("warning", "Record not found !", "Topic you are looking for isn`t available or recently deleted.", false, ph_msg);
                    }
                }

                grd_topics.DataSource = dt;
                grd_topics.DataBind();
                CommonCode.show_alert("success", dt.Rows.Count + " topic(s) found!", "", ph_msg);
            }
            else
            {
                grd_topics.DataSource = null;
                grd_topics.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Topics", errmsg, ph_msg);
        }
    }


    protected void btn_save_edit_Click(object sender, EventArgs e)
    {
        string TopicID = "", Name = "", TopicContent = "", ChildOf = "", errmsg = "", action_msg = "sav";
        bool isParent = false;
        int action = 0;

        Name = textName.Text;
        TopicContent = textDescription.Text.Replace(Environment.NewLine, "<br>");
        isParent = cb_isParent.Checked;
        ChildOf = dd_ChildOf.SelectedValue;

        if (isParent)
        {
            // INSERT PARENT TOPIC
            ChildOf = "0";
            DAL dal = new DAL();

            if (!string.IsNullOrEmpty(Request.QueryString["parentopicid"]))
            {
                dal.insert_update_dele_topics(hf_TopicID.Value, Name, TopicContent, isParent, ChildOf, 1, out errmsg);
                action_msg = "updat";
            }
            else
            {
                dal.insert_update_dele_topics("0", Name, TopicContent, isParent, ChildOf, 0, out errmsg);
                action_msg = "sav";
            }
            if (errmsg == "success")
                Response.Redirect("topics.aspx", true);
            else
                CommonCode.show_alert("danger", "Error while " + action_msg + "ing Parent Topic", errmsg, ph_msg);
        }
        else
        {
            //CHILD TOPIC
            DAL dal = new DAL();
            if (!string.IsNullOrEmpty(Request.QueryString["topicid"]))
            {
                dal.insert_update_dele_topics(hf_TopicID.Value, Name, TopicContent, isParent, ChildOf, 1, out errmsg);
                action_msg = "updat";
            }
            else
            {
                dal.insert_update_dele_topics("0", Name, TopicContent, isParent, ChildOf, 0, out errmsg);
                action_msg = "sav";
            }

            if (errmsg == "success")
                Response.Redirect("topics.aspx", true);
            else
                CommonCode.show_alert("danger", "Error while " + action_msg + "ing Child Topic", errmsg, ph_msg);
        }
    }

    protected void cb_isParent_CheckedChanged(object sender, EventArgs e)
    {
        div_not_parent.Visible = !cb_isParent.Checked;
    }

   

    protected void lbdelete_Click(object sender, EventArgs e)
    {
        //GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        //Label lblID = (Label)clickedRow.FindControl("hf_RowTopicID");       
    }

    protected void grd_topics_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string errmsg = "" , strtopicID = "";
        DAL dal = new DAL();
        bool isParent = false;

        if (e.CommandName == "delete_topic")
        {
            strtopicID = e.CommandArgument.ToString();

            dal.insert_update_dele_topics(strtopicID, "", "", isParent, "0", 2, out errmsg);

            if (errmsg == "success")
                Response.Redirect("topics.aspx", true);
            else
                CommonCode.show_alert("danger", "Error while  deleting Topic", errmsg, ph_msg);

        }
    }
}