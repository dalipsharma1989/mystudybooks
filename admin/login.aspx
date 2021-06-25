<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="admin_login" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>STS Admin | Login </title>

    <link href="/admin-assets/css/bootstrap.min.css" rel="stylesheet">
    <link href="/admin-assets/font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="/admin-assets/css/animate.css" rel="stylesheet">
    <link href="/admin-assets/css/style.css" rel="stylesheet">
    <link href="/admin-assets/css/asp-style.css" rel="stylesheet" />
</head>

<body class="gray-bg">
    <form id="form1" runat="server">
        <div class="loginColumns animated fadeInDown">
            <div class="row">

                <div class="col-md-6">
                    <h2 class="font-bold">Welcome to STS Admin</h2>

                    <p>
                        Perfectly designed and precisely prepared admin panel for your Book Store.
                    </p>

                </div>
                <div class="col-md-6">
                    <div class="ibox-content">
                        <div class="m-t">
                            <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
                            <div class="form-group init-validator ">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    CssClass="validator " ControlToValidate="textUserName"
                                    ErrorMessage="<i class='fa fa-warning'></i>&nbsp;Required"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="textUserName" runat="server" placeholder="Username" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group init-validator">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    CssClass="validator " ControlToValidate="textPassword"
                                    ErrorMessage="<i class='fa fa-warning'></i>&nbsp;Required"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="textPassword" runat="server" placeholder="Password"
                                    class="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnSubmit" runat="server"
                                OnClick="btnSubmit_Click"
                                class="btn btn-primary block full-width m-b" Text="Login" />
                        </div>
                        <p class="m-t">
                            <small>STS Admin Web App &copy; <%:DateTime.Now.Year %></small>
                        </p>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="pull-left" style="display:none;">
                        <strong>Powered By</strong> <a target="_blank" href="http://springtimesoftware.net">Spring Time Software </a>&copy; <%:DateTime.Now.Year %>
                    </div>
                    <div class="pull-right hidden-xs">
                        <strong>Bookstore Version 1.60</strong>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="/admin-assets/js/jquery-2.1.1.js"></script>
    <script src="/admin-assets/js/bootstrap.min.js"></script>
</body>

</html>

