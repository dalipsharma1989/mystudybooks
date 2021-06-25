using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        ltr_master_scripts.Text = "";
        try
        {
            if (Session["iCompanyId"] != null)
            {
                //Do Nothing
            }
            else
            {
                SetCompanyID_BranchIdInSession();
            }
            //load_BookLanguage();            
            if (Session["CustID"] != null)
            {
                //Do Nothing
            }
            else
            {
                Session["CustID"] = "guest";
                Session["CustName"] = "Guest";
            }
            load_Header();
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["OtherCountry"] == null)
            {
                Session["OtherCountry"] = "INDIA";
                Response.Redirect("/");
            }

            //if (Session["iCompanyId"] != null)
            //{
            //    //Do Nothing
            //}
            //else
            //{
            //    SetCompanyID_BranchIdInSession();
            //}
            //if (!(Session["CustID"] == null || Session["CustID"].ToString() == "guest"))
            //{
            //    drp_Region.Visible = false;
            //}
            //if (Session["OtherCountry"] != null && Session["OtherCountry"].ToString() == "INDIA")
            //{
            //    header_Country.Text = "India";
            //    //header_Country_Flag.ImageUrl = "img/India.png";
            //}
            //else
            //{
            //    header_Country.Text = "Other";
            //    //header_Country_Flag.ImageUrl = "img/World.png";
            //}
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }        
    }


    private void SetCompanyID_BranchIdInSession()
    {
        DataTable dt = new DataTable();
        DAL dl = new DAL();
        string errmsg = "";
        dt = dl.ICompanyID_BranchID(out errmsg);
        if (dt.Rows.Count > 0)
        {
            Session["iCompanyId"] = dt.Rows[0]["iCompanyId"].ToString();
            Session["iBranchID"] = dt.Rows[0]["iBranchID"].ToString();
            Session["FinancialPeriod"] = dt.Rows[0]["FinancialPeriod"].ToString();
        }
    }

    private void load_BookLanguage()
    {
        SqlCommand com = new SqlCommand("web_GetMasterLanguage", CommonCode.con);
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
                //rp_LoadLanguage.DataSource = dt;
                //rp_LoadLanguage.DataBind();
                //rp_LoadLanguage_mobile.DataSource = dt;
                //rp_LoadLanguage_mobile.DataBind();
            }
        }
        else
        {
             //CommonCode.show_alert("error", "Error", errmsg, ,false, "", false, "", ph_msg);
        }
    }

    private void load_all_Catalogue()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("FilePath", typeof(String)), new DataColumn("FileName", typeof(String)), new DataColumn("DateCreated", typeof(DateTime))
            , new DataColumn("IconPath", typeof(String)), new DataColumn("Downloadname", typeof(String))
        });

        string[] filePaths = Directory.GetFiles(Server.MapPath("/Catalogue/"));
        List<ListItem> files = new List<ListItem>();
        foreach (string filePath in filePaths)
        {
            string fileName = Path.GetFileName(filePath);
            if (Path.GetExtension(filePath) == ".pdf" || Path.GetExtension(filePath) == ".xls" || Path.GetExtension(filePath) == ".xlsx")
            {
                if (Path.GetExtension(filePath) == ".pdf")
                {
                    if (Path.GetFileName(filePath).Replace(".pdf", "") == "Catalogue")
                    {
                        dt.Rows.Add("../Catalogue/" + Path.GetFileName(filePath), Path.GetFileName(filePath), File.GetCreationTime(filePath), "../Catalogue/" + Path.GetFileName("../Catalogue/PDF.jpg"), "Download Catalogue");
                    }
                    else if (Path.GetFileName(filePath).Replace(".pdf", "") == "checklist_pdf")
                    {
                        dt.Rows.Add("../Catalogue/" + Path.GetFileName(filePath), Path.GetFileName(filePath), File.GetCreationTime(filePath), "../Catalogue/" + Path.GetFileName("../Catalogue/PDF.jpg"), "Download Checklist PDF");
                    }
                    else if (Path.GetFileName(filePath).Replace(".pdf", "") == "Polytechnic_Catalogue")
                    {
                        dt.Rows.Add("../Catalogue/" + Path.GetFileName(filePath), Path.GetFileName(filePath), File.GetCreationTime(filePath), "../Catalogue/" + Path.GetFileName("../Catalogue/PDF.jpg"), "Download Polytechnic Catalogue");
                    }


                }
                else
                {
                    if (Path.GetFileName(filePath).Replace(".xls", "") == "checklist_excel" || Path.GetFileName(filePath).Replace(".xlsx", "") == "checklist_excel")
                    {
                        dt.Rows.Add("../Catalogue/" + Path.GetFileName(filePath), Path.GetFileName(filePath), File.GetCreationTime(filePath), "../Catalogue/" + Path.GetFileName("/Catalogue/excel.jpg"), "Download Checklist Excel");
                    }

                }

            }

        }
        if (dt.Rows.Count > 0)
        {
            //rp_Catalogue.DataSource = dt;
            //rp_Catalogue.DataBind();
            //rp_CatalogueHome.DataSource = dt;
            //rp_CatalogueHome.DataBind();
        }
    } 

    public void load_Header()
    {
        string str_cart = "";
        string kop_cart = "";
        string qty = "";
        str_cart = "<li class=''><a href='Customer/user_cart.aspx'><i class='fa fa-shopping-cart'></i>&nbsp;Cart</a></li>";

        string str_account = "";
        str_account += "<li class='dropdown'>" + Environment.NewLine;
        str_account += "    <a  class='dropdown-toggle' data-toggle='dropdown'>Account &nbsp;</a>" + Environment.NewLine;
        str_account += "    <ul class='dropdown-menu' role='menu' aria-labelledby='a_my_account' style='margin-left: -60px;font-size: 20px;'>" + Environment.NewLine;
        str_account += "        <li><a href='Customer/user_login.aspx'><i class='fa fa-sign-in'></i>&nbsp;Login/Sign Up</a></li>" + Environment.NewLine;
        str_account += "    </ul>" + Environment.NewLine;
        str_account += "</li>" + Environment.NewLine;
        ul_account.InnerHtml = str_account + str_cart;
        ulaccountmobile.InnerHtml = str_account + str_cart;
        //load_main_menu();
        //load_ExamNotification();
        //load_main_SubjectandSubject();
        if (Session["CustID"] != null)
        {
            if (Session["CustID"].ToString() == "guest")
            {
                load_temp_cart(out str_cart, out str_account, out kop_cart, out qty);
                //ul_account.InnerHtml = str_account + str_cart;
                ul_account.InnerHtml = str_account;
                ulaccountmobile.InnerHtml = str_account;
                //mini_cart_sub.InnerHtml = kop_cart;
                cart_qty.InnerText = qty;
                Span1.InnerText = qty;
            }
            else
            {
                load_user_and_cart(out str_cart, out str_account, out kop_cart, out qty);
                ul_account.InnerHtml = str_account;
                ulaccountmobile.InnerHtml = str_account;
                //mini_cart_sub.InnerHtml = kop_cart;
                cart_qty.InnerText = qty;
                Span1.InnerText = qty;
            }
        }
    } 

    public void load_temp_cart(out string str_cart, out string str_account, out string kop_cart, out string qty)
    {
        kop_cart = "";
        qty = "";
        String OtherCountry = "";
        if (Session["OtherCountry"] != null)
        {
            OtherCountry = Session["OtherCountry"].ToString();
        }
        else
        {
            OtherCountry = "";
        }
        str_cart = "<li class=''><a href='Customer/user_cart.aspx' style='display:none;' class='atagmenu'><i class='fa fa-shopping-cart'></i>&nbsp;Cart</a></li>";
        str_account = "<li class=''><a href='search_results.aspx'  class='atagmenu'><i class='fa fa-search'></i>&nbsp;Search all Books</a></li>";
        str_account += "        <li><a href='Customer/user_login.aspx' class='atagmenu'><i class='fa fa-sign-in' ></i>&nbsp;Login/Sign Up</a></li>";
        //str_account += "<li class='dropdown'>" + Environment.NewLine;
        //str_account += "    <a  class='dropdown-toggle' data-toggle='dropdown'>&nbsp;<i class='fa fa-user' ></i>&nbsp;My Account</a>" + Environment.NewLine;
        //str_account += "    <ul class='dropdown-menu  login-dropdown' role='menu' aria-labelledby='a_my_account' >" + Environment.NewLine;
        //str_account += "        <li><a href='Customer/user_login.aspx'><i class='fa fa-sign-in' ></i>&nbsp;Login/Sign Up</a></li>" + Environment.NewLine;
        //str_account += "    </ul>" + Environment.NewLine;
        //str_account += "</li>" + Environment.NewLine;
        str_account += "<li class=''><a href='Customer/register.aspx' style='display:block;' class='atagmenu'><i class='fa fa-user-plus' aria-hidden='true'></i>&nbsp;New User</a></li>" + Environment.NewLine;

        string path = "";
        if (System.IO.File.Exists(Server.MapPath("index.aspx")))
            path = "Customer/";
        //string kop_cart = "";
        DataTable dt_tmp_cart = new DataTable();
        if (Session["tmpcart"] != null)
        {
            dt_tmp_cart = (DataTable)Session["tmpcart"];
            if (dt_tmp_cart.Rows.Count > 0)
            {
                kop_cart = "<div class='cart-product' style='max-height: 50vh; overflow: auto;'>";
                double totprice = 0.0 , TotalNetAmt = 0.0, cartQty=0;
                foreach (DataRow dr in dt_tmp_cart.Rows)
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        string errmsg;
                        SqlCommand com = new SqlCommand("Web_get_single_book_details", CommonCode.con);
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.Add("@ProductID", SqlDbType.VarChar, 30).Value = dr[0].ToString();
                        com.Parameters.Add("@OtherCountry", SqlDbType.VarChar , 100).Value = OtherCountry;
                        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyID"].ToString();
                        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
                        dt = CommonCode.getData(com, out errmsg);
                        DataRow book = dt.Rows[0];
                        try
                        {
                            double price = Convert.ToDouble(book["SalePrice"].ToString());
                            string filepath = "resources/product/" + book["ISBN"].ToString() + ".jpg";
                            string name = book["BookName"].ToString();
                            totprice += price * Convert.ToDouble(dr[1].ToString());
                            cartQty += Convert.ToDouble(dr[1].ToString());
                            TotalNetAmt += Convert.ToDouble(book["TotalPrice"].ToString()) * Convert.ToDouble(dr[1].ToString());

                            string price_to_display = dr[1].ToString() + "  x  " + book["SaleCurrency"].ToString() + " " + Convert.ToString(price);
                            string cart_item = "<div class='single-cart'> <div class='cart-img'> <a href = '#' ><img src='" + filepath + "' onError=this.onerror = null; this.src = '../resources/no-image.jpg';  alt = '"+ name + "' /></a></div>";
                            cart_item += "<div class='cart-info' style='overflow: hidden; float: right;width: 100%;'> <h5 style='height: auto; text-overflow: ellipsis;text-align: right;'> <a href = 'view_book.aspx?productid=";
                            cart_item += dr[0].ToString() + "'>" + name + "</a></h5> <p style='float: right;'> " + price_to_display + "</p> </div></div> ";
                            kop_cart += cart_item; 
                        }
                        catch
                        { }
                    }
                    catch
                    { }
                }
                kop_cart += "</div><div class='cart-totals'><h5 style='font-size: 20px;font-weight: 800;'>Total <label style='float: right; margin-right: 10px; '>" + totprice.ToString() + "</label></h5></div>";
                //kop_cart += "<div class='cart-bottom' style='font-size: 20px;font-weight: 800;'> <a class='view-cart a' href='Customer/user_cart.aspx'>view cart </a><br/><div style='padding-top: 10px;'></div><a class='view-cart a' href = 'Customer/proceed_to_checkout.aspx' > Check out</a> </div>";
                kop_cart += "<div class='cart-bottom' style='font-size: 20px;font-weight: 800;'> <a class='view-cart a' href='Customer/user_cart.aspx'>view cart </a><br/><div style='padding-top: 10px;'></div></div>";
                str_cart = "<li class=''><a href='" + path + "user_cart.aspx'><i class='fa fa-shopping-cart'></i>&nbsp;Cart&nbsp;<span class='badge'>" + dt_tmp_cart.Rows.Count + "</span></a></li>";
                qty = cartQty.ToString(); //dt_tmp_cart.Rows.Count.ToString();
                cart_Price.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + TotalNetAmt.ToString();
                Span2.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + TotalNetAmt.ToString();
                //cart_Price.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + ds.Tables[1].Rows[0]["NetAmount"].ToString();

            }
            else
            {
                kop_cart = "<b style='text-align: center;'> Cart is Empty </b>";
                qty = "0";
                str_cart = "<li class=''><a href='" + path + "user_cart.aspx'><i class='fa fa-shopping-cart'></i>&nbsp;Cart</a></li>";
            }
        }
        else
        {
            kop_cart = "<b style='text-align: center; width: 100%;'> Cart is Empty </b>";
            qty = "0";
            cart_Price.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " 0.00";
            Span2.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " 0.00";
        }
    }

    public void load_main_menu()
    {
        DAL dal = new DAL();
        string errmsg = "";
        DataTable dt = new DataTable();
        dt = dal.get_menu_items("0",Session["iCompanyID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            //if (dt.Rows.Count > 0)
            //{
            //    rp_main_menu.DataSource = dt;
            //    rp_main_menu.DataBind();
            //    rp_main_menu_mobile.DataSource = dt;
            //    rp_main_menu_mobile.DataBind(); 
            //}
            //else
            //{
            //    rp_main_menu.DataSource = null;
            //    rp_main_menu.DataBind();
            //    rp_main_menu_mobile.DataSource = null;
            //    rp_main_menu_mobile.DataBind();
                 
            //}
        }
        else
        {
            //CommonCode.show_alert("danger", "Error while loading menu items !", errmsg, );
        }
    }

    public void load_main_SubjectandSubject(string SubjectID = "")
    {
        DAL dal = new DAL();
        string errmsg = "";
        DataTable dt = new DataTable();
        dt = dal.get_Subjectsubject(SubjectID, Session["iCompanyID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            //if (dt.Rows.Count > 0)
            //{   
            //    if (SubjectID == "")
            //    {
            //        rp_loadSubject.DataSource = dt;
            //        rp_loadSubject.DataBind();
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "", "openNav();", true);
            //        rp_loadSubSubject.DataSource = dt;
            //        rp_loadSubSubject.DataBind();
            //    }
            //}
            //else
            //{
            //    rp_loadSubject.DataSource = null;
            //    rp_loadSubject.DataBind();
            //    rp_loadSubSubject.DataSource = null;
            //    rp_loadSubSubject.DataBind();
            //}
        }
        else
        {
            //CommonCode.show_alert("danger", "Error while loading menu items !", errmsg, );
        }
    }
     
    public void load_user_and_cart(out string str_cart, out string str_account, out string kop_cart, out string qty)
    {
        kop_cart = "";
        qty = "";
        String OtherCountry = "";
        if (Session["OtherCountry"] != null)
        {
            OtherCountry = Session["OtherCountry"].ToString();
        }
        else
        {
            OtherCountry = "";
        }

        str_cart = "<li class=''><a href='Customer/user_cart.aspx'><i class='fa fa-shopping-cart'></i>&nbsp;Cart</a></li>";
        str_account = "<li class=''><a href='search_results.aspx' ><i class='fa fa-search'></i>&nbsp;Search all Books</a></li>";
        str_account += "<li class='dropdown'>" + Environment.NewLine;
        str_account += "    <a  class='dropdown-toggle' data-toggle='dropdown'>Account &nbsp;</a>" + Environment.NewLine;
        str_account += "    <ul class='dropdown-menu' role='menu' aria-labelledby='a_my_account' style='margin-left: -60px;font-size: 20px;'>" + Environment.NewLine;
        str_account += "        <li><a href='Customer/user_login.aspx'><i class='fa fa-sign-in'></i>&nbsp;Login/Sign Up</a></li>" + Environment.NewLine;
        str_account += "    </ul>" + Environment.NewLine;
        str_account += "</li>" + Environment.NewLine;
        str_account += "<li class=''><a href='Customer/register.aspx' style='display:block;'><i class='fa fa-user-plus' aria-hidden='true'></i>&nbsp;New User</a></li>" + Environment.NewLine;
        string errmsg = "";
        DAL dal = new DAL();
        DataSet ds = new DataSet(); 
        ds = dal.get_user_and_cart(OtherCountry, Session["CustID"].ToString(),Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string path = "";
                if (System.IO.File.Exists(Server.MapPath("index.aspx")))
                    path = "Customer/";

                Session["CustName"] = ds.Tables[0].Rows[0]["CustName"].ToString();
                DataTable tmp_cart = (DataTable)(ds.Tables[1]);
                if (tmp_cart.Rows.Count > 0)
                {
                    kop_cart = "<div class='cart-product' style='max-height: 50vh; overflow: auto;'>";
                    double totprice = 0.0, totqty = 0;

                    foreach (DataRow book in (tmp_cart.AsEnumerable()))
                    {
                        try
                        {
                            double price = Convert.ToDouble(book["Price"].ToString());
                            string filepath = "resources/product/" + book["ISBN"].ToString() + ".jpg";
                            string name = book["BookName"].ToString();
                            totprice += price * Convert.ToDouble(book["Qty"].ToString());
                            string price_to_display = book["Qty"].ToString() + "  x  " + book["SaleCurrency"].ToString() + " " + Convert.ToString(price);
                            string cart_item = "<div class='single-cart'> <div class='cart-img'> <a href = '#' ><img src='" + filepath + "' onError=this.onerror=null;this.src='../resources/no-image.jpg'; alt = '";
                            cart_item += name + "' /></a> </div>  <div class='cart-info' style='overflow: hidden; float: right;width: 100%;'> <h5 style='height: auto; text-overflow: ellipsis;text-align: right;'> <a href = 'view_book.aspx?productid=";
                            cart_item += book["ProductId"] + "'> " + name + "</a></h5> <p style='float: right;'> " + price_to_display + "</p> </div></div> ";
                            kop_cart += cart_item;
                            totqty += Convert.ToDouble(book["Qty"].ToString());
                        }
                        catch
                        { }
                    }
                    kop_cart += "</div><div class='cart-totals'><h5 style='font-size: 20px;font-weight: 800;'>Total <label style='float: right; margin-right: 10px; '>" + totprice.ToString() + "</label></h5></div>";
                    //kop_cart += "<div class='cart-bottom' style='font-size: 20px;font-weight: 800;'> <a class='view-cart a' href='Customer/user_cart.aspx'>view cart </a><br/><div style='padding-top: 10px;'></div><a class='view-cart a' href = 'Customer/proceed_to_checkout.aspx' > Check out</a> </div>";
                    kop_cart += "<div class='cart-bottom' style='font-size: 20px;font-weight: 800;'> <a class='view-cart a' href='Customer/user_cart.aspx'>view cart </a><br/><div style='padding-top: 10px;'></div></div>";
                    str_cart = "<li class=''><a href='" + path + "user_cart.aspx'><i class='fa fa-shopping-cart'></i>&nbsp;Cart&nbsp;<span class='badge'>" + tmp_cart.Rows.Count + "</span></a></li>";
                    qty = totqty.ToString(); //tmp_cart.Rows.Count.ToString();
                    cart_Price.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() +" "+ (Convert.ToDouble(ds.Tables[1].Rows[0]["NetAmount"].ToString()) - Convert.ToDouble(ds.Tables[1].Rows[0]["Shipcost"].ToString()));
                    Span2.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + (Convert.ToDouble(ds.Tables[1].Rows[0]["NetAmount"].ToString()) - Convert.ToDouble(ds.Tables[1].Rows[0]["Shipcost"].ToString())) ;
                }
                else
                {
                    kop_cart = "<b style='text-align: center;'> Cart is Empty </b>";
                    qty = "0";
                    str_cart = "<li class=''><a href='" + path + "user_cart.aspx'><i class='fa fa-shopping-cart'></i>&nbsp;Cart</a></li>";
                }
                //  style = 'display: inline-flex;' > style='margin-left: -60px;font-size: 20px;' < span style = 'font-size: 20px; margin-left: -20px; margin-top: 30px;width:165px;' > &nbsp; Login </ span >
                str_account = "<li class=''><a href='search_results.aspx' ><i class='fa fa-search'></i>&nbsp;Search all Books</a></li>";
                str_account += "<li class='dropdown'>" + Environment.NewLine;
                str_account += "    <a class='dropdown-toggle collapsible-1' data-toggle='dropdown'  > Hi " + Session["CustName"] + "" + "</a>" + Environment.NewLine;
                //str_account += "     <div class='dropdown-menu' aria-labelledby='dropdownMenuLink'></div>" +
                //                    "<ul class='dropdown-menu content-1' role='menu' aria-labelledby='a_my_account' style='font-size: 15px;'>" + Environment.NewLine;
                //str_account += "<ul class='dropdown-menu content-1' role='menu' aria-labelledby='a_my_account' style='font-size: 15px;'>" + Environment.NewLine;
                //str_account += "<li><a href='" + path + "profile.aspx?Type=A'><i class='fa fa-user' ></i>&nbsp; Profile</a></li>" +
                //    "<li><a href='" + path + "profile.aspx?Type=P'><i class='fa fa-key' ></i>&nbsp;Change Password</a></li>" +
                //    "<li><a href='" + path + "wishlist.aspx'><i class='fa fa-heart'></i>&nbsp;Wishlist</a></li>" +
                //    "<li><a href='" + path + "order_history.aspx'><i class='fa fa-history' ></i>&nbsp;Order History</a></li>" +
                //    "<li><a href='" + path + "logout.aspx'><i class='fa fa-sign-out' ></i>&nbsp;Logout</a></li>";
                str_account += "    <ul class='dropdown-menu content-1' role='menu' aria-labelledby='a_my_account' style='font-size: 15px;'>" + Environment.NewLine;
                str_account += "" +
                    "<li><a href='" + path + "profile.aspx?Type=A'><i class='fa fa-user' ></i>&nbsp; Profile</a></li>" +
                    "<li><a href='" + path + "profile.aspx?Type=P'><i class='fa fa-key' ></i>&nbsp;Change Password</a></li>" +
                    "<li><a href='" + path + "wishlist.aspx'><i class='fa fa-heart' ></i>&nbsp;Wishlist</a></li>" +
                    "<li><a href='" + path + "order_history.aspx'><i class='fa fa-history'  ></i>&nbsp;Order History</a></li>" +
                    "<li><a href='" + path + "logout.aspx'><i class='fa fa-sign-out'  ></i>&nbsp;Logout</a></li>";
                str_account += "    </ul>" + Environment.NewLine;
                str_account += "</li>" + Environment.NewLine;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Error while loading user and cart details');", true);
            //CommonCode.show_alert("danger", "Error while loading user and cart details", errmsg, ph_msg);
        }
    }

    protected void NowIndiaSelected(object sender, EventArgs e)
    {
        Session["OtherCountry"] = "INDIA";
        //Response.Redirect("/");
    }
    protected void NowOtherSelected(object sender, EventArgs e)
    {
        Session["OtherCountry"] = "OtherCountry";
       // Response.Redirect("/");
    }


    protected void rp_loadSubject_ItemCommand(object source, RepeaterCommandEventArgs e)
    { 
        load_main_SubjectandSubject(e.CommandArgument.ToString());
    }

    private void load_ExamNotification()
    {
        DAL dal = new DAL();
        string errmsg = "";
        DataTable dt = new DataTable();
        dt = dal.get_ExamsNotifications("0", Session["iCompanyID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            //if (dt.Rows.Count > 0)
            //{
            //    rp_ExamNotification.DataSource = dt;
            //    rp_ExamNotification.DataBind();
            //}
            //else
            //{
            //    rp_ExamNotification.DataSource = null;
            //    rp_ExamNotification.DataBind();
            //}
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Today Saying !", errmsg, ph_msg);
        }
    }

}