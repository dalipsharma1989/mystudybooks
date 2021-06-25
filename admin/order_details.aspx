<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="order_details.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- FooTable -->
    <link href="/admin-assets/css/plugins/footable/footable.core.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">Orders</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Admin</a>
                </li>
                <li>
                    <a>E-commerce</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Orders</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight ecommerce">
        <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox">
            <div  style="border-bottom:2px solid  #3051a0" class="ibox-title">
                <h5>Order Details </h5>
                <div class="ibox-tools">
                    <a class="btn btn-xs" href="orders.aspx">
                        <i class="fa fa-backward"></i>&nbsp;
                        View All Orders
                    </a>

                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" >
                    <asp:Literal ID="ltr_alert_msg" runat="server"></asp:Literal>
                    <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
                    <div class="col-lg-6">
                        <div class="ibox-content m-b-sm border-bottom">                            
                            <h3>Customer Details</h3>
                            <asp:Repeater ID="rp_order" runat="server">
                                <ItemTemplate>
                                    <span class="pull-right">Order Date: <%# Eval ("OrderDate","{0:dd-MMM-yyyy}") %></span>
                                    <h3>Transaction ID :- <%# Eval ("OrderID") %></h3>
                                    <hr />
                                    <address>
                                        <abbr title="Phone">Customer Name :</abbr><strong><%# Eval ("CustName") %></strong><br>                                        
                                        <abbr title="Phone">Billing Add :</abbr><%# Eval ("BillingAddress") %><br>
                                        <abbr title="Phone">Phone Num   :</abbr><%# Eval ("Mobile") %><br />
                                        <abbr title="Phone">Email ID    :</abbr><a href="mailto:#"><%# Eval ("EmailID") %></a>
                                    </address>
                                   <%-- <span class="pull-right">Order Date: <%# Eval ("OrderDate","{0:dd-MMM-yyyy}") %></span><br />                        
                                    <h3>Order ID <%# Eval ("OrderID") %></h3> 
                                    <strong style="font-size: 16px;color: #3051a0;"><%# Eval ("CustName") %></strong><br>
                                    <address style="width:35%">                            
                                        <%# Eval ("ShipAddress") %><br>
                                        <abbr style="padding-top: 6px;font-size: 14px;display: inline-block;" title="Phone">Ph:</abbr>
                                        <%# Eval ("Mobile") %><br />Email:
                                        <a style="padding-top: 8px;display: inline-block;font-size: 15px;color: #171616eb;" href="#"><%# Eval ("EmailID") %></a>
                                    </address>--%>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="ibox-content m-b-sm border-bottom"> 
                            <h3>Order  Details</h3>                           
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <%--<span class="pull-right">Order Date: <%# Eval ("OrderDate","{0:dd-MMM-yyyy}") %></span><br />                        
                                    <h3>Order ID <%# Eval ("OrderID") %></h3>
                         
                                    <strong style="font-size: 16px;color: #3051a0;"><%# Eval ("CustName") %></strong><br>
                                    <address style="width:35%">                            
                                        <%# Eval ("ShipAddress") %><br>
                                        <abbr style="padding-top: 6px;font-size: 14px;display: inline-block;" title="Phone">Ph:</abbr>
                                        <%# Eval ("Mobile") %><br />Email:
                                        <a style="padding-top: 8px;display: inline-block;font-size: 15px;color: #171616eb;" href="#"><%# Eval ("EmailID") %></a>
                                    </address>--%>
                                    <span class="pull-right">Order Date: <%# Eval ("OrderDate","{0:dd-MMM-yyyy}") %></span>
                                    <h3>Order No :- <%# Eval ("OrderDocNo") %></h3>
                                    <hr />
                                    <address>
                                        <abbr title="Phone">Customer Name :</abbr><strong><%# Eval ("CustName") %></strong><strong><span class="pull-right">Order Status: <%# Eval ("OrderStatus") %></span></strong><br>                                        
                                        <abbr title="Phone">Shipping Add:</abbr><%# Eval ("ShipAddress") %><br>
                                        <abbr title="Phone">Phone Num   :</abbr><%# Eval ("Mobile") %><br />
                                        <abbr title="Phone">Email ID    :</abbr><a href="mailto:#"><%# Eval ("EmailID") %></a>
                                    </address>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                             <div class="ibox-content m-b-sm border-bottom"> 
                                <h3>Invoice  Details</h3>                           
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate> 
                                        <span class="pull-right">Invoice Date: <%# Eval ("TrnsDocDate","{0:dd-MMM-yyyy}") %></span>
                                        <h3>Invoice No :- <%# Eval ("TrnsDocNo") %></h3>
                                        <hr />
                                        <strong><span class="pull-right">TransPort Detail</span></strong> <br />
                                        <address >
                                            <abbr title="Phone">Customer Name :</abbr><strong><%# Eval ("CustName") %></strong><span class="pull-right">Airway No: <%# Eval ("TrnsGrNo") %></span><br>
                                            <abbr title="Phone">Shipping Add:</abbr><%# Eval ("ShipAddress") %><span class="pull-right">Airway Date: <%# Eval ("TrnsGrDate","{0:dd-MMM-yyyy}") %></span><br>
                                            <abbr title="Phone">Phone Num   :</abbr><%# Eval ("Mobile") %><span class="pull-right">Tracking URL: <%# Eval ("TrackingURL") %></span><br />
                                            <abbr title="Phone">Email ID    :</abbr><a href="mailto:#"><%# Eval ("EmailID") %></a>
                                        </address>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox">
                    <div class="ibox-content">

                        <table class="table table-hover margin bottom"  data-page-size="100">
                            <thead>
                                <tr>
                                    <th style="text-align: right;">SrNo</th>
                                    <th data-hide="all">ISBN</th>
                                    <th data-hide="phone">BookName</th>
                                    <th data-hide="phone" style="text-align: right;">Price</th>
                                    <th data-hide="phone" style="text-align: center;">Quantity</th>
                                    <th data-hide="phone" style="text-align: right;">Disc(%)</th>
                                    <th data-hide="phone" style="text-align: right;">Discount</th>
                                    <th data-hide="phone" style="text-align: right;">Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rp_order_details" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: right;"><%# Container.ItemIndex+1 %>.</td>
                                            <td><%# Eval("ISBN") %></td>
                                            <td><%# Eval("BookName") %></td>
                                            <td style="text-align: right;"><%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%> <%# Eval("Price") %></td>
                                            <td style="text-align: center;"><%# Eval("qty") %></td>
                                            <td style="text-align: right;"><%# Eval("DiscountPer") %></td>
                                            <td style="text-align: right;"><%# Eval("DiscountAmt") %></td>
                                            <td style="text-align: right;">  <%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%> <%# Eval("TotalAmt") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr id="trEmpty" runat="server" visible="false">
                                            <td colspan="6">
                                                <p class="text-danger">No records found.</p>
                                            </td>
                                        </tr>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="rp_order_tot_details" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td></td>
                                                    <td colspan="6">Shipping Cost : </td>
                                                    <td style="text-align: right;"><%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%>&nbsp;<%# Eval("ShipCost") %></td>
                                                </tr>
                                                </tr>
                                                    <td></td>
                                                    <td colspan="6">Total Cost : </td>
                                                    <td style="text-align: right;"><%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%>&nbsp;<%# Eval("TotalNetAmount") %></td>
                                                </tr>
                                            </Itemtemplate>
                                </asp:Repeater>
                            </tbody>
                            <%--<tfoot>
                                <tr>
                                    <td colspan="5">
                                        <ul class="pagination pull-right"></ul>
                                    </td>
                                </tr>
                            </tfoot>--%>
                        </table>

                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
    <!-- FooTable -->
    <script src="/admin-assets/js/plugins/footable/footable.all.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.footable').footable();

            $('.footer').css('display','none');
        });

    </script>

</asp:Content>


