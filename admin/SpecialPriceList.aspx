<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminMaster.master" CodeFile="SpecialPriceList.aspx.cs" Inherits="admin_SpecialPriceList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- FooTable -->
    <link href="/admin-assets/css/plugins/footable/footable.core.css" rel="stylesheet">
    <link href="/admin-assets/css/plugins/datapicker/datepicker3.css" rel="stylesheet"> 
</asp:Content>
<asp:Content ID="Content3" runat="Server" ContentPlaceHolderID="ContentPlaceHolder1"> 
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-9">
            <h2 style="font-weight: 600;font-size: 29px;">Product Offer list</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Home</a>
                </li> 
                <li>
                    <a>Offers</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Product Offer list</strong>
                </li>
            </ol>
        </div> 
    </div>
    <div class="wrapper wrapper-content animated fadeInRight ecommerce"> 
        <div class="row">
            <div class="col-lg-12">
                <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
            </div>
            <div class="col-lg-12">
                <div class="ibox">
                    <div class="ibox-content table-responsive">
                        <asp:GridView ID="grd_books" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" AllowPaging="true" 
                            PagerStyle-CssClass="asp-paging"   PageSize="100" OnPageIndexChanging="grd_books_PageIndexChanging"
                            OnRowCommand="grd_books_RowCommand"  >
                            <Columns>
                                <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>.
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BookCode" HeaderText="ISBN" />
                                <asp:BoundField DataField="BookName" HeaderText="Book Name" />
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <%# Eval("SaleCurrency") %> <%# Eval("SalePrice",CommonCode .AmountFormat()) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spl Price">
                                    <ItemTemplate>
                                        <%# Eval("SaleCurrency") %> <%# Eval("DiscountPrice",CommonCode .AmountFormat()) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spl Disc(%)">
                                    <ItemTemplate> 
                                        <%# Eval("DiscountPercent",CommonCode .AmountFormat()) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Date">
                                    <ItemTemplate> 
                                        <%# Eval("StartDate","{0:dd MMM yyyy}") %>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date">
                                    <ItemTemplate> 
                                        <%# Eval("EndDate","{0:dd MMM yyyy}") %> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <div class="btn-group"> 
                                            <a class="btn btn-success btn-xs" href="product_edit.aspx?editid=<%# Eval("BookCode") %>">Edit</a>
                                            <asp:LinkButton ID="lnkBookCode" runat="server" CommandArgument='<%# Eval("BookCode") %>' CommandName="Delete_bookCode" Text="Remove"
                                                 CssClass="btn btn-xs btn-danger">
                                               <i class="fa fa-trash-o"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>


    
    <!-- Data picker -->
    <script src="/admin-assets/js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- FooTable -->
    <script src="/admin-assets/js/plugins/footable/footable.all.min.js"></script>
    <!-- Page-Level Scripts -->
    <script>
        $(document).ready(function () {

            $('.footable').footable();

            <%--$('#<% =textCreatedDate.ClientID %>').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            });--%>
        });

    </script>
</asp:Content>
