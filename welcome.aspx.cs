using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["custid"]))
            {
                Response.Redirect("index.aspx", true);
            }
            else
            {
                load_user_data(Request.QueryString["custid"]);
                //load_book1();
                //load_book2();
            }
        }
    }

    public String CustName = "", CustType = "";
    private void load_user_data(String CustID)
    {
        SqlCommand com = new SqlCommand("Web_Get_Customer_Data", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = CustID;
        com.Parameters.Add("@CustName", SqlDbType.VarChar, 200).Value = "";
        com.Parameters.Add("@Mobile", SqlDbType.VarChar, 10).Value = "";
        com.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = CustID;
        com.Parameters.Add("@ICompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
        com.Parameters.Add("@iBranchId", SqlDbType.Int).Value = Session["iBranchID"].ToString();
        
            
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                String UserType="";
                CustName = dt.Rows[0]["CustName"].ToString();
                UserType = dt.Rows[0]["UserType"].ToString();

                if(UserType=="SCHOOL")
                {
                    CommonCode.show_alert("success", "<i class='fa fa-smile-o'></i>&nbsp;Thank you for Registering your School !", "We`ll verify your details via call or email. Your School account will get activated after your verification.", ltr_msg);
                }
                else if (UserType == "TEACHER")
                {
                    CommonCode.show_alert("success", "<i class='fa fa-smile-o'></i>&nbsp;Thank you for Registering your Spectra-Teachers account !", "We`ll verify your details via call or email. After your verification you can login and enjoy offers and discounts.", ltr_msg);
                }
                else
                {
                    CommonCode.show_alert("success", "<i class='fa fa-smile-o'></i>&nbsp;Thank you for Registering your account !", "start exploring our products and Enjoy reading.<a href='/'  >click here to explore</a>", ltr_msg);
                }
            }
        }
        else
        {
            CommonCode.show_toastr("error", "Error", errmsg, false, "", false, "", ltr_script);
        }
    }

    private void load_book1()
    {
        SqlCommand com = new SqlCommand("dbo_get_books", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@no", SqlDbType.Int).Value = 8;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        //CommonCode.show_alert("success", "Search results for <em>'" + query + "</em>'", dt.Rows.Count + " book(s) found", false, ltr_msg);
        if (dt.Rows.Count > 0)
        {
            dt = CommonCode.getImagepath(dt);
            rp_books1.DataSource = dt;
            rp_books1.DataBind();
        }
    }

    private void load_book2()
    {
        SqlCommand com = new SqlCommand("dbo_get_books", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@no", SqlDbType.Int).Value = 8;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        //CommonCode.show_alert("success", "Search results for <em>'" + query + "</em>'", dt.Rows.Count + " book(s) found", false, ltr_msg);
        if (dt.Rows.Count > 0)
        {
            dt = CommonCode.getImagepath(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
    }

}