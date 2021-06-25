<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="view_users.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
     <script type="text/javascript">
                    $(document).ready(function () {
                                $(function () {
                                    $("[id$=txtCustomer_Name]").autocomplete({
                                        source: function (request, response) {
                                            $.ajax({
                                                url: '<%=ResolveUrl("~/admin/view_users.aspx/GetDataList") %>',
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
                                            $("[id$=hftextCustomer_Name]").val(i.item.val);
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
    <!--PAGE TITLE-->
    <div class="row wrapper  border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2 id="h2_title" runat="server">View Registered Schools </h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Admin</a>
                </li>
                <li class="active">
                    <strong id="strong_li_title" runat="server">View Schools</strong>
                </li>
            </ol>
        </div>
    </div>
    <!--PAGE TITLE-->
     <div class="row wrapper  border-bottom white-bg page-heading" style="padding-top: 20px;">
        <div class="form-group" style="display:block">
            <label class="col-sm-2 control-label"><h2 style="margin: 0px;"><strong>Search</strong></h2> </label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtCustomer_Name" class="form-control"  placeholder="Type here Username, Email and Mobile No....................." runat="server"
                     OnTextChanged="txtCustomer_Name_TextChanged"></asp:TextBox>
                <asp:HiddenField ID="hftextCustomer_Name" runat="server" />                    
            </div>
            <div class="col-sm-4">
                <asp:Button ID="btn_search_edit" class="btn btn-primary" OnClick="btn_search_edit_Click" runat="server" Text="Search" />
            </div>
        </div>        
    </div>
    <!--PAGE CONTENT-->
    <div class="row">
        <asp:Literal ID="ltr_alert_msg" runat="server"></asp:Literal>
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView2" runat="server"  CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Record Found !"
                            AllowPaging="true" PageSize="20"  OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand" PagerStyle-CssClass="asp-paging"
                            OnPageIndexChanging="GridView2_PageIndexChanging" AutoGenerateColumns="false">                             
                            <Columns>
                                <asp:BoundField DataField="CustId" HeaderText="Customer Id"/>
                                <asp:BoundField DataField="CustName" HeaderText="Customer Name"/>
                                <asp:BoundField DataField="Mobile" HeaderText="Mobile No"/>
                                <asp:BoundField DataField="EmailID" HeaderText="Email ID"/>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Registered On"/>
                                <asp:BoundField DataField="LastLogin" HeaderText="Last Login"/>
                                <asp:BoundField DataField="UserPassword" HeaderText="Password"/>
                                <asp:BoundField DataField="BillingAddress" HeaderText="Address" ItemStyle-Width="50"  HeaderStyle-Width="50"  ItemStyle-Wrap="true"/>                                
                                <asp:TemplateField HeaderText ="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <ItemTemplate> 
                                            <a id="" href="ModiFyDeliverAddress.aspx?CustID=<%# Eval("CustId") %>"><i class="fa fa-ambulance"></i>&nbsp;Edit Address</a>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <!--PAGE CONTENT-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>


