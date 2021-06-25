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
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Checkout"); 
        ltr_ship_alert_msg.Text = "";
        ltr_scripts.Text = "";
        ltr_ship_alert_msg.Visible = true;
        Session["RefNO"] = null;        
        if (Session["CustID"] != null && Session["CustType"] != null && Session["CustID"].ToString() != "guest")
        {
            if (!IsPostBack)
            {
                
                if (ConfigurationManager.AppSettings["DefaultCountryCode"].ToString() != "")
                {
                    CommonCode.getCountry_Details(0, ConfigurationManager.AppSettings["DefaultCountryCode"].ToString(), "", bill_dd_Country);
                    CommonCode.getCountry_Details(1, ConfigurationManager.AppSettings["DefaultCountryCode"].ToString(), "", bill_dd_State);
                                        
                    CommonCode.getCountry_Details(0, ConfigurationManager.AppSettings["DefaultCountryCode"].ToString(), "", dd_new_ship_country);
                    CommonCode.getCountry_Details(1, ConfigurationManager.AppSettings["DefaultCountryCode"].ToString(), "", dd_new_ship_state);
                }
                else
                {
                    
                    CommonCode.getCountry_Details(0, "", "", bill_dd_Country);
                    CommonCode.getCountry_Details(0, "", "", dd_new_ship_country);
                }
                
                load_user_details();
                load_shipping_locations();
                load_cart_products();
                if (Session["OtherCountry"] != null && Session["OtherCountry"].ToString() == "OtherCountry" )
                {
                    bill_dd_Country.Enabled = false;
                    bill_dd_State.Enabled = false;
                    bill_dd_City.Enabled = false;
                    //rb_ship_method.Enabled = false;
                    rb_ship_method.Items[1].Attributes.CssStyle.Add("visibility", "hidden");                    
                    dv_Country.Visible = false;
                    dv_State.Visible = false;
                    dv_City.Visible = false;
                    //UpdatePanelDelivery.Visible = false;
                }
                else
                {
                    bill_dd_Country.Enabled = true;
                    bill_dd_State.Enabled = true;
                    bill_dd_City.Enabled = true;
                    dv_Country.Visible = true;
                    dv_State.Visible = true;
                    dv_City.Visible = true;
                }


                if (Session["CustType"].ToString() == "Retail")
                {
                    //Retail User
                }
                else if (Session["CustType"].ToString() == "School")
                {
                    //School
                }
                else
                {
                    //Student
                }
            }
           
                div_paypal.Visible = false;
             
        }
        else
        {
            if (Session["CustID"].ToString() == "guest")
            {
                Response.Redirect("user_login.aspx", true);
            }
            else
            {
                Response.Redirect("user_login.aspx?session_expired=true", true);
            }            
        }
    }


    public void load_cart_products()
    {
        string errmsg = ""; 
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_load_cart_products(Session["CustID"].ToString(), Session["OtherCountry"].ToString(), Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                int tot_qty = 0;
                float net_amount = 0; float net_weight = 0;
                float.TryParse(dt.Rows[0]["NetAmount"].ToString(), out net_amount);
                decimal tot_amt = 0, totDisc_amt = 0; 

                // Calculating total quantity
                foreach (DataRow rr in dt.Rows)
                {
                    tot_qty += Convert.ToInt32(rr["Qty"]);
                    tot_amt += Convert.ToDecimal(rr["TotalAmt"].ToString());
                    net_weight += Convert.ToInt32(rr["CartWeight"]);
                    totDisc_amt += Convert.ToDecimal(rr["DiscountAmt"].ToString());
                }

                Cart_Subtotal.InnerText = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + tot_amt.ToString();
                cart_weight.InnerText = net_weight.ToString();
                hf_cartID.Value = dt.Rows[0]["CartID"].ToString();
                SPN_discount.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " - " + totDisc_amt;
                //string tmpText = "+" + dt.Rows[0]["SaleCurrency"] + " " + string.Format(CommonCode.AmountFormat(), dt.Rows[0]["ShipCost"]) + "";
                string tmpText = "+" + " " + string.Format(CommonCode.AmountFormat(), dt.Rows[0]["ShipCost"]) + "";
                shipping_amt.InnerText = tmpText;
                
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "updateText('" + tmpText + "');", true);

                hf_totalitems.Value = tot_qty + "";

                string totalAmount = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + string.Format(CommonCode.AmountFormat(), net_amount.ToString());
                b_total_amount.InnerHtml = totalAmount;
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "amnt", "updateTotalAmount('" + totalAmount + "');", true);
                hf_total_amount.Value = string.Format(CommonCode.AmountFormat(), net_amount.ToString());

                DataTable NewLaunch = new DataTable();
                NewLaunch.Columns.Add("BookName", typeof(string));
                NewLaunch.Columns.Add("TotalAmt", typeof(string));
                NewLaunch.Columns.Add("MessagetoShow", typeof(string));
                DataRow drs;
                DataTable dt1 = new DataTable();
                for (int i=0; i <= dt.Rows.Count - 1; i++)
                {
                    DAL dal1 = new DAL(); 
                    errmsg = "";
                    drs = NewLaunch.NewRow();
                    dt1 = dal1.get_New_Launched_item_from_Cart(dt.Rows[i]["ISBN"].ToString(), Session["iCompanyId"].ToString(), out errmsg);
                    if(dt1.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dt1.Rows)
                        {
                            drs["BookName"] = dr["BookName"].ToString();
                            drs["TotalAmt"] = Convert.ToDecimal(dt.Rows[i]["TotalAmt"].ToString());
                            drs["MessagetoShow"] = dr["MessagetoShow"].ToString();
                            NewLaunch.Rows.Add(drs);
                        }
                    }
                }

                if (NewLaunch.Rows.Count > 0)
                {
                    dv_impinfo.Visible = true;
                    h2_Notice.InnerHtml = NewLaunch.Rows[0]["MessagetoShow"].ToString();
                    rp_NewLaunch.DataSource = NewLaunch;
                    rp_NewLaunch.DataBind();
                }
                else
                {
                    h2_Notice.InnerHtml = "";
                    dv_impinfo.Visible = false;
                    rp_NewLaunch.DataSource = null;
                    rp_NewLaunch.DataBind();
                }
                rp_cart.DataSource = dt;
                rp_cart.DataBind();

                string productinfo = "";
                if (dt.Rows.Count == 1)                    
                    //dt.Rows[0]["ISBN"] + "@" + dt.Rows[0]["SaleCurrency"] + " " + string.Format(CommonCode.AmountFormat(), dt.Rows[0]["TotalAmt"].ToString());
                    productinfo = tot_qty.ToString() + "@" + dt.Rows[0]["SaleCurrency"] + " " + string.Format(CommonCode.AmountFormat(), dt.Rows[0]["TotalAmt"].ToString());
                else
                    productinfo = tot_qty.ToString() + " books @" + dt.Rows[0]["SaleCurrency"] + " " + string.Format(CommonCode.AmountFormat(), net_amount.ToString());
                hf_productinfo.Value = productinfo;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('No products found in your cart. Please select books in cart to proceed.');window.location ='../search_results.aspx';", true);
                CommonCode.show_alert("warning", "Cart is empty", "No products found in your cart. Please select books in cart to proceed.", ph_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Cart products", errmsg, ph_msg);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Shipping Address Deleted !');window.location ='proceed_to_checkout.aspx';", true);
                CommonCode.show_toastr("success", "Shipping Address Deleted !", "", false, "", false, "", ltr_scripts);
            }
            else
            {
                CommonCode.show_toastr("error", "Error while deleting Shipping Location!", errmsg.Replace('\'', ' '), false, "", false, "", ltr_scripts);
            }
        }
    }

    protected void btn_Save_new_shipping_address_Click(object sender, EventArgs e)
    {
        save_shipping_location();
    }

    protected void bill_dd_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Added 5/6/2019
        // If no country selected Clear all address fields
        //if(bill_dd_Country.SelectedIndex == 0)
        //{
        //    clear();
        //    div_paypal.Visible = false;
        //    return;
        //}
        //bill_dd_State.Items.Clear();
        //CommonCode.getCountry_Details(1, bill_dd_Country.SelectedValue, "", bill_dd_State);

        //bool condition = !bill_dd_Country.SelectedValue.Equals("1");
        //if (condition)
        //{
        //    bill_dd_State.Items.FindByText("Others").Selected = true;
        //    CommonCode.getCountry_Details(2, "", bill_dd_State.SelectedValue, bill_dd_City);
        //    bill_dd_State.Enabled = false;
        //    bill_dd_State.CssClass = "form-control";
        //    bill_dd_City.Items.FindByText("Others").Selected = true;
        //    bill_dd_City.Enabled = false;
        //    bill_dd_City.CssClass = "form-control";
        //    updateShippingAmount( hf_cartID.Value, "102");
        //    div_paypal.Visible = true;
        //}
        //else
        //{
        //    bill_dd_State.Enabled = true;
        //    bill_dd_State.CssClass = "form-control";
        //    bill_dd_City.Items.Clear();
        //    bill_dd_City.Enabled = true;
        //    bill_dd_City.CssClass = "form-control";
        //    div_paypal.Visible = false;
        //}
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


    private void register_user(string CustName, string UserPassword, string BillingAddress, string BillingCityID, string BillingPostalCode, string BillingPhone, string Mobile, string EmailID, string Remark,
        string CreatedBy, string UpdatedBy, String UserType, String iCompanyID, String iBranchID, String ActionType, String schoolName, String ClassYear, String studentStream, String OtherInfo, out string  errmsg)
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
        com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = Session["CustID"].ToString();
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
        string AddressID_output = "";

        /* Update Customer Information*/
        if(bill_textName.Text.Trim() == "")
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
        register_user(bill_textName.Text.Trim(),"",bill_textAdress.Text.Trim(),bill_dd_City.SelectedValue,bill_textZipCode.Text.Trim(),bill_textPhone.Text.Trim(), bill_textPhone.Text.Trim(),bill_textEmailID.Text.Trim(),"","","", "Individual", 
            Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(),"1","","","","", out Custerrmsg);
        if(Custerrmsg != "Record Updated Successfully!")
        {
            CommonCode.show_alert("danger", "Error while updating your info", Custerrmsg, ltr_bill_msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Error while updating your info');", true);
            return;
        }
        if(chkAcceptTerms.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please accept Terms and Conditions for placing order');", true);
            return;
        }

        if (cb_copy_bill_to_ship.Checked)
        {
            DAL dal = new DAL();
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
                DAL dal = new DAL();
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

        if (rb_cod.Checked)
        {
        }
        else if (rb_online_pay.Checked)
        {
            string shf_total_amount = hf_total_amount.Value;
                
            string CheckoutID_output = "" , errmsg = "";
            string sess = Session["CustID"].ToString() , cartid = hf_cartID.Value;
            String OtherCountry = "";
            if (Session["OtherCountry"] != null)
            {
                OtherCountry = Session["OtherCountry"].ToString();
            }
            else
            {
                OtherCountry = "";
            }
            DAL dal = new DAL();
            dal.insert_update_delete_Checkout("0", Session["CustID"].ToString(), hf_cartID.Value, AddressID_output, 0, "Payment Started",  0, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out CheckoutID_output, out errmsg);

            if (errmsg == "success")
             {
                DataTable dt_Payment_Variables = new DataTable();
                dt_Payment_Variables.Columns.AddRange(new DataColumn[] { new DataColumn("Amount",typeof(float)), new DataColumn("FirstName") , new DataColumn("LastName") ,
                new DataColumn("Email") ,new DataColumn("Phone") , new DataColumn("Address") , new DataColumn("City"),new DataColumn("State") , new DataColumn("Country"),
                new DataColumn("Zip") , new DataColumn("Remarks"),new DataColumn("CheckoutID"), new DataColumn("UDF1_Currency") ,new DataColumn("UDF2_CurrencySymbol"),
                new DataColumn("UDF3_TotalItems"),new DataColumn("UDF4_ProductInfo"), new DataColumn("ShippingAmount"), new DataColumn("courier"), new DataColumn("ShipToAccountID") });
                errmsg = "";
                SqlCommand com = new SqlCommand("Web_Get_Cart", CommonCode.con);
                com.CommandType = CommandType.StoredProcedure;                      
                com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = Session["CustID"].ToString();
                com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
                com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
                com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
                DataTable dt = new DataTable();
                dt = CommonCode.getData(com, out errmsg);
                if (errmsg == "success")
                {
                    if (dt.Rows.Count > 0)
                    {
                    float net_amount = 0, ship_Amt = 0;
                    float.TryParse(dt.Rows[0]["NetAmount"].ToString(), out net_amount);
                    float.TryParse(dt.Rows[0]["ShipCost"].ToString(), out ship_Amt);
                    hf_total_amount.Value = string.Format(CommonCode.AmountFormat(), net_amount.ToString());                                                        
                    Boolean courier;
                    if (rb_ship_method.SelectedValue == "1")
                    {
                        courier = true;
                    }
                    else
                    {
                        courier = false;
                    }

                    if (Session["OtherCountry"] != null && Session["OtherCountry"].ToString() == "OtherCountry")
                    {                             
                        dt_Payment_Variables.Rows.Add(hf_total_amount.Value, bill_textName.Text, "", bill_textEmailID.Text, bill_textPhone.Text, bill_textAdress.Text,
                        bill_dd_City.SelectedItem.Text, bill_dd_State.SelectedItem.Text, bill_dd_Country.SelectedItem.Text, bill_textZipCode.Text, "CheckoutID" + CheckoutID_output,
                        CheckoutID_output, "USD", "USD", hf_totalitems.Value, hf_productinfo.Value, ship_Amt, courier,AddressID_output.ToString());
                        Session["Payment_Variables"] = dt_Payment_Variables;
                        if (rbt_CCAvenue.Checked == true)
                        {
                            Response.Redirect("gateway.aspx");
                        }
                        else
                        {
                            Response.Redirect("gateway_paypal.aspx");
                        }
                                
                    }
                    else
                    {
                        dt_Payment_Variables.Rows.Add(hf_total_amount.Value, bill_textName.Text, "", bill_textEmailID.Text, bill_textPhone.Text, bill_textAdress.Text,
                        bill_dd_City.SelectedItem.Text, bill_dd_State.SelectedItem.Text, bill_dd_Country.SelectedItem.Text, bill_textZipCode.Text, "CheckoutID" + CheckoutID_output,
                        CheckoutID_output, CommonCode.AppSettings("Currency"), CommonCode.AppSettings("CurrencySymbol"), hf_totalitems.Value, hf_productinfo.Value, ship_Amt, courier, AddressID_output.ToString());
                        Session["Payment_Variables"] = dt_Payment_Variables;
                            if(ConfigurationManager.AppSettings["PayMode"].ToString() == "COD")
                            {
                                Response.Redirect("PlaceOrder_withoutpay.aspx");
                            }
                            else
                            {
                                Response.Redirect("gateway.aspx");
                            }
                        
                    }
                    }
                        else
                    {
                        CommonCode.show_toastr("error", "Error while processing checkout and may be your is empty !", errmsg.Replace('\'', ' '), false, "", false, "", ltr_scripts);
                        return;
                    }

                  }
                    /*
                        // Last Update before 10/06/2019
                       dt_Payment_Variables.Rows.Add(hf_total_amount.Value, bill_textName.Text, "", bill_textEmailID.Text, bill_textPhone.Text, bill_textAdress.Text,
                           bill_dd_City.SelectedItem.Text, bill_dd_State.SelectedItem.Text, bill_dd_Country.SelectedItem.Text, bill_textZipCode.Text, "CheckoutID" + CheckoutID_output,
                           CheckoutID_output, CommonCode.AppSettings("Currency"), CommonCode.AppSettings("CurrencySymbol"), hf_totalitems.Value, hf_productinfo.Value);
                   */

                    // Updated : 11/06/2019
                    // Changed the amounts fields according to the user country
                    // If India Selected , payment will be done in INR otherwise in USD 

                        //Session["Payment_Variables"] = dt_Payment_Variables;

                        //Response.Redirect("gateway.aspx");
                }
                else
                {
                    CommonCode.show_toastr("error", "Error while processing checkout and payment !", errmsg.Replace('\'', ' '), false, "", false, "", ltr_scripts);
                    return;
                }
            }
        //}
        //else
        //{
        //    if (!cond3)
        //    {
        //        CommonCode.show_alert("danger", "Shipping Address Not Complete !", " Country Not Selected ", ltr_bill_msg);
        //    }
        //    else if (!cond2)
        //    {
        //        CommonCode.show_alert("danger", "Shipping Address Not Complete !", " State Not Selected ", ltr_bill_msg);
        //    }
        //    else
        //    {
        //        CommonCode.show_alert("danger", "Shipping Address Not Complete !", " City Not Selected ", ltr_bill_msg);
        //    }
        //}
    }


    private void updateShippingAmount(string cartID, string CityID)
    {
        try
        { 
            //decimal weight = 0;
            //foreach (RepeaterItem i in rp_cart.Items)
            //{
            //    string weights = "0";
            //    Label lblweight = (Label)i.FindControl("lblWeight");
            //    if (lblweight.Text.Trim() == "")
            //    {
            //        weights = "0";
            //    }
            //    else
            //    {
            //        weights = lblweight.Text.Trim();
            //    }

            //    if (lblweight != null)
            //    {
            //        weight += Convert.ToDecimal(lblweight.Text);
            //    }
            //}
            string errmsg = "";
            string report = "";
            SqlCommand com = new SqlCommand("[web_update_Cart_ShippingAmount_BasedOnCity]", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CartID", SqlDbType.BigInt).Value = cartID;
            com.Parameters.Add("@cityID", SqlDbType.VarChar,10).Value = CityID;
            com.Parameters.Add("@iCompanyId", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
            com.Parameters.Add("@iBranchId", SqlDbType.Int).Value = Session["iBranchID"].ToString();
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                load_cart_products();
            }
            else
                report += "<li>" + errmsg + "</li>";
        }
        catch (Exception ex) { throw (ex); }
    }

    public void clear()
    {
        bill_dd_Country.SelectedValue = null;
        bill_dd_State.SelectedValue = null;
        bill_dd_City.SelectedValue = null;
        bill_dd_State.Items.Clear();
        bill_dd_City.Items.Clear();
    }

    // --------------------- Updated : 28/06/2019 --------------------------------- //

    protected void rb_Ship_Method_Changed(object sender, EventArgs e)
    {
        updateShippingAmount(hf_cartID.Value, bill_dd_State.SelectedValue);
    }


    protected void bill_dd_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        updateShippingAmount(hf_cartID.Value, bill_dd_City.SelectedValue);
    }

    protected void rb_ship_here_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rb1 = (RadioButton)sender;
       
        foreach (RepeaterItem ri in rp_ship_addresses.Items)
        {
            RadioButton rb_ship_here = ri.FindControl("rb_ship_here") as RadioButton;
            if(rb_ship_here !=null && rb_ship_here.Checked)
            {
                rb_ship_here.Checked = false;
            }
        }
        rb1.Checked = true;
    }
}