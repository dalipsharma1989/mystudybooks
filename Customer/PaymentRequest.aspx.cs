﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using System.Configuration;

public partial class _PaymentRequest : System.Web.UI.Page
    { 
        CCACrypto ccaCrypto = new CCACrypto();
    string workingKey = ConfigurationManager.AppSettings["WorkingKey"];// "EF8D27393CBFFD5DB36B5DD3BCD25A0A";//put in the 32bit alpha numeric key in the quotes provided here 	
        string ccaRequest = "";
        public string strEncRequest="";
        public string strAccessCode = ConfigurationManager.AppSettings["AccessCode"]; // "X7AC119SH9X0LCNZ";// put the access key in the quotes provided here.
    protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
            {
               foreach (string name in Request.Form)
                {
                    if (name != null)
                    {
                        if (!name.StartsWith("_"))
                        {
                            ccaRequest = ccaRequest + name + "=" + Request.Form[name] + "&";
                          /* Response.Write(name + "=" + Request.Form[name]);
                            Response.Write("</br>");*/
                        }
                    }
                }
                strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey); 
            }
        }
    }

