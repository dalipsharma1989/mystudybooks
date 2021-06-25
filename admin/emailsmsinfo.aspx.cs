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
        ltr_msg.Text = "";
        ltr_scripts.Text = "";
        if (Session["AdminUserName"] == null || Session["AdminUserName"].ToString() == "")
        {
            Response.Redirect("../admin/");
        }
        if (!IsPostBack)
        {
            load_info();
        }
    }

    private void load_info()
    {
        string info_errmsg = "", mail_errmsg = "";
        DataRow rr = CommonCode.fetch_emailsms_info(Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out info_errmsg);
        if (info_errmsg == "success")
        {
            textEmailHeaderName.Text = rr["DisplaySenderName"] + "";
            textEmailID.Text = rr["EmailFrom"] + "";
            textPassword.Text = rr["EmailPassword"].ToString().Trim() + "";
            textPort.Text = rr["SMTPPort"] + "";
            textHost.Text = rr["SMTPHost"] + "";
            textPassword.Attributes["value"] = textPassword.Text; 

            int enablessl;
            if (!string.IsNullOrEmpty(rr["EnableSsl"] + ""))
            {
                enablessl = Convert.ToInt32(rr["EnableSsl"]);
            }
            else
            {
                enablessl = 0;
            } 
            if (enablessl == 0)
            {
                cb_enablessl.Checked = false;
            }
            else
            {
                cb_enablessl.Checked = true;
            } 
            //textSmsURL.Text = "";// rr["SmsURL"] + "";
            //textSmsUserName.Text = "";// rr["SmsUserName"] + "";
            //textSmsPass.Text = "";// rr["SmsPass"] + "";
            //textSmsSenderID.Text = ""; // rr["SmsSenderID"] + "";
            //hf_is_data_present.Value = "true";
        }
        else if (info_errmsg == "No Email Info found! ")
        {
            hf_is_data_present.Value = "false";
            CommonCode.show_alert("warning", "Warning", info_errmsg, ltr_msg);
        }
        else
        {
            hf_is_data_present.Value = "false";
            CommonCode.show_alert("danger", "Error while loading Email Information!", info_errmsg, ltr_msg);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlCommand com = new SqlCommand("dbo_insert_edit_delete_emailsmsinfo", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        if (hf_is_data_present.Value == "true")
        {
            //UPDATE
            com.Parameters.Add("@action", SqlDbType.Int).Value = 1;
        }
        else if (hf_is_data_present.Value == "false")
        {
            //INSERT
            com.Parameters.Add("@action", SqlDbType.Int).Value = 0;
        }

        string enablessl = (cb_enablessl.Checked ? "1" : "0");

        com.Parameters.Add("@EmailHeaderName", SqlDbType.VarChar, 500).Value = textEmailHeaderName.Text;
        com.Parameters.Add("@EmailID", SqlDbType.VarChar, 500).Value = textEmailID.Text;
        com.Parameters.Add("@Password", SqlDbType.VarChar, 1000).Value = textPassword.Text;
        com.Parameters.Add("@Port", SqlDbType.Int).Value = textPort.Text;
        com.Parameters.Add("@Host", SqlDbType.VarChar, 500).Value = textHost.Text;
        com.Parameters.Add("@EnableSSL", SqlDbType.Int).Value = enablessl;
        com.Parameters.Add("@SmsURL", SqlDbType.VarChar, 500).Value = textSmsURL.Text;
        com.Parameters.Add("@SmsUserName", SqlDbType.VarChar, 500).Value = textSmsUserName.Text;
        com.Parameters.Add("@SmsPass", SqlDbType.VarChar, 500).Value = textSmsPass.Text;
        com.Parameters.Add("@SmsSenderID", SqlDbType.VarChar, 500).Value = textSmsSenderID.Text;
        com.Parameters.Add("@UDF1", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@UDF2", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@UDF3", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@UDF4", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@UDF5", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@UDF6", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@UDF7", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@UDF8", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@UDF9", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@UDF10", SqlDbType.NVarChar).Value = "";
        com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
        string errmsg;
        errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
        if (errmsg == "success")
        {
            CommonCode.show_alert("success", "Data Saved", "Email Info saved successfully! ", false, ltr_msg);
            //clear_form();
            load_info();
        }
        else
        {
            CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
        }
    }

    private void clear_form()
    {
        textEmailHeaderName.Text = "";
        textEmailID.Text = "";
        textPassword.Text = "";
        textPort.Text = "";
        textHost.Text = "";
        textSmsURL.Text = "";
        textSmsUserName.Text = "";
        textSmsPass.Text = "";
        textSmsSenderID.Text = "";
    }

    protected void btn_sendEmail_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(text_toemail.Text))
        {
            CommonCode.show_toastr("error", "Empty Field !", "Please provide Email to which Test Message can be sent", false, "", false, "", ltr_scripts);
            return;
        }
        else
        {
            string info_errmsg = "", mail_errmsg = "";
            DataRow rr = CommonCode.fetch_emailsms_info(Session["iCompanyID"].ToString(), Session["iBranchID"].ToString(), out info_errmsg);
            if (info_errmsg == "success")
            {
                Mail mail = new Mail();
                string html = "<div style='text-center;padding:15px;margin:5px;border:4px double #000'>";
                html += "<h1>Spring Time Software - Book Store Admin Panel</h1>";
                html += "<h2>Lorem Ipsum</h2>";
                html += "<h3><em>Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...</em></h3>";
                html += "<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>";
                html += "<p>This is Test Mail message from '" + rr["EmailID"] + "' sent by Spring Time Software - Book Store Admin Panel</p>";
                html += "</div>";
                mail.send_new_mail(rr, text_toemail.Text, html, "Test Mail from " + rr["EmailID"] + " - Powered by Spring Time Software Online Book Store", out mail_errmsg);
                if (mail_errmsg == "success")
                    CommonCode.show_toastr("success", "Mail sent !", "Mail successfully sent to " + text_toemail.Text.Replace("'", "") + ". Please check your mail.", false, "", false, "", ltr_scripts);
                else
                    CommonCode.show_toastr("error", "Unable to send mail", mail_errmsg, false, "", false, "", ltr_scripts);
            }
        }
    }

    protected void btn_sendSMS_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(text_tono.Text))
        {
            CommonCode.show_toastr("error", "Empty Field !", "Please provide Mobile No to which Test Message can be sent", false, "", false, "", ltr_scripts);
            return;
        }
        else
        {
            CommonCode.show_toastr("info", "SMS Sending API is not working !", "SMS API is suspended due to incomplete API details. Contact STS.", false, "", false, "", ltr_scripts);
        }
    }
}