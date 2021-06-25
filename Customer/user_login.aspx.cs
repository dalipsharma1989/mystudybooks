using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Configuration;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Login");
        ltr_msg_for_login.Text = "";
        div_checkout_guest.Visible = false;
        textEmail_login.Focus();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["session_expired"]))
            {
                CommonCode.show_alert("info", "Session Expired!", "It seems like your session has been expired due to security reasons. Please try login again.", ltr_msg_for_login);
            }
            load_remember_me_cookie();
            if (Session["tmpcart"] != null)
            {
                //div_checkout_guest.Visible = true;
            }
        }
    }

    private void clear_form()
    {
    }


    private bool check_if_cart_is_Empty()
    {
        bool ok = true;

        
        SqlCommand com = new SqlCommand("dbo_get_cart", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = Session["CustID"];
        
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                ok = false;
            }
            else
            {
                ok = true;
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            ok = true;
        }
        return ok;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        String Password = textPassword_Login.Text;
        String DecryptedPassword = "";
        String EncryptedPassword = cryptography.Encrypt(Password);
        byte[] UserPassword = Encoding.UTF8.GetBytes(EncryptedPassword);

        SqlCommand com = new SqlCommand("Web_Customer_login", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@UserName", SqlDbType.VarChar,200).Value = textEmail_login.Text;
        com.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = Password;
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
        SqlDataAdapter ad = new SqlDataAdapter(com);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            //EncryptedPassword = Encoding.UTF8.GetString((Byte[])dt.Rows[0]["UserPassword"]);
            //DecryptedPassword = cryptography.Decrypt(EncryptedPassword);
            DecryptedPassword = dt.Rows[0]["pwd"].ToString();
            if (Password == DecryptedPassword)
            { 
                    Session["CustID"] = dt.Rows[0]["CustID"].ToString();
                    Session["CustEmail"] = dt.Rows[0]["EmailID"].ToString();
                    Session["CustName"] = dt.Rows[0]["CustName"].ToString();
                    Session["CustType"] = "Retail";
                    Session["Password"] = textPassword_Login.Text.Trim();
                    set_remember_me_cookie();
                    if (Session["tmpcart"] != null)
                    {
                        savetmpcart_to_db(dt.Rows[0]["CustID"].ToString());
                    }                 
                    
                    //----------REDIRECTION OPTIONS ------------------------
                    
                    if (!string.IsNullOrEmpty(Request.QueryString["returnto"]))
                    {
                        Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["returnto"]), true);
                    }
                    else
                    {
                    if (check_if_cart_is_Empty())
                    {
                        Response.Redirect("/index.aspx", true);
                    }
                    else
                    {
                        Response.Redirect("user_cart.aspx", true);
                    }       
                    }                    
                    //----------REDIRECTION OPTIONS ------------------------
                    
            }
            else
            {
                CommonCode.show_alert("danger", "Invalid Password ", "You have entered wrong Password !", ltr_msg_for_login);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Invalid Email ID ", "You have entered may be wrong Email ID or Password", ltr_msg_for_login);
        }
    }

    private void savetmpcart_to_db(String CustID)
    {
        String CartID = "", errmsg = "";
        DataTable dt = Session["tmpcart"] as DataTable;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow rr in dt.Rows)
            {
                errmsg = errmsg + CommonCode.AddToCart(CustID, rr["ProductID"] + "", Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out CartID);
            }
        }
        if (!string.IsNullOrEmpty(CartID))
            Session["CartID"] = CartID;
    }


    private void set_remember_me_cookie()
    {
        if (chb_RememberMe.Checked)
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
        }
        else
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
        }
        //Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
        //Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
        Response.Cookies["UserName"].Value = textEmail_login.Text.Trim();
        Response.Cookies["Password"].Value = textPassword_Login.Text.Trim();
    }

    private void load_remember_me_cookie()
    {
        if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
        {
            textEmail_login.Text = Request.Cookies["UserName"].Value;
            textPassword_Login.Attributes["value"] = Request.Cookies["Password"].Value;
            chb_RememberMe.Checked = true;
        }
        else
        {
            chb_RememberMe.Checked = false;
        }
    }


    protected void btn_Forgotpwd_Click(object sender, EventArgs e)
    {

        string errmsg, mail_err_msg = "";
        DataTable dt = new DataTable();
        DAL UD = new DAL();

        dt = UD.get_UserLoginPassword_Forgot(textEmail_login.Text.ToString(), "", Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                String EmailID = dt.Rows[0]["EmailID"].ToString();
                String UserPassword = "";
                String CustName = dt.Rows[0]["CustName"].ToString();
                UserPassword = dt.Rows[0]["pwd"].ToString();
                Email_Send(EmailID, CustName, UserPassword, out mail_err_msg);
                if (mail_err_msg == "success")
                {
                    CommonCode.show_alert("Alert", "Your Password has been sent to " + EmailID.ToString(), "Please get it by login your Email ID at your Mailbox, Thanks.", ltr_msg_for_login);
                }
                else
                {
                    CommonCode.show_alert("danger", "Password recovery failed", "You have entered wrong Email ID", ltr_msg_for_login);
                }
            }
            else
            {
                CommonCode.show_alert("danger", "Invalid EmailID", "You have entered wrong Email ID, Please enter correct EMail", ltr_msg_for_login);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Invalid EmailID", "You have entered wrong Email ID, Please enter correct EMail", ltr_msg_for_login);
        }

        //string errmsg, mail_err_msg = "";
        //SqlCommand com = new SqlCommand(" Select * from MasterCustomer where EmailID = '" + textEmail_login.Text.Trim().ToString() + "'", CommonCode.con);
        //DataTable dt = new DataTable();
        //dt = CommonCode.getData(com, out errmsg);
        //if (errmsg == "success")
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        String EmailID = dt.Rows[0]["EmailID"].ToString();
        //        String UserPassword = "";
        //        String CustName = dt.Rows[0]["CustomerName"].ToString();

        //        String EncryptedPassword = Encoding.UTF8.GetString((Byte[])dt.Rows[0]["pwd"]);
        //        String DecryptedPassword = cryptography.Decrypt(EncryptedPassword);
        //        UserPassword = DecryptedPassword;
        //        Email_Send(EmailID, CustName, UserPassword, out mail_err_msg);
        //        if (mail_err_msg == "success")
        //        {
        //            CommonCode.show_alert("Alert", "Your Password has been sent to " + EmailID.ToString(), "Please get it by login your Email ID at your Mailbox, Thanks.", ltr_msg_for_login);
        //        }
        //        else
        //        {
        //            CommonCode.show_alert("danger", "Password recovery failed", "You have entered wrong Email ID", ltr_msg_for_login);
        //        }
        //    }
        //    else
        //    {
        //        CommonCode.show_alert("danger", "Invalid EmailID", "You have entered wrong Email ID, Please enter correct EMail", ltr_msg_for_login);
        //    }
        //}
        //else
        //{
        //    CommonCode.show_alert("danger", "Invalid EmailID", "You have entered wrong Email ID, Please enter correct EMail", ltr_msg_for_login);
        //}


    }

    private void Email_Send(String EmailID, String CustName, String UserPassword, out string mail_err_msg)
    {
        mail_err_msg = "";
        string txtTo = EmailID;
        string txtEmail = "";
        string txtPassword = "";
        string smtpHostName = "";
        Boolean sslenable = false;
        Int32 smtpPortNo = 0;
        string ssql = " Select Top 1 * from EmailConfig where companyID = '" + CommonCode.CompanyID() + "' ";
        SqlCommand com = new SqlCommand(ssql, CommonCode.con);
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);

        //if (dt.Rows.Count > 0)
        //{
        //    txtEmail = dt.Rows[0]["SMTPUserid"].ToString();
        //    txtPassword = dt.Rows[0]["SMTPPassword"].ToString().Trim();
        //    smtpHostName = dt.Rows[0]["SMTPHost"].ToString();
        //    smtpPortNo = int.Parse(dt.Rows[0]["SMTPPortNo"].ToString());
        //    sslenable = bool.Parse(dt.Rows[0]["enableSsl"].ToString());
        //}

        txtEmail = ConfigurationManager.AppSettings["SMTP_EmailID"].ToString();
        txtPassword = ConfigurationManager.AppSettings["SMTP_PASSWORD"].ToString();
        smtpHostName = ConfigurationManager.AppSettings["SMTP_HOSTNAME"].ToString();
        smtpPortNo = int.Parse(ConfigurationManager.AppSettings["SMTP_PORNO"].ToString());
        sslenable = bool.Parse(ConfigurationManager.AppSettings["SMTP_ENABLESSL"].ToString());


        using (MailMessage mm = new MailMessage())
        {
            mm.From = new MailAddress(txtEmail);
            mm.To.Add(new MailAddress(txtTo)); //adding multiple TO Email Id  
            //mm.Bcc.Add(new MailAddress(bccid));
            mm.Subject = "Password Recovery Email from mystudybooks.in";
            mm.IsBodyHtml = true;
            mm.Body = "Dear " + CustName + "";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "            Thank you for visit at https://mystudybooks.in/  ";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "           Your password is <b>" + UserPassword + "</b>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "  Please visit at https://mystudybooks.in/Customer/user_login.aspx and login for your desired item order. ";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "Best Regards,";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += ConfigurationManager.AppSettings["CompanyName"].ToString()+".";

            SmtpClient smtp = new SmtpClient();

            smtp.Host = smtpHostName; 
            //"smtpout.secureserver.net" ;
            //smtp.Host = "relay-hosting.secureserver.net";
            //UnComment While Updating Online 
            //smtp.EnableSsl = false;
            //Comment While Updating Online and vice versa
            smtp.EnableSsl = sslenable;
            NetworkCredential NetworkCred = new NetworkCredential(txtEmail, txtPassword.Trim());
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            smtp.Port = smtpPortNo; // 80;
            smtp.Send(mm);
            mail_err_msg = "success";
        }


    }

}