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
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_subjects();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }

    }

    private void load_subjects()
    {
        String errmsg = "", SubjectID = "";

        DataTable dt = new DataTable();
        DAL dal = new DAL();
        dt = dal.get_Subjectsubject(SubjectID, Session["iCompanyId"].ToString(), out errmsg); 

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["subjectid"]))
                {
                    btnSave.Text = "Update";
                    btnSave.Attributes["class"] = "btn btn-sm btn-primary";
                    SubjectID = Request.QueryString["subjectid"];
                    DataRow[] rr = dt.Select("SubjectID = " + SubjectID);
                    if (rr.Length > 0)
                    {
                        textSubjectName.Text = rr[0]["SubjectName"].ToString();
                        hf_SubjectID.Value = rr[0]["SubjectID"].ToString();
                    }
                    else
                    {
                        CommonCode.show_alert("warning", "Record not found !", "Subject you are looking for isn`t available or recently deleted.", false, ltr_msg);
                    }
                }
                grd_subjects.DataSource = dt;
                grd_subjects.DataBind();
            }
            else
            {
                grd_subjects.DataSource = null;
                grd_subjects.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Subjects", errmsg, false, ltr_msg);
        }
    }

    protected void grd_subjects_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string SubjectID = e.CommandArgument.ToString();
        if(e.CommandName== "delete_record")
        {
            string errmsg = "",action_msg= "delet";
            DAL dal = new DAL();
            dal.insert_update_delete_subjects(hf_SubjectID.Value, "", "",2, false, out errmsg);
            if (errmsg == "success")
            {
                CommonCode.show_alert("success", "Subject '<em>" + textSubjectName.Text + "</em>' " + action_msg + "ed successfully !", "", ltr_msg);
                reset_form();
            }
            else
            {
                CommonCode.show_alert("danger", "Error while " + action_msg + "ing subject.", errmsg, ltr_msg);
            }
        }
    }

    protected void grd_subjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_subjects.PageIndex = e.NewPageIndex;
        load_subjects();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string errmsg = "", action_msg = "sav";

        DAL dal = new DAL();
        if (!string.IsNullOrEmpty(Request.QueryString["subjectid"]))
        {
            dal.insert_update_delete_subjects(hf_SubjectID.Value, "", textSubjectName.Text, 1, false, out errmsg);
            action_msg = "updat";
        }
        else
        {
            dal.insert_update_delete_subjects("0", "", textSubjectName.Text, 0, false, out errmsg);
            action_msg = "sav";
        }
        if (errmsg == "success")
        {
            CommonCode.show_alert("success", "Subject '<em>" + textSubjectName.Text + "</em>' " + action_msg + "ed successfully !", "", ltr_msg);
            reset_form();
        }   
        else
        {
            CommonCode.show_alert("danger", "Error while " + action_msg + "ing subject.", errmsg, ltr_msg);
        }
        
    }

    private void reset_form()
    {
        btnSave.Text = "Save";
        btnSave.Attributes["class"] = "btn btn-sm btn-success";
        textSubjectName.Text = "";
        hf_SubjectID.Value = "";
        load_subjects();
    }
}