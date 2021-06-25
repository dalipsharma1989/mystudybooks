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

public partial class admin_uploadSchoolClassLogo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Upload School Class Logo");
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

    private void load_SchoolImages(String schoolId = "")
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

                    string[] filePaths = Directory.GetFiles(Server.MapPath("../resources/ClassLogo/"));
                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        string fileName = Path.GetFileName(filePath);
                        if (Path.GetExtension(filePath) == ".jpg" || Path.GetExtension(filePath) == ".png" || Path.GetExtension(filePath) == ".jpeg")
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                DataTable dtClass = new DataTable();
                                dtClass = dal.get_Classes("", dr["CustomerCode"].ToString(), Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);
                                if (dtClass.Rows.Count > 0)
                                {
                                    foreach (DataRow drz in dtClass.Rows)
                                    {
                                        if (dr["CustomerCode"].ToString() + "-" + drz["ClassCode"].ToString() == Path.GetFileName(filePath).ToString().Replace(".png", "").Replace(".jpeg", "").Replace(".jpg", ""))
                                        {
                                            dtSchool.Rows.Add("../resources/ClassLogo/" + Path.GetFileName(filePath), dr["CustomerCode"].ToString() + "-" + drz["ClassCode"].ToString(), drz["ClassName"].ToString(), File.GetCreationTime(filePath));
                                        }
                                    }
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

    private void LoadClasses(string SchoolID = "", string className = "")
    {

        string errmsg = "";
        drp_Class.Items.Clear();
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_Classes(className, SchoolID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                drp_Class.Items.Add(new ListItem("Select Class", "Nil"));
                foreach (DataRow rr in dt.Rows)
                {
                    drp_Class.Items.Add(new ListItem(rr[1].ToString(), rr[0].ToString()));
                }
            }
            else
            {
                drp_Class.Items.Add(new ListItem("Select Class", "Nil"));
            }
        }
        else
        {
            drp_Class.Items.Add(new ListItem("Select Class", "Nil"));
        }
    }


    protected void grd_slider_RowCommand(object sender, GridViewCommandEventArgs e)
    {

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

                    filepath = "/resources/ClassLogo/" + drp_School.SelectedValue.ToString() + "-" + drp_Class.SelectedValue.ToString() + ".jpg";
                    CommonCode.CreateDirectory("ClassLogo");
                    img = Server.MapPath("/resources/ClassLogo/") + drp_School.SelectedValue.ToString() + "-" + drp_Class.SelectedValue.ToString() + ".jpg";
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

    protected void drp_School_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadClasses(drp_School.SelectedValue,"");
    }
}