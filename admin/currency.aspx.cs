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
        if (!IsPostBack)
        {
            load_Currency();
        }
    }

    private void load_Currency()
    {
        String errmsg = "", CurrencyID = "";

        DataTable dt = new DataTable();
        DAL dal = new DAL();
        dt = dal.get_currency(CurrencyID, out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["currencyid"]))
                {
                    btnSave.Text = "Update";
                    btnSave.Attributes["Currency"] = "btn btn-sm btn-primary";
                    CurrencyID = Request.QueryString["currencyid"];
                    DataRow[] rr = dt.Select("currencyid = '" + CurrencyID+"'");
                    if (rr.Length > 0)
                    {
                        textCurrencyID.Text = rr[0]["CurrencyID"].ToString();
                        textCurrencyName.Text = rr[0]["CurrencyName"].ToString();
                        textCurrencySymbol.Text = rr[0]["CurrencySymbol"].ToString();
                        textRate.Text = rr[0]["Rate"].ToString();
                        cb_isDefault.Checked = Convert.ToBoolean(rr[0]["DefaultCurr"]);
                        hf_Currency.Value = rr[0]["CurrencyID"].ToString();
                    }
                    else
                    {
                        CommonCode.show_alert("warning", "Record not found !", "Currency you are looking for isn`t available or recently deleted.", false, ltr_msg);
                    }
                }
                grd_currency.DataSource = dt;
                grd_currency.DataBind();
            }
            else
            {
                grd_currency.DataSource = null;
                grd_currency.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Currency", errmsg, false, ltr_msg);
        }
    }



    protected void grd_currency_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string CurrencyID = e.CommandArgument.ToString();
        if (e.CommandName == "delete_record")
        {
            string errmsg = "", action_msg = "delet";
            DAL dal = new DAL();
            dal.insert_update_delete_Currency(CurrencyID, "","", false, 0, "Admin", 2, out errmsg);
            if (errmsg == "success")
            {
                CommonCode.show_alert("success", "Currency '<em>" + textCurrencyName.Text + "</em>' " + action_msg + "ed successfully !", "", ltr_msg);
                reset_form();
            }
            else
            {
                CommonCode.show_alert("danger", "Error while " + action_msg + "ing Currency.", errmsg, ltr_msg);
            }
        }
    }

    protected void grd_currency_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_currency.PageIndex = e.NewPageIndex;
        load_Currency();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string errmsg = "", action_msg = "sav";

        decimal rate = 0;
        decimal.TryParse(textRate.Text, out rate);
        DAL dal = new DAL();
        if (!string.IsNullOrEmpty(Request.QueryString["CurrencyID"]))
        {
            dal.insert_update_delete_Currency(hf_Currency.Value, textCurrencyName.Text, textCurrencySymbol.Text, cb_isDefault.Checked, rate, "Admin", 1, out errmsg);
            action_msg = "updat";
        }
        else
        {
            dal.insert_update_delete_Currency(textCurrencyID.Text, textCurrencyName.Text, textCurrencySymbol.Text, cb_isDefault.Checked, rate, "Admin", 0, out errmsg);
            action_msg = "sav";
        }
        if (errmsg == "success")
        {
            CommonCode.show_alert("success", "Currency '<em>" + textCurrencyName.Text + "</em>' " + action_msg + "ed successfully !", "", ltr_msg);
            reset_form();
        }
        else
        {
            CommonCode.show_alert("danger", "Error while " + action_msg + "ing Currency.", errmsg, ltr_msg);
        }

    }

    private void reset_form()
    {
        btnSave.Text = "Save";
        btnSave.Attributes["Currency"] = "btn btn-sm btn-success";
        textCurrencyID.Text = "";
        textCurrencyName.Text = "";
        textRate.Text = "1";
        hf_Currency.Value = "";
        load_Currency();
    }
}