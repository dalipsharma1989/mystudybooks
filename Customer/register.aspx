 <%@ Page Title="" Language="C#" MasterPageFile="~/OuterMaster.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <link href="../css/customecss/dc-login-styles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <%--<style>
        .su-ph{
            margin-left: 16%;
        }
        .password-strength {
            font-size: 12px;
            color: #fff;
            padding: 0 12px 0;
        }

        .alert-danger {
            color: white !important;
            background-color: #3051a0 !important;
            border-color: #f5c6cb !important;
            text-align: center !important;
            font-size: 20px !important;
        }

        
        #signUp input[type=submit]{
            background-color: #3051a0 !important;
            color: white !important;
            font-size: 25px !important;
        }

        #ContentPlaceHolder1_panel_signup input[type=password],input[type=text],textarea,select{
            -webkit-transition: all 0.30s ease-in-out !important;
            -moz-transition: all 0.30s ease-in-out !important;
            -ms-transition: all 0.30s ease-in-out !important;
            -o-transition: all 0.30s ease-in-out !important;
            outline: none !important;
            padding: 3px 0px 3px 3px !important;
            margin: 5px 1px 3px 0px !important;
                border: 4px double gray;
            
        }
        #ContentPlaceHolder1_panel_signup input[type=password]:focus,input[type=text]:focus, textarea:focus,select:focus {
                                  box-shadow: 0 0 5px rgba(81, 203, 238, 1) !important;
                                  padding: 3px 0px 3px 3px !important;
                                  margin: 5px 1px 3px 0px !important;
                                 
                                 
                                  
        }
        #signUp input[type=submit] {
    width: 100%;
    padding: 10px;
    color: white !important;
    border: none;
    background: #361298;
    font-size: 21px !important;
}
        #ContentPlaceHolder1_CompareValidator1 {
            position: relative !important;
        }
        #ContentPlaceHolder1_RequiredFieldValidator4{
            position: relative !important;
        }
        #ContentPlaceHolder1_RequiredFieldValidator5{
            position: relative !important;
        }
        #YourRegion > div {
            padding-left: 10px;
    padding-right: 10px;
        }
         @media (max-width:1080px) {
            .containercl {
                font-size: 16px !important;
            }
        }

    </style>--%>
    <script>

        $(document).ready(function () {            
            
            SelectradioButton();

        });



        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //Specific code for partial postbacks can go in here.
                $(document).ready(function () {
                    $('[data-toggle="tooltip"]').tooltip();

                    var value = $("[id*=rb_user_type] input:checked").val();
                    if (value == "Teacher") {
                        $("#div_teacher_document").show(500, 'swing');
                        $("#div_school_list").hide(500, 'swing');
                    }
                    else if (value == "School Student") {
                        $("#div_school_list").show(500, 'swing');
                        $("#div_teacher_document").hide(500, 'swing');
                    }
                    else {
                        $("#div_school_list").hide(500, 'swing');
                        $("#div_teacher_document").hide(500, 'swing');
                    }

                });
                //$.getScript("js/tooltip.js", function () {
                //    //alert("Script loaded and executed.");
                //});


                $("[id*=rb_user_type] input").click(function () {
                    var value = $("[id*=rb_user_type] input:checked").val();
                    if (value == "Teacher") {
                        $("#div_teacher_document").show(500, 'swing');
                        $("#div_school_list").hide(500, 'swing');
                    }
                    else if (value == "School Student") {
                        $("#div_school_list").show(500, 'swing');
                        $("#div_teacher_document").hide(500, 'swing');
                    }
                    else {
                        $("#div_school_list").hide(500, 'swing');
                        $("#div_teacher_document").hide(500, 'swing');
                    }
                });

            }
        }
    </script>
    <style>
     

    .containercl {
          display: block;
          position: relative;
          padding-left: 35px;
          margin-bottom: 12px;
          cursor: pointer;
          font-size: 22px;
          -webkit-user-select: none;
          -moz-user-select: none;
          -ms-user-select: none;
          user-select: none;
        }

/* Hide the browser's default radio button */
.containercl input {
  position: absolute;
  opacity: 0;
  cursor: pointer;
}

/* Create a custom radio button */
.checkmark {
  position: absolute;
  top: 0;
  left: 0;
  height: 25px;
  width: 25px;
  background-color: #382f2f;
  border-radius: 50%;
}

