using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    public static string FreeShippingMessage = "", ShippingMsg_div_class = "hidden";
    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_book_detail_msg.Text = "";
        ltr_scripts.Text = "";
        this.Title = CommonCode.SetPageTitle("View Book");
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ProductID"]))
                load_book_details(Request.QueryString["ProductID"]);
            else
                CommonCode.show_alert("warning", "Item Not Found", "The item your are searching is not available. <a href='index.aspx'>CLICK HERE</a> to view other items.", ltr_book_detail_msg);

            //div_review_block.Visible = false;
           // btn_post_review.ToolTip = "Login Required";
            if (Session["CustName"] != null && Session["CustEmail"] != null)
            {
                // div_review_block.Visible = true;
                // btn_post_review.ToolTip = "Review will be visible only after approval";
            }
        }
    }
    
    private void load_book_details(String ProductID)
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        if (Session["OtherCountry"] != null && Session["OtherCountry"].ToString() == "OtherCountry")
        {
            ds = dal.get_curr_book_details("OtherCountry", ProductID, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        }            
        else
        {
            ds = dal.get_curr_book_details("", ProductID, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        }
            

        if (errmsg == "success")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

               // div_book_detail.Visible = true;
                DataTable dt_book_detail = new DataTable();
                DataTable dt_book_similar = new DataTable();

                // add image in detail
                dt_book_detail = ds.Tables[0];
                dt_book_detail = dal.get_ImagesfromAPI(dt_book_detail);

                // add image in similar
                dt_book_similar = ds.Tables[3];
                dt_book_similar = dal.get_ImagesfromAPI(dt_book_similar);

                rp_book_detail_side_image.DataSource = dt_book_detail;
                rp_book_detail_side_image.DataBind();

                review_bookname.InnerText = dt_book_detail.Rows[0]["BookName"].ToString();

                rp_book_detail_main.DataSource = dt_book_detail;
                rp_book_detail_main.DataBind();
                if (dt_book_detail.Rows[0]["AboutProduct"].ToString().Trim() != "")
                {
                    dv_ItemSummary.Visible = true;
                    dv_decorateProduct.Visible = true;
                    rp_book_details_tab.DataSource = dt_book_detail;
                    rp_book_details_tab.DataBind();
                }
                else
                {
                    dv_ItemSummary.Visible = false;
                    dv_decorateProduct.Visible = false;
                }

                if (dt_book_detail.Rows[0]["AboutAuthor"].ToString().Trim() != "")
                {
                    dv_ItemAboutAuthor.Visible = true;
                    dv_decorateAuthor.Visible = true;
                    rp_book_details_tab1.DataSource = dt_book_detail;
                    rp_book_details_tab1.DataBind();
                }
                else
                {
                    dv_ItemAboutAuthor.Visible = false;
                    dv_decorateAuthor.Visible = false;
                }

                

                
                //rp_book_detail_price.DataSource = dt_book_detail;
                //rp_book_detail_price.DataBind();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    rp_reviews.DataSource = ds.Tables[1];
                    rp_reviews.DataBind();
                    p_blank_review_msg.Visible = false;
                    rp_reviews.Visible = true;
                    dvReview.Visible = true;
                    review_count.InnerText = "Reviews " + ds.Tables[1].Rows.Count.ToString();
                }
                else
                {
                    dvReview.Visible = false;
                    p_blank_review_msg.Visible = true;
                    rp_reviews.Visible = false;
                    review_count.InnerText = "Reviews";
                }

                if (ds.Tables[2].Rows.Count > 0)
                {

                    FreeShippingMessage = ds.Tables[2].Rows[0]["FreeShippingMessage"] + "";
                   // ltr_FreeShippingMessage.Text = FreeShippingMessage;
                    ShippingMsg_div_class = "";
                }
                if (dt_book_similar.Rows.Count > 0)
                {
                    similarItem.Visible = true;
                    rpsimilarbooks.DataSource = dt_book_similar;
                    rpsimilarbooks.DataBind();
                }
                else
                {
                    similarItem.Visible = false;
                    rpsimilarbooks.DataSource = null;
                    rpsimilarbooks.DataBind();
                }

                this.Title = CommonCode.SetPageTitle("" + dt_book_detail.Rows[0]["BookName"]);
            }
            else
            {
               // div_book_detail.Visible = false;
                CommonCode.show_alert("warning", "Item Not Found", "The item your are searching is not available. <a href='index.aspx'>CLICK HERE</a> to view other items.", ltr_book_detail_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading book details", errmsg, ltr_book_detail_msg);
        }
    }

    protected void RepeaterCommandArg(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "AddToCart")
        {
            add_item_to_cart(e.CommandArgument.ToString());
        }
        else if (e.CommandName == "addtowishlist")
        {
            if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
            {
                Response.Redirect("/Customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
            }
            else
            {
                add_item_to_WishList(e.CommandArgument.ToString());
            }
        }
        Master.load_Header();
    }

    protected void rp_book_detail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String ProductID = e.CommandArgument.ToString();
        if (e.CommandName == "addtocart")
        {
            add_item_to_cart(ProductID);
        }
        else if (e.CommandName == "buynow")
        {
            if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
            {
                Response.Redirect("/Customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
            }
            else
            {
                add_item_to_cart(ProductID);
                Response.Redirect("/Customer/proceed_to_checkout.aspx", true);
            } 
        }

        else if (e.CommandName == "addtowishlist")
        {
            if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
            {
                Response.Redirect("/Customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
            }
            else
            {
                add_item_to_WishList(ProductID);
            }
        }
            (this.Master as MasterPage).load_Header();
    }

    protected void rp_similar_books_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String ProductID = e.CommandArgument.ToString();
        if (e.CommandName == "addtocart")
        {
            add_item_to_cart(ProductID);
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
            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item successfully added to wishlist');", true);
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else if (errmsg == "Product is already in your wishlist !")
        {
            ltr_scripts.Text = "<script>" +
                "toastr.options = {closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000}; " +
                "toastr.options.onclick = function () {window.location.replace('Customer/wishlist.aspx');};" +
            "toastr.info(' Already in wishlist ! ', 'wishlist');</script>";
            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item Already in wishlist !');", true);
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else
        {
            ltr_scripts.Text = "<script>toastr.error('" + errmsg.Replace('\'', ' ') + "', 'WishList Error !');</script>";
            //exception
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
            ltr_scripts.Text = "<script>" +
                "toastr.options = {closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000}; " +
                "toastr.options.onclick = function () {window.location.replace('Customer/user_cart.aspx');};" +
            "toastr.success('Successfuly added to cart ! ', 'Cart');</script>";
            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item successfully added to in your cart');", true);
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else if (errmsg == "already exist")
        {
            ltr_scripts.Text = "<script>" +
                "toastr.options = {closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000}; " +
                "toastr.options.onclick = function () {window.location.replace('Customer/user_cart.aspx');};" +
            "toastr.info(' Already in cart ! ', 'Cart');</script>";
            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item Already in cart !');", true);
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else
        {
            ltr_scripts.Text = "<script>toastr.error('" + errmsg.Replace('\'', ' ') + "', 'Cart Error !');</script>"; 
        }
    }

    protected void btn_post_review_Click(object sender, EventArgs e)
    {
        if (txtRvwHeading.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Review Heading');", true);
            return;
        }
        if (txtBookReview.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Review Description');", true);
            return;
        }

        if (Session["CustID"] != null && Session["CustID"].ToString() != "guest")
        {
            SqlCommand com = new SqlCommand("web_insert_edit_delete_approve_reviews", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@Action", SqlDbType.Int).Value = 0;
            com.Parameters.Add("@ReviewID", SqlDbType.BigInt).Value = 0;
            com.Parameters.Add("@ProductId", SqlDbType.VarChar, 20).Value = Request.QueryString["ProductID"];
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = Session["CustID"];
            com.Parameters.Add("@ReviewDesc", SqlDbType.Text).Value = txtBookReview.Text.Replace(Environment.NewLine, "<br>");      //textReview.Text.Replace(Environment.NewLine, "<br>");
            com.Parameters.Add("@Rating", SqlDbType.Int).Value = Rating1.CurrentRating;
            com.Parameters.Add("@isApporved", SqlDbType.Bit).Value = 0;
            com.Parameters.Add("@icompanyid", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
            com.Parameters.Add("@ReviewerHeading", SqlDbType.VarChar, 500).Value = txtRvwHeading.Text.Replace(Environment.NewLine, "<br>");
            DataTable dt = new DataTable();
            string errmsg;
            dt = CommonCode.getData(com, out errmsg);
            if (errmsg == "success")
            {
                txtBookReview.Text = "";
                Rating1.CurrentRating = 0;
                txtName.Text = "";
                txtRvwHeading.Text = "";
                CommonCode.show_toastr("success", "Thank you for your review", "It`ll soon be visible after approval.", false, "", false, "", ltr_scripts);
                //ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Thank you for your review, It`ll soon be visible after approval.');", true);
            }
            else
            {
                CommonCode.show_toastr("error", "Error", errmsg, false, "", false, "", ltr_scripts);
            }
        }
        else
        {
            Response.Redirect("/Customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.AbsoluteUri), true);
        }
    }
    
    protected void btn_notifyme_Click(object sender, EventArgs e)
    {
        //   string EmailID = "", Phone = "", errmsg = "";
        //   //EmailID = textEmail.Text;
        //  // Phone = textPhone.Text;
        //   DAL dal = new DAL();        
        //   dal.insert_update_delete_NotifyMe_Users("0", EmailID, Phone, Request.QueryString["productid"], 0, out errmsg);
        ////   textEmail.Text = "";
        // //  textPhone.Text = "";
        //   if (errmsg == "success")
        //   {
        //       CommonCode.show_alert("success", "You will be notified when this book will be available", "", ph_msg);
        //       CommonCode.show_toastr("success", "You will be notified when this book will be available", "", false, "", false, "", ltr_scripts);
        //   }
        //   else
        //   {
        //       CommonCode.show_alert("danger", "Error occured while saving notify me user", errmsg, ph_msg);
        //       CommonCode.show_toastr("error", "Error occured while saving notify me user", errmsg, false, "", false, "", ltr_scripts);
        //   }

        string EmailID = "", Phone = "", errmsg = "", NoifyName = "", mail_err_msg = "";
        EmailID = textEmail.Text;
        Phone = textPhone.Text;
        NoifyName = txtNameNotify.Text;
        DAL dal = new DAL();
        dal.insert_update_delete_NotifyMe_Users("0", EmailID, Phone, NoifyName, Request.QueryString["productid"], 0, Session["iCompanyId"].ToString(), out errmsg);
        textEmail.Text = "";
        textPhone.Text = "";
        txtNameNotify.Text = "";

        if (errmsg == "success")
        {
            //Email_Send(EmailID, NoifyName, Phone, Request.QueryString["productid"], out mail_err_msg);
            //Email_Send_Customer(EmailID, NoifyName, Phone, Request.QueryString["productid"], out mail_err_msg);

            CommonCode.show_alert("success", "You will be notified when this book will be available", "", ph_msg);
            CommonCode.show_toastr("success", "You will be notified when this book will be available", "", false, "", false, "", ltr_scripts);
        }
        else if (errmsg == "You already have notified this book")
        {
            CommonCode.show_alert("success", "Already registered for notify me service !!!", "", ph_msg);
            CommonCode.show_toastr("success", "Already registered for notify me service !!!, You will be notified when this book will be available", "", false, "", false, "", ltr_scripts);
        }
        else
        {
            CommonCode.show_alert("danger", "Error occured while saving notify me user", errmsg, ph_msg);
            CommonCode.show_toastr("error", "Error occured while saving notify me user", errmsg, false, "", false, "", ltr_scripts);
        }

    }


    private void Email_Send(String EmailID, String Name, String PhoneNo, string ProductID, out string mail_err_msg)
    {
        mail_err_msg = "";
        string txtTo = "katariabooks@yahoo.com";
        string txtEmail = "";
        string txtPassword = "";
        string smtpHostName = "", ISBN = "", BookName = "";
        Int32 smtpPortNo = 0;
        DataTable dt = new DataTable();
        DataTable Dtproduct = new DataTable();
        string errmsg;
        string ssql = " Select Top 1 * from EmailConfig where companyID = '" + CommonCode.CompanyID() + "' ";
        string ssqlProduct = " Select ISBN , BookName from MasterProduct where ProductID = '" + ProductID + "' and  companyID = '" + CommonCode.CompanyID() + "' ";
        try
        {
            SqlCommand com = new SqlCommand(ssql, CommonCode.con);
            dt = CommonCode.getData(com, out errmsg);
            if (dt.Rows.Count > 0)
            {
                txtEmail = dt.Rows[0]["SMTPUserid"].ToString();
                txtPassword = dt.Rows[0]["SMTPPassword"].ToString().Trim();
                smtpHostName = dt.Rows[0]["SMTPHost"].ToString();
                smtpPortNo = int.Parse(dt.Rows[0]["SMTPPortNo"].ToString());
            }
            errmsg = "";
            SqlCommand coms = new SqlCommand(ssqlProduct, CommonCode.con);
            Dtproduct = CommonCode.getData(coms, out errmsg);

            if (errmsg == "success")
            {
                if (Dtproduct.Rows.Count > 0)
                {
                    ISBN = Dtproduct.Rows[0]["ISBN"].ToString();
                    BookName = Dtproduct.Rows[0]["BookName"].ToString().Trim();
                }
            }

            using (MailMessage mm = new MailMessage())
            {
                mm.From = new MailAddress(txtEmail);
                mm.To.Add(new MailAddress(txtTo)); //adding multiple TO Email Id  
                mm.CC.Add(new MailAddress("katariabook@gmail.com")); //adding multiple TO Email Id  katariabook@gmail.com
                mm.Subject = "Noify me when book will available";
                mm.IsBodyHtml = true;
                mm.Body = "Dear Bhavya Books Team";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "  I require the book named <b>" + BookName + "</b> with ISBN <b>" + ISBN + "</b>.";
                mm.Body += "<br/>";
                mm.Body += "Please inform me when this book will be available. I want to buy this book.";
                mm.Body += "<br/>";
                mm.Body += " My Contact Details are : ";
                mm.Body += "<br/>";
                mm.Body += "<b> Name     : " + Name + "  </b>";
                mm.Body += "<br/>";
                mm.Body += "<b> EmailID  : " + EmailID + "  </b>";
                mm.Body += "<br/>";
                mm.Body += "<b> Phone No : " + PhoneNo + "  </b>";
                mm.Body += "<br/>";
                mm.Body += "Best Regards";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += Name;
                SmtpClient smtp = new SmtpClient();

                smtp.Host = smtpHostName; //"smtpout.secureserver.net" ;
                                          //smtp.Host = "relay-hosting.secureserver.net";
                smtp.EnableSsl = false;
                //Comment While Updating Online and vice versa
                //    smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(txtEmail, txtPassword.Trim());
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = smtpPortNo; // 80;
                smtp.Send(mm);
                Session["MailSent"] = "MailSent";
                mail_err_msg = "success";
            }
        }
        catch (Exception ex)
        {
            mail_err_msg = "Email not sent due to" + ex.Message;
        }
    }

    private void Email_Send_Customer(String EmailID, String Name, String PhoneNo, string ProductID, out string mail_err_msg)
    {
        mail_err_msg = "";
        string txtTo = EmailID;
        string txtEmail = "";
        string txtPassword = "";
        string smtpHostName = "", ISBN = "", BookName = "";
        Int32 smtpPortNo = 0;
        DataTable dt = new DataTable();
        DataTable Dtproduct = new DataTable();
        string errmsg;
        string ssql = " Select Top 1 * from EmailConfig where companyID = '" + CommonCode.CompanyID() + "' ";
        string ssqlProduct = " Select ISBN , BookName from MasterProduct where ProductID = '" + ProductID + "' and  companyID = '" + CommonCode.CompanyID() + "' ";
        try
        {
            SqlCommand com = new SqlCommand(ssql, CommonCode.con);
            dt = CommonCode.getData(com, out errmsg);
            if (dt.Rows.Count > 0)
            {
                txtEmail = dt.Rows[0]["SMTPUserid"].ToString();
                txtPassword = dt.Rows[0]["SMTPPassword"].ToString().Trim();
                smtpHostName = dt.Rows[0]["SMTPHost"].ToString();
                smtpPortNo = int.Parse(dt.Rows[0]["SMTPPortNo"].ToString());
            }
            errmsg = "";
            SqlCommand coms = new SqlCommand(ssqlProduct, CommonCode.con);
            Dtproduct = CommonCode.getData(coms, out errmsg);

            if (errmsg == "success")
            {
                if (Dtproduct.Rows.Count > 0)
                {
                    ISBN = Dtproduct.Rows[0]["ISBN"].ToString();
                    BookName = Dtproduct.Rows[0]["BookName"].ToString().Trim();
                }
            }

            using (MailMessage mm = new MailMessage())
            {
                mm.From = new MailAddress(txtEmail);
                mm.To.Add(new MailAddress(txtTo)); //adding multiple TO Email Id                  
                mm.Subject = "Noify me";
                mm.IsBodyHtml = true;
                mm.Body = "Dear Valued Customer";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "Thanks for your login in our website www.bhavyabooks.com.";
                mm.Body += "<br/>";
                mm.Body += "The book named <b>" + BookName + "</b> with ISBN <b>" + ISBN + "</b> is out of stock.";
                mm.Body += "<br/>";
                mm.Body += "If you want to buy this book please email us on katariabook@gmail.com or contact us on +91-11-23243489, 23269324, 43551243, 9871775858.";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "Regards";
                mm.Body += "<br/>";
                mm.Body += "www.bhavyabooks.com";

                SmtpClient smtp = new SmtpClient();

                smtp.Host = smtpHostName; //"smtpout.secureserver.net" ;
                                          //smtp.Host = "relay-hosting.secureserver.net";
                smtp.EnableSsl = false;
                //Comment While Updating Online and vice versa
                //    smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(txtEmail, txtPassword.Trim());
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = smtpPortNo; // 80;
                smtp.Send(mm);
                Session["MailSent"] = "MailSent";
                mail_err_msg = "success";
            }
        }
        catch (Exception ex)
        {
            mail_err_msg = "Email not sent due to " + ex.Message;
        }
    }


    protected void CreateRatingStars(object sender, RepeaterItemEventArgs e)
    {
        DataRowView objData = (DataRowView)e.Item.DataItem;
        System.Diagnostics.Debug.WriteLine("");
        System.Diagnostics.Debug.WriteLine(Convert.ToInt32(objData["Rating"].ToString()));
        System.Diagnostics.Debug.WriteLine("");
        HtmlGenericControl starsHtml = new HtmlGenericControl();
        int stars = Convert.ToInt32(objData["Rating"].ToString());
        for (int i = 0; i < stars; i++)
        {
            HtmlGenericControl tmp = new HtmlGenericControl();
            tmp.TagName = "a";
            HtmlGenericControl tmp2 = new HtmlGenericControl();
            tmp2.TagName = "i";
            tmp2.Attributes["class"] = "fa fa-star";
            tmp2.Attributes["style"] = "color: #ffd700; " +
            "font-size: 15px; " +
            "padding-right: 8px; " +
            "position: relative; " +
            "top: -2px;";
            tmp.Controls.Add(tmp2);
            starsHtml.Controls.Add(tmp);
        }
        for (int i = 0; i < 5-stars; i++)
        {
            HtmlGenericControl tmp = new HtmlGenericControl();
            tmp.TagName = "a";
            HtmlGenericControl tmp2 = new HtmlGenericControl();
            tmp2.TagName = "i";
            tmp2.Attributes["class"] = "fa fa-star";
            tmp2.Attributes["style"] = "color: #7a7a7a; " +
            "font-size: 15px; " +
            "padding-right: 8px; " +
            "position: relative; ";
            tmp.Controls.Add(tmp2);
            starsHtml.Controls.Add(tmp);
        }
        HtmlGenericControl div1 = e.Item.FindControl("rating_result") as HtmlGenericControl;
        div1.Controls.Add(starsHtml);
    }

}