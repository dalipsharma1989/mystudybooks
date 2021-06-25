using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String strQuery = "DASHBOARD";

            if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
            {
                Response.Redirect("../admin/");
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["TYPE"]))
                {
                    strQuery = Request.QueryString["TYPE"].ToString();
                } 

                if(strQuery.ToUpper() == "PENDINGORDER")
                {
                    dv_Status.Visible = false;
                    dv_successOrder.Visible = false;
                    dv_PendingOrders.Visible = true;
                    dv_PendingReview.Visible = false;
                    load_statsPendingPayment();
                    this.Title = CommonCode.SetPageTitle("Pending Orders");
                }
                else if (strQuery.ToUpper() == "PENDINGREVIEW")
                {
                    dv_Status.Visible = false;
                    dv_successOrder.Visible = false;
                    dv_PendingOrders.Visible = false;
                    dv_PendingReview.Visible = true;
                    load_pending_reviews();
                    this.Title = CommonCode.SetPageTitle("Pending Reviews");
                }
                else if (strQuery.ToUpper() == "DASHBOARD")
                {
                    dv_Status.Visible = true;
                    dv_successOrder.Visible = true;
                    dv_PendingOrders.Visible = false;
                    dv_PendingReview.Visible = false;
                    load_stats();
                    this.Title = CommonCode.SetPageTitle("DASHBOARD");
                } 
            }
        }
        catch (Exception ex)
        {
          string erro =   ex.Message;

            Response.Redirect("../admin/");
        }        
    }
    
    private void load_stats(string SearchData = "0")
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        ds = dal.get_admin_stats(SearchData,Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        if (errmsg == "success")
        {                
            if (ds.Tables.Count > 0)
            {
                h1_monthly_income.InnerText = CommonCode.AppSettings("CurrencySymbol") +" " + string.Format("{0:n}", ds.Tables[0].Rows[0]["TotalMonthIncome"]);
                h1_monthly_orders.InnerText = ds.Tables[0].Rows[0]["TotalMonthOrders"].ToString();

                h1_annual_orders.InnerText = ds.Tables[1].Rows[0]["TotalAnnualOrders"].ToString();

                h1_registered_users.InnerText = ds.Tables[3].Rows[0]["TotalRegisteredUsers"].ToString();

                //rp_latest_orders.DataSource = ds.Tables[4];
                //rp_latest_orders.DataBind();
                GridView1.DataSource = ds.Tables[4];
                GridView1.DataBind();   
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading statistics", errmsg, ltr_scripts);
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        load_statsCustomData(txtOrderPaymentId.Text.Trim().ToString());
    }
    private void load_statsCustomData(string SearchData = "0")
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        ds = dal.get_admin_stats(SearchData, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (ds.Tables.Count > 0)
            {
                //monthly_income = string.Format("{0:n}", ds.Tables[0].Rows[0]["TotalMonthIncome"]);
                //monthly_orders = ds.Tables[0].Rows[0]["TotalMonthOrders"].ToString();

                //annual_orders = ds.Tables[1].Rows[0]["TotalAnnualOrders"].ToString();

                //registered_users = ds.Tables[3].Rows[0]["TotalRegisteredUsers"].ToString();
                h1_monthly_income.InnerText = CommonCode.AppSettings("CurrencySymbol") + " " + string.Format("{0:n}", ds.Tables[0].Rows[0]["TotalMonthIncome"]);
                h1_monthly_orders.InnerText = ds.Tables[0].Rows[0]["TotalMonthOrders"].ToString();

                h1_annual_orders.InnerText = ds.Tables[1].Rows[0]["TotalAnnualOrders"].ToString();

                h1_registered_users.InnerText = ds.Tables[3].Rows[0]["TotalRegisteredUsers"].ToString();

                GridView1.DataSource = ds.Tables[4];
                GridView1.DataBind();
                //rp_latest_orders.DataSource = ds.Tables[4];
                //rp_latest_orders.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading statistics", errmsg, ltr_scripts);
        }
    }

    private void load_statsPendingPayment(string get_admin_stats_PendingPayment = "0" )
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        ds = dal.get_admin_stats_PendingPayment(Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), get_admin_stats_PendingPayment, out errmsg);
        if (errmsg == "success")
        {
            if (ds.Tables.Count > 0)
            {
                rp_Pending_order_Payment.DataSource = ds.Tables[0];
                rp_Pending_order_Payment.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Pending Orders Payment", errmsg, ltr_scripts);
        }        
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindGridView( e.NewPageIndex, "0"); //bindgridview will get the data source and bind it again
    }

    private void bindGridView( int offset, string SearchData = "0")
    {

        string errmsg = "";
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        ds = dal.get_admin_stats(SearchData, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out errmsg);
        if (errmsg == "success")
        {
            if (ds.Tables.Count > 0)
            {
                GridView1.DataSource = ds.Tables[4];
                GridView1.DataBind();
            }
        }         
    }

    private void load_pending_reviews()
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_admin_pending_reviews(Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(),"0", out errmsg);
        div_pending_reviews.Visible = false;
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {

                div_pending_reviews.Visible = true;
                rp_pending_reviews.DataSource = dt;
                rp_pending_reviews.DataBind();
                rp_review_modals.DataSource = dt;
                rp_review_modals.DataBind();
            }
        }
        else
        {
            CommonCode.show_toastr("error", "Error while loading Pending Reviews", errmsg, false, "", false, "", ltr_scripts);
        }
        //    SqlCommand com = new SqlCommand("select MP.ProductID,MP.ISBN,MP.BookName,MC.CustName,MPR.* from " +
        //                                "MasterProductReview MPR right join MasterProduct MP " +
        //                                "on MP.ProductID = MPR.ProductID join MasterCustomer MC " +
        //                                "on MPR.CustID = MC.CustId " +
        //                                "where MPR.isApporved=0 order by CreatedOn desc", CommonCode.con);
        //DataTable dt = new DataTable();
        //string errmsg;
        //dt = CommonCode.getData(com, out errmsg);
        //div_pending_reviews.Visible = false;
        //if (errmsg == "success")
        //{
        //    if (dt.Rows.Count > 0)
        //    {

        //        div_pending_reviews.Visible = true;
        //        rp_pending_reviews.DataSource = dt;
        //        rp_pending_reviews.DataBind();
        //        rp_review_modals.DataSource = dt;
        //        rp_review_modals.DataBind();
        //    }
        //}
        //else
        //{
        //    CommonCode.show_toastr("error", "Error while loading Pending Reviews", errmsg, false, "", false, "", ltr_scripts);
        //}
    }

    protected void rp_Pending_order_Payment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rp_Pending_order_Payment.PageIndex = e.NewPageIndex;
        bind_rp_Pending_order_Payment(e.NewPageIndex , "0"); //bindgridview will get the data source and bind it again
    }

    protected void rp_pending_reviews_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rp_pending_reviews.PageIndex = e.NewPageIndex;
        bind_rp_pending_reviews(e.NewPageIndex); //bindgridview will get the data source and bind it again
    }

    private void bind_rp_pending_reviews(int offset)
    {

        //SqlCommand com = new SqlCommand("select MP.ProductID,MP.ISBN,MP.BookName,MC.CustName,MPR.* from " +
        //                                "MasterProductReview MPR right join MasterProduct MP " +
        //                                "on MP.ProductID = MPR.ProductID join MasterCustomer MC " +
        //                                "on MPR.CustID = MC.CustId " +
        //                                "where MPR.isApporved=0 order by CreatedOn desc", CommonCode.con);
        //DataTable dt = new DataTable();
        //string errmsg;
        //dt = CommonCode.getData(com, out errmsg);
        //div_pending_reviews.Visible = false;
        //if (errmsg == "success")
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        div_pending_reviews.Visible = true;
        //        rp_pending_reviews.DataSource = dt;
        //        rp_pending_reviews.DataBind();
        //    }
        //}
        //else
        //{
        //    CommonCode.show_toastr("error", "Error while loading Pending Reviews", errmsg, false, "", false, "", ltr_scripts);
        //}

        string errmsg = "";
        DAL dal = new DAL();
        DataTable dt = new DataTable();
        dt = dal.get_admin_pending_reviews(Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(),"0", out errmsg);
        div_pending_reviews.Visible = false;
        if (errmsg == "success")
        {
            if (dt.Rows.Count > 0)
            {

                div_pending_reviews.Visible = true;
                rp_pending_reviews.DataSource = dt;
                rp_pending_reviews.DataBind();
                rp_review_modals.DataSource = dt;
                rp_review_modals.DataBind();
            }
        }
        else
        {
            CommonCode.show_toastr("error", "Error while loading Pending Reviews", errmsg, false, "", false, "", ltr_scripts);
        }

    }

    private void bind_rp_Pending_order_Payment(int offset, string get_admin_stats_PendingPayment = "0")
    {
        string errmsg = "";
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        ds = dal.get_admin_stats_PendingPayment(Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), get_admin_stats_PendingPayment, out errmsg);
        if (errmsg == "success")
        {
            if (ds.Tables.Count > 0)
            {
                rp_Pending_order_Payment.DataSource = ds.Tables[0];
                rp_Pending_order_Payment.DataBind();
            }
        }
        else
        {
            CommonCode.show_alert("danger", "Error while loading Pending Orders Payment", errmsg, ltr_scripts);
        }

        //SqlCommand com = new SqlCommand("dbo_get_order_stats_PendingPayment", CommonCode.con);
        //com.CommandType = CommandType.StoredProcedure;
        //SqlDataAdapter ad = new SqlDataAdapter(com);
        //DataSet ds = new DataSet();
        //ad.Fill(ds);
        //if (ds.Tables.Count > 0)
        //{
        //    rp_Pending_order_Payment.DataSource = ds.Tables[0];
        //    rp_Pending_order_Payment.DataBind();
        //}
    }

    protected void rp_Pending_order_Payment_ItemCommand(object source, GridViewCommandEventArgs e)
    {
        DAL dal = new DAL();
        string  OrderID_output = "", errmsg = "" , mail_err_msg = "";
        try
        {        
            if (e.CommandName == "payment_received")
                {
                    //GridView gd = (GridView)(e.CommandSource);
                    GridViewRow grow = rp_Pending_order_Payment.Rows[Convert.ToInt32(e.CommandArgument.ToString())%25];
                    TextBox tb = (TextBox)grow.Cells[0].FindControl("transaction_id");
                    string orderid = grow.Cells[4].Text;
                    string mail = grow.Cells[5].Text;
                    string status = grow.Cells[6].Text;
                    string name = grow.Cells[7].Text;
                    string transactionid = grow.Cells[12].Text;
                    string totalqty = (grow.Cells[9].FindControl("tot_qty") as HtmlGenericControl).InnerText;
                    string amount = (grow.Cells[10].FindControl("amount") as HtmlGenericControl).InnerText;
                    string ship_cost = (grow.Cells[11].FindControl("ship_cost") as HtmlGenericControl).InnerText;
                    string paymethod = grow.Cells[13].Text;
                    string shipmethod = grow.Cells[14].Text;
                    string remark = grow.Cells[15].Text;
                    string customerCode = grow.Cells[16].Text;
                    //dal.Order_PaymentResStatus(OrderID: orderid, TotalQty: totalqty, TotalAmount: amount, ShipCost: ship_cost, ShipMethod: shipmethod, Remark: remark, Status: "1",
                    //StatusRemark: "PaymentReceived", PayMethod: paymethod, CardType: "", NameOnCard: "", CardNum: "", CardExpires: "manu", OtherDetail: tb.Text, TxnID: transactionid, 
                    //iCompanyID: Session["iCompanyId"].ToString(), iBranchID: Session["iBranchID"].ToString(),FinancialPeriod: Session["FinancialPeriod"].ToString(),
                    //CustID: customerCode, OrderID_output: out OrderID_output, errmsg: out errmsg);

                if (errmsg == "success")
                        {
                           // Email_Send(mail, name, tb.Text, orderid, amount, out mail_err_msg);
                            load_stats();
                            load_statsPendingPayment();
                            bind_rp_Pending_order_Payment(0);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('Record Updated Successfully');window.location ='adminhome.aspx?TYPE=PENDINGORDER';", true);
                    //CommonCode.show_toastr("Success", "Record Updated Successfully", errmsg, false, "", false, "", ltr_scripts);
                        }
                }
            else if(e.CommandName == "delete_order")
                {
                    errmsg = "";
                    DataTable delete = new DataTable();
                    GridViewRow grow = rp_Pending_order_Payment.Rows[Convert.ToInt32(e.CommandArgument.ToString())%25];
            
                    SqlCommand com = new SqlCommand("Update [TransOrder] set Deleted = 1 where TransactionIDord = '" + grow.Cells[4].Text + "'", CommonCode.con);
                    delete = CommonCode.getData(com, out errmsg);
                    if (errmsg == "success")
                    {
                        load_stats();
                        load_statsPendingPayment();
                        CommonCode.show_toastr("Success", "Record Deleted Successfully", errmsg, false, "", false, "", ltr_scripts);
                    }
                }
        }
        catch(Exception ex)
        {
            CommonCode.show_toastr("Danger!!!", "Record not Deleted due to some issue.", ex.Message, false, "", false, "", ltr_scripts); 
        }
    }

    public string imgPath, strBookName, strISBN, strReviewHeading, strReviewDesc, strReviewCustomerName, strReviewDate;

    protected void rp_pending_reviews_ItemCommand(object source, GridViewCommandEventArgs e)
    {
        if( e.CommandName.ToString() == "view_review")
        {
            //String ReviewsID = e.CommandArgument.ToString();
            //string errmsgs = "";
            //DAL dal = new DAL();
            //DataTable dts = new DataTable();
            //dts = dal.get_admin_pending_reviews(Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), ReviewsID, out errmsgs);
             
            //if (errmsgs == "success")
            //{
            //    if (dts.Rows.Count > 0)
            //    {
            //        imgPath = dts.Rows[0][];

            //        //rp_review_modals.DataSource = dts;
            //        //rp_review_modals.DataBind();
            //    }
            //}
            //else
            //{
            //    CommonCode.show_toastr("error", "Error while loading Pending Reviews", errmsgs, false, "", false, "", ltr_scripts);
            //}


            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "$('#review-modal-').modal({backdrop: 'static',keyboard: false})", true);
            return;
        }
        String ReviewID = e.CommandArgument.ToString();
        String Action = "Approved";
        SqlCommand com = new SqlCommand("web_insert_edit_delete_approve_reviews", CommonCode.con);
        if (e.CommandName == "apporve_review")
        {
            com.Parameters.Add("@Action", SqlDbType.Int).Value = 3;
        }
        else if (e.CommandName == "delete_review")
        {
            com.Parameters.Add("@Action", SqlDbType.Int).Value = 2;
            Action = "Deleted";
        }
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@ReviewID", SqlDbType.BigInt).Value = ReviewID;
        com.Parameters.Add("@ProductId", SqlDbType.VarChar, 20).Value = "";
        com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = "";
        com.Parameters.Add("@ReviewDesc", SqlDbType.Text).Value = "";
        com.Parameters.Add("@Rating", SqlDbType.Int).Value = 0;
        com.Parameters.Add("@isApporved", SqlDbType.Bit).Value = 1;
        com.Parameters.Add("@icompanyid", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
        com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
        com.Parameters.Add("@ReviewerHeading", SqlDbType.VarChar, 500).Value = "";
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);
        if (errmsg == "success")
        {
            CommonCode.show_toastr("success", "Success", "Review " + Action, false, "", false, "", ltr_scripts);
        }
        else
        {
            CommonCode.show_toastr("error", "Error while " + Action.Substring(0, Action.Length - 2) + "ing review", errmsg, false, "", false, "", ltr_scripts);
        }
        load_pending_reviews();
    }


    

    //private void Email_Send(string EmailID, string Name, string subject, string body, out string mail_err_msg)
    //{
    //    mail_err_msg = "success";
    //    try
    //    {
    //        string txtTo = EmailID;
    //        string txtEmail = "";
    //        string txtPassword = "";
    //        string smtpHostName = "";
    //        Int32 smtpPortNo = 0;
    //        string ssql = " Select Top 1 * from EmailConfig where companyID = '" + CommonCode.CompanyID() + "' ";
    //        SqlCommand com = new SqlCommand(ssql, CommonCode.con);
    //        DataTable dt = new DataTable();
    //        string errmsg;
    //        dt = CommonCode.getData(com, out errmsg);

    //        if (dt.Rows.Count > 0)
    //        {
    //            txtEmail = dt.Rows[0]["SMTPUserid"].ToString();
    //            txtPassword = dt.Rows[0]["SMTPPassword"].ToString().Trim();
    //            smtpHostName = dt.Rows[0]["SMTPHost"].ToString();
    //            smtpPortNo = int.Parse(dt.Rows[0]["SMTPPortNo"].ToString());
    //        }

    //        // string bccid = "myschoolbookstore@gmail.com";
    //        MailMessage mm = new MailMessage();
    //        mm.From = new MailAddress(txtEmail);
    //        mm.To.Add(new MailAddress(txtTo)); //adding multiple TO Email Id  
    //        //mm.Bcc.Add(new MailAddress(bccid));
    //        mm.Subject = subject;
    //        mm.IsBodyHtml = true;
    //        mm.Body = "Dear Mr./Mrs. "+Name+ "<br/><br/>" + body;

    //        SmtpClient smtp = new SmtpClient();

    //        smtp.Host = smtpHostName; //"smtpout.secureserver.net" ;
    //        //smtp.Host = "relay-hosting.secureserver.net";

    //        //UnComment While Updating Online 
    //        smtp.EnableSsl = false;
    //        //Comment While Updating Online and vice versa
    //        //    smtp.EnableSsl = true;
    //        NetworkCredential NetworkCred = new NetworkCredential(txtEmail, txtPassword.Trim());
    //        smtp.UseDefaultCredentials = false;
    //        smtp.Credentials = NetworkCred;
    //        smtp.Port = smtpPortNo; // 80;
    //        smtp.Send(mm);
    //        Session["MailSent"] = "MailSent";
    //        mail_err_msg = "success";
    //    }
    //    catch (Exception ex)
    //    {
    //        mail_err_msg = "";
    //        mail_err_msg += " \n<br/>";
    //        mail_err_msg += ex.Message;
    //        mail_err_msg += " \n<br/>";
    //        mail_err_msg += ex.StackTrace;
    //        //ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "alert('Please contact the developer for this issue<br/>" + err_response+"')", true);
    //    }

    //}


    private void Email_Send(String EmailID, String Name, String refno, string TransactionID, string Amount, out string mail_err_msg)
    {
        mail_err_msg = "";
        string txtTo = EmailID;
        string txtEmail = "";
        string txtPassword = "";
        string smtpHostName = "";
        Int32 smtpPortNo = 0;
        DataTable dt = new DataTable();
        string errmsg;
        string ssql = " Select Top 1 * from EmailConfig where companyID = '" + CommonCode.CompanyID() + "' ";
        try
        {
            SqlCommand com = new SqlCommand(ssql, CommonCode.con);
            dt = CommonCode.getData(com, out errmsg);

            if (dt.Rows.Count > 0)
            {
                txtEmail = dt.Rows[0]["SMTPUserid"].ToString();
                txtPassword = dt.Rows[0]["SMTPPassword"].ToString().Trim();
                smtpHostName = dt.Rows[0]["SMTPHost"].ToString();
                smtpPortNo = int.Parse(dt.Rows[0]["SMTPPortNo"].ToString());
            }

            using (MailMessage mm = new MailMessage())
            {
                mm.From = new MailAddress(txtEmail);
                mm.To.Add(new MailAddress(txtTo)); //adding multiple TO Email Id  
                mm.Subject = "Your Order Confirmation #" + TransactionID + "";
                mm.IsBodyHtml = true;
                mm.Body = "Dear " + Name + "";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "            Thank you for using skkatariaandsons.com ";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "            Your valuable order has been registered against OrderID " + TransactionID;
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "            We are happy to inform you that your payment request has been successfully received and the details are as follows: ";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "<b>Transaction Reference Number :  " + TransactionID + "</b>";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "<b>Payment Reference Number :  " + refno + " and  Date & Time : " + DateTime.Now + "</b>";
                //mm.Body += "<br/>";
                //mm.Body += "<b>Date and Time : " + DateTime.Now + "</b>";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "<b>Transaction Type :  " + "ORDER PAYMENT" + "</b>";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "<b> Amount (USD):  " + Amount + "</b>";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "<b> Transaction Status :  " + "SUCCESS" + "</b> .";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "Best Regards,";
                mm.Body += "<br/>";
                mm.Body += "<br/>";
                mm.Body += "S.K.KATARIA & SONS.";

                SmtpClient smtp = new SmtpClient();

                smtp.Host = smtpHostName; //"smtpout.secureserver.net" ;
                                          //smtp.Host = "relay-hosting.secureserver.net";
                smtp.EnableSsl = false;
                //Comment While Updating Online and vice versa
                //    smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(txtEmail, txtPassword.Trim());
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = smtpPortNo; // 80;
                smtp.Send(mm);
                Session["MailSent"] = "MailSent";
                mail_err_msg = "success";
            }
        }
        catch (Exception ex)
        {
            mail_err_msg = "Email not sent due to " + ex.Message;
        }
    }


    protected void btn_PendingOrderGet_Click(object sender, EventArgs e)
    {
        load_statsPendingPayment( txtPendingOrder.Text.ToString().Trim());
    }
}