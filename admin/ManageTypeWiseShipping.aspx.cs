using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Area/Location Wise Shipping");

        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                string CountryID = "", StateID = "", CityID = "", AreaShipAmountID = "0", AreaID = "";
                //dd_State.Items.Clear();
                //CommonCode.getCountry_Details(0, "", "", dd_Country);

                //if (!string.IsNullOrEmpty(Request.QueryString["cityid"]))
                //{
                //    CityID = Request.QueryString["cityid"];
                //}
                if (!string.IsNullOrEmpty(Request.QueryString["AreaID"]))
                {
                    AreaID = Request.QueryString["AreaID"];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["AreaShipAmountID"]))
                {
                    AreaShipAmountID = Request.QueryString["AreaShipAmountID"];
                }
                //if (!string.IsNullOrEmpty(Request.QueryString["countryid"]))
                //{
                //    CountryID = Request.QueryString["countryid"];
                //    if (!string.IsNullOrEmpty(Request.QueryString["countryid"]))
                //    {
                //        StateID = Request.QueryString["stateid"];
                //        set_country_state_city(CountryID, StateID, CityID);
                //    }
                //}
                //if (!string.IsNullOrEmpty(AreaID))
                //{
                //    get_AreaWiseShippingAmount(AreaID, AreaShipAmountID);
                //}
                get_AreaWiseShippingAmount(AreaID, AreaShipAmountID);
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }


    }

    private void set_country_state_city(string CountryID, string StateID, string CityID)
    {
        dd_City.AutoPostBack = false;
        dd_Country.SelectedIndex = dd_Country.Items.IndexOf(dd_Country.Items.FindByValue(CountryID));
        dd_State.Items.Clear();
        CommonCode.getCountry_Details(1, CountryID, "", dd_State);
        dd_State.SelectedIndex = dd_State.Items.IndexOf(dd_State.Items.FindByValue(StateID));
        dd_City.Items.Clear();
        CommonCode.getCountry_Details(2, "", StateID, dd_City);
        dd_City.SelectedIndex = dd_City.Items.IndexOf(dd_City.Items.FindByValue(CityID));
        dd_City.AutoPostBack = true;
    }

    private void get_AreaWiseShippingAmount(string AreaID, string AreaShipAmountID)
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_AreaWiseShippingCharges(AreaShipAmountID, AreaID, Session["iCompanyID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["AreaShipAmountID"]))
                {
                    //string CountryID = "", StateID = "";
                    //CountryID = dt.Rows[0]["CountryID"].ToString();
                    //StateID = dt.Rows[0]["StateID"].ToString();
                    //AreaID = dt.Rows[0]["CityID"].ToString();
                    //set_country_state_city(CountryID, StateID, AreaID);
                    drp_AreaType.SelectedIndex = drp_AreaType.Items.IndexOf(drp_AreaType.Items.FindByValue(AreaID));
                    hf_AreaShipAmountID.Value = AreaShipAmountID;
                    textFromRate.Text = dt.Rows[0]["FromRate"].ToString();
                    textToRate.Text = dt.Rows[0]["ToRate"].ToString();
                    textShippingAmount.Text = dt.Rows[0]["ShippingAmount"].ToString();
                }
                grd_shippingamount.DataSource = dt;
                grd_shippingamount.DataBind();
            }
            else
            {
                grd_shippingamount.DataSource = null;
                grd_shippingamount.DataBind();
                CommonCode.show_alert("warning", "Shipping Amount Details Not Found !", "", ph_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Shipping Amount Details", errmsg, ph_msg);
        }
    }

    protected void dd_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_State.Items.Clear();
        CommonCode.getCountry_Details(1, dd_Country.SelectedValue, "", dd_State);
    }

    protected void dd_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        dd_City.Items.Clear();
        CommonCode.getCountry_Details(2, "", dd_State.SelectedValue, dd_City);
    }

    

    protected void dd_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CountryID, StateID, CityID = "", CityShipAmountID = "0";
        CountryID = dd_Country.SelectedValue;
        StateID = dd_State.SelectedValue;
        CityID = dd_City.SelectedValue; 
        Response.Redirect(string.Format("ManageTypeWiseShipping.aspx?countryid={0}&stateid={1}&cityid={2}", CountryID, StateID, CityID));
    }
     

    protected void grd_shippingamount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ShipID = e.CommandArgument.ToString();

        if (e.CommandName == "delete_topic")
        {
            string errmsg = "", action_msg = "delet";
            DAL dal = new DAL();
            dal.insert_update_delete_AreaWiseShippingCharges(ShipID, "", Session["iCompanyID"].ToString(), 0, 0, 0, 2, out errmsg);
            if (errmsg == "success")
            {
                CommonCode.show_alert("success", "Shipping amount '<em>" + textShippingAmount.Text + "</em>' " + action_msg + "ed successfully !", "", ph_msg);
                Response.Redirect("ManageTypeWiseShipping.aspx", true);
            }
            else
            {
                CommonCode.show_alert("danger", "Error while " + action_msg + "ing shipping amount.", errmsg, ph_msg);
            }
        }
    }

    protected void drp_AreaType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string AreaID = "";
        //CountryID = dd_Country.SelectedValue;
        //StateID = dd_State.SelectedValue;
        //CityID = dd_City.SelectedValue;
        AreaID = drp_AreaType.SelectedValue;
        get_AreaWiseShippingAmount(AreaID, "0");
        //Response.Redirect(string.Format("ManageTypeWiseShipping.aspx?AreaID={0}", AreaID));
    }

    protected void btnsaveshipping_Click(object sender, EventArgs e)
    {
        string errmsg = "";
        DAL dal = new DAL();
        float FromRate, ToRate, ShippingAmount;
        if (drp_AreaType.SelectedValue == "Nil")
        {
            CommonCode.show_alert("warning", "Area/Location is not valid!", "Please Select valid Area/Location", ph_msg);
            return;
        }
        if (!float.TryParse(textFromRate.Text, out FromRate))
        {
            CommonCode.show_alert("warning", "From Weight was not valid!", "Please enter valid weight", ph_msg);
            return;
        }
        if (!float.TryParse(textToRate.Text, out ToRate))
        {
            CommonCode.show_alert("warning", "To Weight was not valid!", "Please enter valid weight", ph_msg);
            return;
        }
        if (!float.TryParse(textShippingAmount.Text, out ShippingAmount))
        {
            CommonCode.show_alert("warning", "Shipping Amount was not valid!", "Please enter valid amount", ph_msg);
            return;
        }


        string action_mode = "";
        if (!string.IsNullOrEmpty(Request.QueryString["AreaShipAmountID"]))
        {
            dal.insert_update_delete_AreaWiseShippingCharges(hf_AreaShipAmountID.Value, drp_AreaType.SelectedValue, Session["iCompanyID"].ToString(), FromRate, ToRate, ShippingAmount, 1, out errmsg);
            action_mode = "updat";
        }
        else
        {
            dal.insert_update_delete_AreaWiseShippingCharges("0", drp_AreaType.SelectedValue, Session["iCompanyID"].ToString(), FromRate, ToRate, ShippingAmount, 0, out errmsg);
            action_mode = "sav";
        }
        if (errmsg == "success")
        {
            Response.Redirect("ManageTypeWiseShipping.aspx", true);
        }
        else
        {
            CommonCode.show_alert("danger", "Error while " + action_mode + "ing Shipping Amount Details", errmsg, ph_msg);
        }
    }

    protected void btn_save_Shipping_Click(object sender, EventArgs e)
    {

    }
}