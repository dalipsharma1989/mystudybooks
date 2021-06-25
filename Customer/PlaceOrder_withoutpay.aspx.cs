using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class _payment_gatewaywithoutpay : System.Web.UI.Page
{
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    public string txnid1 = string.Empty;

    public string TransactionID_C;

    public String Product_C, Amount_C, FirstName_C, LastName_C, Email_C, Phone_C, Mobile_C, Address1_C, Address2_C, City_C, State_C, Country_C, ZipCode_C, Remarks_C,
    UDF1_C, UDF2_C, UDF3_C, UDF4_C, UDF5_C, Status_C, Mode_C, ErrorCode_C, PG_TYPE_C, Bank_Ref_Num_C, PayUMoneyID_C, AdditionalCharges_C, 
        tid_C, merchant_id_C, order_id_C, amount_C, currency_C, redirect_url_C, cancel_url_C, language_C, billing_name_C, billing_address_C, billing_city_C, 
        billing_state_C, billing_zip_C, billing_country_C, billing_tel_C, billing_email_C, delivery_name_C, delivery_address_C, delivery_city_C, delivery_state_C, 
        delivery_zip_C, delivery_country_C, delivery_tel_C, merchant_param1_C, merchant_param2_C, merchant_param3_C, merchant_param4_C, merchant_param5_C, promo_code_C, 
        customer_identifier_C;
    public Boolean Courier ;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             
            merchant_id.Value = ConfigurationManager.AppSettings["MERCHANT_KEY"];

            if (Session["RefNO"] != null)
            {
                Session["RefNO"] = null;
                Response.Redirect("user_login.aspx?session_expired=true", true);
            }

            if (!IsPostBack)
            {
                if (Session["CustID"] != null && Session["CustID"].ToString() != "guest")
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
                else
                {
                    Response.Redirect("user_login.aspx?session_expired=true", true);
                }
            }

            //if (string.IsNullOrEmpty(Request.Form["hash"]))
            //    btn_proceed_to_payment.Visible = true;
            //else
            //    btn_proceed_to_payment.Visible = false;
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "Error occured while proccesing payment!", ex.Message, ph_msg);
        }
    }

   

    private void TestPayment()
    {
        try
        { 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://secure.telr.com/");
                client.DefaultRequestHeaders.ExpectContinue = false;
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var result = client.PostAsync("gateway/order.json",
                    new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
                    {
                new KeyValuePair<string, string>("ivp_method", "create"),
                new KeyValuePair<string, string>("ivp_store", "23847"),
                new KeyValuePair<string, string>("ivp_authkey", "QZg5@RgVfW-WVSN8"),
                new KeyValuePair<string, string>("ivp_cart", "5454547"),
                new KeyValuePair<string, string>("ivp_desc", "Testing Order"),
                new KeyValuePair<string, string>("ivp_test", "1"),
                new KeyValuePair<string, string>("ivp_amount", "1.00"),
                new KeyValuePair<string, string>("ivp_currency", "AED"),
                new KeyValuePair<string, string>("return_auth", "http://localhost:62780/Customer/PaymentResponse.aspx"),
                new KeyValuePair<string, string>("return_can", "http://localhost:62780/Customer/PaymentResponse.aspx"),
                new KeyValuePair<string, string>("return_decl", "http://localhost:62780/Customer/PaymentResponse.aspx"),
                new KeyValuePair<string, string>("bill_title", ""),
                new KeyValuePair<string, string>("bill_fname", "Dalip"),
                new KeyValuePair<string, string>("bill_sname", "Sharma"),
                new KeyValuePair<string, string>("bill_addr1", "G-27, Sector-3, Spring Time Software"),
                new KeyValuePair<string, string>("bill_city", "Noida"),
                new KeyValuePair<string, string>("bill_region","Utter Pradesh"),
                new KeyValuePair<string, string>("bill_country", "INDIA"),
                new KeyValuePair<string, string>("bill_zip", "201301"),
                new KeyValuePair<string, string>("bill_email", "dalip@springtimesoftware.net"),
                new KeyValuePair<string,string>("ivp_update_url","www.urdomain.com"),
                new KeyValuePair<string,string>("ivp_framed","0"),
                    })).Result;
                if (result.IsSuccessStatusCode)
                {
                    string data = (result.Content.ReadAsStringAsync().Result);
                    data = JsonConvert.DeserializeObject(data).ToString();
                    string[] arrayResponse = data.Split(':');

                    string[] arr = arrayResponse[4].Split('\"');
                    string val = arr[1];

                    Session["RefNO"] = val;
                    string URL = "https://secure.telr.com/gateway/process.html?o="+val; 
                    Response.Redirect(URL);
                } 
            }
        }
        catch (AggregateException err)
        {
            foreach (var errInner in err.InnerExceptions)
            {
                Debug.WriteLine(errInner); //this will call ToString() on the inner execption and get you message, stacktrace and you could perhaps drill down further into the inner exception of it if necessary 
            }
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
                 Country, Zip, Remarks, CheckoutID, UDF1_Currency, UDF2_CurrencySymbol, UDF3_TotalItems,SessShippingAmount;
                 
                string SURL, FURL, CURL, Merchant_Key, ProductInfo, Service_Provider;

                SURL = CommonCode.AppSettings("UrlPath") + "PayResponse.aspx?val=success";
                FURL = CommonCode.AppSettings("UrlPath") + "PayResponse.aspx?val=failure";
                CURL = CommonCode.AppSettings("UrlPath") + "PayResponse.aspx?val=cancel";
                Merchant_Key = ConfigurationManager.AppSettings["MERCHANT_KEY"];

                if (!float.TryParse(string.Format(CommonCode.AmountFormat(), dt_Payment_Variables.Rows[0]["Amount"].ToString()), out Amount))
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
                SessShippingAmount = dt_Payment_Variables.Rows[0]["ShippingAmount"] + "";
                tid.Value = DateTime.Now.ToString("yyyyMMddHHmmssff"); 
                merchant_id.Value = Merchant_Key;
                Courier = Convert.ToBoolean(dt_Payment_Variables.Rows[0]["courier"].ToString());  
                if (ConfigurationManager.AppSettings["LocalPrice"] == "Y")
                {
                    amount.Text = Convert.ToDecimal(ConfigurationManager.AppSettings["AmountforTesting"] ).ToString("f2");
                    //Convert.ToDecimal(Request.Form[hash_var]).ToString("g29");
                }
                else
                {
                    amount.Text = Amount.ToString("f2");
                }                
                //Orderid.Value = ""  ;


                

                String OtherCountry = "";
                if (Session["OtherCountry"] != null)
                {
                    OtherCountry = Session["OtherCountry"].ToString();
                }
                else
                {
                    OtherCountry = "";
                }

                if (OtherCountry=="INDIA")
                {
                    currency.Text = "AED";
                }
                else
                {
                    currency.Text = "USD";
                }


                redirect_url.Value = CommonCode.AppSettings("UrlPath");// + "PayResponse.aspx?val=success";
                cancel_url.Value = CommonCode.AppSettings("RetUrlPath");// + "PayResponse.aspx?val=cancel";
                //  Billing information(optional):
                billing_name.Text = Firstname +" "+Lastname;
                billing_address.Text = Address;
                billing_city.Text = City; 
                billing_state.Text = State;
                billing_zip.Text = Zip;
                billing_country.Text = Country;
                billing_tel.Text = Phone;
                billing_email.Text = Email;
                //  Shipping information(optional):
                delivery_name.Value = Firstname + " " + Lastname;
                delivery_address.Value = Address;
                delivery_city.Value = City; 
                delivery_state.Value = State;
                delivery_zip.Value = Zip;
                delivery_country.Value = Country;
                delivery_tel.Value = Phone;
                merchant_param2.Text = ProductInfo;
                merchant_param1.Text = Session["CustID"].ToString();
                 
                if (ConfigurationManager.AppSettings["LocalPrice"] == "Y")
                {
                    merchant_param4.Text = UDF2_CurrencySymbol+" "+Convert.ToDecimal(ConfigurationManager.AppSettings["AmountforTesting"]).ToString("f2");
                    merchant_param3.Text = UDF2_CurrencySymbol + " " + Convert.ToDecimal(ConfigurationManager.AppSettings["AmountforTesting"]).ToString("f2");
                    //Convert.ToDecimal(Request.Form[hash_var]).ToString("g29");
                }
                else
                {
                    merchant_param4.Text = UDF2_CurrencySymbol + " " + Amount.ToString("f2");
                    merchant_param3.Text = UDF2_CurrencySymbol + " " + SessShippingAmount.ToString();
                }           
                promo_code.Value = ""  ;
                customer_identifier.Text = "";

                CommonCode.show_alert("info", "Payment initialization", "Initiating payment of " + UDF2_CurrencySymbol + " " + Amount + " from " + Firstname + ", <br>" + Address +
                    ",<br>" + City + ", " + State + "-" + Zip + ", " + Country + ".", ph_msg);

                CreateOrder();
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


    public void SaveOrder(string TxnID, string Status, string Mode, string ErrorCode, string PG_TYPE, string Bank_Ref_Num, string PayUMoneyID, string AdditionalCharges,
      string tid,Boolean couriour, out string OrderID, out string db_error_msg)
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
                   
                dal.insert_order(CheckoutID, UDF3_TotalItems, Amount.ToString(), shipCost, "ShipMethod", Remarks, "0", "Payment Pending", "Online-" + Mode + "-" + PG_TYPE, 
                    "CardType", "NameOnCard", "CardNum", "CradExpire", tid, OtherCountry, couriour, TxnID, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), 
                    Session["FinancialPeriod"].ToString(), Session["CustID"].ToString(),AddressID_OutPut, out OrderID, out errmsg);
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

    public void CreateOrder()
    {
        try
        {
            string[] hashVarsSeq;
            string hash_string = string.Empty;

            if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
            {
                Random rnd = new Random();
                string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                txnid1 = strHash.ToString().Substring(0, 20);
            }
            else
            {
                txnid1 = Request.Form["txnid"];
            }

            if (txnid1 == Request.Form["txnid"]) // generating txnid
            {

            }
            else
            {
                Mode_C = Request.Form["mode"];
                Status_C = Request.Form["status"];
                Amount_C = Request.Form["amount"];
                Product_C = Request.Form["productinfo"];
                FirstName_C = Request.Form["firstname"];
                LastName_C = Request.Form["lastname"];
                Address1_C = Request.Form["address1"];
                Address2_C = Request.Form["address2"];
                City_C = Request.Form["city"];
                State_C = Request.Form["state"];
                Country_C = Request.Form["country"];
                ZipCode_C = Request.Form["zipcode"];
                Email_C = Request.Form["email"];
                Phone_C = Request.Form["phone"];
                UDF1_C = Request.Form["udf1"];
                UDF2_C = Request.Form["udf2"];
                UDF3_C = Request.Form["udf3"];
                UDF4_C = Request.Form["udf4"];
                UDF5_C = Request.Form["udf5"];
                ErrorCode_C = Request.Form["Error"];
                PG_TYPE_C = Request.Form["PG_TYPE"];
                Bank_Ref_Num_C = Request.Form["bank_ref_num"];
                PayUMoneyID_C = Request.Form["payuMoneyId"];
                AdditionalCharges_C = Request.Form["additionalCharges"];
                TransactionID_C = txnid1.ToUpper(); //Request.Form["txnid"];
                tid_C = tid.Value;

                string db_error_msg = "", mail_err_msg = "", OrderID = "";
                SaveOrder(TransactionID_C, Status_C, Mode_C, ErrorCode_C, PG_TYPE_C, Bank_Ref_Num_C, PayUMoneyID_C, AdditionalCharges_C, tid_C, Courier,out OrderID, out db_error_msg);
                order_id.Text = OrderID ;
                Session["OrderID"] = OrderID;
                merchant_param5.Value = OrderID;
                tid.Value = OrderID;
            }

        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", "Error", ex.Message, ph_msg);
        }
    }


    protected void btn_proceed_to_payment_Click(object sender, EventArgs e)
    {
        try
        {
            string db_error_msg = "", mail_err_msg = "", OrderID = "", errmsg = "";
            DAL dal = new DAL(); 
            //dal.Order_PaymentResStatus(order_id.Text.Trim(), "", amount.Text.Trim(), "0", "", "Cash on Delivery", "1", "COD", "COD", "COD", "1234", "1234", "2020", "Payment Due", "", 
            //    Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), Session["FinancialPeriod"].ToString(), Session["CustID"].ToString(), out OrderID, out db_error_msg);

            Email_Send(billing_email.Text.Trim(), billing_name.Text.Trim(), merchant_param1.Text.Trim(), order_id.Text.Trim(), amount.Text.Trim(), out mail_err_msg);


            string details = ""; 
            details += "<li class='text-success'>Order ID- " + order_id.Text.Trim() + "</li>";
            details += "<li class='text-success'> Amount- " + ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + amount.Text.Trim() + "</li>";

            if (db_error_msg == "success")
            {

            }
            else
            {
                details += "<li class='text-danger'>[Status] - <i class='fa fa-times'></i> Unsuccessful due to some Technical issue - " + db_error_msg + "</li>";
            }
                

            if (mail_err_msg == "success")
                details += "<li class='text-success'>Email Sent on Your Registered Email ID</li>";
            else
                details += "<li class='text-danger'>[Mail TRANS] - <i class='fa fa-times'></i> Unsuccessful - " + mail_err_msg + "</li>";

            send_response("success", "<h1>Thank You for Shopping with us!</h1>", "Order Placed Successfully<br>Details<br><ul>" + details + "</ul>", order_id.Text.Trim());


        }
        catch (AggregateException err)
        {
            foreach (var errInner in err.InnerExceptions)
            {
                Debug.WriteLine(errInner);
            }
        }

    }
    public string[] pay_response = new string[4];
    public void send_response(string status, string title, string msg, string OrderID)
    {
        
        try
        {
            pay_response[0] = status;
            pay_response[1] = title;
            pay_response[2] = msg;
            pay_response[3] = OrderID;
            Session["pay_response"] = pay_response;
            Response.Redirect("postpay.aspx", false);
        }
        catch (Exception ex)
        {
            send_response("info", "Order not Generated Against Transaction !", "" + "Possible reason for error<br> " + ex.Message, OrderID);
        }

    }

    private void Email_Send(String EmailID, String Name, String Product, string TransactionID, string Amount, out string mail_err_msg)
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
        txtEmail = ConfigurationManager.AppSettings["SMTP_EmailID"].ToString();
        txtPassword = ConfigurationManager.AppSettings["SMTP_PASSWORD"].ToString();
        smtpHostName = ConfigurationManager.AppSettings["SMTP_HOSTNAME"].ToString();
        smtpPortNo = int.Parse(ConfigurationManager.AppSettings["SMTP_PORNO"].ToString());
        sslenable = bool.Parse(ConfigurationManager.AppSettings["SMTP_ENABLESSL"].ToString());

        using (MailMessage mm = new MailMessage())
        {
            mm.From = new MailAddress(txtEmail);
            mm.To.Add(new MailAddress(txtTo)); //adding multiple TO Email Id  
            mm.Subject = "Your Order Confirmation #" + TransactionID + "";
            mm.IsBodyHtml = true;
            mm.Body = "Dear " + Name + "";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "            Thank you for using dcbooks.ae";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "            Your valuable order has been registered against OrderID <b>" + TransactionID + "</b>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "            We regret to inform you that currently online payment is not active. We will organise to deliver your order by courier and the payment to be made on delivery. ";
            mm.Body += "<br/>"; 
            mm.Body += "<br/>";  
            mm.Body += "<b> Amount :  " + Amount + "</b>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<b> Terms :  " + "PAYMENT ON DELIVERY" + "</b> .";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "Best Regards,";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += ConfigurationManager.AppSettings["CompanyName"].ToString() + "."; 
            SmtpClient smtp = new SmtpClient(); 
            smtp.Host = smtpHostName; 
            smtp.EnableSsl = sslenable; 
            NetworkCredential NetworkCred = new NetworkCredential(txtEmail, txtPassword.Trim());
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            smtp.Port = smtpPortNo; // 80;
            smtp.Send(mm);
            Session["MailSent"] = "MailSent";
            mail_err_msg = "success";
        }

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

    // *********** Updated : 11/06/2019 ********************** //
    // ********** Added Code for Cancel Button *************** //
    //protected void Cancelled_order(object sender, EventArgs e)
    //{
    //    Response.Redirect("user_cart.aspx");
    //}
    // ************** Updated : 24/06/2019 ************************ //
    // ************** Changed Amount Format from g29 to f2 ************************ //

}
