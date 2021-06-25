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
    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_alert_msg.Text = "";
        ltr_scripts.Text = "";
        this.Title = CommonCode.SetPageTitle("Edit Product");
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                textDiscountStartDate.Text = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
                textDiscountEndDate.Text = string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(5));
                //load_currency();
                //load_Categories(0, "0");
                //load_subjects("0");
                //load_Class("0");
                //string bind_language_errmsg = "";
                //CommonCode.bind_languages(dd_language, out bind_language_errmsg);
                if (!string.IsNullOrEmpty(Request.QueryString["editid"]))
                {
                    if (Request.QueryString["editid"].ToString() == "NEW")
                    {
                        //New
                    }
                    else
                    {
                        //Edit 
                        load_Product_details();
                    }
                }
                else
                {
                    Response.Redirect("product_list.aspx");
                }
                if (!string.IsNullOrEmpty(Request.QueryString["update"]))
                {
                    CommonCode.show_alert("success", "Product Updated", "Product Updated successfully !", ltr_alert_msg);
                }
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }

        
    }

    private void load_currency()
    {
        dd_currency.Items.Clear();
        dd_currency.Items.Add(new ListItem(CommonCode.AppSettings("Currency") + "-" + CommonCode.AppSettings("CurrencySymbol"), CommonCode.AppSettings("CurrencySymbol")));
    }

    private void load_Product_details()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_book_details(Request.QueryString["editid"], false, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                textProductName.Text = dt.Rows[0]["BookName"] + "";
                textISBN.Text = dt.Rows[0]["ISBN"] + "";
                textPrice.Text = dt.Rows[0]["SalePrice"] + "";
                textAuthor.Text = dt.Rows[0]["Author"] + "";
                textPublisher.Text = dt.Rows[0]["Publisher"] + "";
                textPublishYear.Text = dt.Rows[0]["PublishYear"] + "";
                textWeight.Text = dt.Rows[0]["Weight"] + "";
                textVolume.Text = dt.Rows[0]["Volume"] + "";
                textEdition.Text = dt.Rows[0]["Edition"] + "";
                txtReprint.Text = dt.Rows[0]["RePrint"] + "";
                textTotalPages.Text = dt.Rows[0]["TotalPages"] + "";
                textClBal.Text = dt.Rows[0]["ClosingBal"] + "";

                float DiscPrice = 0;
                float.TryParse(dt.Rows[0]["DiscountPrice"].ToString(), out DiscPrice);
                if(DiscPrice != 0)
                {
                    textDiscountPrice.Text = dt.Rows[0]["DiscountPrice"] + ""; 
                }
                else
                {
                    textDiscountPrice.Text = ""; 
                }
                float DiscPer = 0;
                float.TryParse(dt.Rows[0]["DiscountPercent"].ToString(), out DiscPer);
                if (DiscPer != 0)
                { 
                    textDiscountPercent.Text = dt.Rows[0]["DiscountPercent"] + "";
                }
                else
                { 
                    textDiscountPercent.Text = "";
                }

                //textDiscountStartDate.Text = (string.IsNullOrEmpty(dt.Rows[0]["StartDate"].ToString()) ? string.Format("{0:yyyy/MM/dd}", DateTime.Now) : string.Format("{0:yyyy/MM/dd}", dt.Rows[0]["StartDate"]));
                //textDiscountEndDate.Text = (string.IsNullOrEmpty(dt.Rows[0]["EndDate"].ToString()) ? string.Format("{0:yyyy/MM/dd}", DateTime.Now.AddDays(5)) : string.Format("{0:yyyy/MM/dd}", dt.Rows[0]["EndDate"]));


                dd_currency.SelectedIndex = dd_currency.Items.IndexOf(dd_currency.Items.FindByValue(dt.Rows[0]["SaleCurrency"] + ""));
                dd_binding.SelectedIndex = dd_binding.Items.IndexOf(dd_binding.Items.FindByValue(dt.Rows[0]["Binding"] + ""));
                dd_language.SelectedIndex = dd_language.Items.IndexOf(dd_language.Items.FindByValue(dt.Rows[0]["LanguageID"] + ""));

                dd_categories.SelectedIndex = dd_categories.Items.IndexOf(dd_categories.Items.FindByValue(dt.Rows[0]["CategoryID"] + ""));

                dd_subject.SelectedIndex = dd_subject.Items.IndexOf(dd_subject.Items.FindByValue(dt.Rows[0]["SubjectID"] + ""));

                //dd_class.SelectedIndex = dd_class.Items.IndexOf(dd_class.Items.FindByValue(dt.Rows[0]["ClassID"] + ""));

                //load_subjects(string SubjectID)


                //if (!string.IsNullOrEmpty(dt.Rows[0]["SubjectID"] + ""))
                //{
                //    load_subjects(dt.Rows[0]["SubjectID"] + "");
                //    dd_subject.SelectedIndex = dd_subject.Items.IndexOf(dd_subject.Items.FindByValue(dt.Rows[0]["SubjectID"] + ""));
                //}

                //if (!string.IsNullOrEmpty(dt.Rows[0]["CategoryID"] + ""))
                //{
                //    load_SubCategories(dt.Rows[0]["CategoryID"] + "");

                //    dd_subcategories.SelectedIndex = dd_subcategories.Items.IndexOf(dd_subcategories.Items.FindByValue(dt.Rows[0]["SubCategoryID"] + ""));
                //}
                textDesc.Text = dt.Rows[0]["AboutProduct"].ToString().Replace("<br>", Environment.NewLine);
                IsDeactivate.Checked = Convert.ToBoolean(dt.Rows[0]["IsDeactive"].ToString());
            }
            else
            {
                CommonCode.show_alert("danger", "Product Not Found", "Product is either not available or deleted !", ltr_alert_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Book Details", errmsg, ltr_alert_msg);
        }


    }

    protected void btn_save_edit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["editid"]))
        {   
            if (Request.QueryString["editid"].ToString() == "NEW")
            {
                //New Mode
              //  save_new_product();
            }
            else
            {
                //Edit 
                insert_update_product_Offer();
            } 
        }
        else
        {
            Response.Redirect("product_list.aspx");
        }
    }

    private void insert_update_product_Offer() 
    {
        SqlCommand com = new SqlCommand("Web_insert_Update_product_Offer", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@ProductId", SqlDbType.VarChar,100).Value = Request.QueryString["editid"];        
        com.Parameters.Add("@ISBN", SqlDbType.VarChar, 20).Value = textISBN.Text.Replace(" ", string.Empty);
        com.Parameters.Add("@BookName", SqlDbType.VarChar, 250).Value = textProductName.Text;
        com.Parameters.Add("@SalePrice", SqlDbType.Decimal).Value = textPrice.Text; 

        decimal percentDiscount, discountedPrice;
        ComputeDiscount(out percentDiscount, out discountedPrice);

        if (percentDiscount != 0 && discountedPrice != 0)
        {
            com.Parameters.Add("@DiscountPrice", SqlDbType.Decimal).Value = discountedPrice;
            com.Parameters.Add("@DiscountPercent", SqlDbType.Decimal).Value = percentDiscount;

            DateTime startdate, enddate;
            CommonCode.CheckDate(textDiscountStartDate.Text, out startdate);
            CommonCode.CheckDate(textDiscountEndDate.Text, out enddate);

            com.Parameters.Add("@StartDate", SqlDbType.SmallDateTime).Value = startdate;
            com.Parameters.Add("@EndDate", SqlDbType.SmallDateTime).Value = enddate;
        }
        else
        {
            com.Parameters.Add("@DiscountPrice", SqlDbType.Decimal).Value = (object)DBNull.Value; ;
            com.Parameters.Add("@DiscountPercent", SqlDbType.Decimal).Value = (object)DBNull.Value;
            com.Parameters.Add("@StartDate", SqlDbType.SmallDateTime).Value = (object)DBNull.Value;
            com.Parameters.Add("@EndDate", SqlDbType.SmallDateTime).Value = (object)DBNull.Value;
        } 
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();

        string errmsg;
        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        if (errmsg == "success")
        { 
            load_Product_details();            
            Response.Redirect(Request.Url.AbsoluteUri.Replace("&update=true", "") + "&update=true", true);
        }
        else
        {
            CommonCode.show_alert("danger", "Error while updating product - <em>" + textProductName.Text + "</em>", errmsg, false, ltr_alert_msg);
        }
    }
  
    private void save_new_product()
    {
        SqlCommand com = new SqlCommand("dbo_update_product", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@ProductId", SqlDbType.VarChar, 100).Value = Request.QueryString["editid"];
        com.Parameters.Add("@BookName", SqlDbType.VarChar, 100).Value = textProductName.Text;
        com.Parameters.Add("@ISBN", SqlDbType.VarChar, 20).Value = textISBN.Text.Replace(" ", string.Empty);
        com.Parameters.Add("@LanguageID", SqlDbType.VarChar, 15).Value = (dd_language.SelectedValue == "Nil" ? (object)DBNull.Value : dd_language.SelectedValue);
        com.Parameters.Add("@BookShortName", SqlDbType.VarChar, 100).Value = textProductName.Text;
        com.Parameters.Add("@Publisher", SqlDbType.VarChar, 100).Value = (string.IsNullOrEmpty(textPublisher.Text) ? (object)DBNull.Value : textPublisher.Text);
        com.Parameters.Add("@Author", SqlDbType.VarChar, 100).Value = (string.IsNullOrEmpty(textAuthor.Text) ? (object)DBNull.Value : textAuthor.Text);
        com.Parameters.Add("@DisplaySubJect", SqlDbType.VarChar, 500).Value = "";
        com.Parameters.Add("@DisplaySubSubject", SqlDbType.VarChar, 500).Value = "";
        com.Parameters.Add("@SalePrice", SqlDbType.Decimal).Value = textPrice.Text;
        com.Parameters.Add("@SaleCurrency", SqlDbType.VarChar, 7).Value = dd_currency.SelectedValue;
        com.Parameters.Add("@Weight", SqlDbType.Int).Value = (string.IsNullOrEmpty(textWeight.Text) ? (object)DBNull.Value : textWeight.Text);
        com.Parameters.Add("@Volume", SqlDbType.VarChar, 5).Value = (string.IsNullOrEmpty(textVolume.Text) ? (object)DBNull.Value : textVolume.Text);
        com.Parameters.Add("@Edition", SqlDbType.VarChar, 15).Value = (string.IsNullOrEmpty(textEdition.Text) ? (object)DBNull.Value : textEdition.Text);
        com.Parameters.Add("@RePrint", SqlDbType.VarChar, 15).Value = (string.IsNullOrEmpty(txtReprint.Text) ? (object)DBNull.Value : txtReprint.Text);
        com.Parameters.Add("@TotalPages", SqlDbType.VarChar, 5).Value = (string.IsNullOrEmpty(textTotalPages.Text) ? (object)DBNull.Value : textTotalPages.Text);
        com.Parameters.Add("@PublishYear", SqlDbType.Int).Value = (string.IsNullOrEmpty(textPublishYear.Text) ? (object)DBNull.Value : textPublishYear.Text);
        com.Parameters.Add("@Binding", SqlDbType.VarChar, 10).Value = dd_binding.SelectedValue;
        com.Parameters.Add("@ClBal", SqlDbType.Int).Value = textClBal.Text;
        com.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 15).Value = "admin";
        com.Parameters.Add("@AboutProduct", SqlDbType.Text).Value = (string.IsNullOrEmpty(textDesc.Text) ? (object)DBNull.Value : textDesc.Text.ToString().Replace(Environment.NewLine, "<br>"));
        com.Parameters.Add("@CategoryID", SqlDbType.BigInt).Value = (dd_categories.SelectedValue == "Nil" ? (object)DBNull.Value : dd_categories.SelectedValue);
        com.Parameters.Add("@SubCategoryID", SqlDbType.BigInt).Value = (dd_subcategories.SelectedValue == "Nil" ? (object)DBNull.Value : dd_subcategories.SelectedValue);

        string subjectid = dd_subject.SelectedValue;
        string subjectname = "";
        subjectname = textNewSubject.Text.Trim();
        if (string.IsNullOrEmpty(subjectname))
        {
            if (subjectid == "Nil")
            {
                com.Parameters.Add("@SubjectID", SqlDbType.BigInt).Value = (object)DBNull.Value;
                com.Parameters.Add("@SubjectName", SqlDbType.VarChar, 100).Value = DBNull.Value;
            }
            else
            {
                com.Parameters.Add("@SubjectID", SqlDbType.BigInt).Value = subjectid;
                com.Parameters.Add("@SubjectName", SqlDbType.VarChar, 100).Value = DBNull.Value;
            }
        }
        else
        {
            com.Parameters.Add("@SubjectID", SqlDbType.BigInt).Value = (object)DBNull.Value;
            com.Parameters.Add("@SubjectName", SqlDbType.VarChar, 100).Value = subjectname;
        }


        string classid = dd_class.SelectedValue;
        string classname = "";
        classname = textNewClass.Text.Trim();
        if (string.IsNullOrEmpty(classname))
        {
            if (classid == "Nil")
            {
                com.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = (object)DBNull.Value;
                com.Parameters.Add("@ClassName", SqlDbType.VarChar, 100).Value = DBNull.Value;
            }
            else
            {
                com.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = classid;
                com.Parameters.Add("@ClassName", SqlDbType.VarChar, 100).Value = DBNull.Value;
            }
        }
        else
        {
            com.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = (object)DBNull.Value;
            com.Parameters.Add("@ClassName", SqlDbType.VarChar, 100).Value = classname;
        }


        decimal percentDiscount, discountedPrice;
        ComputeDiscount(out percentDiscount, out discountedPrice);

        if (percentDiscount != 0 && discountedPrice != 0)
        {
            com.Parameters.Add("@DiscountPrice", SqlDbType.Decimal).Value = discountedPrice;
            com.Parameters.Add("@DiscountPercent", SqlDbType.Decimal).Value = percentDiscount;

            DateTime startdate, enddate;
            CommonCode.CheckDate(textDiscountStartDate.Text, out startdate);
            CommonCode.CheckDate(textDiscountEndDate.Text, out enddate);

            com.Parameters.Add("@StartDate", SqlDbType.SmallDateTime).Value = startdate;
            com.Parameters.Add("@EndDate", SqlDbType.SmallDateTime).Value = enddate;
        }
        else
        {
            com.Parameters.Add("@DiscountPrice", SqlDbType.Decimal).Value = (object)DBNull.Value; ;
            com.Parameters.Add("@DiscountPercent", SqlDbType.Decimal).Value = (object)DBNull.Value;
            com.Parameters.Add("@StartDate", SqlDbType.SmallDateTime).Value = (object)DBNull.Value;
            com.Parameters.Add("@EndDate", SqlDbType.SmallDateTime).Value = (object)DBNull.Value;
        }

        com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
        com.Parameters.Add("@IsDeactive", SqlDbType.Int).Value = (IsDeactivate.Checked == true ? 1 : 0);

        string errmsg;
        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        if (errmsg == "success")
        {
            load_subjects("0");
            load_Class("0");
            load_Product_details();
            CommonCode.show_alert("success", "Product Saved", "Product Saved successfully !", ltr_alert_msg);
            //Response.Redirect(Request.Url.AbsoluteUri.Replace("&update=true", "") + "&update=true", true);
        }
        else
        {
            CommonCode.show_alert("danger", "Error while Creating product - <em>" + textProductName.Text + "</em>", errmsg, false, ltr_alert_msg);
        }
        
    }

    private void load_Categories(int Type, string CategoryID)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_get_menu_category_subcat", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = Type;
        com.Parameters.Add("@CategoryID ", SqlDbType.BigInt).Value = CategoryID;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            dd_categories.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                dd_categories.Items.Add(new ListItem("Select Category", "Nil"));
                foreach (DataRow rr in dt.Rows)
                {
                    dd_categories.Items.Add(new ListItem(rr["CategoryDesc"] + "", rr["CategoryID"] + ""));
                }
            }
            else
            {
                dd_categories.Items.Add(new ListItem("No Category Found", "Nil"));
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Categories", errmsg, false, ltr_alert_msg);
        }
    }

    private void load_SubCategories(string CategoryID)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_get_menu_category_subcat", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = 1;
        com.Parameters.Add("@CategoryID ", SqlDbType.BigInt).Value = CategoryID;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);

        if (errmsg == "success")
        {
            dd_subcategories.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                dd_subcategories.Items.Add(new ListItem("Select Sub Category", "Nil"));
                foreach (DataRow rr in dt.Rows)
                {
                    dd_subcategories.Items.Add(new ListItem(rr["SubCategoryDesc"] + "", rr["SubCategoryID"] + ""));
                }
            }
            else
            {
                dd_subcategories.Items.Add(new ListItem("No Sub Category Found", "Nil"));
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Sub Categories", errmsg, false, ltr_alert_msg);
        }
    }

    private void load_subjects(string SubjectID)
    {
        String errmsg = "";
        DataTable dt = new DataTable();
        DAL dal = new DAL();
        dt = dal.get_subjects(SubjectID,0, out errmsg);
        if (errmsg == "success")
        {
            dd_subject.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                dd_subject.Items.Add(new ListItem("Select Subject", "Nil"));
                foreach (DataRow rr in dt.Rows)
                {
                    dd_subject.Items.Add(new ListItem(rr["SubjectName"] + "", rr["SubjectID"] + ""));
                }
            }
            else
            {
                dd_subject.Items.Add(new ListItem("No Subject Found", "Nil"));
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Subjects", errmsg, false, ltr_alert_msg);
        }
    }

    private void load_Class(string ClassID)
    {
        String errmsg = "";
        DataTable dt = new DataTable();
        DAL dal = new DAL();
        dt = dal.get_Classes(ClassID, out errmsg);
        if (errmsg == "success")
        {
            dd_class.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                dd_class.Items.Add(new ListItem("Select Class", "Nil"));
                foreach (DataRow rr in dt.Rows)
                {
                    dd_class.Items.Add(new ListItem(rr["ClassName"] + "", rr["ClassID"] + ""));
                }
            }
            else
            {
                dd_class.Items.Add(new ListItem("No Classes Found", "Nil"));
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Classes", errmsg, false, ltr_alert_msg);
        }
    }

    protected void dd_categories_SelectedIndexChanged(object sender, EventArgs e)
    {
        load_SubCategories(dd_categories.SelectedValue);
    }

    private void ComputeDiscount(out decimal percentDiscount, out decimal discountedPrice)
    {
        decimal originalPrice = 0;
        percentDiscount = 0;
        discountedPrice = 0;

        if (!string.IsNullOrEmpty(textPrice.Text))
        {
            originalPrice = Convert.ToDecimal(textPrice.Text);
            if (string.IsNullOrEmpty(textDiscountPrice.Text))
            {
                if (!string.IsNullOrEmpty(textDiscountPercent.Text))
                {
                    percentDiscount = Convert.ToDecimal(textDiscountPercent.Text);
                    decimal markdown = Math.Round(originalPrice * (percentDiscount / 100m), 2, MidpointRounding.ToEven);
                    discountedPrice = originalPrice - markdown;
                }
            }
            else
            {
                discountedPrice = Convert.ToDecimal(textDiscountPrice.Text);
                decimal markdown = Math.Round(((originalPrice - discountedPrice) / originalPrice), 2, MidpointRounding.ToEven);
                percentDiscount = markdown * 100;
            }

        }

    }

    private void Send_Email_for_the_product()
    {
        string Productid = Request.QueryString["editid"];
        String errmsg = "", EmailID = "", NoifyName = "", Phone = "", ISBN = "", BookName = "", mail_err_msg = "", rowID = "";
        DataTable dt = new DataTable();

        try
        {
            DAL dal = new DAL();
            dt = dal.get_Notifyme_book(Productid, out errmsg);
            if (errmsg == "success")
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        EmailID = dtRow["EmailID"].ToString();
                        NoifyName = dtRow["NotifyUserName"].ToString();
                        Phone = dtRow["Phone"].ToString();
                        ISBN = dtRow["ISBN"].ToString();
                        BookName = dtRow["BookName"].ToString();
                        rowID = dtRow["RowID"].ToString();
                        Email_Send_Customer(EmailID, NoifyName, Phone, ISBN, BookName, out mail_err_msg);
                        if (mail_err_msg == "success")
                        {
                            DAL Notifydal = new DAL();
                            Notifydal.insert_update_delete_NotifyMe_Users(rowID, EmailID, Phone, NoifyName, Productid, 1,Session["iCompanyID"].ToString(), out errmsg);
                        }
                    }
                }
            }
            else
            {
                CommonCode.show_alert("danger", "Error while checking notify me users", errmsg, false, ltr_alert_msg);
            }
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "Error while checking notify me users", ex.Message, false, ltr_alert_msg);
        }
    }

    private void Email_Send_Customer(String EmailID, String Name, String PhoneNo, string ISBN, string BookName, out string mail_err_msg)
    {
        mail_err_msg = "";
        string txtTo = EmailID;
        string txtEmail = "";
        string txtPassword = "";
        string smtpHostName = "";
        Int32 smtpPortNo = 0;
        DataTable dt = new DataTable();
        DataTable Dtproduct = new DataTable();
        string errmsg;
        string ssql = " Select Top 1 * from EmailConfig where companyID = '" + CommonCode.CompanyID() + "' ";        
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
            using (MailMessage mm = new MailMessage())
            {
                mm.From = new MailAddress(txtEmail);
                mm.To.Add(new MailAddress(txtTo));   //adding multiple TO Email Id                  
                mm.Subject = "Noify me";
                mm.IsBodyHtml = true;
                mm.Body = "Dear Valued Customer";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "Now the book named <b>" + BookName + "</b> with ISBN <b>" + ISBN + "</b> is in stock.";
                mm.Body += "<br/>";
                mm.Body += "You can purchase this book online through our website <b> dcbooks.ae </b>.";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "Regards";
                mm.Body += "<br/>";
                mm.Body += "dcbooks.ae";

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

}