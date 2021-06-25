using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            load_Categories();

            if (!string.IsNullOrEmpty(Request.QueryString["catid"]))
            {
                load_SubCategory(Request.QueryString["catid"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["subcatid"]) && !string.IsNullOrEmpty(Request.QueryString["subcat"]))
            {
                textSubCategories.Visible = false;
                div_form_grp_subcategory.Visible = false;
                textCategoryName.ReadOnly = true;
                div_udpate_sub_cate.Visible = true;
                text_subcat_to_update.Text = Request.QueryString["subcat"];
            }
        }
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
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                ol_breadcrumb.InnerHtml = "<li><a href='adminhome.aspx'>Home</a></li><li class='active'><strong>Categories</strong></li>";
                grd_cats.DataSource = dt;
                grd_cats.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Categories", errmsg, ltr_msg);
        }
    }


    private void load_SubCategory(String CategoryID)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_get_menu_category_subcat", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = 1;
        com.Parameters.Add("@CategoryID ", SqlDbType.BigInt).Value = CategoryID;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (dt.Rows.Count > 0)
        {
            ol_breadcrumb.InnerHtml = "<li><a href='adminhome.aspx'>Home</a></li>" +
                                      "<li ><a href='categories.aspx'>Categories</a></li>" +
                                      "<li class='active'><strong>" + dt.Rows[0]["CategoryDesc"] + "</strong></li>";
            textSubCategories.Attributes["placeholder"] = "Add new Sub Categories here (One Per line)";
            textSubCategories.Visible = true;
            div_form_grp_subcategory.Visible = true;
            div_categories.Visible = false;
            btnSave.Visible = false;
            btn_update.Visible = true;
            grd_cats.Visible = false;
            textCategoryName.Text = dt.Rows[0]["CategoryDesc"] + "";
            grd_sub_categories.DataSource = dt;
            grd_sub_categories.DataBind();
        }
        else
        {

        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_insert_edit_dele_categories", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CategoryID", SqlDbType.BigInt).Value = 0;
        com.Parameters.Add("@CategoryDesc", SqlDbType.NVarChar).Value = textCategoryName.Text;
        com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
        com.Parameters.Add("@action", SqlDbType.Int).Value = 0;

        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        if (errmsg == "success")
        {
            CommonCode.show_alert("success", "Record Saved", "", ltr_msg);
            if (!string.IsNullOrEmpty(textSubCategories.Text))
            {
                String[] SubCats = textSubCategories.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (SubCats.Length > 0)
                {
                    saveSubCats(SubCats, textCategoryName.Text);
                }
            }

            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        else
        {
            CommonCode.show_alert("danger", "Error while saving new record !", errmsg, ltr_msg);
        }
    }

    private void saveSubCats(String[] SubCats, String CategoryDesc)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("select CategoryID from MasterProductCategory where CategoryDesc=@CategoryDesc ", CommonCode.con);
        com.Parameters.Add("@CategoryDesc", SqlDbType.NVarChar).Value = CategoryDesc;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (dt.Rows.Count > 0)
        {
            String CategoryID = dt.Rows[0]["CategoryID"].ToString();
            foreach (String subCat in SubCats)
            {
                SqlCommand com2 = new SqlCommand("dbo_insert_edit_dele_sub_categories", CommonCode.con);
                com2.CommandType = CommandType.StoredProcedure;
                com2.Parameters.Add("@CategoryID", SqlDbType.BigInt).Value = CategoryID;
                com2.Parameters.Add("@SubCategoryID", SqlDbType.BigInt).Value = 0;
                com2.Parameters.Add("@SubCategoryDesc", SqlDbType.NVarChar).Value = subCat;
                com2.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
                com2.Parameters.Add("@action", SqlDbType.Int).Value = 0;
                errmsg = CommonCode.ExecuteNoQuery(com2, CommonCode.con);
            }
        }
    }


    protected void grd_cats_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String CategoryID = e.CommandArgument.ToString();
        if (e.CommandName == "delete_category")
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_categories", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CategoryID", SqlDbType.BigInt).Value = CategoryID;
            com.Parameters.Add("@CategoryDesc", SqlDbType.NVarChar).Value = "";
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 2;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                Page.Response.Redirect("categories.aspx", true);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["subcatid"]) && !string.IsNullOrEmpty(Request.QueryString["subcat"]))
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_sub_categories", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CategoryID", SqlDbType.BigInt).Value = Request.QueryString["catid"];
            com.Parameters.Add("@SubCategoryID", SqlDbType.BigInt).Value = Request.QueryString["subcatid"];
            com.Parameters.Add("@SubCategoryDesc", SqlDbType.NVarChar).Value = text_subcat_to_update.Text;
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 1;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                load_SubCategory(Request.QueryString["catid"]);
                CommonCode.show_alert("success", "Sub Category Updated !", "", false, ltr_msg);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }
        else
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_categories", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CategoryID", SqlDbType.BigInt).Value = Request.QueryString["catid"];
            com.Parameters.Add("@CategoryDesc", SqlDbType.NVarChar).Value = textCategoryName.Text;
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 1;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                if (!string.IsNullOrEmpty(textSubCategories.Text))
                {
                    String[] SubCats = textSubCategories.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                    if (SubCats.Length > 0)
                    {
                        saveSubCats(SubCats, textCategoryName.Text);
                    }
                }

                load_SubCategory(Request.QueryString["catid"]);
                CommonCode.show_alert("success", "Category Updated !", "", false, ltr_msg);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }
    }

    protected void grd_sub_categories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String SubCategoryID = e.CommandArgument.ToString();
        if (e.CommandName == "delete_subcategory")
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_sub_categories", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CategoryID", SqlDbType.BigInt).Value = 0;
            com.Parameters.Add("@SubCategoryID", SqlDbType.BigInt).Value = SubCategoryID;
            com.Parameters.Add("@SubCategoryDesc", SqlDbType.NVarChar).Value = "";
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 2;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                Page.Response.Redirect("categories.aspx?catid=" + Request.QueryString["catid"], true);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }
    }
}