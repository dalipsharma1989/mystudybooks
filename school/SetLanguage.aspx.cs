using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Telerik.Web.UI;
public partial class school_SetLanguage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // #f7dfc8a6

        Page.Title = CommonCode.SetPageTitle("Set Information");
        string str_SchoolID = "", str_ClassID = "", str_SetID = "", str_SetDesc = "";
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["School"]))
            {
                str_SchoolID = Request.QueryString["School"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Class"]))
            {
                str_ClassID = Request.QueryString["Class"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["SetId"]))
            {
                str_SetID = Request.QueryString["SetId"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["SetName"]))
            {
                str_SetDesc = Request.QueryString["SetName"].ToString();
            }
            Get_SetInformation(str_SchoolID, str_ClassID, str_SetID, str_SetDesc);
        }
    }


    private void Get_SetInformation(string schoolID, String ClassID, String SetID, String SetDesc)
    {
        DAL dal = new DAL();
        DataTable dt = new DataTable();

        dt = dal.Get_Set_Information_By_School_Class_Set(schoolID, ClassID, SetID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(),"");
        if (dt.Rows.Count > 0)
        {
            h_Heading.InnerHtml = dt.Rows[0]["SchoolName"].ToString() + " || " + dt.Rows[0]["ClassDESC"].ToString() + " || " + dt.Rows[0]["BundleDesc"].ToString();


            Decimal totalNetAmt = 0;
            foreach (DataRow dr in  dt.Rows)
            {
                totalNetAmt += Convert.ToDecimal(dr["NetAmount"].ToString());
            }

            DataView dtnewdataviewBookCategory = new DataView(dt.DefaultView.ToTable("Categorytbl", true, "BookCategoryID", "BookCategoryDesc"));
            spn_NetTOtl.InnerHtml = totalNetAmt.ToString();
            // rp_SetInformation.DataSource = dtnewdataviewBookCategory.ToTable();
            rp_SetInformation.DataSource = dt;
            rp_SetInformation.DataBind();
        }
        else
        {
            rp_SetInformation.DataSource = null;
            rp_SetInformation.DataBind();
        }
    }

    private void Get_SetInformationDetail(string schoolID, String ClassID, String SetID, String SetDesc, String str_CategoryID, Repeater rp_SetDetail) 
    {
        DAL dal = new DAL();
        DataTable dt = new DataTable();

        dt = dal.Get_Set_Information_By_School_Class_Set(schoolID, ClassID, SetID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), str_CategoryID);
        if (dt.Rows.Count > 0)
        { 
            rp_SetDetail.DataSource = dt;
            rp_SetDetail.DataBind();
        }
        else
        {
            rp_SetDetail.DataSource = null;
            rp_SetDetail.DataBind();
        }
    }

    protected void rp_SetInformation_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string str_SchoolID = "", str_ClassID = "", str_SetID = "", str_SetDesc = "";
        if (!string.IsNullOrEmpty(Request.QueryString["School"]))
        {
            str_SchoolID = Request.QueryString["School"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["Class"]))
        {
            str_ClassID = Request.QueryString["Class"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["SetId"]))
        {
            str_SetID = Request.QueryString["SetId"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["SetName"]))
        {
            str_SetDesc = Request.QueryString["SetName"].ToString();
        }

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string hf_CategoryID = (e.Item.FindControl("hf_CategoryID") as HiddenField).Value; 
            Repeater rp_SetDetail = e.Item.FindControl("rp_SetDetail") as Repeater;
            Get_SetInformationDetail(str_SchoolID, str_ClassID, str_SetID, str_SetDesc, hf_CategoryID, rp_SetDetail); 
        }
    } 

    protected void btn_BuyNow_Click(object sender, EventArgs e)
    {
        string str_SchoolID = "", str_ClassID = "", str_SetID = "", str_RollNumber = "", str_StudentName = "" ;

        if (!string.IsNullOrEmpty(Request.QueryString["School"]))
        {
            str_SchoolID = Request.QueryString["School"].ToString();
        }
        else
        {
            Response.Redirect("/school/school.aspx", true);
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Class"]))
        {
            str_ClassID = Request.QueryString["Class"].ToString();
        }
        else
        {
            Response.Redirect("/school/school.aspx", true);
        }
        if (!string.IsNullOrEmpty(Request.QueryString["SetId"]))
        {
            str_SetID = Request.QueryString["SetId"].ToString();
        }
        else
        {
            Response.Redirect("/school/school.aspx", true);
        }

        if (txt_RollNumber.Text.Trim() != "")
        {
            str_RollNumber = txt_RollNumber.Text.Trim();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Addmission/Roll Number');", true);
            return;
        }

        if (txt_StudentName.Text.Trim() != "")
        {
            str_StudentName = txt_StudentName.Text.Trim().ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "danger", "alert('Please Enter Student Name');", true);
            return;
        }

        if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
        {
            Response.Redirect("/customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
        }
        else
        {
            Response.Redirect("/customer/directcheckoutset?school=" + str_SchoolID + "&class=" + str_ClassID + "&setid=" + str_SetID + "&studname=" + str_StudentName + "&studRoll=" + str_RollNumber, true);
        }
    }
}