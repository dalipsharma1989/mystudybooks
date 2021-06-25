using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            load_all_images();
        }
    }

    private void load_all_images()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("FilePath", typeof(String)), new DataColumn("FileName", typeof(String)), new DataColumn("DateCreated", typeof(DateTime))
            , new DataColumn("IconPath", typeof(String))
        });

        string[] filePaths = Directory.GetFiles(Server.MapPath("/Catalogue/"));
        List<ListItem> files = new List<ListItem>();
        foreach (string filePath in filePaths)
        {
            string fileName = Path.GetFileName(filePath);
            if (Path.GetExtension(filePath) == ".pdf" || Path.GetExtension(filePath) == ".xls" || Path.GetExtension(filePath) == ".xlsx")
            {
                if(Path.GetExtension(filePath) == ".pdf")
                {
                    dt.Rows.Add("../Catalogue/" + Path.GetFileName(filePath), Path.GetFileName(filePath), File.GetCreationTime(filePath), "../Catalogue/" + Path.GetFileName("../Catalogue/PDF.jpg"));
                }
                else
                {
                    dt.Rows.Add("../Catalogue/" + Path.GetFileName(filePath), Path.GetFileName(filePath), File.GetCreationTime(filePath), "../Catalogue/" + Path.GetFileName("/Catalogue/excel.jpg"));
                }

            }
                
        }
        if (dt.Rows.Count > 0)
        {
            rp_media.DataSource = dt;
            rp_media.DataBind();
        }
    }


    protected void rp_media_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String FilePath = e.CommandArgument.ToString();
        if (e.CommandName == "delete_image")
        {
            if (File.Exists(Server.MapPath(FilePath)))
            {
                File.Delete(Server.MapPath(FilePath));
                CommonCode.show_alert("success", "<i class='fa fa-check'></i>&nbsp;File deleted Successfuly", "", ltr_msg);
                load_all_images();
            }
            else
            {
                CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;File doesn`t exists", "", ltr_msg);
            }
        }
    }

    protected void btn_uploadFiles_Click(object sender, EventArgs e)
    {
        String FolderName = "../Catalogue/";
        int count = 0;
        try
        {
            string errmsg = "";
            if (FileUpload1.PostedFiles.Count > 5)
            {
                CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;Overflow", "Max 5 files allowed.", ltr_msg);
            }
            else
            {
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    if (postedFile.ContentLength < 25600000)
                    {
                        if (System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".pdf" || System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".xls" || System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".xlsx")
                        {
                            string fileName = Path.GetFileName(postedFile.FileName);
                            postedFile.SaveAs(Server.MapPath(FolderName) + fileName);
                            count++;
                        }
                        else
                        {
                            errmsg += "<li class='text-danger'>File " + postedFile.FileName + " is not an image file</li>";
                        }
                    }
                    else
                    {
                        errmsg += "<li class='text-danger'>File " + postedFile.FileName + " is larger than 250 KB.</li>";
                    }

                }
                CommonCode.show_alert("success", "Report",
                    "<p>" + count + " Documents upload successfully <a href='UploadDocuments.aspx' class='btn btn-xs btn-success'><i class='fa fa-refresh'></i>&nbsp;Refresh to load uploaded Documents</a></p>" +
                    "<ul>" +
                    errmsg +
                    "</ul>"+
                    ""
                    , ltr_msg);
            }
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;Error", ex.Message, ltr_msg);
        }
    }
}