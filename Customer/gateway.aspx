<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gateway.aspx.cs" Inherits="_payment_gateway" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>Processing Payment...</title>
    <link rel="stylesheet" href="/css/bootstrap.css" />
    <link rel="stylesheet" href="/css/font-awesome.4.3.0.min.css" />
</head>
<body>
    <form id="form1" runat="server" method="post">
         <div class="row">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div style="text-align: center; font-family: Calibri; font-size: larger">
                        Your Details for Order...<br />
                        Please Do Not Refersh or Close This Window.
                    </div>
                    <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
                </div>
                <input type="hidden" runat="server" id="key" name="key" />
                <input type="hidden" runat="server" id="hash" name="hash" />
                <input type="hidden" runat="server" id="txnid" name="txnid" />
                <input type="hidden" runat="server" id="enforce_paymethod" name="enforce_paymethod" />
            </div>
            <!-- /.row -->
            <div class="row" style="display:none;">
                <div  class="col-lg-12">
                    <input id="chkDelivery" type="checkbox" runat="server" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:label ID="lbl_deliveryDate" runat="server"></asp:label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div >
                        <label>Order ID </label><asp:TextBox ReadOnly="true" ID="txtOrderID" runat="server" class="form-control" />
                    </div>

                    <div>
                        <label>Product</label>
                        <asp:TextBox ReadOnly="true" ID="productinfo" runat="server" class="form-control" />
                    </div>
                    <div>
                        <label>Amount *</label>
                        <div class="input-group">
                            <div class="input-group-addon"><%:CommonCode.AppSettings("CurrencySymbol") %></div>
                            <asp:TextBox ReadOnly="true" ID="amount" runat="server" placeholder="Amount" class="form-control" />
                        </div>
                    </div>
                    <div>
                        <label>Billing Name *</label><asp:TextBox ReadOnly="true" ID="firstname" runat="server" class="form-control" />
                    </div>
                    <div style="display:none;" >
                        <label>Last Name </label><asp:TextBox ReadOnly="true" ID="lastname" runat="server" class="form-control" />
                    </div>
                    <div>
                        <label>Billing EmailID*</label><asp:TextBox ReadOnly="true" ID="email" runat="server" class="form-control" />
                    </div>
                    <div>
                        <label>Billing Phone / Mobile *</label><asp:TextBox ReadOnly="true" ID="phone" runat="server" class="form-control" />
                    </div>
                    <div>
                        <label>Billing Address *</label><asp:TextBox ReadOnly="true" ID="address1" runat="server" placeholder="Address" class="form-control" />
                    </div>
                    <div>
                        <label>Billing City *</label><asp:TextBox ReadOnly="true" ID="city" runat="server" placeholder="City" class="form-control" />
                    </div>
                    <div>
                        <label>Billing State *</label><asp:TextBox ReadOnly="true" ID="state" runat="server" placeholder="State" class="form-control" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div>
                        <label>Billing Country *</label><asp:TextBox ReadOnly="true" ID="country" runat="server" class="form-control" Text="India" />
                    </div>
                    <div>
                        <label>Billing Zip *</label><asp:TextBox ReadOnly="true" ID="zipcode" runat="server" placeholder="Zip" class="form-control" />
                    </div>
                    <div>
                        <label>Shipping EmailID *</label><asp:TextBox ReadOnly="true" ID="txtShipEmail" runat="server" class="form-control" Text="email" />
                    </div>
                    <div>
                        <label>Shipping Mobile *</label><asp:TextBox ReadOnly="true" ID="txtShipMobile" runat="server" class="form-control" Text="mobile" />
                    </div>
                    <div>
                        <label>Shipping Address *</label><asp:TextBox ReadOnly="true" ID="txtShipaddress" runat="server" class="form-control" Text="address" />
                    </div>
                    <div>
                        <label>Shipping City *</label><asp:TextBox ReadOnly="true" ID="txtShipcity" runat="server" placeholder="City" class="form-control" />
                    </div>
                    <div>
                        <label>Shipping State *</label><asp:TextBox ReadOnly="true" ID="txtShipstate" runat="server" placeholder="state" class="form-control" />
                    </div>
                    <div>
                        <label>Shipping Country *</label><asp:TextBox ReadOnly="true" ID="txtShipcountry" runat="server" placeholder="country" class="form-control" />
                    </div>
                    <div>
                        <label>Shipping Zip *</label><asp:TextBox ReadOnly="true" ID="txtShipzip" runat="server" placeholder="zip" class="form-control" />
                    </div>
                </div>

                <div class="hidden">
                    <asp:TextBox ReadOnly="true" ID="address2" runat="server" />
                    <asp:TextBox ReadOnly="true" ID="surl" runat="server" />
                    <asp:TextBox ReadOnly="true" ID="furl" runat="server" />
                    <asp:TextBox ReadOnly="true" ID="curl" runat="server" />

                    <asp:TextBox ReadOnly="true" ID="service_provider" runat="server" Text="payu_paisa" />
                    <asp:TextBox ReadOnly="true" ID="remarks" runat="server" />
                    <asp:TextBox ReadOnly="true" ID="udf1" runat="server" />
                    <asp:TextBox ReadOnly="true" ID="udf2" runat="server" />
                    <asp:TextBox ReadOnly="true" ID="udf3" runat="server" />
                    <asp:TextBox ReadOnly="true" ID="udf4" runat="server" />
                    <asp:TextBox ReadOnly="true" ID="udf5" runat="server" />
                    <asp:TextBox ReadOnly="true" ID="pg" runat="server" />
                </div>

                <div class=" col-lg-12 button-group" style="text-align:center;padding-top:20px; ">
                    <asp:Button ID="btn_proceed_to_payment" runat="server"
                        OnClick="btn_proceed_to_payment_Click"
                        Text="Next" class="btn btn-lg btn-primary" />

                    <a class="btn btn-lg btn-danger" href="../index.aspx">Cancel</a>
                </div>
            </div>
        </div>
        <!-- /.container -->
    </form>

    <script src="/js/jquery.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
</body>
</html>
