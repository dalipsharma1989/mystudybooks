using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cityid"]) && !string.IsNullOrEmpty(Request.QueryString["action"]) && !string.IsNullOrEmpty(Request.QueryString["city_stateid"]))
            {
                // EDIT Current City
                // Show All Cities of city_state_id

                div_update_country.Visible = false;
                textCountryName.Visible = false;
                textCountryName.ReadOnly = true;

                div_form_grp_States.Visible = false;
                textStates.Visible = false;

                div_udpate_State.Visible = true;
                text_State_to_update.Visible = true;
                text_State_to_update.ReadOnly = true;

                div_form_grp_Cities.Visible = false;
                textCities.Visible = false;

                div_udpate_City.Visible = true;
                text_City_to_update.Visible = true;
                text_City_to_update.ReadOnly = false;

                div_grd_Countries.Visible = false;
                div_grd_states.Visible = false;
                div_grd_cities.Visible = false;

                btnSave.Visible = false;
                btn_update.Visible = true;

                load_single_details(Request.QueryString["cityid"], "5"); // RETURN SINGLE STATE DETAILS
                //load_all_Cities(Request.QueryString["city_stateid"]);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["stateid"]) && !string.IsNullOrEmpty(Request.QueryString["action"]) && !string.IsNullOrEmpty(Request.QueryString["state_countryid"]))
            {
                // EDIT Current State
                // Show All Cities of stateid
                // Add new Cities in stateid
                div_update_country.Visible = true;
                textCountryName.Visible = true;
                textCountryName.ReadOnly = true;

                div_form_grp_States.Visible = false;
                textStates.Visible = false;

                div_udpate_State.Visible = true;
                text_State_to_update.Visible = true;
                text_State_to_update.ReadOnly = false;

                div_form_grp_Cities.Visible = true;
                textCities.Visible = true;

                div_udpate_City.Visible = false;
                text_City_to_update.Visible = false;
                text_City_to_update.ReadOnly = true;

                div_grd_Countries.Visible = false;
                div_grd_states.Visible = false;
                div_grd_cities.Visible = true;

                btnSave.Visible = false;
                btn_update.Visible = true;

                load_single_details(Request.QueryString["stateid"], "3"); // RETURN SINGLE STATE DETAILS
                load_all_Cities(Request.QueryString["stateid"]);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["countryid"]) && !string.IsNullOrEmpty(Request.QueryString["action"]))
            {
                // EDIT Current Country
                // Show All States of countryid
                // Add new States in countryid

                div_update_country.Visible = true;
                textCountryName.Visible = true;
                textCountryName.ReadOnly = false;

                div_form_grp_States.Visible = true;
                textStates.Visible = true;

                div_udpate_State.Visible = false;
                text_State_to_update.Visible = false;
                text_State_to_update.ReadOnly = true;

                div_form_grp_Cities.Visible = false;
                textCities.Visible = false;

                div_udpate_City.Visible = false;
                text_City_to_update.Visible = false;
                text_City_to_update.ReadOnly = true;

                div_grd_Countries.Visible = false;
                div_grd_states.Visible = true;
                div_grd_cities.Visible = false;

                btnSave.Visible = false;
                btn_update.Visible = true;

                load_single_details(Request.QueryString["countryid"], "2"); // RETURN SINGLE COUNTRY DETAILS
                load_all_States(Request.QueryString["countryid"]);
            }
            else
            {
                div_update_country.Visible = true;
                textCountryName.Visible = true;
                textCountryName.ReadOnly = false;

                div_form_grp_States.Visible = false;
                textStates.Visible = false;

                div_udpate_State.Visible = false;
                text_State_to_update.Visible = false;
                text_State_to_update.ReadOnly = true;

                div_form_grp_Cities.Visible = false;
                textCities.Visible = false;

                div_udpate_City.Visible = false;
                text_City_to_update.Visible = false;
                text_City_to_update.ReadOnly = true;


                div_grd_Countries.Visible = true;
                div_grd_states.Visible = false;
                div_grd_cities.Visible = false;

                btnSave.Visible = true;
                btn_update.Visible = false;
                load_all_Countries();
            }
        }
    }

    private void load_all_Countries()
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_get_country_state", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = 0;
        com.Parameters.Add("@ID", SqlDbType.BigInt).Value = 0;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                ol_breadcrumb.InnerHtml = "<li><a href='adminhome.aspx'>Home</a></li><li class='active'><strong>Countries</strong></li>";
                grd_countries.DataSource = dt;
                grd_countries.DataBind();
            }
            else
            {
                CommonCode.show_alert("warning", "No Countries found !", "", false, ltr_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
        }
    }

    private void load_all_States(String CountryID)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_get_country_state", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = 1;
        com.Parameters.Add("@ID ", SqlDbType.BigInt).Value = CountryID;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                ol_breadcrumb.InnerHtml = "<li><a href='adminhome.aspx'>Home</a></li>" +
                                          "<li ><a href='locations.aspx'>Countries</a></li>" +
                                          "<li class='active'><strong>" + dt.Rows[0]["CountryName"] + "</strong></li>";

                textStates.Attributes["placeholder"] = "Add new States here (One Per line)";
                grd_states.DataSource = dt;
                grd_states.DataBind();
            }
            else
            {
                CommonCode.show_alert("warning", "No States found !", "", false, ltr_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
        }
    }

    private void load_all_Cities(String StateID)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_get_country_state", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = 4;
        com.Parameters.Add("@ID", SqlDbType.BigInt).Value = StateID;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {

                ol_breadcrumb.InnerHtml = "<li><a href='adminhome.aspx'>Home</a></li>" +
                                          "<li ><a href='locations.aspx'>Countries</a></li>" +
                                          "<li><a href='locations.aspx?countryid=" + dt.Rows[0]["CountryID"] + "&action=edit'>" + dt.Rows[0]["CountryName"] + "</a></li>" +
                                          "<li class='active'><strong>" + dt.Rows[0]["StateName"] + "</strong></li>";
                grd_cities.DataSource = dt;
                grd_cities.DataBind();
            }
            else
            {
                CommonCode.show_alert("warning", "No Cities found !", "", false, ltr_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error - Loading All Cities", errmsg, false, ltr_msg);
        }
    }

    private void load_single_details(String ID, String Type)
    {
        SqlCommand com = new SqlCommand("dbo_get_country_state", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = Type;
        com.Parameters.Add("@ID", SqlDbType.BigInt).Value = ID;
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                if (Type == "2")
                    textCountryName.Text = dt.Rows[0]["CountryName"] + "";
                else if (Type == "3")
                {
                    textCountryName.Text = dt.Rows[0]["CountryName"] + "";
                    text_State_to_update.Text = dt.Rows[0]["StateName"] + "";
                }
                else if (Type == "5")
                {

                    ol_breadcrumb.InnerHtml = "<li><a href='adminhome.aspx'>Home</a></li>" +
                                          "<li ><a href='locations.aspx'>Countries</a></li>" +
                                          "<li><a href='locations.aspx?countryid=" + dt.Rows[0]["CountryID"] + "&action=edit'>" + dt.Rows[0]["CountryName"] + "</a></li>" +
                                          "<li><a href='locations.aspx?stateid=" + dt.Rows[0]["StateID"] +
                                                        "&action=edit&state=" + HttpUtility.UrlEncode(dt.Rows[0]["StateName"] + "") +
                                                        "&state_countryid=" + dt.Rows[0]["CountryID"] + "'>" +
                                                        dt.Rows[0]["StateName"] + "</a></li>" +
                                          "<li class='active'><strong>" + dt.Rows[0]["CityName"] + "</strong></li>";

                    text_State_to_update.Text = dt.Rows[0]["StateName"] + "";
                    text_City_to_update.Text = dt.Rows[0]["CityName"] + "";
                }

            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("dbo_insert_edit_dele_Country", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CountryID", SqlDbType.BigInt).Value = 0;
        com.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = textCountryName.Text;
        com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
        com.Parameters.Add("@action", SqlDbType.Int).Value = 0;

        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        if (errmsg == "success")
        {
            CommonCode.show_alert("success", "Record Saved", "", ltr_msg);
            if (!string.IsNullOrEmpty(textStates.Text))
            {
                String[] States = textStates.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                if (States.Length > 0)
                {
                    saveStates(States, textCountryName.Text);
                }
            }

            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        else
        {
            CommonCode.show_alert("danger", "Error while saving new record !", errmsg, ltr_msg);
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["cityid"]) && !string.IsNullOrEmpty(Request.QueryString["action"]) && !string.IsNullOrEmpty(Request.QueryString["city_stateid"]))
        {
            // UPDATE CITY NAME

            if (string.IsNullOrEmpty(text_City_to_update.Text))
            {
                CommonCode.show_alert("danger", "City Name can`t be empty", "", ltr_msg);
                return;
            }
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_cities", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@StateID", SqlDbType.BigInt).Value = Request.QueryString["city_stateid"];
            com.Parameters.Add("@CityID", SqlDbType.BigInt).Value = Request.QueryString["cityid"];
            com.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = text_City_to_update.Text.Trim();
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 1;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                CommonCode.show_alert("success", "City <em>" + text_City_to_update.Text + "</em> Updated !", "", false, ltr_msg);
                load_single_details(Request.QueryString["cityid"], "5"); // RETURN SINGLE STATE DETAILS
                load_all_Cities(Request.QueryString["city_stateid"]);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }

        else if (!string.IsNullOrEmpty(Request.QueryString["stateid"]) && !string.IsNullOrEmpty(Request.QueryString["action"]) && !string.IsNullOrEmpty(Request.QueryString["state_countryid"]))
        {

            if (string.IsNullOrEmpty(text_State_to_update.Text))
            {
                CommonCode.show_alert("danger", "State Name can`t be empty", "", ltr_msg);
                return;
            }

            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_states", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CountryID", SqlDbType.BigInt).Value = Request.QueryString["state_countryid"];
            com.Parameters.Add("@StateID", SqlDbType.BigInt).Value = Request.QueryString["stateid"];
            com.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = text_State_to_update.Text;
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 1;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                if (!string.IsNullOrEmpty(textCities.Text))
                {
                    String[] Cities = textCities.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    if (Cities.Length > 0)
                    {
                        saveCities(Cities, Request.QueryString["stateid"]);
                    }
                }

                CommonCode.show_alert("success", "State <em>" + text_State_to_update.Text + "</em> Updated !", "", false, ltr_msg);
                load_single_details(Request.QueryString["stateid"], "3"); // RETURN SINGLE STATE DETAILS
                load_all_Cities(Request.QueryString["stateid"]);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["countryid"]) && !string.IsNullOrEmpty(Request.QueryString["action"]))
        {

            if (string.IsNullOrEmpty(textCountryName.Text))
            {
                CommonCode.show_alert("danger", "Country Name can`t be empty", "", ltr_msg);
                return;
            }
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_Country", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CountryID", SqlDbType.BigInt).Value = Request.QueryString["countryid"];
            com.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = textCountryName.Text;
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 1;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                if (!string.IsNullOrEmpty(textStates.Text))
                {
                    String[] States = textStates.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    if (States.Length > 0)
                    {
                        saveStates(States, textCountryName.Text);
                    }
                }
                CommonCode.show_alert("success", "Country <em>" + textCountryName.Text + "</em> Updated !", "", false, ltr_msg);
                load_single_details(Request.QueryString["countryid"], "2"); // RETURN SINGLE COUNTRY DETAILS
                load_all_States(Request.QueryString["countryid"]);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }
    }

    private void saveStates(String[] States, String CountryName)
    {
        String errmsg = "";
        SqlCommand com = new SqlCommand("select CountryID from MasterCountry where CountryName=@CountryName ", CommonCode.con);
        com.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = CountryName;
        DataTable dt = new DataTable();
        dt = CommonCode.getData(com, out errmsg);
        if (dt.Rows.Count > 0)
        {
            String CountryID = dt.Rows[0]["CountryID"].ToString();
            foreach (String state in States)
            {
                SqlCommand com2 = new SqlCommand("dbo_insert_edit_dele_states", CommonCode.con);
                com2.CommandType = CommandType.StoredProcedure;
                com2.Parameters.Add("@CountryID", SqlDbType.BigInt).Value = CountryID;
                com2.Parameters.Add("@StateID", SqlDbType.BigInt).Value = 0;
                com2.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = state;
                com2.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
                com2.Parameters.Add("@action", SqlDbType.Int).Value = 0;
                errmsg = CommonCode.ExecuteNoQuery(com2, CommonCode.con);
            }
        }
    }

    private void saveCities(String[] Cities, String StateID)
    {
        String errmsg = "";
        foreach (String city in Cities)
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_cities", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@StateID", SqlDbType.BigInt).Value = StateID;
            com.Parameters.Add("@CityID", SqlDbType.BigInt).Value = 0;
            com.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = city;
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 0;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        }
    }


    protected void grd_countries_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String CountryID = e.CommandArgument.ToString();
        if (e.CommandName == "delete_country")
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_Country", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CountryID", SqlDbType.BigInt).Value = CountryID;
            com.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = "";
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 2;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                Page.Response.Redirect("locations.aspx", true);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }
    }

    protected void grd_states_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String StateID = e.CommandArgument.ToString();
        if (e.CommandName == "delete_State")
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_states", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CountryID", SqlDbType.BigInt).Value = 0;
            com.Parameters.Add("@StateID", SqlDbType.BigInt).Value = StateID;
            com.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = "";
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 2;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                Page.Response.Redirect("locations.aspx?countryid=" + Request.QueryString["countryid"] + "&action=edit", true);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }
    }

    protected void grd_cities_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String CityID = e.CommandArgument.ToString();
        if (e.CommandName == "delete_City")
        {
            SqlCommand com = new SqlCommand("dbo_insert_edit_dele_cities", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@StateID", SqlDbType.BigInt).Value = 0;
            com.Parameters.Add("@CityID", SqlDbType.BigInt).Value = CityID;
            com.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = "";
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@action", SqlDbType.Int).Value = 2;
            string errmsg;
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                Page.Response.Redirect("locations.aspx?countryid=" + Request.QueryString["countryid"] + "&action=edit", true);
            }
            else
            {
                CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
            }
        }
    }
}