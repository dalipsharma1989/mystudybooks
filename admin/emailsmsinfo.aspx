<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="emailsmsinfo.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .panel-heading{
            background:#3051a0 !important;
            border:1px solid #3051a0 !important;
        }
         .btn-success {
             background:#3051a0 !important;
              color:white !important;
             border:1px solid #3051a0 !important;
                      }

            .btn-success:hover {
                background: white !important;
                border: 1px solid #3051a0 !important;
                color: #3051a0 !important;
    }
    </style>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-8">
            <h2 style="font-weight: 600;font-size: 29px;">Email Info</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong style="color:#3051a0">Email Info</strong></li>
            </ol>
        </div>

        <div class="col-lg-4">
            <div class="title-action">
                <asp:Button ID="btnSave" runat="server"
                    OnClick="btnSave_Click"
                    class="btn btn-primary" Text="Save" />

                <asp:Button ID="btn_update" runat="server"
                    Visible="false"
                    class="btn btn-success" Text="Update" />
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <asp:HiddenField ID="hf_is_data_present" runat="server" />
        <div class="row">
            <div class="col-lg-6">
                <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5 style="font-size: 18px;color: black">Email Info</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Name</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textEmailHeaderName" placeholder="Email Header Name" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">EmailID</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textEmailID" placeholder="EmailID" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">Password</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textPassword" TextMode="Password"  class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">Host</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textHost" placeholder="Host" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">Port</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textPort" placeholder="Port (numeric only)" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label"></label>
                                <div class="col-lg-4">
                                    <asp:CheckBox ID="cb_enablessl" Text="Enable SSL" runat="server" />
                                </div>
                                <label class="col-lg-1 control-label"></label>
                                <div class="col-lg-5">
                                    <asp:CheckBox ID="chkUseDefCredential" Text="UseDefaultCredentials" runat="server" />
                                </div> 
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-6 hidden">
                <div class="ibox  float-e-margins">
                    <div class="ibox-title">
                        <h5>SMS Info</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <label class="col-lg-2 control-label">SMS URL</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textSmsURL" placeholder="SMS URL" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">SenderID</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textSmsSenderID" placeholder="SenderID" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">UserName</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textSmsUserName" placeholder="UserName" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">Password</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textSmsPass" placeholder="Password" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-4">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        Send Test Email
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="text_toemail" CssClass="form-control" placeholder="Email ID" runat="server"></asp:TextBox><br />
                        <asp:LinkButton ID="btn_sendEmail" runat="server"
                            OnClick="btn_sendEmail_Click"
                            CssClass="btn btn-success "><i class ="fa fa-mail-forward"></i>&nbsp;Send Mail</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <!--SPACE-->
            </div>

            <div class="col-lg-4 hidden">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        Send Test SMS
                    </div>
                    <div class="panel-body">
                        <asp:TextBox ID="text_tono" CssClass="form-control" placeholder="10-digit Mobile No" runat="server"></asp:TextBox><br />
                        <asp:LinkButton ID="btn_sendSMS" runat="server"
                            OnClick="btn_sendSMS_Click"
                            CssClass="btn btn-success "><i class ="fa fa-send"></i>&nbsp;Send SMS</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>


