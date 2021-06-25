using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_alert_msg.Text = "";
        ltr_scripts.Text = "";
        if (Session["tmpcart"] != null)
        {
            if (!IsPostBack)
            {
                CommonCode.getCountry_Details(0, "0", "0", dd_bill_country);
                CommonCode.getCountry_Details(0, "0", "0", dd_ship_country);
                CommonCode.show_alert("success", "Provide your details to place order.", "", false, ltr_alert_msg);
            }
        }
        else
        {
            CommonCode.getCountry_Details(0, "0", "0", dd_bill_country);
            CommonCode.getCountry_Details(0, "0", "0", dd_ship_country);
            CommonCode.show_alert("danger", "Session Expired !", "", false, ltr_alert_msg);
            //error --> invalid page entry or Session Expired
        }
    }


    public void load_temp_cart()
    {
        DataTable dt_tmp_cart = new DataTable();
        if (Session["tmpcart"] != null)
        {
            DataTable ddt = new DataTable();
            SqlCommand com1 = new SqlCommand("select 'tmpcart' CartID, mp.ProductID ,mp.ISBN, mp.BookName, mp.SaleCurrency,mp.SalePrice Price, " +
                " 0 Qty, 0 TotalAmt from MasterProduct mp  where 1=2", CommonCode.con);
            string errmsg;
            ddt = CommonCode.getData(com1, out errmsg);
            dt_tmp_cart = Session["tmpcart"] as DataTable;

            if (dt_tmp_cart.Rows.Count > 0)
            {
                foreach (DataRow rr in dt_tmp_cart.Rows)
                {
                    DataTable dt = new DataTable();
                    SqlCommand com = new SqlCommand("dbo_get_product_detail_for_tmpcart", CommonCode.con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = rr["ProductID"];
                    com.Parameters.Add("@Qty", SqlDbType.Int).Value = rr["Qty"];
                    dt = CommonCode.getData(com, out errmsg);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow r1 = dt.Rows[0];
                        ddt.Rows.Add(r1.ItemArray);
                    }
                }
                if (ddt.Rows.Count > 0)
                {
                    float total_amount = 0;
                    foreach (DataRow rr1 in ddt.Rows)
                    {
                        total_amount += float.Parse(rr1["TotalAmt"].ToString());
                    }

                }
            }
            else
            {
                CommonCode.show_alert("danger", "Your Cart is Empty", "<a href='/index.aspx' class='btn btn-link'>Click here select products</a>", false, ltr_alert_msg);
            }
        }
    }


    protected void btn_place_order_Click(object sender, EventArgs e)
    {
        String errmsg = "success";

        DataTable dt_tmp_cart = new DataTable();
        dt_tmp_cart = (DataTable)Session["tmpcart"];


        String BillAddress = text_bill_Address.Text,
        BillCityID = dd_bill_City.SelectedValue,
        BillPostalCode = text_bill_Pincode.Text,
        BillMobile = text_bill_Phone.Text,
        BillEmail = text_bill_Email.Text;


        SqlCommand com = new SqlCommand("dbo_insert_in_orders", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CustID", SqlDbType.BigInt).Value = "GuestUser";

        if (cb_ship_to_bill_addr.Checked)
        {
            com.Parameters.Add("@ShipAddress", SqlDbType.NVarChar).Value = BillAddress;
            com.Parameters.Add("@ShipCityID", SqlDbType.NVarChar).Value = BillCityID;
            com.Parameters.Add("@ShipPostalCode", SqlDbType.NVarChar).Value = BillPostalCode;
            com.Parameters.Add("@ShipPhone", SqlDbType.NVarChar).Value = BillMobile;
            com.Parameters.Add("@ShipFaxNo", SqlDbType.NVarChar).Value = "";
        }
        else
        {
            com.Parameters.Add("@ShipAddress", SqlDbType.NVarChar).Value = text_ship_Address.Text;
            com.Parameters.Add("@ShipCityID", SqlDbType.NVarChar).Value = dd_ship_City.SelectedValue;
            com.Parameters.Add("@ShipPostalCode", SqlDbType.NVarChar).Value = text_ship_Pincode.Text;
            com.Parameters.Add("@ShipPhone", SqlDbType.NVarChar).Value = text_ship_Phone.Text;
            com.Parameters.Add("@ShipFaxNo", SqlDbType.NVarChar).Value = "";
        }

        com.Parameters.Add("@BillAddress", SqlDbType.NVarChar).Value = BillAddress;
        com.Parameters.Add("@BillingCityID", SqlDbType.NVarChar).Value = BillCityID;
        com.Parameters.Add("@BillPostalCode", SqlDbType.NVarChar).Value = BillPostalCode;
        com.Parameters.Add("@BillingPhone", SqlDbType.NVarChar).Value = BillMobile;
        com.Parameters.Add("@BillingFaxNo", SqlDbType.NVarChar).Value = "";

        //com.Parameters.Add("@TotalAmount", SqlDbType.Float).Value = Convert.ToInt32(hf_total_amount.Value);
        com.Parameters.Add("@Mobile", SqlDbType.VarChar, 10).Value = BillMobile;
        com.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = BillEmail;
        com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
        com.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = "-";
        com.Parameters.Add("@CartID", SqlDbType.BigInt).Value = "tmpcart";
        com.Parameters.Add("@OrderID", SqlDbType.BigInt);
        com.Parameters["@OrderID"].Direction = ParameterDirection.Output;

        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        if (errmsg == "success")
        {
            if (!string.IsNullOrEmpty(com.Parameters["@OrderID"].Value.ToString()))
            {
                //save_order_details(com.Parameters["@OrderID"].Value.ToString(), hf_cartID.Value);
                //ltr_scripts.Text = "<script>toastr.success('Your Order of amount INR " + hf_total_amount.Value + " has been successfuly placed ! ', 'Order Placed');</script>";
                Response.Redirect("order_history.aspx", true);
            }
            else
            {
                CommonCode.show_toastr("error", "Order Error !", "Failed to save your Order. More Info - Blank Order", false, "", false, "", ltr_scripts);
            }
        }
        else
        {
            CommonCode.show_toastr("error", "Order Error !", errmsg.Replace('\'', ' '), false, "", false, "", ltr_scripts);
        }
    }


    private void save_order_details(String OrderID, String CartID)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_insert_in_order_details", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CartID", SqlDbType.BigInt).Value = CartID;
        com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = OrderID;
        com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        if (errmsg == "success")
        {

        }
        else
        {
            CommonCode.show_toastr("error", "Order Error !", errmsg.Replace('\'', ' '), false, "", false, "", ltr_scripts);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("/", true);
    }

    protected void dd_bill_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_bill_State.Items.Clear();
        CommonCode.getCountry_Details(1, dd_bill_country.SelectedValue, "0", dd_bill_State);
    }

    protected void dd_bill_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_bill_City.Items.Clear();
        CommonCode.getCountry_Details(2, "0", dd_bill_State.SelectedValue, dd_bill_City);
    }

    protected void dd_ship_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_ship_State.Items.Clear();
        CommonCode.getCountry_Details(1, dd_ship_country.SelectedValue, "0", dd_ship_State);
    }

    protected void dd_ship_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_ship_City.Items.Clear();
        CommonCode.getCountry_Details(2, "0", dd_ship_State.SelectedValue, dd_ship_City);
    }

}

