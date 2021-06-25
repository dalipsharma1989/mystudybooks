<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="shipping-setting.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Shipping Amount</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong>Shipping Amount</strong></li>
            </ol>
        </div>
        <div class="col-lg-2">
            <div class="title-action">
                <a href="shipping-setting.aspx" class="btn btn-info"><i class="fa fa-edit"></i>&nbsp;Shipping Amount</a>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Shipping Amount</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <asp:HiddenField ID="hf_RowID" runat="server" />
                            <div class="form-group">
                                <label class="col-lg-2 control-label">
                                    <small>Default Shipping Amount</small>
                                </label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textDefaultShippingAmount" placeholder="Default Shipping Amount" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        CssClass="validator" ControlToValidate="textDefaultShippingAmount">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="textDefaultShippingAmount"
                                        CssClass="validator" MinimumValue="0" MaximumValue="999999" Type="Double">
                                                <i class="fa fa-warning"></i>&nbsp;Invalid Amount</asp:RangeValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">
                                    <small>Min Amount for Free Shipping</small>
                                </label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textMinAmountForFreeShipping" placeholder="Min Amount for Free Shipping"
                                        class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        CssClass="validator" ControlToValidate="textMinAmountForFreeShipping">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="textMinAmountForFreeShipping"
                                        CssClass="validator" MinimumValue="0" MaximumValue="999999" Type="Double">
                                                <i class="fa fa-warning"></i>&nbsp;Invalid Amount</asp:RangeValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">FreeShippingMessage</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textFreeShippingMessage" placeholder="Free Shipping Message"
                                        TextMode="MultiLine" Style="height: 180px; resize: vertical"
                                        class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        CssClass="validator" ControlToValidate="textFreeShippingMessage">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>