/* On mouse-over, add a grey background color */
.containercl:hover input ~ .checkmark {
  background-color: #ccc;
}

/* When the radio button is checked, add a blue background */
.containercl input:checked ~ .checkmark {
  background-color: #2196F3;
}

/* Create the indicator (the dot/circle - hidden when not checked) */
.checkmark:after {
  content: "";
  position: absolute;
  display: none;
}

/* Show the indicator (dot/circle) when checked */
.containercl input:checked ~ .checkmark:after {
  display: block;
}

/* Style the indicator (dot/circle) */
.containercl .checkmark:after {
 	top: 9px;
	left: 9px;
	width: 8px;
	height: 8px;
	border-radius: 50%;
	background: white;
}
.su-name{
        border: 4px double gray;
}
.e-mail:focus{
    border:4px double grey !important;
}
.e-mail{
    border:4px double grey !important;
}
.billing-fields{
    margin-top: 30px;
}
.signup-heading{
        border-bottom: 5px double #361298;
    width: 180px;
    margin: auto;
    padding-top: 30px;
}
.single-register label{
      white-space: nowrap;  
      font-size:15px;
}
.single-register input{
    font-size:16px;
}
.chosen-select{
    font-size:16px;
}
@media only screen and (min-width:576px){
    .su-ph{
            width: 76% !important;
    }
}

