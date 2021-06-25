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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Product List");
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
        string Searchname = Request.Form[product_name.UniqueID];
        Int32 iAll;
        if (Searchname == null || Searchname == "")
        {
            iAll = 1;
        }
        else
        {
            iAll = 2;
        }

        SqlCommand com = new SqlCommand("web_get_products", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@isAll", SqlDbType.Int).Value = iAll;
        com.Parameters.Add("@ProductID", SqlDbType.VarChar, 750).Value = Searchname;
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_books.PageIndex = e.NewPageIndex;
        load_all_products();
    }


    [WebMethod]
    public static string[] GetDataList(string prefix)
    {
        List<string> Products = new List<string>();
        //using (SqlConnection conn = new SqlConnection())
        //{
        //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.CommandText += " select distinct Top 99 MP.ISBN +' ' +  MP.BookName, MP.BookName from MasterProduct MP ";

        //        cmd.CommandText += " where   isnull(MP.IsDeActive,0)=0 and 1=1 and MP.ISBN + ' ' + MP.BookName  like '%' + @SearchText + '%' order by MP.BookName ASC ";

        //        if (prefix != null)
        //        {
        //            prefix = prefix.Replace("-", "[-]");
        //        }

        //        cmd.Parameters.AddWithValue("@SearchText", prefix);
        //        cmd.Connection = conn;
        //        conn.Open();
        //        using (SqlDataReader sdr = cmd.ExecuteReader())
        //        {
        //            while (sdr.Read())
        //            {
        //                if (!string.IsNullOrEmpty(prefix))
        //                    Products.Add(string.Format("{0}|{1}", sdr["BookName"], sdr["BookName"]));
        //            }
        //        }
        //        conn.Close();
        //    }
        //}
         

        DAL dal = new DAL();
        String errmsg = "";
        String OtherCountry = "INDIA";
        String iCompanyId = HttpContext.Current.Session["iCompanyId"].ToString();
        String iBranchID = HttpContext.Current.Session["iBranchID"].ToString();
        DataTable dt = new DataTable();

        dt = dal.get_custom_searchdatabyUsers("BOOKNAMECODE", prefix, OtherCountry, iCompanyId, iBranchID,"", out errmsg);

        if (!string.IsNullOrEmpty(prefix))
        {
            foreach (DataRow dr in dt.Rows)
            {
                Products.Add(string.Format("{0}|{1}", dr["BookCode"].ToString(), dr["BookName"].ToString()));
            }

        }




        return Products.ToArray();
    }



    protected void product_name_TextChanged1(object sender, EventArgs e)
    {
        load_all_products();
    }



    protected void btn_newEntry_Click(object sender, EventArgs e)
    {
        Response.Redirect("product_edit.aspx?editid=NEW",true);
    }
}