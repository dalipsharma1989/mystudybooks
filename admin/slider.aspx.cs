using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Big Slider");
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_Slider_images();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }        
    }


    private void save_images(System.Drawing.Image image, String Filename,string BackGroundImage, out string func_errmsg)
    {
        func_errmsg = "";
        try
        {
            SqlCommand com = null;
            string id = "";
            String filepath = "";
            String img_errmsg = "";
            string img = string.Empty;

            int width = 1440, height = 480;
            //get_Image_size(Request.QueryString["Sliderid"], out width, out height);
            System.Drawing.Image resizedImage = ImageFixedResize.FixedSize(image, width, height, out img_errmsg);

            if (img_errmsg != "success")
            {
                CommonCode.show_alert("danger", "Error", img_errmsg, ltr_msg);
                func_errmsg += "<br>" + img_errmsg;
                return;
            }

            filepath = "/resources/sliders/" + Filename + ".Jpeg";
            CommonCode.CreateDirectory("sliders");
            img = Server.MapPath("/resources/sliders/") + Filename + ".Jpeg";
            resizedImage.Save(img, System.Drawing.Imaging.ImageFormat.Jpeg); 

            //var quantizer = new WuQuantizer();
            //using (var quantized = quantizer.QuantizeImage(bmp1))
            //{
            //    quantized.Save(targetPath, ImageFormat.Png);
            //}

            DAL dal = new DAL();
            String errmsg = "";
            dal.Insert_Update_Delete_Sliders("0", filepath, "", 0, BackGroundImage, "", Session["iCompanyId"].ToString(), out errmsg);
            //com = new SqlCommand("web_insert_edit_delete_sliders", CommonCode.con);
            //com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.Add("@SliderID", SqlDbType.BigInt).Value = 0;
            //com.Parameters.Add("@SliderPath", SqlDbType.NVarChar).Value = filepath;
            //com.Parameters.Add("@URL", SqlDbType.NVarChar).Value = "";
            //com.Parameters.Add("@Action", SqlDbType.Int).Value = 0;
            //com.Parameters.Add("@URL", SqlDbType.VarChar,20).Value = "";
            //com.Parameters.Add("@BackImagePath", SqlDbType.VarChar, 8000).Value = BackGroundImage;            
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

    //public static Image ScaleImage(Image image, int height)
    //{
    //    double ratio = (double)height / image.Height;
    //    int newWidth = (int)(image.Width * ratio);
    //    int newHeight = (int)(image.Height * ratio);
    //    System.Drawing.Bitmap newImage = new System.Drawing.Bitmap(newWidth, newHeight);
    //    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newImage))
    //    {
    //        g.DrawImage(image, 0, 0, newWidth, newHeight);
    //    }
    //    image.Dispose();
    //    return newImage;
    //}

    public  string save_BackgroundImages(out string func_errmsg) 
    {
        func_errmsg = "";
        string backgroundimagePath = "" ;
        try
        {   
            String filepath = "";
            String img_errmsg = "", fileName = "";
            string img = string.Empty;
            int width = 1920, height = 736;
            if (FileUpload2.PostedFiles.Count > 1)
            {
                CommonCode.show_alert("danger", "Error", "one file allowed one time.", ltr_msg);
            }
            else
            {
                foreach (HttpPostedFile postedFile in FileUpload2.PostedFiles)
                {                     
                    System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
                    fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    System.Drawing.Image resizedImage = ImageFixedResize.FixedSize(image, width, height, out img_errmsg);

                    filepath = "/resources/BackGroundImages/" + fileName + ".jpg";
                    CommonCode.CreateDirectory("BackGroundImages");
                    img = Server.MapPath("/resources/BackGroundImages/") + fileName + ".jpg";
                    resizedImage.Save(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    backgroundimagePath = filepath;
                    if (img_errmsg != "success")
                    {
                        CommonCode.show_alert("danger", "Error", img_errmsg, ltr_msg);
                        func_errmsg += "<br>" + img_errmsg;
                        return func_errmsg;
                    }
                     
                }

            }
            return backgroundimagePath;
        }
        catch (Exception ex)
        {
            func_errmsg = ex.Message;
            backgroundimagePath = "";
        }
        return backgroundimagePath;
    }


    private void load_Slider_images()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_Sliders(Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if(dt.Rows.Count > 0)
            {
                grd_slider.DataSource = dt;
                grd_slider.DataBind();
            }
            else
            {
                grd_slider.DataSource = null;
                grd_slider.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FileUpload1.PostedFiles.Count > 20)
        {
            CommonCode.show_alert("danger", "Error", "one file allowed one time.", ltr_msg);
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
                String BackGroundImage = save_BackgroundImages(out errmsg); 

                save_images(image, fileName, BackGroundImage, out errmsg);
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

            load_Slider_images();
        }
    }

    protected void grd_slider_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DAL dal = new DAL();
        String SliderID = e.CommandArgument.ToString();
        String errmsg = "";
        if (e.CommandName == "set_active_Slider")
        {
             
            //SqlCommand com = new SqlCommand("dbo_set_active_Slider_Slider", CommonCode.con);
            //com.Parameters.Add("@SliderID", SqlDbType.BigInt).Value = SliderID;
            //com.CommandType = CommandType.StoredProcedure;
            //errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            //if (errmsg != "success")
            //    CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
            //else
            //    Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        else if (e.CommandName == "update_link")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            HiddenField hf_Sliderpath = (HiddenField)row.FindControl("hf_Sliderpath");
            HiddenField hf_BackImagePath = (HiddenField)row.FindControl("hf_BackImagePath");
            HiddenField hf_active = (HiddenField)row.FindControl("hf_active");
            TextBox textLink = (TextBox)row.FindControl("textLink");
            TextBox txtBookCode = (TextBox)row.FindControl("txtBookCode");            
            dal.Insert_Update_Delete_Sliders(SliderID,hf_Sliderpath.Value,textLink.Text.Trim(),1,hf_BackImagePath.Value,txtBookCode.Text.Trim(), Session["iCompanyId"].ToString(),out errmsg);
            //SqlCommand com = new SqlCommand("web_insert_edit_delete_sliders", CommonCode.con);
            //com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.Add("@SliderID", SqlDbType.BigInt).Value = SliderID;
            //com.Parameters.Add("@SliderPath", SqlDbType.NVarChar).Value = hf_Sliderpath.Value;
            //com.Parameters.Add("@URL", SqlDbType.NVarChar).Value = textLink.Text.Trim();
            //com.Parameters.Add("@action", SqlDbType.Int).Value = 1;
            //com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
            //com.Parameters.Add("@BookCode", SqlDbType.VarChar,20).Value = txtBookCode.Text.Trim();
            //com.Parameters.Add("@BackImagePath", SqlDbType.VarChar, 8000).Value = hf_BackImagePath.Value;
            //com.CommandType = CommandType.StoredProcedure;
            //errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg != "success")
                CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
            else
                Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        else if (e.CommandName == "delete_record")
        {            
            var values = SliderID.Split(';');
            if (values.Length == 2)
            {
                String CA_SliderID = values[0], CA_SliderpPath = values[1];

                if (File.Exists(Server.MapPath(CA_SliderpPath)))
                    File.Delete(Server.MapPath(CA_SliderpPath));
                dal.Insert_Update_Delete_Sliders(CA_SliderID, "", "", 2, "", "", Session["iCompanyId"].ToString(), out errmsg);
                //SqlCommand com = new SqlCommand("web_insert_edit_delete_sliders", CommonCode.con);
                //com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.Add("@SliderID", SqlDbType.BigInt).Value = CA_SliderID;
                //com.Parameters.Add("@SliderPath", SqlDbType.NVarChar).Value = "";
                //com.Parameters.Add("@URL", SqlDbType.NVarChar).Value = "";
                //com.Parameters.Add("@Action", SqlDbType.Int).Value = 2;
                //com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
                //com.Parameters.Add("@BookCode", SqlDbType.VarChar, 20).Value = "";
                //com.Parameters.Add("@BackImagePath", SqlDbType.VarChar, 8000).Value = "";
                //com.CommandType = CommandType.StoredProcedure;
                //errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
                if (errmsg != "success")
                    CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
                else
                    Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            }
        }
    }
    
}