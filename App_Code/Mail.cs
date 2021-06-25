using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for Mail
/// </summary>
public class Mail
{
    public void send_new_mail(DataRow rr_info, string mailTo, string Body, string Subject, out string errmsg)
    {
        try
        {
            bool enablessl;
            if (!string.IsNullOrEmpty(rr_info["EnableSSL"] + ""))
                enablessl = Convert.ToBoolean(rr_info["EnableSSL"]);
            else
                enablessl = false;

            SmtpClient Smtp_Server = new SmtpClient();
            MailMessage e_mail;
            Smtp_Server.UseDefaultCredentials = false;
            Smtp_Server.Credentials = new NetworkCredential(rr_info["EmailID"] + "", rr_info["Password"] + "");
            Smtp_Server.Port = Convert.ToInt32(rr_info["Port"] + "");
            Smtp_Server.EnableSsl = enablessl;
            Smtp_Server.Host = rr_info["Host"] + "";

            e_mail = new MailMessage();
            e_mail.From = new MailAddress(rr_info["EmailID"] + "", rr_info["EmailHeaderName"] + "");
            e_mail.To.Add(mailTo);
            e_mail.Subject = Subject;
            e_mail.IsBodyHtml = true;
            e_mail.Body = Body;
            Smtp_Server.Send(e_mail);
            errmsg = "success";
        }
        catch (Exception ex)
        {
            errmsg = ex.Message ;
        }

        //try
        //{
        //    MailMessage mailObj = new MailMessage(new MailAddress(rr_info["EmailID"] + "", rr_info["EmailHeaderName"] + ""), new MailAddress(mailTo));
        //    mailObj.Subject = Subject;
        //    mailObj.Body = Body;
        //    mailObj.IsBodyHtml = true;
        //    SmtpClient SMTPServer = new SmtpClient(rr_info["Host"] + "", Convert.ToInt32(rr_info["Port"] + ""));
        //    SMTPServer.Credentials = new System.Net.NetworkCredential(rr_info["EmailID"] + "", rr_info["Password"] + "");
        //    SMTPServer.Send(mailObj);
        //    errmsg = "success";
        //}
        //catch (Exception ex)
        //{
        //    errmsg = ex.Message;
        //}
    }
}

/*
try
        {
            String EmailSenderID = "anoop@springtimesoftware.net";
            String EmailSenderPwd = "signature@123";
            Int16 EmailPort = 25;
            String EmailHost = "smtp.springtimesoftware.net";
            SmtpClient Smtp_Server = new SmtpClient();
            MailMessage e_mail;
            Smtp_Server.UseDefaultCredentials = false;
            Smtp_Server.Credentials = new NetworkCredential(EmailSenderID, EmailSenderPwd);
            Smtp_Server.Port = EmailPort; //587;
            Smtp_Server.EnableSsl = false;
            Smtp_Server.Host = EmailHost; //"smtp.gmail.com";

            e_mail = new MailMessage();
            e_mail.From = new MailAddress(EmailSenderID);
            e_mail.To.Add(strTo);
            e_mail.Subject = strSubject;
            e_mail.IsBodyHtml = true;
            e_mail.Body = strBody;
            Smtp_Server.Send(e_mail);
            errmsg = "success";
        }
        catch (Exception ex)
        {
            errmsg = ex.Message ;
        }
*/
