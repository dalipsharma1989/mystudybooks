using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
public partial class _menu_pageNewLaunch : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Manage New Launched Items");
        
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                 load_NewLaunched();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }
    }


    protected void grd_menu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_menu.PageIndex = e.NewPageIndex;
        load_NewLaunched();
    }

    private void load_NewLaunched()
    { 
        DAL dal = new DAL();
        string errmsg = "" ;
        DataTable dt = new DataTable();
        dt = dal.get_New_Launched_items("0", Session["iCompanyID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                textmainContent.Text = dt.Rows[0]["MessagetoShow"].ToString();
                foreach (DataRow rr in dt.Rows)
                {
                    textmainContenttelerik.Entries.Add(new AutoCompleteBoxEntry(rr["BookName"].ToString(), rr["ISBN"].ToString()));
                }
                grd_menu.DataSource = dt;
                grd_menu.DataBind(); 
            }
            else
            {
                
                grd_menu.DataSource = null;
                grd_menu.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading New Launched items !", errmsg, ph_msg);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string errmsg = "", action_msg = "sav";
        int sort_order = Convert.ToInt32(textSortOrder.Text);
        DAL dal = new DAL();

        string slider_books = ""; 
          
        bool isNewRelease = false;
        string savestatus = "success";
        foreach (AutoCompleteBoxEntry item in textmainContenttelerik.Entries)
        {
            slider_books = item.Value;

            if (slider_books != "")
            {
                isNewRelease = true;
                dal.insert_update_delete_New_Released("0", slider_books, isNewRelease,0, Session["iCompanyID"].ToString(), textmainContent.Text.Trim(), out errmsg);
                if (errmsg != "success")
                {
                    savestatus += slider_books + "|";
                }
            } 
        } 

        if (savestatus == "success")
        {
            CommonCode.show_alert("success", "New Launched Item '<em>" + textName.Text + "</em>' " + action_msg + "ed successfully !", "", ph_msg);
            reset_form();
        }
        else
        {
            CommonCode.show_alert("danger", "Error while saving New Launched item.", errmsg, ph_msg);
        }
         
    }

    protected void grd_menu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string NewReleaseID = e.CommandArgument.ToString();
        if (e.CommandName == "delete_record")
        {
            string errmsg = "", action_msg = "delet";
            DAL dal = new DAL();
            dal.insert_update_delete_New_Released(NewReleaseID, "",  false, 1, Session["iCompanyID"].ToString(),"", out errmsg);
            if (errmsg == "success")
            {
                CommonCode.show_alert("success", "New Launched Item '<em>" + textName.Text + "</em>' " + action_msg + "ed successfully !", "", ph_msg);
                Response.Redirect("NewLaunch.aspx", true);
            }
            else
            {
                CommonCode.show_alert("danger", "Error while " + action_msg + "ing New Launched item.", errmsg, ph_msg);
            }
        }
    }

    private void reset_form()
    {
        btnSave.Text = "Save";
        btnSave.Attributes["class"] = "btn btn-sm btn-success";
        textName.Text = "";
        dd_type.SelectedIndex = 0;
        textHeaderContent.Text = "";
        textmainContent.Text = "";
        textmainContenttelerik.Entries.Clear();
        textSortOrder.Text = "1";
        hf_menuID.Value = "";
        load_NewLaunched();
    }


    protected void dd_type_SelectedIndexChanged(object sender, EventArgs e)
    {

        // Added One Normal Text Box For Type = ' Page ' or ' Link '. Id = textmaincontent 
        // and another AutoCompleteBox for ' Product Grid '. Id = textmaincontenttelerik
        // Need revision
        /* Update on 4/6/2019 by Arvind */

        string type = dd_type.SelectedValue;
        
        bool cond1 = type != "Page";
        bool cond2 = type == "Product Grid";
        bool cond3 = type == "Page";


        //if (cond1)
        //{
        //    textmainContent.TextMode = TextBoxMode.SingleLine;
        //    //textmainContent.Text = TextBoxMode.MultiLine;
        //}
        //else
        //{
        //    textmainContent.TextMode = TextBoxMode.MultiLine;
        //}
        //if (cond2)
        //{
        //    textmainContent.Visible = false;
        //    textmainContenttelerik.Visible = true;
        //}
        //else if (cond3)
        //{
        //    textmainContent.Visible = true;
        //    textmainContenttelerik.Visible = false;
        //}
        //else
        //{
        //    textmainContent.Visible = true;
        //    textmainContenttelerik.Visible = false;
        //}

    }

    private void set_AutoCompleteBoxEntry(string csv)
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();

        string[] slider_arr = csv.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string slider_csv;
        
        slider_csv = slider_arr[1].Replace("{", "").Replace("}", "");

        dt = dal.getDataByQuery("select BookName, BookCode as ProductID from MasterTitle where BookCode in (" + slider_csv + ") and iCompanyId = '" + Session["iCompanyID"].ToString() + "'", out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rr in dt.Rows)
                {
                    textmainContenttelerik.Entries.Add(new AutoCompleteBoxEntry(rr["BookName"].ToString(), rr["ProductID"].ToString()));
                }
            }
        }
    }

}