using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
public partial class admin_SpecialPriceList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Special Rate List");
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_all_products();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }
    }

    private void load_all_products()
    { 
        SqlCommand com = new SqlCommand("web_get_product_Special_Sale", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure; 
        com.Parameters.Add("@ProductID", SqlDbType.VarChar, 750).Value = "";
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
        SqlDataAdapter ad = new SqlDataAdapter(com);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grd_books.DataSource = dt;
            grd_books.DataBind();
        }
    }
      
    protected void grd_books_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_books.PageIndex = e.NewPageIndex;
        load_all_products();
    }

    protected void grd_books_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String BookCode = e.CommandArgument.ToString();
        string errormsg = "";
        if (e.CommandName.ToString() == "Delete_bookCode")
        {
            load_product_from_Special(BookCode, out errormsg);
            if(errormsg == "success")
            {
                load_all_products();
                CommonCode.show_alert("success", "Book successfully removed from Offer Table", "ISBN - " + BookCode , ltr_scripts);
            }
        }
    }

    private string load_product_from_Special(string BookCode , out string errormsg)
    {
        if (BookCode != "")
        {
            SqlCommand com = new SqlCommand("web_Delete_product_Special_Item", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ProductID", SqlDbType.VarChar, 20).Value = BookCode;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
            errormsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        else
        {
            errormsg = "ISBN not available in this row";
        } 
        return errormsg;
    }

}