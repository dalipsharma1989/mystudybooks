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
        CommonCode.SetPageTitle("Categories");
        load_Categories();
    }


    private void load_Categories()
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_get_menu_category_subcat", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = 0;
        com.Parameters.Add("@CategoryID ", SqlDbType.BigInt).Value = 0;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (dt.Rows.Count > 0)
        {
            ltr_breadcrumb.Text = "<ul class='breadcrumb'>";
            ltr_breadcrumb.Text += "<li><a href='index.aspx'>Home</a></li><li class='active'><strong>Categories</strong></li>";
            ltr_breadcrumb.Text += "</ul>";

            Literal1.Text = "<div class='row'>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sub_cat_html = load_SubCategory(dt.Rows[i]["CategoryID"] + "");
                Literal1.Text += " <div class='col-xs-4'>";
                Literal1.Text += "              <h3><a href='search_results.aspx?cat=" + dt.Rows[i]["CategoryDesc"] + "&catid=" + dt.Rows[i]["CategoryID"] + "'>" + dt.Rows[i]["CategoryDesc"] + "</a></h3>";
                Literal1.Text += "                            <div>";
                Literal1.Text += sub_cat_html;
                Literal1.Text += "                            </div>";
                Literal1.Text += "                        </div>";

                if ((i + 1) % 3 == 0)
                {
                    Literal1.Text += "</div>";
                    Literal1.Text += "<div class='row'>";
                }
            }
            Literal1.Text += "<div>";
        }
    }


    private string load_SubCategory(String CategoryID)
    {
        String errmsg = "";
        string html = "";
        SqlCommand com = new SqlCommand("dbo_get_menu_category_subcat", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = 1;
        com.Parameters.Add("@CategoryID ", SqlDbType.BigInt).Value = CategoryID;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                html = "                                <ul class='list-group'>";
                foreach (DataRow rr in dt.Rows)
                {
                    html += "                                  <li class='list-group-item'>";
                    html += "                                      <a class='sub-cat' href='search_results.aspx?subcat=" + rr["SubCategoryDesc"] + "&subcatid=" + rr["SubCategoryID"] + "'>" + rr["SubCategoryDesc"] + "</a>";
                    html += "                                  </li>";
                }
                html += "                                </ul>";
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
        }
        return html;
    }

    
}