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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
public partial class admin_UserUploadedBookList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        load_get_UserBookList("");
    }

    private void load_get_UserBookList(string UserID)
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_UserBookList(UserID, "", "", "", "", 2, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, ph_Msg);
        }
    }
    protected void btn_search_edit_Click(object sender, EventArgs e)
    {
        load_get_UserBookList("");  
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        load_get_UserBookList("");
    }
}