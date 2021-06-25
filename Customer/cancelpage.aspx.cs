using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Customer_cancelpage : System.Web.UI.Page
{
  

    protected void Page_Load(object sender, EventArgs e)
    {
        string ORDID = "";
        try
        {            
            
                if (Session["CustID"] != null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["orderID"]))
                        {
                            ORDID = Request.QueryString["orderID"].ToString();
                            RetrieveCart(ORDID);
                        }
                    else
                        {
                            Response.Redirect("user_cart.aspx", true);
                        }
                }
                else
                {
                    Response.Redirect("user_login.aspx?session_expired=true", true);
                }
          
        }
        catch  
        {
            Response.Redirect("user_cart.aspx", true);
        }
    }
    private void RetrieveCart(string OrderID)  
    {
        try
        {
            string errmsg = "";            
                SqlCommand com = new SqlCommand("Web_Release_Cart", CommonCode.con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@OrderID", SqlDbType.BigInt).Value = OrderID;
                errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
                Response.Redirect("user_cart.aspx",true);            
        }
        catch 
        {                        
            Response.Redirect("user_cart.aspx", true);
        }
    }
}