<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="conversionrate.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .grid-image {
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Daily Conversion Rate</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong>Daily Conversion Rate</strong></li>
            </ol>
        </div>
    <%--    <div class="col-lg-2">
            <div class="title-action">
                <a href="classes.aspx" class="btn btn-info"><i class="fa fa-plus"></i>&nbsp;Add New Class</a>
            </div>
        </div>--%>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Add Daily Conversion Rate</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <asp:HiddenField ID="hf_ClassID" runat="server" />
                            <div class="form-group">
                                <label class="col-lg-3 control-label">Add New Conversion Rate</label>
                                <div class="col-lg-9 init-validator">
                                    <asp:TextBox ID="textConversionRate" placeholder="Enter Conversion Rate" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validator" ControlToValidate="textConversionRate">
                                        <i class="fa fa-warning"></i>&nbsp;Required </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-3">
                                    
                                </div>
                                <div class="col-lg-offset-2 col-lg-9">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" class="btn btn-sm btn-success" Text="Save Conversion Rate" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_ConversionRate" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false"
                                PagerStyle-CssClass="asp-paging" OnPageIndexChanging="grd_ConversionRate_PageIndexChanging">
                                <EmptyDataTemplate>
                                    <div class="alert alert-warning">
                                        <strong><i class="fa fa-warning"></i></strong>&nbsp;No record found
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Conversion Date">
                                        <ItemTemplate>
                                            <%# Eval("ConversionDate") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Conversion Rate" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>                                            
                                            <%# Eval("ConversionRate") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>




