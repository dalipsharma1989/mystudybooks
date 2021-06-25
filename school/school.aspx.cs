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
public partial class school_school : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = CommonCode.SetPageTitle("SCHOOL");
        if (!IsPostBack)
        {
            LoadSchools();
        }
    }

    private void LoadSchools(string schoolId = "")
    {
        string errmsg = "";
        DAL dal = new DAL();

        DataTable dt1 = new DataTable();
        dt1 = dal.ICompanyID_BranchID(out errmsg);
        if (dt1.Rows.Count > 0)
        {
            string iCompanyID = dt1.Rows[0]["iCompanyId"].ToString();
            string iBranchID = dt1.Rows[0]["iBranchID"].ToString();

            Session["iCompanyId"] = dt1.Rows[0]["iCompanyId"].ToString();
            Session["iBranchID"] = dt1.Rows[0]["iBranchID"].ToString();
            Session["FinancialPeriod"] = dt1.Rows[0]["FinancialPeriod"].ToString();

            DataTable dt = new DataTable();
            dt = dal.get_schools(schoolId, iCompanyID, iBranchID, out errmsg);
            if (errmsg == "success")
            {
                if (dt.Rows.Count > 0)
                {
                    rp_school.DataSource = dt;
                    rp_school.DataBind();
                }
            } 
        }

    }


}