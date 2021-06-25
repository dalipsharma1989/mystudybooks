using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Telerik.Web.UI;
public partial class admin_UploadSchoolLogo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Upload School Logo");
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                LoadSchools("");
                load_SchoolImages();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }
    }


    private void load_SchoolImages(String schoolId= "")
    {
        string errmsg = "";
        DAL dal = new DAL();

        DataTable dt1 = new DataTable();
        dt1 = dal.ICompanyID_BranchID(out errmsg);
        if (dt1.Rows.Count > 0)
        {
            string iCompanyID = dt1.Rows[0]["iCompanyId"].ToString();
            string iBranchID = dt1.Rows[0]["iBranchID"].ToString();

            Session["iCompanyId"] = dt1.Rows[0]["iCompanyId"].ToString();
            Session["iBranchID"] = dt1.Rows[0]["iBranchID"].ToString();
            Session["FinancialPeriod"] = dt1.Rows[0]["FinancialPeriod"].ToString();

            DataTable dt = new DataTable();
            dt = dal.get_schools(schoolId, iCompanyID, iBranchID, out errmsg);
            if (errmsg == "success")
            {
                if (dt.Rows.Count > 0)
                {
                    DataTable dtSchool = new DataTable();
                    dtSchool.Columns.AddRange(new DataColumn[] 
                                                {   new DataColumn("FilePath", typeof(String)),
                                                    new DataColumn("SchoolCode", typeof(String)),
                                                    new DataColumn("SchoolName", typeof(String)),
                                                    new DataColumn("DateCreated", typeof(DateTime)) 
                                                }
                                            );

                    string[] filePaths = Directory.GetFiles(Server.MapPath("../resources/SchoolLogo/"));
                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        string fileName = Path.GetFileName(filePath);
                        if (Path.GetExtension(filePath) == ".jpg" || Path.GetExtension(filePath) == ".png" || Path.GetExtension(filePath) == ".jpeg")
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if(dr["CustomerCode"].ToString() == Path.GetFileName(filePath).ToString().Replace(".png", "").Replace(".jpeg", "").Replace(".jpg",""))
                                {
                                    dtSchool.Rows.Add("../resources/SchoolLogo/" + Path.GetFileName(filePath), dr["CustomerCode"].ToString(), dr["CustomerName"].ToString(), File.GetCreationTime(filePath));
                                }
                            } 
                        }
                    }

                    grd_slider.DataSource = dtSchool;
                    grd_slider.DataBind();
                }
                else
                {
                    drp_School.Items.Add(new ListItem("Select School", "Nil"));
                }
            }
        }
    }

    private void LoadSchools(string schoolId = "")
    {
        string errmsg = "";
        DAL dal = new DAL();

        DataTable dt1 = new DataTable();
        dt1 = dal.ICompanyID_BranchID(out errmsg);
        if (dt1.Rows.Count > 0)
        {
            string iCompanyID = dt1.Rows[0]["iCompanyId"].ToString();
            string iBranchID = dt1.Rows[0]["iBranchID"].ToString();

            Session["iCompanyId"] = dt1.Rows[0]["iCompanyId"].ToString();
            Session["iBranchID"] = dt1.Rows[0]["iBranchID"].ToString();
            Session["FinancialPeriod"] = dt1.Rows[0]["FinancialPeriod"].ToString();

            DataTable dt = new DataTable();
            dt = dal.get_schools(schoolId, iCompanyID, iBranchID, out errmsg);
            if (errmsg == "success")
            {
                if (dt.Rows.Count > 0)
                { 
                    drp_School.Items.Add(new ListItem("Select School", "Nil"));
                    foreach (DataRow rr in dt.Rows)
                    {
                        drp_School.Items.Add(new ListItem(rr[1].ToString(), rr[0].ToString()));
                    }
                }
                else
                {
                    drp_School.Items.Add(new ListItem("Select School", "Nil"));
                }
            }
        }

    }

    protected void btn_SaveLogo_Click(object sender, EventArgs e)
    {
        String filepath = "";
        String img_errmsg = "", fileName = "";
        string img = string.Empty;
        int width = 200, height = 300;
        try
        {
            if (FileUpload1.PostedFiles.Count > 1)
            {
                CommonCode.show_alert("danger", "Error", "one file allowed one time.", ltr_msg);
            }
            else
            {
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
                    fileName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    System.Drawing.Image resizedImage = ImageFixedResize.FixedSize(image, width, height, out img_errmsg);

                    filepath = "/resources/SchoolLogo/" + drp_School.SelectedValue.ToString() + ".jpg";
                    CommonCode.CreateDirectory("SchoolLogo");
                    img = Server.MapPath("/resources/SchoolLogo/") + drp_School.SelectedValue.ToString() + ".jpg";
                    resizedImage.Save(img, System.Drawing.Imaging.ImageFormat.Jpeg);

                    if (img_errmsg != "success")
                    {
                        CommonCode.show_alert("danger", "Error", img_errmsg, ltr_msg); 
                    }
                    else
                    {
                        load_SchoolImages();
                        CommonCode.show_alert("success", "Logo Saved Successfully", "", ltr_msg);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
 
    protected void grd_slider_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}