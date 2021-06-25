using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Mail;

public partial class Customer_directcheckoutset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Checkout");

        if (Session["CustID"] != null && Session["CustType"] != null && Session["CustID"].ToString() != "guest")
        {
            if (!IsPostBack)
            {
                string str_SchoolID = "", str_ClassID = "", str_SetID = "", str_SetDesc = "";
                CommonCode.getCountry_Details(0, "", "", bill_dd_Country);
                CommonCode.getCountry_Details(0, "", "", dd_new_ship_country);
                load_user_details();
                load_shipping_locations();
                if (!string.IsNullOrEmpty(Request.QueryString["School"]))
                {
                    str_SchoolID = Request.QueryString["School"].ToString();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["Class"]))
                {
                    str_ClassID = Request.QueryString["Class"].ToString();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["SetId"]))
                {
                    str_SetID = Request.QueryString["SetId"].ToString();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["SetName"]))
                {
                    str_SetDesc = Request.QueryString["SetName"].ToString();
                }
                Get_SetInformation(str_SchoolID, str_ClassID, str_SetID, str_SetDesc);
            }
        }
    }

    private void Get_SetInformation(string schoolID, String ClassID, String SetID, String SetDesc)
    {
        DAL dal = new DAL();
        DataTable dt = new DataTable();

        dt = dal.Get_Set_Information_By_School_Class_Set(schoolID, ClassID, SetID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), "");
        if (dt.Rows.Count > 0)
        {
            Decimal totalNetAmt = 0;
            foreach (DataRow dr in dt.Rows)
            {
                totalNetAmt += Convert.ToDecimal(dr["NetAmount"].ToString());
            }
            b_total_amount.InnerHtml = totalNetAmt.ToString();
            hf_total_amount.Value = totalNetAmt.ToString();
            //DataView dtnewdataviewBookCategory = new DataView(dt.DefaultView.ToTable("Categorytbl", true, "BookCategoryID", "BookCategoryDesc"));
            Cart_Subtotal.InnerHtml = totalNetAmt.ToString();
            // rp_SetInformation.DataSource = dtnewdataviewBookCategory.ToTable();
            rp_cart.DataSource = dt;
            rp_cart.DataBind();
        }
        else
        {
            rp_cart.DataSource = null;
            rp_cart.DataBind();
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
                bill_textName.Text = dt.Rows[0]["CustName"].ToString();
                bill_textPhone.Text = dt.Rows[0]["Mobile"].ToString();
                bill_textEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                bill_textAdress.Text = dt.Rows[0]["BillingAddress"].ToString();
                bill_textZipCode.Text = dt.Rows[0]["BillingPostalCode"].ToString();
                if (dt.Rows[0]["CountryID"].ToString() == "")
                {
                    bill_dd_Country.SelectedIndex = bill_dd_Country.Items.IndexOf(bill_dd_Country.Items.FindByValue(ConfigurationManager.AppSettings["DefaultCountryCode"].ToString()));
                }
                else
                {
                    bill_dd_Country.SelectedIndex = bill_dd_Country.Items.IndexOf(bill_dd_Country.Items.FindByValue(dt.Rows[0]["CountryID"].ToString()));
                }

                CommonCode.getCountry_Details(1, bill_dd_Country.SelectedValue, "", bill_dd_State);
                bill_dd_State.SelectedIndex = bill_dd_State.Items.IndexOf(bill_dd_State.Items.FindByValue(dt.Rows[0]["StateID"].ToString()));
                CommonCode.getCountry_Details(2, "", bill_dd_State.SelectedValue, bill_dd_City);
                bill_dd_City.SelectedIndex = bill_dd_City.Items.IndexOf(bill_dd_City.Items.FindByValue(dt.Rows[0]["CityID"].ToString()));
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading user details", errmsg, ph_msg);
        }
    }

    protected void bill_dd_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        bill_dd_State.Items.Clear();
        bill_dd_State.Items.Add(new ListItem("Select State", "Nil"));
        CommonCode.getCountry_Details(1, bill_dd_Country.SelectedValue, "", bill_dd_State);
        bill_dd_Country.Focus();
    }

    protected void bill_dd_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        bill_dd_City.Items.Clear();
        bill_dd_City.Items.Add(new ListItem("Select City", "Nil"));
        CommonCode.getCountry_Details(2, "", bill_dd_State.SelectedValue, bill_dd_City);
    }

    protected void bill_dd_City_SelectedIndexChanged(object sender, EventArgs e)
    {
       // updateShippingAmount(hf_cartID.Value, bill_dd_City.SelectedValue);
    }

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

    protected void btn_place_order_Click(object sender, EventArgs e)
    {
        DAL dal = new DAL();

        if (bill_textName.Text.Trim() == "")
        {
            CommonCode.show_alert("danger", "Name Can't allowed blank", "", ltr_bill_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Name Can't allowed blank');", true);
            return;
        }
        if (bill_textAdress.Text.Trim() == "")
        {
            CommonCode.show_alert("danger", "Address Can't allowed blank", "", ltr_bill_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Address Can't allowed blank');", true);
            return;
        }
        if (bill_dd_City.SelectedValue == "Nil")
        {
            CommonCode.show_alert("danger", "Please Select Country, State and City", "", ltr_bill_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Select Country, State and City');", true);
            return;
        }
        if (bill_textPhone.Text.Trim() == "")
        {
            CommonCode.show_alert("danger", "Please Enter Phone / Mobile No", "", ltr_bill_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Phone / Mobile No');", true);
            return;
        }
        if (bill_textEmailID.Text.Trim() == "")
        {
            CommonCode.show_alert("danger", "Please Enter EmailID", "", ltr_bill_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter EmailID');", true);
            return;
        }

        string Custerrmsg = "";
        register_user(bill_textName.Text.Trim(), "", bill_textAdress.Text.Trim(), bill_dd_City.SelectedValue, bill_textZipCode.Text.Trim(), bill_textPhone.Text.Trim(), bill_textPhone.Text.Trim(), bill_textEmailID.Text.Trim(), "", "", "", "Individual",
            Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), "1", "", "", "", "", out Custerrmsg);
        if (Custerrmsg != "Record Updated Successfully!")
        {
            CommonCode.show_alert("danger", "Error while updating your info", Custerrmsg, ltr_bill_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Error while updating your info');", true);
            return;
        }

        string AddressID_output = "";

        if (chkAcceptTerms.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please accept Terms and Conditions for placing order');", true);
            return;
        }

        if (cb_copy_bill_to_ship.Checked)
        {
            
            string errmsg = "";
            dal.insert_update_delete_shipping_address("0", Session["CustID"].ToString(), bill_textAdress.Text, bill_dd_City.SelectedValue, "0", bill_textZipCode.Text,
                bill_textPhone.Text, "", bill_textPhone.Text, bill_textEmailID.Text, false, 0, Session["iCompanyId"].ToString(), out AddressID_output, out errmsg);
            if (errmsg != "success")
            {
                if (errmsg != "Shipping Address already exists !")
                {
                    CommonCode.show_alert("danger", "Error while saving Shipping Location", errmsg, ltr_bill_msg);
                    ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Error while saving Shipping Location');", true);
                    return;
                }
            }
        }
        else
        {
            if (rp_ship_addresses.Items.Count > 0)
            { 
                string errmsg = "";
                Boolean CheckRec = false;
                String ship_address = "", ship_postalcode = "", ship_email = "", ship_mobile = "", ship_cityID = "";
                foreach (RepeaterItem ri in rp_ship_addresses.Items)
                {
                    RadioButton rb_ship_here = ri.FindControl("rb_ship_here") as RadioButton;
                    HiddenField hf_ship_address = ri.FindControl("hf_ship_address") as HiddenField;
                    HiddenField hf_ship_postalcode = ri.FindControl("hf_ship_postalcode") as HiddenField;
                    HiddenField hf_ship_email = ri.FindControl("hf_ship_email") as HiddenField;
                    HiddenField hf_ship_phone = ri.FindControl("hf_ship_phone") as HiddenField;
                    HiddenField hf_ship_cityid = ri.FindControl("hf_ship_cityid") as HiddenField;
                    HiddenField hf_addressID = ri.FindControl("hf_addressID") as HiddenField;
                    if (rb_ship_here.Checked)
                    {
                        ship_address = hf_ship_address.Value;
                        ship_postalcode = hf_ship_postalcode.Value;
                        ship_email = hf_ship_email.Value;
                        ship_mobile = hf_ship_phone.Value;
                        ship_cityID = hf_ship_cityid.Value;
                        AddressID_output = hf_addressID.Value;
                        CheckRec = true;
                        break;
                    }
                    else
                    {
                        CheckRec = false;
                    }
                }
                if (CheckRec == false)
                {
                    CommonCode.show_alert("warning", "No Shipping Location selected !", "Please select a Shipping Location" + errmsg, ltr_bill_msg);
                    ScriptManager.RegisterStartupScript(this, GetType(), "warning", "alert('No Shipping Location selected !, Please select a Shipping Location ');", true);
                    //CommonCode.show_alert("warning", "No Shipping Location selected !", "Please select a Shipping Location", ltr_bill_msg);
                    return;
                }
            }
            else
            {
                CommonCode.show_alert("warning", "No Shipping Location selected !", "Please select a Shipping Location", ltr_bill_msg);
                ScriptManager.RegisterStartupScript(this, GetType(), "warning", "alert('No Shipping Location selected !, Please select a Shipping Location ');", true);
                return;
            }
        }

        string str_SchoolID = "", str_ClassID = "", str_SetID = "", str_SetDesc = "" , str_StudentName = "", str_StudentRollNo = "";

        if (!string.IsNullOrEmpty(Request.QueryString["School"]))
        {
            str_SchoolID = Request.QueryString["School"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["Class"]))
        {
            str_ClassID = Request.QueryString["Class"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["SetId"]))
        {
            str_SetID = Request.QueryString["SetId"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["SetName"]))
        {
            str_SetDesc = Request.QueryString["SetName"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["studname"]))
        {
            str_StudentName = Request.QueryString["studname"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["studRoll"]))
        {
            str_StudentRollNo = Request.QueryString["studRoll"].ToString();
        }

        DataTable dtOrder = new DataTable(); 
        dtOrder = dal.insert_order_for_school_set(Session["CustID"].ToString(), str_SchoolID, str_ClassID, str_SetID, Session["iCompanyId"].ToString(),
            Session["iBranchID"].ToString(), AddressID_output, Session["FinancialPeriod"].ToString() , str_StudentName, str_StudentRollNo);

        if (dtOrder.Rows.Count > 0)
        {
            if (dtOrder.Rows[0]["RESULT"].ToString() == "OK")
            {
                Response.Redirect("gateway.aspx?OrderID=" + HttpUtility.UrlEncode(Encrypt(dtOrder.Rows[0]["TRANSACTIONID"].ToString()))+ "&CustomerID=" + HttpUtility.UrlEncode(Encrypt(dtOrder.Rows[0]["BILLTOACCOUNTID"].ToString())));
            }
            else
            {
                CommonCode.show_alert("Error", "Failed to Place Order ", dtOrder.Rows[0]["RESULT"].ToString(), ltr_bill_msg);
                ScriptManager.RegisterStartupScript(this, GetType(), "warning", "alert('Failed to Place Order !, "+ dtOrder.Rows[0]["RESULT"].ToString() + ", Please contact site Administrator ');", true);
                return;
            } 
        }
        else
        {
            CommonCode.show_alert("Error", "Failed to Place Order ", "Please contact site Administrator", ltr_bill_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "warning", "alert('Failed to Place Order ! , Please contact site Administrator ');", true);
            return;
        } 
    }

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    protected void btn_Save_new_shipping_address_Click(object sender, EventArgs e)
    {
        save_shipping_location();
    }

    protected void dd_new_ship_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_new_ship_city.Items.Clear();
        CommonCode.getCountry_Details(2, "", dd_new_ship_state.SelectedValue, dd_new_ship_city);
        dd_new_ship_state.Focus();
    }

    protected void dd_new_ship_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_new_ship_state.Items.Clear();
        CommonCode.getCountry_Details(1, dd_new_ship_country.SelectedValue, "", dd_new_ship_state);
        dd_new_ship_country.Focus();
    }

    private void save_shipping_location()
    {
        DAL dal = new DAL();
        string errmsg = "", AddressID_output = "";
        dal.insert_update_delete_shipping_address("0", Session["CustID"].ToString(), text_new_shippingAddress.Text, dd_new_ship_city.SelectedValue, "0",
        text_new_ship_PostalCode.Text, text_new_ship_Mobile.Text, "", text_new_ship_Mobile.Text, text_new_ship_EmailID.Text, cb_make_default.Checked, 0,
        Session["iCompanyId"].ToString(), out AddressID_output, out errmsg);
        if (errmsg == "success")
        {
            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            load_shipping_locations();
            CommonCode.show_toastr("success", "Shipping Address Saved !", "", false, "", false, "", ltr_scripts);
        }
        else
        {
            CommonCode.show_toastr("error", "Error while saving Shipping Location!", errmsg.Replace('\'', ' '), false, "", false, "", ltr_scripts);
        }

    }


    private void load_shipping_locations()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_Shipping_Information(Session["CustID"].ToString(), "0", Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                rp_ship_addresses.DataSource = dt;
                rp_ship_addresses.DataBind();
            }
            else
            {
                CommonCode.show_alert("info", "Item will be delievered to the Billing Address", "", false, ltr_ship_alert_msg);
            }
        }
        else
        {
            CommonCode.show_alert("info", "Item will be delievered to the Billing Address", "", false, ltr_ship_alert_msg);
        }
    }


    protected void rp_ship_addresses_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string AddressID = e.CommandArgument.ToString();
        if (e.CommandName == "delete_ship_address")
        {
            DAL dal = new DAL();
            String errmsg = "", AddressID_output = "";
            dal.insert_update_delete_shipping_address(AddressID, "0", "", "0", "0", "", "", "", "", "", false, 2, Session["iCompanyId"].ToString(), out AddressID_output, out errmsg);
            if (errmsg == "success")
            {
                load_shipping_locations();
                ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Shipping Address Deleted !');window.location ='"+ HttpContext.Current.Request.Url.ToString() + "';", true);
                CommonCode.show_toastr("success", "Shipping Address Deleted !", "", false, "", false, "", ltr_scripts);
            }
            else
            {
                CommonCode.show_toastr("error", "Error while deleting Shipping Location!", errmsg.Replace('\'', ' '), false, "", false, "", ltr_scripts);
            }
        }
    }

    protected void rb_ship_here_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rb1 = (RadioButton)sender;

        foreach (RepeaterItem ri in rp_ship_addresses.Items)
        {
            RadioButton rb_ship_here = ri.FindControl("rb_ship_here") as RadioButton;
            if (rb_ship_here != null && rb_ship_here.Checked)
            {
                rb_ship_here.Checked = false;
            }
        }
        rb1.Checked = true;
    }
}