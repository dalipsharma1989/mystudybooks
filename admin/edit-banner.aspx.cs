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
        this.Title = CommonCode.SetPageTitle("Banner Images");
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["bannerid"]))
                {
                    load_banner_images(Request.QueryString["bannerid"]);
                }
                else
                {
                    Response.Redirect("select_banner.aspx", true);
                }
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }        
    }

    private void get_Image_size(String BannerID, out int Width, out int Height)
    {
        if (BannerID == "1" )
        {
            Width = 1011;
            Height = 1288;
        }
        else if (BannerID == "2")
        {
            Width = 960;
            Height = 736;
        }
        else if (BannerID == "3")
        {
            Width = 1920;
            Height = 142;
        }
        else if (BannerID == "4" ||  BannerID == "5" || BannerID == "6")
        {
            Width = 500;
            Height = 281;
        }
        else
        {
            Width = 1920;
            Height = 736;
        }
    }

    private void save_images(System.Drawing.Image image, String Filename, out string func_errmsg)
    {
        func_errmsg = "";
        try
        {
            SqlCommand com = null;
            string id = "";
            String filepath = "";
            String img_errmsg = "";
            string img = string.Empty;

            int width = 0, height = 0;
            get_Image_size(Request.QueryString["bannerid"], out width, out height);
            System.Drawing.Image resizedImage = ImageFixedResize.FixedSize(image, width, height, out img_errmsg);

            if (img_errmsg != "success")
            {
                CommonCode.show_alert("danger", "Error", img_errmsg, ltr_msg);
                func_errmsg += "<br>" + img_errmsg;
                return;
            }

            filepath = "/resources/banners/" + Filename + ".Jpeg";
            CommonCode.CreateDirectory("banners");
            img = Server.MapPath("/resources/banners/") + Filename + ".Jpeg";
            resizedImage.Save(img, System.Drawing.Imaging.ImageFormat.Jpeg);
            String errmsg = "";
            DAL dal = new DAL();
            dal.Insert_Update_Delete_Banners("0", Request.QueryString["bannerid"],filepath,"",false,0,"", Session["iCompanyId"].ToString(),out errmsg);

            //com = new SqlCommand("dbo_insert_edit_banner_images", CommonCode.con);
            //com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.Add("@ImageID", SqlDbType.BigInt).Value = 0;
            //com.Parameters.Add("@BannerID", SqlDbType.Int).Value = Request.QueryString["bannerid"];
            //com.Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = filepath;
            //com.Parameters.Add("@Active", SqlDbType.Bit).Value = 0;
            //com.Parameters.Add("@Link", SqlDbType.VarChar, 1000).Value = "";
            //com.Parameters.Add("@action", SqlDbType.Int).Value = 0;            
            //errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);

            if (errmsg != "success")
                func_errmsg += errmsg;
            else
                func_errmsg = "success";
        }
        catch (Exception ex)
        {
            func_errmsg = ex.Message;
        }

    }

    private void load_banner_images(String BannerID)
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_BannerImages(Session["iCompanyId"].ToString(), BannerID, out errmsg);
        if (errmsg == "success")
        {
            if(dt.Rows.Count  > 0)
            {
                dv_Upload.Visible = false;
                grd_cats.DataSource = dt;
                grd_cats.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
        }

        //String errmsg = "";
        //SqlCommand com = new SqlCommand("select * from BannerImages where BannerID=@BannerID", CommonCode.con);
        //com.Parameters.Add("@BannerID", SqlDbType.Int).Value = BannerID;
        //DataTable dt = new DataTable();
        //dt = CommonCode.getData(com, out errmsg);
        //if (errmsg == "success")
        //{
        //    grd_cats.DataSource = dt;
        //    grd_cats.DataBind();
        //}
        //else
        //    CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FileUpload1.PostedFiles.Count > 20)
        {
            CommonCode.show_alert("danger", "Error", "Maximum 20 files allowed", ltr_msg);
        }
        else
        {
            DataTable errdt = new DataTable();
            errdt.Columns.AddRange(new DataColumn[] { new DataColumn("FileName"), new DataColumn("Status"), new DataColumn("Msg") });

            foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
            {
                String errmsg = "", Status = "";
                System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
                string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                save_images(image, fileName, out errmsg);
                if (errmsg == "success")
                    Status = "Saved";
                else
                    Status = "Not Saved";
                errdt.Rows.Add(fileName, Status, errmsg);
            }

            if (errdt.Rows.Count > 0)
            {
                grd_errdt.DataSource = errdt;
                grd_errdt.DataBind();
            }

            load_banner_images(Request.QueryString["bannerid"]);
        }
    }

    protected void grd_cats_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grd_cats_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String ImageID = e.CommandArgument.ToString();
        if (e.CommandName == "set_active_banner")
        {
            String errmsg = "";
            SqlCommand com = new SqlCommand("web_set_active_banner_image", CommonCode.con);
            com.Parameters.Add("@ImageID", SqlDbType.BigInt).Value = ImageID;
            com.Parameters.Add("@BannerID", SqlDbType.Int).Value = Request.QueryString["bannerid"];
            com.Parameters.Add("@icompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
            com.CommandType = CommandType.StoredProcedure;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg != "success")
                CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
            else
                Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        else if (e.CommandName == "update_link")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            HiddenField hf_imagepath = (HiddenField)row.FindControl("hf_imagepath");
            HiddenField hf_active = (HiddenField)row.FindControl("hf_active");
            TextBox textLink = (TextBox)row.FindControl("textLink");
            TextBox txtBookCode = (TextBox)row.FindControl("txtBookCode");
            String errmsg = "";
            bool hf_activ = false;
            if (hf_active.Value == "False")
            {
                hf_activ = false;
            }
            else
            {
                hf_activ = true;
            }
            DAL dal = new DAL();
            dal.Insert_Update_Delete_Banners(ImageID, Request.QueryString["bannerid"], hf_imagepath.Value, textLink.Text.Trim(), hf_activ, 1, txtBookCode.Text.Trim(), Session["iCompanyId"].ToString(), out errmsg); 

            if (errmsg != "success")
            {
                CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
            }
            else
            {
                Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            }
                
        }
        else if (e.CommandName == "delete_record")
        {
 
            var values = ImageID.Split(';');
            if (values.Length == 2)
            {
                String CA_ImageID = values[0], CA_ImagepPath = values[1];

                if (File.Exists(Server.MapPath(CA_ImagepPath)))
                    File.Delete(Server.MapPath(CA_ImagepPath));
                String errmsg = ""; 
                DAL dal = new DAL();
                dal.Insert_Update_Delete_Banners(CA_ImageID, Request.QueryString["bannerid"], "", "", false, 2,"", Session["iCompanyId"].ToString(), out errmsg); 

                if (errmsg != "success")
                {
                    CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
                }
                else
                {
                    Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                }                    
            }
        }
    }

    protected void grd_cats_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}