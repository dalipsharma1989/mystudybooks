<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="wishlist_details.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- FooTable -->
    <link href="/admin-assets/css/plugins/footable/footable.core.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">WishList</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Admin</a>
                </li>
                <li>
                    <a>E-commerce</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">WishList</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight ecommerce">
        <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox">
            <div style="border-bottom:2px solid  #3051a0" class="ibox-title">
                <h5>WishList Details </h5>
                <div class="ibox-tools">
                    <a class="btn btn-xs" href="wishlist.aspx">
                        <i class="fa fa-backward"></i>&nbsp;
                        View All WishLists
                    </a>

                </div>
            </div>
            <div class="ibox-content m-b-sm border-bottom">
                <asp:Literal ID="ltr_alert_msg" runat="server"></asp:Literal>
                <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
                <asp:Repeater ID="rp_order" runat="server">
                    <ItemTemplate>
                        <span class="pull-right"><%-- Cart Date: <%# Eval ("OrderDate","{0:dd-MMM-yyyy}") %>--%></span>
                        <%--<h3>WishListID <%# Eval ("WishListID") %></h3>--%>
                         
                        <address>
                            <strong  style="font-size: 16px;color: #3051a0;"><%# Eval ("CustName") %></strong><br>
                            <%--<%# Eval ("ShipAddress") %><br>--%>
                            <abbr style="padding-top: 6px;font-size: 14px;display: inline-block;" title="Phone">P:</abbr>
                            <%# Eval ("Mobile") %><br />
                            <a style="padding-top: 8px;display: inline-block;font-size: 15px;color: #171616eb;" href="mailto:<%# Eval ("EmailID") %>"><%# Eval ("EmailID") %></a>
                        </address>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox">
                    <div class="ibox-content">

                        <table class="table table-hover margin bottom"  data-page-size="100">
                            <thead>
                                <tr>
                                    <th>SrNo</th>
                                    <th data-hide="all">ISBN</th>
                                    <th data-hide="phone">BookName</th>
                                    <th data-hide="phone">SaleCurrency</th>
                                    <th data-hide="phone">Price</th>
                                    <%--<th data-hide="phone">Quantity</th>
                                    <th data-hide="phone">Amount</th>--%>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rp_order_details" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex+1 %>.</td>
                                            <td><%# Eval("ISBN") %></td>
                                            <td><%# Eval("BookName") %></td>
                                            <td><%# Eval("SaleCurrency") %></td>
                                            <td><%# Eval("SalePrice") %></td>
                                            <%--<td><%# Eval("qty") %></td>
                                            <td><%# Eval("SaleCurrency") %> <%# Eval("TotalAmt") %></td>--%>
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
        });
    </script>

</asp:Content>


