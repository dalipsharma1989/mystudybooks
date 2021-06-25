using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
public partial class _menu_page : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = CommonCode.SetPageTitle("Manage Menu Items");
        
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_menu();
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
        load_menu();
    }

    private void load_menu()
    {
        textmainContenttelerik.Visible = false;
        DAL dal = new DAL();
        string errmsg = "", MenuID = "";
        DataTable dt = new DataTable();
        dt = dal.get_menu_items("0", Session["iCompanyID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                grd_menu.DataSource = dt;
                grd_menu.DataBind();

                if (!string.IsNullOrEmpty(Request.QueryString["menuid"]))
                {
                    btnSave.Text = "Update";
                    btnSave.Attributes["class"] = "btn btn-sm btn-primary";
                    MenuID = Request.QueryString["menuid"];
                    DataRow[] rr = dt.Select("MenuID = " + MenuID);
                    if (rr.Length > 0)
                    {
                        hf_menuID.Value = rr[0]["MenuID"].ToString();
                        textName.Text = rr[0]["Name"].ToString();
                        dd_type.SelectedIndex = dd_type.Items.IndexOf(dd_type.Items.FindByValue(rr[0]["Type"].ToString()));


                        // Added One Normal Text Box For Type = ' Page ' or ' Link '. Id = textmaincontent 
                        // and another AutoCompleteBox for ' Product Grid '. Id = textmaincontenttelerik
                        // Need revision
                        /* Update on 4/6/2019 by Arvind */

                        string type = rr[0]["Type"] + "";
                        bool cond1 = type != "Page";
                        bool cond2 = type == "Product Grid";
                        bool cond3 = type == "Page";

                        string[] slider_arr = rr[0]["MainContent"].ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        string slider_csv;

                        if (cond1)
                        {
                            slider_csv = slider_arr[0].Replace("{", "").Replace("}", "");
                            textmainContent.TextMode = TextBoxMode.SingleLine;
                            //textmainContent.Text = TextBoxMode.MultiLine;
                        }
                        if (cond2)
                        {
                            slider_csv = slider_arr[1].Replace("{", "").Replace("}", "");
                            textmainContent.Visible = false;
                            textmainContenttelerik.Visible = true;
                            set_AutoCompleteBoxEntry(rr[0]["MainContent"].ToString());
                        }
                        else if (cond3)
                        {
                            slider_csv = slider_arr[0].Replace("{", "").Replace("}", "");
                            textmainContent.Visible = true;
                            textmainContenttelerik.Visible = false;
                            textmainContent.Text = slider_arr[1].Replace("{", "").Replace("}", "").ToString(); 
                        }
                        else
                        {
                            slider_csv = slider_arr[0].Replace("{", "").Replace("}", "");
                            textmainContent.Visible = true;
                            textmainContenttelerik.Visible = false;
                            textmainContent.Text = slider_csv;
                        }
                        textHeaderContent.Text = rr[0]["HeaderContent"].ToString();
                        //    textmainContenttelerik.Text = rr[0]["MainContent"].ToString();
                        textSortOrder.Text = rr[0]["SortOrder"].ToString();
                    }
                    else
                    {
                        CommonCode.show_alert("warning", "Record not found !", "Menu Item you are looking for isn`t available or recently deleted.", false, ph_msg);
                    }
                }

            }
            else
            {
                grd_menu.DataSource = null;
                grd_menu.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading menu items !", errmsg, ph_msg);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string errmsg = "", action_msg = "sav";
        int sort_order = Convert.ToInt32(textSortOrder.Text);
        DAL dal = new DAL();

        string slider_books = "";
        String Slider_csv = "";

        // Added One Normal Text Box For Type = ' Page ' or ' Link '. Id = textmaincontent 
        // and another AutoCompleteBox for ' Product Grid '. Id = textmaincontenttelerik
        // Need revision
        /* Update on 4/6/2019 by Arvind */

        string type = dd_type.SelectedValue;
        bool cond2 = (type == "Product Grid");

        
        if (cond2)
        {
            foreach (AutoCompleteBoxEntry item in textmainContenttelerik.Entries)
            {
                slider_books += item.Value + "','";
            }
            if (!string.IsNullOrEmpty(slider_books))
                slider_books = slider_books.Substring(0, slider_books.Length - 2);
        }
        else
        {
            slider_books = textmainContent.Text;
        }

        if (type == "Link")
        {
            Slider_csv = slider_books;
        }
        else
        {
            Slider_csv = "{" + textHeaderContent.Text.Trim() + "}|{'" + slider_books + "}";
        }
        // Slider_csv = "{" + textHeaderContent.Text.Trim() + "}|{" + slider_books + "}";

        //ltr_msg.Text = Slider_csv;      

        if (!string.IsNullOrEmpty(Request.QueryString["menuid"]))
        {
            dal.insert_update_delete_menu(hf_menuID.Value, textName.Text, dd_type.SelectedValue, textHeaderContent.Text, Slider_csv /*textmainContent.Text*/, sort_order, 1, Session["iCompanyID"].ToString(), out errmsg);
            action_msg = "updat";
        }
        else
        {
            dal.insert_update_delete_menu("0", textName.Text, dd_type.SelectedValue, textHeaderContent.Text, Slider_csv /*textmainContent.Text*/, sort_order, 0, Session["iCompanyID"].ToString(), out errmsg);
            action_msg = "sav";
        }
        if (errmsg == "success")
        {
            CommonCode.show_alert("success", "Menu Item '<em>" + textName.Text + "</em>' " + action_msg + "ed successfully !", "", ph_msg);
            reset_form();
        }
        else
        {
            CommonCode.show_alert("danger", "Error while " + action_msg + "ing menu item.", errmsg, ph_msg);
        }
    }

    protected void grd_menu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string MenuId = e.CommandArgument.ToString();
        if (e.CommandName == "delete_record")
        {
            string errmsg = "", action_msg = "delet";
            DAL dal = new DAL();
            dal.insert_update_delete_menu(MenuId, "", "", "", "", 0, 2, Session["iCompanyID"].ToString(), out errmsg);
            if (errmsg == "success")
            {
                CommonCode.show_alert("success", "Menu Item '<em>" + textName.Text + "</em>' " + action_msg + "ed successfully !", "", ph_msg);
                Response.Redirect("menu.aspx", true);
            }
            else
            {
                CommonCode.show_alert("danger", "Error while " + action_msg + "ing menu item.", errmsg, ph_msg);
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
        load_menu();
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


        if (cond1)
        {
            textmainContent.TextMode = TextBoxMode.SingleLine;
            //textmainContent.Text = TextBoxMode.MultiLine;
        }
        else
        {
            textmainContent.TextMode = TextBoxMode.MultiLine;
        }
        if (cond2)
        {
            textmainContent.Visible = false;
            textmainContenttelerik.Visible = true;
        }
        else if (cond3)
        {
            textmainContent.Visible = true;
            textmainContenttelerik.Visible = false;
        }
        else
        {
            textmainContent.Visible = true;
            textmainContenttelerik.Visible = false;
        }

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