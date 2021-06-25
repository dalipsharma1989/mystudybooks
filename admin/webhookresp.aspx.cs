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

public partial class admin_webhookresp : System.Web.UI.Page
{
    public string AppPath = "", Order = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        AppPath = Request.PhysicalApplicationPath + "ErrorLog";
        DAL dal = new DAL();
        string errorInsert = "", jsonerr = "";
        string db_error_msg = "", mail_err_msg = "", OrderID = "", errorSMS = "";

        DataTable dt = new DataTable();
        var reader = new StreamReader(Request.InputStream);
        string json = reader.ReadToEnd();
        jsonerr = json;

        var files = Path.Combine(AppPath, "WebhookJsonLog-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt");
        File.WriteAllText(files, "Get Webhook Param Json string:- " + json.ToString());
    }
}