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
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["updated"]))
                CommonCode.show_alert("success", "Shipping Amount Details saved successfully !", "", ph_msg);

            load_shipping_amount();
        }
    }

    private void load_shipping_amount()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_shipping_amount_details(out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                hf_RowID.Value = dt.Rows[0]["RowID"] + "";
                textDefaultShippingAmount.Text = dt.Rows[0]["DefaultShippingAmount"] + "";
                textMinAmountForFreeShipping.Text = dt.Rows[0]["MinAmountForFreeShipping"] + "";
                textFreeShippingMessage.Text = dt.Rows[0]["FreeShippingMessage"].ToString().Replace("<br>", Environment.NewLine);
            }
            else
            {
                CommonCode.show_alert("warning", "Shipping Amount Details Not Found !", "", ph_msg);
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Shipping Amount Details", errmsg, ph_msg);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string errmsg = "";
        DAL dal = new DAL();
        int RowID = 0;
        float DefaultShippingAmount, MinAmountForFreeShipping;
        if (!float.TryParse(textDefaultShippingAmount.Text, out DefaultShippingAmount))
        {
            CommonCode.show_alert("warning", "Default Shipping Amount was not valid!", "Please enter valid amount", ph_msg);
            return;
        }
        if (!float.TryParse(textMinAmountForFreeShipping.Text, out MinAmountForFreeShipping))
        {
            CommonCode.show_alert("warning", "Min Amount For Free Shipping was not valid!", "Please enter valid amount", ph_msg);
            return;
        }
        if (!string.IsNullOrEmpty(hf_RowID.Value))
            RowID = Convert.ToInt32(hf_RowID.Value);

        dal.insert_update_delete_shipping_amount(RowID, DefaultShippingAmount, MinAmountForFreeShipping, textFreeShippingMessage.Text.Replace(Environment.NewLine, "<br>"), out errmsg);
        if (errmsg == "success")
        {
            Response.Redirect(Request.Url.AbsoluteUri.Replace("?updated=true", "") + "?updated=true", true);
        }
        else
        {
            CommonCode.show_alert("danger", "Error while saving Shipping Amount Details", errmsg, ph_msg);
        }
    }
}