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
        
        if (Session["CustID"] != null && Session["CustID"].ToString() != "guest")
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                if (Request.QueryString["Type"].ToString() == "A")
                {
                    Change_Address.Visible = true;
                    Change_Password.Visible = false;
                    changeAddress.Visible = true;
                    changepwd.Visible = false;
                    btnSave.Text = "Save Changes";
                }
                else
                {
                    Change_Address.Visible = false;
                    Change_Password.Visible = true;
                    changeAddress.Visible = false;
                    changepwd.Visible = true;
                    btnSave.Text = "Change Password";
                }
            }
            else
            {
                Session.Abandon();
                Response.Redirect("user_login.aspx", true);
            }

            if (!IsPostBack)
            {
                dv_ZipCode.Visible = true;
                dvcountry.Visible = true;
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
               // CommonCode.getCountry_Details(0, "", "", dd_Country);
                //CommonCode.getCountry_Details(1, "", "0", dd_State);
                load_user_details();
            }
                
        }
        else
        {
            Session.Abandon();
            Response.Redirect("user_login.aspx", true);
        }
        
    }

    private void load_user_details()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_user_details(Session["CustID"].ToString(), Session["iCompanyId"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                textEmail.Text = dt.Rows[0]["EmailID"].ToString();
                textPhone.Text = dt.Rows[0]["Mobile"].ToString();
                txtBilltoEmail.Text = dt.Rows[0]["EmailID"].ToString();
                txtBilltoMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtBilltoName.Text = dt.Rows[0]["CustName"].ToString();

                if (dt.Rows[0]["CountryID"].ToString() == "")
                {
                    dd_Country.SelectedIndex = dd_Country.Items.IndexOf(dd_Country.Items.FindByValue(ConfigurationManager.AppSettings["DefaultCountryCode"].ToString()));
                }
                else
                {
                    dd_Country.SelectedIndex = dd_Country.Items.IndexOf(dd_Country.Items.FindByValue(dt.Rows[0]["CountryID"].ToString()));
                }

               // dd_Country.SelectedIndex = dd_Country.Items.IndexOf(dd_Country.Items.FindByValue(dt.Rows[0]["CountryID"].ToString()));
                CommonCode.getCountry_Details(1, dd_Country.SelectedValue, "", dd_State);
                dd_State.SelectedIndex = dd_State.Items.IndexOf(dd_State.Items.FindByValue(dt.Rows[0]["StateID"].ToString()));
                CommonCode.getCountry_Details(2, "", dd_State.SelectedValue, dd_City);
                dd_City.SelectedIndex = dd_City.Items.IndexOf(dd_City.Items.FindByValue(dt.Rows[0]["CityID"].ToString()));
                txtBilltoPincode.Text = dt.Rows[0]["BillingPostalCode"].ToString();
                txtBilltoAddress.Text = dt.Rows[0]["BillingAddress"].ToString();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading User Details", errmsg, ph_msg);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Type"].ToString() == "A")
        {
            directional_signup();
        }
        else
        {
            User_Change_Password();
        } 
    }

    private void User_Change_Password() 
    {

        String Password = textCurrentPassword.Text; 
        if (Session["Password"].ToString() == Password)
        {
            string Custerrmsg = "";
            if(textNewPassword.Text.ToString().Trim() == textConfirmPassword.Text.ToString().Trim())
            {
                register_user("", textNewPassword.Text.ToString().Trim(),"", "", "", "", "", "", "", "", "", "", Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), "2", "", "", "", "", out Custerrmsg);
                if (Custerrmsg != "Password Changed Successfully!")
                {
                    CommonCode.show_alert("danger", "Error while Changing Password", Custerrmsg, ph_msg);
                    ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Error while Changing Password');", true);
                    return;
                }
                else
                {
                    Session.Abandon();
                    Session.Clear();
                    Session.Abandon();
                    Session.RemoveAll();
                    System.Web.Security.FormsAuthentication.SignOut();
                    ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Password Changed Successfully');window.location ='user_login.aspx';", true);
                    //Response.Redirect("user_login.aspx", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Your New Password and Confirm Password is not matched!');", true);
                return;
            } 
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Your Current Password is not correct!');", true);
        }
    }




    private void directional_signup()
    {
        string CustName; string BillingAddress; string BillingCityID;
        string BillingPostalCode; string BillingPhone; string Mobile; string EmailID;
        
        CustName = txtBilltoName.Text.Trim();
        BillingAddress = txtBilltoAddress.Text.Trim();
        BillingCityID = dd_City.SelectedValue;
        BillingPostalCode = txtBilltoPincode.Text.Trim();
        BillingPhone = textPhone.Text.Trim();
        Mobile = textPhone.Text.Trim();
        EmailID = textEmail.Text.Trim();         
     //   register_user(CustName, BillingAddress, BillingCityID, BillingPostalCode, BillingPhone, Mobile, EmailID);

        if (txtBilltoName.Text.Trim() == "")
        {
            CommonCode.show_alert("danger", "Name Can't allowed blank", "", ph_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Name Can't allowed blank');", true);
            return;
        }
        if (txtBilltoAddress.Text.Trim() == "")
        {
            CommonCode.show_alert("danger", "Address Can't allowed blank", "", ph_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Address Can't allowed blank');", true);
            return;
        }
        if (dd_City.SelectedValue == "Nil")
        {
            CommonCode.show_alert("danger", "Please Select Country, State and City", "", ph_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Select Country, State and City');", true);
            return;
        }
        if (textPhone.Text.Trim() == "")
        {
            CommonCode.show_alert("danger", "Please Enter Phone / Mobile No", "", ph_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Phone / Mobile No');", true);
            return;
        }
        if (textEmail.Text.Trim() == "")
        {
            CommonCode.show_alert("danger", "Please Enter EmailID", "", ph_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter EmailID');", true);
            return;
        }
        string Custerrmsg = "";
        register_user(txtBilltoName.Text.Trim(), "", txtBilltoAddress.Text.Trim(), dd_City.SelectedValue, txtBilltoPincode.Text.Trim(), textPhone.Text.Trim(), textPhone.Text.Trim(), textEmail.Text.Trim(), "", "", "", "Individual",
            Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), "1", "", "", "", "", out Custerrmsg);
        if (Custerrmsg != "Record Updated Successfully!")
        {
            CommonCode.show_alert("danger", "Error while updating your info", Custerrmsg, ph_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Error while updating your info');", true);
            return;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Information Updated Successfully');", true);
        }


    }


    //private void register_user(string CustName, string BillingAddress, string BillingCityID, string BillingPostalCode, string BillingPhone, string Mobile,
    //    string EmailID)
    //{
    //    SqlCommand com = new SqlCommand("dbo_Update_Master_customer", CommonCode.con);
    //    com.CommandType = CommandType.StoredProcedure;
    //    com.Parameters.Add("@CustName", SqlDbType.VarChar, 200).Value = CustName;
    //    com.Parameters.Add("@BillingAddress", SqlDbType.VarChar, 1000).Value = BillingAddress;
    //    com.Parameters.Add("@BillingCityID", SqlDbType.VarChar, 10).Value = BillingCityID;
    //    com.Parameters.Add("@BillingPostalCode", SqlDbType.VarChar, 100).Value = BillingPostalCode;
    //    com.Parameters.Add("@BillingPhone", SqlDbType.VarChar, 100).Value = BillingPhone;
    //    com.Parameters.Add("@Mobile", SqlDbType.VarChar, 10).Value = Mobile;
    //    com.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = EmailID;        
    //    com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
    //    com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = Session["CustID"] + "";
    //    string errmsg;
    //    errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
    //    if (errmsg == "success")
    //    {
    //        load_user_details();
    //        ScriptManager.RegisterStartupScript(this, GetType(), "info", "alert('Information updated successfully!');", true);
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Error Occured while updating Information '" + errmsg + ");", true);
    //    }
    //}

    private void register_user(string CustName, string UserPassword, string BillingAddress, string BillingCityID, string BillingPostalCode, string BillingPhone, string Mobile, string EmailID, string Remark,
   string CreatedBy, string UpdatedBy, String UserType, String iCompanyID, String iBranchID, String ActionType, String schoolName, String ClassYear, String studentStream, String OtherInfo, out string errmsg)
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
        com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = Session["CustID"].ToString();
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
        com.Parameters.Add("@ActionType", SqlDbType.Int).Value = ActionType;
        com.Parameters.Add("@SchooName", SqlDbType.VarChar, 200).Value = schoolName;
        com.Parameters.Add("@classyear", SqlDbType.VarChar, 200).Value = ClassYear;
        com.Parameters.Add("@Stream", SqlDbType.VarChar, 200).Value = studentStream;
        com.Parameters.Add("@OtherInfo", SqlDbType.VarChar, 200).Value = OtherInfo;
        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
    }




    protected void dd_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvcountrys.Visible = true;
        dd_State.Items.Clear();
        CommonCode.getCountry_Details(1, dd_Country.SelectedValue, "", dd_State);
    }

    protected void dd_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvcountrys.Visible = true;

        dd_City.Items.Clear();
        CommonCode.getCountry_Details(2, "", dd_State.SelectedValue, dd_City);

    }



    protected void textCurrentPassword_TextChanged(object sender, EventArgs e)
    {
        SqlCommand com = new SqlCommand("Web_Customer_login", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@UserName", SqlDbType.VarChar, 200).Value = txtBilltoEmail.Text.Trim();
        com.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = textCurrentPassword.Text.Trim();
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
        SqlDataAdapter ad = new SqlDataAdapter(com);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            if(dt.Rows[0]["pwd"].ToString() != textCurrentPassword.Text.ToString().Trim())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Your Current Password is wrong!');", true);
                return;
            }
            else
            {
                textCurrentPassword.Text = dt.Rows[0]["pwd"].ToString();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Your Current Password is wrong!');", true);
            return;
        }

    }
}