.asterisk::-webkit-input-placeholder {
    color:    #f00;
    content:"*";
}
.asterisk:-moz-placeholder {
   color:    #f00;
   content:"*";
   opacity:  1;
}
.asterisk::-moz-placeholder {
   color:    #f00;
   content:"*";
   opacity:  1;
}
.asterisk:-ms-input-placeholder {
   color:    #f00;
   content:"*";
}



    </style>


    <!-- breadcrumbs-area-start -->
	    <div class="breadcrumbs-area"> 
		    <div class="row top-menu">
			    <div class="col-lg-12 col-md-12 col-sm-12">
				    <div class="breadcrumbs-menu">
					    <ul>
						    <li><a href="/">Home</a></li>
                            <li><a href="user_login.aspx" class="active">Login</a></li>
						    <li><a href="#" class="active">Register</a></li>
					    </ul>
				    </div>
			    </div>
		    </div> 
	    </div>
		<!-- breadcrumbs-area-end -->
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <asp:Literal ID="ltr_msg_for_signup" runat="server"></asp:Literal>
        <div class="user-login-area"> 
                <asp:Panel ID="panel_signup" runat="server" DefaultButton="btnSignUp" class="row signup-div">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/img/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="position:fixed;top:45%;left:45%;" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
					<div class="col-lg-12 col-md-12 col-sm-12">
						<div style="display:none" class="login-title text-center">
							<h2 class="signup-heading">Sign Up</h2>
						</div>
					</div>

                   <%-- new code--%>
                    <div class="col-lg-offset-3 col-lg-6 col-md-offset-3 col-sm-offset-3 col-md-6 col-sm-12 col-xs-12">
						<div  class="login-form">
                            <div style="text-align:center;padding-top:40px" class="">
                                <h3 style="font-size:18px">SIGN UP NOW!</h3>
                            </div>
                            <div class="">
                                <asp:TextBox ID="textName" runat="server" class="register-ph form-control asterisk" placeholder="First Name *" Width="100%"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="textName" runat="server"  ValidationGroup="memberSignUp" CssClass="validator"  ID="RequiredFieldValidator8" 
                                    ErrorMessage="**Required" />
                                
                            </div>
                            <div class="">
                                <asp:TextBox ID="textNameL" runat="server" class="register-ph form-control last-name" placeholder="Last Name" Width="100%"></asp:TextBox>
                            </div>
                            <div class="">
                                   <%--<asp:TextBox ID="TextBox1" runat="server" autocomplete="off" CssClass="register-ph" placeholder="Email ID"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textEmail" CssClass="validator" 
                                                            ValidationGroup="memberSignUp" ErrorMessage="**Required"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="#cc3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                                CssClass="validator" ValidationGroup="memberSignUp" ControlToValidate="textEmail" runat="server" ErrorMessage="Invalid Email ID">
                                                        </asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="textEmail" runat="server" autocomplete="off" CssClass="register-ph form-control asterisk" placeholder="Email ID *" Width="100%"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="#cc3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        CssClass="validator" ValidationGroup="memberSignUp" ControlToValidate="textEmail" runat="server" ErrorMessage="Invalid Email ID">
                                </asp:RegularExpressionValidator>    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="textEmail" CssClass="validator" 
                                        ValidationGroup="memberSignUp" ErrorMessage="**Required"></asp:RequiredFieldValidator>
                                    
                            </div>
                            <div class="">

                                <%--<asp:TextBox ID="TextBox2" runat="server" autocomplete="off" CssClass="register-ph  mobile-ph"  placeholder="Phone"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" CssClass="validator" ValidationGroup="memberSignUp" runat="server" 
                                ValidationExpression="[0-9]{10}" ControlToValidate="textPhone" ErrorMessage="Invalid Mobile No"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ControlToValidate="textPhone" runat="server" ValidationGroup="memberSignUp" CssClass="validator"
                                ID="RequiredFieldValidator9" ErrorMessage="**Required" />--%>
                                <asp:TextBox ID="textPhone" runat="server" autocomplete="off" CssClass="register-ph form-control mobile-ph  asterisk"  Width="100%"
                                    title="Please enter valid Mobile No." placeholder="Mobile No *" pattern="^[0-9]{1,10}$"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="validator" ValidationGroup="memberSignUp" runat="server" 
                                    ValidationExpression="[0-9]{10}" ControlToValidate="textPhone" ErrorMessage="Invalid Mobile No"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ControlToValidate="textPhone" runat="server" ValidationGroup="memberSignUp" CssClass="validator"
                                    ID="RequiredFieldValidator3" ErrorMessage="**Required" />
                            </div>
                            <div class="" style="display:none;">
                                <div style="margin-top:11px">
                                    <div class="col-sm-4">Gender:</div> 
                                    <div class="col-sm-4">
                                        <input name="gender" type="radio" value="1"  checked="checked"   > Male
                                    </div>
                                    <div class="col-sm-4"><input name="gender" type="radio" value="0"> Female</div>
                                </div> 
                             </div>
                            <div class="" style="border-bottom:1px solid #eee9e9;padding-top:19px;margin-bottom: 15px;display:none;"></div>
                               <div class="">
                                    <%--<asp:RequiredFieldValidator ControlToValidate="textPassword" CssClass="validator" runat="server" ValidationGroup="memberSignUp" ID="RequiredFieldValidator1" 
                                        ErrorMessage=""> </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TextBox1" runat="server" onkeyup="CheckPasswordStrength(this.value);" TextMode="Password" data-toggle="tooltip" CssClass="su-pw  e-mail" 
                                        placeholder="Password" data-title="Min 8 characters, Must contain an Uppercase(A-Z), a Lowercase(a-z) and a Numeric(0-9) character." Width="100%" >
                                    </asp:TextBox>
                                    <p class="password-strength" id="password_strength"></p>--%>
                                    
                                    <asp:RequiredFieldValidator ControlToValidate="textPassword" CssClass="validator" runat="server" ValidationGroup="memberSignUp" ID="RequiredFieldValidator6" ErrorMessage=""> </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="textPassword" runat="server" onkeyup="CheckPasswordStrength(this.value);" TextMode="Password" data-toggle="tooltip" CssClass="register-ph form-control mobile-ph  asterisk" 
                                            placeholder="Password *" data-title="Min 8 characters, Must contain an Uppercase(A-Z), a Lowercase(a-z) and a Numeric(0-9) character." Width="100%" >
                                        </asp:TextBox>
                                        <p class="password-strength" id="password_strength">Min 8 characters, Must contain an Uppercase(A-Z), a Lowercase(a-z), a Special ([$@$!%*#?&]) and a Numeric(0-9) character.</p>
                                </div>
                              <div class="">
                                  <asp:TextBox ID="textConfirmPassword" runat="server" TextMode="Password" Width="100%" CssClass="register-ph form-control mobile-ph confirm-pswd asterisk" placeholder="Confirm Password *"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="textConfirmPassword" CssClass="validator"  ControlToCompare="textPassword" 
                                        ValidationGroup="memberSignUp" ErrorMessage="Password Didn`t Match" ></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ControlToValidate="textConfirmPassword" runat="server" CssClass="validator" ValidationGroup="memberSignUp" 
                                        ID="RequiredFieldValidator7" ErrorMessage=""></asp:RequiredFieldValidator>
                                 <%--<asp:TextBox ID="TextBox4" runat="server" TextMode="Password"  CssClass="register-ph mobile-ph" placeholder="Confirm Password"></asp:TextBox>
                                 <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="textConfirmPassword" CssClass="validator"  ControlToCompare="textPassword"  
                                     ValidationGroup="memberSignUp" ErrorMessage="Password Didn`t Match" ></asp:CompareValidator>
                                 <asp:RequiredFieldValidator ControlToValidate="textConfirmPassword" runat="server" CssClass="validator validator1"  ValidationGroup="memberSignUp" ID="RequiredFieldValidator11" 
                                     ErrorMessage=""></asp:RequiredFieldValidator>--%>
                              </div>
                            <div class="">
                       <div class="account-btn">
                           <asp:Button ID="btnSignUp" ValidationGroup="memberSignUp"  CssClass="btn-primary" OnClientClick="checkControlforValidation()" OnClick="btnSignUp_Click" runat="server" Text="Create Account"/>
                           <%--<input name="" type="submit" value="Sign up now" class="blulogin">--%>
                       </div>
                        <div class="col-md-6 text-right"></div>
                               </div>
                             <div class="" style="border-bottom:1px solid #eee9e9;padding-top:19px;margin-bottom: 15px;"></div>
                            <div class=" text-center">Already have an account? <a href="user_login.aspx">Click here</a> to sign in </div>
                      
                            </div>
                        </div>
                     

					<div style="display:none" class="col-lg-12 col-md-12 col-sm-12" style="margin:auto;">
                      
                            <div class="billing-fields"> 
							    <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">
                                                        <label>First Name <span>*</span></label>
                                                    </div>                                                    
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">
                                                        
                                                    </div>
                                                </div>                                                
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">
                                                        <label>Last Name </label>
                                                    </div>                                                    
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">
                                                        
                                                    </div>                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div> 
							    </div>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">
                                                        <label>Email Address<span>*</span></label>
                                                    </div>                                                    
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">
                                                        
                                                    </div>                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">
                                                        <label>Mobile<span>*</span></label>
                                                    </div>                                                    
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">
                                                        <span class="su-dd" >
                                                            <asp:DropDownList ID="dd_country_code" CssClass="chosen-select" data-toggle="tooltip" title="Select Country Code" 
                                                                runat="server" Width="20%">
                                                                <asp:ListItem Selected="True">+91</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </span>
                                                        
                                                    </div>                                                    
                                                </div> 
                                            </div>                                            
                                        </div>
                                    </div>  
							    </div>    
                                
                            
                                <asp:UpdatePanel ID="AddressUpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <div class="col-lg-4 col-md-4">
                                                            <div class="single-register">
                                                            <label>Country <span>*</span></label>
                                                                </div>
                                                        </div>
                                                        <div class="col-lg-8 col-md-8">
                                                            <div class="single-register">
                                                                <asp:DropDownList ID="dd_Country" AutoPostBack="true" OnSelectedIndexChanged="dd_Country_SelectedIndexChanged" CssClass="chosen-select su-co" runat="server">
                                                                    <asp:ListItem Value="Nil">Select Country</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="dd_Country" ValidationGroup="memberSignUp"  
                                                                            ValueToCompare="Nil" Operator="NotEqual" ErrorMessage="**Required"></asp:CompareValidator>
                                                                        <asp:CustomValidator ID="CustomValidator2" ControlToValidate="dd_Country" runat="server" Display="Dynamic">
                                                                            <i class="fa fa-warning" data-toggle="tooltip" title="Please Select Country"></i>&nbsp;Please Select Country
                                                                        </asp:CustomValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div> 
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                         <div class="col-lg-4 col-md-4">
                                                              <div class="single-register">
                                                            <label>State <span>*</span></label>
                                                                  </div>
                                                        </div>
                                                        <div class="col-lg-8 col-md-8">
                                                            <div class="single-register">
                                                                <asp:DropDownList ID="dd_State" AutoPostBack="true" OnSelectedIndexChanged="dd_State_SelectedIndexChanged" CssClass="chosen-select su-st" runat="server">
                                                                    <asp:ListItem Value="Nil">Select State</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="dd_State" ValidationGroup="memberSignUp"  
                                                                            ValueToCompare="Nil" Operator="NotEqual" ErrorMessage="**Required"></asp:CompareValidator>
                                                                        <asp:CustomValidator ID="CustomValidator1" ControlToValidate="dd_State" runat="server" Display="Dynamic">
                                                                            <i class="fa fa-warning" data-toggle="tooltip" title="Please Select State"></i>&nbsp;Please Select State
                                                                        </asp:CustomValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div> 
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <div class="col-lg-4 col-md-4">
                                                            <div class="single-register">
                                                                <label>City <span>*</span></label> 
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-8 col-md-8">
                                                            <div class="single-register"> 
                                                                <asp:DropDownList ID="dd_City" CssClass="chosen-select su-ci" runat="server">
                                                                    <asp:ListItem Value="Nil">Select City</asp:ListItem>
                                                                </asp:DropDownList> 
                                                                <%--<asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="dd_City" ValidationGroup="memberSignUp"  
                                                                            ValueToCompare="Nil" Operator="NotEqual" ErrorMessage="**Required"></asp:CompareValidator>
                                                                        <asp:CustomValidator ID="CustomValidator5" ControlToValidate="dd_City" runat="server" Display="Dynamic">
                                                                            <i class="fa fa-warning" data-toggle="tooltip" title="Please Select City"></i>&nbsp;Please Select City
                                                                        </asp:CustomValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>                                                    
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <div class="col-lg-4 col-md-5">
                                                            <div class="single-register">
                                                                <label id="postalCode" >PIN Code</label> <span ">*</span> 
                                                                <i id="pinPostcode"  style="margin-top: 11px;font-size: 16px;color: black;"  class="fa fa-info-circle" data-toggle="tooltip" title="Provide PIN Code"></i>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-8 col-md-8">
                                                            <div class="single-register">
                                                                <asp:TextBox ID="textPinCode" runat="server" CssClass="su-pc" placeholder="PinCode" Width="100%"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ControlToValidate="textPinCode" CssClass="validator" runat="server"  ValidationGroup="memberSignUp"
                                                                    ID="RequiredFieldValidator4" ErrorMessage="**Required" />
                                                                <asp:CustomValidator ID="CustomValidator4"  ControlToValidate="textPinCode" runat="server" Display="Dynamic">
                                                                    <i class="fa fa-warning" data-toggle="tooltip" title="Enter PinCode"></i>&nbsp;Enter PinCode
                                                                </asp:CustomValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div> 
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSignUp" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="col-lg-2 col-md-2">
                                            <div class="single-register">
							                    <label>Address <span ">*</span>
                                                    <i class="fa fa-info-circle" data-toggle="tooltip"  
                                                        title="Provide Detailed Address along with Building No, Street and Nearest Landmark for easier and fast delivery."></i>
							                    </label>							                     
							                </div>
                                        </div>
                                        <div class="col-lg-10 col-md-10">
                                            <div class="single-register">                       
                                                <asp:TextBox ID="textAddress" runat="server" CssClass="su-bp" Width="99%" TextMode="MultiLine"></asp:TextBox>                      
	                                           <%-- <asp:RequiredFieldValidator ControlToValidate="textAddress" CssClass="validator" runat="server" ValidationGroup="memberSignUp"  ID="RequiredFieldValidator5" ErrorMessage="**Required" />                                                
                                                <asp:CustomValidator ID="CustomValidator3" ControlToValidate="textAddress" runat="server" Display="Dynamic"> <i class="fa fa-warning" data-toggle="tooltip" title="Enter Plot No"></i>&nbsp;Enter address .
                                                </asp:CustomValidator>--%>
                                            </div>
                                        </div> 
                                    </div>
                               
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">  
                                                        <label>School / College</label> 
                                                    </div>
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">  
                                                        <asp:TextBox ID="txtSchoolColUni" runat="server" CssClass="su-bp" Width="100%" ></asp:TextBox> 
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">  
                                                        <label>Class / Year / Session</label> 
                                                    </div>
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">  
                                                        <asp:TextBox ID="txtClassYearSession" runat="server" CssClass="su-bp" Width="100%" ></asp:TextBox> 
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                 <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">  
                                                        <label>Stream</label> 
                                                    </div>
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">  
                                                        <asp:TextBox ID="txtstream" runat="server" CssClass="su-bp" Width="100%" ></asp:TextBox> 
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">  
                                                        <label>Other info 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip"  
                                                                title="Other information if any."></i>
                                                        </label> 
                                                    </div>
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">  
                                                        <asp:TextBox ID="txtOtherInfo" runat="server" CssClass="su-bp" Width="100%" ></asp:TextBox> 
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">  
                                                        <label>Password <span>*</span></label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">  
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="col-lg-4 col-md-4">
                                                    <div class="single-register">  
                                                        <label>Confirm Password</label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-8 col-md-8">
                                                    <div class="single-register">  
                                                        
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div> 
                                    </div> 
                                    <br />
                                    <div class="col-lg-12 col-md-12 col-sm-12" >
                                        <div class="single-register single-register-3" style="text-align:center;font-size:25px;margin-top:20px"> 
								            <input id="rememberme" runat="server" type="checkbox" name="rememberme" style="height:25px;width:25px;" value="forever"/>
								            <label class="inline" style="vertical-align: top;font-size: 18px;padding-top: 6px;">
                                                I agree <a href="#"> Terms & Condition</a>
								            </label>
							            </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="single-register" id="signUp" style="text-align:center;padding-top:25px;">
								                                             
							            </div>
                                    </div> 
                                </div> 
                            </div>
                        
                    </div>
                </asp:Panel>             
        </div>
    
    <script type="text/javascript">

        function checkControlforValidation() {
            <%--var Namecountry = '<%=Session["OtherCountry"] %>';
            if (Namecountry == "OtherCountry") {                             
            }
            else {                
                
            }--%>
            if (document.getElementById("RestOfWorld").checked == true) {

            }
            else {
                if ($("#ContentPlaceHolder1_dd_State").val() == "" || $("#ContentPlaceHolder1_dd_State").val() == null || $("#ContentPlaceHolder1_dd_State").val().toLowerCase() == "nil") {
                    $("#ContentPlaceHolder1_dd_State").focus();
                    alert('Please Select State !!!');
                    return false
                }
                if ($("#ContentPlaceHolder1_dd_City").val() == "" || $("#ContentPlaceHolder1_dd_City").val() == null || $("#ContentPlaceHolder1_dd_City").val().toLowerCase() == "nil") {
                    $("#ContentPlaceHolder1_dd_City").focus();
                    alert("Please Select City!!!")
                    return false
                }
                
                
            }
        }

        function SelectradioButton(elementRef) {
            var Namecountry = '<%=Session["OtherCountry"] %>';            
            var ElementID = "";
            if (elementRef == null || elementRef == undefined) {
                if(Namecountry=="OtherCountry"){
                    ElementID = "RestOfWorld"
                    //document.getElementById("RestOfWorld").checked = true;
                } else {
                    ElementID = ""
                   // document.getElementById("IndiaSelected").checked = true;
                }
            } else {
                ElementID=elementRef.id;
            }

            if (ElementID == "RestOfWorld") {
                document.getElementById("ContentPlaceHolder1_dd_country_code").style.display = "none";
                $("#dv_contcode").css('display', "none");
                document.getElementById("ContentPlaceHolder1_AddressUpdatePanel").style.display = "none";
                $("#postalCode")[0].innerText = "ZIP Code";
                $('#ContentPlaceHolder1_textPinCode').attr('placeholder', "Enter Zipcode");
                //$("#pinPostcode")[0].innerText = "Provide Zip Code";
                $("#pinPostcode").attr('title', "Provide Zip Code");

                $("#div_mobile").css('width', "100%");
                //$("#div_mobile").css('margin-left', "0px");

                $("#ContentPlaceHolder1_restworld").val("Others");

                //document.getElementById("div_mobile").style.width = "119% !important";
                //document.getElementById("div_mobile").style.marginLeft = "0px !important";
            } else {
                document.getElementById("ContentPlaceHolder1_dd_country_code").style.display = "inline-block";
                $("#dv_contcode").css('display', "inline-block");
                document.getElementById("ContentPlaceHolder1_AddressUpdatePanel").style.display = "block";
                $("#postalCode")[0].innerText = "PIN Code";
                $('#ContentPlaceHolder1_textPinCode').attr('placeholder', "Enter Pincode");
                //$("#pinPostcode")[0].innerText = "Provide Pin Code";
                $("#pinPostcode").attr('title', "Provide Pin Code");
                $("#div_mobile").css('width', "78%");
                //$("#div_mobile").css('margin-left', "68px");                
                //document.getElementById("dvcountry").style.display = "none";
                $("#dvcountry").css('display', "none");
                $("#ContentPlaceHolder1_restworld").val("INDIA");

            }
        }
                function CheckPasswordStrength(password) {
                    var textbox = document.getElementById('<%=textPassword.ClientID %>');
                    var password_strength = document.getElementById("password_strength");
                    var submitbutton = document.getElementById('<%=btnSignUp.ClientID %>');
                    //TextBox left blank.
                    if (password.length == 0) {
                    $('#password_strength').text('Min 8 characters, Must contain an Uppercase(A-Z), a Lowercase(a-z), a Special ([$@$!%*#?&]) and a Numeric(0-9) character.');
                    //password_strength.innerHTML = "Min 8 characters, must contain an Uppercase,a Special, a Lowercase and a numeric character.";
                    $('#password_strength').css({ 'color': 'red' });
                    return;
                    }

                    //Regular Expressions.
                    var regex = new Array();
                    regex.push("[A-Z]"); //Uppercase Alphabet.
                    regex.push("[a-z]"); //Lowercase Alphabet.
                    regex.push("[0-9]"); //Digit.
                    regex.push("[$@$!%*#?&]"); //Special Character.

                    var passed = 0;

                    //Validate for each Regular Expression.
                    for (var i = 0; i < regex.length; i++) {
                    if (new RegExp(regex[i]).test(password)) {
                        passed++;
                    }
                    }

                    //Validate for length of Password.
                    if (passed > 2 && password.length > 6) {
                    passed++;
                    }

                    //Display status.
                    var color = "";
                    var strength = "";
                    switch (passed) {
                    case 0:
                    case 1:
                        strength = "Weak";
                        color = "#cc3300";
                        submitbutton.disabled = true;
                        break;
                    case 2:
                        strength = "Weak";
                        color = "#cc3300";
                        submitbutton.disabled = true;
                        break;
                    case 3:
                        strength = "Good";
                        color = "darkorange";
                        submitbutton.disabled = false;
                        break;
                    case 4:
                        strength = "Strong";
                        color = "green";
                        submitbutton.disabled = false;
                        break;
                    case 5:
                        strength = "Very Strong";
                        color = "darkgreen";
                        submitbutton.disabled = false;
                        break;
                    }

                    if (strength == "Weak") {
                    $('#password_strength').text('Min 8 characters, must contain an Uppercase,a Special, a Lowercase and a numeric character. Your Password is too ' + strength);
                    $('#password_strength').css({ 'color': 'red' });
                    } else if (strength == "Good") {
                    $('#password_strength').text('Your Password is ' + strength);
                    $('#password_strength').css({ 'color': color });
                    }else if (strength == "Strong") {
                    $('#password_strength').text('Your Password is  ' + strength);
                    $('#password_strength').css({ 'color': color });
                    } else if (strength == "Very Strong") {
                    $('#password_strength').text('Your Password is ' + strength);
                    $('#password_strength').css({ 'color': color });
                    } 
                    textbox.style.color = color;
                    }

            </script>
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
    </script>
    <!--CONTENT END-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <style>
        .chosen-select{
            width : 100%;
        }
        .su-cc{
            position: absolute;
            width: 15%;
        }
     </style>
    <script type="text/javascript">

        $(document).ready(function () {
            check_user_type();
        });
        
        $("[id*=rb_user_type] input").click(function () {
            check_user_type();
        });

        function check_user_type() {
            var value = $("[id*=rb_user_type] input:checked").val();
            if (value == "Teacher") {
                $("#div_teacher_document").show(500, 'swing');
                $("#div_school_list").hide(500, 'swing');
            }
            else if (value == "School Student") {
                $("#div_school_list").show(500, 'swing');
                $("#div_teacher_document").hide(500, 'swing');
            }
            else {
                $("#div_school_list").hide(500, 'swing');
                $("#div_teacher_document").hide(500, 'swing');
            }
        }
    </script>
</asp:Content>

