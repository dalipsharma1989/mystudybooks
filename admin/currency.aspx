<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="currency.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Currency</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong>Currency</strong></li>
            </ol>
        </div>
        <div class="col-lg-2">
            <div class="title-action">
                <a href="currency.aspx" class="btn btn-info"><i class="fa fa-plus"></i>&nbsp;Add New Currency</a>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Add/Edit Currency</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <asp:HiddenField ID="hf_Currency" runat="server" />
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Currency ID</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textCurrencyID" placeholder="Currency ID" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        CssClass="validator" ControlToValidate="textCurrencyID">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1" runat="server"
                                        ValidationExpression="[a-zA-Z]{3}" ToolTip="Eg. Indian Rupee- INR, US Dollar- USD" data-toggle="tooltip"
                                        CssClass="validator" ControlToValidate="textCurrencyID">
                                        <i class="fa fa-warning"></i>&nbsp;Only 3 characters are allowed
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">Currency Name</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textCurrencyName" placeholder="Currency Name" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        CssClass="validator" ControlToValidate="textCurrencyName">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">
                                    <small data-toggle="tooltip" title="Eg. Indian Rupees- Rs/₹, US Dollar- $">Currency Symbol&nbsp;<i class="fa fa-info-circle"></i></small></label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textCurrencySymbol" placeholder="Currency Name" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        CssClass="validator" ControlToValidate="textCurrencySymbol">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-2 control-label">Rate</label>
                                <div class="col-sm-8 init-validator">
                                    <asp:TextBox ID="textRate" class="form-control" placeholder="Rate"
                                        Text="1"
                                        runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        CssClass="validator" ControlToValidate="textRate" ErrorMessage="Rate is empty">
                                                    <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="textRate"
                                        CssClass="validator" MinimumValue="0" MaximumValue="999999" Type="Double" ErrorMessage="Rate must be non-negative decimal number">
                                                <i class="fa fa-warning"></i>&nbsp;Invalid Rate</asp:RangeValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">Is Default</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:CheckBox ID="cb_isDefault" runat="server" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <asp:Button ID="btnSave" runat="server"
                                        OnClick="btnSave_Click"
                                        class="btn btn-sm btn-success" Text="Save" />
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
                            <asp:GridView ID="grd_currency" runat="server"
                                CssClass="table table-striped table-bordered table-hover"
                                AutoGenerateColumns="false"
                                PagerStyle-CssClass="asp-paging"
                                OnRowCommand="grd_currency_RowCommand"
                                OnPageIndexChanging="grd_currency_PageIndexChanging">
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

                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <a href="currency.aspx?currencyid=<%# Eval("CurrencyID") %>">
                                                <%# Eval("CurrencyID") %> - <%# Eval("CurrencySymbol") %> - <%# Eval("CurrencyName") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <%# Eval("Rate") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Is Default">
                                        <ItemTemplate>
                                            <%# Eval("DefaultCurr") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <a href="currency.aspx?currencyid=<%# Eval("CurrencyID") %>" class="btn btn-xs btn-primary" data-toggle="tooltip" title="Edit">
                                                <i class="fa fa-edit"></i>
                                            </a>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("CurrencyID")%>' CommandName="delete_record"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                                runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>
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




