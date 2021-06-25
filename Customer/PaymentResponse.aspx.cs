using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using CCA.Util;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Linq;
public partial class _ResponseHandler : System.Web.UI.Page 
    {
        public String tid_C, merchant_id_C, order_id_C, amount_C, currency_C, BanklRefNo_C , OrderStatus_C, failure_message_C , paymentMode_C , CardName_C, 
        billing_name_C, billing_address_C, billing_city_C, billing_state_C, billing_zip_C, billing_country_C, billing_tel_C, billing_email_C, 
        delivery_name_C, delivery_address_C, delivery_city_C, delivery_state_C, delivery_zip_C, delivery_country_C, delivery_tel_C, 
        merchant_param1_C, merchant_param2_C, merchant_param3_C, merchant_param4_C, merchant_param5_C, status_code_C, Status_message_C,  
        promo_code_C, customer_identifier_C, tracking_id_C,  mer_amount_C, order_status_C;
        DateTime trans_date_C;
    public string[] pay_response = new string[4];
    protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["RefNO"] != null)
                {
                    TestPaymentcheck();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Payment session Expired, taking you to login Window');window.location ='user_login.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            } 
        } 
    public DataTable JsonStringToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        List<string> ColumnsName = new List<string>();
        foreach (string jSA in jsonStringArray)
        {
            string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            foreach (string ColumnsNameData in jsonStringData)
            {
                try
                {
                    int idx = ColumnsNameData.IndexOf(":");
                    string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                    if (!ColumnsName.Contains(ColumnsNameString))
                    {
                        ColumnsName.Add(ColumnsNameString);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                }
            }
            break;
        }
        foreach (string AddColumnName in ColumnsName)
        {
            dt.Columns.Add(AddColumnName);
        }
        foreach (string jSA in jsonStringArray)
        {
            string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in RowData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[RowColumns] = RowDataString;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }

    private void TestPaymentcheck()
    {
        try
        {
            
            DataTable dt = new DataTable();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://secure.telr.com/");
                client.DefaultRequestHeaders.ExpectContinue = false;
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                var result = client.PostAsync("gateway/order.json",
                    new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
                    {
                new KeyValuePair<string, string>("ivp_method", "check"),
                new KeyValuePair<string, string>("ivp_store", "23847"),
                new KeyValuePair<string, string>("ivp_authkey", "QZg5@RgVfW-WVSN8"),
                new KeyValuePair<string, string>("order_ref",Session["RefNO"].ToString()),//Session["RefNO"].ToString()), // Session["RefNO"].ToString()),//
                    })).Result;
                if (result.IsSuccessStatusCode)
                {
                    var json = (result.Content.ReadAsStringAsync().Result);
                    json = JsonConvert.DeserializeObject(json).ToString();

                   dt = stsTable(json); 

                    if (dt.Rows.Count > 0)
                    { 
                        string db_error_msg = "", mail_err_msg = "", OrderID = "", errmsg = "";
                        DAL dal = new DAL();

                        DataTable dts = new DataTable();
                        DAL dl = new DAL();
                        dts = dl.ICompanyID_BranchID(out errmsg);
                        if (dts.Rows.Count > 0)
                        {
                            Session["iCompanyId"] = dts.Rows[0]["iCompanyId"].ToString();
                            Session["iBranchID"] = dts.Rows[0]["iBranchID"].ToString();
                            Session["FinancialPeriod"] = dts.Rows[0]["FinancialPeriod"].ToString();
                        }
                        if (dt.Rows[0]["status_text"].ToString().ToLower() == "paid")
                        {
                            tid_C = dt.Rows[0]["ref"].ToString();
                            order_id_C = dt.Rows[0]["cartid"].ToString();
                            tracking_id_C = dt.Rows[0]["transaction_ref"].ToString();                        
                            order_status_C = dt.Rows[0]["transaction_message"].ToString();                        
                            BanklRefNo_C = dt.Rows[0]["transaction_ref"].ToString();
                            amount_C = dt.Rows[0]["amount"].ToString();
                            status_code_C = dt.Rows[0]["status_code"].ToString();
                            Status_message_C = dt.Rows[0]["status_text"].ToString();
                            paymentMode_C = dt.Rows[0]["paymethod"].ToString();
                            currency_C = dt.Rows[0]["currency"].ToString();
                            billing_email_C = dt.Rows[0]["customer_email"].ToString();
                            billing_name_C = dt.Rows[0]["name_forenames"].ToString() + " " + dt.Rows[0]["name_surname"].ToString();
                            merchant_param1_C = dt.Rows[0]["description"].ToString();
                            

                            string otherDetails = "{Ref:" + tracking_id_C + "},{order_status:" + order_status_C + "},{PaymentMode:" + paymentMode_C +
                                                "},{status_message:" + Status_message_C + "},{trans_date:" + DateTime.Now + "}," + "{Bank_Ref_Num:" + BanklRefNo_C + "}"
                                                + "{TransType:" + dt.Rows[0]["transaction_type"].ToString() + "}" + "{Transclass:" + dt.Rows[0]["transaction_class"].ToString() + "}"
                                                + "{TransStatus:" + dt.Rows[0]["transaction_status"].ToString() + "}" + "{Transcode:" + dt.Rows[0]["transaction_code"].ToString() + "}"
                                                + "{CTYpe:" + dt.Rows[0]["card_type"].ToString() + "}" + "{Clast4:" + dt.Rows[0]["card_last4"].ToString() + "}" + "{Ccountry:" + dt.Rows[0]["card_country"].ToString() + "}"
                                                + "{Cfirst6:" + dt.Rows[0]["card_first6"].ToString() + "}" + "{expiry_month:" + dt.Rows[0]["expiry_month"].ToString() + "}" + "{expiry_year:" + dt.Rows[0]["expiry_year"].ToString() + "}"
                                                + "{expiry_year:" + dt.Rows[0]["expiry_year"].ToString() + "}"  ;

                            //Session["CustID"] = dt.Rows[0]["address_line2"].ToString();

                            SqlCommand com = new SqlCommand("Web_Customer_login", CommonCode.con);
                            com.CommandType = CommandType.StoredProcedure;
                            com.Parameters.Add("@UserName", SqlDbType.VarChar, 200).Value = dt.Rows[0]["customer_email"].ToString();
                            com.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = "";
                            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
                            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
                            SqlDataAdapter ad = new SqlDataAdapter(com);
                            DataTable dt2 = new DataTable();
                            ad.Fill(dt2);
                            if (dt2.Rows.Count > 0)
                            {
                                Session["CustID"] = dt2.Rows[0]["CustID"].ToString();
                                Session["CustEmail"] = dt2.Rows[0]["EmailID"].ToString();
                                Session["CustName"] = dt2.Rows[0]["CustName"].ToString();
                                Session["CustType"] = "Retail";
                            }


                            //dal.Order_PaymentResStatus(order_id_C, tracking_id_C, amount_C.ToString(), "0", "", "", "1", "PaymentReceived", paymentMode_C, dt.Rows[0]["card_type"].ToString(), 
                            //    dt.Rows[0]["card_last4"].ToString(), dt.Rows[0]["card_last4"].ToString(), dt.Rows[0]["expiry_month"].ToString() + " " + dt.Rows[0]["expiry_year"].ToString().Substring(dt.Rows[0]["expiry_year"].ToString().Length - 2), otherDetails, 
                            //                     merchant_param3_C, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), Session["FinancialPeriod"].ToString(), Session["CustID"].ToString(), out OrderID, out db_error_msg);

                            
                             
                            /*
                               public void Order_PaymentResStatus(string OrderID, string TotalQty, string TotalAmount, string ShipCost,
                                string ShipMethod, string Remark, string Status, string StatusRemark, string PayMethod, string CardType, string NameOnCard,
                                string CardNum, DateTime CardExpires, string OtherDetail, String TxnID, String iCompanyID, String iBranchID,
                                out string OrderID_output, out string errmsg)  
                            */
                             
                            Email_Send(billing_email_C, billing_name_C, merchant_param1_C, order_id_C, amount_C, out mail_err_msg);

                            string details = "";
                            details += "<li class='text-success'>Transaction ID- " + order_id_C + "</li>";
                            details += "<li class='text-success'> Amount- "+ ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + amount_C + "</li>";

                            if (db_error_msg == "success")
                                details += "<li class='text-success'>[Payment TRANS] - <i class='fa fa-check'></i> Successful</li>";
                            else
                                details += "<li class='text-danger'>[Payment TRANS] - <i class='fa fa-times'></i> Unsuccessful - " + db_error_msg + "</li>";

                            if (mail_err_msg == "success")
                                details += "<li class='text-success'>[Mail TRANS] - <i class='fa fa-check'></i> Successful</li>";
                            else
                                details += "<li class='text-danger'>[Mail TRANS] - <i class='fa fa-times'></i> Unsuccessful - " + mail_err_msg + "</li>";

                            send_response("success", "Payment processed successfully", "Details<br><ul>" + details + "</ul>", order_id_C);
                        }
                        else
                        {
                            string err_details = "";
                            DataTable dtE = new DataTable();
                            SqlCommand com = new SqlCommand(" Update TransOrder set StatusRemark = '"+ dt.Rows[0]["status_text"].ToString() + "' where TransactionIDord = '" + order_id_C + "' and iCompanyId = '" + Session["iCompanyId"].ToString() + "' and iBranchId = '" + Session["iBranchID"].ToString() + "'", CommonCode.con);
                            dtE = CommonCode.getData(com, out err_details);

                            err_details = "<li>Status - " + dt.Rows[0]["status_text"].ToString() + "</li>";
                            send_response("info", "Payment unsuccessful !", "" + "Possible reason for error<br> <ul>" + err_details + "</ul>", order_id_C);
                        } 
                    } 
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

   
    private JObject rtnInnerTable(JObject cleanRow , JProperty MAinjProp, List<JToken> resultsMain)
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
                cleanRow= rtnInnerTable(  cleanRow, jProp, resultsInner);
            }
        }


        return cleanRow;
    }
    public DataTable stsTable(string json)
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
            if (item.Name == "order")
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


    public void send_response(string status, string title, string msg, string OrderID)
    {        
        //string ssql = "";
        //ssql = " Select OrderID From [Order] where OrderID = '" + OrderID + "'";
        //SqlCommand com = new SqlCommand(ssql, CommonCode.con);
        //DataTable dt = new DataTable();
        //string errmsg;
        //dt = CommonCode.getData(com, out errmsg);
        //if (errmsg == "success")
        //{
        //    OrderID = dt.Rows[0]["OrderID"].ToString();
        //    dt.Dispose();
            

        //}
        //else
        //{
        //    dt.Dispose();
        //    send_response("info", "Order not Generated Against Transaction !", "" + "Possible reason for error<br> " + errmsg, "");
        //}

        try
        {
            pay_response[0] = status;
            pay_response[1] = title;
            pay_response[2] = msg;
            pay_response[3] = OrderID;
            Session["pay_response"] = pay_response; 
            Response.Redirect("postpay.aspx", false);
        }
        catch(Exception ex)
        {
            send_response("info", "Order not Generated Against Transaction !", "" + "Possible reason for error<br> " + ex.Message, order_id_C);
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

        //if (dt.Rows.Count > 0)
        //{
        //    txtEmail = dt.Rows[0]["SMTPUserid"].ToString();
        //    txtPassword = dt.Rows[0]["SMTPPassword"].ToString().Trim();
        //    smtpHostName = dt.Rows[0]["SMTPHost"].ToString();
        //    smtpPortNo = int.Parse(dt.Rows[0]["SMTPPortNo"].ToString());
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
            mm.Body += "            We are happy to inform you that your payment request has been successfully received and the details are as follows: ";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<b>Transaction Reference Number :  " + TransactionID + "</b>";
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
            mm.Body += "<b> Transaction Status :  " + "SUCCESSFULL PAYMENT" + "</b> .";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += "Best Regards,";
            mm.Body += "<br/>";
            mm.Body += "<br/>";
            mm.Body += ConfigurationManager.AppSettings["CompanyName"].ToString() + ".";

            SmtpClient smtp = new SmtpClient();

            smtp.Host = smtpHostName; //"smtpout.secureserver.net" ;
            //smtp.Host = "relay-hosting.secureserver.net";

            smtp.EnableSsl = sslenable;
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


    private void InsertOrderPayment()
    {
        string workingKey = ConfigurationManager.AppSettings["WorkingKey"]; //  "EF8D27393CBFFD5DB36B5DD3BCD25A0A";//put in the 32bit alpha numeric key in the quotes provided here
        CCACrypto ccaCrypto = new CCACrypto();
        string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
        NameValueCollection Params = new NameValueCollection();
        string[] segments = encResponse.Split('&');
        string[] parts;
        // Order ID -----------
        parts = segments[0].Split('=');
        if (parts.Length > 0)
        {
            order_id_C = parts[1].Trim();
        }
        // Tracking ID -----------
        parts = segments[1].Split('=');
        if (parts.Length > 0)
        {
            tracking_id_C = parts[1].Trim();
        }
        // bank_ref_no -----------
        parts = segments[2].Split('=');
        if (parts.Length > 0)
        {
            BanklRefNo_C = parts[1].Trim();
        }
        // order_status -----------
        parts = segments[3].Split('=');
        if (parts.Length > 0)
        {
            order_status_C = parts[1].Trim();
        }
        // payment_mode   -----------
        parts = segments[5].Split('=');
        if (parts.Length > 0)
        {
            paymentMode_C = parts[1].Trim();
        }
        // card_name    -----------
        parts = segments[6].Split('=');
        if (parts.Length > 0)
        {
            CardName_C = parts[1].Trim();
        }
        parts = segments[8].Split('=');
        if (parts.Length > 0)
        {
            Status_message_C = parts[1].Trim();
        }
        // currency --------------------
        parts = segments[9].Split('=');
        if (parts.Length > 0)
        {
            currency_C = parts[1].Trim();
        }
        // amount --------------------
        parts = segments[10].Split('=');
        if (parts.Length > 0)
        {
            amount_C = parts[1].Trim();
        }
        // billing_name-----------
        parts = segments[11].Split('=');
        if (parts.Length > 0)
        {
            billing_name_C = parts[1].Trim();
        }
        // billing_address -----------
        parts = segments[12].Split('=');
        if (parts.Length > 0)
        {
            billing_address_C = parts[1].Trim();
        }
        // billing_city -----------
        parts = segments[13].Split('=');
        if (parts.Length > 0)
        {
            billing_city_C = parts[1].Trim();
        }
        // billing_state -----------
        parts = segments[14].Split('=');
        if (parts.Length > 0)
        {
            billing_state_C = parts[1].Trim();
        }
        // billing_zip   -----------
        parts = segments[15].Split('=');
        if (parts.Length > 0)
        {
            billing_zip_C = parts[1].Trim();
        }
        // billing_country    -----------
        parts = segments[16].Split('=');
        if (parts.Length > 0)
        {
            billing_country_C = parts[1].Trim();
        }
        // billing_tel     -----------
        parts = segments[17].Split('=');
        if (parts.Length > 0)
        {
            billing_tel_C = parts[1].Trim();
        }
        // billing_email     -----------
        parts = segments[18].Split('=');
        if (parts.Length > 0)
        {
            billing_email_C = parts[1].Trim();
        }
        // delivery_name  --------------------
        parts = segments[19].Split('=');
        if (parts.Length > 0)
        {
            delivery_name_C = parts[1].Trim();
        }
        // delivery_address --------------------
        parts = segments[20].Split('=');
        if (parts.Length > 0)
        {
            delivery_address_C = parts[1].Trim();
        }
        // delivery_city  --------------------
        parts = segments[21].Split('=');
        if (parts.Length > 0)
        {
            delivery_city_C = parts[1].Trim();
        }
        // delivery_state  --------------------
        parts = segments[22].Split('=');
        if (parts.Length > 0)
        {
            delivery_state_C = parts[1].Trim();
        }

        // delivery_zip  --------------------
        parts = segments[23].Split('=');
        if (parts.Length > 0)
        {
            delivery_zip_C = parts[1].Trim();
        }
        // delivery_country  --------------------
        parts = segments[24].Split('=');
        if (parts.Length > 0)
        {
            delivery_country_C = parts[1].Trim();
        }
        // delivery_tel  --------------------
        parts = segments[25].Split('=');
        if (parts.Length > 0)
        {
            delivery_tel_C = parts[1].Trim();
        }
        // merchant_param1  --------------------
        parts = segments[26].Split('=');
        if (parts.Length > 0)
        {
            merchant_param1_C = parts[1].Trim();
        }
        // merchant_param2 --------------------
        parts = segments[27].Split('=');
        if (parts.Length > 0)
        {
            merchant_param2_C = parts[1].Trim();
        }
        // merchant_param3 --------------------
        parts = segments[28].Split('=');
        if (parts.Length > 0)
        {
            merchant_param3_C = parts[1].Trim();
        }
        // merchant_param4 --------------------
        parts = segments[29].Split('=');
        if (parts.Length > 0)
        {
            merchant_param4_C = parts[1].Trim();
        }
        // merchant_param5 --------------------
        parts = segments[30].Split('=');
        if (parts.Length > 0)
        {
            merchant_param5_C = parts[1].Trim();
        }
        parts = segments[35].Split('=');
        if (parts.Length > 0)
        {
            mer_amount_C = parts[1].Trim();
        }

        String custids = "";
        if (Session["CustID"] != null)
        {
            custids = Session["CustID"].ToString();
        }
        else
        {
            Session["CustID"] = merchant_param2_C;
        }

        string db_error_msg = "", mail_err_msg = "", OrderID = "", errmsg = "";
        DAL dal = new DAL();

        DataTable dt = new DataTable();
        DAL dl = new DAL();
        dt = dl.ICompanyID_BranchID(out errmsg);
        if (dt.Rows.Count > 0)
        {
            Session["iCompanyId"] = dt.Rows[0]["iCompanyId"].ToString();
            Session["iBranchID"] = dt.Rows[0]["iBranchID"].ToString();
            Session["FinancialPeriod"] = dt.Rows[0]["FinancialPeriod"].ToString();
        }

        //string otherDetails = "{TrackingID:" + tracking_id_C + "},{order_status:" + order_status_C + "},{PaymentMode:" + paymentMode_C +
        //                        "},{status_message:" + Status_message_C + "},{trans_date:" + DateTime.Now + "}," + "{Bank_Ref_Num:" + BanklRefNo_C + "}";
        if (order_status_C.ToLower() == "success")
        {
            //    dal.Order_PaymentResStatus(order_id_C, merchant_param1_C, amount_C.ToString(), "0", "", "", "1", "PaymentReceived", paymentMode_C,
            //                         CardName_C, tracking_id_C, BanklRefNo_C, DateTime.Now, otherDetails, merchant_param3_C, Session["iCompanyId"].ToString(),
            //                         Session["iBranchID"].ToString(), out OrderID, out db_error_msg);
            Email_Send(billing_email_C, billing_name_C, merchant_param1_C, order_id_C, amount_C, out mail_err_msg);

            string details = "";
            details += "<li class='text-success'>Transaction ID- " + order_id_C + "</li>";
            details += "<li class='text-success'> Amount- INR " + amount_C + "</li>";

            if (db_error_msg == "success")
                details += "<li class='text-success'>[Payment TRANS] - <i class='fa fa-check'></i> Successful</li>";
            else
                details += "<li class='text-danger'>[Payment TRANS] - <i class='fa fa-times'></i> Unsuccessful - " + db_error_msg + "</li>";

            if (mail_err_msg == "success")
                details += "<li class='text-success'>[Mail TRANS] - <i class='fa fa-check'></i> Successful</li>";
            else
                details += "<li class='text-danger'>[Mail TRANS] - <i class='fa fa-times'></i> Unsuccessful - " + mail_err_msg + "</li>";

            send_response("success", "Payment processed successfully", "Details<br><ul>" + details + "</ul>", order_id_C);
        }
        else
        {
            string err_details = "";
            DataTable dts = new DataTable();
            SqlCommand com = new SqlCommand(" Update TransOrder set StatusRemark = 'Cancelled' where TransactionIDord = '" + order_id_C + "' and iCompanyId = '" + Session["iCompanyId"].ToString() + "' and iBranchId = '" + Session["iBranchID"].ToString() + "'", CommonCode.con);
            dts = CommonCode.getData(com, out err_details);

            err_details += "<li>Status - " + order_status_C + "</li>";
            send_response("info", "Payment unsuccessful !", "" + "Possible reason for error<br> <ul>" + err_details + "</ul>", order_id_C);
        }
    }

    private void printKeysAndValues(string json)
    {
        var jobject = (JObject)((JArray)JsonConvert.DeserializeObject(json))[2];
        String Param = "";
        foreach (var jproperty in jobject.Properties())
        {
            Console.WriteLine("{0} - {1}", jproperty.Name, jproperty.Value);
        }
    }


}

