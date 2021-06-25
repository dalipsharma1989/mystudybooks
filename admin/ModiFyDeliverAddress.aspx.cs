using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class _Default : System.Web.UI.Page
{
    string orderqty;
    string OrderAmt;
    DataTable dtOrder;
    Decimal shipamt;
    Decimal TotalNet;
    Decimal intHandling;
    Decimal roundoffamt;
    string TotalTaxable;
    string Totaltax ;
    string TotalNetAmt ;

    string PId, ISBNs, TQty, Curr, SPrice, TotAmt,BundleID;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ltr_ship_alert_msg.Text = "";
        ltr_scripts.Text = "";
        String CustomerCode = "";
        if (Session["LoginSessionID"] == null || Session["LoginSessionID"].ToString() == "")
        {
            Response.Redirect("../admin/");
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["CustID"]))
            {
                CustomerCode = Request.QueryString["CustID"].ToString();
                CommonCode.getCountry_Details(0, "", "", bill_dd_Country);
                CommonCode.getCountry_Details(0, "", "", dd_new_ship_country);
                load_shipping_locations(CustomerCode);
            }
        } 
    }

      

    public decimal TruncateDecimal(decimal value, int precision)
    {
        decimal step = (decimal)Math.Pow(10, precision);
        decimal tmp = Math.Truncate(step * value);
        return tmp / step;
    }

    private void load_Notifications()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        SqlCommand com = new SqlCommand(" select top 1 ManageRowID , MngLineone , MngLineTwo , ManageCheckoutNotification from Web_ManageNotification ", CommonCode.con);
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ManageCheckoutNotification"].ToString() == "")
                {
                    ul_impinfo.Visible = false;
                }
                else
                {
                    lblcheckOutNotification.Text = dt.Rows[0]["ManageCheckoutNotification"].ToString();
                }
                
                
            }
        }
        else
        {
            //CommonCode.show_alert("danger", "Error while loading Notification", errmsg, ph_msg);
        }
    }

    public void load_cart_products()
    {
        string errmsg = "";
        SqlCommand com = new SqlCommand("dbo_get_cart", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = Session["CustID"];
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                int tot_qty = 0;
                float total_amount = 0;
                foreach (DataRow rr in dt.Rows)
                {
                    tot_qty += Int32.Parse(rr["Qty"].ToString());
                    total_amount += float.Parse(rr["TotalAmt"].ToString());
                }
                hf_cartID.Value = dt.Rows[0]["CartID"].ToString();
                //hf_totalitems.Value = dt.Rows.Count + "";
                hf_totalitems.Value = tot_qty + "";
                b_total_amount.InnerHtml = dt.Rows[0]["SaleCurrency"] + " " + string.Format(CommonCode.AmountFormat(), total_amount);
                hf_total_amount.Value = total_amount.ToString();
                rp_cart.DataSource = dt;
                rp_cart.DataBind();

                string productinfo = "";

                if (dt.Rows.Count == 1)
                    productinfo = dt.Rows[0]["ISBN"] + "@ " + string.Format(CommonCode.AmountFormat(), dt.Rows[0]["Price"].ToString());
                else
                    productinfo = dt.Rows.Count + " books @ " + string.Format(CommonCode.AmountFormat(), total_amount);
                hf_productinfo.Value = productinfo;
            }
            else
            {
                CommonCode.show_alert("warning", "Cart is empty", "No products found in your cart. Please select books in cart to proceed.", ph_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Cart products", errmsg, ph_msg);
        }
    }

    private void load_shipping_locations(String CustomerCode = "")
    {
        string errmsg = ""; 
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        DataTable dts = new DataTable(); 
        errmsg = "";

        dt = dal.get_Shipping_Information(CustomerCode, "0", Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg); 
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                rp_ship_addresses.DataSource = dt;
                rp_ship_addresses.DataBind();
            }
            else
            {
                CommonCode.show_alert("info", "No Shipping Address Found", "", false, ltr_ship_alert_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Shipping Address", errmsg, ph_msg);
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
                bill_dd_Country.SelectedIndex = bill_dd_Country.Items.IndexOf(bill_dd_Country.Items.FindByValue(dt.Rows[0]["CountryID"].ToString()));
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

    private void save_shipping_location()
    {
        DAL dal = new DAL();
        string errmsg = "", AddressID_output = "";
        dal.insert_update_delete_shipping_address(hf_ShipaddressID.Value, Request.QueryString["CustID"].ToString(),bill_textAdress.Text.ToString(), bill_dd_City.SelectedValue,"0", bill_textZipCode.Text.Trim(), 
            bill_textPhone.Text, "", bill_textPhone.Text, bill_textEmailID.Text, false, 1, Session["iCompanyId"].ToString(), out AddressID_output, out errmsg);
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


    protected void rp_ship_addresses_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string AddressID = e.CommandArgument.ToString();
        if (e.CommandName == "Change_ship_address")
        {
            DataTable dt = new DataTable();
            DAL dal = new DAL();
            String errmsg = "";
            dt = dal.get_Shipping_Information(Request.QueryString["CustID"].ToString(), AddressID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);
            if (errmsg == "success")
            {
               if(dt.Rows.Count > 0)
                {
                    hf_ShipaddressID.Value = dt.Rows[0]["AddressID"].ToString();
                    bill_textName.Text = dt.Rows[0]["CustName"].ToString();
                    bill_textPhone.Text = dt.Rows[0]["Mobile"].ToString();
                    bill_textEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                    bill_textAdress.Text = dt.Rows[0]["ShipAddress"].ToString();
                    bill_textZipCode.Text = dt.Rows[0]["ShipPostalCode"].ToString();
                    bill_dd_Country.SelectedIndex = bill_dd_Country.Items.IndexOf(bill_dd_Country.Items.FindByValue(dt.Rows[0]["CountryID"].ToString()));
                    CommonCode.getCountry_Details(1, bill_dd_Country.SelectedValue, "", bill_dd_State);
                    bill_dd_State.SelectedIndex = bill_dd_State.Items.IndexOf(bill_dd_State.Items.FindByValue(dt.Rows[0]["StateID"].ToString()));
                    CommonCode.getCountry_Details(2, "", bill_dd_State.SelectedValue, bill_dd_City);
                    bill_dd_City.SelectedIndex = bill_dd_City.Items.IndexOf(bill_dd_City.Items.FindByValue(dt.Rows[0]["CityID"].ToString()));
                }
                else
                {
                    CommonCode.show_toastr("error", "No Shipping Location Found!", errmsg.Replace('\'', ' '), false, "", false, "", ltr_scripts);
                }
            }
            else
            {
                CommonCode.show_toastr("error", "Error while loading Shipping Location!", errmsg.Replace('\'', ' '), false, "", false, "", ltr_scripts);
            }
        }
    }

    protected void btn_Save_new_shipping_address_Click(object sender, EventArgs e)
    {
        save_shipping_location();
    }

    protected void bill_dd_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        bill_dd_State.Items.Clear();
        CommonCode.getCountry_Details(1, bill_dd_Country.SelectedValue, "", bill_dd_State);
        bill_dd_Country.Focus();
    }

    protected void bill_dd_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        bill_dd_City.Items.Clear();
        CommonCode.getCountry_Details(2, "", bill_dd_State.SelectedValue, bill_dd_City);
        bill_dd_State.Focus();
    }

    protected void dd_new_ship_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_new_ship_state.Items.Clear();
        CommonCode.getCountry_Details(1, dd_new_ship_country.SelectedValue, "", dd_new_ship_state);
        dd_new_ship_country.Focus();
    }

    protected void dd_new_ship_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_new_ship_city.Items.Clear();
        CommonCode.getCountry_Details(2, "", dd_new_ship_state.SelectedValue, dd_new_ship_city);
        dd_new_ship_state.Focus();
    }
      

    private void InsertOrder(String AddressID, out string OrderID, out string errmsg)
    {
        string CustID, BillingAddress, BillingCityID, BillingPostalCode, BillingPhone, Mobile, EmailID, ShipAddress, ShipCityID,
            ShipPostalCode, ShipPhone, TotalQty, TotalAmount, ShipCost, prmintHandling, prmroundoffamt,SchoolID="",ClassID="";
        
        CustID = Session["CustID"].ToString();
        BillingAddress = bill_textAdress.Text.Trim();
        BillingCityID = bill_dd_City.SelectedValue;
        BillingPostalCode = bill_textZipCode.Text.Trim();
        BillingPhone = bill_textPhone.Text.Trim();
        Mobile = bill_textPhone.Text.Trim();
        EmailID = bill_textEmailID.Text.Trim();
        ShipAddress = bill_textAdress.Text.Trim();
        ShipCityID = "";
        ShipPostalCode = "";
        ShipPhone = "";
        TotalQty = orderqty;
        TotalAmount = OrderAmt;
        ShipCost = shipamt.ToString();
        prmintHandling = intHandling.ToString();
        prmroundoffamt = roundoffamt.ToString();
        if (!string.IsNullOrEmpty(Request.QueryString["schoolid"]))
        {
            SchoolID = Request.QueryString["schoolid"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["ClID"]))
        {
            ClassID = Request.QueryString["ClID"].ToString();
        }
        
        
        string OrderID_output = "";
        try
        {  
            SqlCommand com = new SqlCommand("Web_Insert_WebOrder", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = CustID;
            com.Parameters.Add("@ShipToAccountID", SqlDbType.VarChar, 1000).Value = AddressID.ToString();
            com.Parameters.Add("@TotalQty", SqlDbType.Int).Value = TotalQty;
            com.Parameters.Add("@TotalAmount", SqlDbType.Float).Value = TotalAmount;
            com.Parameters.Add("@ShipCost", SqlDbType.Float).Value = ShipCost;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
            com.Parameters.Add("@FinancialPeriod", SqlDbType.VarChar,10).Value = Session["FinancialPeriod"].ToString();
            com.Parameters.Add("@OrderID_output", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            com.Parameters.Add("@intHandling", SqlDbType.Float).Value = prmintHandling;
            com.Parameters.Add("@TotRoundoff", SqlDbType.Float).Value = prmroundoffamt;
            com.Parameters.Add("@SchoolId", SqlDbType.VarChar, 30).Value = SchoolID.Replace("'","");
            com.Parameters.Add("@ClassID", SqlDbType.VarChar, 50).Value = ClassID.Replace("'", "");
            com.Parameters.Add("@BundleID", SqlDbType.VarChar, 30).Value  = dtOrder.Rows[0]["BundleID"].ToString().Replace("'", "");
            if (drp_couriercompany.SelectedValue == "Nil")
            {
                com.Parameters.Add("@CourierMasterID", SqlDbType.BigInt).Value = "0";
            }
            else
            {
                com.Parameters.Add("@CourierMasterID", SqlDbType.BigInt).Value = drp_couriercompany.SelectedValue;
            }
            com.Parameters.Add("@StudentName", SqlDbType.VarChar, 500).Value = bill_textStudent.Text.Trim();
            



            //com.Parameters.Add("@TotalNetAmt", SqlDbType.Float).Value = prmroundoffamt;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                OrderID_output = com.Parameters["@OrderID_output"].Value.ToString();
                Session["OrderID_output"] = OrderID_output;
            }
            else
            {
                OrderID_output = "";
            }
        }
        catch (Exception ex)
        {
            OrderID_output = "";
            errmsg = ex.Message;
        }
        OrderID = OrderID_output;
    }

    private void InsertOrderDetail(string OrderID, out string errmsg)
    {
        string ISBN, Qty, Currency, Price, TotalAmt ,bundleid,tottaxable,tottax,totnetamt;
        string db_errmsg = "", errreport = "";
        //LoadCartValue();
        bundleid = dtOrder.Rows[0]["BundleID"].ToString();
        Session["BundleID"] = dtOrder.Rows[0]["BundleID"].ToString();
        if (dtOrder.Rows.Count > 0)
        {
            for (int i = 0; i < dtOrder.Rows.Count; i++)
            {
                ISBN = dtOrder.Rows[i]["ISBN"].ToString();
                string ssql;
                ssql = "select BookCode ProductId from MasterTitle where BookCode = '" + ISBN + "' and ICompanyID = '"+ Session["iCompanyId"].ToString() +"' ";
                SqlCommand com = new SqlCommand(ssql, CommonCode.con);
                DataTable dt = new DataTable();

                dt = CommonCode.getData(com, out errmsg);
                if (dt.Rows.Count > 0)
                {
                    PId = dt.Rows[0]["ProductId"].ToString();
                }

                Qty = dtOrder.Rows[i]["Qty"].ToString();
                Currency = dtOrder.Rows[i]["SaleCurrency"].ToString();
                Price = dtOrder.Rows[i]["SalePrice"].ToString();
                TotalAmt = dtOrder.Rows[i]["TotalAmount"].ToString();
                tottaxable = dtOrder.Rows[i]["TotalTaxable"].ToString();
                tottax = dtOrder.Rows[i]["TotalTax"].ToString();
                totnetamt = dtOrder.Rows[i]["TotalNetAmount"].ToString();
                                
                saveDetail(OrderID, PId, ISBN, Qty, Currency, Price, TotalAmt, bundleid, tottaxable, tottax, totnetamt, out db_errmsg);

                if (db_errmsg != "success")
                {
                    errreport += "ProductID: " + PId + " | ISBN: " + ISBN + " | Qty: " + Qty + " | Error:" + db_errmsg + " <br> ";
                }
                PId = "";
            }
        }
        if (errreport != "")
            errmsg = errreport;
        else
            errmsg = "success";
    }

    private void saveDetail(String OrderID, String ProductId, String ISBN, String Qty, String Currency, String Price, String TotalAmt,string bundleid, 
        string tottaxable, string tottax, string totnetamt, out string errmsg)
    {
        try
        {
            SqlCommand com = new SqlCommand("Web_Insert_WebOrder_Details", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = OrderID;
            com.Parameters.Add("@ProductId", SqlDbType.VarChar, 1000).Value = ProductId;
            com.Parameters.Add("@ISBN", SqlDbType.VarChar, 20).Value = ISBN;
            com.Parameters.Add("@Qty", SqlDbType.BigInt).Value = Qty;
            com.Parameters.Add("@Currency", SqlDbType.VarChar, 100).Value = Currency;
            com.Parameters.Add("@Price", SqlDbType.Float).Value = Price;
            com.Parameters.Add("@TotalAmt", SqlDbType.Float).Value = TotalAmt;
            com.Parameters.Add("@BundleID", SqlDbType.VarChar, 30).Value = bundleid;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
            com.Parameters.Add("@FinancialPeriod", SqlDbType.VarChar, 10).Value = Session["FinancialPeriod"].ToString();
            com.Parameters.Add("@tottaxable", SqlDbType.Float).Value = tottaxable;
            com.Parameters.Add("@tottax", SqlDbType.Float).Value = tottax;
            com.Parameters.Add("@totnetamt", SqlDbType.Float).Value = totnetamt;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }

        catch (Exception ex)
        {
            errmsg = ex.Message;
        }
    }

    protected void btn_place_order_Click(object sender, EventArgs e)
    {
        save_shipping_location();
    }

    protected void drp_couriercompany_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
}

