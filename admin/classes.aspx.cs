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
            load_Classes();
        }
    }

    private void load_Classes()
    {
        String errmsg = "", ClassID = "0";

        DataTable dt = new DataTable();
        DAL dal = new DAL();
        dt = dal.get_Classes(ClassID, out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Classid"]))
                {
                    btnSave.Text = "Update";
                    btnSave.Attributes["class"] = "btn btn-sm btn-primary";
                    ClassID = Request.QueryString["Classid"];
                    DataRow[] rr = dt.Select("ClassID = " + ClassID);
                    if (rr.Length > 0)
                    {
                        textClassName.Text = rr[0]["ClassName"].ToString();
                        hf_ClassID.Value = rr[0]["ClassID"].ToString();
                    }
                    else
                    {
                        CommonCode.show_alert("warning", "Record not found !", "Class you are looking for isn`t available or recently deleted.", false, ltr_msg);
                    }
                }
                grd_Classes.DataSource = dt;
                grd_Classes.DataBind();
            }
            else
            {
                grd_Classes.DataSource = null;
                grd_Classes.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Classes", errmsg, false, ltr_msg);
        }
    }

    protected void grd_Classes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ClassID = e.CommandArgument.ToString();
        if (e.CommandName == "delete_record")
        {
            string errmsg = "", action_msg = "delet";
            DAL dal = new DAL();
            dal.insert_update_delete_Class(ClassID, "", "", 2, false, out errmsg);
            if (errmsg == "success")
            {
                CommonCode.show_alert("success", "Class '<em>" + textClassName.Text + "</em>' " + action_msg + "ed successfully !", "", ltr_msg);
                reset_form();
            }
            else
            {
                CommonCode.show_alert("danger", "Error while " + action_msg + "ing Class.", errmsg, ltr_msg);
            }
        }
    }

    protected void grd_Classes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Classes.PageIndex = e.NewPageIndex;
        load_Classes();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string errmsg = "", action_msg = "sav";

        DAL dal = new DAL();
        if (!string.IsNullOrEmpty(Request.QueryString["Classid"]))
        {
            dal.insert_update_delete_Class(hf_ClassID.Value, "", textClassName.Text, 1, false, out errmsg);
            action_msg = "updat";
        }
        else
        {
            dal.insert_update_delete_Class("0", "", textClassName.Text, 0, false, out errmsg);
            action_msg = "sav";
        }
        if (errmsg == "success")
        {
            CommonCode.show_alert("success", "Class '<em>" + textClassName.Text + "</em>' " + action_msg + "ed successfully !", "", ltr_msg);
            reset_form();
        }
        else
        {
            CommonCode.show_alert("danger", "Error while " + action_msg + "ing Class.", errmsg, ltr_msg);
        }

    }

    private void reset_form()
    {
        btnSave.Text = "Save";
        btnSave.Attributes["class"] = "btn btn-sm btn-success";
        textClassName.Text = "";
        hf_ClassID.Value = "";
        load_Classes();
    }
}