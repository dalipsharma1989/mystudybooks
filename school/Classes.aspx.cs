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
public partial class Classes : System.Web.UI.Page
{

    public string QuerySchoolId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = CommonCode.SetPageTitle("SCHOOL");
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["SchoolID"]))
            {
                LoadClasses(Request.QueryString["SchoolID"].ToString(),"");
            }
        }
    }

    private void LoadClasses(string SchoolID = "", string className = "")
    {
        QuerySchoolId = SchoolID;
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_Classes(className, SchoolID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("SchoolID"));

                foreach (DataRow dr in dt.Rows)
                {
                    dr["SchoolID"] = SchoolID;
                }
                rp_class.DataSource = dt;
                rp_class.DataBind();
            } 
        } 
    }

    private void LoadClasses_Sets(string ClassID, Repeater rp_Sets)
    {
        string errmsg = "", SchoolID = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
    
        if (!string.IsNullOrEmpty(Request.QueryString["SchoolID"]))
        {
            SchoolID =   Request.QueryString["SchoolID"].ToString();
        }


        dt = dal.get_Bundles("",ClassID, SchoolID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("SchoolID"));
                dt.Columns.Add(new DataColumn("ClassID"));
                foreach (DataRow dr in dt.Rows)
                {
                    dr["SchoolID"] = SchoolID.ToString();
                    dr["ClassID"] = ClassID.ToString();
                }
                rp_Sets.DataSource = dt;
                rp_Sets.DataBind();
            }
        }
    }


    protected void rp_class_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string hf_HiddenClass = (e.Item.FindControl("hf_HiddenClass") as HiddenField).Value; 
            Repeater rp_Sets = e.Item.FindControl("rp_Sets") as Repeater;
            LoadClasses_Sets(hf_HiddenClass, rp_Sets);
        }
    }
}