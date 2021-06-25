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
    public String BannerImage1, BannerImage2, BannerImage3, BannerImage4, BannerImage5, BannerImage6;
    public String BannerImage_Link1, BannerImage_Link2, BannerImage_Link3, BannerImage_Link4, BannerImage_Link5, BannerImage_Link6;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.Title = CommonCode.SetPageTitle("Banners");
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_banners();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }
    }

   private void load_banners()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_BannerImages(Session["iCompanyId"].ToString(), "", out errmsg);       
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                search_datatable("1", dt, out BannerImage1, out BannerImage_Link1);
                search_datatable("2", dt, out BannerImage2, out BannerImage_Link2);
                search_datatable("3", dt, out BannerImage3, out BannerImage_Link3);
                search_datatable("4", dt, out BannerImage4, out BannerImage_Link4);
                search_datatable("5", dt, out BannerImage5, out BannerImage_Link5);
                search_datatable("6", dt, out BannerImage6, out BannerImage_Link6);

                if (!string.IsNullOrEmpty(BannerImage1))
                {
                    banner_1.InnerHtml = "<img src='" + BannerImage1 + "' class='img-responsive banner' style='height: 210px;padding-top: 15px;padding-bottom: 15px;'>";
                }
                if (!string.IsNullOrEmpty(BannerImage2))
                {
                    banner_2.InnerHtml = "<img src='" + BannerImage2 + "' class='img-responsive banner' style='height: 210px;padding-top: 15px;padding-bottom: 15px;' >";
                }
                if (!string.IsNullOrEmpty(BannerImage3))
                {
                    banner_3.InnerHtml = "<img src='" + BannerImage3 + "' class='img-responsive banner' style='height: 210px;padding-top: 15px;padding-bottom: 15px;'>";
                }

                if (!string.IsNullOrEmpty(BannerImage4))
                {
                    banner_4.InnerHtml = "<img src='" + BannerImage4 + "' class='img-responsive banner' style='height: 210px;padding-top: 15px;padding-bottom: 15px;'>";
                }
                if (!string.IsNullOrEmpty(BannerImage5))
                {
                    banner_5.InnerHtml = "<img src='" + BannerImage5 + "' class='img-responsive banner' style='height: 210px;padding-top: 15px;padding-bottom: 15px;'>";
                }

                if (!string.IsNullOrEmpty(BannerImage6))
                {
                    banner_6.InnerHtml = "<img src='" + BannerImage6 + "' class='img-responsive banner' style='height: 210px;padding-top: 15px;padding-bottom: 15px;'>";
                }
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
        }
    }

    private void search_datatable(String Value, DataTable dt, out String ImagePath, out String Link)
    {
        ImagePath = "";
        Link = "";
        foreach (DataRow rr in dt.Rows)
        {
            if (rr["BannerID"].ToString() == Value)
            {
                ImagePath = rr["ImagePath"].ToString();
                Link = rr["Link"].ToString();
            }
        }
    }
}