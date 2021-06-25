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
using System.Web.Services;
using System.Configuration;
using System.Text;
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

    protected void Page_Init(object sender, EventArgs e)
    {
        

    }

    protected void Page_Load(object sender, EventArgs e)
    { 
        this.Title = CommonCode.SetPageTitle("Search you Books"); 
        ltr_scripts.Text = ""; 
        if (!IsPostBack)
        {
            string search_string = "", option_name = "",Category="",Language="";
            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                option_name = "Search All";
                search_string = Request.QueryString["q"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["author"]))
            {
                option_name = "Author";
                search_string = Request.QueryString["author"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Cat"]))
            {
                option_name = "Categories";
                Category = Request.QueryString["Cat"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["langid"]))
            {
                option_name = "Language";
                Language = Request.QueryString["langid"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["publisher"]))
            {
                option_name = "Publisher";
                search_string = Request.QueryString["publisher"];
            }
             
        }
    }
       
      
    protected void rp_bind_books_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String ProductID = e.CommandArgument.ToString();
        if (e.CommandName == "addtocart")
        {
            add_item_to_cart(ProductID);
        }
        else if (e.CommandName == "addtowishlist")
        {
            string wishlist_msg = "";
            if (Session["CustID"] != null)
            {
                wishlist_msg = CommonCode.AddToWishlist(Session["CustID"] + "", ProductID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString() );
            }
            else
            {
                wishlist_msg = CommonCode.AddToWishlist("guest", ProductID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString());
            }

            if (wishlist_msg == "success")
            {
                CommonCode.show_toastr("success", "Wishlist", "Product added to Wishlist !", true, "closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000", true, "window.location.replace('Customer/wishlist.aspx');", ltr_scripts);
            }
            else if (wishlist_msg == "Product is already in your wishlist !")
            {
                CommonCode.show_toastr("info", "Wishlist", "Product is already in your wishlist !", true, "closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000", true, "window.location.replace('Customer/wishlist.aspx');", ltr_scripts);
            }
            else
            {
                CommonCode.show_toastr("error", "Wishlist", wishlist_msg, false, "", false, "", ltr_scripts);
            }
        } 
    }

    private void add_item_to_cart(String ProductID)
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
            CommonCode.show_toastr("success", "Cart", "Successfuly added to cart !", true, "closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:5000",
                                        true, "window.location.replace('Customer/user_cart.aspx');", ltrMsg);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Successfuly added to cart !');", true);
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else
        {
            CommonCode.show_toastr("error", "Cart Error !", errmsg.Replace('\'', ' '), true, "closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000",
                                        true, "window.location.replace('Customer/user_cart.aspx');", ltrMsg);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('"+ errmsg.Replace('\'', ' ') + "');", true);
        } 
    }

    protected void lstView_NeedDataSource(object source, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
       // RadListView1.DataSource = rtnDataBooks("", "");
        //RadListView2.DataSource = rtnDataBooks("", "");
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
                ddl_Language.Items.Add(new ListItem("All Titles", "Nil"));
                foreach (DataRow rr in dt.Rows)
                {
                    ddl_Language.Items.Add(new ListItem(rr[1].ToString(), rr[0].ToString()));
                } 
            }
        }
        else
        {
            //CommonCode.show_alert("error", "Error", errmsg, ,false, "", false, "", ph_msg);
        }
    }

    
    [WebMethod]
    public static string[] GetDataList(string prefix, string cmpOption, string rb_SubjectID, string cmbcategory)
    {
        string filterType = ""; string filterTxt = "", CategoryID = "";
        List<string> Products = new List<string>();
 
        if (cmpOption.ToUpper() == "NONE" & string.IsNullOrEmpty(filterType.Trim()) & string.IsNullOrEmpty(filterTxt.Trim()))
        {           }
        else
        {
            if (string.IsNullOrEmpty(filterType.Trim()))
            {
                if (cmbcategory.ToUpper() == "NIL")
                {
                    filterType = "ALL"; 
                }
                else
                {
                    filterType = cmpOption.ToUpper(); 
                }
            }

            if (string.IsNullOrEmpty(filterTxt.Trim()))
            {
                filterTxt = prefix;
            } 
        }

        if (prefix != null)
        {
            prefix = prefix.Replace("-", "[-]");
        }

        DAL dal = new DAL();
        String errmsg = "";
        DataTable dt = new DataTable();

        dt = dal.get_custom_searchdatabyUsers(filterType.Trim(),prefix, HttpContext.Current.Session["OtherCountry"].ToString(), HttpContext.Current.Session["iCompanyId"].ToString(), HttpContext.Current.Session["iBranchID"].ToString(), cmbcategory.Trim(), out errmsg);

        if (!string.IsNullOrEmpty(prefix))
        {
            foreach(DataRow dr in dt.Rows)
            {
                string []arr = dr["BookCode"].ToString().Split(' '); 

                var ImageContent = "<a><div><span class='SearchISBN' id='search_ISBN' >" + arr[0].ToString() + "</span>" + dr["BookName"].ToString().Replace(arr[0].ToString(), "") + "</div></a>";

                Products.Add(string.Format("{0}|{1}", dr["BookCode"].ToString() , ImageContent.ToString()));
            }
            
        }
             
        return Products.ToArray();
    }
      
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public static ModalBookData GetData(string ProductID)
    {
        ModalBookData mdb = new ModalBookData();
        string errmsg = "";
        DAL dal = new DAL();
        //DataSet ds = dal.get_curr_book_details( ProductID, out errmsg);
        DataSet ds = new DataSet();
        if (HttpContext.Current.Session["OtherCountry"] != null && HttpContext.Current.Session["OtherCountry"].ToString() == "OtherCountry")
            ds = dal.get_curr_book_details("OtherCountry", ProductID, HttpContext.Current.Session["iCompanyID"].ToString(), HttpContext.Current.Session["iBranchID"].ToString(), out errmsg);
        else
            ds = dal.get_curr_book_details("", ProductID, HttpContext.Current.Session["iCompanyID"].ToString(), HttpContext.Current.Session["iBranchID"].ToString(), out errmsg);


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
                                "<tr>" + "<th>" + "ISBN" + "</th>" + "<td>" + dr["ISBN"].ToString() + "</td>" + "</tr>"
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
    public void delete_wishlist(String ProductID)
    {
        String errmsg = "success";

        if (ProductID == "")
        {
            SqlCommand com = new SqlCommand("dbo_delete_item_from_Wishlist", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = Session["CustID"].ToString();
            com.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = 0;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                 
                ltr_scripts.Text = "<script> $(function(){ toastr.success('Items Removed from Wishlist ! ', 'Item Removed');});</script>";
            }
            else
            {
                ltr_scripts.Text = "<script> $(function(){ toastr.error('" + errmsg.Replace('\'', ' ') + "', 'Wishlist Error !');});</script>";
            }
        }
        else
        {
            SqlCommand com = new SqlCommand("dbo_delete_item_from_Wishlist", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = Session["CustID"].ToString();
            com.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = ProductID;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            { 
                ltr_scripts.Text = "<script> $(function(){ toastr.success('Item Removed from Wishlist ! ', 'Item Removed');});</script>";
            }
            else
            {
                ltr_scripts.Text = "<script> $(function(){ toastr.error('" + errmsg.Replace('\'', ' ') + "', 'Wishlist Error !');});</script>";
            }
        }


    }

    protected void rp_wishlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String ProductID = e.CommandArgument.ToString();
        if (e.CommandName == "add")
        {
            add_item_to_cart(ProductID);
            if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
            {
                Response.Redirect("/Customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
            }
            else
            {
                add_item_to_cart(ProductID);
                 
            }
            Master.load_Header();
        }
        else if (e.CommandName == "remove")
        {
            if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
            {
                Response.Redirect("/Customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
            }
            else
            {
                delete_wishlist(ProductID); 
            }
        }
    }

    // ******************* Updated : 02/07/2019 *******************//
    // ****** Added Add to Cart on List Views on Left pane  *******//

    protected void RAD_List_View1_command(object sender, RadListViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArgument = e.CommandArgument.ToString();
        if(commandName == "AddToCart")
        {
            add_item_to_cart(commandArgument);
        }
        else if (e.CommandName == "addtowishlist")
        {
            string wishlist_msg = "";

            if (Session["CustID"] != null && Session["CustID"].ToString() != "guest")
            {
                wishlist_msg = CommonCode.AddToWishlist(Session["CustID"] + "", commandArgument, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString());
            }
            else
            {
                Response.Redirect("/Customer/user_login.aspx", true);
            }
            //if (Session["CustID"] != null)
            //{
                
            //}
            //else
            //{
            //    wishlist_msg = CommonCode.AddToWishlist("guest", commandArgument, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString());
            //}

            if (wishlist_msg == "success")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Item added to Wishlist !');", true);
                //CommonCode.show_toastr("success", "Wishlist", "Product added to Wishlist !", true, "closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000", true, "window.location.replace('Customer/wishlist.aspx');", ltr_scripts);
            }
            else if (wishlist_msg == "Product is already in your wishlist !")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Item is already in your wishlist !');", true);
                //CommonCode.show_toastr("info", "Wishlist", "Product is already in your wishlist !", true, "closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000", true, "window.location.replace('Customer/wishlist.aspx');", ltr_scripts);
            }
            else
            {
                CommonCode.show_toastr("error", "Wishlist", wishlist_msg, false, "", false, "", ltr_scripts);
            }
        } 
        ((MasterPage)this.Page.Master).load_Header();        
        //this.updSerchGrid.Update();
    }
      
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public static CartData AddDataintoCart(string ProductID)
    {
        CartData mdbs = new CartData();

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
        mdbs.Result = errmsg;
        return mdbs;
    }


    protected void ddl_Language_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

public class CartData
{
    public string Result { get; set; }
    public string CartItems { get; set; }
    public string ltrnmsg { get; set; }
    public string litmsg { get; set; }
}
