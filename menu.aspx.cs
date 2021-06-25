using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _menu : System.Web.UI.Page
{
    public static string HeaderContent = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string strtype = "";

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                strtype = Request.QueryString["type"].ToString();
            }

            if (strtype == "noti")
            {
                if (!string.IsNullOrEmpty(Request.QueryString["NotificationID"]))
                {
                    load_ExamNotification(Request.QueryString["NotificationID"]);
                } 
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["menuid"]))
                {
                    load_menu(Request.QueryString["menuid"]);
                }
                else
                {
                    Response.Redirect("/", true);
                }
            } 
        }
    }

    private void load_menu(string menuid)
    {
        DAL dal = new DAL();
        string errmsg = "";
        DataTable dt = new DataTable();
        dt = dal.get_menu_items(menuid, Session["iCompanyID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                tagName.InnerHtml = "Menu Name";
                this.Title = CommonCode.SetPageTitle(dt.Rows[0]["HeaderContent"] + "");
                HeaderContent = dt.Rows[0]["HeaderContent"] + "";
                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                {
                    if (Request.QueryString["type"] == "Link")
                    {
                        string temp = dt.Rows[0]["MainContent"].ToString();
                        temp = temp.Replace("{", "").Replace("}", "");
                        string[] csv = temp.Split('|');
                        if (String.IsNullOrEmpty(csv[1]))
                            Response.Redirect("/",true);
                        else
                            Response.Redirect(csv[1] + "", true);
                    }
                    if (Request.QueryString["type"] == "Page")
                    {
                        rp_page.DataSource = dt;
                        rp_page.DataBind();
                    }
                    if (Request.QueryString["type"] == "Product Grid")
                    {
                        string grid_csv = dt.Rows[0]["MainContent"] + "";

                        string product_selector = "", product_csv = "";
                        if (grid_csv != string.Empty)
                        {
                            string[] grid = grid_csv.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                            if (grid.Length == 2)
                            {
                                product_selector = grid[0].Replace("{", "").Replace("}", "");
                                product_csv = grid[1].Replace("{", "").Replace("}", "");
                                if (product_csv.Trim() == "" || product_csv.Trim() == "'")
                                {
                                    CommonCode.show_alert("warning", "No menu items defined !", "", ph_msg);
                                }
                                else
                                {
                                    get_products(product_selector, product_csv);
                                }                                
                            }
                        }
                    }
                }
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading menu items !", errmsg, ph_msg);
        }
    }


    private void get_products(string product_selector, string product_csv)
    {
        string errmsg = "";
        DataTable dt_slider = new DataTable();
        DAL dal = new DAL();
        dt_slider = dal.get_sliders_books("", product_csv, Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt_slider.Rows.Count > 0)
            {
                rp_products.DataSource = dt_slider;
                rp_products.DataBind();
            }
            else
            {
                CommonCode.show_alert("danger", "Empty Product CSV", "", ph_msg);
            }
                
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Products ", errmsg, ph_msg);
        }
            
    }

    private void load_ExamNotification(string NotificationID) 
    {
        string errmsg = "";  
        DAL dal = new DAL(); 
        DataTable dt = new DataTable();
        dt = dal.get_ExamsNotifications(NotificationID, Session["iCompanyID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {
                tagName.InnerHtml = "Exams Notification";
                this.Title = CommonCode.SetPageTitle(dt.Rows[0]["HeaderContent"] + "");
                HeaderContent = dt.Rows[0]["HeaderContent"] + "";

                string grid_csv = dt.Rows[0]["MainContent"] + "";

                string product_selector = "", product_csv = "";
                if (grid_csv != string.Empty)
                {
                    string[] grid = grid_csv.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (grid.Length == 2)
                    {
                        product_selector = grid[0].Replace("{", "").Replace("}", "");
                        product_csv = grid[1].Replace("{", "").Replace("}", "");
                        get_products(product_selector, product_csv);
                    }
                }

            }
        }

    }
}