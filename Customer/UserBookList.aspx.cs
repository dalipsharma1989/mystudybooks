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

public partial class Customer_UserBookList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["CustID"] != null && Session["CustID"].ToString() != "guest")
            {
                if (!IsPostBack)
                {
                    this.Title = CommonCode.SetPageTitle("Book List");
                    load_get_UserBookList(Session["CustID"].ToString());
                }
            }
            else
            { 
                Response.Redirect("user_login.aspx", false);
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void load_get_UserBookList(string UserID)
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_UserBookList(UserID, "", "", "", "", 1, out  errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
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
            CommonCode.show_alert("danger", "Error", errmsg, ph_Msg);
        }
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (txt_SchoolName.Text == "")
        {
            //CommonCode.show_toastr("", "", "", 0, "", 0, "", ltr_Msg);
            ltr_Msg.Text = "<script>toastr.error('Please Enter School Name ', 'Upload Error');</script>";
            return;
        }
        if (txt_ClassDesc.Text == "")
        {
            //CommonCode.show_toastr("", "", "", 0, "", 0, "", ltr_Msg);
            ltr_Msg.Text = "<script>toastr.error('Please Enter Class Name ', 'Upload Error');</script>";
            return;
        }
        if (txt_Language.Text == "")
        {
            //CommonCode.show_toastr("", "", "", 0, "", 0, "", ltr_Msg);
            ltr_Msg.Text = "<script>toastr.error('Please Enter Language Name ', 'Upload Error');</script>";
            return;
        }
        if (uploadbooklist.PostedFiles.Count > 1)
        {
            CommonCode.show_alert("danger", "Error", "one file allowed one time.", ph_Msg);
            return;
        }
        else
        {
            DataTable errdt = new DataTable();
            errdt.Columns.AddRange(new DataColumn[] { new DataColumn("FileName"), new DataColumn("Status"), new DataColumn("Msg") });

            foreach (HttpPostedFile postedFile in uploadbooklist.PostedFiles)
            {
                String errmsg = "", Status = "";
                System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
                string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName); 

                save_images(image, fileName,  out errmsg);
                if (errmsg == "success")
                {
                    load_get_UserBookList(Session["CustID"].ToString());
                    CommonCode.show_alert("success", "BookList Uploaded Successfully", "", ph_Msg);
                }    
                else
                {
                    CommonCode.show_alert("danger", "Something went wrong, Please try again.", errmsg, ph_Msg);
                }

            } 
        } 
    }


    private void save_images(System.Drawing.Image image, String Filename, out string func_errmsg)
    {
        func_errmsg = "";
        try
        {
            String filepath = "";
            String img_errmsg = "";
            string img = string.Empty;

            int width = 1920, height = 768; 
            System.Drawing.Image resizedImage = ImageFixedResize.FixedSize(image, width, height, out img_errmsg);
             
            filepath = "/resources/UsersBookList/" + Session["CustID"].ToString() + "/" + Filename + ".Jpeg";
            CommonCode.CreateDirectoryBookList(Session["CustID"].ToString());
            img = Server.MapPath("/resources/UsersBookList/" + Session["CustID"].ToString() + "/") + Filename + ".Jpeg";
            resizedImage.Save(img, System.Drawing.Imaging.ImageFormat.Jpeg);

            DAL dal = new DAL();
            dal.Insert_UserBookList(Session["CustID"].ToString(),  txt_SchoolName.Text.Trim(),  txt_ClassDesc.Text.Trim(), txt_Language.Text.Trim(), filepath, 0 , out func_errmsg); 
        }
        catch (Exception ex)
        {
            func_errmsg = ex.Message;
        }
    }
}