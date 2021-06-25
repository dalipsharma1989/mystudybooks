<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.master" AutoEventWireup="true" CodeFile="order_history.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .label{
            font-size:13px;
        }
        .cart-tbl .wid-10 {
            width: 100px;
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
    

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="kode-inner-banner">
        <div class="kode-page-heading">
            <h2 style="text-align: center;">Order History</h2>
        </div>
    </div>

    <div class="kode-content padding-tb-50">
        
            <div class="row">
                <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>

                <div style="margin-top: 15px;" class="col-lg-12 table-responsive">
                    <h3 style="color: #96c943">Orders</h3>
                    <table class="table  table-hover cart-tbl">
                        <tbody>
                            <tr>
                                <th class="wid-4">SrNo.</th>
                                <th class="wid-10">Order ID </th>
                                <th>Order Date</th>
                                <th class="text-center">Items</th>
                                <th class="text-right">Total</th>
                                <th class="text-right">Ship Charges</th>
                                <th class="text-right">Net Total</th>
                                <th class="text-left">Order Status</th>
                                <%--<th>Shipping</th>--%>
                                <th class="wid-10">View</th>
                            </tr>
                            <asp:Repeater ID="rp_orders" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="width-10">
                                            <%# Container.ItemIndex+1 %>.
                                        </td>
                                        <td>
                                            <a href="order_summary.aspx?orderid=<%# Eval("OrderNo") %>"data-toggle="tooltip" title="View Order"><%# Eval("OrderID") %></a>
                                        </td>
                                        <td><%# Eval("OrderDate","{0:dd MMM, yyyy}") %></td>

                                        <td class="qty-txt-box text-center"><%# Eval("TotalQty") %></td>
                                        <td class="text-right"><%# Eval("TotalAmount",CommonCode.AmountFormat()) %></td>
                                        <td class="text-right"><%# Eval("ShipCost",CommonCode.AmountFormat()) %></td>
                                        <td class="text-right"><%# Eval("TotalNetAmount",CommonCode.AmountFormat()) %></td>
                                        <td class="text-left">
                                            <label style="color:#96c943" class="label <%# Eval("StatusClass") %>"><%# Eval("OrderStatus") %></label></td>
                                        <%--<td>
                                            <label class="label <%# Eval("StatusClass") %>"><%# Eval("PubStatus") %></label></td>--%>
                                        <td>
                                            <a  href="order_summary.aspx?orderid=<%# Eval("OrderNo") %>" class="btn btn-xs btn-default btn-primary view-button">
                                                <i class="fa fa-eye"></i>&nbsp;View Order
                                            </a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>


