<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /*#Profile input[type=text],input[type=password]{
        border: 1px solid !important;
    }*/
        .row{
            width:100%
        }
        #Profile input[type=submit]{
            background-color: #c82128 !important;
            color: white !important;
            font-size: 18px !important;
        }

        .countryWrper{
            display: block;
            width: 100%;
        }

        .su-st{
            width:100%;
            height: auto !important;
        }
       .btn-primary{
                color: #fff;
                background-color: #162e2d !important;
                border-color:#162e2d  !important;
       }
       #ContentPlaceHolder1_btnSave{
           color:white !important;
       }      
        .no-background-slider .addCartButton:hover, .no-background-slider .addCartButton:active, .btn-primary:hover, .btn-primary:active {
        box-shadow: 0px 0px 3px #888888 !important;
    background-color: #162e2d !important;
    border-color: #162e2d !important;       
        }
    #ContentPlaceHolder1_btnSave:focus{
           background-color: #162e2d !important;
           border-color: #162e2d !important;       
       }
        
        .billing-fields > div > div {
            text-align: left;
        }
    </style>

    <script>
        $(document).ready(function () {

            var Namecountry = '<%=Session["OtherCountry"] %>';
            var UID = '<%=Session["CustID"] %>';
            if (Namecountry == "OtherCountry")
            {
                $("#dvcountry").css('display', "none");
                document.getElementById('ContentPlaceHolder1_AddressUpdatePanel').style.display = "none";

            }
            else
            {
             //   document.getElementById('ContentPlaceHolder1_AddressUpdatePanel').style.visibility = 'block';
                $("#dvcountry").css('display', "none");
            }

            <%--$('#<%=textCurrentPassword.ClientID%>').blur(function () {
                alert('Hello');
            });--%>

        });

    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area "> 
				<div class="row">
					<div class="col-lg-12">
						<div class="breadcrumbs-menu">
							<ul>
								<li><a href="/index.aspx">Home</a></li>
								<li><a href="#" class="active">User Profile</a></li>
							</ul>
						</div>
					</div>
				</div> 
		</div>
	<!-- breadcrumbs-area-end -->

	<!-- entry-header-area-start -->
	<div class="entry-header-area" id="changeAddress"  runat="server">
		<div class="row">
			<div class="col-lg-12">
				<div class="entry-header-title"  style="text-align: center;    padding-bottom: 20px;">
					<h2 class="update-heading">Update personal details</h2>
				</div>
			</div>
		</div>
	</div>
	<!-- entry-header-area-end -->
    	<!-- entry-header-area-start -->
	<div class="entry-header-area" id="changepwd" runat="server">
		<div class="row">
			<div class="col-lg-12">
				<div class="entry-header-title" style="text-align: center;padding-bottom:20px">
					<h2 >Change Password</h2>
				</div>
			</div>
		</div>
	</div>
	<!-- entry-header-area-end -->

    <div class="user-login-area mb-70">
         
            <div class="row" style="text-align:center;">
                <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
            </div>            
            <div class="col-lg-offset-2 col-lg-8 col-md-offset-2 col-md-8 col-sm-12 col-xs-12" id="Change_Password" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upd_Pwd">
                        <ProgressTemplate>
                            <div style="position: absolute; top: 1%; height: 100%; width: 100%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center;z-index:99999;">
                                <div style="position: absolute; width: 100%; height: 50%; top: 50%;">
                                    <img src="/img/ring-alt.svg" />
                                </div>
                            </div> 
                        </ProgressTemplate>
                    </asp:UpdateProgress>
				    <asp:UpdatePanel ID="upd_Pwd" runat="server" AutoPostBack="false"  UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="billing-fields" id="Profile">
					            <div class="row">
                                    <div class="single-register col-md-4">
                                        <label>Email : </label>  </div>
                                        <div class="single-register col-md-8">
                                            <asp:TextBox ID="textEmail" class="form-control update-ph" data-toggle="tooltip" ToolTip="Email ID can`t be changed" ReadOnly="true" runat="server"></asp:TextBox> 
                                        </div>
                                   
                                </div>
                                <div class="row">
                                    <div class="single-register col-md-4">
                                        <label>Phone No : </label>
                                        </div>
                                        <div class="single-register col-md-8">
                                            <asp:TextBox ID="textPhone" class="form-control update-ph" data-toggle="tooltip" ToolTip="Mobile No. can`t be changed" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>
                                    </div> 
                                <div class="row">
                                    <div class="single-register col-md-4">
                                        <label>Current Password : </label>
                                         </div>
                                        <div class="single-register col-md-8">                                            
                                            <asp:TextBox ID="textCurrentPassword" EnableViewState="true" AutoPostBack="false" CausesValidation="false" OnTextChanged="textCurrentPassword_TextChanged" class="form-control update-ph" TextMode="Password" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="textCurrentPassword" CssClass="validator" runat="server" ID="RequiredFieldValidator5" 
                                                ErrorMessage="<i class='fa fa-warning'></i>" />
                                        </div>
                                    </div> 
                                <div class="row">
                                    <div class="single-register col-md-4">
                                        <label>New Password : </label>
                                         </div>
                                        <div class="single-register col-md-8">                                           
                                            <asp:TextBox ID="textNewPassword" class="form-control update-ph"  onkeyup="CheckPasswordStrength(this.value)" TextMode="Password" runat="server"></asp:TextBox>
                                             <asp:RequiredFieldValidator ControlToValidate="textNewPassword" CssClass="validator" runat="server" ID="RequiredFieldValidator1" 
                                                ErrorMessage="<i class='fa fa-warning'></i>" />
                                            <p class="password-strength" id="password_strength">Min 8 characters, Must contain an Uppercase(A-Z), a Lowercase(a-z), a Special ([$@$!%*#?&]) and a Numeric(0-9) character.</p>
                                        </div>
                                    </div> 
                                <div class="row">
                                        <div class="single-register col-md-4">
                                            <label>Confirm New Password : </label>
                                        </div>
                                        <div class="single-register col-md-8">                                           
                                            <asp:TextBox ID="textConfirmPassword" onkeyup="CheckConfirmPassword(this.value)"  TextMode="Password" class="form-control update-ph" runat="server"></asp:TextBox> 
                                             <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="textConfirmPassword" CssClass="validator" 
                                                ControlToCompare="textNewPassword" ErrorMessage="Password didn`t match" ></asp:CompareValidator>

                                            <asp:RequiredFieldValidator ControlToValidate="textConfirmPassword" CssClass="validator" runat="server" ID="RequiredFieldValidator2" 
                                                ErrorMessage="<i class='fa fa-warning'></i>" />
                                            <p class="password-strength" id="password_Confirm"></p>
                                        </div>
                                    </div>  
                                <script type="text/javascript">
                                    function CheckConfirmPassword(password) {
                                        var textNewbox = document.getElementById('<%=textNewPassword.ClientID %>');
                                        var submitbutton = document.getElementById('<%=btnSave.ClientID %>');
                                        if (textNewbox.value == password) {
                                            $('#password_Confirm').css({ 'display': 'none' });
                                            submitbutton.disabled = false;
                                        } else {
                                            $('#password_Confirm').text("Password didn't Match");
                                            submitbutton.disabled = true;
                                        }
                                    }
                                     function CheckPasswordStrength(password) {
                                            var textbox = document.getElementById('<%=textNewPassword.ClientID %>');
                                            var password_strength = document.getElementById("password_strength");
                                            var submitbutton = document.getElementById('<%=btnSave.ClientID %>');
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
                                    <%--function CheckPasswordStrength(password) {
                                        var textbox = document.getElementById('<%=textNewPassword.ClientID %>');
                                        //var password_strength = document.getElementById("password_strength");
                                        var submitbutton = document.getElementById('<%=btnSave.ClientID %>');
                                        //TextBox left blank.
                                        if (password.length == 0) {
                                            //password_strength.innerHTML = "";
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
                                        if (passed > 2 && password.length > 8) {
                                            passed++;
                                        }
                                        //Display status.
                                        var color = "";
                                        var strength = "";
                                        switch (passed) {
                                            case 0:
                                            case 1:
                                                strength = "Weak ((Min 8 characters) Contain atleast 1 Uppercase, 1 Lowercase, 1 numeric and 1 special character) ";
                                                color = "#cc3300";
                                                submitbutton.disabled = true;
                                                break;
                                            case 2:
                                                strength = "Good ((Min 8 characters) Contain atleast 1 Uppercase, 1 Lowercase, 1 numeric and 1 special character)";
                                                color = "darkorange";
                                                submitbutton.disabled = true;
                                                break;
                                            case 3:
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
                                        //password_strength.innerHTML = strength;
                                        textbox.style.color = color;
                                    }--%>

                                </script>  
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="textCurrentPassword" />
                        </Triggers>
				    </asp:UpdatePanel>  
                </div>
                <div class="col-lg-offset-2 col-lg-8 col-md-offset-2 col-md-8 col-sm-12 col-xs-12" id="Change_Address" runat="server">
                    <div class="billing-fields">
                        <div class="row">
                            <div class="single-register col-md-4">
                                <label>Email : </label>
                                 </div>
                                <div class="single-register col-md-8">
                                    <asp:TextBox ID="txtBilltoEmail" class="form-control update-ph" data-toggle="tooltip" ToolTip="Email ID can`t be changed" ReadOnly="true" runat="server"></asp:TextBox> 
                                </div>
                            </div>
                        <div class="row">
                            <div class="single-register col-md-4">
                                <label>Phone No : </label>
                                </div>
                                <div class="single-register col-md-8">
                                    <asp:TextBox ID="txtBilltoMobile" class="form-control update-ph" data-toggle="tooltip" ToolTip="Mobile No. can`t be changed" ReadOnly="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        
                        <div class="row">
                            <div class="single-register col-md-4">
                                <label>Name : </label>
                                </div>
                                <div class="single-register col-md-8">
                                    <asp:TextBox ID="txtBilltoName" class="form-control update-ph" data-toggle="tooltip" ToolTip="Enter Your full Name"  runat="server"></asp:TextBox>
                                </div>
                            </div>
                       <asp:UpdatePanel ID="AddressUpdatePanel" class="row" runat="server">
                                    <ContentTemplate>
                                        <div id="dvcountry" class="countryWrper"  runat="server">
                                                <div class="single-register col-md-4" id="dvcountrys" runat="server">								        
                                                    <label>Country <span>*</span></label>  
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="dd_Country" AutoPostBack="true" OnSelectedIndexChanged ="dd_Country_SelectedIndexChanged" 
                                                        CssClass="form-control su-st text-box" runat="server">
                                                        <asp:ListItem Value="Nil">Select Country</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
							                </div>
                                       
                                         
                                            <div  class="single-register col-md-4">
								                <label>State <span>*</span></label></div>
                                        <div  class="single-register col-md-8">
                                                <asp:DropDownList ID="dd_State" AutoPostBack="true" OnSelectedIndexChanged="dd_State_SelectedIndexChanged" 
                                                    CssClass=" su-st text-box form-control" runat="server">
                                                    <asp:ListItem Value="nil">Select State</asp:ListItem>
                                                </asp:DropDownList>
							                </div> 
                                    
                                        <div  class="single-register col-md-4">
                                            <label>City <span>*</span></label>
                                            </div>
                                        <div  class="single-register col-md-8">
                                            <asp:DropDownList ID="dd_City" CssClass="su-st text-box form-control" runat="server">
                                                <asp:ListItem Value="nil">Select City</asp:ListItem>
                                            </asp:DropDownList>                                        
                                        </div>
                                    </ContentTemplate>
                                    <%--<Triggers>
                                        <asp:PostBackTrigger ControlID="btnSignUp" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>                    
                        <div class="row" id="dv_ZipCode" runat="server">
                            <div style="margin-top: 10px;" class="single-register col-md-4">
                                <label style="white-space: nowrap;">Zip/Pin Code : </label>
                            </div>
                            <div style="margin-top: 10px;" class="single-register col-md-8">
                                <asp:TextBox ID="txtBilltoPincode" class="form-control update-ph" data-toggle="tooltip" ToolTip="Provide Pin Code" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="single-register col-md-4">
                                <label>Address : </label>
                            </div>
                            <div class="single-register col-md-8">
                                <asp:TextBox ID="txtBilltoAddress" class="form-control update-ph" data-toggle="tooltip" ToolTip="Provide Detailed Address along with Building No, Street and Nearest Landmark for easier and fast delivery." runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-offset-2 col-lg-8 col-md-offset-2 col-md-8 col-sm-12 col-xs-12">
				<div style="overflow: inherit;text-align: center;" class="single-register">
                    <asp:Button ID="btnSave" runat="server" class="btn btn-primary save-btn" OnClick="btnSave_Click" Text="Save Changes" />
				</div>
            </div>
         
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

