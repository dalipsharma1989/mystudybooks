using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Author Invitation");
        ltr_msg_for_signup.Text = "";
        ltr_msg.Text = "";
        if (!IsPostBack)
        {
            dd_State.Items.Clear();
            CommonCode.getCountry_Details(0, "0", "0", dd_Country);
        }
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        btnSignUp.CssClass = "btn btn-success disabled";
        directional_signup();
        btnSignUp.CssClass = "btn btn-success";
    }

    private void directional_signup()
    {
        string CustName; string BillingAddress; string BillingCityID;
        string BillingPostalCode; string BillingPhone; string Mobile; string EmailID;

        CustName = textName.Text.Trim()+" "+ textNameL.Text.Trim();
        BillingAddress = textPlot.Text.Trim() + " " + textStreet.Text.Trim() + textAddress.Text.Trim();
        BillingCityID = dd_City.SelectedValue;
        BillingPostalCode = textPinCode.Text.Trim();
        BillingPhone =  textPhone.Text.Trim();
        Mobile =  textPhone.Text.Trim();
        EmailID = textEmail.Text.Trim();

        String Password = textPassword.Text;
        String EncryptedPassword = cryptography.Encrypt(Password);
        byte[] UserPassword = Encoding.UTF8.GetBytes(EncryptedPassword);

        register_user("", CustName, UserPassword, BillingAddress, BillingCityID, "", BillingPostalCode, BillingPhone, "", Mobile, EmailID,
                                    "", false, false, "", "", "", "", "", "Individual", "self", "self", false, "", "", true, "");
    }

    private void register_user(string PreFix, string CustName, byte[] UserPassword, string BillingAddress, string BillingCityID, string BillingZoneID,
        string BillingPostalCode, string BillingPhone, string BillingFaxNo, string Mobile, string EmailID, string Website,
        bool isSchool, bool isStudent, string SchoolID, string ClassID, string SectionID, string RollNo, string AgeGroupID,
        string Remark, string CreatedBy, string UpdatedBy,
        bool isTeacher, string TeacherID, string TeacherIDproof, bool isApproved, String UserType)
    {
        SqlCommand com = new SqlCommand("dbo_insert_Master_customer", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@PreFix", SqlDbType.VarChar, 10).Value = PreFix;
        com.Parameters.Add("@CustName", SqlDbType.VarChar, 200).Value = CustName;
        com.Parameters.Add("@UserPassword", SqlDbType.VarBinary, 500).Value = UserPassword;
        com.Parameters.Add("@BillingAddress", SqlDbType.VarChar, 1000).Value = BillingAddress;
        com.Parameters.Add("@BillingCityID", SqlDbType.VarChar, 10).Value = BillingCityID;
        com.Parameters.Add("@BillingZoneID", SqlDbType.VarChar, 7).Value = BillingZoneID;
        com.Parameters.Add("@BillingPostalCode", SqlDbType.VarChar, 100).Value = BillingPostalCode;
        com.Parameters.Add("@BillingPhone", SqlDbType.VarChar, 100).Value = BillingPhone;
        com.Parameters.Add("@BillingFaxNo", SqlDbType.VarChar, 100).Value = BillingFaxNo;
        com.Parameters.Add("@Mobile", SqlDbType.VarChar, 10).Value = Mobile;
        com.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = EmailID;
        com.Parameters.Add("@Website", SqlDbType.VarChar, 100).Value = Website;
        com.Parameters.Add("@isSchool", SqlDbType.Bit).Value = isSchool;
        com.Parameters.Add("@isStudent", SqlDbType.Bit).Value = isStudent;
        com.Parameters.AddWithValue("@SchoolID", SchoolID ?? (object)DBNull.Value);
        com.Parameters.AddWithValue("@ClassID", ClassID ?? (object)DBNull.Value);
        com.Parameters.AddWithValue("@SectionID", SectionID ?? (object)DBNull.Value);

        com.Parameters.Add("@RollNo", SqlDbType.VarChar, 25).Value = RollNo;
        com.Parameters.AddWithValue("@AgeGroupID", AgeGroupID ?? (object)DBNull.Value);

        com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
        com.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = Remark;
        com.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 15).Value = CreatedBy;
        com.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 15).Value = UpdatedBy;
        com.Parameters.Add("@isTeacher", SqlDbType.Bit).Value = isTeacher;
        com.Parameters.Add("@TeacherID", SqlDbType.VarChar, 100).Value = TeacherID;
        com.Parameters.Add("@TeacherIDproof", SqlDbType.VarChar, 1000).Value = TeacherIDproof;
        com.Parameters.Add("@isApproved", SqlDbType.Bit).Value = isApproved;
        com.Parameters.Add("@UserType", SqlDbType.VarChar, 50).Value = UserType;
        //com.Parameters.Add("@isVerified", SqlDbType.Bit).Value = isVerified;
        com.Parameters.Add("@CustID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
        string errmsg;
        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        if (errmsg == "success")
        {
            String CustID = com.Parameters["@CustID"].Value.ToString();
            if (isSchool)
            {
                CommonCode.show_alert("success", "<i class='fa fa-smile-o'></i>&nbsp;Thank you for Registering your School !", "We`ll verify your details via call or email. Your School account will get activated after your verification.", ltr_msg);
                clear_form();
                Response.Redirect("/welcome.aspx?custid=" + CustID, true);
            }
            else if (isTeacher)
            {
                CommonCode.show_alert("success", "<i class='fa fa-smile-o'></i>&nbsp;Thank you for Registering your Teachers account !", "We`ll verify your details via call or email. After your verification you can login and enjoy offers and discounts.", ltr_msg);
                clear_form();
                Response.Redirect("/welcome.aspx?custid=" + CustID, true);
            }
            else
            {
                CommonCode.show_alert("success", "<i class='fa fa-smile-o'></i>&nbsp;Thank you for Registering your account !", "Login and start exploring our products and offers.", ltr_msg);
                clear_form();
                Session["CustID"] = CustID;
                Session["CustEmail"] = EmailID;
                Session["CustName"] = CustName;
                Session["CustType"] = UserType;

                if (Session["tmpcart"] != null)
                {
                    savetmpcart_to_db(CustID);
                }

                Response.Redirect("/welcome.aspx?custid=" + CustID, true);
            }
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
            Session["CartID"] = CartID;
    }
    private void send_confimation_mail(string mode)
    {
        //string info_errmsg = "", mail_errmsg = "";
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
    }

    protected void dd_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_City.Items.Clear();
        CommonCode.getCountry_Details(2, "0", dd_State.SelectedValue, dd_City);
    }

    protected void dd_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_State.Items.Clear();
        CommonCode.getCountry_Details(1, dd_Country.SelectedValue, "0", dd_State);
    }
}