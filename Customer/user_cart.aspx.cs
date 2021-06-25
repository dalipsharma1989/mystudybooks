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
        ltr_msg.Text = "";
        ltr_scripts.Text = "";
        this.Title = CommonCode.SetPageTitle("Cart");
        if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
        {
            if (Session["tmpcart"] != null)
            {
                if (!IsPostBack)
                {
                    load_temp_cart();
                }
            }
            else
            {
                Response.Redirect("user_login.aspx", true);
            } 
        }
        else
        {
            if (!IsPostBack)
            {
                load_cart_products();
            }
        }
    }

    public void load_cart_products()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        String OtherCountry = "";
        if (Session["OtherCountry"] != null)
        {
            OtherCountry = Session["OtherCountry"].ToString();
        }
        else
        {
            OtherCountry = "";
        }

        dt = dal.get_curr_cart(OtherCountry, Session["CustID"] + "", Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            div_user_cart.Visible = true;
            if (dt.Rows.Count > 0)
            {
                float net_amount = 0, tot_amount = 0, tot_qty = 0;

                float.TryParse(dt.Rows[0]["NetAmount"].ToString(), out net_amount);

                foreach (DataRow rr in dt.Rows)
                {
                    tot_amount += float.Parse(rr["TotalAmt"] + "");
                    tot_qty += float.Parse(rr["Qty"] + "");
                }

                hf_cartID.Value = dt.Rows[0]["CartID"].ToString();

                span_total_amt.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + string.Format(CommonCode.AmountFormat(), tot_amount) + "";

                double tot_ship_cost = Convert.ToDouble(dt.Rows[0]["ShipCost"]);

                if (tot_ship_cost == 0)
                {
                    shipping_amt.InnerHtml = "Free";
                }
                else
                {
                    shipping_amt.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + string.Format(CommonCode.AmountFormat(), tot_ship_cost) + "";
                }
                
                
                td_total_amount.InnerHtml = "<span id='ttl-amt' class='ttl-amt pulsor'>" + ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + string.Format(CommonCode.AmountFormat(), net_amount) + "</span>";
                hf_total_amount.Value = net_amount.ToString();
                rp_cart.DataSource = dt;
                rp_cart.DataBind();
                rp_MobileCart.DataSource = dt;
                rp_MobileCart.DataBind();
            }
            else
            {
                div_user_cart.Visible = false;
                CommonCode.show_alert("info", "Your Cart is Empty", "<a href='/index.aspx' class='btn btn-link'>Click here select products</a>", ltr_msg);
                span_total_amt.InnerText = "0.00";
                shipping_amt.InnerText = "0.00";
                td_total_amount.InnerText = "0.00";
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Cart", errmsg, ltr_msg);
        }
    }

    public void load_temp_cart()
    {
        DataTable dt_tmp_cart = new DataTable();
        String OtherCountry = "";
        if (Session["OtherCountry"] != null)
        {
            OtherCountry = Session["OtherCountry"].ToString();
        }
        else
        {
            OtherCountry = "";
        } 

        if (Session["tmpcart"] != null)
        {
            DataSet ddt = new DataSet();
            SqlCommand com1 = new SqlCommand("web_Get_tmpCart_Structure", CommonCode.con);

            string errmsg;
            ddt = CommonCode.getDataInDataSet(com1, out errmsg);
            dt_tmp_cart = HttpContext.Current.Session["tmpcart"] as DataTable;
            div_user_cart.Visible = true;
            if (dt_tmp_cart.Rows.Count > 0)
            {
                foreach (DataRow rr in dt_tmp_cart.Rows)
                {
                    DataTable dt = new DataTable();
                    SqlCommand com = new SqlCommand("Web_Get_Product_Detail_For_Tmpcart", CommonCode.con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@ProductID", SqlDbType.VarChar,20).Value = rr["ProductID"];
                    com.Parameters.Add("@Qty", SqlDbType.Int).Value = rr["Qty"];
                    com.Parameters.Add("@OtherCountry", SqlDbType.VarChar, 100).Value = OtherCountry;
                    com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyID"].ToString();
                    com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
                    dt = CommonCode.getData(com, out errmsg);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow r1 = dt.Rows[0];
                        ddt.Tables[0].Rows.Add(r1.ItemArray);
                    }
                }
                if (ddt.Tables[0].Rows.Count > 0)
                {
                    float net_amount = 0, tot_amount = 0,tot_qty = 0;

                    foreach (DataRow rr1 in ddt.Tables[0].Rows)
                    {
                        tot_amount += float.Parse(rr1["TotalAmt"] + "");
                        tot_qty += float.Parse(rr1["Qty"]+"");
                    }
                    
                    float ShipAmount = 0, min_amount_for_free_ship = 0;

                     
                    net_amount = tot_amount + (ShipAmount * tot_qty );


                    span_total_amt.InnerHtml = ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + string.Format(CommonCode.AmountFormat(), tot_amount) + "";

                    if (ShipAmount == 0)
                    {
                        td_subtotal.Visible = false;
                        td1.Visible = false;
                    }
                    else
                    {
                        td_subtotal.Visible = true;
                        td1.Visible = true;
                        shipping_amt.InnerHtml = "+" + ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + string.Format(CommonCode.AmountFormat(), (ShipAmount * tot_qty)) + "";
                    }
                    

                    td_total_amount.InnerHtml = "<span id='ttl-amt' class='ttl-amt pulsor'>" + ConfigurationManager.AppSettings["CurrencySymbol"].ToString() + " " + string.Format(CommonCode.AmountFormat(), net_amount) + "</span>";
                    hf_cartID.Value = ddt.Tables[0].Rows[0]["CartID"].ToString();
                    hf_total_amount.Value = net_amount.ToString();
                    rp_cart.DataSource = ddt.Tables[0];
                    rp_cart.DataBind();
                    rp_MobileCart.DataSource = ddt.Tables[0];
                    rp_MobileCart.DataBind();
                }
            }
            else
            {
                div_user_cart.Visible = false;
                CommonCode.show_alert("danger", "Your Cart is Empty", "<a href='/index.aspx' class='btn btn-link'>Click here select products</a>", false, ltr_msg);
            }
        }
        else
        {
            div_user_cart.Visible = false;
            CommonCode.show_alert("danger", "Your Cart is Empty", "<a href='/index.aspx' class='btn btn-link'>Click here select products</a>", false, ltr_msg);
        }
    }

    protected void btn_update_cart_Click(object sender, EventArgs e)
    {
        String errmsg = "";
        String CartID = hf_cartID.Value;
        int total_items_in_rp_cart = rp_cart.Items.Count;
        int success_items = 0;
        string report = "";
        foreach (RepeaterItem item in rp_cart.Items)
        {
            int qty = 1;
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hf_ProductID = item.FindControl("hf_ProductID") as HiddenField;
                HiddenField hf_ISBN = item.FindControl("hf_ISBN") as HiddenField;
                HiddenField hf_BookName = item.FindControl("hf_BookName") as HiddenField;
                HiddenField hf_Clbal = item.FindControl("hf_Clbal") as HiddenField;
                HiddenField hf_discount = item.FindControl("hf_discount") as HiddenField;

                TextBox textQuantity = item.FindControl("textQuantity") as TextBox;
                if (!string.IsNullOrEmpty(textQuantity.Text))
                    qty = Convert.ToInt32(textQuantity.Text);

                if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
                {
                    if (Session["tmpcart"] != null)
                    {
                        int Clbal = 0;
                        int.TryParse(hf_Clbal.Value, out Clbal);                        
                        if (qty <= Clbal)
                        {
                            update_tmp_cart(qty, hf_ProductID.Value);
                            success_items += 1;
                            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Qty successfully updated into cart');", true);
                            ltr_scripts.Text = "<script>toastr.success('Qty successfully updated into cart ! ', '"+ hf_BookName.Value + "');</script>";
                        }
                        else
                        {
                            report += "<li>" + hf_BookName.Value + " - " + "Please select a lesser quantity" + "</li>";
                        }
                    }
                }
                else
                {

                    int Clbal = 0;
                    int.TryParse(hf_Clbal.Value, out Clbal);

                    if (qty <= Clbal)
                    {                        
                        DAL dal = new DAL();
                        dal.update_Cart_QTY(qty, CartID, hf_ProductID.Value, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);
                        if (errmsg == "success")
                        {
                            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Qty successfully updated into cart');", true);
                            ltr_scripts.Text = "<script>toastr.success('Qty successfully updated into cart ! ', '" + hf_BookName.Value + "');</script>";
                            success_items += 1;
                        }
                        else
                        {
                            report += "<li>" + hf_BookName.Value + " - " + errmsg + "</li>";
                        }
                    }
                    else
                    {
                        report += "<li>" + hf_BookName.Value + " - " + "Please select a lesser quantity" + "</li>";
                    }

                    

                    //SqlCommand com = new SqlCommand("web_update_cart_qty", CommonCode.con);
                    //com.CommandType = CommandType.StoredProcedure;
                    //com.Parameters.Add("@Qty", SqlDbType.Int).Value = qty;
                    //com.Parameters.Add("@CartID", SqlDbType.BigInt).Value = CartID;
                    //com.Parameters.Add("@ProductId", SqlDbType.BigInt).Value = hf_ProductID.Value;
                    //errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
                                            
                }
            }
        }

        if (report != "")
        {
            CommonCode.show_alert("danger", "Error Occured while updating Cart", "<ul>" + report + "</ul>", ltr_msg);
        }
        else
        { 
            if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
            {
                if (Session["tmpcart"] != null)
                {
                    load_temp_cart();
                }                    
            }
            else
            {
                load_cart_products();
            }                
            ltr_scripts.Text = "<script>toastr.success('" + success_items + " out of " + total_items_in_rp_cart + " items updated successfuly ! ', 'Cart Updated !');</script>";
            CART_TOTALS.Update();
        }
        Master.load_Header();
    }

    private void update_tmp_cart(int Qty, String ProductID)
    {
        DataTable dt = new DataTable();

        if (Session["tmpcart"] != null)
        {
            dt = Session["tmpcart"] as DataTable;

            DataRow[] foundRows = dt.Select("ProductID = '"+ ProductID + "'");
            if (foundRows.Length > 0)
            {
                foundRows[0]["Qty"] = Qty;
            }
            Session["tmpcart"] = dt;
        }
        else
        {
            //empty session
        }
    }

    protected void rp_cart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String errmsg = "success";
        String ProductID = e.CommandArgument.ToString();
        if (e.CommandName == "remove_item_from_cart")
        {
            if (!string.IsNullOrEmpty(hf_cartID.Value.ToString()))
            {
                if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
                {
                    errmsg = "Temp Cart Empty.";
                    if (Session["tmpcart"] != null)
                    {
                        DataTable dt = (DataTable)Session["tmpcart"];
                        DataRow[] foundRows = dt.Select("ProductID = '"+ ProductID + "'");
                        errmsg = "Product not found in cart.";
                        if (foundRows.Length > 0)
                        {
                            dt.Rows.Remove(foundRows[0]);
                            errmsg = "success";
                            ltr_scripts.Text = "<script>toastr.success('Item Removed from cart ! ', 'Item Removed');</script>";
                            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item deleted successfully from your cart');", true);
                            load_temp_cart();
                        }
                        if (dt.Rows.Count == 0)
                        {
                            Session["tmpcart"] = null;
                            load_temp_cart();
                            ltr_scripts.Text = "<script>toastr.success('Item Removed from cart ! ', 'Item Removed');</script>";
                            CART_TOTALS.Update();
                            return;
                        }
                    }
                }
                else
                {
                    SqlCommand com = new SqlCommand("Web_delete_item_from_cart", CommonCode.con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@ProductID", SqlDbType.VarChar,20).Value = ProductID;
                    com.Parameters.Add("@CartID", SqlDbType.BigInt).Value = hf_cartID.Value;
                    com.Parameters.Add("@iCompanyId", SqlDbType.Int).Value = Session["iCompanyID"].ToString();
                    com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
                    errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
                    if (errmsg == "success")
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item deleted successfully from your cart');", true);
                        load_cart_products();
                        ltr_scripts.Text = "<script>toastr.success('Item Removed from cart ! ', 'Item Removed');</script>";
                        CART_TOTALS.Update();
                    }
                    else
                    {
                        ltr_scripts.Text = "<script>toastr.error('" + errmsg.Replace('\'', ' ') + "', 'Cart Error !');</script>";
                    }
                }

            }
        }

        //(this.Master as CustomerMaster).load_Header();
        Master.load_Header();
    }

    protected void btn_proceed_to_checkout_Click(object sender, EventArgs e)
    {

        String errmsg = "";
        String CartID = hf_cartID.Value;
        int total_items_in_rp_cart = rp_cart.Items.Count; 
        string report = "";
        foreach (RepeaterItem item in rp_cart.Items)
        {
            int qty = 1;
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hf_ProductID = item.FindControl("hf_ProductID") as HiddenField;
                HiddenField hf_ISBN = item.FindControl("hf_ISBN") as HiddenField;
                HiddenField hf_BookName = item.FindControl("hf_BookName") as HiddenField;

                HiddenField hf_Clbal = item.FindControl("hf_Clbal") as HiddenField;

                TextBox textQuantity = item.FindControl("textQuantity") as TextBox;
                if (!string.IsNullOrEmpty(textQuantity.Text))
                {
                    qty = Convert.ToInt32(textQuantity.Text);
                }  
                DAL dal = new DAL();
                dal.update_Cart_QTY(qty, CartID, hf_ProductID.Value, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg); 
                if (errmsg != "success")  
                {
                    report += "<li>" + hf_BookName.Value + " - " + errmsg + "</li>";
                } 
            }
        }

        if (report != "")
        {
            CommonCode.show_alert("danger", "Error Occured while updating Cart", "<ul>" + report + "</ul>", ltr_msg);
        }  

        if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
        {
            if (Session["tmpcart"] != null)
            {
                Response.Redirect("user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
            }
        }
        Response.Redirect("proceed_to_checkout.aspx?cartid=" + hf_cartID.Value, true);
    }

    protected void btn_Update_Cart_Mobile_Click(object sender, EventArgs e)
    {
        String errmsg = "";
        String CartID = hf_cartID.Value;
        int total_items_in_rp_cart = rp_MobileCart.Items.Count;
        int success_items = 0;
        string report = "";
        foreach (RepeaterItem item in rp_MobileCart.Items)
        {
            int qty = 1;
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hf_ProductID = item.FindControl("hf_ProductID") as HiddenField;
                HiddenField hf_ISBN = item.FindControl("hf_ISBN") as HiddenField;
                HiddenField hf_BookName = item.FindControl("hf_BookName") as HiddenField;
                HiddenField hf_Clbal = item.FindControl("hf_Clbal") as HiddenField;
                HiddenField hf_discount = item.FindControl("hf_discount") as HiddenField;

                TextBox textQuantity = item.FindControl("textQuantity") as TextBox;
                if (!string.IsNullOrEmpty(textQuantity.Text))
                    qty = Convert.ToInt32(textQuantity.Text);

                if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
                {
                    if (Session["tmpcart"] != null)
                    {
                        int Clbal = 0;
                        int.TryParse(hf_Clbal.Value, out Clbal);
                        if (qty <= Clbal)
                        {
                            update_tmp_cart(qty, hf_ProductID.Value);
                            success_items += 1;
                            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Qty successfully updated into cart');", true);
                            ltr_scripts.Text = "<script>toastr.success('Qty successfully updated into cart ! ', '" + hf_BookName.Value + "');</script>";
                        }
                        else
                        {
                            report += "<li>" + hf_BookName.Value + " - " + "Please select a lesser quantity" + "</li>";
                        }
                    }
                }
                else
                {

                    int Clbal = 0;
                    int.TryParse(hf_Clbal.Value, out Clbal);

                    if (qty <= Clbal)
                    {
                        DAL dal = new DAL();
                        dal.update_Cart_QTY(qty, CartID, hf_ProductID.Value, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);
                        if (errmsg == "success")
                        {
                            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Qty successfully updated into cart');", true);
                            ltr_scripts.Text = "<script>toastr.success('Qty successfully updated into cart ! ', '" + hf_BookName.Value + "');</script>";
                            success_items += 1;
                        }
                        else
                        {
                            report += "<li>" + hf_BookName.Value + " - " + errmsg + "</li>";
                        }
                    }
                    else
                    {
                        report += "<li>" + hf_BookName.Value + " - " + "Please select a lesser quantity" + "</li>";
                    }



                    //SqlCommand com = new SqlCommand("web_update_cart_qty", CommonCode.con);
                    //com.CommandType = CommandType.StoredProcedure;
                    //com.Parameters.Add("@Qty", SqlDbType.Int).Value = qty;
                    //com.Parameters.Add("@CartID", SqlDbType.BigInt).Value = CartID;
                    //com.Parameters.Add("@ProductId", SqlDbType.BigInt).Value = hf_ProductID.Value;
                    //errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);

                }
            }
        }

        if (report != "")
        {
            CommonCode.show_alert("danger", "Error Occured while updating Cart", "<ul>" + report + "</ul>", ltr_msg);
        }
        else
        {
            if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
            {
                if (Session["tmpcart"] != null)
                {
                    load_temp_cart();
                }
            }
            else
            {
                load_cart_products();
            }
            ltr_scripts.Text = "<script>toastr.success('" + success_items + " out of " + total_items_in_rp_cart + " items updated successfuly ! ', 'Cart Updated !');</script>";
            CART_TOTALS.Update();
        }
        Master.load_Header();
    }

    protected void rp_MobileCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String errmsg = "success";
        String ProductID = e.CommandArgument.ToString();
        if (e.CommandName == "remove_item_from_cart")
        {
            if (!string.IsNullOrEmpty(hf_cartID.Value.ToString()))
            {
                if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
                {
                    errmsg = "Temp Cart Empty.";
                    if (Session["tmpcart"] != null)
                    {
                        DataTable dt = (DataTable)Session["tmpcart"];
                        DataRow[] foundRows = dt.Select("ProductID = '" + ProductID + "'");
                        errmsg = "Product not found in cart.";
                        if (foundRows.Length > 0)
                        {
                            dt.Rows.Remove(foundRows[0]);
                            errmsg = "success";
                            ltr_scripts.Text = "<script>toastr.success('Item Removed from cart ! ', 'Item Removed');</script>";
                            //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item deleted successfully from your cart');", true);
                            load_temp_cart();
                        }
                        if (dt.Rows.Count == 0)
                        {
                            Session["tmpcart"] = null;
                            load_temp_cart();
                            ltr_scripts.Text = "<script>toastr.success('Item Removed from cart ! ', 'Item Removed');</script>";
                            CART_TOTALS.Update();
                            return;
                        }
                    }
                }
                else
                {
                    SqlCommand com = new SqlCommand("Web_delete_item_from_cart", CommonCode.con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@ProductID", SqlDbType.VarChar, 20).Value = ProductID;
                    com.Parameters.Add("@CartID", SqlDbType.BigInt).Value = hf_cartID.Value;
                    com.Parameters.Add("@iCompanyId", SqlDbType.Int).Value = Session["iCompanyID"].ToString();
                    com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
                    errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
                    if (errmsg == "success")
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item deleted successfully from your cart');", true);
                        load_cart_products();
                        ltr_scripts.Text = "<script>toastr.success('Item Removed from cart ! ', 'Item Removed');</script>";
                        CART_TOTALS.Update();
                    }
                    else
                    {
                        ltr_scripts.Text = "<script>toastr.error('" + errmsg.Replace('\'', ' ') + "', 'Cart Error !');</script>";
                    }
                }

            }
        } 
        Master.load_Header();
    }
}