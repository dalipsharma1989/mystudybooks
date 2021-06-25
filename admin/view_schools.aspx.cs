using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_alert_msg.Text = "";
        ltr_scripts.Text = "";
        if (!IsPostBack)
        {
            load_school();
        }
    }

    private void load_school()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            if (Request.QueryString["action"] == "pending")
            {
                load_all_schools(true);
                h2_title.InnerText = "Pending Schools";
            }
            else
            {
                h2_title.InnerText = "Approved Schools";
                load_all_schools(false);
            }
        }
        else
        {
            h2_title.InnerText = "Approved Schools";
            load_all_schools(false);
        }
    }


    private void load_all_schools(bool isPending)
    {
        SqlCommand com = new SqlCommand("dbo_get_all_schools", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@isPending", SqlDbType.Bit).Value = isPending;
        SqlDataAdapter ad = new SqlDataAdapter(com);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        dt.Columns.Add("Password");

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow rr in dt.Rows)
            {
                String EncryptedPassword = Encoding.UTF8.GetString((Byte[])rr["UserPassword"]);
                String UserPassword = cryptography.Decrypt(EncryptedPassword);
                rr["Password"] = UserPassword;
            }
        }
        GridView2.DataSource = dt;
        GridView2.DataBind();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete_school")
        {
            string CustID = e.CommandArgument.ToString();
            CommonCode.show_alert("warning", "Delete request sent ", "Record will be deleted after approval", ltr_alert_msg);
            ltr_scripts.Text = "<script>toastr.warning('Record will be deleted after approval', 'Delete request sent !');</script>";
            //deleteProject(ProjectID);
        }
        else if (e.CommandName == "approve_school")
        {
            var values = e.CommandArgument.ToString().Split(';');
            if (values.Length == 2)
            {
                bool isApproved = Convert.ToBoolean(values[1]);
                string msg = "";
                if (isApproved)
                    msg = "School Un-Apporved Successfully !";
                else
                    msg = "School Apporved Successfully !";
                SqlCommand com = new SqlCommand("dbo_approve_school_or_teacher", CommonCode.con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = values[0];
                com.Parameters.Add("@isApproved", SqlDbType.Bit).Value = !isApproved;
                string errmsg;
                errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
                if (errmsg == "success")
                {
                    load_school();
                    CommonCode.show_toastr("success", "Action Completed", msg, false, "", false, "", ltr_scripts);
                }
                else
                {
                    CommonCode.show_alert("danger", "Error ", errmsg, ltr_alert_msg);
                }
            }
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        load_school();
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[1].Text);
            //e.Row.Cells[1].Text = decodedText; // Don`t know why I used this 

        }
    }
}