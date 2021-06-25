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
    public static string mode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["authors"]))
            {
                mode = "Authors";
                load_authors();
            }
            else
            {
                mode = "Publishers";
                load_Publishers();
            }
            this.Title = CommonCode.SetPageTitle("Browse by " + mode + "");
        }
    }

    private void load_Publishers()
    {
        try
        {
            String errmsg = "";
            SqlCommand com = new SqlCommand("select distinct Publisher from masterproduct where not coalesce(publisher,'')='' or publisher <> '' ", CommonCode.con);
            DataTable dt = new DataTable();
            dt = CommonCode.getData(com, out errmsg);
            if (errmsg == "success")
            {
                if (dt.Rows.Count > 0)
                {
                    dl_Publishers.DataSource = dt;
                    dl_Publishers.DataBind();
                    CommonCode.show_alert("info", dt.Rows.Count + " Publisher found", "", ltr_errmsg);
                }
                else
                {
                    CommonCode.show_alert("warning", "No Publisher found", "No publisher found in database. It`ll be soon updated ! :)", ltr_errmsg);
                }
            }
            else
            {
                CommonCode.show_alert("danger", "Error while loading Publishers", errmsg, ltr_errmsg);
            }
        }
        catch (Exception ex)
        {

            CommonCode.show_alert("danger", "Error while loading Publishers - Main Catch block", ex.Message, ltr_errmsg);
        }

    }


    private void load_authors()
    {
        try
        {
            String errmsg = "";
            SqlCommand com = new SqlCommand("select distinct Author from masterproduct where not coalesce(Author,'')='' or Author <> '' ", CommonCode.con);
            DataTable dt = new DataTable();
            dt = CommonCode.getData(com, out errmsg);
            if (errmsg == "success")
            {
                if (dt.Rows.Count > 0)
                {
                    dl_authors.DataSource = dt;
                    dl_authors.DataBind();
                    CommonCode.show_alert("info", dt.Rows.Count + " Author found", "", ltr_errmsg);
                }
                else
                {
                    CommonCode.show_alert("warning", "No Author found", "No Author found in database. It`ll be soon updated ! :)", ltr_errmsg);
                }
            }
            else
            {
                CommonCode.show_alert("danger", "Error while loading Author", errmsg, ltr_errmsg);
            }
        }
        catch (Exception ex)
        {

            CommonCode.show_alert("danger", "Error while loading Authors - Main Catch block", ex.Message, ltr_errmsg);
        }

        //
    }

}