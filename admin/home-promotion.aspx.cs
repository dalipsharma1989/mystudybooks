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
public partial class _Defaultds : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("HomePage small Sliders");
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_homepage_data();
                if (!string.IsNullOrEmpty(Request.QueryString["slider"]))
                {
                    hf_sliderID.Value = Request.QueryString["slider"];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["updated"]))
                {
                    CommonCode.show_alert("success", "HomePage Updated !", "", ltr_msg);
                }                    
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }
    }

    protected void btn_save_edit_Click(object sender, EventArgs e)
    {
        String Slider_csv = "";
        string slider_name = "Slider 1";
        if (!string.IsNullOrEmpty(textRad_SliderName.Text))
            slider_name = textRad_SliderName.Text;
        Slider_csv = "{" + slider_name + "}";

        string slider_books = "";
        foreach (AutoCompleteBoxEntry item in text_Rad_search_Books.Entries)
        {
            slider_books += item.Value + "','";
        }
        if (!string.IsNullOrEmpty(slider_books))
            slider_books = slider_books.Substring(0, slider_books.Length - 2);

        Slider_csv += "|{'" + slider_books + "}";
        ltr_msg.Text = Slider_csv;

        save_Homepage_data(Slider_csv);
    }

    private void load_all_products()
    {
        SqlCommand com = new SqlCommand("dbo_get_books", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@no", SqlDbType.Int).Value = 0;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {

            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
        }
    }

    private void load_homepage_data()
    {
        DAL dal = new DAL();
        string errmsg = "";
        DataTable dt = new DataTable();
        dt = dal.get_homepage_data(Session["iCompanyID"].ToString() ,out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                hf_RowID.Value = dt.Rows[0]["RowID"] + "";
                string IstBookSlider = "", IIndBookSlider = "", IIIrdBookSlider = "", IVthBookSlider = "", VthBookSlider = "", VIthBookSlider = "", VIIBookSlider = "", VIIIBookSlider = "";
                IstBookSlider = hf_Slider1_value.Value = dt.Rows[0]["IstBookSlider"] + "";
                IIndBookSlider = hf_Slider2_value.Value = dt.Rows[0]["IIndBookSlider"] + "";
                IIIrdBookSlider = hf_Slider3_value.Value = dt.Rows[0]["IIIrdBookSlider"] + "";
                IVthBookSlider = hf_Slider4_value.Value = dt.Rows[0]["IVthBookSlider"] + "";
                VthBookSlider = hf_Slider5_value.Value = dt.Rows[0]["VthBookSlider"] + "";
                VIthBookSlider = hf_Slider6_value.Value = dt.Rows[0]["VIthBookSlider"] + "";
                VIIBookSlider = hf_Slider7_value.Value = dt.Rows[0]["VIIBookSlider"] + "";
                VIIIBookSlider = hf_Slider8_value.Value = dt.Rows[0]["VIIIBookSlider"] + "";

                ltr_books_selected.Text = "Ist - " + IstBookSlider + "<br>" + 
                                          "IInd - " + IIndBookSlider + "<br>" + 
                                          "IIIrd- " + IIIrdBookSlider + "<br>" + 
                                          "IVth- " + IVthBookSlider + "<br>" + 
                                          "Vth- " + VthBookSlider + "<br>" +
                                          "VIth- " + VIthBookSlider + "<br>" +
                                          "VII- " + VIIBookSlider + "<br>" +
                                          "VIII- " + VIIIBookSlider;
            }
            else
            {
                hf_RowID.Value = "0";
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
        }
    }

    private void save_Homepage_data(string slider_csv)
    {
        string RowID = "", Sidebar = "", PromotionSlider = "", IstBanner = "", IIndBanner = "", IIIrdBanner = "", IVthBanner = "", VthBanner = "", VIthBanner = "", VIIBanner = "", VIIIBanner = "",
                IstBookSlider = "", IIndBookSlider = "", IIIrdBookSlider = "", IVthBookSlider = "", VthBookSlider = "", VIthBookSlider = "", VIIBookSlider = "", VIIIBookSlider = "";

        RowID = hf_RowID.Value;

        if (dd_rad_Slider.SelectedValue == "Slider1")
            IstBookSlider = slider_csv;
        if (dd_rad_Slider.SelectedValue == "Slider2")
            IIndBookSlider = slider_csv;
        if (dd_rad_Slider.SelectedValue == "Slider3")
            IIIrdBookSlider = slider_csv;
        if (dd_rad_Slider.SelectedValue == "Slider4")
            IVthBookSlider = slider_csv;
        if (dd_rad_Slider.SelectedValue == "Slider5")
            VthBookSlider = slider_csv;
        if (dd_rad_Slider.SelectedValue == "Slider6")
            VIthBookSlider = slider_csv;
        if (dd_rad_Slider.SelectedValue == "Slider7")
            VIIBookSlider = slider_csv;
        if (dd_rad_Slider.SelectedValue == "Slider8")
            VIIIBookSlider = slider_csv;
        DAL dal = new DAL();
        string errmsg = "";
        dal.insert_update_delete_homepage(Sidebar, PromotionSlider, IstBanner, IIndBanner, IIIrdBanner, IVthBanner, VthBanner, VIthBanner, VIIBanner, VIIIBanner,
            IstBookSlider, IIndBookSlider, IIIrdBookSlider, IVthBookSlider, VthBookSlider, VIthBookSlider, VIIBookSlider, VIIIBookSlider, Session["iCompanyID"].ToString(), out errmsg);


        if (errmsg == "success")
        {
            CommonCode.show_alert("success", "HomePage Updated !", "", ltr_msg);
            //load_homepage_data();
            Response.Redirect("home-promotion.aspx?updated=1", true);
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, ltr_msg);
        }
    }
    
    protected void dd_rad_Slider_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        string slider = "";
        text_Rad_search_Books.Entries.Clear();
        textRad_SliderName.Text = "";
        if (dd_rad_Slider.SelectedValue == "Slider1")
            slider = hf_Slider1_value.Value;
        else if (dd_rad_Slider.SelectedValue == "Slider2")
            slider = hf_Slider2_value.Value;
        else if (dd_rad_Slider.SelectedValue == "Slider3")
            slider = hf_Slider3_value.Value;
        else if (dd_rad_Slider.SelectedValue == "Slider4")
            slider = hf_Slider4_value.Value;
        else if (dd_rad_Slider.SelectedValue == "Slider5")
            slider = hf_Slider5_value.Value;
        else if (dd_rad_Slider.SelectedValue == "Slider6")
            slider = hf_Slider6_value.Value;
        else if (dd_rad_Slider.SelectedValue == "Slider7")
            slider = hf_Slider7_value.Value;
        else if (dd_rad_Slider.SelectedValue == "Slider8")
            slider = hf_Slider8_value.Value;

        if (!string.IsNullOrEmpty(slider))
        {
            string[] slider_arr = slider.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string slider_csv;
            string slider_name = slider_arr[0].Replace("{", "").Replace("}", "");

            textRad_SliderName.Text = slider_name;
            slider_csv = slider_arr[1].Replace("{", "").Replace("}", "");
            set_AutoCompleteBoxEntry(slider_csv);
        }
    }
    private void set_AutoCompleteBoxEntry(string csv)
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.getDataByQuery("select BookName, BookCode as ProductID from MasterTitle where BookCode in (" + csv + ") and iCompanyId = '"+ Session["iCompanyID"].ToString() +"' ", out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rr in dt.Rows) 
                {
                    text_Rad_search_Books.Entries.Add(new AutoCompleteBoxEntry(rr["BookName"].ToString(), rr["ProductID"].ToString()));
                }
            }
        }
    }
}