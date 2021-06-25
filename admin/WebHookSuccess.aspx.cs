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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
//using ASPSnippets.SmsAPI;
using System.Net.Mail;
using System.Web.Script.Serialization;
using RestSharp;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class admin_WebHookSuccess : System.Web.UI.Page
{
    public String Product, Amount, FirstName, LastName, Email, Phone, Mobile, Address1, Address2, City, State, Country, ZipCode, Remarks,
       UDF1, UDF2, UDF3, UDF4, UDF5, Status, Mode, ErrorCode, PG_TYPE, Bank_Ref_Num, PayUMoneyID, AdditionalCharges, TransactionID, strMobileNo,
       strschoolName, strClassName, strSetName, strShipAddress;
    public string AppPath = "", Order = "";

    protected void Page_Load(object sender, EventArgs e)
    { 
        AppPath = Request.PhysicalApplicationPath + "ErrorLog"; 
        DAL dal = new DAL();
        string errorInsert = "",jsonerr="";
        string db_error_msg = "", mail_err_msg = "", OrderID = "", errorSMS = "";

        try
        {
            DataTable dt = new DataTable();
            var reader = new StreamReader(Request.InputStream);
            string json = reader.ReadToEnd(); 
            jsonerr = json;
             
            var files = Path.Combine(AppPath, "WebhookJsonLog-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt");
            File.WriteAllText(files, "Get Webhook Param Json string:- " + json.ToString());


            string[] jsonStringArray = Regex.Split(json.Replace("[", "").Replace("]", ""), "},{");
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

          //  dt = stsTable(json);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString().ToLower() == "success")
                {
                    Mode = dt.Rows[0]["paymentMode"].ToString();
                    Status = dt.Rows[0]["status"].ToString().ToLower();
                    Amount = dt.Rows[0]["amount"].ToString();
                    Product = dt.Rows[0]["productInfo"].ToString();
                    FirstName = dt.Rows[0]["customerName"].ToString();  
                    Email = dt.Rows[0]["customerEmail"].ToString();
                    Phone = dt.Rows[0]["customerPhone"].ToString();
                    ErrorCode = dt.Rows[0]["error_Message"].ToString(); 
                    Bank_Ref_Num = dt.Rows[0]["notificationId"].ToString();
                    PayUMoneyID = dt.Rows[0]["paymentId"].ToString(); 
                    TransactionID = dt.Rows[0]["merchantTransactionId"].ToString();

                    string otherDetails = "{TXNID:" + TransactionID + "},{Status:" + Status + "},{Mode:" + Mode + "},{ErrorCode:" + ErrorCode + "}," +
                                          "{Bank_Ref_Num:" + Bank_Ref_Num + "},{PayUMoneyID:" + PayUMoneyID + "}";
                    Session["iCompanyId"] = ConfigurationManager.AppSettings["ICompanyID"].ToString();
                    Session["iBranchID"] = ConfigurationManager.AppSettings["iBranchID"].ToString();
                    db_error_msg = "";
                    string statusremark = "", stats = "0";

                    if (Status == "success")
                    {
                        statusremark = "PaymentReceived";
                        stats = "1";
                    }
                    else
                    {
                        statusremark = Status;
                        stats = "0";
                    }
                    //dal.Order_PaymentStatus("0", stats, Remarks, statusremark, "Online-" + Mode, "", "", "WebHookUpdated", "", otherDetails, TransactionID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out OrderID, out db_error_msg);
                     
                    if (Status == "success")
                    {

                        errorInsert = "";
                        string detailPay = "Error DB:- {TXNID :- " + TransactionID + "},{ STATUS :- " + Status + " },{ MODE :- " + Mode + "},{ ErrorCode :- " + ErrorCode + "},{ Bank_Ref_Num :-" + Bank_Ref_Num + "},{ PayUMoneyID :- " + PayUMoneyID + "}";

                        DataSet dtset = new DataSet();
                        string errorOrdr = "";
                       // dtset = dal.get_OrderDetail("", false, TransactionID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errorOrdr);
                        if (errorOrdr == "success")
                        {
                            if (dtset.Tables.Count > 0)
                            {
                                if (dtset.Tables[0].Rows.Count > 0)
                                {
                                    strschoolName = dtset.Tables[0].Rows[0]["SchoolName"] + "";
                                    strClassName = dtset.Tables[0].Rows[0]["SetName"] + "";
                                    strSetName = dtset.Tables[0].Rows[0]["ClassDesc"] + "";
                                    strShipAddress = dtset.Tables[0].Rows[0]["ShipAddress"] + "";
                                }
                            }
                            else
                            {
                                var file = Path.Combine(AppPath, "PaymentErrorLog-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt");
                                File.WriteAllText(file, "Error in Order Detail:- Failed to Load Order Details " + TransactionID);
                            }
                        }
                        else
                        {
                            var file = Path.Combine(AppPath, "OrderErrorLog-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt");
                            File.WriteAllText(file, "Error in Order Detail:- Failed to Load Order Details  Error :-" + errorOrdr);
                        }

                        string ProviderURL = "", UserIDCaption = "", UserIDValue = "", PasswordCaption = "", PasswordValue = "", MsgCaption = "", MsgTypeCaption = "",
                        MsgTypeValue = "", BothSameCaption = "", CDMACaption = "", GSMCaption = "", SenderName = "", Sresult = "", Message = "";
                        bool BothSame = false;
                        DAL md = new DAL();
                        DataTable data = new DataTable();
                        //data = md.get_SendSMSAPIINFO(Session["iCompanyId"].ToString(), out errorSMS);
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

                            Message = "Thank you for placing order with eazyshopee, your transaction number is: " + TransactionID + " and Order Amount is : " + Amount.ToString() + " and Your PayumoneyID is - " + PayUMoneyID.ToString() + ". To Check Your Order Please follow given Link: https://eazyshopee.com/order_track.aspx";
                            Sresult = ProviderURL + "&" + UserIDCaption + "=" + UserIDValue + "&" + PasswordCaption + "=" + PasswordValue + "&" + MsgCaption
                            + "=" + Message + "&" + MsgTypeCaption + "=" + MsgTypeValue + "&" + BothSameCaption + "=" + Phone.ToString();
                            new System.Net.WebClient().DownloadString(Sresult);
                        }
                        else
                        {
                            var file = Path.Combine(AppPath, "SMS_CredentialsErrorLog-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt");
                            File.WriteAllText(file, "Error DB:- {TXNID :- " + TransactionID + "},{ STATUS :- " + Status + " },{ MODE :- " + Mode + "},{ ErrorCode :- " + ErrorCode + "},{ Bank_Ref_Num :-" + Bank_Ref_Num + "},{ PayUMoneyID :- " + PayUMoneyID + "}" + "  SMS_Credentials Error:- " + errorSMS + " ErrorNotInsert : - " + errorInsert);
                        }

                        Email_Send_Manual(Email, FirstName, Product, TransactionID, Amount, strschoolName, strClassName, strSetName, strShipAddress, Phone.ToString(), out mail_err_msg);

                        if (mail_err_msg != "success")
                        {
                            Session["MailSent"] = "MailNotSent";
                            var Emailfile = Path.Combine(AppPath, "Send_EmailErrorLog-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt");
                            File.WriteAllText(Emailfile, "Error DB:- {TXNID :- " + TransactionID + "},{ STATUS :- " + Status + " },{ MODE :- " + Mode + "},{ ErrorCode :- " + ErrorCode + "},{ PG_TYPE :- " + PG_TYPE + "},{ Bank_Ref_Num :-" + Bank_Ref_Num + "},{ PayUMoneyID :- " + PayUMoneyID + "},{ AdditionalCharges :- " + AdditionalCharges + "}" + "  Email Error:- " + mail_err_msg + " ErrorNotInsert : - " + errorInsert);
                        }
                    }
                }
                else if (dt.Rows[0]["status"].ToString() != "success")
                {
                    SqlCommand coms = new SqlCommand(" Update [TransOrder] set [STATUS] = 0 ,  StatusRemark = '" + dt.Rows[0]["unmappedstatus"].ToString() + " status -" + dt.Rows[0]["status"].ToString() + "' where TransactionIDord = " + TransactionID + "", CommonCode.con);
                    DataTable dte = new DataTable();
                   string errmsg = ""; 
                    dte = CommonCode.getData(coms, out errmsg); 
                    var filess = Path.Combine(AppPath, "WebhookstatusErrorLog-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt");
                    File.WriteAllText(filess, "Webhook Error Transaction Status not success in Json string:- " + json.ToString());
                }
            }
            else
            {  
                var filess = Path.Combine(AppPath, "WebhooktableJsonLog-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt");
                File.WriteAllText(filess, "Get Webhook data table coming blank Json string:- " + json.ToString()); 
            }
        }
        catch (Exception ex)
        {
            var file = Path.Combine(AppPath, "PaymentErrorLog-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt");
            File.WriteAllText(file, "Error in Order Detail:- Failed to Insert JSON :-  " + ex.Message + " - Json string:- " + jsonerr.ToString());
        }
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
            if (item.Name == "result")
            {
                id = item.Value;
                foreach (JObject jProp in id.Children().ToList())
                {
                    var srcArray = jProp["postBackParam"];
                    List<JToken> results1 = srcArray.Children().ToList();
                    var cleanRow = new JObject();
                    foreach (JProperty item1 in results1)
                    {
                        item1.CreateReader();
                        JToken id1;
                        id1 = item1.Value;
                        if (item1.Value is JValue)
                        {
                            cleanRow.Add(item1.Name, item1.Value);
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
            if (blnValFound == true)
            {
                break;
            }
        } 
        return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
    }


    private void Email_Send_Manual(String EmailID, String Name, String Product, string TransactionID, string Amount, string strschoolName, string strClassName, string strSetName, string strShipAddress, string strPhone, out string mail_err_msg)
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
            //string ssql = " exec web_GetEmailConfig '" + Session["iCompanyId"].ToString() + "','" + Session["iBranchID"].ToString() + "' ";
            string ssql = " select * from EmailConfig where iCompanyID =  '" + Session["iCompanyId"].ToString() + "' and iBranchID = '" + Session["iBranchID"].ToString() + "' ";
            SqlCommand com = new SqlCommand(ssql, CommonCode.con);
            DataTable dt = new DataTable();
            string errmsg;
            dt = CommonCode.getData(com, out errmsg);

            //if (dt.Rows.Count > 0)
            //{
            //    txtEmail = dt.Rows[0]["EmailFrom"].ToString();
            //    txtPassword = dt.Rows[0]["EmailPassword"].ToString().Trim();
            //    smtpHostName = dt.Rows[0]["SMTPHost"].ToString();
            //    smtpPortNo = int.Parse(dt.Rows[0]["SMTPPort"].ToString());
            //    sslenable = bool.Parse(dt.Rows[0]["EnableSsl"].ToString());
            //}
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
            mm.Body += "<br/><b>";
            mm.Body += "    School Name : - " + strschoolName;
            mm.Body += "<br/>";
            mm.Body += "    Class Name : - " + strClassName;
            mm.Body += "<br/>";
            mm.Body += "    Set / Bundle Name : - " + strSetName;
            mm.Body += "</b><br/>";
            mm.Body += "    Delivery Address : - " + strShipAddress;
            mm.Body += "<br/>";
            mm.Body += "    Mobile :-" + strPhone;
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

}