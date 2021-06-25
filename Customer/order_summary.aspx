<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.master" AutoEventWireup="true" CodeFile="order_summary.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css" >
        .book-grid {
            width: 50px;
            float: left;
            margin-right: 15px;
        }

        .ttl-amt {
            font-weight: bold;
                color:#f56544fa;
        }

        .label.status-completed {
            background-color: #2E7D32;
        }

        .label.status-closed {
            background-color: #C62828;
        }

        .label.status-cancelled {
            background-color: #F57F17;
        }

        .label.status-processing {
            background-color: #428bca;
        }

        .label.status-pending {
            background-color: #00BCD4;
        }
        
        table tr:nth-child(2n-1){
            background:#d3d3d326;
        }
        .table-responsive>.table>tbody>tr>td {
    white-space: normal;
}
        
        abbr[title] {
    text-decoration: none;
}
         .panel-body span{
             color:#f56544fa;
         }
         address{
              font-size: 14px;
              color: black;
              font-weight:600;
         }
        .panel-primary > .panel-heading {
            color: #fff;
            background-color: #f59744 !important;
            border-color: none !important;
        }
       .panel-success>.panel-heading{
           color: #fff;
           background-color: #f59744 !important;
           border-color: none !important;
       }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="kode-inner-banner">
        <div style="text-align:center" class="kode-page-heading">
            <h2 class="order-heading">Order History</h2>
        </div>
    </div>

    <div class="kode-content padding-tb-50">
       
            <div class="row">
                <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
                <div class="col-lg-12 table-responsive">
                    <%--<h1 class="btn btn-xs btn-info pull-right" id="h2" runat="server">Pub Status</h1>--%>
                    <h3 id="h3_orderid" runat="server">Order ID </h3>
                    <span id="span_order_date" runat="server">Order Date: </span>
                    <div class="clearfix">
                        <a class="btn btn-xs btn-primary btn-info pull-right view-order" style="background-color: #f59744;"
                            href="order_history.aspx">View All Orders</a>
                    </div>
                    <hr />

                    <div class="row">
                        <div class="col-lg-6">
                            <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);border: none !important;" class="panel panel-primary">
                                <a class="btn btn-xs pull-right" style="color: #fff !important;box-shadow:none !important" id="col_div_bill" aria-expanded="true"
                                    data-toggle="collapse" href="#<%=div_bill_address.ClientID %>"><i class="fa fa-arrow-circle-left"></i>&nbsp;Open</a>
                                <div class="panel-heading">Billing Address</div>

                                <div class="panel-collapse collapse panel-body" id="div_bill_address" runat="server">
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);border: none !important;" class="panel panel-success">
                                <a class="btn btn-xs pull-right" id="col_div_ship" aria-expanded="true" data-toggle="collapse" href="#<%=div_ship_address.ClientID %>"><i class="fa fa-arrow-circle-left"></i>&nbsp;Open</a>
                                <div class="panel-heading">Shipping Address</div>
                                <div class="panel-collapse collapse panel-body" id="div_ship_address" runat="server"></div>
                            </div>
                        </div>
                    </div>

                    <h3>Order Details</h3>
                    <table style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="table cart-tbl table-row">
                        <tbody>
                            <tr >
                                <th style="width: 20px">SrNo.</th>
                                <th class="wid-60 left-text">Items You Buy </th>
                                <th class="wid-10">Price</th>
                                <th class="wid-10 t-center">Quantity</th>
                                <th class="wid-10 t-center">Discount</th>
                                <th class="wid-10">Subtotal</th>
                            </tr>
                            <asp:Repeater ID="rp_order_summary" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Container.ItemIndex+1 %>.
                                        </td>
                                        <td>
                                            <img src="<%# Eval("ImgPath") %>"
                                                onerror="this.onerror=null;this.src='/resources/no-image.png';" class="book-grid" />
                                            <%# Eval("BookName") %><br />
                                            <small>(<%# Eval("ISBN") %>)</small>
                                        </td>
                                        <td class="center"><%# Eval("Currency") %>&nbsp;<%# Eval("Price",CommonCode.AmountFormat()) %></td>
                                        <td class="qty-txt-box ">
                                            <%# Eval("Qty") %>
                                        </td>
                                        <td class="center"><%# Eval("Currency") %>&nbsp;<%# Eval("DiscountAmt",CommonCode.AmountFormat()) %></td>
                                        <td class="center"><%# Eval("Currency") %>&nbsp;<%# Eval("TotalAmt",CommonCode.AmountFormat()) %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tfoot>
                            <tr>                                
                                <td ></td>
                                <td>Sub Total</td>
                                <td colspan="3"></td>
                                <td  style="color:#f56544fa;font-weight:bold" >&nbsp;<%:subtotl  %></td>
                            </tr>
                            <tr>                                
                                <td ></td>
                                <td>Shipping Charges</td>
                                <td colspan="2"></td>
                                <td style="color:#f56544fa;font-weight:bold;text-align:right;">+</td>
                                <td  style="color:#f56544fa;font-weight:bold" >&nbsp;<%:ShippingAmountText  %></td>
                            </tr>
                            <tr>
                                <td ></td>
                                <td>Total</td>
                                <td colspan="3">
                                    <asp:HiddenField ID="hf_total_amount" runat="server" />
                                </td>
                                <td id="td_total_amount" runat="server">
                                    <span id="ttl-amt" class="ttl-amt pulsor"></span>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                    <hr /> 
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <h4 class="text-success" id="h4_status" runat="server"></h4>
                                <h4 class="text-success" id="h4_invno" runat="server"></h4>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" id="dv_shippingDet" runat="server">
                                <h4 class="text-success" id="h1_Courier" runat="server">Order Tracking Details</h4>
                                <h4 class="text-success" id="h4_awbno" runat="server"></h4>
                                <h4 class="text-success" id="h4_Date" runat="server"></h4>
                                <a id="trackingUrl" class="fa fa-cab" runat="server" ></a>
                            </div>
                        </div>
                    </div>
                    
                    <hr />
                    <div class="row" id="dv_payinfo" runat="server">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <h3 class="text-success">Payment Information</h3>
                            <h5 class="text-primary" id="h_paymentInfo" runat="server"></h5>        
                        </div>
                    </div>
                </div>
            </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">

    <script>
        $(document).ready(function () {
            $("#<%=div_bill_address.ClientID %>").on("show.bs.collapse", function () {
                $("#col_div_bill").html('<i class="fa fa-arrow-circle-down"></i> Close');
            });

            $("#<%=div_bill_address.ClientID %>").on("hide.bs.collapse", function () {
                $("#col_div_bill").html('<i class="fa fa-arrow-circle-left"></i> Open');
            });
            
            $("#<%=div_ship_address.ClientID %>").on("show.bs.collapse", function () {
                $("#col_div_ship").html('<i class="fa fa-arrow-circle-down"></i> Close');
            });

            $("#<%=div_ship_address.ClientID %>").on("hide.bs.collapse", function () {
                $("#col_div_ship").html('<i class="fa fa-arrow-circle-left"></i> Open');
            });
            
        });
    </script>
</asp:Content>

