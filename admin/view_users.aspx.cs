using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Web.Services;
public partial class _Default : System.Web.UI.Page
{
    string type = "2";
    string schoolid = "-1";

    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_alert_msg.Text = "";
        ltr_scripts.Text = "";
        this.Title = CommonCode.SetPageTitle("Retail Users");
        /*Adeed by Ashok on 18-03-2020*/
        if (Session["LoginSessionID"] == null || Session["LoginSessionID"].ToString() == "")
        {
            Response.Redirect("../admin/");
        }
        if (!IsPostBack)
        {
            h2_title.InnerText = "Retail Users";
            strong_li_title.InnerText = "Retail Users";
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                if(!string.IsNullOrEmpty(Request.QueryString["school"]))
                {
                    if (Request.QueryString["type"] == "students")
                    {
                        //view all students from school
                        h2_title.InnerText = "Students";
                        strong_li_title.InnerText = "Students";
                        type = "1";
                        schoolid = Request.QueryString["school"];
                    }
                }

                if (Request.QueryString["type"] == "schools")
                {
                    //view all schools
                    h2_title.InnerText = "Schools";
                    strong_li_title.InnerText = "Schools";
                    type = "0";
                }
            }
            load_all_users(type, schoolid);
        }
    }

    private void load_all_users(String type, String SchoolID)
    {

        string Searchname = Request.Form[txtCustomer_Name.UniqueID];
        Int32 iAll;
        if (Searchname == null || Searchname == "")
        {
            iAll = 1;
        }
        else
        {
            iAll = 2;
        }

        string iCompanyID = Session["iCompanyId"].ToString();
        string iBranchID = Session["iBranchID"].ToString();
        /*Adeed by Ashok on 18-03-2020*/
        SqlCommand com = new SqlCommand("Web_get_all_users", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = type;
        com.Parameters.Add("@SchoolID", SqlDbType.VarChar, 30).Value = SchoolID;
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
        com.Parameters.Add("@isAll", SqlDbType.Int).Value = iAll;
        com.Parameters.Add("@CustSearch", SqlDbType.VarChar, 8000).Value = Searchname;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        //dt.Columns.Add("Password");
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow rr in dt.Rows)
            {
                //String EncryptedPassword = Encoding.UTF8.GetString((Byte[])rr["UserPassword"]);
                //String UserPassword = cryptography.Decrypt(EncryptedPassword);
                //rr["Password"] = rr["UserPassword"];/// UserPassword;
                /*Adeed by Ashok on 18-03-2020*/
                if (type=="0")
                {
                    rr["CustName"] = "<a href='view_users.aspx?type=students&school=" + rr["CustID"] + "'>" + rr["CustName"] + "</a>";
                }
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
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string type = "2";
        string schoolid = "";
        GridView2.PageIndex = e.NewPageIndex;
        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            if (Request.QueryString["type"] == "schools")
            {
                //view all schools
                type = "0";
            }
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["type"]) && !string.IsNullOrEmpty(Request.QueryString["school"]))
        {
            //view all students from school
            type = "1";
            schoolid = Request.QueryString["school"];
        }
        else
        {
            //view all external user
            type = "2";
        }
        load_all_users(type, schoolid);
    }

    
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.QueryString["type"] == "schools")
            {
                string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[1].Text);
                e.Row.Cells[1].Text = decodedText;
            }
        }
    }

    protected void btn_search_edit_Click(object sender, EventArgs e)
    {
        load_all_users(type, schoolid);
    }


    protected void txtCustomer_Name_TextChanged(object sender, EventArgs e)
    {
        load_all_users(type, schoolid);
    }


    [WebMethod]
    public static string[] GetDataList(string prefix)
    {
        List<string> Products = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText += " select top 20 (CustomerName + ' ' + Mobile + ' ' + EmailID) CustName,CustomerName   from MasterCustomer ";

                cmd.CommandText += " where (CustomerName + ' ' + Mobile + ' ' + EmailID) like '%'+ @SearchText + '%' and iCompanyID = " + HttpContext.Current.Session["iCompanyId"].ToString() + " order by CustName ASC ";

                if (prefix != null)
                {
                    prefix = prefix.Replace("-", "[-]");
                }

                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (!string.IsNullOrEmpty(prefix))
                            Products.Add(string.Format("{0}|{1}", sdr["CustName"], sdr["CustName"]));
                    }
                }
                conn.Close();
            }
        }
        return Products.ToArray();
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}