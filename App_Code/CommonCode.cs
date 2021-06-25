using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.UI;
using System.IO;
/// <summary>
/// Summary description for CommonCode
/// </summary>
public class CommonCode
{
    public static String CompanyName = WebConfigurationManager.AppSettings["CompanyName"];
    public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

    public static String ExecuteNoQuery(SqlCommand com, SqlConnection con)
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            return "success";
        }
        catch (Exception ex)
        {
            con.Close();
            return ex.Message;
        }
    }

    public static DataTable getData(SqlCommand com, out String errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
            {
                com.Connection = con;
                com.CommandTimeout = 0;
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
                errmsg = "success";
                con.Close();
            }
        }
        catch (Exception ex)
        {
            dt = null;
            errmsg = ex.Message;
        }
        return dt;
    }

    public static DataSet getDataInDataSet(SqlCommand com, out String errmsg)
    {
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString))
            {
                com.Connection = con;
                com.CommandTimeout = 0;
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(ds);
                errmsg = "success";
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ds = null;
            errmsg = ex.Message;
        }
        return ds;
    }

    public static string AppSettings(string SettingName)
    {
        return WebConfigurationManager.AppSettings[SettingName];
    }

    public static Guid CompanyID()
    {
        return new Guid(WebConfigurationManager.AppSettings["companyID"]);
    }

    public static string AmountFormat()
    {
        string format = "{0:0.00}";
        string prec_zeros = "";
        try
        {
            int decimals = Convert.ToInt32(WebConfigurationManager.AppSettings["AmountUptoDecimal"]);
            for (int i = 1; i <= decimals; i++)
            {
                prec_zeros += "0";
            }
            format = "{0:0." + prec_zeros + "}";
        }
        catch (Exception)
        {
            format = "{0:0.00}";
        }
        return format;
    }
    
     

    public static void bind_languages(DropDownList ddl, out string errmsg)
    {
        SqlCommand com = new SqlCommand("dbo_get_languages", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        ddl.Items.Clear();
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                ddl.Items.Add(new ListItem("Select Language", "Nil"));
                foreach (DataRow rr in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(rr["LanguageName"] + "", rr["LanguageID"] + ""));
                }
            }
            else
            {
                ddl.Items.Add(new ListItem("No Language Found", "Nil"));
            }
        }
        else
        {
            ddl.Items.Add(new ListItem("No Language Found", "Nil"));
        }
    }

    public static void CreateDirectory(String DirectoryName)
    {
        string directoryPath = HttpContext.Current.Server.MapPath(string.Format("~/resources/{0}/", DirectoryName));
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
    public static void CreateDirectoryBookList(String DirectoryName)
    {
        string directoryPath = HttpContext.Current.Server.MapPath(string.Format("~/resources/UsersBookList/{0}/", DirectoryName));
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public static String SafeFileName(String filename)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
        {
            filename = filename.Replace(c, '-');
        }
        return filename;
    }

    public static String AddToCart(String CustID, String ProductID, string iCompanyID, string iBranchID, out String outCartID)
    {
        String errmsg = "";
        outCartID = "";
        bool blnexist=false;
        //add to cart
        if (string.IsNullOrEmpty(CustID) || CustID == "guest")
        {
            DataTable dt = new DataTable();

            if (HttpContext.Current.Session["tmpcart"] == null)
            {
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("ProductID", typeof(String)), new DataColumn("Qty", typeof(String)) });
            }
            else
            {
                dt = HttpContext.Current.Session["tmpcart"] as DataTable;
                DataRow[] foundRows = dt.Select("ProductID = '"+ ProductID + "'");
                if (foundRows.Length > 0)
                {
                    blnexist = true;
                    goto skip_to_add_row;
                }
            }
            dt.Rows.Add(ProductID, "1");
            skip_to_add_row:
            HttpContext.Current.Session["tmpcart"] = dt;
            if (blnexist==true )            
                errmsg = "already exist";                    
            else
                errmsg = "success";

            outCartID = "tmpcart";
        }
        else
        {

            string ssql = "";
            ssql = " select top 1 C.CartID from ( Web_CustCart C inner join Web_CustCartDetails CD on c.CartID = cd.CartId )  ";
            ssql = ssql + " where CustID = '" + CustID + "' and Status = 0 and c.icompanyid = '" + iCompanyID + "' and c.iBranchID= '" + iBranchID + "'  and isvalidupto >= convert(date,getdate()) And ProductId = '" + ProductID + "'";

            SqlCommand comm = new SqlCommand(ssql, CommonCode.con);
            DataTable dt = new DataTable();
            string em;
            dt = CommonCode.getData(comm, out em);

            if( dt.Rows.Count>0)
            {
                dt.Dispose();
                errmsg = "already exist";
                outCartID = dt.Rows[0]["CartID"].ToString();
            }
            else
            {
                dt.Dispose();
                SqlCommand com = new SqlCommand("web_insert_in_cart", CommonCode.con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = CustID;
                com.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = "-";
                com.Parameters.Add("@ProductId", SqlDbType.VarChar,20).Value = ProductID;
                com.Parameters.Add("@Qty", SqlDbType.Int).Value = "1";
                com.Parameters.Add("@CartID", SqlDbType.BigInt);
                com.Parameters["@CartID"].Direction = ParameterDirection.Output;
                com.Parameters.Add("@icompanyid", SqlDbType.Int).Value = iCompanyID;
                com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
                errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
                if (!string.IsNullOrEmpty(com.Parameters["@CartID"].Value.ToString()))
                    outCartID = com.Parameters["@CartID"].Value.ToString();
            }
            
        }

        return errmsg;
    }

    public static String AddToCart(String CustID, String ProductID, int Qty, out String outCartID)
    {
        String errmsg = "";
        outCartID = "";
        //add to cart
        if (string.IsNullOrEmpty(CustID) || CustID == "guest")
        {
            DataTable dt = new DataTable();

            if (HttpContext.Current.Session["tmpcart"] == null)
            {
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("ProductID", typeof(String)), new DataColumn("Qty", typeof(String)) });
            }
            else
            {
                dt = HttpContext.Current.Session["tmpcart"] as DataTable;
                DataRow[] foundRows = dt.Select("ProductID = " + ProductID + "");
                if (foundRows.Length > 0)
                {
                    goto skip_to_add_row;
                }
            }
            dt.Rows.Add(ProductID, Qty + "");
            skip_to_add_row:
            HttpContext.Current.Session["tmpcart"] = dt;
            errmsg = "success";
            outCartID = "tmpcart";
        }
        else
        {
            SqlCommand com = new SqlCommand("dbo_insert_in_cart", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = CustID;
            com.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = "-";
            com.Parameters.Add("@ProductId", SqlDbType.BigInt).Value = ProductID;
            com.Parameters.Add("@Qty", SqlDbType.Int).Value = Qty;
            com.Parameters.Add("@CartID", SqlDbType.BigInt);
            com.Parameters["@CartID"].Direction = ParameterDirection.Output;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (!string.IsNullOrEmpty(com.Parameters["@CartID"].Value.ToString()))
                outCartID = com.Parameters["@CartID"].Value.ToString();
        }

        return errmsg;

    }

    public static String AddToWishlist(String CustID, String ProductID, String icompanyid, String iBranchID) 
    {
        string errmsg = "";
        if (CustID == "guest")
        {
            DataTable dt = new DataTable();

            if (HttpContext.Current.Session["tmpwishlist"] == null)
            {
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("CustID", typeof(String)), new DataColumn("ProductID", typeof(String)) });
            }
            else
            {
                dt = HttpContext.Current.Session["tmpwishlist"] as DataTable;
                DataRow[] foundRows = dt.Select("ProductID = " + ProductID + "");
                if (foundRows.Length > 0)
                {
                    errmsg = "Product is already in your wishlist !";
                    goto skip_to_add_row;

                }
            }
            dt.Rows.Add("guest", ProductID);
            errmsg = "success";
            skip_to_add_row:
            HttpContext.Current.Session["tmpwishlist"] = dt;
        }
        else
        {
            DAL dal = new DAL();
            dal.insert_edit_delete_wishlist("0", CustID, ProductID, 0, icompanyid, iBranchID, out errmsg); 
        }
        return errmsg;
    }
     

    public static DataTable get_city_details_by_cityid(String CityID)
    {
        DataTable dt = new DataTable();
        SqlCommand com = new SqlCommand("dbo_get_city_details_by_cityid", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("CityID", SqlDbType.BigInt).Value = CityID;
        com.Parameters.Add("CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
        string errmsg = "";
        dt = CommonCode.getData(com, out errmsg);
        return dt;
    }

    public static void getCountry_Details(int Mode, String CountryID, String StateID, DropDownList ddl)
    {
        String aderrmsg = "success";
        string ddl_mode = "Country";
        if (Mode == 1)
            ddl_mode = "State";
        else if (Mode == 2)
            ddl_mode = "City";

        SqlCommand com = new SqlCommand("Web_Get_Country_Details", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@isMode", SqlDbType.Int).Value = Mode;
        com.Parameters.Add("@CountryID", SqlDbType.VarChar).Value = CountryID;
        com.Parameters.Add("@StateID", SqlDbType.VarChar).Value = StateID;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out aderrmsg);
        if (aderrmsg == "success")
        {
            ddl.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                ddl.Items.Add(new ListItem("Select " + ddl_mode, "Nil"));
                foreach (DataRow rr in dt.Rows)
                {
                    ddl.Items.Add(new ListItem(rr[1].ToString(), rr[0].ToString()));
                }
            }
            else
            {
                ddl.Items.Add(new ListItem("No " + ddl_mode + " Found", "Nil"));
            }
        }
    }

    public static String DetectDeviceType()
    {
        string userAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
        Regex OS = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        Regex device = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string device_info = string.Empty;
        if (OS.IsMatch(userAgent))
        {
            device_info = OS.Match(userAgent).Groups[0].Value;
        }
        if (device.IsMatch(userAgent.Substring(0, 4)))
        {
            device_info += device.Match(userAgent).Groups[0].Value;
        }

        if (device_info.Contains("iPhone"))
            device_info = "Mobile";
        else if (device_info.Contains("Mobile"))
            device_info = "Mobile";
        else if (string.IsNullOrEmpty(device_info))
            device_info = "Desktop";
        return device_info;
    }

    public static String Generate_ID(int Length)
    {
        string id = "";

        SqlDataAdapter ad = new SqlDataAdapter("select RIGHT(CAST(CAST(NEWID() AS VARBINARY(36)) AS BIGINT), " + Length + ")", CommonCode.con);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            id = dt.Rows[0][0].ToString();
        }
        return id;
    }

    public static void load_new_arrival(Repeater rp_new_arrival)
    {
        SqlCommand com = new SqlCommand("dbo_get_type_of_books", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@type", SqlDbType.VarChar).Value = "new_arrival";
        SqlDataAdapter ad = new SqlDataAdapter(com);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            rp_new_arrival.DataSource = dt;
            rp_new_arrival.DataBind();
        }
    }

    public static string TimesAgo(DateTime Time)
    {
        String returnTime = string.Empty;
        DateTime now = DateTime.UtcNow.AddMinutes(330);
        TimeSpan result = now - Time;
        int[] ts = { (int)result.TotalDays, (int)result.TotalHours, (int)result.TotalMinutes, (int)result.TotalSeconds };
        if (ts[3] > 0 && ts[3] <= 59)
        {
            //now
            returnTime = "Now";
        }
        else if (ts[2] > 0 && ts[2] <= 59)
        {
            returnTime = ts[2] + " mins ago";
            if (ts[2] == 1)
                returnTime = "1 min ago";
        }
        else if (ts[1] > 0 && ts[1] <= 24)
        {
            returnTime = ts[1] + " hours ago";
            if (ts[1] == 1)
                returnTime = "1 hour ago";
        }
        else if (ts[0] >= 1)
        {
            returnTime = ts[0] + " days ago";
            if (ts[0] == 1)
                returnTime = "1 day ago";
        }
        return returnTime;
    }

    public static string GetIP()
    {

        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily.ToString() == "InterNetwork")
            {
                localIP = ip.ToString();
            }
        }
        return localIP;
    }

    public static string getExternalIp()
    {
        try
        {
            string externalIp = "";
            externalIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (externalIp == "" || externalIp == null)
                externalIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return externalIp;
        }
        catch { return null; }
    }

    public static string getIP_DetailsFromXML(string node)
    {
        String data = string.Empty;
        String IP = getExternalIp();
        String URLString = "http://ip-api.com/xml/" + IP;
        XmlTextReader reader = new XmlTextReader(URLString);
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    if (reader.Name == node)
                        data = reader.ReadString();
                    break;
            }
        }
        return data;
    }

    /// <summary>
    /// Show up a Bootstrap Dismissible Alert Message.
    /// </summary>
    /// <param name="cssclass">Can be success, danger, info, primary, warning.</param>
    /// <param name="Title">Alert Title.</param>
    /// <param name="Content">Alert content.</param>
    /// <param name="ltr">Literal Control to which script will bind.</param>
    public static void show_alert(String cssclass, String Title, String Content, Literal ltr)
    {
        string icon = "<i class='fa fa-spin fa-gear'></i>";
        switch (cssclass)
        {
            case "success":
                icon = "<i class='fa fa-check-o'></i>";
                break;
            case "danger":
                icon = "<i class='fa fa-times'></i>";
                break;
            case "info":
                icon = "<i class='fa fa-info-circle'></i>";
                break;
            case "warning":
                icon = "<i class='fa fa-warning'></i>";
                break;
        }
        String ihtml = "<div class='alert alert-" + cssclass + "' >";
        ihtml += "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a> ";
        ihtml += "<strong>" + icon + "&nbsp;";
        ihtml += Title + "</strong><br>";
        ihtml += "<span>" + Content + "</span>";
        ihtml += "</div>";
        ltr.Text = ihtml;
    }

    /// <summary>
    /// Show up a Bootstrap Alert Message which can be Dismissble (Option).
    /// </summary>
    /// <param name="cssclass">Can be success, danger, info, primary, warning.</param>
    /// <param name="Title">Alert Title.</param>
    /// <param name="Content">Alert content.</param>
    /// <param name="setClose">Make alert dismissible or not.</param>
    /// <param name="ltr">Literal Control to which script will bind.</param>
    public static void show_alert(String cssclass, String Title, String Content, Boolean setClose, Literal ltr)
    {
        string icon = "<i class='fa fa-spin fa-gear'></i>";
        switch (cssclass)
        {
            case "success":
                icon = "<i class='fa fa-check-o'></i>";
                break;
            case "danger":
                icon = "<i class='fa fa-times'></i>";
                break;
            case "info":
                icon = "<i class='fa fa-info-circle'></i>";
                break;
            case "warning":
                icon = "<i class='fa fa-warning'></i>";
                break;
        }
        String ihtml = "<div class='alert alert-" + cssclass + "' >";
        if (setClose)
            ihtml += "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a> ";
        ihtml += "<strong>" + icon + "&nbsp;";
        ihtml += Title + "</strong><br>";
        ihtml += "<span>" + Content + "</span>";
        ihtml += "</div>";
        ltr.Text = ihtml;
    }

    /// <summary>
    /// Show up a Bootstrap Dismissible Alert Message.
    /// </summary>
    /// <param name="cssclass">Can be success, danger, info, primary, warning.</param>
    /// <param name="Title">Alert Title.</param>
    /// <param name="Content">Alert content.</param>
    /// <param name="ph">PlaceHolder Control to which new alert message is bind.</param>
    public static void show_alert(String cssclass, String Title, String Content, PlaceHolder ph)
    {
        string icon = "<i class='fa fa-spin fa-gear'></i>";
        switch (cssclass)
        {
            case "success":
                icon = "<i class='fa fa-check'></i>";
                break;
            case "danger":
                icon = "<i class='fa fa-times'></i>";
                break;
            case "info":
                icon = "<i class='fa fa-info-circle'></i>";
                break;
            case "warning":
                icon = "<i class='fa fa-warning'></i>";
                break;
        }
        String ihtml = "<div class='alert alert-" + cssclass + "' >";
        ihtml += "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a> ";
        ihtml += (Convert.ToString("<strong>") + icon) + "&nbsp;";
        ihtml += Title + "</strong><br>";
        ihtml += "<span>" + Content + "</span>";
        ihtml += "</div>";
        Literal ltr = new Literal();
        ltr.Text = ihtml;
        ph.Controls.Add(ltr);
    }

    /// <summary>
    /// Show up a Bootstrap Alert Message which can be Dismissble (Option).
    /// </summary>
    /// <param name="cssclass">Can be success, danger, info, primary, warning.</param>
    /// <param name="Title">Alert Title.</param>
    /// <param name="Content">Alert content.</param>
    /// <param name="setClose">Make alert dismissible or not.</param>
    /// <param name="ph">PlaceHolder Control to which new alert message is bind.</param>
    public static void show_alert(String cssclass, String Title, String Content, Boolean setClose, PlaceHolder ph)
    {
        string icon = "<i class='fa fa-spin fa-gear'></i>";
        switch (cssclass)
        {
            case "success":
                icon = "<i class='fa fa-check-o'></i>";
                break;
            case "danger":
                icon = "<i class='fa fa-times'></i>";
                break;
            case "info":
                icon = "<i class='fa fa-info-circle'></i>";
                break;
            case "warning":
                icon = "<i class='fa fa-warning'></i>";
                break;
        }
        String ihtml = "<div class='alert alert-" + cssclass + "' >";
        if (setClose)
        {
            ihtml += "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a> ";
        }
        ihtml += (Convert.ToString("<strong>") + icon) + "&nbsp;";
        ihtml += Title + "</strong><br>";
        ihtml += "<span>" + Content + "</span>";
        ihtml += "</div>";
        Literal ltr = new Literal();
        ltr.Text = ihtml;
        ph.Controls.Add(ltr);
    }

