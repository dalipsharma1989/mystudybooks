using System;
using System.IO;
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
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json; 
using System.Net; 
using System.Collections.Generic; 
using System.Collections;
using System.Collections.Specialized;
using CCA.Util; 
using System.Net.Mail; 
using System.Diagnostics;
using System.Net.Http;
using System.Linq;
public partial class PayResponse : System.Web.UI.Page
{
    public String Product, Amount, FirstName, LastName, Email, Phone, Mobile, Address1, Address2, City, State, Country, ZipCode, Remarks,
        UDF1, UDF2, UDF3, UDF4, UDF5, Status, Mode, ErrorCode, PG_TYPE, Bank_Ref_Num, PayUMoneyID, AdditionalCharges,TransactionID ;
    public string[] pay_response = new string[4];

    public void send_response(string status, string title, string msg, string OrderID)
    {
        pay_response[0] = status;
        pay_response[1] = title;
        pay_response[2] = msg;
        pay_response[3] = OrderID;
        Session["pay_response"] = pay_response;

        Response.Redirect("postpay.aspx", false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string OrderID = "";
        // Get_PaymentVerification("326121");   ///use it if want to check single order payment status
        //web_SendSMS("1", "1", "45679465", "500",  "9812113771","97846578");
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["TransactionID"].ToString()))
            {
                OrderID = Decrypt(HttpUtility.UrlDecode(Request.QueryString["TransactionID"]));
                Get_PaymentVerification(OrderID);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Get_PaymentVerification(string OrderID)
    {
        string command = "verify_payment";
        string hashstr = ConfigurationManager.AppSettings["MERCHANT_KEY"] + "|" + command + "|" + OrderID + "|" + ConfigurationManager.AppSettings["MERCHANT_SALT"];

        string hash = Generatehash512(hashstr);

        ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["PAYU_VERIFY_URL"]);

        var postData = "key=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["MERCHANT_KEY"]);
        postData += "&hash=" + Uri.EscapeDataString(hash);
        postData += "&var1=" + Uri.EscapeDataString(OrderID);
        postData += "&command=" + Uri.EscapeDataString(command);
        var data = Encoding.ASCII.GetBytes(postData);

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        DataTable dt = new DataTable();
        dt = stsTable(responseString, OrderID);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][OrderID + "_status"].ToString() == "success")
            {
                Mode = dt.Rows[0][OrderID + "_mode"].ToString();
                Status = dt.Rows[0][OrderID + "_status"].ToString();
                Amount = dt.Rows[0][OrderID + "_amt"].ToString();
                Product = dt.Rows[0][OrderID + "_productinfo"].ToString();
                FirstName = dt.Rows[0][OrderID + "_firstname"].ToString();
                //LastName = dt.Rows[0][OrderID + "_status"].ToString();
                //Address1 = dt.Rows[0][OrderID + "_status"].ToString();
                //Address2 = dt.Rows[0][OrderID + "_status"].ToString();
                //City = dt.Rows[0][OrderID + "_status"].ToString();
                //State = dt.Rows[0][OrderID + "_status"].ToString();
                //Country = dt.Rows[0][OrderID + "_status"].ToString();
                //ZipCode = dt.Rows[0][OrderID + "_status"].ToString();
                //Email = dt.Rows[0][OrderID + "_status"].ToString();
                //Phone = dt.Rows[0][OrderID + "_status"].ToString();
                UDF1 = dt.Rows[0][OrderID + "_udf1"].ToString();                //EmailID
                UDF2 = dt.Rows[0][OrderID + "_udf2"].ToString();                // FinancialPeriod
                UDF3 = dt.Rows[0][OrderID + "_udf3"].ToString();                // CUstID
                UDF4 = dt.Rows[0][OrderID + "_udf4"].ToString();                // BranchID
                UDF5 = dt.Rows[0][OrderID + "_udf5"].ToString();                // CompanyID
                ErrorCode = dt.Rows[0][OrderID + "_error_code"].ToString();
                PG_TYPE = dt.Rows[0][OrderID + "_PG_TYPE"].ToString();
                Bank_Ref_Num = dt.Rows[0][OrderID + "_bank_ref_num"].ToString();
                PayUMoneyID = dt.Rows[0][OrderID + "_mihpayid"].ToString();
                AdditionalCharges = dt.Rows[0][OrderID + "_additional_charges"].ToString();
                TransactionID = dt.Rows[0][OrderID + "_txnid"].ToString();
                Remarks = dt.Rows[0][OrderID + "_field9"].ToString();
                string db_error_msg = "", mail_err_msg = "" ;

                DAL dal = new DAL();

                string otherDetails = "{TXNID:" + TransactionID + "},{Status:" + Status + "},{Mode:" + Mode + "},{ErrorCode:" + ErrorCode + "},{PG_TYPE:" + PG_TYPE + "}," +
                                      "{Bank_Ref_Num:" + Bank_Ref_Num + "},{PayUMoneyID:" + PayUMoneyID + "},{AdditionalCharges:" + AdditionalCharges + "},{Remarks:" + Remarks + "}";
                 
                dal.Order_PaymentResStatus(OrderID, Remarks, "1", "PaymentReceived", "Online-" + Mode + "-" + PG_TYPE, PG_TYPE, "", PayUMoneyID, Bank_Ref_Num, Amount,"","", otherDetails,
                    UDF5, UDF4, UDF2, UDF3,  out db_error_msg);

                Session["CustID"] = UDF3;
                Session["CustEmail"] = UDF1;
                Session["CustName"] = FirstName;
                Session["CustType"] = "Retail";
                Session["Password"] = "";
                Session["iCompanyId"] = UDF5;
                Session["iBranchID"] = UDF4;
                Session["FinancialPeriod"] = UDF2;
                Session["OtherCountry"] = "INDIA";

                web_SendSMS(UDF5, UDF4, TransactionID, Amount, UDF1 , PayUMoneyID);
                Email_Send(UDF1, FirstName, TransactionID, Amount, out mail_err_msg);

                string details = "";
                details += "<li class='text-success'>Transaction ID- " + TransactionID + "</li>";
                details += "<li class='text-success'>Product- " + Product + "</li>";
                details += "<li class='text-success'> Amount- " + Amount + "</li>";

                if (db_error_msg == "success")
                    details += "<li class='text-success'>[DB TRANS] - <i class='fa fa-check'></i> Successful</li>";
                else
                    details += "<li class='text-danger'>[DB TRANS] - <i class='fa fa-times'></i> Unsuccessful - " + db_error_msg + "</li>";

                if (mail_err_msg == "success")
                    details += "<li class='text-success'>[MAIL TRANS] - <i class='fa fa-check'></i> Successful</li>";
                else
                    details += "<li class='text-danger'>[MAIL TRANS] - <i class='fa fa-times'></i> Unsuccessful - " + mail_err_msg + "</li>";

                send_response("success", "Payment processed successfully", "Details<br><ul>" + details + "</ul>", OrderID);

            }
            else
            {

                UDF1 = dt.Rows[0][OrderID + "_udf1"].ToString();                //EmailID
                UDF2 = dt.Rows[0][OrderID + "_udf2"].ToString();                // FinancialPeriod
                UDF3 = dt.Rows[0][OrderID + "_udf3"].ToString();                // CUstID
                UDF4 = dt.Rows[0][OrderID + "_udf4"].ToString();                // BranchID
                UDF5 = dt.Rows[0][OrderID + "_udf5"].ToString();                // CompanyID

                Session["CustID"] = UDF3;
                Session["CustEmail"] = UDF1;
                Session["CustName"] = FirstName;
                Session["CustType"] = "Retail";
                Session["Password"] = "";
                Session["iCompanyId"] = UDF5;
                Session["iBranchID"] = UDF4;
                Session["FinancialPeriod"] = UDF2;
                Session["OtherCountry"] = "INDIA";
                string err_details = "", errmsg="";  
                SqlCommand coms = new SqlCommand(" Update [TransOrder] set [STATUS] = 0 ,  StatusRemark = 'Status" + dt.Rows[0][OrderID + "_status"].ToString() + " and Error Msg : -" + dt.Rows[0][OrderID + "_field9"].ToString() + "and Error Detail : -" + dt.Rows[0][OrderID + "_error_Message"].ToString() + "' where TransactionIDord = " + OrderID + "", CommonCode.con);
                DataTable dte = new DataTable();
                errmsg = "";
                dte = CommonCode.getData(coms, out errmsg);
                Session["MailSent"] = "MailNotSent";
                err_details += "<li>Status - " + dt.Rows[0][OrderID + "_status"].ToString() + "</li>";
                err_details += "<li>Error - " + dt.Rows[0][OrderID + "_field9"].ToString() + "</li>";
                err_details += "<li>Error Details- " + dt.Rows[0][OrderID + "_error_Message"].ToString() + "</li>";
                send_response("info", "Payment unsuccessful !", "" + "Possible reason for error<br> <ul>" + err_details + "</ul>", OrderID); 
            }
        } 
    }
    public void web_SendSMS(string companyID, string BranchID , string TransactionID, string Amount, string Phone, string str_PayUMoneyID) 
    {
        string ProviderURL = "", UserIDCaption = "", UserIDValue = "", PasswordCaption = "", PasswordValue = "", MsgCaption = "", MsgTypeCaption = "",
                   MsgTypeValue = "", BothSameCaption = "", CDMACaption = "", GSMCaption = "", SenderName = "", Sresult = "", Message = "", errorSMS = "";
        bool BothSame = false;
        DAL md = new DAL();
        DataTable data = new DataTable();
        data = md.Get_SMS_Information(companyID, BranchID);

        if (errorSMS == "success")
        {
            ProviderURL = data.Rows[0]["ProviderURL"].ToString();
            UserIDCaption = data.Rows[0]["UserIDCaption"].ToString(); ;
            UserIDValue = data.Rows[0]["UserIDValue"].ToString();
            PasswordCaption = data.Rows[0]["PasswordCaption"].ToString();
            PasswordValue = data.Rows[0]["PasswordValue"].ToString();
            MsgCaption = data.Rows[0]["MsgCaption"].ToString();
            MsgTypeCaption = data.Rows[0]["MsgTypeCaption"].ToString();
            MsgTypeValue = data.Rows[0]["MsgTypeValue"].ToString();
            BothSame = Convert.ToBoolean(data.Rows[0]["BothSame"].ToString() == "1" ? true : false);
            BothSameCaption = data.Rows[0]["BothSameCaption"].ToString();
            CDMACaption = data.Rows[0]["CDMACaption"].ToString();
            GSMCaption = data.Rows[0]["GSMCaption"].ToString();
            SenderName = data.Rows[0]["vSenderName"].ToString();

            Message = "Thank you for placing order with My Study Books, your transaction number is: " + TransactionID + " and Order Amount is : " + Amount.ToString() 
                + " and Your PayumoneyID is - " + str_PayUMoneyID.ToString() 
                + ". Please login to check your order history Link: https://mystudybooks.in/Customer/user_login.aspx";
            Sresult = ProviderURL + "&" + UserIDCaption + "=" + UserIDValue + "&" + PasswordCaption + "=" + PasswordValue + "&" + MsgCaption
            + "=" + Message + "&" + MsgTypeCaption + "=" + MsgTypeValue + "&" + BothSameCaption + "=" + Phone.ToString();
            new System.Net.WebClient().DownloadString(Sresult);
        } 
    }


    private void Email_Send(String EmailID, String Name,  string TransactionID, string Amount, out string mail_err_msg)
    {
        mail_err_msg = "";
        try
        {
            string txtTo = EmailID;
            string txtEmail = "";
            string txtEmailBCC = ConfigurationManager.AppSettings["SMTP_BCCEmailID"].ToString();
            string txtPassword = "";
            string smtpHostName = "";
            Int32 smtpPortNo = 0;
            Boolean sslenable = false;
            txtEmailBCC = ConfigurationManager.AppSettings["SMTP_BCCEmailID"].ToString();
            txtEmail = ConfigurationManager.AppSettings["SMTP_EmailID"].ToString();
            txtPassword = ConfigurationManager.AppSettings["SMTP_PASSWORD"].ToString();
            smtpHostName = ConfigurationManager.AppSettings["SMTP_HOSTNAME"].ToString();
            smtpPortNo = int.Parse(ConfigurationManager.AppSettings["SMTP_PORNO"].ToString());
            sslenable = bool.Parse(ConfigurationManager.AppSettings["SMTP_ENABLESSL"].ToString());

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(txtEmail);
            mm.To.Add(new MailAddress(txtTo)); //adding multiple TO Email Id  
            mm.Bcc.Add(new MailAddress(txtEmailBCC));
            mm.Subject = "Your Order Confirmation #" + TransactionID + "";
            mm.IsBodyHtml = true;
            mm.Body = "Dear " + Name + "";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "            Thank you for using https://mystudybooks.in/ ";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "            Your valuable order has been registered against <b> OrderID :#" + TransactionID + "</b>";
            mm.Body += "<br/>";            
            mm.Body += "<br/>";
            mm.Body += "            We are happy to inform you that your payment request has been successfully received and the details are as follows: ";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<b>Your Order Transaction Reference Number :  " + TransactionID + "</b>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<b>Transaction Date and Time : " + DateTime.Now + "</b>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<b>Transaction Type :  " + "ORDER PAYMENT" + "</b>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<b> Amount :  " + Amount + "</b>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<b> Transaction Status :  " + "SUCCESS" + "</b>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            string encryptedValue = HttpUtility.UrlEncode(Encrypt(TransactionID));
            string URl = ConfigurationManager.AppSettings["OrderUrlPath"].ToString() + encryptedValue;
            mm.Body += " To see the Order Summary please <B> <a href=" + URl + " target='_blank'>Click here</a></B>.";
            mm.Body += "<br/>";
            mm.Body += "Once your order is processed, invoice will be auto-generated and sent on your registered email ID.";
            mm.Body += "<br/>";
            mm.Body += "Also, you will be notified as soon as your order is ready for delivery.";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "Best Regards,";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "My Study Books Team.";

            SmtpClient smtp = new SmtpClient();

            smtp.Host = smtpHostName;
            smtp.EnableSsl = sslenable;
            NetworkCredential NetworkCred = new NetworkCredential(txtEmail, txtPassword.Trim());
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            smtp.Port = smtpPortNo;
            smtp.Send(mm);
            Session["MailSent"] = "MailSent";
            mail_err_msg = "success";
        }
        catch (Exception ex)
        {
            string err_response = "";
            err_response += "\n<br/>";
            err_response += ex.Message;
            err_response += "\n<br/>";
            err_response += ex.StackTrace;
            mail_err_msg = "Error Mail:-" + ex.Message;
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

    public DataTable stsTable(string json, string OrderID)
    {

        DataTable dt = null;
        var jsonLinq = JObject.Parse(json);
        var trgArray = new JArray();
        // Find the first array using Linq
        JToken id;
        List<JToken> results = jsonLinq.Children().ToList();
        Boolean blnValFound = false;
        foreach (JProperty item in results)
        {
            item.CreateReader();
            if (item.Name == "transaction_details")
            {
                id = item.Value;

                List<JToken> results1 = id.Children().ToList();
                var cleanRow = new JObject();
                foreach (JProperty jProp in results1)
                {
                    jProp.CreateReader();
                    JToken id1;
                    id1 = jProp.Value;

                    if (jProp.Value is JValue)
                    {
                        cleanRow.Add(jProp.Name, jProp.Value);
                    }
                    else
                    {
                        List<JToken> resultsInner = id1.Children().ToList();
                        cleanRow = rtnInnerTable(cleanRow, jProp, resultsInner);

                        //foreach (JProperty jPropInner in resultsInner)
                        //{
                        //    jPropInner.CreateReader();
                        //    JToken idInner;
                        //    idInner = jPropInner.Value;

                        //    if (jPropInner.Value is JValue)
                        //    {
                        //        cleanRow.Add(jProp.Name + "_" + jPropInner.Name, jPropInner.Value);
                        //    }

                        //    blnValFound = true;
                        //}
                    }
                    blnValFound = true;
                }
                trgArray.Add(cleanRow);
                if (blnValFound == true)
                {
                    break;
                }
            }
            if (blnValFound == true)
            {
                break;
            }
        }

        return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
    }

    private JObject rtnInnerTable(JObject cleanRow, JProperty MAinjProp, List<JToken> resultsMain)
    {
        foreach (JProperty jProp in resultsMain)
        {
            jProp.CreateReader();
            JToken id1;
            id1 = jProp.Value;

            if (jProp.Value is JValue)
            {
                cleanRow.Add(MAinjProp.Name + "_" + jProp.Name, jProp.Value);
            }
            else
            {
                List<JToken> resultsInner = id1.Children().ToList();
                cleanRow = rtnInnerTable(cleanRow, jProp, resultsInner);
            }
        }


        return cleanRow;
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

    public void get_PaymentResponse()
    {
        try
        {
            string jsonerr = "";
            var reader = new StreamReader(Request.InputStream);
            string json = reader.ReadToEnd();
            jsonerr = json;

            this.Title = CommonCode.SetPageTitle("Processing Payment...");
            string[] merc_hash_vars_seq;
            string merc_hash_string = string.Empty;
            string merc_hash = string.Empty;
            TransactionID = string.Empty;
            string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";


            if (Request.Form["status"] == "success")
            {
                merc_hash_vars_seq = hash_seq.Split('|');
                Array.Reverse(merc_hash_vars_seq);
                merc_hash_string = ConfigurationManager.AppSettings["SALT"] + "|" + Request.Form["status"];

                foreach (string merc_hash_var in merc_hash_vars_seq)
                {
                    merc_hash_string += "|";
                    merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");
                }

                merc_hash = Generatehash512(merc_hash_string).ToLower();

                if (merc_hash != Request.Form["hash"])
                {
                    send_response("danger", "Error while processing payment", "Hash value didn`t match", "");
                }
                else
                {
                    Mode = Request.Form["mode"];
                    Status = Request.Form["status"];
                    Amount = Request.Form["amount"];
                    Product = Request.Form["productinfo"];
                    FirstName = Request.Form["firstname"];
                    LastName = Request.Form["lastname"];
                    Address1 = Request.Form["address1"];
                    Address2 = Request.Form["address2"];
                    City = Request.Form["city"];
                    State = Request.Form["state"];
                    Country = Request.Form["country"];
                    ZipCode = Request.Form["zipcode"];
                    Email = Request.Form["email"];
                    Phone = Request.Form["phone"];
                    UDF1 = Request.Form["udf1"];
                    UDF2 = Request.Form["udf2"];
                    UDF3 = Request.Form["udf3"];
                    UDF4 = Request.Form["udf4"];
                    UDF5 = Request.Form["udf5"];
                    ErrorCode = Request.Form["Error"];
                    PG_TYPE = Request.Form["PG_TYPE"];
                    Bank_Ref_Num = Request.Form["bank_ref_num"];
                    PayUMoneyID = Request.Form["payuMoneyId"];
                    AdditionalCharges = Request.Form["additionalCharges"];
                    TransactionID = Request.Form["txnid"];

                    string db_error_msg = "", mail_err_msg = "", OrderID = "";

                    save_transaction_in_db(TransactionID, Status, Mode, ErrorCode, PG_TYPE, Bank_Ref_Num, PayUMoneyID, AdditionalCharges, out OrderID, out db_error_msg);

                    // send_mail(Email, FirstName, Product, TransactionID, Amount, out mail_err_msg);

                    string details = "";
                    details += "<li class='text-success'>Transaction ID- " + TransactionID + "</li>";
                    details += "<li class='text-success'>Product- " + Product + "</li>";
                    details += "<li class='text-success'> Amount- " + UDF2 + " " + Amount + "</li>";

                    if (db_error_msg == "success")
                        details += "<li class='text-success'>[DB TRANS] - <i class='fa fa-check'></i> Successful</li>";
                    else
                        details += "<li class='text-danger'>[DB TRANS] - <i class='fa fa-times'></i> Unsuccessful - " + db_error_msg + "</li>";

                    //if (mail_err_msg == "success")
                    //    details += "<li class='text-success'>[ML TRANS] - <i class='fa fa-check'></i> Successful</li>";
                    //else
                    //    details += "<li class='text-danger'>[ML TRANS] - <i class='fa fa-times'></i> Unsuccessful - " + mail_err_msg + "</li>";

                    send_response("success", "Payment processed successfully", "Details<br><ul>" + details + "</ul>", "");
                }
            }

            else
            {
                string err_details = "";
                TransactionID = Request.Form["txnid"];
                SqlCommand com = new SqlCommand(" Update [Order] set StatusRemark = 'Cancelled' where TransactionID = '" + TransactionID + "'", CommonCode.con);
                DataTable dt = new DataTable();
                string errmsg;
                dt = CommonCode.getData(com, out errmsg);

                err_details += "<li>Status - " + Request.Form["status"] + "</li>";
                err_details += "<li>Error - " + Request.Form["Error"] + "</li>";
                err_details += "<li>Error Details- " + detect_error(Request.Form["Error"]) + "</li>";
                send_response("info", "Payment unsuccessful !", "" + "Possible reason for error<br> <ul>" + err_details + "</ul>", "");
            }
        }

        catch (Exception ex)
        {
            send_response("danger", "Error while processing payment ! Catch block", ex.Message + " <br>" + ex.StackTrace, "");
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
     
    public string detect_error(string error_code)
    {
        if (string.IsNullOrEmpty(error_code))
            error_code = "";
        switch (error_code.ToLower())
        {
            case "e314": return "Address_failure";
            case "e304": return "Address_invalid";
            case "e702": return "Amount_difference";
            case "e303": return "Authentication_error";
            case "e335": return "Authentication_incomplete";
            case "e334": return "Authentication_service_unavailable";
            case "e505": return "Awaiting_processing";
            case "e312": return "Bank_denied";
            case "e208": return "Bank_server_error";
            case "e216": return "Batch_error";
            case "e201": return "Brand_invalid";
            case "e324": return "Card_fraud_suspected";
            case "e218": return "Card_issuer_timed_out";
            case "e900": return "Card_not_enrolled";
            case "e305": return "Card_number_invalid";
            case "e213": return "Checksum_failure";
            case "e210": return "Communication_error";
            case "e214": return "Curl_call_failure";
            case "e203": return "Curl_error_card_verification";
            case "e205": return "Curl_error_enrolled";
            case "e204": return "Curl_error_not_enrolled";
            case "e206": return "Cutoff_error";
            case "e315": return "Cvc_address_failure";
            case "e313": return "Cvc_failure";
            case "e504": return "Duplicate_transaction";
            case "e311": return "Expired_card";
            case "e336": return "Expiry_date_low_funds";
            case "e219": return "Incomplete_bank_response";
            case "e712": return "Incomplete_data";
            case "e706": return "Insufficient_funds";
            case "e719": return "Insufficient_funds_authentication_failure";
            case "e713": return "Insufficient_funds_expiry_invalid";
            case "e718": return "Insufficient_funds_invalid_cvv";
            case "e903": return "International_card_not_allowed";
            case "e717": return "Invalid_account_number";
            case "e715": return "Invalid_amount";
            case "e709": return "Invalid_card_name";
            case "e902": return "Invalid_card_type";
            case "e333": return "Invalid_contact";
            case "e331": return "Invalid_email_id";
            case "e323": return "Invalid_expiry_date";
            case "e332": return "Invalid_fax";
            case "e327": return "Invalid_login";
            case "e1605": return "Process cancelled by user";
            default: return "May be Process cancelled by user";
        }
    }


    public void save_transaction_in_db(string TxnID, string Status, string Mode, string ErrorCode, string PG_TYPE, string Bank_Ref_Num, string PayUMoneyID, string AdditionalCharges, out string OrderID, out string db_error_msg)
    {
        string errmsg = "";
        try
        {
            if (Session["CustID"] != null && Session["Payment_Variables"] != null)
            {
                DataTable dt_Payment_Variables = new DataTable();
                dt_Payment_Variables = Session["Payment_Variables"] as DataTable;
                if (dt_Payment_Variables.Rows.Count > 0)
                {
                    float Amount = 0;
                    string Firstname, Lastname,
                     Email, Phone, Address, City, State,
                     Country, Zip, Remarks, CheckoutID, UDF1_Currency, UDF2_CurrencySymbol, UDF3_TotalItems, ProductInfo;

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

                    DAL dal = new DAL();

                    string otherDetails = "{TXNID:" + TxnID + "},{Status:" + Status + "},{Mode:" + Mode + "},{ErrorCode:" + ErrorCode + "},{PG_TYPE:" + PG_TYPE + "}," +
                                          "{Bank_Ref_Num:" + Bank_Ref_Num + "},{PayUMoneyID:" + PayUMoneyID + "},{AdditionalCharges:" + AdditionalCharges + "}";
                    dal.Order_PaymentStatus(CheckoutID, UDF3_TotalItems, Amount.ToString(), "0", "", Remarks, "1", "PaymentReceived",
                        "Online-" + Mode + "-" + PG_TYPE, "", "", "", "", otherDetails, out OrderID, out errmsg, TxnID);
                }
                else
                {
                    errmsg = "Payment_Varibles Datatable is empty";
                    OrderID = "";
                }
            }
            else
            {
                errmsg = "Session[Payment_Variables] Expired";
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


    //private void send_mail(String EmailID, String Name, String Product, string TransactionID, string Amount, out string mail_err_msg)
    //{
    //    Mail mail = new Mail();
    //    string errmsg = "";
    //    string html = "";
    //    html += "";
    //    html += "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN'";
    //    html += "<html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head>";
    //    html += "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>";
    //    html += "<meta http-equiv='X-UA-Compatible' content='IE=edge'>";
    //    html += "<meta name='viewport' content='width=device-width, initial-scale=1.0'>";
    //    html += "<title></title>";
    //    html += "<!--[if gte mso 9]><xml>";
    //    html += " <o:OfficeDocumentSettings>";
    //    html += "  <o:AllowPNG/>";
    //    html += "  <o:PixelsPerInch>96</o:PixelsPerInch>";
    //    html += " </o:OfficeDocumentSettings>";
    //    html += "</xml><![endif]-->";
    //    html += "";
    //    html += "<style type='text/css'>";
    //    html += "/* Specifics */";
    //    html += "#outlook a {";
    //    html += "        padding: 0;";
    //    html += "}";
    //    html += ".ReadMsgBody {";
    //    html += "        width: 100%;";
    //    html += "}";
    //    html += ".ExternalClass {";
    //    html += "        width: 100%;";
    //    html += "}";
    //    html += ".ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {";
    //    html += "        line-height: 100%;";
    //    html += "}";
    //    html += "body, table, td, a {";
    //    html += "        -webkit-text-size-adjust: 100%;";
    //    html += "        -ms-text-size-adjust: 100%;";
    //    html += "}";
    //    html += "table, td {";
    //    html += "        mso-table-lspace: 0pt;";
    //    html += "        mso-table-rspace: 0pt;";
    //    html += "        border-collapse: collapse;";
    //    html += "}";
    //    html += "img {";
    //    html += "        -ms-interpolation-mode: bicubic;";
    //    html += "}";
    //    html += "/* Resets */";
    //    html += "body {";
    //    html += "        Margin: 0;";
    //    html += "        padding: 0;";
    //    html += "        min-width: 100%;";
    //    html += "        background-color: #f8f8f8;";
    //    html += "}";
    //    html += "img {";
    //    html += "        border: 0;";
    //    html += "        height: auto;";
    //    html += "        line-height: 100%;";
    //    html += "        outline: none;";
    //    html += "        text-decoration: none;";
    //    html += "}";
    //    html += "table {";
    //    html += "        border-collapse: collapse !important;";
    //    html += "}";
    //    html += "body {";
    //    html += "        height: 100% !important;";
    //    html += "        margin: 0;";
    //    html += "        padding: 0;";
    //    html += "        width: 100% !important;";
    //    html += "}";
    //    html += "body {";
    //    html += "        margin: 0 !important;";
    //    html += "}";
    //    html += "div[style*='margin: 16px 0'] {";
    //    html += "        margin: 0 !important;";
    //    html += "}";
    //    html += "/* iOS Links */";
    //    html += ".apple-footer a {";
    //    html += "        color: #000000;";
    //    html += "        text-decoration: none;";
    //    html += "}";
    //    html += "a[x-apple-data-detectors] {";
    //    html += "        color: inherit !important;";
    //    html += "        text-decoration: none !important;";
    //    html += "        font-size: inherit !important;";
    //    html += "        font-family: inherit !important;";
    //    html += "        font-weight: inherit !important;";
    //    html += "        line-height: inherit !important;";
    //    html += "}";
    //    html += ".wrapper {";
    //    html += "        width: 100%;";
    //    html += "        -webkit-text-size-adjust: 100%;";
    //    html += "        -ms-text-size-adjust: 100%;";
    //    html += "}";
    //    html += "/* Responsive Columns */";
    //    html += ".one-column .contents {";
    //    html += "        text-align: left;";
    //    html += "}";
    //    html += ".one-column p {";
    //    html += "        font-size: 14px;";
    //    html += "        Margin-bottom: 10px;";
    //    html += "}";
    //    html += ".two-column {";
    //    html += "        text-align: center;";
    //    html += "        font-size: 0;";
    //    html += "}";
    //    html += ".two-column .column {";
    //    html += "        width: 100%;";
    //    html += "        max-width: 278px;";
    //    html += "        display: inline-block;";
    //    html += "        vertical-align: middle;";
    //    html += "}";
    //    html += ".contents {";
    //    html += "        width: 100%;";
    //    html += "}";
    //    html += ".two-column .contents {";
    //    html += "        font-size: 14px;";
    //    html += "        text-align: left;";
    //    html += "}";
    //    html += ".two-column img {";
    //    html += "        max-width: 280px;";
    //    html += "        height: auto;";
    //    html += "}";
    //    html += ".two-column .text {";
    //    html += "        padding-top: 10px;";
    //    html += "}";
    //    html += ".three-column {";
    //    html += "        text-align: center;";
    //    html += "        font-size: 0;";
    //    html += "        padding-top: 10px;";
    //    html += "        padding-bottom: 10px;";
    //    html += "}";
    //    html += ".three-column .column {";
    //    html += "        width: 100%;";
    //    html += "        max-width: 200px;";
    //    html += "        display: inline-block;";
    //    html += "        vertical-align: middle;";
    //    html += "}";
    //    html += ".three-column .contents {";
    //    html += "        font-size: 14px;";
    //    html += "        text-align: center;";
    //    html += "}";
    //    html += ".three-column img {";
    //    html += "        width: 100%;";
    //    html += "        max-width: 180px;";
    //    html += "        height: auto;";
    //    html += "}";
    //    html += ".three-column .text {";
    //    html += "        padding-top: 10px;";
    //    html += "}";
    //    html += "";
    //    html += "/* Responsive Styles */";
    //    html += "@media screen and (max-width: 525px) {";
    //    html += "table[class='responsive-table'] {";
    //    html += "        width: 100% !important;";
    //    html += "}";
    //    html += "table[class='responsive-column'] {";
    //    html += "        width: 100% !important;";
    //    html += "        height: auto !important;";
    //    html += "}";
    //    html += "td[class='full'] {";
    //    html += "        width: 100% !important;";
    //    html += "        padding: 0px !important;";
    //    html += "        border: 0px solid #ffffff !important;";
    //    html += "}";
    //    html += "td[class='col-centered'] {";
    //    html += "        text-align: center !important;";
    //    html += "        padding: 10px 0px 10px 0px !important;";
    //    html += "}";
    //    html += "td[class='col-centered-line'] {";
    //    html += "        text-align: center !important;";
    //    html += "        padding: 10px 0px 10px 0px !important;";
    //    html += "        border-bottom: 1px solid #e8e8e8 !important;";
    //    html += "}";
    //    html += "td[class='padding-main'] {";
    //    html += "        padding: 10px 5% 10px 5% !important;";
    //    html += "        text-align: left !important;";
    //    html += "}";
    //    html += "td[class='social-container'] {";
    //    html += "        margin: 0 auto;";
    //    html += "        padding-top: 10px !important;";
    //    html += "        width: 100% !important;";
    //    html += "        text-align: center !important;";
    //    html += "        display: inline-block !important;";
    //    html += "}";
    //    html += "td[class='padding-footer'] {";
    //    html += "        padding: 10px 5% 10px 5% !important;";
    //    html += "        text-align: center !important;";
    //    html += "        font-size: 9px !important;";
    //    html += "}";
    //    html += "img[class='Logo-Shrink'] {";
    //    html += "        width: 100px !important;";
    //    html += "        height: auto !important;";
    //    html += "        text-align: left;";
    //    html += "}";
    //    html += "img[class='img-max'] {";
    //    html += "        width: 100% !important;";
    //    html += "        height: auto !important;";
    //    html += "}";
    //    html += ".socialPod {";
    //    html += "        display: inline-block;";
    //    html += "        width: 14%;";
    //    html += "        text-align: center;";
    //    html += "}";
    //    html += ".socialDivider {";
    //    html += "        display: inline-block;";
    //    html += "        width: 7%;";
    //    html += "}";
    //    html += "/* Buttons on Mobile */";
    //    html += "td[class='mobile-wrapper'] {";
    //    html += "        padding: 10px 5% 15px 5% !important;";
    //    html += "}";
    //    html += "table[class='mobile-button-container'] {";
    //    html += "        margin: 0 auto;";
    //    html += "        width: 100% !important;";
    //    html += "}";
    //    html += "a[class='mobile-button'] {";
    //    html += "        width: 80% !important;";
    //    html += "        padding: 15px !important;";
    //    html += "        border: 0 !important;";
    //    html += "        font-size: 18px !important;";
    //    html += "}";
    //    html += "/* Responsive Columns */";
    //    html += ".two-column .column, .three-column .column {";
    //    html += "        max-width: 100% !important;";
    //    html += "}";
    //    html += ".two-column img {";
    //    html += "        max-width: 100% !important;";
    //    html += "}";
    //    html += ".three-column img {";
    //    html += "        max-width: 50% !important;";
    //    html += "}";
    //    html += "}";
    //    html += "</style>";
    //    html += "</head>";
    //    html += "<body bgcolor='#F8F8F8' style='margin:0px; padding:0px; background-color:#f8f8f8; '><!-- Hidden Preheader -->";
    //    html += "<table width='100%' cellpadding='0' cellspacing='0' border='0' style='border-collapse:collapse; '><tbody><tr style='font-size:0px; line-height:0; mso-line-height-alt:0; -webkit-text-size-adjust:none; mso-margin-top-alt:0px; '><td style='display:none !important; visibility:hidden; mso-hide:all; font-size:1px; color:#f8f8f8; line-height:1px; max-height:0px; max-width:0px; opacity:0; overflow:hidden; '><div class='mktEditable' id='Preview-Line'></div>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "</tbody></table>";
    //    html += "<center class='wrapper' style='width:100%; -webkit-text-size-adjust:100%; -ms-text-size-adjust:100%; '><!-- Outer Table -->";
    //    html += "<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0' bgcolor='#F8F8F8' style='background-color:#f8f8f8; '><tbody><tr><td align='center' style='padding:15px; ' class='full'><table width='600' align='center' cellpadding='0' cellspacing='0' border='0' bgcolor='#FFFFFF' style='background-color:#ffffff; width:600px; ' class='responsive-table'><!-- Inner Containter Table -->";
    //    html += "<tbody><tr><td style='border: 1px solid #ed2d32;font-family:Helvetica, Arial, sans-serif;color:#000000;' class='full'><table width='100%' align='center' cellpadding='0' cellspacing='0' border='0' bgcolor='#FFFFFF' style='border-collapse:collapse; background-color:#ffffff; '><tbody><tr><td style='padding:0px; margin:0px; display:block; font-family:Helvetica, Arial, sans-serif; '><div class='mktEditable' id='logo'><table bgcolor='#FFFFFF' width='100%' border='0' cellspacing='0' cellpadding='0' style='background-color:#ffffff;'>";
    //    html += "<tbody><tr>";
    //    html += "<td bgcolor='#FFFFFF' style='padding: 20px 20px 10px 20px; background-color:#ffffff' class='padding-main'>";
    //    html += "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
    //    html += "<tbody><tr>";
    //    html += "<td align='left'><a href='http://springtimesoftware.net'><img src='http://springtimesoftware.net/assets/images/logo.png' alt='Amazon Web Services' border='0' class='Logo-Shrink'></a></td>";
    //    html += "</tr>";
    //    html += "</tbody></table>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "</tbody></table>";
    //    html += "</div>";
    //    html += "";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "<!-- Main Content -->";
    //    html += "<tr><td style='padding:10px 20px 20px 20px; ' class='padding-main'><table width='100%' align='center' cellpadding='0' cellspacing='0' border='0' bgcolor='#FFFFFF' style='background-color:#ffffff; '><!-- Block 1 -->";
    //    html += "<tbody><tr><td align='left' style='padding:0px; margin:0px; display:block; font-family:Helvetica, Arial, sans-serif; font-size:14px; line-height:24px; '><div class='mktEditable' id='Heading'></div>";
    //    html += "<div class='mktEditable' id='Block-1'><div>";
    //    html += "Dear " + Name + ", <br>";
    //    html += "Thank you for your Payment.";
    //    html += "  <br>Your payment has been successfully transferred.";
    //    html += "  Our team is validating your transaction. You`ll recieve confirmation mail for the same<br><br> If you have any questions, please <a href='mailto:info@springtimesoftware.net' target='_blank'>email us</a>. <br> <br></div></div>";
    //    html += "<div class='mktEditable' id='Event-Details'><div><span style='font-family: Helvetica, Arial, sans-serif; font-size: 18px; line-height: 28px;'><strong>Transaction Details<br></strong></span></div>";
    //    html += "<table cellpadding='0' cellspacing='0' width='100%' align='center' border='0'>";
    //    html += "<tbody>";
    //    html += "<tr>";
    //    html += "<td>Transaction ID</td>";
    //    html += "  <td>" + TransactionID + "</td>";
    //    html += "</tr>";
    //    html += "  ";
    //    html += "  <tr>";
    //    html += "<td>Amount</td>";
    //    html += "  <td>INR " + Amount + "</td>";
    //    html += "</tr>";
    //    html += "  ";
    //    html += "  <tr>";
    //    html += "<td>Status</td>";
    //    html += "  <td>Success</td>";
    //    html += "</tr>";
    //    html += "  ";
    //    html += "</tbody>";
    //    html += "</table></div>";
    //    html += "";
    //    html += "";
    //    html += "";
    //    html += "";
    //    html += "";
    //    html += "<div class='mktEditable' id='Signature-Block'><div><br> <br>Best Regards,<br> Spring Time Software<strong><br></strong></div></div>";
    //    html += "";
    //    html += "<div class='mktEditable' id='Quick-Links'>";
    //    html += "<table width='100%' cellpadding='0' cellspacing='0' border='0'>";
    //    html += "<tbody><tr>";
    //    html += "<td style='padding: 20px 0px 10px 0px; border-top:1px solid #e8e8e8'>";
    //    html += "<table align='center' width='100%' cellpadding='0' cellspacing='0' border='0' style='border-collapse:collapse'>";
    //    html += "<tbody><tr>";
    //    html += "<td align='center'>";
    //    html += "<table align='center' width='100%' cellpadding='0' cellspacing='0' border='0' style='border-collapse:collapse'>";
    //    html += "<tbody><tr>";
    //    html += "<td align='center' style='font-family: Helvetica, Arial, Sans-serif; font-size: 9px; color: #999999;'>";
    //    html += "<a href='http://springtimesoftware.net' style='color: #146EB4; text-decoration:none;'>Home</a> ";
    //    html += "| <a href='http://springtimesoftware.net/products.aspx' style='color: #146EB4; text-decoration:none;'>Products</a> ";
    //    html += "| <a href='http://springtimesoftware.net/services.aspx' style='color: #146EB4; text-decoration:none;'>Services</a> ";
    //    html += "| <a href='http://springtimesoftware.net/projects.aspx' style='color: #146EB4; text-decoration:none;'>Projects</a> ";
    //    html += "| <a href='http://springtimesoftware.net/company_profile.aspx' style='color: #146EB4; text-decoration:none;'>About Us</a> ";
    //    html += "| <a href='http://springtimesoftware.net/contact_us.aspx' style='color: #146EB4; text-decoration:none;'>Contact Us</a> </td>";
    //    html += "</tr>";
    //    html += "</tbody></table>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "</tbody></table>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "</tbody></table>";
    //    html += "</div>";
    //    html += "<div class='mktEditable' id='Footer'>";
    //    html += "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";
    //    html += "<tbody>";
    //    html += "<tr>";
    //    html += "<td style='padding: 10px 0px 10px 0px; border-top: 1px solid #e8e8e8;'>";
    //    html += "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";
    //    html += "<tbody>";
    //    html += "<tr>";
    //    html += "<td style='font-size: 9px; font-family: Helvetica, Arial, sans-serif; color: #999999; line-height: 16px;' align='center' width='100%'>";
    //    html += "We hope you are enjoying our products and services.";
    //    html += "  For any assistance you may call +91-0120-4517000";
    //    html += "  <br>";
    //    html += "  2016 <a href='http://springtimesoftware.net'>Spring Time Software</a>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "</tbody>";
    //    html += "</table>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "</tbody>";
    //    html += "</table>";
    //    html += "</div>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "</tbody></table>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "<!-- End Main Content --></tbody></table>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "<!-- End Inner Container Table --></tbody></table>";
    //    html += "</td>";
    //    html += "</tr>";
    //    html += "</tbody></table>";
    //    html += "<!-- End Outer Table -->";
    //    html += "";
    //    html += "</center>";
    //    html += "";
    //    html += "";
    //    html += "";
    //    html += "</body></html>";

    //    mail.sendMail_with_response(EmailID, html, "Payment through web - Transaction Successful", out errmsg);

    //    mail_err_msg = errmsg;
    //}

    private void SendSMS()
    {

    }


}