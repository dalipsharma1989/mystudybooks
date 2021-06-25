using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.Title = CommonCode.SetPageTitle("Subscribe News & Letters");
        try
        {
            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                load_Emails();
            }
        }
        catch (Exception ex)
        {
            string erro = ex.Message;

            Response.Redirect("../admin/");
        }
    }

    private void load_Emails()
    {
        String errmsg = "", ClassID = "0";

        DataTable dt = new DataTable();
        DAL dal = new DAL();
        dt = dal.get_Emails(Session["iCompanyId"].ToString(), Session["iBranchID"].ToString() ,out errmsg);

        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            { 
                grd_Classes.DataSource = dt;
                grd_Classes.DataBind();
            }
            else
            {
                grd_Classes.DataSource = null;
                grd_Classes.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Emails", errmsg, false, ltr_msg);
        }
    }

    protected void grd_Classes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void grd_Classes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Classes.PageIndex = e.NewPageIndex;
        load_Emails();
    }





    protected void btn_ExportToExcel_Click(object sender, EventArgs e)
    {
        if (grd_Classes.Visible)
        {
            //Response.AddHeader("content-disposition", "attachment; filename=EmailsToExcel.xls");
            //Response.ContentType = "application/excel";
            //StringWriter sWriter = new StringWriter();            
            //HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            //grd_Classes.RenderControl(hTextWriter);
            //Response.Write(sWriter.ToString());
            //Response.End();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=EmailToExcel.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                //To Export all pages
                grd_Classes.AllowPaging = false;
                this.load_Emails();
                //GridView2.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grd_Classes.HeaderRow.Cells)
                {
                    cell.BackColor = grd_Classes.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grd_Classes.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grd_Classes.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grd_Classes.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }
                grd_Classes.RenderControl(hw);
                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}