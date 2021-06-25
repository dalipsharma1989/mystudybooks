<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="change_password.aspx.cs" Inherits="_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Change Pasword</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong>Change Password</strong></li>
            </ol>
        </div>
        <div class="col-lg-2">
            <div class="title-action">
                <a href="change_password.aspx" class="btn btn-info"><i class="fa fa-key"></i>&nbsp;Change Password</a>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Change Password</h5>
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
                                    <small>User Name</small>
                                </label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textusername" placeholder="User name" class="form-control" runat="server" Text="admin" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">
                                    <small>Current Password</small>
                                </label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="txtcurrentpassword" placeholder="Current Password" TextMode ="Password" 
                                        class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        CssClass="validator" ControlToValidate="txtcurrentpassword">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 control-label">
                                    <small>New Password</small>
                                </label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="txtnewpassword" placeholder="New Password" TextMode ="Password" 
                                        class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        CssClass="validator" ControlToValidate="txtnewpassword">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">
                                    <small>Confirm Password</small>
                                </label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="txtconfirmpassword" placeholder="Confirm Password" TextMode ="Password" 
                                        class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        CssClass="validator" ControlToValidate="txtconfirmpassword">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server"  class="validator"
                                        ControlToCompare="txtnewpassword" ControlToValidate="txtconfirmpassword"
                                        ><i class="fa fa-warning"></i>New Password and confirm password didn`t match</asp:CompareValidator>
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


