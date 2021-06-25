using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Register");
        ltr_msg_for_signup.Text = "";
        ltr_msg.Text = "";
        if (Session["CustID"] != null && Session["CustID"].ToString() != "guest")
        {
            Session.Abandon();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("user_login.aspx", true);
        }
        if (!IsPostBack)
        {            
            dd_State.Items.Clear();
            if (ConfigurationManager.AppSettings["DefaultCountryCode"].ToString() != "")
            {
                CommonCode.getCountry_Details(0, ConfigurationManager.AppSettings["DefaultCountryCode"].ToString(), "", dd_Country);
                CommonCode.getCountry_Details(1, ConfigurationManager.AppSettings["DefaultCountryCode"].ToString(), "", dd_State); 
            }
            else
            {
                CommonCode.getCountry_Details(0, "", "", dd_Country); 
                CommonCode.getCountry_Details(1, "", "", dd_State);
            }
            //CommonCode.getCountry_Details(0, "", "", dd_Country);
            
        }
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        btnSignUp.CssClass = "btn btn-success disabled";

        string CustName; string BillingAddress; string BillingCityID = ""; string BillingPostalCode; string BillingPhone; string Mobile; string EmailID;
        CustName = textName.Text.Trim() + " " + textNameL.Text.Trim();
        BillingAddress = textAddress.Text.Trim();
        BillingPostalCode = textPinCode.Text.Trim();
        BillingPhone = textPhone.Text.Trim();
        Mobile = textPhone.Text.Trim();
        EmailID = textEmail.Text.Trim();
        String Password = textPassword.Text;
        
        if (textName.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter First Name');", true);
            return;
        }
        if (CustName.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter First Name and Last Name');", true);
            return;
        }
        

        if (EmailID == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Email ID');", true);
            return;
        }
        if (Mobile == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Mobile');", true);
            return;
        }
        //if (dd_City.SelectedValue == "Nil")
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Select City Name');", true);
        //    return;
        //}
        //if (BillingPostalCode.Trim() == "" || BillingPostalCode.Trim().Length < 6)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Pin / Zip Code');", true);
        //    return;
        //}
        //if (BillingAddress.Trim() == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Address');", true);
        //    return;
        //}

        if (Password == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Password');", true);
            return;
        }
        if (textConfirmPassword.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Confirm Password');", true);
            return;
        }
        //if (rememberme.Checked == false)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Accept Terms and Conditions');", true);
        //    return;
        //}
        if (Password == textConfirmPassword.Text )
        {
            
            register_user(CustName, Password, BillingAddress, dd_City.SelectedValue, BillingPostalCode, BillingPhone, Mobile, EmailID, "", "", "", "Individual", Session["iCompanyId"].ToString(),
                            Session["iBranchID"].ToString(), "0", txtSchoolColUni.Text.Trim(), txtClassYearSession.Text.Trim(), txtstream.Text.Trim(), txtOtherInfo.Text.Trim());

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Password and Confirm Password didn't Match');", true);
            return;
        }
        
        btnSignUp.CssClass = "btn btn-success";
    }
 

    private void register_user(string CustName, string UserPassword, string BillingAddress, string BillingCityID, string BillingPostalCode, string BillingPhone, string Mobile, string EmailID, string Remark, 
        string CreatedBy,  string UpdatedBy, String UserType, String iCompanyID, String iBranchID , String ActionType, String schoolName , String ClassYear ,  String studentStream , String OtherInfo)
    {
        SqlCommand com = new SqlCommand("Web_insert_Master_customer", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure; 
        com.Parameters.Add("@CustName", SqlDbType.VarChar, 200).Value = CustName;
        com.Parameters.Add("@UserPassword", SqlDbType.VarChar, 30).Value = UserPassword;
        com.Parameters.Add("@BillingAddress", SqlDbType.VarChar, 1000).Value = BillingAddress;
        com.Parameters.Add("@BillingCityID", SqlDbType.VarChar, 10).Value = (BillingCityID == "Nil" ? (object)DBNull.Value : BillingCityID);        
        com.Parameters.Add("@BillingPostalCode", SqlDbType.VarChar, 100).Value = BillingPostalCode;
        com.Parameters.Add("@BillingPhone", SqlDbType.VarChar, 100).Value = BillingPhone; 
        com.Parameters.Add("@Mobile", SqlDbType.VarChar, 10).Value = Mobile;
        com.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = EmailID; 
        com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
        com.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = Remark;
        com.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 15).Value = CreatedBy;
        com.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 15).Value = UpdatedBy; 
        com.Parameters.Add("@UserType", SqlDbType.VarChar, 50).Value = UserType;
        com.Parameters.Add("@CustID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
        com.Parameters.Add("@ActionType", SqlDbType.Int).Value = ActionType;
        com.Parameters.Add("@SchooName", SqlDbType.VarChar, 200).Value = schoolName;
        com.Parameters.Add("@classyear", SqlDbType.VarChar, 200).Value = ClassYear; 
        com.Parameters.Add("@Stream", SqlDbType.VarChar, 200).Value = studentStream;
        com.Parameters.Add("@OtherInfo", SqlDbType.VarChar, 200).Value = OtherInfo;
        string errmsg;
        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        if (errmsg == "success")
        {
            String CustID = com.Parameters["@CustID"].Value.ToString();
            string country = dd_Country.SelectedItem.ToString().ToUpper(); 
            country = "INDIA"; 
            Session["OtherCountry"] = country; 
            CommonCode.show_alert("success", "<i class='fa fa-smile-o'></i>&nbsp;Thank you for Registering your account !", "Login and start exploring our products and offers.", ltr_msg);
            clear_form();
            Session["CustID"] = Mobile;
            Session["CustEmail"] = EmailID;
            Session["CustName"] = CustName;
            Session["CustType"] = UserType;
            Session["Password"] = textPassword.Text.Trim();
            if (Session["tmpcart"] != null)
            {
                savetmpcart_to_db(CustID);
            }

            Response.Redirect("../welcome.aspx?custid=" + Mobile, true); 
        }
        else
        {
            CommonCode.show_alert("danger", "<i class='fa fa-warning'></i>&nbsp;Error", errmsg, false, ltr_msg);
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
        {
            Session["CartID"] = CartID;
        }            
    }

    private void send_confimation_mail(string mode)
    {
        string info_errmsg = "", mail_errmsg = "";
        //DataRow rr = CommonCode.fetch_emailsms_info(out info_errmsg);
        //if (info_errmsg == "success")
        //{

        //}
    }

    private void clear_form()
    {
        btnSignUp.CssClass = "btn btn-success";
        textName.Text = "";
        textNameL.Text = "";
        textEmail.Text = "";
        textPhone.Text = "";
        textPassword.Text = "";
        textConfirmPassword.Text = "";
        dd_City.SelectedIndex = -1;
        dd_State.SelectedIndex = -1;
        dd_Country.SelectedIndex = -1;
        textPinCode.Text = "";
        textAddress.Text = "";
        txtClassYearSession.Text = "";
        txtOtherInfo.Text = "";
        txtSchoolColUni.Text = "";
        txtstream.Text = "";
    }

    protected void dd_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        dd_City.Items.Clear();
        CommonCode.getCountry_Details(2, "", dd_State.SelectedValue, dd_City);
    }

    protected void dd_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
         
        dd_State.Items.Clear();
        CommonCode.getCountry_Details(1, dd_Country.SelectedValue, "", dd_State);
    }
}