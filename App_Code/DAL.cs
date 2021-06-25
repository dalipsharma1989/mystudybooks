using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Net;
/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    public DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public DataSet get_admin_stats(out string errmsg)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand com = new SqlCommand("dbo_get_order_stats", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            ds = CommonCode.getDataInDataSet(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }

    public DataTable getDataByQuery(string query, out string errmsg)
    {
        DataTable dt = new DataTable();
        if (string.IsNullOrEmpty(query))
        {
            errmsg = "Query can`t be empty!";
        }
        else
        {
            SqlCommand com = new SqlCommand(query, CommonCode.con);
            dt = CommonCode.getData(com, out errmsg);
        }
        return dt;
    }

    public DataSet get_user_and_cart(string OtherCountry,  string CustID,string iCompanyID,string iBranchID, out string errmsg)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_user_and_cart", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = CustID;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            ds = CommonCode.getDataInDataSet(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }

    public DataTable admin_login(string username, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("dbo_admin_login", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@username", SqlDbType.VarChar, 800).Value = username;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }


    public void update_admin_login(string username, string currentpassword , string confirmpassword, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("dbo_Update_admin_login", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@username", SqlDbType.VarChar ,800).Value = username;
            com.Parameters.Add("@currentpassword", SqlDbType.VarChar, 800).Value = currentpassword ;
            com.Parameters.Add("@confirmpassword", SqlDbType.VarChar, 800).Value = confirmpassword ;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }




    public DataTable get_user_details(string CustID, String iCompanyId, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_Get_User_Details", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = CustID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyId;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    //dbo_get_book_details
    public DataSet get_book_details(string ProductID, out string errmsg)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand com = new SqlCommand("web_get_book_details", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = ProductID;
            com.Parameters.Add("@isSimilar_books", SqlDbType.Bit).Value = false;
            ds = CommonCode.getDataInDataSet(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }
    public DataSet get_curr_book_details(string OtherCountry, string ProductID, String iCompanyID, String iBranchID, out string errmsg)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand com = new SqlCommand("web_curr_get_book_details", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar,100).Value = OtherCountry;
            com.Parameters.Add("@ProductID", SqlDbType.VarChar,20).Value = ProductID;
            com.Parameters.Add("@isSimilar_books", SqlDbType.Bit).Value = false;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            
            ds = CommonCode.getDataInDataSet(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }

    public DataTable get_Classes(string ClassID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            if (string.IsNullOrEmpty(ClassID))
                ClassID = "0";
            SqlCommand com = new SqlCommand("dbo_get_class", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = ClassID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_currency(string CurrencyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("dbo_get_currency", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CurrencyID", SqlDbType.VarChar, 3).Value = CurrencyID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_subjects(string SubjectID, int ShowinGroup, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            if (string.IsNullOrEmpty(SubjectID))
                SubjectID = "0";
            SqlCommand com = new SqlCommand("dbo_get_subjects", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@SubjectID", SqlDbType.BigInt).Value = SubjectID;
            com.Parameters.Add("@ShowinGroup", SqlDbType.Int).Value = ShowinGroup;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_update_delete_subjects(string SubjectID, string ISBN, string SubjectName, int action, bool inProductRel, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_Subjects", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@SubjectID", SqlDbType.BigInt).Value = SubjectID;
            com.Parameters.Add("@ISBN", SqlDbType.VarChar, 20).Value = ISBN;
            com.Parameters.Add("@SubjectName", SqlDbType.VarChar, 200).Value = SubjectName;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@inProductRel", SqlDbType.Bit).Value = inProductRel;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public void insert_update_delete_Class(string ClassID, string ISBN, string ClassName, int action, bool inProductRel, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_Class", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = ClassID;
            com.Parameters.Add("@ISBN", SqlDbType.VarChar, 20).Value = ISBN;
            com.Parameters.Add("@ClassName", SqlDbType.VarChar, 200).Value = ClassName;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@inProductRel", SqlDbType.Bit).Value = inProductRel;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public void insert_update_delete_Currency(string CurrencyID, string CurrencyName, string CurrencySymbol, bool DefaultCurr, decimal Rate, string CreatedBy, int action, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("dbo_insert_update_delete_currency", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CurrencyID", SqlDbType.VarChar, 3).Value = CurrencyID;
            com.Parameters.Add("@CurrencyName", SqlDbType.VarChar, 60).Value = CurrencyName;
            com.Parameters.Add("@CurrencySymbol", SqlDbType.VarChar, 50).Value = CurrencySymbol;
            com.Parameters.Add("@DefaultCurr", SqlDbType.Bit).Value = DefaultCurr;
            com.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Rate;
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = CreatedBy;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public void insert_update_delete_shipping_address(string @AddressID, string @CustId, string @ShipAddress, string @ShipCityID, string @ShipZoneID, string @ShipPostalCode,
        string @ShipPhone, string @ShipFaxNo, string @Mobile, string @EmailID, bool @IsDefault, int @action, string @iCompanyID, out string Address_output, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_Insert_Edit_Dele_Cust_Ship_Address", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@AddressID", SqlDbType.BigInt).Value = AddressID;
            com.Parameters.Add("@CustId", SqlDbType.VarChar, 30).Value = CustId;
            com.Parameters.Add("@ShipAddress", SqlDbType.VarChar, 1000).Value = ShipAddress;
            com.Parameters.Add("@ShipCityID", SqlDbType.VarChar, 10).Value = ShipCityID;
            com.Parameters.Add("@ShipZoneID", SqlDbType.VarChar, 7).Value = ShipZoneID;
            com.Parameters.Add("@ShipPostalCode", SqlDbType.VarChar, 100).Value = ShipPostalCode;
            com.Parameters.Add("@ShipPhone", SqlDbType.VarChar, 100).Value = ShipPhone;
            com.Parameters.Add("@ShipFaxNo", SqlDbType.VarChar, 100).Value = ShipFaxNo;
            com.Parameters.Add("@Mobile", SqlDbType.VarChar, 10).Value = Mobile;
            com.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = EmailID;
            com.Parameters.Add("@IsDefault", SqlDbType.Bit).Value = IsDefault;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@Address_output", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            Address_output = com.Parameters["@Address_output"].Value.ToString();
        }
        catch (Exception ex)
        {
            Address_output = string.Empty;
            errmsg = ex.Message;
        }
    }

    public void insert_update_delete_Checkout(string CheckoutID, string CustID, string CartID, string ShipID, int PayStatus, string PayStatusRemarks, int action,
            string iCompanyID, String iBranchID, out string CheckoutID_output, out string errmsg)
    {
        try
        {
            CheckoutID_output = "";
            SqlCommand com = new SqlCommand("Web_Insert_Edit_Dele_Checkout", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CheckoutID", SqlDbType.BigInt).Value = CheckoutID;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = CustID;
            com.Parameters.Add("@CartID", SqlDbType.VarChar, 150).Value = CartID;
            com.Parameters.Add("@ShipID", SqlDbType.VarChar, 150).Value = ShipID;
            com.Parameters.Add("@PayStatus", SqlDbType.Int).Value = PayStatus;
            com.Parameters.Add("@PayStatusRemarks", SqlDbType.VarChar, 500).Value = PayStatusRemarks;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.Parameters.Add("@CheckoutID_output", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                CheckoutID_output = com.Parameters["@CheckoutID_output"].Value.ToString();
            }
        }
        catch (Exception ex)
        {
            CheckoutID_output = string.Empty;
            errmsg = ex.Message;
        }
    }

    public void Order_PaymentStatus(string OrderID, string TotalQty, string TotalAmount, string ShipCost,
               string ShipMethod, string Remark, string Status, string StatusRemark, string PayMethod, string CardType, string NameOnCard,
               string CardNum, string CardExpires, string OtherDetail, 
               out string OrderID_output, out string errmsg, String TxnID)
    {
        try
        {
            OrderID_output = "";
            SqlCommand com = new SqlCommand("dbo_insert_OrderPayment", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.Add("@CheckoutID", SqlDbType.BigInt).Value = OrderID;
            com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = OrderID;
            com.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            com.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = Remark;
            com.Parameters.Add("@StatusRemark", SqlDbType.VarChar, 500).Value = StatusRemark;
            com.Parameters.Add("@PayMethod", SqlDbType.VarChar, 25).Value = PayMethod;
            com.Parameters.Add("@CardType", SqlDbType.VarChar, 25).Value = CardType;
            com.Parameters.Add("@NameOnCard", SqlDbType.VarChar, 100).Value = NameOnCard;
            com.Parameters.Add("@CardNum", SqlDbType.VarChar, 25).Value = CardNum;
            com.Parameters.Add("@CardExpires", SqlDbType.SmallDateTime).Value = CardExpires;
            com.Parameters.Add("@OtherDetail", SqlDbType.VarChar, 5000).Value = OtherDetail;
            com.Parameters.Add("@transactionID", SqlDbType.VarChar, 200).Value = TxnID;          
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            OrderID_output = string.Empty;
            errmsg = ex.Message;
        }
    }

    public void Order_PaymentResStatus(string OrderID, string Remark, string Status, string StatusRemark, string PayMethod, string CardType, string NameOnCard, string PaymentID , string BankReferenceNo , 
        string AMount, string CardNum, string CardExpires, string OtherDetail, String iCompanyID, String iBranchID, String FinancialPeriod, String CustID,  out string errmsg)
    {
        try
        { 
            SqlCommand com = new SqlCommand("Web_insert_OrderPayment_Ecommerce", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure; 
            com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = OrderID;
            com.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            com.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = Remark;
            com.Parameters.Add("@StatusRemark", SqlDbType.VarChar, 500).Value = StatusRemark;
            com.Parameters.Add("@PayMethod", SqlDbType.VarChar, 25).Value = PayMethod;
            com.Parameters.Add("@CardType", SqlDbType.VarChar, 25).Value = CardType;
            com.Parameters.Add("@NameOnCard", SqlDbType.VarChar, 100).Value = NameOnCard;
            com.Parameters.Add("@CardNum", SqlDbType.VarChar, 25).Value = CardNum;
            com.Parameters.Add("@CardExpires", SqlDbType.VarChar, 4).Value = CardExpires;
            com.Parameters.Add("@OtherDetail", SqlDbType.VarChar, 5000).Value = OtherDetail;
            com.Parameters.Add("@transactionID", SqlDbType.BigInt).Value = OrderID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.Parameters.Add("@FinancialPeriod", SqlDbType.VarChar, 10).Value = FinancialPeriod;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = CustID;
            com.Parameters.Add("@PaymentID", SqlDbType.VarChar, 100).Value = PaymentID;
            com.Parameters.Add("@BankReferenceNo", SqlDbType.VarChar, 150).Value = BankReferenceNo;
            com.Parameters.Add("@AMount", SqlDbType.Decimal, 10).Value = AMount;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        { 
            errmsg = ex.Message;
        }
    }


     
    public void insert_order(string CheckoutID, string TotalQty, string TotalAmount, string ShipCost, string ShipMethod, string Remark, string Status, string StatusRemark, 
        string PayMethod, string CardType, string NameOnCard, string CardNum, string CardExpires, string tid, string OtherCountry, Boolean couriour, String txnsid,
        String iCompanyId, String iBranchID, String FinancialPeriod,String CustID, String AddresID, out string OrderID_output, out string errmsg)
    {
        try
        {
            OrderID_output = "";
            SqlCommand com = new SqlCommand("Web_insert_order_Ecommerce", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CheckoutID", SqlDbType.BigInt).Value = CheckoutID;
            com.Parameters.Add("@TotalQty", SqlDbType.Int).Value = TotalQty;
            com.Parameters.Add("@TotalAmount", SqlDbType.Float).Value = TotalAmount;
            com.Parameters.Add("@ShipCost", SqlDbType.Float).Value = ShipCost;
            com.Parameters.Add("@ShipMethod", SqlDbType.VarChar, 20).Value = ShipMethod;
            com.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = Remark;
            com.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            com.Parameters.Add("@StatusRemark", SqlDbType.VarChar, 500).Value = StatusRemark;
            //com.Parameters.Add("@PayMethod", SqlDbType.VarChar, 25).Value = PayMethod;
            //com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID(); not required
            com.Parameters.Add("@transactionID", SqlDbType.VarChar, 200).Value = txnsid;
            com.Parameters.Add("@tid", SqlDbType.BigInt).Value = tid;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
            com.Parameters.Add("@Courier", SqlDbType.Bit).Value = couriour;
            com.Parameters.Add("@OrderID_output", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyId;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.Parameters.Add("@FinancialPeriod", SqlDbType.VarChar, 10).Value = FinancialPeriod;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = CustID;
            com.Parameters.Add("@ShipToAddressID", SqlDbType.VarChar, 30).Value = AddresID;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
                OrderID_output = com.Parameters["@OrderID_output"].Value.ToString();
        }
        catch (Exception ex)
        {
            OrderID_output = string.Empty;
            errmsg = ex.Message;
        }
    }


    //public void insert_order(string CheckoutID, string TotalQty, string TotalAmount, string ShipCost,
    //           string ShipMethod, string Remark, string Status, string StatusRemark, string PayMethod, string CardType, string NameOnCard,
    //           string CardNum, string CardExpires, string tid, string OtherCountry, Boolean couriour, out string OrderID_output, out string errmsg, String txnsid)
    //{
    //    try
    //    {
    //        OrderID_output = "";
    //        SqlCommand com = new SqlCommand("dbo_insert_order", CommonCode.con);
    //        com.CommandType = CommandType.StoredProcedure;
    //        com.Parameters.Add("@CheckoutID", SqlDbType.BigInt).Value = CheckoutID;
    //        com.Parameters.Add("@TotalQty", SqlDbType.Int).Value = TotalQty;
    //        com.Parameters.Add("@TotalAmount", SqlDbType.Float).Value = TotalAmount;
    //        com.Parameters.Add("@ShipCost", SqlDbType.Float).Value = ShipCost;
    //        com.Parameters.Add("@ShipMethod", SqlDbType.VarChar, 20).Value = ShipMethod;
    //        com.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = Remark;
    //        com.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
    //        com.Parameters.Add("@StatusRemark", SqlDbType.VarChar, 500).Value = StatusRemark;
    //        com.Parameters.Add("@PayMethod", SqlDbType.VarChar, 25).Value = PayMethod;
    //        com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
    //        com.Parameters.Add("@transactionID", SqlDbType.VarChar, 200).Value = txnsid;
    //        com.Parameters.Add("@tid", SqlDbType.BigInt).Value = tid;
    //        com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
    //        com.Parameters.Add("@Courier", SqlDbType.Bit).Value = couriour;
    //        com.Parameters.Add("@OrderID_output", SqlDbType.BigInt).Direction = ParameterDirection.Output;
    //        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
    //        if (errmsg == "success")
    //            OrderID_output = com.Parameters["@OrderID_output"].Value.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        OrderID_output = string.Empty;
    //        errmsg = ex.Message;
    //    }
    //}


    public void insert_update_delete_menu(string MenuID, string Name, string Type, string HeaderContent, string MainContent, int SortOrder, int action, string iCompanyID, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_insert_edit_dele_menu", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@MenuID", SqlDbType.BigInt).Value = MenuID;
            com.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = Name;
            com.Parameters.Add("@Type", SqlDbType.VarChar, 20).Value = Type;
            com.Parameters.Add("@HeaderContent", SqlDbType.NVarChar).Value = HeaderContent;
            com.Parameters.Add("@MainContent", SqlDbType.NVarChar).Value = MainContent;
            com.Parameters.Add("@SortOrder", SqlDbType.Int).Value = SortOrder;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataSet get_Wishlist_details(string OrderID, string CustID, bool isAll, String iCompanyID,String iBranchID, out string errmsg)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_my_wishlist", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = 0;
            com.Parameters.Add("@isAll", SqlDbType.Bit).Value = false;
            com.Parameters.Add("@OrderID", SqlDbType.VarChar, 30).Value = OrderID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            ds = CommonCode.getDataInDataSet(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;

    }

    public DataSet get_Cart_details(string OrderID, string CustID, bool isAll, string iCompanyID, string iBranchID, out string errmsg)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_my_CartDetails", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = 0;
            com.Parameters.Add("@isAll", SqlDbType.Bit).Value = false;
            com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = OrderID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            ds = CommonCode.getDataInDataSet(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;

    }

    public DataSet get_Order_details(string OrderID, string CustID, bool isAll, string iCompanyID, string iBranchID, out string errmsg) 
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand com = new SqlCommand("Web_dbo_get_my_orders_Ecommerce", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = CustID;
            com.Parameters.Add("@isAll", SqlDbType.Bit).Value = false;
            com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = OrderID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            ds = CommonCode.getDataInDataSet(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;

    }

    public DataTable get_menu_items(string MenuID, string iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_menu", CommonCode.con);
            com.Parameters.Add("@MenuID", SqlDbType.BigInt).Value = MenuID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_book_details(string ProductID, bool isSimilar_books, string iCompanyID, string iBranchID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("web_get_book_details", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = ProductID;
            com.Parameters.Add("@isSimilar_books", SqlDbType.Bit).Value = isSimilar_books;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_update_delete_shipping_amount(int RowID, float DefaultShippingAmount, float MinAmountForFreeShipping, string FreeShippingMessage, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("dbo_insert_update_delete_shipping_amount", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@RowID", SqlDbType.BigInt).Value = RowID;
            com.Parameters.Add("@DefaultShippingAmount", SqlDbType.Decimal).Value = DefaultShippingAmount;
            com.Parameters.Add("@MinAmountForFreeShipping", SqlDbType.Decimal).Value = MinAmountForFreeShipping;
            com.Parameters.Add("@FreeShippingMessage", SqlDbType.NVarChar).Value = FreeShippingMessage;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_shipping_amount_details(out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("dbo_get_shipping_amount_details", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_homepage_data(String iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_homepage_data", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@iCompanyID", iCompanyID);
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_update_delete_homepage(string Sidebar, string PromotionSlider, string IstBanner, string IIndBanner, string IIIrdBanner, string IVthBanner, 
                                              string VthBanner, string VIthBanner, string VIIBanner, string VIIIBanner, string IstBookSlider, string IIndBookSlider, string IIIrdBookSlider, string IVthBookSlider, 
                                              string VthBookSlider, string VIthBookSlider, string VIIBookSlider, string VIIIBookSlider, String iCompanyID, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("web_insert_update_delete_homepage", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@Sidebar", SqlDbType.NVarChar).Value = Sidebar;
            com.Parameters.Add("@PromotionSlider", SqlDbType.NVarChar).Value = PromotionSlider;
            com.Parameters.Add("@IstBanner", SqlDbType.NVarChar).Value = IstBanner;
            com.Parameters.Add("@IIndBanner", SqlDbType.NVarChar).Value = IIndBanner;
            com.Parameters.Add("@IIIrdBanner", SqlDbType.NVarChar).Value = IIIrdBanner;
            com.Parameters.Add("@IVthBanner", SqlDbType.NVarChar).Value = IVthBanner;
            com.Parameters.Add("@VthBanner", SqlDbType.NVarChar).Value = VthBanner;
            com.Parameters.Add("@VIthBanner", SqlDbType.NVarChar).Value = VIthBanner;
            com.Parameters.Add("@VIIBanner", SqlDbType.NVarChar).Value = VIIBanner;
            com.Parameters.Add("@VIIIBanner", SqlDbType.NVarChar).Value = VIIIBanner;
            com.Parameters.Add("@IstBookSlider", SqlDbType.NVarChar).Value = IstBookSlider;
            com.Parameters.Add("@IIndBookSlider", SqlDbType.NVarChar).Value = IIndBookSlider;
            com.Parameters.Add("@IIIrdBookSlider", SqlDbType.NVarChar).Value = IIIrdBookSlider;
            com.Parameters.Add("@IVthBookSlider", SqlDbType.NVarChar).Value = IVthBookSlider;
            com.Parameters.Add("@VthBookSlider", SqlDbType.NVarChar).Value = VthBookSlider;
            com.Parameters.Add("@VIthBookSlider", SqlDbType.NVarChar).Value = VIthBookSlider;
            com.Parameters.Add("@VIIBookSlider", SqlDbType.NVarChar).Value = VIIBookSlider;
            com.Parameters.Add("@VIIIBookSlider", SqlDbType.NVarChar).Value = VIIIBookSlider;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }


    //public void insert_update_delete_homepage(string Sidebar, string PromotionSlider, string IstBanner, string IIndBanner, string IIIrdBanner, string IVthBanner, string VthBanner, string VIthBanner, string IstBookSlider, string IIndBookSlider, string IIIrdBookSlider, out string errmsg)
    //{
    //    try
    //    {
    //        SqlCommand com = new SqlCommand("dbo_insert_update_delete_homepage", CommonCode.con);
    //        com.CommandType = CommandType.StoredProcedure;
    //        com.Parameters.Add("@Sidebar", SqlDbType.NVarChar).Value = Sidebar;
    //        com.Parameters.Add("@PromotionSlider", SqlDbType.NVarChar).Value = PromotionSlider;
    //        com.Parameters.Add("@IstBanner", SqlDbType.NVarChar).Value = IstBanner;
    //        com.Parameters.Add("@IIndBanner", SqlDbType.NVarChar).Value = IIndBanner;
    //        com.Parameters.Add("@IIIrdBanner", SqlDbType.NVarChar).Value = IIIrdBanner;
    //        com.Parameters.Add("@IVthBanner", SqlDbType.NVarChar).Value = IVthBanner;
    //        com.Parameters.Add("@VthBanner", SqlDbType.NVarChar).Value = VthBanner;
    //        com.Parameters.Add("@VIthBanner", SqlDbType.NVarChar).Value = VIthBanner;
    //        com.Parameters.Add("@IstBookSlider", SqlDbType.NVarChar).Value = IstBookSlider;
    //        com.Parameters.Add("@IIndBookSlider", SqlDbType.NVarChar).Value = IIndBookSlider;
    //        com.Parameters.Add("@IIIrdBookSlider", SqlDbType.NVarChar).Value = IIIrdBookSlider;
    //        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
    //    }
    //    catch (Exception ex)
    //    {
    //        errmsg = ex.Message;
    //    }
    //}




    //public void insert_update_delete_homepage(string Sidebar, string PromotionSlider, string IstBanner, string IIndBanner, string IIIrdBanner, string IVthBanner, string VthBanner, string VIthBanner, string IstBookSlider, string IIndBookSlider, string IIIrdBookSlider, out string errmsg)
    //{
    //    try
    //    {
    //        SqlCommand com = new SqlCommand("dbo_insert_update_delete_homepage", CommonCode.con);
    //        com.CommandType = CommandType.StoredProcedure;
    //        com.Parameters.Add("@Sidebar", SqlDbType.NVarChar).Value = Sidebar;
    //        com.Parameters.Add("@PromotionSlider", SqlDbType.NVarChar).Value = PromotionSlider;
    //        com.Parameters.Add("@IstBanner", SqlDbType.NVarChar).Value = IstBanner;
    //        com.Parameters.Add("@IIndBanner", SqlDbType.NVarChar).Value = IIndBanner;
    //        com.Parameters.Add("@IIIrdBanner", SqlDbType.NVarChar).Value = IIIrdBanner;
    //        com.Parameters.Add("@IVthBanner", SqlDbType.NVarChar).Value = IVthBanner;
    //        com.Parameters.Add("@VthBanner", SqlDbType.NVarChar).Value = VthBanner;
    //        com.Parameters.Add("@VIthBanner", SqlDbType.NVarChar).Value = VIthBanner;
    //        com.Parameters.Add("@IstBookSlider", SqlDbType.NVarChar).Value = IstBookSlider;
    //        com.Parameters.Add("@IIndBookSlider", SqlDbType.NVarChar).Value = IIndBookSlider;
    //        com.Parameters.Add("@IIIrdBookSlider", SqlDbType.NVarChar).Value = IIIrdBookSlider;
    //        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
    //    }
    //    catch (Exception ex)
    //    {
    //        errmsg = ex.Message;
    //    }
    //}

    public DataTable get_sliders_books(string OtherCountry, string Slider_Csv,string icompanyID,string iBranchId , out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            string sql = "";
            //sql += "	select distinct CEILING(MP.SalePrice) FormattedSalePrice, MP.*, ";
            //sql += "	MPI.ProductImage, ";
            //sql += "	MPSR.DiscountPrice,MPSR.DiscountPercent,case when MPSR.DiscountPercent < 5 then 'Save '+MP.SaleCurrency+' '+cast(cast(CEILING(coalesce((SalePrice-DiscountPrice) ,0)) as numeric(36,2)) as varchar) else cast(ceiling(MPSR.DiscountPercent) as varchar)+'% off' end DiscountStatus ";
            //sql += "	from MasterProduct MP ";
            //sql += "	left join MasterProductInfo MPI on MP.ProductId=MPI.ProductId ";
            //sql += "	left join MasterProductSpecialRate MPSR on MP.ProductID=MPSR.ProductId ";
            //sql += "	where MP.ProductID in (" + Slider_Csv + ")";
            sql = "web_get_sliders_books";
            SqlCommand com = new SqlCommand(sql, CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@query", SqlDbType.VarChar, 5000).Value = Slider_Csv;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = icompanyID;
            com.Parameters.Add("@iBranchId", SqlDbType.Int).Value = iBranchId;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }
    public DataTable get_sliders_books_withName(string OtherCountry, string Slider_Csv, string icompanyID, string iBranchId, string SliderName, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            string sql = ""; 
            sql = "web_get_sliders_books";
            SqlCommand com = new SqlCommand(sql, CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@query", SqlDbType.VarChar, 5000).Value = Slider_Csv;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = icompanyID;
            com.Parameters.Add("@iBranchId", SqlDbType.Int).Value = iBranchId;
            com.Parameters.Add("@SliderName", SqlDbType.VarChar,500).Value = SliderName;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }
    public DataTable get_topics(string TopicID, int action, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web__Get_Topics", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@TopicID", SqlDbType.BigInt).Value = TopicID;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_update_dele_topics(string TopicID, string Name, string TopicContent, bool isParent, string ChildOf, int action, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_dbo_insert_update_dele_topics", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@TopicID", SqlDbType.BigInt).Value = TopicID;
            com.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = Name;
            com.Parameters.Add("@TopicContent", SqlDbType.NVarChar).Value = TopicContent;
            com.Parameters.Add("@isParent", SqlDbType.Bit).Value = isParent;
            com.Parameters.Add("@ChildOf", SqlDbType.BigInt).Value = ChildOf;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_products_for_menu_grid(string product_selector, string product_csv, out string errmsg)
    {
        errmsg = "";
        try
        {
            string column_name = "";
            if (product_selector == "ID")
                column_name = "ProductID";
            else
                column_name = "ISBN";

            string sql = "";
            sql += "	select distinct CEILING(MP.SalePrice) FormattedSalePrice, MP.*, ";
            sql += "	MPI.ProductImage, ";
            sql += "	MPSR.DiscountPrice,MPSR.DiscountPercent,case when MPSR.DiscountPercent < 5 then 'Save '+MP.SaleCurrency+' '+cast(cast(CEILING(coalesce((SalePrice-DiscountPrice) ,0)) as numeric(36,2)) as varchar) else cast(ceiling(MPSR.DiscountPercent) as varchar)+'% off' end DiscountStatus ";
            sql += "	from MasterProduct MP ";
            sql += "	left join MasterProductInfo MPI on MP.ProductId=MPI.ProductId ";
            sql += "	left join MasterProductSpecialRate MPSR on MP.ProductID=MPSR.ProductId ";
            sql += " where MP." + column_name + " in (" + product_csv + ")";
            SqlCommand com = new SqlCommand(sql, CommonCode.con);
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }

        return null;
    }


    public DataTable get_cart(string OtherCountry, string CustID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("dbo_get_cart", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = CustID;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_curr_cart(string OtherCountry, string CustID,string iCompanyID,string iBranchID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_curr_get_cart", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar,100).Value = OtherCountry;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = CustID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_update_delete_NotifyMe_Users(string RowID, string EmailID, string Phone, string NotifyName, string ProductID, int action, string iCompanyID , out string errmsg)
    {
        try 
        {
            SqlCommand com = new SqlCommand("web_insert_update_delete_NotifyMe_Users", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@RowID", SqlDbType.BigInt).Value = RowID;
            com.Parameters.Add("@EmailID", SqlDbType.VarChar, 500).Value = EmailID;
            com.Parameters.Add("@Phone", SqlDbType.VarChar, 50).Value = Phone;
            com.Parameters.Add("@NotifyName", SqlDbType.VarChar, 250).Value = NotifyName;
            com.Parameters.Add("@ProductID", SqlDbType.VarChar, 20).Value = ProductID;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_NotifyMe_Users(string EmailID , string Phone , string ProductID , string iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_NotifyMe_Users", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@EmailID", SqlDbType.VarChar, 500).Value = EmailID;
            com.Parameters.Add("@Phone", SqlDbType.VarChar, 50).Value = Phone;
            com.Parameters.Add("@ProductID", SqlDbType.VarChar,20).Value = ProductID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_NewsLetterEmail(string EmailID, string iCompanyID, string iBranchID, out string errmsg) 
    {
        try
        {
            SqlCommand com = new SqlCommand("web_insert_in_newsletter_emails", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@EmailID", SqlDbType.VarChar, 1000).Value = EmailID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_Emails(string iCompanyID, string iBranchID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
         
            SqlCommand com = new SqlCommand("Web_get_Newsletter_Emails", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }


    /*---------------------------- Original --------------------------------*/
    /*
    public void insert_update_delete_CityWiseShippingCharges(string CityShipAmountID,
     string CityID, float FromWeight, float ToWeight, float ShippingAmount, int action, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_delete_CityWiseShipping", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CityShipAmountID", SqlDbType.BigInt).Value = CityShipAmountID;
            com.Parameters.Add("@CityID", SqlDbType.VarChar, 50).Value = CityID;
            com.Parameters.Add("@FromWeight", SqlDbType.Decimal).Value = FromWeight;
            com.Parameters.Add("@ToWeight", SqlDbType.Decimal).Value = ToWeight;
            com.Parameters.Add("@ShippingAmount", SqlDbType.Decimal).Value = ShippingAmount;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }
    */

    //public void insert_update_delete_StateWiseShippingCharges(string StateShipID, string StateID, float ShippingAmount, int action, out string errmsg)
    //{
    //    try
    //    {
    //        SqlCommand com = new SqlCommand("dbo_insert_edit_delete_StateWiseShipping", CommonCode.con);
    //        com.CommandType = CommandType.StoredProcedure;
    //        com.Parameters.Add("@StateShipID", SqlDbType.BigInt).Value = StateShipID;
    //        com.Parameters.Add("@StateID", SqlDbType.VarChar, 50).Value = StateID;
    //        com.Parameters.Add("@ShippingAmount", SqlDbType.Decimal).Value = ShippingAmount;
    //        com.Parameters.Add("@action", SqlDbType.Int).Value = action;
    //        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
    //    }
    //    catch (Exception ex)
    //    {
    //        errmsg = ex.Message;
    //    }
    //}

    // -------------- Original ---------------------- //
    /*
    public DataTable get_CityWiseShippingCharges(string CityID, string CityShipAmountID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("dbo_get_CityWiseShippingCharges", CommonCode.con);
            com.Parameters.Add("@CityID", SqlDbType.BigInt).Value = CityID;
            com.Parameters.Add("@CityShipAmountID", SqlDbType.BigInt).Value = CityShipAmountID;
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    */
    // ---------------- Updated : 30 / 05 / 2019 ---------------- //

    public void insert_update_delete_StateWiseShippingCharges(string StateShipID, string StateID, float ShippingAmount, float ShippingAmount2, int action, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_delete_StateWiseShipping", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@StateShipID", SqlDbType.BigInt).Value = StateShipID;
            com.Parameters.Add("@StateID", SqlDbType.VarChar, 50).Value = StateID;
            com.Parameters.Add("@ShippingAmount", SqlDbType.Decimal).Value = ShippingAmount;
            com.Parameters.Add("@ShippingAmount2", SqlDbType.Decimal).Value = ShippingAmount2;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }



    public DataTable get_StateWiseShippingCharges(string StateID, string StateShipID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("dbo_get_StateWiseShippingCharges", CommonCode.con);
            com.Parameters.Add("@StateID", SqlDbType.BigInt).Value = StateID;
            com.Parameters.Add("@StateShipID", SqlDbType.BigInt).Value = StateShipID;
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    /* *** ***** Update on 01/06/2019 ***** *** */
    /* For Conversion rate page in admin section */

    public DataTable get_ConversionRate(out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("dbo_GETConversionRate", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_ConversionRate(string StateId, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("dbo_Insert_ConversionRate", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ConversionRate", SqlDbType.BigInt).Value = StateId;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }


    public DataTable get_Notifyme_book(string ProductID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("dbo_get_NotifyMe_for_sendEmail", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = ProductID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }


    //public void insert_update_delete_NotifyMe_Users(string RowID, string EmailID, string Phone, string NotifyName, string ProductID, int action, out string errmsg)
    //{
    //    try
    //    {
    //        SqlCommand com = new SqlCommand("web_insert_update_delete_NotifyMe_Users", CommonCode.con);
    //        com.CommandType = CommandType.StoredProcedure;
    //        com.Parameters.Add("@RowID", SqlDbType.BigInt).Value = RowID;
    //        com.Parameters.Add("@EmailID", SqlDbType.VarChar, 500).Value = EmailID;
    //        com.Parameters.Add("@Phone", SqlDbType.VarChar, 50).Value = Phone;
    //        com.Parameters.Add("@NotifyName", SqlDbType.VarChar, 250).Value = NotifyName;
    //        com.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = ProductID;
    //        com.Parameters.Add("@action", SqlDbType.Int).Value = action;
    //        com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = action;
    //        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
    //    }
    //    catch (Exception ex)
    //    {
    //        errmsg = ex.Message;
    //    }
    //}

    public static string ConvertTo10(string isbn13)
    {
        string isbn10 = string.Empty;
        long temp;
        // *************************************************  // Validation of isbn13 code can be done by        *  // using this snippet found here:                  * // http://www.dreamincode.net/code/snippet5385.htm *  // *************************************************
        if (!string.IsNullOrEmpty(isbn13) && isbn13.Length == 13 && Int64.TryParse(isbn13, out temp))
        {
            isbn10 = isbn13.Substring(3, 9);
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += Int32.Parse(isbn10[i].ToString()) * (i + 1);
            int result = sum % 11;
            char checkDigit = (result > 9) ? 'X' : result.ToString()[0];
            isbn10 += checkDigit;
        }
        return isbn10;
    }


    public DataTable get_ImagesfromAPI(DataTable dt)
    {
        string ISBNNUMBER10 = "";
        dt = CommonCode.getImagepathlocal(dt);
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (dt.Rows[i]["ImagePath"].ToString() == "/resources/no-image.jpg")
            {
                bool exist = false;
                try
                {
                    // http://images.amazon.com/images/P/1900151855.01._SCLZZZZZZZ_.png    
                    ISBNNUMBER10 = ConvertTo10(dt.Rows[i]["ISBN"].ToString());
                    WebRequest req = WebRequest.Create("http://images.amazon.com/images/P/" + ISBNNUMBER10 + ".01._SCLZZZZZZZ_.png?default=false");
                    WebResponse res = req.GetResponse();
                    if (res.ContentLength == 43)
                    {
                        exist = false;
                    }
                    else
                    {
                        exist = true;
                    }

                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("remote name could not be resolved"))
                    {
                        Console.WriteLine("Url is Invalid");
                    }
                }

                if (exist == true)
                {
                    dt.Rows[i]["ImagePath"] = "http://images.amazon.com/images/P/" + ISBNNUMBER10 + ".01._SCLZZZZZZZ_.png?default=false";//"http://covers.openlibrary.org/b/isbn/" + ISBNNUMBER10 + "-L.jpg?default=false";
                    dt.AcceptChanges();
                }
            }
        }
        return dt;
    }

    public DataTable ICompanyID_BranchID(out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("web_get_Web_CompanyInfo", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;

            dt = CommonCode.getData(com, out errmsg);
            if (dt.Rows.Count > 0)
            {


            }
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable Validate_Login(string UserName, string UserPassword, out string errmsg)
    {
        DataTable dtTbl = new DataTable();
        
        try
        {
            SqlCommand loadCommand = new SqlCommand("Web_sp_login", CommonCode.con);
            loadCommand.CommandType = CommandType.StoredProcedure;            
            loadCommand.Parameters.AddWithValue("@Action", "AuthenticateUser");
            loadCommand.Parameters.AddWithValue("@UserName", UserName);
            loadCommand.Parameters.AddWithValue("@UserPassword", UserPassword);
            dtTbl = CommonCode.getData(loadCommand, out errmsg);            
        }
        catch (Exception e)
        {           
            errmsg = e.Message;
            dtTbl = null;
        } 

        return dtTbl;
    }

    public DataSet get_admin_stats(string SearchData, string iCompanyID, string iBranchID, out string errmsg)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand com = new SqlCommand("Web_Get_Order_Stats", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@iCompanyID", iCompanyID);
            com.Parameters.AddWithValue("@iBranchID", iBranchID);
            com.Parameters.AddWithValue("@SearchData", SearchData);
            ds = CommonCode.getDataInDataSet(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }

    public DataSet get_admin_stats_PendingPayment(string iCompanyID, string iBranchID, string TransactionOrderID, out string errmsg)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_order_stats_PendingPayment", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@iCompanyID", iCompanyID);
            com.Parameters.AddWithValue("@iBranchID", iBranchID);
            com.Parameters.AddWithValue("@TransactionOrderID", TransactionOrderID);
            ds = CommonCode.getDataInDataSet(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }

    public DataTable get_admin_pending_reviews(string iCompanyID, string iBranchID, string ReviewID, out string errmsg)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_Get_MasterProductReview", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@iCompanyID", iCompanyID);
            com.Parameters.AddWithValue("@iBranchID", iBranchID);
            com.Parameters.AddWithValue("@ReviewID", ReviewID);
            ds = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }

    public DataTable get_Sliders(string iCompanyID, string iBranchID, out string errmsg)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_Get_Sliders", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@iCompanyID", iCompanyID);
            ds = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }

    public void Insert_Update_Delete_Sliders(string SliderID, string SliderPath, string SliderLink,int Action, string BackPath, string str_BookCode, string iCompanyID , out string errmsg)
    {
        try
        {            
            SqlCommand com = new SqlCommand("web_insert_edit_delete_sliders", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@SliderID", SqlDbType.BigInt).Value = SliderID;
            com.Parameters.Add("@SliderPath", SqlDbType.NVarChar).Value = SliderPath;
            com.Parameters.Add("@URL", SqlDbType.NVarChar).Value = SliderLink;
            com.Parameters.Add("@action", SqlDbType.Int).Value = Action;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@BookCode", SqlDbType.VarChar, 20).Value = str_BookCode;
            com.Parameters.Add("@BackImagePath", SqlDbType.VarChar, 8000).Value = BackPath;            
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {            
            errmsg = ex.Message;
        }
    }

    public DataTable get_BannerImages(string iCompanyID, string BannerID, out string errmsg)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_banner_image", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@iCompanyID", iCompanyID);
            com.Parameters.AddWithValue("@BannerID", BannerID);
            ds = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }

    public void Insert_Update_Delete_Banners(string ImageID, string BannerID , string ImagePath, string BannerLink, bool Active, int Action, string str_BookCode, string iCompanyID, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("web_insert_edit_banner_images", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ImageID", SqlDbType.BigInt).Value = ImageID;
            com.Parameters.Add("@BannerID", SqlDbType.Int).Value = BannerID;
            com.Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = ImagePath;
            com.Parameters.Add("@Active", SqlDbType.Bit).Value = Active;
            com.Parameters.Add("@Link", SqlDbType.VarChar,1000).Value = BannerLink;
            com.Parameters.Add("@action", SqlDbType.Int).Value = Action;
            com.Parameters.Add("@BookCode", SqlDbType.VarChar, 20).Value = str_BookCode;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID; 
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_Subjectsubject(string SubjectID, string iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_GetSubSubjectTitleWise", CommonCode.con);
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@SubjectId", SqlDbType.VarChar,7).Value = SubjectID;            
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }


    public DataTable get_Shipping_Information(string Custcode, string AddressID, string iCompanyID, string iBranchID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_Get_Cust_Shipp_Address", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = Custcode;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.Parameters.Add("@AddressID", SqlDbType.BigInt).Value = AddressID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_load_cart_products(string Custcode, string otherCountry, string iCompanyID, string iBranchID, out string errmsg) 
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("web_curr_get_cart_forCheckout", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = Custcode;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = otherCountry;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void update_Cart_QTY(int qty, string CartID, string ProductID, string iCompanyID, string iBranchId, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("web_update_cart_qty", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@Qty", SqlDbType.Int).Value = qty;
            com.Parameters.Add("@CartID", SqlDbType.BigInt).Value = CartID;
            com.Parameters.Add("@ProductId", SqlDbType.VarChar, 20).Value = ProductID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchId", SqlDbType.Int).Value = iBranchId;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_searchdatabyUsers(string websearchtype, string websearchText, string Searchsubjectlist, string SearchsubjectlistID, string OtherCountry, 
        string iCompanyID, string iBranchID, string rbPublisherID, string rbAuthorID, string ddlLanguageID, string rbCategoryID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_sp_GetDataforSearch_Item_from_Web", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@websearchtype", SqlDbType.VarChar, 1000).Value = websearchtype;
            com.Parameters.Add("@websearchText", SqlDbType.VarChar, 10000).Value = websearchText;
            com.Parameters.Add("@Searchsubjectlist", SqlDbType.VarChar, 1000).Value = Searchsubjectlist;
            com.Parameters.Add("@SearchsubjectlistID", SqlDbType.VarChar, 1000).Value = SearchsubjectlistID;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
            com.Parameters.Add("@icompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.Parameters.Add("@PublisherID", SqlDbType.VarChar, 100).Value = rbPublisherID;
            com.Parameters.Add("@AuthorID", SqlDbType.VarChar, 100).Value = rbAuthorID;
            com.Parameters.Add("@LanguageID", SqlDbType.VarChar, 100).Value = ddlLanguageID;
            com.Parameters.Add("@TitleCategoryID", SqlDbType.VarChar, 100).Value = rbCategoryID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_edit_delete_wishlist(string WishlistID, string CustID, string ProductID,int action, string iCompanyID, string iBranchId, out string errmsg)
    { 
        try
        {
            SqlCommand com = new SqlCommand("web_insert_edit_delete_wishlist", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@WishlistID", SqlDbType.BigInt).Value = 0;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = CustID;
            com.Parameters.Add("@ProductID", SqlDbType.VarChar, 20).Value = ProductID;
            com.Parameters.Add("@Action", SqlDbType.Int).Value = 0;
            com.Parameters.Add("@icompanyid", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchId;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_custom_searchdatabyUsers(string websearchtype, string websearchText, string OtherCountry, string iCompanyID, string iBranchID, String cmbcategory, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("web_getcustomsearchbyUser", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@FilterType", SqlDbType.VarChar, 1000).Value = websearchtype;
            com.Parameters.Add("@Searchtext", SqlDbType.VarChar, 8000).Value = websearchText;            
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
            com.Parameters.Add("@icompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.Parameters.Add("@CategoryID", SqlDbType.VarChar, 7).Value = (cmbcategory == "" ? (object)DBNull.Value : cmbcategory);
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_SliderTwo(string iCompanyID, string iBranchID, out string errmsg)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_Get_SliderTwo", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@iCompanyID", iCompanyID);
            ds = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            ds = null;
        }
        return ds;
    }

    public void Insert_Update_Delete_SliderTWO(string SliderID, string SliderPath, string SliderLink, int Action, string BackPath, string str_BookCode, string iCompanyID, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("web_insert_edit_delete_sliderTwo", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@SliderID", SqlDbType.BigInt).Value = SliderID;
            com.Parameters.Add("@SliderPath", SqlDbType.NVarChar).Value = SliderPath;
            com.Parameters.Add("@URL", SqlDbType.NVarChar).Value = SliderLink;
            com.Parameters.Add("@action", SqlDbType.Int).Value = Action;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@BookCode", SqlDbType.VarChar, 20).Value = str_BookCode;
            com.Parameters.Add("@BackImagePath", SqlDbType.VarChar, 8000).Value = BackPath;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_TodaySaying(string TodayQuoteID, string iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_TodayQuoteNews", CommonCode.con);
            com.Parameters.Add("@TodayQuoteID", SqlDbType.BigInt).Value = TodayQuoteID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }
    public void insert_update_delete_TodayQuoteNews(string TodayQuoteID , string Name, string Type, string HeaderContent, string MainContent, int SortOrder, int action, string iCompanyID, string strQuoteHeading, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_insert_edit_delete_TodayQuoteNews", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@TodayQuoteID", SqlDbType.BigInt).Value = TodayQuoteID;
            com.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = Name;
            com.Parameters.Add("@Type", SqlDbType.VarChar, 20).Value = Type;
            com.Parameters.Add("@HeaderContent", SqlDbType.NVarChar).Value = HeaderContent;
            com.Parameters.Add("@MainContent", SqlDbType.NVarChar).Value = MainContent; 
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@QuoteHeading", SqlDbType.VarChar, 250).Value = strQuoteHeading; 
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public void insert_update_delete_CityWiseShippingCharges(string CityShipAmountID, string CityID, string iCompanyID, float FromWeight, float ToWeight, float ShippingAmount, int action, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_insert_edit_delete_CityWiseShipping", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CityShipAmountID", SqlDbType.BigInt).Value = CityShipAmountID;
            com.Parameters.Add("@CityID", SqlDbType.VarChar, 10).Value = CityID;
            com.Parameters.Add("@FromWeight", SqlDbType.Decimal).Value = FromWeight;
            com.Parameters.Add("@ToWeight", SqlDbType.Decimal).Value = ToWeight;
            com.Parameters.Add("@ShippingAmount", SqlDbType.Decimal).Value = ShippingAmount;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_CityWiseShippingCharges(string CityShipAmountID,  string CityID,  string iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_CityWiseShippingCharges", CommonCode.con); 
            com.Parameters.Add("@CityShipAmountID", SqlDbType.BigInt).Value = CityShipAmountID;
            com.Parameters.Add("@CityID", SqlDbType.VarChar, 10).Value = CityID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_ExamsNotifications(string MenuID, string iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_ExamsNotification", CommonCode.con);
            com.Parameters.Add("@ExamNotificationID", SqlDbType.BigInt).Value = MenuID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }


    public void insert_update_delete_ExamNotification(string ExamNotificationID, string Name, string Type, string HeaderContent, string MainContent, int SortOrder, int action, string iCompanyID, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_insert_edit_delete_ExamNotification", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ExamNotificationID", SqlDbType.BigInt).Value = ExamNotificationID;
            com.Parameters.Add("@Name", SqlDbType.VarChar, 200).Value = Name;
            com.Parameters.Add("@Type", SqlDbType.VarChar, 20).Value = Type;
            com.Parameters.Add("@HeaderContent", SqlDbType.NVarChar).Value = HeaderContent;
            com.Parameters.Add("@MainContent", SqlDbType.NVarChar).Value = MainContent;
            com.Parameters.Add("@SortOrder", SqlDbType.Int).Value = SortOrder;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_AreaWiseShippingCharges(string AreaShipAmountID, string AreaID, string iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_AreaWiseShippingCharges", CommonCode.con);
            com.Parameters.Add("@AreaShipAmountID", SqlDbType.BigInt).Value = AreaShipAmountID;
            com.Parameters.Add("@AreaID", SqlDbType.VarChar, 10).Value = AreaID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_update_delete_AreaWiseShippingCharges(string AreaShipAmountID, string AreaID, string iCompanyID, float FromRate, float ToRate, float ShippingAmount, int action, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_Insert_Update_Delete_AreaWiseShippingCharges", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@AreaShipAmountID", SqlDbType.BigInt).Value = AreaShipAmountID;
            com.Parameters.Add("@AreaID", SqlDbType.VarChar, 50).Value = AreaID;
            com.Parameters.Add("@FromRate", SqlDbType.Decimal).Value = FromRate;
            com.Parameters.Add("@ToRate", SqlDbType.Decimal).Value = ToRate;
            com.Parameters.Add("@ShippingAmount", SqlDbType.Decimal).Value = ShippingAmount;
            com.Parameters.Add("@action", SqlDbType.Int).Value = action;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_CLosingQty(string icompanyID, string iBranchId, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("web_GetClosingQty", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@iCompanyID", SqlDbType.BigInt).Value = icompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.BigInt).Value = iBranchId;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void insert_CLosingQty(string ClosingQty, string icompanyID, string iBranchId, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_Insert_CLosingQty", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@ClosingQty", SqlDbType.BigInt).Value = ClosingQty;
            com.Parameters.Add("@iCompanyID", SqlDbType.BigInt).Value = icompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.BigInt).Value = iBranchId;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public void UpdateOrderPaymentRefNo(string TransactionIDord, string iCompanyID, string iBranchID, String FinancialPeriod, String PaymentOrderRefNo, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("web_UpdateOrderPaymentOrderRefNo", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@TransactionIDord", SqlDbType.BigInt).Value = TransactionIDord;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.Parameters.Add("@FinancialPeriod", SqlDbType.VarChar, 10).Value = FinancialPeriod;
            com.Parameters.Add("@PaymentOrderRefNo", SqlDbType.VarChar, 5000).Value = PaymentOrderRefNo;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_UserLoginPassword_Forgot(string strEmailID, string strPassword, string striCompanyID, string striBranchID, out string errormsg)
    {
        DataTable dtTbl = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        string strCon = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        SqlConnection sqlCon = new SqlConnection(strCon);
        SqlCommand loadCommand;
        try
        {
            sqlCon.Open();
            loadCommand = new SqlCommand("Web_Customer_login", sqlCon);
            loadCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = loadCommand;
            adapter.SelectCommand.CommandTimeout = 0;
            loadCommand.Parameters.AddWithValue("@UserName", strEmailID);
            loadCommand.Parameters.AddWithValue("@Password", strPassword);
            loadCommand.Parameters.AddWithValue("@iCompanyID", striCompanyID);
            loadCommand.Parameters.AddWithValue("@iBranchID", striBranchID);
            adapter.Fill(dtTbl);
            errormsg = "success";
        }
        catch (Exception e)
        {
            errormsg = e.Message;
        }
        finally
        {
            sqlCon.Close();
            if ((adapter.SelectCommand != null))
            {
                if ((adapter.SelectCommand.Connection != null))
                {
                    adapter.SelectCommand.Connection.Dispose();
                }
                adapter.SelectCommand.Dispose();
            }
            adapter.Dispose();
        }
        return dtTbl;
    }


    public DataTable InsertErrorLog(string ErrorMessage, string ErrorSeverity,
           string ErrorState, string UserID)
    {
        DataTable dtTbl = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        try
        {
            CommonCode md = new CommonCode();
            SqlConnection conn = new SqlConnection(md.GetConnectioName());
            SqlCommand loadCommand = new SqlCommand("cp_sp_InsertErrorLog", conn);
            loadCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = loadCommand;
            adapter.SelectCommand.CommandTimeout = 0;
            loadCommand.Parameters.AddWithValue("@ErrorMessage", ErrorMessage);
            loadCommand.Parameters.AddWithValue("@ErrorSeverity", ErrorSeverity);
            loadCommand.Parameters.AddWithValue("@ErrorState", ErrorState);
            loadCommand.Parameters.AddWithValue("@UserID", UserID);
            adapter.Fill(dtTbl);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if ((adapter.SelectCommand != null))
            {
                if ((adapter.SelectCommand.Connection != null))
                {
                    adapter.SelectCommand.Connection.Dispose();
                }
                adapter.SelectCommand.Dispose();
            }
            adapter.Dispose();
        }

        return dtTbl;
    }

    public void insert_update_delete_New_Released(string NewReleaseID, string ISBN, bool isNewRelease, int action, string iCompanyID,string messagetoshow, out string errmsg) 
    {
        try
        {
            SqlCommand com = new SqlCommand("web_insert_Update_Delete_NewReleasedItem", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@NewReleaseID", SqlDbType.BigInt).Value = NewReleaseID;            
            com.Parameters.Add("@BookCode", SqlDbType.VarChar, 20).Value = ISBN;
            com.Parameters.Add("@IsNewReleased", SqlDbType.Bit).Value = isNewRelease;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@MessagetoShow", SqlDbType.VarChar, 5000).Value = messagetoshow;
            com.Parameters.Add("@Action", SqlDbType.Int).Value = action;
            
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }
    public DataTable get_New_Launched_items(string NewReleaseID, string iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("web_Get_NewReleasedItem", CommonCode.con);
            com.Parameters.Add("@NewReleaseID", SqlDbType.BigInt).Value = NewReleaseID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }
    public DataTable get_New_Launched_item_from_Cart(string ISBN, string iCompanyID, out string errmsg) 
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_get_New_Launched_item_from_Cart", CommonCode.con);
            com.Parameters.Add("@ISBN", SqlDbType.VarChar, 20).Value = ISBN;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = iCompanyID;
            com.CommandType = CommandType.StoredProcedure;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_searchdatabyUsers_Pagination(string websearchtype, string websearchText, string Searchsubjectlist, string SearchsubjectlistID, string OtherCountry,
        string iCompanyID, string iBranchID, string rbPublisherID, string rbAuthorID, string ddlLanguageID, string rbCategoryID, string SearchSubsubjectlistID,
        string TitleSubCategoryID, string PublishYear, string Edition, string Binding, Int64 pageIndex, Int64 pageSize, Int64 TotalRecord, string Sortby, 
        string sortingAcenDesc, string ClassList, string MediumList, out string errmsg) 
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("Web_sp_GetDataforSearch_Item_from_Web_Refine", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@websearchtype", SqlDbType.VarChar, 1000).Value = websearchtype;
            com.Parameters.Add("@websearchText", SqlDbType.VarChar, 10000).Value = websearchText;
            com.Parameters.Add("@Searchsubjectlist", SqlDbType.VarChar, 1000).Value = Searchsubjectlist;
            com.Parameters.Add("@SearchsubjectlistID", SqlDbType.VarChar, 1000).Value = SearchsubjectlistID;
            com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
            com.Parameters.Add("@icompanyID", SqlDbType.Int).Value = iCompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = iBranchID;
            com.Parameters.Add("@PublisherID", SqlDbType.VarChar, 100).Value = rbPublisherID;
            com.Parameters.Add("@AuthorID", SqlDbType.VarChar, 100).Value = rbAuthorID;
            com.Parameters.Add("@LanguageID", SqlDbType.VarChar, 100).Value = ddlLanguageID;
            com.Parameters.Add("@TitleCategoryID", SqlDbType.VarChar, 100).Value = rbCategoryID;
            com.Parameters.Add("@Edition", SqlDbType.VarChar, 100).Value = Edition;
            com.Parameters.Add("@SearchSubsubjectlistID", SqlDbType.VarChar, 100).Value = SearchSubsubjectlistID;
            com.Parameters.Add("@TitleSubCategoryID", SqlDbType.VarChar, 100).Value = TitleSubCategoryID;
            com.Parameters.Add("@PublishYear", SqlDbType.VarChar, 100).Value = PublishYear;
            com.Parameters.Add("@Binding", SqlDbType.VarChar, 10).Value = Binding;
            com.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pageIndex;
            com.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
            com.Parameters.Add("@TotalRecord", SqlDbType.BigInt).Value = TotalRecord;
            com.Parameters.Add("@Sortby", SqlDbType.VarChar,50).Value = Sortby;
            com.Parameters.Add("@sortingAcenDesc", SqlDbType.VarChar,50).Value = sortingAcenDesc;
            com.Parameters.Add("@SiteName", SqlDbType.VarChar, 100).Value = "";
            com.Parameters.Add("@SchoolCode", SqlDbType.VarChar, 100).Value = "";
            com.Parameters.Add("@MinPrice", SqlDbType.BigInt).Value = 0;
            com.Parameters.Add("@MaxPrice", SqlDbType.BigInt).Value = 1000000;
            com.Parameters.Add("@ExcludeOutofStock", SqlDbType.Bit).Value = false;
            com.Parameters.Add("@InterestAgeGroup", SqlDbType.VarChar, 100).Value = "";
            com.Parameters.Add("@BookClass", SqlDbType.VarChar, 1000).Value = ClassList;
            com.Parameters.Add("@BookMedium", SqlDbType.VarChar, 1000).Value = MediumList;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public void Insert_UserBookList(string UserID, string SchoolName, string ClassName, string LanguageName, string BookListPath ,int Action, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("web_Insert_User_BookList", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustomerID", SqlDbType.VarChar, 30).Value = UserID;
            com.Parameters.Add("@SchoolName", SqlDbType.VarChar, 200).Value = SchoolName;
            com.Parameters.Add("@ClassName", SqlDbType.VarChar, 50).Value = ClassName;
            com.Parameters.Add("@LanguageName", SqlDbType.VarChar, 50).Value = LanguageName;
            com.Parameters.Add("@BookListPath", SqlDbType.VarChar, 500).Value = BookListPath;
            com.Parameters.Add("@Action", SqlDbType.Int).Value = Action;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    public DataTable get_UserBookList(string UserID, string SchoolName, string ClassName, string LanguageName, string BookListPath, int Action, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("web_Insert_User_BookList", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustomerID", SqlDbType.VarChar, 30).Value = UserID;
            com.Parameters.Add("@SchoolName", SqlDbType.VarChar, 200).Value = SchoolName;
            com.Parameters.Add("@ClassName", SqlDbType.VarChar, 50).Value = ClassName;
            com.Parameters.Add("@LanguageName", SqlDbType.VarChar, 50).Value = LanguageName;
            com.Parameters.Add("@BookListPath", SqlDbType.VarChar, 500).Value = BookListPath;
            com.Parameters.Add("@Action", SqlDbType.Int).Value = Action;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable Get_SMS_Information(string companyID, string BranchID)
    {
        DataTable dtTbl = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        try
        {
            CommonCode md = new CommonCode();
            SqlConnection conn = new SqlConnection(md.GetConnectioName());
            SqlCommand loadCommand = new SqlCommand("cp_sp_GetSMSInformation", conn);
            loadCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = loadCommand;
            adapter.SelectCommand.CommandTimeout = 0; 
            loadCommand.Parameters.AddWithValue("@iCompanyID", companyID); 
            adapter.Fill(dtTbl);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if ((adapter.SelectCommand != null))
            {
                if ((adapter.SelectCommand.Connection != null))
                {
                    adapter.SelectCommand.Connection.Dispose();
                }
                adapter.SelectCommand.Dispose();
            }
            adapter.Dispose();
        }

        return dtTbl;
    }

    public DataTable get_Subject_BySubjectTable(DataTable ddata, string iCompanyID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand com = new SqlCommand("web_Get_SubjectListNonThema", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SubjectList", ddata);
            com.Parameters.AddWithValue("@iCompanyID", iCompanyID);
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }


    public DataTable get_schools(string SchoolID, string ICompanyID, string IBranchID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            //SqlCommand com = new SqlCommand("select PubCustCode,CustName from MasterCustomer where UserType='SCHOOL' Order by CustName Asc ", CommonCode.con);
            SqlCommand com = new SqlCommand("Web_Load_Schools", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = ICompanyID;
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = IBranchID;
            com.Parameters.Add("@School", SqlDbType.VarChar, 150).Value = SchoolID;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_Classes(string className, string SchoolID, string ICompanyID, string IBranchID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            if (string.IsNullOrEmpty(SchoolID))
                SchoolID = "0";
            SqlCommand com = new SqlCommand("Web_Get_Class", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@PubCustCode", SqlDbType.VarChar, 30).Value = SchoolID;
            com.Parameters.Add("@ICompanyID", SqlDbType.Int).Value = ICompanyID;
            com.Parameters.Add("@className", SqlDbType.VarChar, 100).Value = className;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }

    public DataTable get_Bundles(string bundles, string ClassCode, string SchoolID, string ICompanyID, string IBranchID, out string errmsg)
    {
        DataTable dt = new DataTable();
        try
        {
            if (string.IsNullOrEmpty(ClassCode))
                ClassCode = "0";
            SqlCommand com = new SqlCommand("Web_Get_Class_Bundles", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@SchoolID", SqlDbType.VarChar, 30).Value = SchoolID;
            com.Parameters.Add("@ClassCode", SqlDbType.VarChar, 30).Value = ClassCode;
            com.Parameters.Add("@ICompanyID", SqlDbType.Int).Value = ICompanyID;
            com.Parameters.Add("@bundles", SqlDbType.VarChar, 100).Value = bundles;
            dt = CommonCode.getData(com, out errmsg);
        }
        catch (Exception ex)
        {
            errmsg = ex.Message;
            dt = null;
        }
        return dt;
    }
    public DataTable Get_Set_Information_By_School_Class_Set(string schoolID, String ClassID, String SetID, string companyID, string BranchID, string BookCategoryID)
    {
        DataTable dtTbl = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        try
        {
            CommonCode md = new CommonCode();
            SqlConnection conn = new SqlConnection(md.GetConnectioName());
            SqlCommand loadCommand = new SqlCommand("Web_GetMasterBundleDetail", conn);
            loadCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = loadCommand;
            adapter.SelectCommand.CommandTimeout = 0;
            loadCommand.Parameters.AddWithValue("@SchoolId", schoolID);
            loadCommand.Parameters.AddWithValue("@ClassID", ClassID);
            loadCommand.Parameters.AddWithValue("@BUNDLECODE", SetID);
            loadCommand.Parameters.AddWithValue("@iCompanyID", companyID);
            loadCommand.Parameters.AddWithValue("@iBranchID", BranchID);
            loadCommand.Parameters.AddWithValue("@BookCategoryID", BookCategoryID); 
            adapter.Fill(dtTbl);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if ((adapter.SelectCommand != null))
            {
                if ((adapter.SelectCommand.Connection != null))
                {
                    adapter.SelectCommand.Connection.Dispose();
                }
                adapter.SelectCommand.Dispose();
            }
            adapter.Dispose();
        }

        return dtTbl;
    }

    public DataTable insert_order_for_school_set(string CustomerID, string str_SchoolID, string str_ClassID, string str_SetID, String iCompanyId, String iBranchID,
                                           String AddresID, String FinancialPeriod, String StudentName, String ROllNumber) 
    {
        DataTable dtTbl = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        try
        {
            CommonCode md = new CommonCode();
            SqlConnection conn = new SqlConnection(md.GetConnectioName());
            SqlCommand loadCommand = new SqlCommand("Web_insert_order_Ecommerce_for_School_Site", conn);
            loadCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = loadCommand;
            adapter.SelectCommand.CommandTimeout = 0;
            loadCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
            loadCommand.Parameters.AddWithValue("@SchoolID", str_SchoolID);
            loadCommand.Parameters.AddWithValue("@ClassID", str_ClassID);
            loadCommand.Parameters.AddWithValue("@SetID", str_SetID);
            loadCommand.Parameters.AddWithValue("@iCompanyId", iCompanyId);
            loadCommand.Parameters.AddWithValue("@iBranchID", iBranchID);
            loadCommand.Parameters.AddWithValue("@ShipToAddressID", AddresID);
            loadCommand.Parameters.AddWithValue("@FinancialPeriod", FinancialPeriod);
            loadCommand.Parameters.AddWithValue("@StudentName", StudentName);
            loadCommand.Parameters.AddWithValue("@ROllNumber", ROllNumber);
            adapter.Fill(dtTbl);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if ((adapter.SelectCommand != null))
            {
                if ((adapter.SelectCommand.Connection != null))
                {
                    adapter.SelectCommand.Connection.Dispose();
                }
                adapter.SelectCommand.Dispose();
            }
            adapter.Dispose();
        }

        return dtTbl;
    }

    public DataTable Get_order_Detail_by_OrderID(string CustomerID, string str_OrderID, String iCompanyId, String iBranchID, String FinancialPeriod )
    {
        DataTable dtTbl = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        try
        {
            CommonCode md = new CommonCode();
            SqlConnection conn = new SqlConnection(md.GetConnectioName());
            SqlCommand loadCommand = new SqlCommand("Web_Get_order_Ecommerce_for_School_Site_Payment", conn);
            loadCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = loadCommand;
            adapter.SelectCommand.CommandTimeout = 0;
            loadCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
            loadCommand.Parameters.AddWithValue("@OrderID", str_OrderID); 
            loadCommand.Parameters.AddWithValue("@iCompanyId", iCompanyId);
            loadCommand.Parameters.AddWithValue("@iBranchID", iBranchID); 
            loadCommand.Parameters.AddWithValue("@FinancialPeriod", FinancialPeriod); 
            adapter.Fill(dtTbl);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if ((adapter.SelectCommand != null))
            {
                if ((adapter.SelectCommand.Connection != null))
                {
                    adapter.SelectCommand.Connection.Dispose();
                }
                adapter.SelectCommand.Dispose();
            }
            adapter.Dispose();
        }

        return dtTbl;
    }


}




