<%@ Page Title="" Language="C#" MasterPageFile="~/OuterMaster.master" AutoEventWireup="true" CodeFile="user_login.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/customecss/homeDCbooks.css" rel="stylesheet" />
    <link href="../css/customecss/dc-login-styles.css" rel="stylesheet" />
     <script type="text/javascript">
        function ValidatorUpdateDisplay(val) {
            if (typeof (val.display) == "string") {
                if (val.display == "None") {
                    return;
                }
                if (val.display == "Dynamic") {
                    val.style.display = val.isvalid ? "none" : "inline";
                    return;
                }

            }
            val.style.visibility = val.isvalid ? "hidden" : "visible";
            if (val.isvalid) {
                document.getElementById(val.controltovalidate).style.border = '1px solid #e9e9e9';
            }
            else {
                document.getElementById(val.controltovalidate).style.border = '1px solid #E60000';
            }
        }

        $(document).ready(function () {
            $("#ContentPlaceHolder1_btn_Forgotpwd").click(function () {
                if (!IsValidEmail(document.getElementById('ContentPlaceHolder1_textEmail_login').value)) {
                    alert("Please enter a valid Email address for password Recovery.");
                    return false;
                }
            });
        });
        function IsValidEmail(email) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(email);
        };

    </script>
    <!--CONTENT END-->

    <style>
        
        .MyCheckBox input[type="checkbox"]
        {
            margin-top:0px !important;
            display:inline-flex !important;
        }
        .span
        {            
            display:inline-flex !important;
        }
        .MyCheckBox label
        {            
            margin-top:0px !important;
        }

        #ContentPlaceHolder1_btnLogin{
            background-color: #3051a0;
            color: white;
            border-radius: 3px;
            width: 100%;
            height: 48px;
            font-size: 20px;
            border: none;
            font-weight: 600;
        }
        /*.remember-txt{
            white-space:nowrap;
        }*/
        .login-heading{
              border-bottom: 5px double #361298;
    width: 105px;
    margin: auto;
    padding-top: 30px;
        }
   
        @media only screen and (min-width:320px) {
            .forgot-password {
                font-size: 14px !important;
            }

            .login {
                margin-top: 40px;
            }
        }
        @media only screen and (min-width:576px) and (max-width:767px){
            .login-heading{
                float:left;
            }
        }
      
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumbs-area">
        <div class="row">
				<div class="col-lg-12">
					<div style="margin-left: 20px;" class="breadcrumbs-menu">
						<ul>
							<li><a href="/">Home</a></li>
							<li><a href="#" class="active">User Login</a></li>
						</ul>
					</div>
				</div>
			</div> 
	</div>
    <div  class="row">         
            <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
            <div id="div_checkout_guest" runat="server" visible="false">
                    <a href="onepagecheckout.aspx" class="btn btn-lg btn-primary btn-block">Checkout as Guest User</a>
                    <h3 class="text-center">OR</h3>
            </div>

		
                <asp:Panel ID="panel_login" runat="server" DefaultButton="btnLogin" class="col-lg-12 col-md-12 col-sm-12">
					<div style="display:none" class="login-title text-center">
						<h2 class="login-heading">Login</h2>
                        
                    </div>
					<div class="col-lg-offset-3 col-lg-5 col-md-offset-3 col-sm-offset-3 col-md-6 col-sm-12 col-xs-12">
						<div class="login-form">
                            <div style="text-align:center;padding-top:40px" class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                                <h3 style="font-size:18px">RETURNING CUSTOMER</h3>
                                <style>
                                    .alert {
                                            padding: 15px;
                                            /*margin-bottom: 20px;*/
                                            border: 1px solid #96c946;
                                            border-radius: 4px;
                                        }
                                </style>
                                <asp:Literal ID="ltr_msg_for_login" runat="server"></asp:Literal>
                            </div>
							<div class="col-lg-12 col-md-12 col-sm-12 floating-label-group" style="margin-top:20px">
                                <div style="display:none" class="col-lg-12 col-md-12 col-sm-12">
                                    <label style="font-size: 15px;" >Email Or Mobile No<span>*</span></label>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12" style="display:flex;padding-top: 23px;">
                                        <asp:TextBox ID="textEmail_login" runat="server" class="form-control" autocomplete="off" placeholder="Enter Email ID."></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="#cc3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="validator invalid-message"
                                            ValidationGroup="memberLogin" ControlToValidate="textEmail_login" runat="server" style="margin-left:-140px;width:120px;" ErrorMessage="Invalid Email ID"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="textEmail_login" CssClass="validator" ValidationGroup="memberLogin" ErrorMessage=""></asp:RequiredFieldValidator>
                                </div> 
                            </div>
							<div class="col-lg-12 col-md-12 col-sm-12 floating-label-group">
                                <div style="display:none"  class="col-lg-12 col-md-12 col-sm-12">
                                    <label style="font-size: 15px;" >Password <span>*</span></label>
                                </div>
								<div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:RequiredFieldValidator ControlToValidate="textPassword_Login" CssClass="validator" runat="server" ValidationGroup="memberLogin" ID="RequiredFieldValidator1" ErrorMessage="" />
                                    <asp:TextBox ID="textPassword_Login" runat="server" autocomplete="off" TextMode="Password" class="form-control" placeholder="Password"></asp:TextBox>
                                </div>
                                        
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 forgot-pswd">                                        
                            
                                    <asp:Button ID="Button1" runat="server" CssClass="button btn btn-primary"  OnClick="btnLogin_Click" ValidationGroup="memberLogin" Text="Login" />
                                
                                
                              
                                    <asp:button ID="btn_Forgotpwd" CssClass="forgot-password" OnClick="btn_Forgotpwd_Click"  runat="server" Text="Forgot Password?" />
                                
                            </div>
                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12" style="border-bottom:1px solid #eee9e9;padding-top:19px"></div>
                            <div style="display:none" class="col-lg-12 col-md-12 col-sm-12 login-button">
                                <div class="col-lg-4 col-md-4 col-sm-4"> </div>
                                <div class="col-lg-8 col-md-8 col-sm-8 login"> 
                                    <asp:Button ID="btnLogin" runat="server" CssClass="button"  OnClick="btnLogin_Click" ValidationGroup="memberLogin" Text="Login" />
                                </div>                                        
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 or-text" style="display:none;">
                                <span>Or</span>
                             </div>
                            <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12 login-facebook" style="display:none;">
                                <a href="#">
                                    <i class="fa fa-facebook"></i>
                                </a>
                                 <a href="#">
                                     <span>Login with Facebook</span>
                                </a>
                            </div>
                             <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12" style="border-bottom:1px solid #eee9e9;padding-top:19px;display:none"></div>
                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 register-btn">
                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12 sign-in">
                                   <asp:CheckBox ID="chb_RememberMe" runat="server"  CssClass="MyCheckBox"  />
                                    <asp:Label ID="lbl_RememberMe"  runat="server" CssClass="remember-txt" Text="Keep me signed in." ToolTip="Choosing 'Keep me signed in' reduces the number of times you're asked to Sign-In on this device.<br /> To keep your account secure, use this option only on your personal devices."></asp:Label>
                                
                                    </div>
                                <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 register-here">
                                    Don't have an account? <a href="/Customer/register">Click here</a> to Sign Up 
                                </div>
                                    </div>
                            </div>
							<div class="col-lg-12 col-md-12 col-sm-12 request-button" style="display:none;">
                                <div class="col-lg-4 col-md-4 col-sm-4"></div>
                                <div class="col-lg-8 col-md-8 col-sm-8"> 
                                    <Button >Request OTP</Button> 
                                </div>                                        
                            </div>
                            <div style="display:none" class="col-lg-12 col-md-12 col-sm-12  register">
                                <div class="col-lg-4 col-md-4 col-sm-4"></div>
                                <div class="col-lg-8 col-md-8 col-sm-8"> 
                                        <a href="/Customer/register" >New User? Create an account</a> 
                                </div>                                       
                            </div>  
						</div>
                        
                        <div class="col-lg-offset-3 col-lg-6 col-md-offset-3 col-sm-offset-3 col-md-6 col-sm-12 col-xs-12" style="display:none;">
                        <div class="col-md-6 col-lg-6  text-center mt-30">
                            <span style="font-weight: 600;color: black;">OR</span>
                        </div>
                           </div>
                        
                         <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 guest-container" style="display:none;">
                                <div class="col-md-12 col-lg-12">
                                    <button class="guest-btn">CONTINUE AS A GUEST</button>
                                </div>
                             <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 text-center guest">
                                 <span>You'll have the option to register for an account after your purchase</span>
                             </div>
                            </div>
                    </div>
				</asp:Panel>
		
    </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

