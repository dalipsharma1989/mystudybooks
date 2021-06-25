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
            load_all_students();
        }
    }

    private void load_all_students()
    {
        SqlCommand com = new SqlCommand("dbo_get_all_students", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
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
        if (e.CommandName == "delete_student")
        {
            string CustID = e.CommandArgument.ToString();
            CommonCode.show_alert("warning", "Delete request sent ", "Record will be deleted after approval", ltr_alert_msg);
            ltr_scripts.Text = "<script>toastr.warning('Record will be deleted after approval', 'Delete request sent !');</script>";
            //deleteProject(ProjectID);
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        load_all_students();
    }
}