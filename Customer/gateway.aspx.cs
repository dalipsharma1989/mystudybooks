using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System.Data.SqlClient;



public partial class _payment_gateway : System.Web.UI.Page
{
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string txnid1 = string.Empty;
    public string OrderID = "";
    public void SHASample() { }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            key.Value = ConfigurationManager.AppSettings["MERCHANT_KEY"];
            if (Session["PaymentStart"] != null)
            {
                Session["PaymentStart"] = null;
                Session["OrderID_output"] = null;
                ClientScript.RegisterStartupScript(GetType(), "Alert", "alert('Please Try Again for Place Order');", true);
                Response.Redirect("~/index.aspx");
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["OrderID"]))
                {
                    string UserOrderID = "0", UserID = "0";
                    UserOrderID = Request.QueryString["OrderID"].ToString();
                    UserID = Request.QueryString["CustomerID"].ToString();
                    Load_UserInformation_BasedOnOrderID(Decrypt(HttpUtility.UrlDecode(UserOrderID)), Decrypt(HttpUtility.UrlDecode(UserID)));
                }
                else
                {
                    if (Session["Payment_Variables"] != null)
                    {
                        read_session();
                    }
                    else
                    {
                        CommonCode.show_alert("danger", "Session[Payment_Variables] Expired ", "", ph_msg);
                        return;
                    }
                }                
            }

            if (string.IsNullOrEmpty(Request.Form["hash"]))
                btn_proceed_to_payment.Visible = true;
            else
                btn_proceed_to_payment.Visible = false;
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "Error occured while proccsing payment!", ex.Message, ph_msg);
        }

    }

    private void read_session()
    {
        try
        {
            DataTable dt_Payment_Variables = new DataTable();
            dt_Payment_Variables = Session["Payment_Variables"] as DataTable;
            if (dt_Payment_Variables.Rows.Count > 0)
            {
                float Amount = 0;
                string Firstname, Lastname,
                 Email, Phone, Address, City, State,
                 Country, Zip, Remarks, CheckoutID, UDF1_Currency, UDF2_CurrencySymbol, UDF3_TotalItems,internethanding,totRoundoff, ShiptoAddressID, CourierMasterID = "0";

                string SURL, FURL, CURL, Merchant_Key, ProductInfo, Service_Provider;

                SURL = CommonCode.AppSettings("UrlPath") + "?val=success";
                FURL = CommonCode.AppSettings("UrlPath") + "?val=failure";
                CURL = CommonCode.AppSettings("UrlPath") + "?val=cancel";
                Merchant_Key = ConfigurationManager.AppSettings["MERCHANT_KEY"];

                if (!float.TryParse(dt_Payment_Variables.Rows[0]["Amount"].ToString(), out Amount))
                    Amount = 0;

                Firstname = dt_Payment_Variables.Rows[0]["FirstName"] + "";
                Lastname = dt_Payment_Variables.Rows[0]["LastName"] + "";
                Email = dt_Payment_Variables.Rows[0]["Email"] + "";
                Phone = dt_Payment_Variables.Rows[0]["Phone"] + "";
                Address = dt_Payment_Variables.Rows[0]["Address"] + "";
                City = dt_Payment_Variables.Rows[0]["City"] + "";
                State = dt_Payment_Variables.Rows[0]["State"] + "";
                Country = dt_Payment_Variables.Rows[0]["Country"] + "";
                Zip = dt_Payment_Variables.Rows[0]["Zip"] + "";
                Remarks = dt_Payment_Variables.Rows[0]["Remarks"] + "";
                CheckoutID = dt_Payment_Variables.Rows[0]["CheckoutID"] + "";
                UDF1_Currency = dt_Payment_Variables.Rows[0]["UDF1_Currency"] + "";
                UDF2_CurrencySymbol = dt_Payment_Variables.Rows[0]["UDF2_CurrencySymbol"] + "";
                UDF3_TotalItems = dt_Payment_Variables.Rows[0]["UDF3_TotalItems"] + "";
                ProductInfo = dt_Payment_Variables.Rows[0]["UDF4_ProductInfo"] + "";
                Session["ProductInfo"] = ProductInfo.ToString();
                //internethanding = dt_Payment_Variables.Rows[0]["InternetHandling"] + "";
                //totRoundoff = dt_Payment_Variables.Rows[0]["totalRound"] + "";
                Service_Provider = service_provider.Text;
                ShiptoAddressID = dt_Payment_Variables.Rows[0]["ShipToAccountID"] + "";
               // CourierMasterID = dt_Payment_Variables.Rows[0]["CourierMasterID"] + "";
                // Assign above values to hidden fields and initiate process
                amount.Text = Amount.ToString();
                firstname.Text = Firstname;
                lastname.Text = Lastname;
                email.Text = Email;
                phone.Text = Phone;
                address1.Text = Address;
                city.Text = City;
                state.Text = State;
                country.Text = Country;
                zipcode.Text = Zip;
                remarks.Text = Remarks;

                surl.Text = SURL;
                furl.Text = FURL;
                curl.Text = CURL; 
                key.Value = Merchant_Key;
                //productinfo.Text = ProductInfo;
                productinfo.Text = dt_Payment_Variables.Rows[0]["UDF4_ProductInfo"].ToString().Replace("'","") + ""; 
                
                string db_error_msg = "", OrderID= "";
                 if(Session["OrderID_output"] != null)
                {
                    txtOrderID.Text = Session["OrderID_output"].ToString();
                    txnid.Value = Session["OrderID_output"].ToString();
                }
                else
                {
                    SaveOrder(out OrderID, out db_error_msg);
                }
                
                string errmsgs = "";
                DAL dal = new DAL();
                DataTable dt = new DataTable();
                dt = dal.get_Shipping_Information(Session["CustID"].ToString(), ShiptoAddressID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsgs);                
                if (errmsgs == "success")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtShipEmail.Text = dt.Rows[0]["EmailID"].ToString();
                        txtShipMobile.Text = dt.Rows[0]["Mobile"].ToString();                        
                        txtShipaddress.Text = dt.Rows[0]["ShipAddress"].ToString();
                        txtShipcity.Text = dt.Rows[0]["CityName"].ToString();
                        txtShipstate.Text = dt.Rows[0]["StateName"].ToString();
                        txtShipcountry.Text = dt.Rows[0]["CountryName"].ToString();
                        txtShipzip.Text = dt.Rows[0]["ShipPostalCode"].ToString();
                    }
                    else
                    {
                        CommonCode.show_alert("info", "No Shipping Address Found", "", false, ph_msg);
                    }
                } 
            }
            else
            {
                CommonCode.show_alert("danger", "Datatable is empty !", "", ph_msg);
            }
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "Error while processing Payment !", ex.Message, ph_msg);
        }
    }

    private void Load_UserInformation_BasedOnOrderID(string OrderID, string UserID) 
    {
        try
        {
            DAL dal = new DAL(); 
            DataTable dt_Payment_Variables = new DataTable(); 
            dt_Payment_Variables = dal.Get_order_Detail_by_OrderID(UserID, OrderID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), Session["FinancialPeriod"].ToString());
              
            if (dt_Payment_Variables.Rows.Count > 0)
            {
                float Amount = 0;
                string Firstname, Lastname, Email, Phone, Address, City, State, Country, Zip, Remarks, CheckoutID, UDF1_Currency, UDF2_CurrencySymbol, 
                    UDF3_TotalItems, internethanding, totRoundoff, ShiptoAddressID, CourierMasterID = "0";

                string SURL, FURL, CURL, Merchant_Key, ProductInfo, Service_Provider;

                SURL = CommonCode.AppSettings("UrlPath") + "?val=success";
                FURL = CommonCode.AppSettings("UrlPath") + "?val=failure";
                CURL = CommonCode.AppSettings("UrlPath") + "?val=cancel";
                Merchant_Key = ConfigurationManager.AppSettings["MERCHANT_KEY"];

                if (!float.TryParse(dt_Payment_Variables.Rows[0]["Amount"].ToString(), out Amount))
                    Amount = 0; 
                Firstname = dt_Payment_Variables.Rows[0]["FirstName"] + "";
                Lastname = dt_Payment_Variables.Rows[0]["LastName"] + "";
                Email = dt_Payment_Variables.Rows[0]["Email"] + "";
                Phone = dt_Payment_Variables.Rows[0]["Phone"] + "";
                Address = dt_Payment_Variables.Rows[0]["Address"] + "";
                City = dt_Payment_Variables.Rows[0]["City"] + "";
                State = dt_Payment_Variables.Rows[0]["State"] + "";
                Country = dt_Payment_Variables.Rows[0]["Country"] + "";
                Zip = dt_Payment_Variables.Rows[0]["Zip"] + "";
                Remarks = dt_Payment_Variables.Rows[0]["Remarks"] + "";
                CheckoutID = dt_Payment_Variables.Rows[0]["CheckoutID"] + "";
                UDF1_Currency = dt_Payment_Variables.Rows[0]["UDF1_Currency"] + "";
                UDF2_CurrencySymbol = dt_Payment_Variables.Rows[0]["UDF2_CurrencySymbol"] + "";
                UDF3_TotalItems = dt_Payment_Variables.Rows[0]["UDF3_TotalItems"] + "";
                ProductInfo = dt_Payment_Variables.Rows[0]["UDF4_ProductInfo"] + "";
                Session["ProductInfo"] = ProductInfo.ToString(); 
                Service_Provider = service_provider.Text;
                ShiptoAddressID = dt_Payment_Variables.Rows[0]["ShipToAccountID"] + ""; 
                amount.Text = Amount.ToString();
                firstname.Text = Firstname;
                lastname.Text = Lastname;
                email.Text = Email;
                phone.Text = Phone;
                address1.Text = Address;
                city.Text = City;
                state.Text = State;
                country.Text = Country;
                zipcode.Text = Zip;
                remarks.Text = Remarks; 
                surl.Text = SURL;
                furl.Text = FURL;
                curl.Text = CURL;
                key.Value = Merchant_Key; 
                productinfo.Text = dt_Payment_Variables.Rows[0]["UDF4_ProductInfo"].ToString().Replace("'", "") + "";
                txnid.Value = OrderID;
                txtOrderID.Text = OrderID;

                string errmsgs = "";
                DataTable dt = new DataTable();

                dt = dal.get_Shipping_Information(Session["CustID"].ToString(), ShiptoAddressID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsgs);
                if (errmsgs == "success")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtShipEmail.Text = dt.Rows[0]["EmailID"].ToString();
                        txtShipMobile.Text = dt.Rows[0]["Mobile"].ToString();
                        txtShipaddress.Text = dt.Rows[0]["ShipAddress"].ToString();
                        txtShipcity.Text = dt.Rows[0]["CityName"].ToString();
                        txtShipstate.Text = dt.Rows[0]["StateName"].ToString();
                        txtShipcountry.Text = dt.Rows[0]["CountryName"].ToString();
                        txtShipzip.Text = dt.Rows[0]["ShipPostalCode"].ToString();
                    }
                    else
                    {
                        CommonCode.show_alert("info", "No Shipping Address Found", "", false, ph_msg);
                    }
                }
            }
            else
            {
                CommonCode.show_alert("danger", "Datatable is empty !", "", ph_msg);
            }
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "Error while processing Payment !", ex.Message, ph_msg);
        }
    }

    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                Session["CustID"] = null;
                Response.Redirect("~/order_track.aspx");
            }
        }
        return cipherText;
    }



    public void SaveOrder(out string OrderID, out string db_error_msg)
    {
        DAL dal = new DAL();
        String OtherCountry = "";
        string errmsg = "";
        try
        {
            DataTable dt_Payment_Variables = new DataTable();
            dt_Payment_Variables = Session["Payment_Variables"] as DataTable;
            if (dt_Payment_Variables.Rows.Count > 0)
            {
                float Amount = 0;
                string Firstname, Lastname, Email, Phone, Address, City, State, Country, Zip, Remarks, CheckoutID, UDF1_Currency, UDF2_CurrencySymbol, UDF3_TotalItems,
                    ProductInfo, AddressID_OutPut, shipCost;
                if (!float.TryParse(dt_Payment_Variables.Rows[0]["Amount"].ToString(), out Amount))
                    Amount = 0;
                Firstname = dt_Payment_Variables.Rows[0]["FirstName"] + "";
                Lastname = dt_Payment_Variables.Rows[0]["LastName"] + "";
                Email = dt_Payment_Variables.Rows[0]["Email"] + "";
                Phone = dt_Payment_Variables.Rows[0]["Phone"] + "";
                Address = dt_Payment_Variables.Rows[0]["Address"] + "";
                City = dt_Payment_Variables.Rows[0]["City"] + "";
                State = dt_Payment_Variables.Rows[0]["State"] + "";
                Country = dt_Payment_Variables.Rows[0]["Country"] + "";
                Zip = dt_Payment_Variables.Rows[0]["Zip"] + "";
                Remarks = dt_Payment_Variables.Rows[0]["Remarks"] + "";
                CheckoutID = dt_Payment_Variables.Rows[0]["CheckoutID"] + "";
                UDF1_Currency = dt_Payment_Variables.Rows[0]["UDF1_Currency"] + "";
                UDF2_CurrencySymbol = dt_Payment_Variables.Rows[0]["UDF2_CurrencySymbol"] + "";
                UDF3_TotalItems = dt_Payment_Variables.Rows[0]["UDF3_TotalItems"] + "";
                ProductInfo = dt_Payment_Variables.Rows[0]["UDF4_ProductInfo"] + "";
                AddressID_OutPut = dt_Payment_Variables.Rows[0]["ShipToAccountID"] + "";
                shipCost = dt_Payment_Variables.Rows[0]["ShippingAmount"] + "";
                if (Session["OtherCountry"] != null)
                {
                    OtherCountry = Session["OtherCountry"].ToString();
                }
                else
                {
                    OtherCountry = "";
                }

                dal.insert_order(CheckoutID, UDF3_TotalItems, Amount.ToString(), shipCost, "ShipMethod", Remarks, "0", "Payment Pending", "", "CardType", "NameOnCard", "CardNum", 
                    "CradExpire", "0", OtherCountry, false, "0", Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), Session["FinancialPeriod"].ToString(), Session["CustID"].ToString(), 
                    AddressID_OutPut, out OrderID, out errmsg);
                txtOrderID.Text = OrderID.ToString();
                Session["OrderID_output"] = OrderID;
                txnid.Value = OrderID;
            }
            else
            {
                errmsg = "Payment_Varibles Datatable is empty";
                OrderID = "";
            }
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            OrderID = "";
        }
        db_error_msg = errmsg;
    }

    public string Generatehash512(string text)
    {

        byte[] message = Encoding.UTF8.GetBytes(text);

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();
        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
 
    protected void btn_proceed_to_payment_Click(object sender, EventArgs e)
    {
        try
        {
            //if (chkDelivery.Checked == false)
            // {
            //    CommonCode.show_alert("danger", "your didnot select delivery terms", "Please accept delivery date by clicking on check box", ph_msg);
            //    ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('your didnot select delivery terms !, Please accept delivery date by clicking on check box');", true);
            //    return;
            //}
            //else
            //{
            //    string errd = "";
            //    DAL dalc = new DAL();
            //    DataTable dtC = new DataTable();
            //    dtC = dalc.get_shipping_amount_details(Session["school"].ToString(), Session["ClassID"].ToString(), Session["BundleID"].ToString(), Session["CourierMasterID"].ToString(), Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errd);
            //    DateTime deliverydate;
            //    if (dtC.Rows.Count > 0)
            //    {
            //        if (!CommonCode.CheckDate(dtC.Rows[0]["DeliveryDate"]+"", out deliverydate))
            //        {
            //            CommonCode.show_alert("warning", "Delivery Date is not valid!", "Please select a valid date", ph_msg);
            //            return;
            //        }                     
            //        string errmsg;
            //        dalc.UpdateOrderDeliverydate(Session["OrderID_output"].ToString(), Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), deliverydate, out errmsg);

            //        if (errmsg == "success")
            //        {
            //            CommonCode.show_alert("success", "your delivery date accepted", "Thank you for Placing Order", ph_msg);
            //        }
            //        else
            //        {
            //            CommonCode.show_alert("danger", "your delivery date Not accepted", "Please contact Site Administrator", ph_msg);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('delivery date Not available !, Please contact Site Administrator');", true);
            //        CommonCode.show_alert("danger", "delivery date Not available", "Please contact Site Administrator", ph_msg);
            //        return;
            //    }                
            //}
            string[] hashVarsSeq;
            string hash_string = string.Empty;

            if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
            {
                //Random rnd = new Random();
                //string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                //txnid1 = strHash.ToString().Substring(0, 20);
                txnid1 = Session["OrderID_output"].ToString();
            }
            else
            {
                txnid1 = Request.Form["txnid"];
            }
            
            if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
            {
                if (ConfigurationManager.AppSettings["PAYU_BASE_URL"] == "https://test.payu.in/_payment")
                    service_provider.Text = "";
                else
                    service_provider.Text = "payu_paisa";

                string empty_msg = "";
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["MERCHANT_KEY"]))
                    empty_msg += "MERCHANT_KEY, ";
                if (string.IsNullOrEmpty(txnid1))
                    empty_msg += "TXNID, ";
                if (string.IsNullOrEmpty(Request.Form["amount"]))
                    empty_msg += "Amount, ";
                if (string.IsNullOrEmpty(Request.Form["firstname"]))
                    empty_msg += "First Name, ";
                if (string.IsNullOrEmpty(Request.Form["email"]))
                    empty_msg += "Email, ";
                if (string.IsNullOrEmpty(Request.Form["phone"]))
                    empty_msg += "Phone, ";
                if (string.IsNullOrEmpty(Request.Form["productinfo"]))
                    empty_msg += "Product Info, ";
                if (string.IsNullOrEmpty(Request.Form["surl"]))
                    empty_msg += "Surl, ";
                if (string.IsNullOrEmpty(Request.Form["furl"]))
                    empty_msg += "Furl, ";
                if (string.IsNullOrEmpty(Request.Form["curl"]))
                    empty_msg += "Curl, ";

                if (empty_msg != "")
                {
                    string values = empty_msg.Substring(0, empty_msg.Length - 2) + " empty fields";
                    CommonCode.show_alert("danger", "Check for empty fields", values, ph_msg);
                    return;
                }

                else
                {
                    udf1.Text = email.Text.Trim();
                    udf2.Text = Session["FinancialPeriod"].ToString();
                    udf3.Text = Session["CustID"].ToString();
                    udf4.Text = Session["iBranchID"].ToString();
                    udf5.Text = Session["iCompanyId"].ToString();

                    hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
                    hash_string = "";
                    string amunt = "1.00";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        if (hash_var == "key")
                        {
                            hash_string = hash_string + ConfigurationManager.AppSettings["MERCHANT_KEY"];
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "txnid")
                        {
                            hash_string = hash_string + txnid1;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "amount")
                        {
                            if (ConfigurationManager.AppSettings["LocalPrice"].ToString() == "Y")
                            {
                                hash_string = hash_string + amunt;
                            }
                            else
                            {
                                hash_string = hash_string + Convert.ToDecimal(Request.Form[hash_var]).ToString("g29");
                            } 
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf1")
                        {
                            hash_string = hash_string + email.Text.Trim();
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf2")
                        {
                            hash_string = hash_string + Session["FinancialPeriod"].ToString();
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf3")
                        {
                            hash_string = hash_string + Session["CustID"].ToString();
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf4")
                        {
                            hash_string = hash_string + Session["iBranchID"].ToString();
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf5")
                        {
                            hash_string = hash_string + Session["iCompanyId"].ToString();
                            hash_string = hash_string + '|';
                        }
                        else
                        {

                            hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                            hash_string = hash_string + '|';
                        }
                    }

                    hash_string += ConfigurationManager.AppSettings["MERCHANT_SALT"];               // appending SALT
                    hash1 = Generatehash512(hash_string).ToLower();                                 //generating hash
                    action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"].ToString() ;        //+ "/_payment";// setting URL

                    Session["PaymentStart"] = "FlyForPayment";
                }
            }

            else if (!string.IsNullOrEmpty(Request.Form["hash"]))
            {
                hash1 = Request.Form["hash"];
                action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";

            }

            if (!string.IsNullOrEmpty(hash1))
            {
                hash.Value = hash1;
                txnid.Value = txnid1;

                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                data.Add("hash", hash.Value);
                //data.Add("hash_abc", hash_string);
                data.Add("txnid", txnid.Value);
                data.Add("key", key.Value);
                string AmountForm = Convert.ToDecimal(amount.Text.Trim()).ToString("g29");// eliminating trailing zeros
                amount.Text = AmountForm;
                if (ConfigurationManager.AppSettings["LocalPrice"].ToString()=="Y")
                {
                    AmountForm = "1.00";
                    data.Add("amount", AmountForm);
                }
                else
                { 
                    data.Add("amount", AmountForm);
                } 

                data.Add("firstname", firstname.Text.Trim());
                data.Add("email", email.Text.Trim());
                data.Add("phone", phone.Text.Trim());
                data.Add("productinfo", productinfo.Text);
                data.Add("surl", surl.Text.Trim() +"&TransactionID="+ HttpUtility.UrlEncode(Encrypt(txnid.Value.ToString())));
                data.Add("furl", furl.Text.Trim() + "&TransactionID=" + HttpUtility.UrlEncode(Encrypt(txnid.Value.ToString())));
                data.Add("lastname", lastname.Text.Trim());
                data.Add("curl", curl.Text.Trim() + "&TransactionID=" + HttpUtility.UrlEncode(Encrypt(txnid.Value.ToString())));
                data.Add("address1", address1.Text.Trim());
                data.Add("address2", address2.Text.Trim());
                data.Add("city", city.Text.Trim());
                data.Add("state", state.Text.Trim());
                data.Add("country", country.Text.Trim());
                data.Add("zipcode", zipcode.Text.Trim());
                data.Add("udf1", udf1.Text.Trim());
                data.Add("udf2", udf2.Text.Trim());
                data.Add("udf3", udf3.Text.Trim());
                data.Add("udf4", udf4.Text.Trim());
                data.Add("udf5", udf5.Text.Trim());
                data.Add("pg", pg.Text.Trim());
                //data.Add("service_provider", service_provider.Text);

                string strForm = PreparePOSTForm(action1, data);
                Page.Controls.Add(new LiteralControl(strForm));
            }
            else
            {
                //no hash
            }
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "Error", ex.Message, ph_msg);
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

    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        foreach (System.Collections.DictionaryEntry key in data)
        {

            strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                           "\" value=\"" + key.Value + "\">");
        }


        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }
}