/// <summary>
    /// Show up a Bootstrap Alert Message with auto-redirection after certain time.
    /// </summary>
    /// <param name="cssclass">Can be success, danger, info, primary, warning.</param>
    /// <param name="Title">Alert Title.</param>
    /// <param name="Content">Alert content.</param>
    /// <param name="seconds">Seconds upto which alert will be shown</param>
    /// <param name="url">Path where to redirect</param>
    /// <param name="ph">PlaceHolder Control to which new alert message is bind.</param>
    public static void show_alert(String cssclass, String Title, String Content, string seconds, string url, PlaceHolder ph)
    {
        string icon = "<i class='fa fa-spin fa-gear'></i>";
        switch (cssclass)
        {
            case "success":
                icon = "<i class='fa fa-check-o'></i>";
                break;
            case "danger":
                icon = "<i class='fa fa-times'></i>";
                break;
            case "info":
                icon = "<i class='fa fa-info-circle'></i>";
                break;
            case "warning":
                icon = "<i class='fa fa-warning'></i>";
                break;
        }


        string script = "";
        script += "<script>" + Environment.NewLine;
        script += "    var count = " + seconds + ";" + Environment.NewLine;
        script += "    setInterval(function(){" + Environment.NewLine;
        script += "    count--;" + Environment.NewLine;
        script += "    if (count > 0) {" + Environment.NewLine;
        script += "    document.getElementById('countDown').innerHTML = count;" + Environment.NewLine;
        script += "}" + Environment.NewLine;
        script += "    if (count == 1) {" + Environment.NewLine;
        script += "       window.location = '" + url + "'; " + Environment.NewLine;
        script += "    }" + Environment.NewLine;
        script += "},1000);" + Environment.NewLine;
        script += "</script>" + Environment.NewLine;
        script += "Page will redirect in ";
        script += "<span id='countDown'>" + seconds + ".</span> ";
        script += "seconds.&nbsp;<i class='fa fa-gear fa-spin fa-2x'></i>";
        String ihtml = "<div class='alert alert-" + cssclass + "' >";
        ihtml += (Convert.ToString("<strong>") + icon) + "&nbsp;";
        ihtml += Title + "</strong><br>";
        ihtml += "<span>" + Content + "</span><br>";
        ihtml += script;
        ihtml += "</div>";
        Literal ltr = new Literal();
        ltr.Text = ihtml;
        ph.Controls.Add(ltr);
    }

    /// <summary>
    /// Displays a Growl-Notification
    /// </summary>
    /// <param name="cssclass">Can be success, error, info, warning.</param>
    /// <param name="Title">Alert Title.</param>
    /// <param name="Content">Alert content.</param>
    /// <param name="setOption">Whether to set options or not.</param>
    /// <param name="Options">Pass options to display (like ShowProgress).</param>
    /// <param name="setOnClick">Whether to set a callback function.</param>
    /// <param name="onClick">Callback Function.</param>
    /// <param name="ltr">Literal Control to which script will bind.</param>
    public static void show_toastr(String cssclass, String Title, String Content,
        Boolean setOption, String Options,
        Boolean setOnClick, String onClick,
        Literal ltr)
    {
        String ihtml = "<script type='text/javascript'> $(function(){ ";
        if (setOption)
            ihtml += "toastr.options = {" + Options + "}; ";
        if (setOnClick)
            ihtml += "toastr.options.onclick = function () { " + onClick + " };";
        ihtml += "toastr." + cssclass.ToLower() + "(";
        ihtml += "'" + Content.Replace("'", "") + "',";
        ihtml += "'" + Title.Replace("'", "") + "'";
        ihtml += ");";
        ihtml += "});</script>";

        ltr.Text = ihtml; 
    }
    public static string show_toastr_forAjax(String cssclass, String Title, String Content, Boolean setOption, String Options, Boolean setOnClick, String onClick, string ltr)
    { 
        String ihtml = "<script type='text/javascript'> $(function(){ ";
        if (setOption)
            ihtml += "toastr.options = {" + Options + "}; ";
        if (setOnClick)
        ihtml += "toastr.options.onclick = function () { " + onClick + " };";
        ihtml += "toastr." + cssclass.ToLower() + "(";
        ihtml += "'" + Content.Replace("'", "") + "',";
        ihtml += "'" + Title.Replace("'", "") + "'";
        ihtml += ");";
        ihtml += "});</script>";
        //ltr.Text = ihtml;
        return ihtml;
    }

    public static Boolean check_image(FileUpload file_upload, Literal ltr_msg)
    {
        bool ok = false;
        if (file_upload.HasFile)
        {
            if (System.IO.Path.GetExtension(file_upload.FileName).ToLower() == ".png" || System.IO.Path.GetExtension(file_upload.FileName).ToLower() == ".jpg" || System.IO.Path.GetExtension(file_upload.FileName).ToLower() == ".jpeg")
            {
                if (file_upload.FileContent.Length > (1024 * 2))
                {

                }
                else
                {
                    CommonCode.show_alert("danger", "<i class='fa fa-warning'></i>&nbsp;Max file size 2MB ", "For more info on SpectraWide-Teacher Account <a>[Click Here]</a>.", ltr_msg);
                }
            }
            else
            {
                CommonCode.show_alert("danger", "<i class='fa fa-warning'></i>&nbsp;Allowed file types : .jpeg, .jpg , .png, .bmp .", "For more info on SpectraWide-Teacher Account <a>[Click Here]</a>.", ltr_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "<i class='fa fa-warning'></i>&nbsp;You have to upload your Teacher`s ID proof", "For more info on SpectraWide-Teacher Account <a>[Click Here]</a>.", ltr_msg);
        }
        return ok;
    }
    
    public static String SetPageTitle(String Page)
    {
        return Page + " - " + CommonCode.CompanyName;
    }

    public static bool CheckDate(String date, out DateTime Date)
    {
        try
        {
            DateTime dt = DateTime.Parse(date);
            Date = dt;
            return true;
        }
        catch
        {
            Date = DateTime.Now;
            return false;
        }
    }

   
    public static DataRow fetch_emailsms_info(String iCompanyID, String iBranchID, out string errmsg)
    {
        DataRow rr = null;
        SqlCommand com = new SqlCommand("Web_get_EmailConfigInfo", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                rr = dt.Rows[0];
            }
            else
                errmsg = "No Email Info found! ";
        }
        return rr;
    }

    public static string get_Shipping_Amount_Via_State_Id( string StateId, out string errmsg)
    {
        DataRow rr = null;
        try
        {
            SqlCommand com = new SqlCommand("get_ship_cost_via_state_id", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@StateId", SqlDbType.Decimal, 10).Value = StateId;
            DataTable dt = new DataTable();
            dt = CommonCode.getData(com, out errmsg);
            if (errmsg == "success")
            {
                if (dt.Rows.Count > 0)
                {

                    string s = "";
                    foreach (DataRow myRow in dt.Rows)
                            s = myRow[0].ToString();
                    return s;
                }
                else
                    errmsg = "No Email or SMS Info found! ";
            }
            return null;
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            return null;
        }
    }

    public static DataTable getImagepathlocal(DataTable dt)
    {
        if (dt != null)
        {
            dt.Columns.Add(new DataColumn("ImagePath"));
            foreach (DataRow rr in dt.Rows)
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("/resources/product/" + rr["ISBN"] + ".jpg")))
                {
                    rr["ImagePath"] = "/resources/product/" + rr["ISBN"] + ".jpg";
                }
                else if (File.Exists(HttpContext.Current.Server.MapPath("/resources/product/" + rr["ISBN"] + ".png")))
                {
                    rr["ImagePath"] = "/resources/product/" + rr["ISBN"] + ".jpg";
                }
                else if (File.Exists(HttpContext.Current.Server.MapPath("/resources/product/" + rr["ISBN"] + ".jpeg")))
                {
                    rr["ImagePath"] = "/resources/product/" + rr["ISBN"] + ".jpeg";
                }
                else if (File.Exists(HttpContext.Current.Server.MapPath("/resources/product/" + rr["ISBN"] + ".bmp")))
                {
                    rr["ImagePath"] = "/resources/product/" + rr["ISBN"] + ".bmp";
                }
                else
                {
                    rr["ImagePath"] = "/resources/no-image.png";
                }
            }
        }
        
        return dt;
    }

    public static DataTable getImagepath(DataTable dt)
    {
        dt.Columns.Add(new DataColumn("ImagePath"));
        foreach (DataRow rr in dt.Rows)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("/resources/product/" + rr["ISBN"] + ".png")))
            {
                rr["ImagePath"] = "/resources/product/" + rr["ISBN"] + ".png";
            }
            else if (File.Exists(HttpContext.Current.Server.MapPath("/resources/product/" + rr["ISBN"] + ".jpg")))
            {
                rr["ImagePath"] = "/resources/product/" + rr["ISBN"] + ".jpg";
            }
            else if (File.Exists(HttpContext.Current.Server.MapPath("/resources/product/" + rr["ISBN"] + ".jpeg")))
            {
                rr["ImagePath"] = "/resources/product/" + rr["ISBN"] + ".jpeg";
            }
            else if (File.Exists(HttpContext.Current.Server.MapPath("/resources/product/" + rr["ISBN"] + ".bmp")))
            {
                rr["ImagePath"] = "/resources/product/" + rr["ISBN"] + ".bmp";
            }
            else
            {
                rr["ImagePath"] = "/resources/product/no-image.png";
            }
        }
        return dt;
    }

    public string GetConnectioName()
    {
        string ConStr = "";
        try
        {
            System.Configuration.Configuration rootWebConfig = default(System.Configuration.Configuration);
            rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            System.Configuration.ConnectionStringSettings connString = default(System.Configuration.ConnectionStringSettings);
            if ((rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0))
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["ConStr"];
                if (!(connString.ConnectionString == null))
                {
                    ConStr = connString.ConnectionString;
                }
            }

        }
        catch (Exception ex)
        {
        }
        return ConStr;

    }

}