<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlaceOrder_withoutpay.aspx.cs" Inherits="_payment_gatewaywithoutpay" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>Processing Payment...</title>
    <link rel="stylesheet" href="/css/bootstrap.css" />
    <%--<link rel="stylesheet" href="/css/font-awesome.4.3.0.min.css" />--%>

    <script src="/js/jquery.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <script >
        $(document).ready(function () {
            var Namecountry = '<%=Session["OtherCountry"] %>';// '@Session["OtherCountry"]'; //Session("OtherCountry").ToString();
            //alert(Namecountry);
            if (Namecountry == "OtherCountry")
            {
                document.getElementById("dv_BillToCity").style.display = "none";
                document.getElementById("dv_BillToState").style.display = "none";
                document.getElementById("dv_BillToCountry").style.display = "none";
                document.getElementById("dv_customerID").style.display = "none";                
            }


        });
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post">
        <div class="container" style="background:#7b2;color:white;">
            <div class="row" style="display:none;">
                <div class="col-lg-12">
                    <div style="text-align: center; font-family: Calibri; font-size: larger">
                        Processing Payment...<br />
                        Please do not refersh or close window.
                    </div>
                    <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
                </div>
            </div>

            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <h1 style="font-weight: bold;font-size: 30px;">
                        We regret to inform you that currently online payment is not activated. If you want to place the order on cash on delivery basis please proceed.
                    </h1>
                </div> 
            </div><br />
            <div class="row">
                <div class="col-lg-12 button-group"  style="text-align: -webkit-center;">
                    <asp:Button ID="btn_proceed_to_payment" runat="server" OnClick="btn_proceed_to_payment_Click" Text="Proceed" class="btn btn-lg btn-primary" />
                    <a class="btn btn-lg btn-danger" href="#" onclick="this.href='cancelpage.aspx?orderID='+document.getElementById('order_id').value" >Cancel</a>
                </div>
            </div><br />
            <div class="row">
                <div class="col-md-6">
                    <div class="row" >
                        <input type="hidden" runat="server" id="tid" name="tid" />
                    </div> 
                    <div  class="row" style="display:none;">
                        <input type="hidden" runat="server" id="merchant_id" name="merchant_id" />
                    </div>
                    <br />
                    <div  class="row">
                        <div class="col-md-4">
                            <label>Order ID</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="order_id" runat="server" class="form-control" />
                        </div>                        
                    </div><br />
                    <div  class="row">
                        <div class="col-md-4">
                            <label>Amount</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="amount" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <div class="row">
                        <div class="col-md-4">
                            <label>Currency</label>
                        </div>                        
                        <div class="col-md-8">                                           
                            <asp:TextBox ReadOnly="true" ID="currency" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <div class="row" style="display:none;" >
                        <input type="hidden" runat="server" id="redirect_url" name="redirect_url" />                         
                    </div>
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="cancel_url" name="cancel_url" />
                    </div>
                
                    <%-- Billing Information --%>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Billing Name</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="billing_name" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <div class="row">
                        <div class="col-md-4">
                            <label>Billing Address</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="billing_address" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <div class="row" style="display:none;"> 
                        <div class="col-md-4">
                            <label>Billing Zip</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="billing_zip" runat="server" class="form-control" />
                        </div>
                    </div> 
                    <div class="row" id="dv_BillToCity">
                        <div class="col-md-4">
                            <label>Billing City</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="billing_city" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <div class="row" id="dv_BillToState">
                        <div class="col-md-4">
                            <label>Billing State</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="billing_state" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    

                </div>
                <div class="col-md-6">
                    
                    <div class="row" id="dv_BillToCountry"><br />
                        <div class="col-md-4">
                            <label>Billing Country</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="billing_country" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <div class="row">
                        <div class="col-md-4">
                            <label>Billing Phone</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="billing_tel" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <div class="row">
                        <div class="col-md-4">
                            <label>Billing Email</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="billing_email" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <%-- Shipping Information --%>
                 
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="delivery_name" name="delivery_name" />
                    </div>
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="delivery_address" name="delivery_address" /> 
                    </div>
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="delivery_city" name="delivery_city" />                          
                    </div>
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="delivery_state" name="delivery_state" />                         
                    </div>
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="delivery_zip" name="delivery_zip" />
                    </div>
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="delivery_country" name="delivery_country" />                         
                    </div>
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="delivery_tel" name="delivery_tel" />
                    </div>

                    
                    <div class="row" style="display:none;">
                        <div class="col-md-4">
                            <label>Additional Info</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="merchant_param1" runat="server" class="form-control" />
                        </div>
                        <br />
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Product</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="merchant_param2" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <div class="row">
                        <div class="col-md-4">
                            <label>Shipping Amt</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="merchant_param3" runat="server" class="form-control" />
                        </div>
                    </div><br />
                    <div class="row">
                        <div class="col-md-4">
                            <label>Net Amt</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="merchant_param4" runat="server" class="form-control" />
                        </div>
                    </div><br /> 
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="merchant_param5" name="merchant_param5" />  
                    </div>
                    <div class="row" style="display:none;">
                        <input type="hidden" runat="server" id="promo_code" name="promo_code" /> 
                    </div>
                    <div class="row" id="dv_customerID"style="display:none;" >
                        <div class="col-md-4">
                            <label>Customer Id</label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ReadOnly="true" ID="customer_identifier" runat="server" class="form-control" />
                        </div>
                    </div> 
                </div> 
                <br />
            </div>
        </div>
        <!-- /.container -->
    </form>
     

    
</body>
</html>
