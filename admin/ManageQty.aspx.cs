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

        this.Title = CommonCode.SetPageTitle("Manage Closing Qty");

        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_ConversionRate(); 
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }


    }

    private void load_ConversionRate()
    {
        String errmsg = "";

        DataTable dt = new DataTable();
        DAL dal = new DAL();
        dt = dal.get_CLosingQty(Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                grd_ConversionRate.DataSource = dt;
                grd_ConversionRate.DataBind();
            }
            else
            {
                grd_ConversionRate.DataSource = null;
                grd_ConversionRate.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Qty", errmsg, false, ltr_msg);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string errmsg = "", action_msg = "sav";

        DAL dal = new DAL();
        float Conversionrate;
        if (!float.TryParse(textConversionRate.Text, out Conversionrate))
        {
            CommonCode.show_alert("warning", "Qty is not valid!", "Please enter valid Closing Qty", ltr_msg);
            return;
        }

        dal.insert_CLosingQty(textConversionRate.Text, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            CommonCode.show_alert("success", "Closing Qty '<em>" + textConversionRate.Text + "</em>' " + action_msg + "ed successfully !", "", ltr_msg);
            reset_form();
        }
        else
        {
            CommonCode.show_alert("danger", "Error while " + action_msg + "ing Conversion Rate.", errmsg, ltr_msg);
        }

    }

    private void reset_form()
    {
        btnSave.Text = "Save";
        btnSave.Attributes["class"] = "btn btn-sm btn-success";
        textConversionRate.Text = "";
        hf_ClassID.Value = "";
        load_ConversionRate();
    }

    protected void grd_ConversionRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_ConversionRate.PageIndex = e.NewPageIndex;
        load_ConversionRate();
    }
}