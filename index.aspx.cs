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
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Services;
// Made a class which can store the detail of one book.
public class ModalBookData
{
    public string Name { get; set; }
    public string Currency { get; set; }
    public string Price { get; set; }
    public string Details { get; set; }
    public string ImagePath { get; set; }
    public string Availability { get; set; }
    public string AddToCart { get; set; } 
}

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Home Page");
        this.MetaDescription = CommonCode.SetPageTitle("Home Page");
        try
        {
            //ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "showModal()", true);
            Session["RefNO"] = null;
            if (!IsPostBack)
            {
 
            }
        }
        catch(Exception ex)
        {
            String error = ex.Message;
        } 
    }
     

    private void load_ItemSpecialOffer()
    {
        SqlCommand com = new SqlCommand("web_get_books_SpecialRates", CommonCode.con);
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyID"] + "";
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"] + "";
        com.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                dv_SpecialOffer.Visible = true;
                //rp_SpecialOfferItem.DataSource = dt;
                //rp_SpecialOfferItem.DataBind();
            }
            else {
                dv_SpecialOffer.Visible = false;
            }
        }
        else
        {
            dv_SpecialOffer.Visible = false;
            CommonCode.show_toastr("error", "Error", errmsg, false, "", false, "", ltr_scripts);
        }
    }


    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_ItemSpecialOffer()
    {
        SqlCommand com = new SqlCommand("web_get_books_SpecialRates", CommonCode.con);
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = HttpContext.Current.Session["iCompanyID"] + "";
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = HttpContext.Current.Session["iBranchID"] + "";
        com.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }

            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_ItemSliders()
    {
        DAL dal = new DAL();
        string errmsg = "";
        DataTable dt = new DataTable();
        dt = dal.get_homepage_data(HttpContext.Current.Session["iCompanyID"].ToString(), out errmsg); 

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_BookCategories()
    {
        string errmsg;
        DataTable dt = new DataTable();

        if (HttpContext.Current.Session["Category"] == null)
        {
            SqlCommand com = new SqlCommand("web_GetBookCategory", CommonCode.con);
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = HttpContext.Current.Session["iCompanyID"] + "";
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = HttpContext.Current.Session["iBranchID"] + "";
            com.CommandType = CommandType.StoredProcedure; 
            dt = CommonCode.getData(com, out errmsg);
        }
        else
        {
            DataView dtview = (DataView)HttpContext.Current.Session["Category"];
            dt = dtview.ToTable();
        }
        

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_BookClasses(string BooKCategoryID = "")
    {
        string errmsg;
        DataTable dt = new DataTable();
        if (HttpContext.Current.Session["Class"] == null)
        {
            SqlCommand com = new SqlCommand("web_GetMasterTitle_Class", CommonCode.con);
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = HttpContext.Current.Session["iCompanyID"] + "";
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = HttpContext.Current.Session["iBranchID"] + "";
            com.Parameters.Add("@BookCategoryID", SqlDbType.VarChar, 30).Value = BooKCategoryID;
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        else
        {
            DataView dtview = (DataView)HttpContext.Current.Session["Class"];
            dt = dtview.ToTable();
        } 

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_BookMedium(string BooKCategoryID = "", string MediumID = "") 
    {
        string errmsg;
        DataTable dt = new DataTable(); 

        if (HttpContext.Current.Session["Medium"] == null)
        {
            SqlCommand com = new SqlCommand("web_GetMasterTitle_Medium", CommonCode.con);
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = HttpContext.Current.Session["iCompanyID"] + "";
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = HttpContext.Current.Session["iBranchID"] + "";
            com.Parameters.Add("@BookCategoryID", SqlDbType.VarChar, 30).Value = BooKCategoryID;
            com.Parameters.Add("@MediumID", SqlDbType.VarChar, 30).Value = MediumID;
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        else
        {
            DataView dtview = (DataView)HttpContext.Current.Session["Medium"];
            dt = dtview.ToTable();
        }

        

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_BookSubjectAsCategory()
    {
        string errmsg;
        DataTable dt = new DataTable();
        if (HttpContext.Current.Session["Subject"] == null)
        {
            SqlCommand com = new SqlCommand("web_GetBookSubjects", CommonCode.con);
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = HttpContext.Current.Session["iCompanyID"] + "";
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = HttpContext.Current.Session["iBranchID"] + "";
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        else
        {
            DAL dal = new DAL();
            DataView dtview = (DataView)HttpContext.Current.Session["Subject"];
            dt = dtview.ToTable();
            dt = dal.get_Subject_BySubjectTable(dt, HttpContext.Current.Session["iCompanyID"] + "", out errmsg);
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_Load_TodaySaying()
    {
        DAL dal = new DAL();
        string errmsg = "", MenuID = "";
        DataTable dt = new DataTable();
        dt = dal.get_TodaySaying("0", HttpContext.Current.Session["iCompanyID"] + "", out errmsg);        
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);        
    }
            

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_ItemSliderDetail(string sliderName)
    {
        string sliderheading = "", errmsg = "";         
        DAL dal1 = new DAL();
        DataTable dt_slider1 = new DataTable();
        dt_slider1 = get_slider_info_Detail(HttpContext.Current.Session["OtherCountry"].ToString(), "1", sliderName, out sliderheading, out errmsg);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt_slider1.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt_slider1.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }    

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_banners()
    { 
        DataTable dt = new DataTable();
        string errmsg;
        
        DAL dal = new DAL();
        dt = dal.get_BannerImages(HttpContext.Current.Session["iCompanyID"].ToString(), "0", out errmsg);
         
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_sliders()
    {
       
        SqlCommand com = new SqlCommand("Web_Get_Sliders", CommonCode.con);
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = HttpContext.Current.Session["iCompanyID"] + "";
        com.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    private void load_sliders()
    {
        SqlCommand com = new SqlCommand("Web_Get_Sliders", CommonCode.con);
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyID"] + "";        
        com.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
 
                //rp_sliders.DataSource = dt;
               // rp_sliders.DataBind();
            }
        }
        else
        {
            CommonCode.show_toastr("error", "Error", errmsg, false, "", false, "", ltr_scripts);
        }
    }

    private void load_sliderTwo()
    {
        SqlCommand com = new SqlCommand("Web_Get_SliderTwo", CommonCode.con);
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyID"] + "";
        com.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                //rp_BackandSliderTwo.DataSource = dt;
                //rp_BackandSliderTwo.DataBind();
            }
        }
        else
        {
            CommonCode.show_toastr("error", "Error", errmsg, false, "", false, "", ltr_scripts);
        }
    }


    public String BannerImage1, BannerImage2, BannerImage3, BannerImage4, BannerImage5, BannerImage6;
    public String BannerImage_Link1, BannerImage_Link2, BannerImage_Link3, BannerImage_Link4, BannerImage_Link5, BannerImage_Link6;
    public String BannerBookCode1, BannerBookCode2, BannerBookCode3, BannerBookCode4, BannerBookCode5, BannerBookCode6;
    public String BannerBookName1, BannerBookName2, BannerBookName3, BannerBookName4, BannerBookName5, BannerBookName6;
    public String BannerAboutBook1, BannerAboutBook2, BannerAboutBook3, BannerAboutBook4, BannerAboutBook5, BannerAboutBook6;
    public String BannerBookPrice1, BannerBookPrice2, BannerBookPrice3, BannerBookPrice4, BannerBookPrice5, BannerBookPrice6;
    public String BannerBookDisc1, BannerBookDisc2, BannerBookDisc3, BannerBookDisc4, BannerBookDisc5, BannerBookDisc6;
    public String BannerBookAuthor1, BannerBookAuthor2, BannerBookAuthor3, BannerBookAuthor4, BannerBookAuthor5, BannerBookAuthor6; 
    private void load_banners()
    {
        string default_img_banner_1 = "/resources/no-image.jpg";
        string default_img_banner_2 = "/resources/no-image.jpg";
        string default_img_banner_3 = "/resources/no-image.jpg";
        string default_img_banner_4 = "/resources/no-image.jpg";
        string default_img_banner_5 = "/resources/no-image.jpg";
        string default_img_banner_6 = "/resources/no-image.jpg";

        string onError1 = "this.onerror = null; this.src = \"" + default_img_banner_1 + "\"";
        string onError2 = "this.onerror = null; this.src = \"" + default_img_banner_2 + "\"";
        string onError3 = "this.onerror = null; this.src = \"" + default_img_banner_3 + "\"";
        string onError4 = "this.onerror = null; this.src = \"" + default_img_banner_4 + "\"";
        string onError5 = "this.onerror = null; this.src = \"" + default_img_banner_5 + "\"";
        string onError6 = "this.onerror = null; this.src = \"" + default_img_banner_6 + "\"";
        DataTable dt = new DataTable();
        string errmsg; 
        DAL dal = new DAL();
        dt = dal.get_BannerImages(Session["iCompanyID"].ToString(),"0",out errmsg); 
         
    }

    private void search_datatable(String Value, DataTable dt, out String ImagePath, out String Link, out String BannerBookCode , out String  BannerBookName, out String BannerAboutBook, out String BannerBookPrice , out String BannerBookDisc, out string  BannerBookAuthor)
    {
        ImagePath = "";
        Link = "";
        BannerBookCode = "";
        BannerBookName = "";
        BannerAboutBook = "";
        BannerBookPrice = "";
        BannerBookDisc = "";
        BannerBookAuthor = "";
        foreach (DataRow rr in dt.Rows)
        {
            if (rr["BannerID"].ToString() == Value)
            {
                ImagePath = rr["ImagePath"].ToString();
                Link = rr["Link"].ToString();
                if(rr["BookCode"].ToString() == null)
                {
                    BannerBookCode = "";
                }
                else
                {
                    BannerBookCode = rr["BookCode"].ToString();
                } 
                BannerBookName = rr["BookName"].ToString();
                BannerAboutBook = rr["AboutBook"].ToString();
                BannerBookPrice = rr["SalePrice"].ToString();
                BannerBookDisc = rr["SaleDiscount"].ToString();
                BannerBookAuthor = rr["AuthorName"].ToString();
            }
        }
    }

   
    private DataTable get_slider_info(string OtherCountry, string SliderPos, string SliderCSV, out string SliderName, out string errmsg)
    {
        DataTable dt_slider = new DataTable();
        errmsg = "";
        SliderName = "";
        try
        {
            // dt_slider = null;
            if (!string.IsNullOrEmpty(SliderCSV))
            {
                string slider_csv = "";
                string[] slider_arr = SliderCSV.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                SliderName = slider_arr[0].Replace("{", "").Replace("}", "");
                slider_csv = slider_arr[1].Replace("{", "").Replace("}", "");
                if (!string.IsNullOrEmpty(slider_csv))
                {
                    DAL dal = new DAL();
                    if(slider_csv != "'")
                    {
                        dt_slider = dal.get_sliders_books(OtherCountry, slider_csv, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);
                        if (errmsg == "success")
                        {
                            if (dt_slider.Rows.Count > 0)
                            {
                                errmsg = "success";
                            } 
                        } 
                    }
                }
                else
                    errmsg = "success";
            }
            else
                errmsg = "success";
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
        return dt_slider;
    }
    
    public static DataTable get_slider_info_Detail(string OtherCountry, string SliderPos, string SliderCSV, out string SliderName, out string errmsg) 
    {
        DataTable dt_slider = new DataTable();
        errmsg = "";
        SliderName = "";
        try
        {
            // dt_slider = null;
            if (!string.IsNullOrEmpty(SliderCSV))
            {
                string slider_csv = "";
                string[] slider_arr = SliderCSV.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for(var i=0;i< slider_arr.Length; i++)
                {
                    slider_csv = slider_csv + "'" + slider_arr[i] + "',"; 
                }
                slider_csv= slider_csv.Substring(0, slider_csv.Length - 1);
                //SliderName = slider_arr[0].Replace("{", "").Replace("}", "");
                //slider_csv = slider_arr[1].Replace("{", "").Replace("}", "");
                if (!string.IsNullOrEmpty(slider_csv))
                {
                    DAL dal = new DAL();
                    if (slider_csv != "'")
                    {
                        dt_slider = dal.get_sliders_books_withName(OtherCountry, slider_csv, HttpContext.Current.Session["iCompanyID"].ToString(), HttpContext.Current.Session["iBranchID"].ToString(), "", out errmsg);
                        if (errmsg == "success")
                        {
                            if (dt_slider.Rows.Count > 0)
                            {
                                errmsg = "success";
                            }
                            //else
                            //{
                            //    CommonCode.show_alert("danger", "Empty Slider " + SliderPos + "!", "", ph_msg);
                            //}
                                
                        }
                        //else
                        //{ 
                        //    CommonCode.show_alert("danger", "Error while loading Slider " + SliderPos + " !", errmsg, ph_msg);
                        //}
                    }
                }
                else
                    errmsg = "success";
            }
            else
                errmsg = "success";
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
        return dt_slider;
    }

    // ****************** Added : 04/07/2019 ******************* //
    // *************** Added Add to Cart feature *************** //
    protected void RepeaterCommandArg(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "AddToCart")
        {
            add_item_to_cart(e.CommandArgument.ToString());
            
        }
        else if (e.CommandName == "addtowishlist")
        {
            if (Session["CustID"] !=null && Session["CustID"].ToString() != "guest")
            {
                add_item_to_WishList(e.CommandArgument.ToString());
                //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item successfully added to in your Wish list');", true);
            }
            else
            {
                Response.Redirect("/Customer/user_login.aspx", true);
            }
        }
        ((MasterPage)this.Page.Master).load_Header();
        
    }

    public void add_item_to_cart(String ProductID)
    {
        String errmsg = "success";
        String CustID = "";
        if (Session["CustID"] != null)
        {
            CustID = Session["CustID"].ToString();
        }
        String CartID = "";
        errmsg = CommonCode.AddToCart(CustID, ProductID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out CartID);
        if (errmsg == "success")
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item successfully added to in your cart');", true);
            CommonCode.show_toastr("success", "Cart", "Successfuly added to cart !", true, "closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000",
                                        true, "window.location.replace('Customer/user_cart.aspx');", ltr_scripts);
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item " + errmsg + " in your cart');", true);
            CommonCode.show_toastr("error", "Cart Error !", errmsg.Replace('\'', ' '), true, "closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000",
                                        true, "window.location.replace('Customer/user_cart.aspx');", ltr_scripts);
        }
    }
    
    // ******************* Updated : 04/07/2019 *******************//
    // ******************* Gets data for Modal *******************//
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, UseHttpGet = false)]
    public static ModalBookData GetData(string ProductID)
    {
        ModalBookData mdb = new ModalBookData();
        string errmsg = "";
        DAL dal = new DAL();
        //DataSet ds = dal.get_curr_book_details( ProductID, out errmsg);
        DataSet ds = new DataSet();
        if (HttpContext.Current.Session["OtherCountry"] != null && HttpContext.Current.Session["OtherCountry"].ToString() == "OtherCountry")
        {
            ds = dal.get_curr_book_details("OtherCountry", ProductID, HttpContext.Current.Session["iCompanyID"].ToString(), HttpContext.Current.Session["iBranchID"].ToString(), out errmsg);
        }            
        else
        {
            ds = dal.get_curr_book_details("", ProductID, HttpContext.Current.Session["iCompanyID"].ToString(), HttpContext.Current.Session["iBranchID"].ToString(), out errmsg);
        }
             
        if (errmsg == "success")
        {
            DataTable product_details = ds.Tables[0];
            DataTable customer_reviews = ds.Tables[1];
            DataTable shipping_message = ds.Tables[2];
            if (product_details.Rows.Count > 0)
            {

                product_details = dal.get_ImagesfromAPI(product_details);

                customer_reviews = dal.get_ImagesfromAPI(customer_reviews);

                DataRow dr = product_details.Rows[0];
                mdb.Name = dr["BookName"].ToString();
                mdb.Currency = dr["SaleCurrency"].ToString();

                if (String.IsNullOrEmpty(dr["DiscountPrice"].ToString()))
                {
                    mdb.Price = "<span>" + dr["SaleCurrency"].ToString() + " " + dr["SalePrice"].ToString() + "</span>";
                }
                else
                {
                    mdb.Price = "<strike style='color: black; font-weight:200; font-size: large;'>" + dr["SaleCurrency"].ToString() + " " + dr["SalePrice"].ToString() +
                                    "</strike><br/>" + "<span>" + dr["SaleCurrency"].ToString() + " " + dr["DiscountPrice"].ToString() + "</span>";
                }
                if (dr["ClosingBalStatus"].ToString() == "Out of Stock")
                {                
                    mdb.Availability = "<i  style='color:red;'> Out of Stock </i>";
                    mdb.AddToCart = "<a href='index.aspx?productid=" + ProductID.ToString() + "' id='ANC_AddtoCart' title='Out of Stock' class='qty btn btn-primary disabled' style='line-height:35px !important;background-color: #000000;border-color:#000000;' ><i style='font-weight:800;width:100%;' class='fa fa-shopping-cart'> Add To Cart</i></a>";
                }
                else
                {
                    mdb.Availability = "<i  class='fa fa-check'></i>In Stock";
                    //mdb.AddToCart = "<a href='index.aspx?productid="+ProductID.ToString()+ "' id='ANC_AddtoCart' Title='Add To Cart' class='qty' style='line-height:35px !important;' ><i style='font-weight:800;width:100%;' class='fa fa-shopping-cart'> Add To Cart</i></a>";
                    mdb.AddToCart = "<a href='index.aspx?productid=" + ProductID.ToString() + "' id='ANC_AddtoCart' Title='Add To Cart' class='qty btn btn-primary' style='line-height:35px !important;' ><i style='font-weight:800;width:100%;' class='fa fa-shopping-cart'> Add To Cart</i></a>";
                }
                    
                mdb.ImagePath = product_details.Rows[0]["ImagePath"] + ""; // "/resources/product/" + dr["ISBN"].ToString() + ".png";
                mdb.Details = "<div class='product-info-table'>" + 
                                "<table class='table product-table' style='border-bottom: 1px solid #ddd;'>" + 
                                "<tr>" + "<th>" +  "ISBN" + "</th>" + "<td>" + dr["ISBN"].ToString() + "</td>" + "</tr>"
                                + (string.IsNullOrEmpty(dr["BookName"].ToString()) ? "" : "<tr><th>Book Name</th><td>" + dr["BookName"].ToString() + "</td></tr>")
                                + (string.IsNullOrEmpty(dr["Author"].ToString()) ? "" : "<tr><th>Author</th><td>" + dr["Author"].ToString() + "</td></tr>")
                                + (string.IsNullOrEmpty(dr["Publisher"].ToString()) ? "" : "<tr><th>Publisher</th><td>" + dr["Publisher"].ToString() + "</td></tr>")
                                + (string.IsNullOrEmpty(dr["edition"].ToString()) ? "" : "<tr><th>Edition</th><td>" + dr["edition"].ToString() + "</td></tr>")
                                + (string.IsNullOrEmpty(dr["volume"].ToString()) ? "" : "<tr><th>Volume</th><td>" + dr["volume"].ToString() + "</td></tr>")
                                + (string.IsNullOrEmpty(dr["PublishYear"].ToString()) ? "" : "<tr><th>Publish Year</th><td>" + dr["PublishYear"].ToString() + "</td></tr>")
                                + (string.IsNullOrEmpty(dr["Binding"].ToString()) ? "" : "<tr><th>Binding</th><td>" + dr["Binding"].ToString() + "</td></tr>")   //+ (string.IsNullOrEmpty(dr["AboutProduct"].ToString()) ? "" : "<tr><td colspan='2'><div style='max-height: 25vw; overflow: auto;'>" + dr["AboutProduct"].ToString() + "</div></td></tr>")
                                + "</table>" + "</div>";
            }
            else
            {
                mdb.Name = "-1";
            }
        }
        else
        {
            mdb.Name = "-1";
            mdb.Details = errmsg;
        }
        return mdb;
    }

    protected void btn_signup_Click(object sender, EventArgs e)
    {
        string EmailID = "", errmsg = "";
        EmailID = textEmail.Text;
        DAL dal = new DAL();
        dal.insert_NewsLetterEmail(EmailID, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        textEmail.Text = "";
        if (errmsg == "success")
        {
            Response.Redirect("~/newsletter.aspx");
        }
        else if (errmsg == "EmailID Already Exists !")
        {
            ltr_scripts.Text = "<script>" +
                "toastr.options = {closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000}; " +
                "toastr.options.onclick = function () {};" +
            "toastr.info(' Already Subscribed For News Letter ! ', '');</script>";
            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Already Subscribed For News Letter !');", true);             
        }
        else
        { 
            CommonCode.show_toastr("error", "Error occured while saving SignUp user", errmsg, false, "", false, "", ltr_scripts);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        Session["OtherCountry"] = "INDIA";
        Response.Redirect("/"); 
    }

    protected void btnNot_Click(object sender, EventArgs e)
    { 
            Session["OtherCountry"] = "OtherCountry";
            // Session["OtherCountry"] = "OtherCountry";
            //load_homepage_data();
            Response.Redirect("/");
         
    }

    private void load_TodaySaying()
    {
        DAL dal = new DAL();
        string errmsg = "", MenuID = "";
        DataTable dt = new DataTable();
        dt = dal.get_TodaySaying("0", Session["iCompanyID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["QuoteHeading"].ToString()))
                {
                    todayQuote.InnerHtml = dt.Rows[0]["QuoteHeading"].ToString();
                } 
            } 
        } 
    }

    private void add_item_to_WishList(String ProductID)
    {
        String errmsg = "success";
        String CustID = "";
        if (Session["CustID"] != null)
        {
            CustID = Session["CustID"].ToString();
        }
        String CartID = "";
        errmsg = CommonCode.AddToWishlist(CustID, ProductID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString());
        if (errmsg == "success")
        {
            ltr_scripts.Text = "<script>" + "toastr.options = {closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000}; " +
                "toastr.options.onclick = function () {window.location.replace('Customer/wishlist.aspx');};" +
            "toastr.success('Successfuly added to wishlist ! ', 'wishlist');</script>";
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else if (errmsg == "already exist")
        {
            ltr_scripts.Text = "<script>" +
                "toastr.options = {closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000}; " +
                "toastr.options.onclick = function () {window.location.replace('Customer/wishlist.aspx');};" +
            "toastr.info(' Already in wishlist ! ', 'wishlist');</script>";
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else
        {
            ltr_scripts.Text = "<script>toastr.error('" + errmsg.Replace('\'', ' ') + "', 'WishList Error !');</script>";
            //exception
        }
    }

    protected void btnSearchMore1_Click(object sender, EventArgs e)
    {
        Response.Redirect("search_results.aspx",true);    
    }
    protected void btnSearchMore2_Click(object sender, EventArgs e)
    {
        Response.Redirect("search_results.aspx", true);
    }
    protected void btnSearchMore5_Click(object sender, EventArgs e)
    {
        Response.Redirect("search_results.aspx", true);
    }
    protected void btnSearchMore6_Click(object sender, EventArgs e)
    {
        Response.Redirect("search_results.aspx", true);
    }
    protected void btnSearchMore7_Click(object sender, EventArgs e)
    {
        Response.Redirect("search_results.aspx", true);
    }
    protected void btnSearchMore8_Click(object sender, EventArgs e)
    {
        Response.Redirect("search_results.aspx", true);
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, UseHttpGet = false)]
    public static CartData AddDataintoCart(string ProductID) 
    {
        CartData mdbs  = new CartData();

        String errmsg = "success";
        String CustID = "";
        if (HttpContext.Current.Session["CustID"] != null)
        {
            CustID = HttpContext.Current.Session["CustID"].ToString();
        }
        String CartID = "";
        errmsg = CommonCode.AddToCart(CustID, ProductID, HttpContext.Current.Session["iCompanyId"].ToString(), HttpContext.Current.Session["iBranchID"].ToString(), out CartID);
        if (!string.IsNullOrEmpty(CartID))
        {
            HttpContext.Current.Session["CartID"] = CartID;
        }
        
        double cartQty = 0;
        string errmsgs = "";
        if (HttpContext.Current.Session["CustID"] != null)
        {
            if (HttpContext.Current.Session["CustID"].ToString() == "guest")
            {
                DataTable dt_tmp_cart;
                dt_tmp_cart = (DataTable)HttpContext.Current.Session["tmpcart"];
                foreach (DataRow dr in dt_tmp_cart.Rows)
                {
                    cartQty += Convert.ToDouble(dr[1].ToString());
                }
            }
            else
            {                
                DAL dal = new DAL();
                DataSet ds = new DataSet();
                ds = dal.get_user_and_cart("INDIA", HttpContext.Current.Session["CustID"].ToString(), HttpContext.Current.Session["iCompanyId"].ToString(), HttpContext.Current.Session["iBranchID"].ToString(), out errmsgs);
                if (errmsg == "success")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable tmp_cart = (DataTable)(ds.Tables[1]);
                        if (tmp_cart.Rows.Count > 0)
                        {
                            foreach (DataRow book in (tmp_cart.AsEnumerable()))
                            {
                                try
                                {
                                    cartQty += Convert.ToDouble(book["Qty"].ToString());
                                }
                                catch
                                { }
                            }
                        }
                    }
                }
            }
        }

        StringBuilder strScript = new StringBuilder();
        string htmls = CommonCode.show_toastr_forAjax("success", "Cart", "Successfuly added to cart !", true, "closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000", true, "window.location.replace('Customer/user_cart.aspx');", "");
        
        strScript.Append(@htmls);     
        mdbs.ltrnmsg = htmls;
        mdbs.litmsg = strScript.ToString();
        mdbs.CartItems = cartQty.ToString();
        mdbs.Result =  errmsg;
        return mdbs;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, UseHttpGet = false)]
    public static CartData AddDataInToWishList(string ProductID) 
    {
        CartData mdbs = new CartData(); 
        String errmsg = "";  
        if (HttpContext.Current.Session["CustID"] != null && HttpContext.Current.Session["CustID"].ToString() != "guest")
        {
            errmsg = add_data_to_WishList(ProductID); 
        }
        else
        {
            mdbs.uriDirect = "/Customer/user_login.aspx"; 
        }    

        mdbs.Result = errmsg;
        return mdbs;
    }

    public static string add_data_to_WishList(String ProductID)
    {
        String errmsg = "success";
        String CustID = "";
        if (HttpContext.Current.Session["CustID"] != null)
        {
            CustID = HttpContext.Current.Session["CustID"].ToString();
        } 
        errmsg = CommonCode.AddToWishlist(CustID, ProductID, HttpContext.Current.Session["iCompanyId"].ToString(), HttpContext.Current.Session["iBranchID"].ToString());
        
        return errmsg;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_ItemforShopGrid(string Language, string nextPage, string Previouspage, string sortingAcenDesc, string sortby, string SelectedCategories,
        string SelectedPublisher, string SelectedAuthor, string totPageSize = "25",string queryString = "", string SubjectList = "", string ClassList = "", string MediumList = "")  
    {
        string websearchtype = "";
        string websearchText = queryString;
        string Searchsubjectlist = "";
        string SearchsubjectlistID = SubjectList.Replace("Subject","");
        string OtherCountry = "INDIA";
        string iCompanyID = HttpContext.Current.Session["iCompanyId"].ToString();
        string iBranchID = HttpContext.Current.Session["iBranchID"].ToString();
        string rbPublisherID = SelectedPublisher.Replace("Publisher", "");
        string rbAuthorID = SelectedAuthor.Replace("Author", ""); 
        string rbCategoryID = SelectedCategories.Replace("Category", "");
        string SearchSubsubjectlistID = "";
        string TitleSubCategoryID = "";
        string PublishYear = "";
        string Edition = "0";
        string Binding = "";
        Int64 pageIndex = 1;
        Int64.TryParse(nextPage, out pageIndex);
        Int64 pageSize;
        Int64.TryParse(totPageSize, out pageSize);
        Int64 TotalRecord = 0; 
        string ddlLanguageID = "";
        if (Language == "Nil")
        {
            ddlLanguageID = "";
        }
        else
        {
            ddlLanguageID = Language;
        }
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_searchdatabyUsers_Pagination(websearchtype, websearchText, Searchsubjectlist, SearchsubjectlistID, OtherCountry, iCompanyID, iBranchID,
            rbPublisherID, rbAuthorID, ddlLanguageID, rbCategoryID, SearchSubsubjectlistID, TitleSubCategoryID, PublishYear, Edition, Binding, pageIndex,
            pageSize, TotalRecord, sortby.ToLower(), sortingAcenDesc.ToLower(), ClassList.Replace("Class", ""), MediumList.Replace("Medium",""), out  errmsg);


        if (websearchText != "" || SearchsubjectlistID != "" || rbCategoryID != "" || rbPublisherID != "" || ClassList.Replace("Class", "") != "" || MediumList.Replace("Medium", "") != "")
        {
            DataView dtnewdataview = new DataView(dt.DefaultView.ToTable("Publishertbl", true, "PublisherID", "PublisherName"));
            HttpContext.Current.Session["Publisher"] = dtnewdataview;

            DataView dtnewdataviewCategory = new DataView(dt.DefaultView.ToTable("Categorytbl", true, "BookCategoryID", "BookCategoryDesc"));
            dtnewdataviewCategory.RowFilter = "isnull(BookCategoryID,'') <>''";
            HttpContext.Current.Session["Category"] = dtnewdataviewCategory;

            DataView dtnewdataviewSubject = new DataView(dt.DefaultView.ToTable("Subjecttbl", true, "DisplaySubject"));
            HttpContext.Current.Session["Subject"] = dtnewdataviewSubject;

            DataView dtnewdataviewClass = new DataView(dt.DefaultView.ToTable("Classtbl", true, "ClassID", "ClassDesc"));
            dtnewdataviewClass.RowFilter = "isnull(ClassID,'') <>''";
            HttpContext.Current.Session["Class"] = dtnewdataviewClass;

            DataView dtnewdataviewMedium = new DataView(dt.DefaultView.ToTable("Mediumtbl", true, "MediumID", "MediumDesc"));
            dtnewdataviewMedium.RowFilter = "isnull(MediumID,'') <>''";
            HttpContext.Current.Session["Medium"] = dtnewdataviewMedium;
        }
        else
        {
            HttpContext.Current.Session["Publisher"] = null;
            HttpContext.Current.Session["Category"] = null;
            HttpContext.Current.Session["Subject"] = null;
            HttpContext.Current.Session["Class"] = null;
            HttpContext.Current.Session["Medium"] = null;
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }

            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_Categories() 
    {
        string iCompanyID = HttpContext.Current.Session["iCompanyId"].ToString();
        string iBranchID = HttpContext.Current.Session["iBranchID"].ToString();
        DataTable dt_slider1 = new DataTable();
        string errmsg;
        if (HttpContext.Current.Session["Category"] == null)
        {
            SqlCommand com = new SqlCommand("web_GetBookCategory", CommonCode.con);
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.CommandType = CommandType.StoredProcedure; 
            dt_slider1 = CommonCode.getData(com, out errmsg); 
        }
        else
        {
            DataView dtview = (DataView)HttpContext.Current.Session["Category"];
            dt_slider1 = dtview.ToTable();
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt_slider1.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt_slider1.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_Publisher()
    {
        string iCompanyID = HttpContext.Current.Session["iCompanyId"].ToString();
        string iBranchID = HttpContext.Current.Session["iBranchID"].ToString();
        DataTable dt_slider1 = new DataTable();
        string errmsg;
        if (HttpContext.Current.Session["Publisher"] == null)
        {
            SqlCommand com = new SqlCommand("web_GetPublishers", CommonCode.con);
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.CommandType = CommandType.StoredProcedure; 
            dt_slider1 = CommonCode.getData(com, out errmsg);
        }
        else
        {
            DataView dtview = (DataView)HttpContext.Current.Session["Publisher"];
            dt_slider1 = dtview.ToTable();
        }



        






        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt_slider1.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt_slider1.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    } 

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_Author()
    {  
        string iCompanyID = HttpContext.Current.Session["iCompanyId"].ToString();
        string iBranchID = HttpContext.Current.Session["iBranchID"].ToString();

        SqlCommand com = new SqlCommand("web_GetAuthors", CommonCode.con);
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
        com.CommandType = CommandType.StoredProcedure;
        DataTable dt_slider1 = new DataTable();
        string errmsg;
        dt_slider1 = CommonCode.getData(com, out errmsg);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt_slider1.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt_slider1.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);

    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_Language()
    {
        string iCompanyID = HttpContext.Current.Session["iCompanyId"].ToString();
        string iBranchID = HttpContext.Current.Session["iBranchID"].ToString();

        SqlCommand com = new SqlCommand("web_GetMasterLanguage", CommonCode.con);
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
        com.CommandType = CommandType.StoredProcedure;
        DataTable dt_slider1 = new DataTable();
        string errmsg;
        dt_slider1 = CommonCode.getData(com, out errmsg);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt_slider1.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt_slider1.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_MainMenu() 
    {
        string iCompanyID = HttpContext.Current.Session["iCompanyId"].ToString();
        string iBranchID = HttpContext.Current.Session["iBranchID"].ToString();

        DAL dal = new DAL();
        string errmsg = "";
        DataTable dt_slider1 = new DataTable();
        dt_slider1 = dal.get_menu_items("0", iCompanyID, out errmsg); 

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt_slider1.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt_slider1.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string WEB_load_FooterContent(string ParentID = "0", string Action = "0") 
    {
        string iCompanyID = HttpContext.Current.Session["iCompanyId"].ToString();
        string iBranchID = HttpContext.Current.Session["iBranchID"].ToString();
        int  action= 0; 
        Int32.TryParse(Action, out action); 
        DAL dal = new DAL();
        string errmsg = "";
        DataTable dt_slider1 = new DataTable();
        dt_slider1 = dal.get_topics(ParentID, action, out errmsg);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        foreach (DataRow dr in dt_slider1.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt_slider1.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static CartData WEB_CLearSearchSession()
    {
        CartData mdbs = new CartData();  
        HttpContext.Current.Session["Publisher"] = null;
        HttpContext.Current.Session["Category"] = null;
        HttpContext.Current.Session["Subject"] = null;
        HttpContext.Current.Session["Class"] = null;
        HttpContext.Current.Session["Medium"] = null;

        mdbs.Result = "Session Removed";

        return mdbs;
    }

}
public class CartData
{
    public string Result { get; set; }
    public string CartItems { get; set; } 
    public string ltrnmsg { get; set; }
    public string litmsg { get; set; }
    public string uriDirect { get; set; }
}
