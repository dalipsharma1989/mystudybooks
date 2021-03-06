<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="cartlist.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- FooTable -->
    <link href="/admin-assets/css/plugins/footable/footable.core.css" rel="stylesheet">
    <link href="/admin-assets/css/plugins/datapicker/datepicker3.css" rel="stylesheet">
</asp:Content>

 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
    .search-button{
    width: 107px;
    height: 35px;
    padding-top: 5px;
    font-size: 14px;
    background: #3051a0;
    border: 1px solid #3051a0;
    }
    .search-button:hover{
        background:white;
        border: 1px solid #3051a0;
        color:#3051a0;
    }
    .btn-white{
    border: 3px double lightgrey;
    width: 70px;
    background: #3051a0;
    color: white;
    font-size: 12px;
    }
    .btn-white:hover{
        background:white;
        color:#3051a0;
        border: 3px double lightgrey;
    }
   </style>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">Cart List</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Admin</a>
                </li>
                <li>
                    <a>E-commerce</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Cart List</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>

<div class="wrapper wrapper-content animated fadeInRight ecommerce">
        <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox-content m-b-sm border-bottom">
            <asp:Literal ID="ltr_alert_msg" runat="server"></asp:Literal>
            <div class="row">
                <div class="col-sm-4">
                    <div class="form-group init-validator  ">
                        <label class="control-label" for="<%=textOrderID.ClientID %>">Cart ID</label>
                        <asp:TextBox ID="textOrderID" runat="server" placeholder="Cart ID" class="form-control cart-ph"></asp:TextBox>
                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer"
                            CssClass="validator"
                            ControlToValidate="textOrderID" ErrorMessage="Value must be a whole number" />
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="form-group">
                        <label class="control-label" for="<%=textCustomerName.ClientID  %>">Customer Name</label>
                        <asp:TextBox ID="textCustomerName" runat="server" placeholder="Customer Name" class="form-control cart-ph"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="display:none;">
                <div class="col-sm-4">
                    <div class="form-group">
                        <label class="control-label" for="<%=textCreatedDate.ClientID %>">Created Date</label>
                        <div class="input-group date">
                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                            <asp:TextBox ID="textCreatedDate" runat="server"
                                placeholder="Created Date" class="form-control cart-ph"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="form-group init-validator ">
                        <label class="control-label" for="<%=textAmount.ClientID  %>">Amount</label>
                        <asp:TextBox ID="textAmount" runat="server" placeholder="Amount" class="form-control cart-ph"></asp:TextBox>
                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer"
                            CssClass="validator"
                            ControlToValidate="textAmount" ErrorMessage="Value must be a whole number" />
                    </div>
                </div>
                <div class="col-sm-4">
                </div>
            </div>

            <div class="ibox-footer ">
                <asp:LinkButton ID="btn_search_order" class="btn btn-sm btn-success search-button"
                    OnClick="btn_search_order_Click"
                    runat="server"><i class="fa fa-search "></i>&nbsp;Search</asp:LinkButton>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox">
                    <div class="ibox-content">

                        <table class="footable table table-stripped toggle-circle" data-page-size="100">
                            <thead>
                                <tr>
                                    <th>Sr. No.</th>
                                    <th>Cart ID</th>
                                    <th data-hide="phone">Customer</th>
                                    <th data-hide="all">Mobile</th>
                                    <th data-hide="all">EmailID</th>
                                    <%--<th data-hide="phone" style="display:none;">Ship Address</th>--%>
                                    <th data-hide="phone">Currency</th>
                                    <th data-hide="phone" class="text-right">Amount</th>                                    
                                    <th data-hide="phone">Cart ValidUpto</th>
                                    <%--<th data-hide="phone">Status</th>--%>
                                    <th class="text-right">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rp_orders" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td  ><%# Container.ItemIndex+1 %>.</td>
                                            <td><%# Eval("CartID") %></td>
                                            <td><%# Eval("CustName") %></td>
                                            <td><%# Eval("Mobile") %></td>
                                            <td><%# Eval("EmailID") %></td>
                                            <%--<td style="display:none;"><%# Eval("ShipAddress") %></td>--%>
                                            <td><%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%></td>
                                            <td class="text-right"><%# Eval("TotalAmount") %></td>  
                                            <td><%# Eval("IsValidUpTo","{0:dd-MMM-yyyy}") %></td>                                          
                                            <%--<td><%# Eval("Status") %></td>--%>
                                            <td class="text-right">
                                                <div class="btn-group">
                                                    <a href="Cart_details.aspx?orderid=<%# Eval("CartID") %>" class="btn-white btn btn-xs">View</a>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr id="trEmpty" runat="server" visible="false">
                                            <td colspan="6">
                                                <p class="text-danger">No records found.</p>
                                            </td>
                                        </tr>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="6">
                                        <ul class="pagination pull-right"></ul>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>

                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
    <!-- Data picker -->
    <script src="/admin-assets/js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- FooTable -->
    <script src="/admin-assets/js/plugins/footable/footable.all.min.js"></script>
    <!-- Page-Level Scripts -->
    <script>
        $(document).ready(function () {

            $('.footable').footable();

            $('#<% =textCreatedDate.ClientID %>').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            });
        });

    </script>
</asp:Content>

