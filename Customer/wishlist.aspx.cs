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
        this.Title = CommonCode.SetPageTitle("wishlist");
        if (Session["CustID"] != null && Session["CustType"] != null && Session["CustID"].ToString() != "guest")
        {
            if (!IsPostBack)
            {
                load_wishlist_products();
            }
        }
        else
        {
            if (Session["tmpwishlist"] != null)
            {
                if (!IsPostBack)
                {
                    load_temp_wishlist();
                }
            }
            else
                Response.Redirect("user_login.aspx", true);
        }
    }

    public void load_wishlist_products()
    {

        SqlCommand com = new SqlCommand("Web_Get_Wishlist", CommonCode.con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CustID", SqlDbType.VarChar,30).Value = Session["CustID"].ToString();
        com.Parameters.Add("@iCompanyId", SqlDbType.Int).Value = Session["iCompanyID"].ToString();
        com.Parameters.Add("@iBranchId", SqlDbType.Int).Value = Session["iBranchID"].ToString();
        DataTable dt = new DataTable();
        string errmsg;
        dt = CommonCode.getData(com, out errmsg);

        div_user_wishlist.Visible = true;
        if (dt.Rows.Count > 0)
        {
            rp_wishlist.DataSource = dt;
            rp_wishlist.DataBind();

        }
        else
        {
            div_user_wishlist.Visible = false;
            CommonCode.show_alert("danger", "Your Wishlist is Empty", "<a href='/index.aspx' class='btn btn-link'>Click here select products</a>", false, ltr_msg);
        }
    }


    public void load_temp_wishlist()
    {
        DataTable dt_tmp_wishlist = new DataTable();
        if (Session["tmpwishlist"] != null)
        {
            DataTable ddt = new DataTable();
            SqlCommand com1 = new SqlCommand("select 'tmpwishlist' WishlistID, mp.ProductID ,mp.ISBN, mp.BookName, mp.SaleCurrency,mp.SalePrice Price, " +
                " 0 Qty, 0 TotalAmt from MasterProduct mp  where 1=2", CommonCode.con);
            string errmsg;
            ddt = CommonCode.getData(com1, out errmsg);
            dt_tmp_wishlist = HttpContext.Current.Session["tmpwishlist"] as DataTable;
            div_user_wishlist.Visible = true;
            if (dt_tmp_wishlist.Rows.Count > 0)
            {
                foreach (DataRow rr in dt_tmp_wishlist.Rows)
                {
                    DataTable dt = new DataTable();
                    SqlCommand com = new SqlCommand("select 'tmpcart' WishlistID, mp.ProductID ,mp.ISBN, mp.BookName, mp.SaleCurrency,mp.SalePrice Price " +
                                                    "from MasterProduct mp " +
                                                    "where ProductID =@ProductID", CommonCode.con);
                    com.Parameters.Add("@ProductID", SqlDbType.BigInt).Value = rr["ProductID"];
                    dt = CommonCode.getData(com, out errmsg);
                    if (errmsg == "success")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataRow r1 = dt.Rows[0];
                            ddt.Rows.Add(r1.ItemArray);
                        }
                    }
                    else
                    {
                        CommonCode.show_alert("danger", "Error", errmsg, false, ltr_msg);
                    }
                }
                if (ddt.Rows.Count > 0)
                {  
                    rp_wishlist.DataSource = ddt;
                    rp_wishlist.DataBind();
                }
            }
            else
            {
                div_user_wishlist.Visible = false;
                CommonCode.show_alert("danger", "Your wishlist is Empty", "<a href='/index.aspx' class='btn btn-link'>Click here select products</a>", false, ltr_msg);
            }
        }
        else
        {
            div_user_wishlist.Visible = false;
            CommonCode.show_alert("danger", "Your wishlist is Empty", "<a href='/index.aspx' class='btn btn-link'>Click here select products</a>", false, ltr_msg);
        }
    }

    public void delete_wishlist( String ProductID, String TypeInfo)
    {
        String errmsg = "success";

        if (ProductID == "" )
        {
            SqlCommand com = new SqlCommand("Web_delete_item_from_Wishlist", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = Session["CustID"].ToString();
            com.Parameters.Add("@ProductID", SqlDbType.VarChar, 20).Value = "";
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                load_wishlist_products();
                ltr_scripts.Text = "<script>toastr.success('Items Removed from Wishlist ! ', 'Item Removed');</script>";
            }
            else
            {
                ltr_scripts.Text = "<script>toastr.error('" + errmsg.Replace('\'', ' ') + "', 'Wishlist Error !');</script>";
            }
        }
        else
        {
            SqlCommand com = new SqlCommand("Web_delete_item_from_Wishlist", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@CustID", SqlDbType.VarChar, 30).Value = Session["CustID"].ToString();
            com.Parameters.Add("@ProductID", SqlDbType.VarChar, 20).Value = ProductID;
            com.Parameters.Add("@iCompanyID", SqlDbType.Int).Value = Session["iCompanyId"].ToString();
            com.Parameters.Add("@iBranchID", SqlDbType.Int).Value = Session["iBranchID"].ToString();
            errmsg = CommonCode.ExecuteNoQuery(com, CommonCode.con);
            if (errmsg == "success")
            {
                load_wishlist_products();
                if(TypeInfo.Trim() == "Cart")
                {
                    ltr_scripts.Text = "<script>toastr.success('Item Removed from Wishlist and moved to Cart ! ', 'Item Removed');</script>";
                }
                else
                {
                    ltr_scripts.Text = "<script>toastr.success('Item Removed from Wishlist ! ', 'Item Removed');</script>";
                }
                
            }
            else
            {
                ltr_scripts.Text = "<script>toastr.error('" + errmsg.Replace('\'', ' ') + "', 'Wishlist Error !');</script>";
            }
        }
        

    }


    protected void rp_wishlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
                
        String ProductID = e.CommandArgument.ToString();
           if (e.CommandName == "addtoCart")
            {
                   add_item_to_cart(ProductID);
                if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
                {
                    Response.Redirect("/Customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
                }
                else
                {
                    delete_wishlist(ProductID, "Cart");
                }

            }    
           else if (e.CommandName == "remove_item_from_wishlist")
            {

                    if (Session["CustID"] == null || Session["CustID"].ToString() == "guest")
                    {
                        Response.Redirect("/Customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
                    }
                    else
                    {
                        delete_wishlist(ProductID, "Wishlist");
                        //ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Item successfully removed from your Wish list');", true);
                    }            

            }     

            (this.Master as CustomerMaster).load_Header();
    }

    protected void btn_clear_wishlist_Click(object sender, EventArgs e)
    {
        if (Session["CustID"] != null && Session["CustID"].ToString() == "guest")
        {
            Response.Redirect("/Customer/user_login.aspx?returnto=" + HttpUtility.UrlEncode(Request.RawUrl), true);
        }
        else
        {
            delete_wishlist("", "Wishlist");
        }


    }

    private void add_item_to_cart(String ProductID)
    {
        String errmsg = "success";
        String CustID = "";
        if (Session["CustID"] != null)
        {
            CustID = Session["CustID"].ToString();
        }
        String CartID = "";
        errmsg = CommonCode.AddToCart(CustID, ProductID, Session["iCompanyId"].ToString(), Session["iBranchID"].ToString(), out CartID);
        if (errmsg == "success")
        {
            ltr_scripts.Text = "<script>" +
                "toastr.options = {closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000}; " +
                "toastr.options.onclick = function () {window.location.replace('Customer/user_cart.aspx');};" +
            "toastr.success('Successfuly added to cart ! ', 'Cart');</script>";
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else if (errmsg == "already exist")
        {
            ltr_scripts.Text = "<script>" +
                "toastr.options = {closeButton: true,progressBar: true,showMethod: 'slideDown',timeOut:4000}; " +
                "toastr.options.onclick = function () {window.location.replace('Customer/user_cart.aspx');};" +
            "toastr.info(' Already in cart ! ', 'Cart');</script>";
            if (!string.IsNullOrEmpty(CartID))
                Session["CartID"] = CartID;
        }
        else
        {
            ltr_scripts.Text = "<script>toastr.error('" + errmsg.Replace('\'', ' ') + "', 'Cart Error !');</script>";
            //exception
        }
    }
}