using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class SearchBook : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
      //  load_BookCategories(); 
    }

    private void load_BookCategories()
    {
        SqlCommand com = new SqlCommand("web_GetBookCategory", CommonCode.con);
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyID"] + "";
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"] + "";
        com.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {

                drp_Categories.Items.Add(new ListItem("All Categories", "Nil"));
                foreach (DataRow rr in dt.Rows)
                {
                    drp_Categories.Items.Add(new ListItem(rr[1].ToString().ToLower(), rr[0].ToString().ToLower()));
                } 
            }
        }
        else
        {
          //  CommonCode.show_toastr("error", "Error", errmsg, false, "", false, "", ltr_scripts);
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(textSearchBooks.Text))
        //{

        //}
        if (File.Exists(Server.MapPath("search_results.aspx")))
            Response.Redirect("search_results.aspx?q=" + HttpUtility.UrlEncode(textSearchBooks.Text) + "&Cat=" + HttpUtility.UrlEncode(drp_Categories.SelectedValue));
        else
        {
            Response.Redirect("/search_results.aspx?q=" + HttpUtility.UrlEncode(textSearchBooks.Text) + "&Cat=" + HttpUtility.UrlEncode(drp_Categories.SelectedValue));
        }
    }

    protected void textSearchBooks_TextChanged(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(textSearchBooks.Text))
        //{

        //}
        if (File.Exists(Server.MapPath("search_results.aspx")))
        {
            Response.Redirect("search_results.aspx?q=" + HttpUtility.UrlEncode(textSearchBooks.Text) + "&Cat=" + HttpUtility.UrlEncode(drp_Categories.SelectedValue));
        }
        else
        {
            Response.Redirect("/search_results.aspx?q=" + HttpUtility.UrlEncode(textSearchBooks.Text) + "&Cat=" + HttpUtility.UrlEncode(drp_Categories.SelectedValue));
        }
    }

}