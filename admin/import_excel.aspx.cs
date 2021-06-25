using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Text;
using OfficeOpenXml;

public partial class _Default : System.Web.UI.Page
{
    DataTable errdt = new DataTable();
    int type = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ltr_err_msg.Text = "";
        ltr_script.Text = "";

        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            if (Request.QueryString["type"] == "students")
            {
                type = 0;
                a_donwload_file.HRef = @"\resources\SampleStudents.xlsx";
            }
            else if (Request.QueryString["type"] == "schools")
            {
                type = 1;
                a_donwload_file.HRef = @"\resources\SampleSchools.xlsx";
            }
            else if (Request.QueryString["type"] == "books")
            {
                type = 2;
                a_donwload_file.HRef = @"\resources\SampleBooks.xlsx";
            }
            else if (Request.QueryString["type"] == "book_relation")
            {
                type = 3;
                a_donwload_file.HRef = @"\resources\SampleBookRelations.xlsx";
            }

            else if (Request.QueryString["type"] == "ShippingCharges")
            {
                type = 4;
                a_donwload_file.HRef = @"\resources\SampleShippingCharges.xlsx";
            }
            else
            {
                Response.Redirect("404.html");
            }
        }
        else
        {
            Response.Redirect("adminhome.aspx");
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
            DataTable dt = new DataTable();
            dt = package.ToDataTable();

            errdt.Columns.Add("Id", typeof(String));
            errdt.Columns.Add("isSuccess", typeof(Boolean));
            errdt.Columns.Add("Message", typeof(String));
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow rr in dt.Rows)
                {
                    String ID, message;
                    Boolean isSuccess;

                    save_in_db(rr, out ID, out isSuccess, out message);
                    errdt.Rows.Add(ID, isSuccess, message);
                }
            }
            else
            {
                CommonCode.show_alert("danger", "Empty Excel File!", "Please, make sure your excel file must contain validated data.", ltr_err_msg);
                return;
            }

            if (errdt.Rows.Count > 0)
            {
                String caption;
                Int32 totalcount = 0;
                Int32 successcount = 0;
                Int32 unsuccesscount = 0;
                totalcount = errdt.Rows.Count;
                caption = "<font color='#286090' > Import Report :- Total Rows - <b>" + totalcount + "</b></font><br>";
                foreach (DataRow rr in errdt.Rows)
                {
                    if (Convert.ToBoolean(rr["isSuccess"]))
                    {
                        successcount += 1;
                    }
                }
                unsuccesscount = totalcount - successcount;
                caption += " <font color='#398439' >Successful Rows - <b>" + successcount + "</b></font><br>";
                caption += " <font color='#d9534f' >Failed Rows - <b>" + unsuccesscount + "</b></font>";
                GridView2.Caption = caption;
            }

            GridView2.DataSource = errdt;
            GridView2.DataBind();

            ltr_script.Text = "<script>toastr.success('Excel file imported successfuly ! ', 'Import Excel');</script>";
        }
        catch (Exception ex)
        {
            CommonCode.show_alert("danger", ex.Message, ex.StackTrace, ltr_err_msg);
            ltr_script.Text = "<script>toastr.error('" + ex.Message + "', 'Error');</script>";
        }
    }

    private void save_in_db(DataRow rr, out String ID, out Boolean isSuccess, out String message)
    {
        isSuccess = true;
        message = "success";
        ID = "1";

        if (type == 0)
        {
            //students
            String Password = System.Web.Security.Membership.GeneratePassword(5, 1);
            String EncryptedPassword = cryptography.Encrypt(Password);
            byte[] UserPassword = Encoding.UTF8.GetBytes(EncryptedPassword);
            SqlCommand com = new SqlCommand("dbo_insert_student", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@CustName", SqlDbType.VarChar, 200).Value = rr["CustName"].ToString();
            com.Parameters.Add("@UserPassword", SqlDbType.VarBinary, 500).Value = UserPassword;
            com.Parameters.Add("@BillingAddress", SqlDbType.VarChar, 1000).Value = rr["BillingAddress"].ToString();
            com.Parameters.Add("@BillingCityID", SqlDbType.VarChar, 10).Value = rr["BillingCityID"].ToString();
            com.Parameters.Add("@BillingPostalCode", SqlDbType.VarChar, 100).Value = rr["BillingPostalCode"].ToString();
            com.Parameters.Add("@BillingPhone", SqlDbType.VarChar, 100).Value = rr["BillingPhone"].ToString();
            com.Parameters.Add("@Mobile", SqlDbType.VarChar, 10).Value = rr["Mobile"].ToString();
            com.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = rr["EmailID"].ToString();
            com.Parameters.Add("@SchoolID", SqlDbType.BigInt).Value = rr["SchoolID"].ToString();
            com.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = rr["ClassID"].ToString();
            com.Parameters.Add("@SectionID", SqlDbType.BigInt).Value = rr["SectionID"].ToString();
            com.Parameters.Add("@RollNo", SqlDbType.VarChar, 25).Value = rr["RollNo"].ToString();
            com.Parameters.Add("@AgeGroupID", SqlDbType.Int).Value = rr["AgeGroupID"].ToString();
            com.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = rr["Remark"].ToString();

            try
            {
                string msg = CommonCode.ExecuteNoQuery(com, CommonCode.con);

                if (msg == "success")
                {
                    isSuccess = true;
                    message = "Success";
                    ID = rr["RollNo"].ToString() + " - " + rr["CustName"].ToString();
                }
                else
                {
                    isSuccess = false;
                    message = msg;
                    ID = rr["RollNo"].ToString() + " - " + rr["CustName"].ToString();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
                ID = rr["RollNo"].ToString() + " - " + rr["CustName"].ToString();
            }

        }
        else if (type == 1)
        {
            //schools
            String Password = System.Web.Security.Membership.GeneratePassword(8, 2);
            String EncryptedPassword = cryptography.Encrypt(Password);
            byte[] UserPassword = Encoding.UTF8.GetBytes(EncryptedPassword);
            SqlCommand com = new SqlCommand("dbo_insert_school", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@Prefix", SqlDbType.VarChar, 200).Value = rr["Prefix"].ToString();
            com.Parameters.Add("@CustName", SqlDbType.VarChar, 200).Value = rr["CustName"].ToString();
            com.Parameters.Add("@UserPassword", SqlDbType.VarBinary, 500).Value = UserPassword;
            com.Parameters.Add("@BillingAddress", SqlDbType.VarChar, 1000).Value = rr["BillingAddress"].ToString();
            com.Parameters.Add("@BillingCityID", SqlDbType.VarChar, 10).Value = rr["BillingCityID"].ToString();
            com.Parameters.Add("@BillingPostalCode", SqlDbType.VarChar, 100).Value = rr["BillingPostalCode"].ToString();
            com.Parameters.Add("@BillingPhone", SqlDbType.VarChar, 100).Value = rr["BillingPhone"].ToString();
            com.Parameters.Add("@BillingFaxNo", SqlDbType.VarChar, 100).Value = rr["BillingFaxNo"].ToString();
            com.Parameters.Add("@Mobile", SqlDbType.VarChar, 10).Value = rr["Mobile"].ToString();
            com.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = rr["EmailID"].ToString();
            com.Parameters.Add("@Website", SqlDbType.VarChar, 200).Value = rr["Website"].ToString();
            com.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = rr["Remark"].ToString();

            try
            {
                string msg = CommonCode.ExecuteNoQuery(com, CommonCode.con);

                if (msg == "success")
                {
                    isSuccess = true;
                    message = "Success";
                    ID = rr["CustName"].ToString();
                }
                else
                {
                    isSuccess = false;
                    message = msg;
                    ID = rr["CustName"].ToString();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
                ID = rr["CustName"].ToString();
            }
        }
        else if (type == 2)
        {
            //books
            SqlCommand com = new SqlCommand("dbo_insert_edit_Master_Product", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@ProductId", SqlDbType.BigInt).Value = 0;
            com.Parameters.Add("@ISBN", SqlDbType.VarChar, 20).Value = rr["ISBN"].ToString();
            com.Parameters.Add("@BookName", SqlDbType.VarChar, 250).Value = rr["BookName"].ToString();
            com.Parameters.Add("@ISBN2", SqlDbType.VarChar, 20).Value = rr["ISBN2"].ToString();
            com.Parameters.Add("@ISBN3", SqlDbType.VarChar, 20).Value = rr["ISBN3"].ToString();
            com.Parameters.Add("@ISBN4", SqlDbType.VarChar, 20).Value = rr["ISBN4"].ToString();
            com.Parameters.Add("@SKU", SqlDbType.VarChar, 50).Value = rr["SKU"].ToString();
            com.Parameters.Add("@LanguageID", SqlDbType.VarChar, 15).Value = rr["LanguageID"].ToString();
            com.Parameters.Add("@BookShortName", SqlDbType.VarChar, 100).Value = rr["BookShortName"].ToString();
            com.Parameters.Add("@Publisher", SqlDbType.VarChar, 20).Value = rr["Publisher"].ToString();
            com.Parameters.Add("@Author", SqlDbType.VarChar, 100).Value = rr["Author"].ToString();
            com.Parameters.Add("@DisplaySubJect", SqlDbType.VarChar, 500).Value = rr["DisplaySubJect"].ToString();
            com.Parameters.Add("@DisplaySubSubject", SqlDbType.VarChar, 500).Value = rr["DisplaySubSubject"].ToString();
            com.Parameters.Add("@SalePrice", SqlDbType.Decimal).Value = (string.IsNullOrEmpty(rr["SalePrice"].ToString()) ? (object)DBNull.Value : rr["SalePrice"].ToString());
            com.Parameters.Add("@SaleCurrency", SqlDbType.VarChar, 7).Value = rr["SaleCurrency"].ToString();
            com.Parameters.Add("@SaleDiscount", SqlDbType.Decimal).Value = (string.IsNullOrEmpty(rr["SaleDiscount"].ToString()) ? (object)DBNull.Value : rr["SaleDiscount"].ToString());
            com.Parameters.Add("@SaleAddDiscount", SqlDbType.Decimal).Value = (string.IsNullOrEmpty(rr["SaleAddDiscount"].ToString()) ? (object)DBNull.Value : rr["SaleAddDiscount"].ToString());
            com.Parameters.Add("@Clbal", SqlDbType.BigInt).Value = (string.IsNullOrEmpty(rr["Clbal"].ToString()) ? (object)DBNull.Value : rr["Clbal"].ToString());
            com.Parameters.Add("@PubClbal", SqlDbType.BigInt).Value = (string.IsNullOrEmpty(rr["PubClbal"].ToString()) ? (object)DBNull.Value : rr["PubClbal"].ToString());
            com.Parameters.Add("@TmpQty", SqlDbType.BigInt).Value = (string.IsNullOrEmpty(rr["TmpQty"].ToString()) ? (object)DBNull.Value : rr["TmpQty"].ToString());
            com.Parameters.Add("@CurrentOrder", SqlDbType.VarChar, 10).Value = rr["CurrentOrder"].ToString();
            com.Parameters.Add("@Weight", SqlDbType.Int).Value = (string.IsNullOrEmpty(rr["Weight"].ToString()) ? (object)DBNull.Value : rr["Weight"].ToString());
            com.Parameters.Add("@Volume", SqlDbType.VarChar, 5).Value = rr["Volume"].ToString();
            com.Parameters.Add("@Edition", SqlDbType.VarChar, 5).Value = rr["Edition"].ToString();
            com.Parameters.Add("@TotalPages", SqlDbType.VarChar, 5).Value = rr["TotalPages"].ToString();
            com.Parameters.Add("@PublishYear", SqlDbType.Int).Value = (string.IsNullOrEmpty(rr["PublishYear"].ToString()) ? (object)DBNull.Value : rr["PublishYear"].ToString());
            com.Parameters.Add("@Binding", SqlDbType.VarChar, 10).Value = rr["Binding"].ToString();
            com.Parameters.Add("@Taxable", SqlDbType.Bit).Value = (string.IsNullOrEmpty(rr["Taxable"].ToString()) ? (object)DBNull.Value : rr["Taxable"].ToString());
            com.Parameters.Add("@TaxID", SqlDbType.VarChar, 7).Value = rr["TaxID"].ToString();
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier).Value = CommonCode.CompanyID();
            com.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 15).Value = "admin";
            com.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 15).Value = "admin";
            com.Parameters.Add("@FromPub", SqlDbType.Bit).Value = (string.IsNullOrEmpty(rr["FromPub"].ToString()) ? (object)DBNull.Value : rr["FromPub"].ToString());
            com.Parameters.Add("@AvgRating", SqlDbType.TinyInt).Value = (string.IsNullOrEmpty(rr["AvgRating"].ToString()) ? (object)DBNull.Value : rr["AvgRating"].ToString());
            com.Parameters.Add("@action", SqlDbType.Int).Value = 0;
            try
            {
                string msg = CommonCode.ExecuteNoQuery(com, CommonCode.con);

                if (msg == "success")
                {
                    isSuccess = true;
                    message = "Success";
                    ID = rr["ISBN"].ToString() + " - " + rr["BookName"].ToString();
                }
                else
                {
                    isSuccess = false;
                    message = msg;
                    ID = rr["ISBN"].ToString() + " - " + rr["BookName"].ToString();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
                ID = rr["ISBN"].ToString() + " - " + rr["BookName"].ToString();
            }
        }

        else if (type == 3)
        {
            //books
            SqlCommand com = new SqlCommand("dbo_import_book_relation", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@ISBN", SqlDbType.VarChar, 20).Value = rr["ISBN"].ToString();
            com.Parameters.Add("@Category", SqlDbType.VarChar, 500).Value = rr["Category"].ToString();
            com.Parameters.Add("@SubCategory", SqlDbType.VarChar, 500).Value = (string.IsNullOrEmpty(rr["SubCategory"].ToString()) ? (object)DBNull.Value : rr["SubCategory"].ToString());
            com.Parameters.Add("@Subject", SqlDbType.VarChar, 500).Value = rr["Subject"].ToString();
            com.Parameters.Add("@Class", SqlDbType.VarChar, 500).Value = rr["Class"].ToString();
            com.Parameters.Add("@CompanyID", SqlDbType.UniqueIdentifier ).Value = CommonCode.CompanyID();
            try
            {
                string msg = CommonCode.ExecuteNoQuery(com, CommonCode.con);

                if (msg == "success")
                {
                    isSuccess = true;
                    message = "Success";
                    ID = rr["ISBN"].ToString();
                }
                else
                {
                    isSuccess = false;
                    message = msg;
                    ID = rr["ISBN"].ToString();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
                ID = rr["ISBN"].ToString();
            }
        }

        else if (type == 4)
        {
            //books
            SqlCommand com = new SqlCommand("dbo_import_ShippingCityWise", CommonCode.con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@CityID", SqlDbType.BigInt).Value = rr["CityID"].ToString();
            com.Parameters.Add("@cityName", SqlDbType.VarChar, 50).Value = rr["cityName"].ToString();
            com.Parameters.Add("@FromWeight", SqlDbType.Decimal).Value = rr["FromWeight"].ToString();
            com.Parameters.Add("@ToWeight", SqlDbType.Decimal).Value = rr["ToWeight"].ToString();
            com.Parameters.Add("@ShippingAmount", SqlDbType.Decimal).Value = rr["ShippingAmount"].ToString();            
            try
            {
                string msg = CommonCode.ExecuteNoQuery(com, CommonCode.con);

                if (msg == "success")
                {
                    isSuccess = true;
                    message = "Success";
                    ID = rr["CityID"].ToString() ;
                }
                else
                {
                    isSuccess = false;
                    message = msg;
                    ID = rr["CityID"].ToString();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
                ID = rr["CityID"].ToString();
            }
        }
    }


    private bool validate_excel(DataTable dt, string type, out string report)
    {
        report = "";
        bool ok = false;
        int fail_count = 0;

        if (type == "books")
        {
            if ((dt.Columns.Contains("BookCode") & dt.Columns.Contains("BookName") & dt.Columns.Contains("Subject") & dt.Columns.Contains("Author") & dt.Columns.Contains("Rate")))
            {
                dynamic count = 0;
                dynamic rownos = "";
                foreach (DataRow rr in dt.Rows)
                {
                    count += 1;
                    if (string.IsNullOrEmpty(rr["BookCode"].ToString()) | string.IsNullOrEmpty(rr["BookName"].ToString()))
                    {
                        fail_count += 1;
                        report += "<li>" + (string.IsNullOrEmpty(rr["BookCode"].ToString()) ? "Row No -" + count : rr["BookCode"].ToString());
                        report += (string.IsNullOrEmpty(rr["BookName"].ToString()) ? "Row No -" + count : rr["BookName"].ToString()) + " has BookName or BookCode empty </li>";
                        //rr("BookCode").ToString & "</li>"
                        rownos += count + ", ";
                    }
                }

                if (fail_count > 0)
                {
                    ok = false;
                    report += "<li>" + fail_count + " rows have empty BookCode and BookName</li>";
                    report += "<li>" + rownos.Remove(rownos.Length - 2, 2) + "</li>";
                }
                else
                {
                    ok = true;
                    report = "success";
                }
            }
            else
            {
                ok = false;
                report += "<li> To Import Title your excel must contain <b>BookCode, BookName, Subject, Author and Rate</b> Columns along with valid data.</li>";
            }
        }
        return ok;
    }

    public bool validate_fields(string mode, object field)
    {
        bool ok = false;
        if (mode == "Int")
        {
            int i = 0;
            if (int.TryParse(field.ToString(), out i))
            {
                ok = true;
            }
            else
            {
                ok = false;
            }
        }
        if (mode == "Date")
        {
            DateTime Temp;
            if (DateTime.TryParse(field.ToString(), out Temp) == true)
                ok = true;
            else
                ok = false;
        }
        return ok;
    }
}