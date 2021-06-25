<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="store-settings.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Menu</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong>Store Settings</strong></li>
            </ol>
        </div>
        <div class="col-lg-2">
            <div class="title-action">
                <a href="store-settings.aspx" class="btn btn-info"><i class="fa fa-gear"></i>&nbsp;Settings</a>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Settings</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <asp:HiddenField ID="hf_settingID" runat="server" />
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Title</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textMenuTitle" placeholder="Menu Title" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        CssClass="validator" ControlToValidate="textMenuTitle">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">URL</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textMenuUrl" placeholder="http:\\"
                                        class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        CssClass="validator" ControlToValidate="textMenuUrl">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">Sort Order</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textSortOrder" placeholder="Sort Order"
                                        Text="1" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server"
                                        CssClass="validator" ControlToValidate="textSortOrder"
                                        MinimumValue="0" MaximumValue="100" Type="Integer">
                                        <i class="fa fa-warning"></i>&nbsp;Value must be in between 0 - 100</asp:RangeValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <asp:Button ID="btnSave" runat="server"
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



