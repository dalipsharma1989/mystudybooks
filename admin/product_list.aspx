<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="product_list.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <link href="/admin-assets/css/plugins/datapicker/datepicker3.css" rel="stylesheet">

     <script type="text/javascript">
                    $(document).ready(function () {
                                $(function () {
                                    $("[id$=product_name]").autocomplete({
                                        source: function (request, response) {
                                            $.ajax({
                                                url: '<%=ResolveUrl("~/admin/product_list.aspx/GetDataList") %>',
                                                data: "{ 'prefix': '" + request.term + "'}",
                                                //data: "{'prefix': '" + request.term + "' , 'cmpOption': 'ALL', 'rb_SubjectID': '" + $('#ContentPlaceHolder1_rb_subjects').val() + "'}",
                                                dataType: "json",
                                                type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                success: function (data) {
                                                    response($.map(data.d, function (item) {
                                                        return {
                                                            label: item.split('|')[0],
                                                            val: item.split('|')[1]
                                                        }
                                                    }))
                                                },
                                                error: function (response) {
                                                    alert(response.responseText);
                                                },
                                                failure: function (response) {
                                                    alert(response.responseText);
                                                }
                                            });
                                        },
                                        select: function (e, i) {
                                            $("[id$=hftxtSearchBooks]").val(i.item.val);
                                        },
                                        minLength: 1
                                    });
                                }); 
                        });
        </script>
    <style>
        .ui-autocomplete {
                                max-height: 450px;
                                overflow-y: scroll; /* prevent horizontal scrollbar */
                                overflow-x: hidden; /* add padding to account for vertical scrollbar */
                                z-index: 1000 !important;
                                max-width:750px;
                                font-size:15px;
                            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-9">
            <h2 style="font-weight: 600;font-size: 29px;">Product list</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Home</a>
                </li>

                <li>
                    <a>Products</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Product list</strong>
                </li>
            </ol>
        </div>
         <div class="col-lg-3" Style="display:none;">
            <div class="title-action">
                <asp:Button ID="btn_newEntry" class="btn btn-primary" OnClick="btn_newEntry_Click" runat="server" Text="New Entry"  />
                <a href="product_list.aspx" class="btn btn-default">Cancel</a>
            </div>
        </div> 
    </div>

    <div class="wrapper wrapper-content animated fadeInRight ecommerce"> 
        <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);"  class="ibox-content m-b-sm border-bottom">
            <div class="row">
                <div class="col-sm-4">
                    <%--<div class="form-group">
                        <label class="control-label" for="product_name">Product Name</label>
                        <input type="text" id="product_name" name="product_name" value="" placeholder="Product Name" class="form-control">
                    </div>--%>
                    <div class="form-group">                                                       
                        <div>
                            <label class="control-label" for="product_name">Product Name</label> <br />
                        </div>                                 
                        <asp:TextBox runat="server"  CausesValidation="false" placeholder="Search Product Name..."    autocomplete="off"  ID="product_name" CssClass="form-control" 
                            AutoPostBack="true"  OnTextChanged="product_name_TextChanged1"></asp:TextBox>
                        <asp:HiddenField ID="hftextSearchBooks" runat="server" />                    
                        <%--<input type="text" id="product_name" runat="server"  CausesValidation="false" name="product_name" value="" placeholder="Search Product Name..." class="form-control">--%>
                    </div>
                </div>
                <div class="col-sm-2" style="display:none;">
                    <div class="form-group">
                        <label class="control-label" for="price">Price</label>
                        <input type="text" id="price" name="price" value="" placeholder="Price" class="form-control">
                    </div>
                </div>
                <div class="col-sm-2" style="display:none;">
                    <div class="form-group">
                        <label class="control-label" for="quantity">Quantity</label>
                        <input type="text" id="quantity" name="quantity" value="" placeholder="Quantity" class="form-control">
                    </div>
                </div>
                <div class="col-sm-4" style="display:none;">
                    <div class="form-group">
                        <label class="control-label" for="status">Status</label>
                        <select name="status" id="status" class="form-control">
                            <option value="1" selected>Enabled</option>
                            <option value="0">Disabled</option>
                        </select>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox">
                    <div class="ibox-content table-responsive">
                        <asp:GridView ID="grd_books" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" AllowPaging="true" 
                            PagerStyle-CssClass="asp-paging"   PageSize="100" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="35px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>.
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                                <asp:BoundField DataField="BookName" HeaderText="Book" />
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <%# Eval("SaleCurrency") %> <%# Eval("SalePrice",CommonCode .AmountFormat()) %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <a class="btn btn-primary btn-xs" href="product_edit.aspx?editid=<%# Eval("ProductID") %>">View</a>
                                            <a class="btn btn-success btn-xs" href="product_edit.aspx?editid=<%# Eval("ProductID") %>">Edit</a>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <!-- Data picker -->
    <script src="/admin-assets/js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $(document).ready(function () {


            $('#date_added').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true
            });

            $('#date_modified').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true
            });

        });

    </script>
</asp:Content>

