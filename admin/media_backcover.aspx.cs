using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

public partial class _Default : System.Web.UI.Page
{

    DataTable dtimage = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_all_images();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }

        
    }


    private void load_all_images()
    {
        DataTable dt = new DataTable();
        String folderName = "product/backcovers/";
        if (Directory.Exists(Server.MapPath("~/resources/" + folderName + "")))
        {
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("FilePath", typeof(String)),
            new DataColumn("FileName", typeof(String)),
            new DataColumn("DateCreated", typeof(DateTime))
        });
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/resources/" + folderName + ""));
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                if (Path.GetExtension(filePath) == ".png" || Path.GetExtension(filePath) == ".jpg" || Path.GetExtension(filePath) == ".bmp")
                    dt.Rows.Add("/resources/" + folderName + "/" + Path.GetFileName(filePath), Path.GetFileName(filePath), File.GetCreationTime(filePath));
            }
            if (dt.Rows.Count > 0)
            {
                CommonCode.show_alert("info", dt.Rows.Count + " Back Covers found !", "", ltr_msg);
                grd_img.DataSource = dt;
                grd_img.DataBind();
            }
            else
                CommonCode.show_alert("info", "<i class='fa fa-info'></i>&nbsp;No Back cover found", "", ltr_msg);
        }
        else
        {
            CommonCode.show_alert("warning", "<i class='fa fa-warning'></i>&nbsp;Invalid Folder Mapping", "Folder <b>" + folderName + "</b> doesn`t exists", ltr_msg);
        }
    }

    protected void btn_uploadFiles_Click(object sender, EventArgs e)
    {
        if (rb_options.SelectedValue == "Auto-Resize")
        {
            auto_resize();
        }
        else if (rb_options.SelectedValue == "Fixed-Resize")
        {
            fixed_resize();
        }
        else
        {
            no_resize();
        }
        load_all_images();
    }

    private void no_resize()
    {
        String FolderName = "/resources/product/backcovers/";
        int count = 0;
        try
        {
            string errmsg = "";
            if (FileUpload1.PostedFiles.Count > 1000)
            {
                CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;Overflow", "Max 1000 files allowed.", ltr_msg);
            }
            else
            {
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    if (postedFile.ContentLength < 256000)
                    {
                        if (System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".png" || System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".jpg" || System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".jpeg")
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
                    "<p>" + count + " images upload successfully <a href='media.aspx' class='btn btn-xs btn-success'><i class='fa fa-refresh'></i>&nbsp;Refresh to load uploaded images</a></p>" +
                    "<ul>" +
                    errmsg +
                    "</ul>" +
                    ""
                    , ltr_msg);
            }
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;Error", ex.Message, ltr_msg);
        }
    }

    private void auto_resize()
    {
        CommonCode.CreateDirectory("product");
        String FolderName = "/resources/product/backcovers/";
        int count = 0;
        try
        {
            string errmsg = "";
            if (FileUpload1.PostedFiles.Count > 1000)
            {
                CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;Overflow", "Max 1000 files allowed.", ltr_msg);
            }
            else
            {
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    if (postedFile.ContentLength < 256000)
                    {
                        if (System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".png" || System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".jpg"
                            || System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".jpeg")
                        {
                            string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName); //Path.GetFileName(postedFile.FileName);

                            string img = string.Empty;
                            String filepath = "";
                            Bitmap bmpImg = null;

                            System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);

                            int intWidth, intHeight;
                            intWidth = image.Width;
                            intHeight = image.Height;
                            if (image.Width > 200)
                            {
                                intWidth = 200;
                                intHeight = (int)(((float)intWidth / image.Width) * image.Height);
                            }
                            var newWidth = (int)(intWidth);
                            var newHeight = (int)(intHeight);
                            bmpImg = CompressImage.Resize_Image(postedFile.InputStream, newWidth, newHeight);

                            filepath = FolderName + fileName + ".png";
                            img = (Server.MapPath(FolderName) + fileName + ".png");
                            bmpImg.Save(img, System.Drawing.Imaging.ImageFormat.Png);

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
                    "<p>" + count + " images upload successfully <a href='media.aspx' class='btn btn-xs btn-success'><i class='fa fa-refresh'></i>&nbsp;Refresh to load uploaded images</a></p>" +
                    "<ul>" +
                    errmsg +
                    "</ul>" +
                    ""
                    , ltr_msg);
            }
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;Error", ex.Message, ltr_msg);
        }
    }

    private void fixed_resize()
    {
        CommonCode.CreateDirectory("product");
        String FolderName = "/resources/product/backcovers/";
        int count = 0;
        try
        {
            string errmsg = "";
            if (FileUpload1.PostedFiles.Count > 1000)
            {
                CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;Overflow", "Max 1000 files allowed.", ltr_msg);
            }
            else
            {
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    if (postedFile.ContentLength < 256000)
                    {
                        if (System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".png" || System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".jpg" || System.IO.Path.GetExtension(postedFile.FileName).ToLower() == ".jpeg")
                        {

                            string fileName = Path.GetFileName(postedFile.FileName);

                            string img = string.Empty;
                            String filepath = "";
                            Bitmap bmpImg = null;

                            System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
                            System.Drawing.Image resized_image;
                            int intWidth, intHeight;
                            intWidth = image.Width;
                            intHeight = image.Height;
                            if (image.Width > 200)
                            {
                                intWidth = 200;
                                intHeight = (int)(((float)intWidth / image.Width) * image.Height);
                            }
                            var newWidth = (int)(intWidth);
                            var newHeight = (int)(intHeight);
                            resized_image = ImageFixedResize.FixedSize(image, 200, 300, out errmsg);
                            filepath = FolderName + postedFile.FileName;
                            img = (Server.MapPath(FolderName) + fileName);
                            resized_image.Save(img, ImageFormat.Png);
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
                    "<p>" + count + " images upload successfully <a href='media.aspx' class='btn btn-xs btn-success'><i class='fa fa-refresh'></i>&nbsp;Refresh to load uploaded images</a></p>" +
                    "<ul>" +
                    errmsg +
                    "</ul>" +
                    ""
                    , ltr_msg);

            }
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;Error", ex.Message, ltr_msg);
        }
    }

    protected void grd_img_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_img.PageIndex = e.NewPageIndex;
        load_all_images();
    }

    protected void grd_img_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String FilePath = e.CommandArgument.ToString();
        if (e.CommandName == "delete_image")
        {
            if (File.Exists(Server.MapPath(FilePath)))
            {
                File.Delete(Server.MapPath(FilePath));
                CommonCode.show_alert("success", "<i class='fa fa-check'></i>&nbsp;Image deleted Successfuly", "", ltr_msg);
                load_all_images();
            }
            else
            {
                CommonCode.show_alert("danger", "<i class='fa fa-times'></i>&nbsp;File doesn`t exists", "", ltr_msg);
            }
        }
    }

    private void check_books_without_images()
    {
        string errmsg = "";
        //Build the CSV file data as a Comma separated string.
        string csv = string.Empty;
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.getDataByQuery("select ISBN,BookName from MasterProduct", out errmsg);
        DataTable dt_list_books = new DataTable();
        dt_list_books.Columns.AddRange(new DataColumn[] { new DataColumn("ISBN"), new DataColumn("BookName") });
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                csv = "ISBN,BookName,";
                //Add new line.
                csv += "\r\n";

                foreach (DataRow rr in dt.Rows)
                {
                    if (!File.Exists(Server.MapPath("~/resources/product/") + rr["ISBN"] + ".png"))
                    {
                        csv += rr["ISBN"] + "," + rr["BookName"] + ",";
                        csv += "\r\n";
                    }
                }

                //Download the CSV file.
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ExportToCSV.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
            else
            {
                CommonCode.show_alert("warning", "No books found", "", ltr_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading books", errmsg, ltr_msg);
        }
    }

    protected void lb_exporttocsv_Click(object sender, EventArgs e)
    {
        check_books_without_images();
    }

    private void search_images()
    {
        String folderName = "product/backcovers/", isbn = "", found_files = "";
        isbn = txtsearch.Text;
        if (!string.IsNullOrEmpty(isbn))
        {
            string[] files = Directory.GetFiles(Server.MapPath("~/resources/" + folderName + ""), isbn + "*.png", SearchOption.TopDirectoryOnly);

            grd_img.DataSource = null;
            grd_img.DataBind();
            if (files.Length > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("FilePath", typeof(String)),
                    new DataColumn("FileName", typeof(String)),
                    new DataColumn("DateCreated", typeof(DateTime))
                });

                foreach (string item in files)
                {
                    found_files = item.Substring(item.IndexOf(folderName.Replace("/", @"\")) + folderName.Length);
                    dt.Rows.Add("/resources/" + folderName + "/" + found_files, found_files, DateTime.Now);
                }

                if (dt.Rows.Count > 0)
                {
                    CommonCode.show_alert("info", dt.Rows.Count + " Back Covers found for search: '" + isbn + "'", "", ltr_msg);
                    grd_img.DataSource = dt;
                    grd_img.DataBind();
                }
                else
                    CommonCode.show_alert("warning", "<i class='fa fa-info'></i>&nbsp;No Back cover found", "", ltr_msg);
            }
            else
                CommonCode.show_alert("warning", "<i class='fa fa-info'></i>&nbsp;No Back cover found", "", ltr_msg);
        }
        else
            Response.Redirect("media_backcover.aspx", true);
    }

    protected void lb_search_isbn_image_Click(object sender, EventArgs e)
    {
        search_images();
    }
}