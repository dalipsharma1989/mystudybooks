<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="authorinvitation.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .su-ph{
            /*margin-left: 16%;*/
        }
        .password-strength {
            font-size: 12px;
            color: #fff;
            padding: 0 12px 0;
        }
        .single-register input{     
            background: rgba(0, 0, 0, 0) none repeat scroll 0 0 !important;           
            border: 1px solid #2d2e30 !important;
            box-shadow: none !important;
            color: #333 !important;
            font-size: 14px !important;
            height: 45px !important;
            padding-left: 10px !important;
            width: 100% !important;
            font-family: 'Rufina', serif !important;
            font-weight: 400 !important;

        }
    </style>
    <script>
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
        .password-strength {
            font-size: 12px;
            color: #fff;
            padding: 0 12px 0;
        }
    </style>
    <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area mb-70"  style="margin-bottom:20px !important;">
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<div class="breadcrumbs-menu">
							<ul>
								<li><a href="/">Home</a></li>
								<li><a href="#" class="active">Author Invitation</a></li>
							</ul>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- breadcrumbs-area-end -->
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <asp:Literal ID="ltr_msg_for_signup" runat="server"></asp:Literal>
        <div class="user-login-area mb-70">
            <div class="container">
                <asp:Panel ID="panel_signup" runat="server" DefaultButton="btnSignUp" class="row">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/img/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="position:fixed;top:45%;left:45%;" />
                                </div>
                            </ProgressTemplate>
                    </asp:UpdateProgress>
					<div class="col-lg-12">
						<div class="login-title text-center mb-30">
							<h3 style="text-decoration:underline;">Author Invitation</h3>
                            <br />
                            <p style="text-align:left !important;">
                                We as a Publishers serves as an ideal platform for both the established as well as budding authors for getting their books published at an earlier date. Our main aim is to promote Authors globally and we invite authors to join hands with us.
                                <br />
                                Authors who are interested in getting their manuscripts published kindly fill the form undergiven and e-mail the same to us, i.e. duly filled Form (E-mail ID: info@ssjpd.com) with list of contents and one/two sample chapters for the proposed manuscript. We assure to get back to you at the earliest.
                            </p>
						</div>
					</div>
                    <div class="col-lg-12">
						<div class="login-title text-left">
							<h6 style="text-decoration:underline;">NEW BOOK PROPOSAL AUTHOR AND TITLE INFORMATION</h6>
                            <br />                            
						</div>
					</div>
                    <div class="col-lg-12">
							<div class="row">
								<div class="col-lg-6"> 
                                    <div class="single-register">
                                        <label>Author’s / Editor’s full name:<span>*</span></label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                        <div class="single-register">
                                            <asp:RequiredFieldValidator ControlToValidate="textName" runat="server" ValidationGroup="memberSignUp" CssClass="validator" ID="RequiredFieldValidator3" ErrorMessage="" />
                                            <asp:TextBox ID="textName" runat="server" class="su-name" placeholder=""></asp:TextBox>
                                        </div>
                                </div>                                
							</div>
                        </div>
                    <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="single-register">
                                        <label>Position and Affiliation (please enclose your curriculum vitae): </label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
									<div class="single-register">                                            
											<asp:TextBox ID="textNameL" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                    <div class="col-lg-12">                            
                        <div class="row">
                                <div class="col-lg-6">
                                    <div class="single-register">
                                        <label>Full Mailing addresses:<span>*</span></label>
                                    </div>
                                </div>
								<div class="col-lg-6">
									<div class="single-register">										
										<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="#cc3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                            CssClass="validator" ValidationGroup="memberSignUp" ControlToValidate="textEmail" runat="server" ErrorMessage="Invalid Email ID"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="textEmail" CssClass="validator"  ValidationGroup="memberSignUp" ErrorMessage=""></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="textEmail" runat="server" autocomplete="off" CssClass="su-em" placeholder="Email ID"></asp:TextBox>
									</div>
								</div>
							</div>
                    </div>
                    <div class="col-lg-12">
                        <div  class="row">
                                <div class="col-lg-6">
                                    <div class="single-register">
                                        <label>Telephone:<span>*</span></label>
                                    </div>                                    
                                </div>
                            <div  style="display:none;">									
                                    <div class="single-register">
                                        <span class="su-dd">
                                            <asp:DropDownList ID="dd_country_code" CssClass="chosen-select su-cc" data-toggle="tooltip" title="Select Country Code"  runat="server"><asp:ListItem Selected="True">+91</asp:ListItem> </asp:DropDownList>
                                        </span>                                        
									</div>
								</div>
                                 <div class="col-lg-6">									
                                    <div class="single-register">                                    
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="validator" ValidationGroup="memberSignUp" runat="server" ValidationExpression="[0-9]{10}" ControlToValidate="textPhone" ErrorMessage="Invalid Mobile No"></asp:RegularExpressionValidator> 
                                        <asp:RequiredFieldValidator ControlToValidate="textPhone" runat="server" ValidationGroup="memberSignUp" CssClass="validator" ID="RequiredFieldValidator1" ErrorMessage="" />
                                        <asp:TextBox ID="textPhone" runat="server" autocomplete="off" CssClass="su-ph" Width="84%"></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>                            
                    
                            <div class="col-lg-12">
						        <div class="login-title text-left">
							        <h6 style="text-decoration:underline;">SUBJECT MATTER</h6>
                                    <br />                            
						        </div>
					        </div>
                    <div class="col-lg-12">
                          <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>Title of the Book:</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtTitleofbook" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                          <div class="col-lg-12">
                              <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>Definition of topic covered in the book:</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtdefoftopic" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                          </div>
                            
                    <div class="col-lg-12">
                        <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>Write a short description of your book giving an overview of the contents:</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtshotdescription" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                            
                    <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>Outline your reasons for proposing a new book in this area:</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtoutlinereason" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                    <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>Describe the major features of your book which make it unique:</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtdescmajorfeature" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                    <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>Please give complete table of contents, including section and sub-section headings:</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtTablecompletesection" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                    <div class="col-lg-12">
						<div class="login-title text-left">
							<h6 style="text-decoration:underline;">MANUSCRIPT INFORMATION</h6>
                            <br />                            
						</div>
					</div>
                    <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>Approximately how many pages, would you expect your book to contain? About pages, in size:</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtaboutpagesinsize" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                    <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>How long do you estimate it will take for delivery of the completed manuscript from the time of acceptance of proposal?:</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtcompletemenuscript" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                    <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>Please list any special physical features you would expect to include (tables, illustrations problems/solutions, photographs, etc.):</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtspecialphysical" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                    <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <div class="single-register">
                                        <label>Please list your previous works:</label>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									<div class="single-register">                                            
											<asp:TextBox ID="txtyourwork" runat="server" class="su-name" placeholder=""></asp:TextBox>
									</div>
								</div>
                            </div>
                    </div>
                     <div class="col-lg-12">
						<div class="login-title text-center">
							<asp:Button ID="btnSignUp" ValidationGroup="memberSignUp"  CssClass="su-sb" OnClick="btnSignUp_Click" runat="server" Text="Submit Details" />                         
						</div>
					</div>
                    <%--<div class="col-lg-12"></div>--%>
                            <asp:UpdatePanel ID="AddressUpdatePanel" runat="server" >
                                <ContentTemplate>
                                    <div class="single-register" style="display:none;">
								        <label>Country <span>*</span></label>
                                        <asp:DropDownList ID="dd_Country" AutoPostBack="true" OnSelectedIndexChanged="dd_Country_SelectedIndexChanged"
                                            CssClass="chosen-select su-co" runat="server">
                                            <asp:ListItem Value="Nil">Select Country</asp:ListItem>
                                        </asp:DropDownList>
							        </div>
                                    <div class="single-register" style="display:none;">
								        <label>State <span>*</span></label>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="dd_State" ValidationGroup="memberSignUp" 
                                            CssClass="validator" ValueToCompare="Nil" Operator="NotEqual" ErrorMessage=""></asp:CompareValidator>
                                        <asp:DropDownList ID="dd_State"
                                            AutoPostBack="true" OnSelectedIndexChanged="dd_State_SelectedIndexChanged"
                                            CssClass="chosen-select su-st" runat="server">
                                            <asp:ListItem Value="Nil">Select State</asp:ListItem>
                                        </asp:DropDownList>
							        </div>
                                    <div class="single-register" style="display:none;">
                                        <label>City <span>*</span></label>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="dd_City" ValidationGroup="memberSignUp" 
                                            CssClass="validator" ValueToCompare="Nil" Operator="NotEqual" ErrorMessage=""></asp:CompareValidator>
                                        <asp:CustomValidator ID="CustomValidator5" ClientValidationFunction="ClientValidate" ControlToValidate="dd_City" runat="server" Display="Dynamic">
                                            <i class="fa fa-warning" data-toggle="tooltip" title="Please Select City"></i>&nbsp;Please Select City
                                        </asp:CustomValidator>
                                        <asp:DropDownList ID="dd_City" CssClass="chosen-select su-ci" runat="server">
                                            <asp:ListItem Value="Nil">Select City</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSignUp" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="single-register" style="display:none;">
							    <label>PIN Code <span>*</span><i class="fa fa-info-circle" data-toggle="tooltip" title="Provide PIN Code"></i></label>
								<asp:RequiredFieldValidator ControlToValidate="textPinCode" CssClass="validator"
                                    runat="server" ValidationGroup="memberSignUp" ID="RequiredFieldValidator7" ErrorMessage=""></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidator4" ClientValidationFunction="ClientValidate" ControlToValidate="textPinCode" runat="server" Display="Dynamic">
                                    <i class="fa fa-warning" data-toggle="tooltip" title="Enter PinCode"></i>&nbsp;Enter PinCode
                                </asp:CustomValidator>
                                <asp:TextBox ID="textPinCode" runat="server" CssClass="su-pc" placeholder="PinCode"></asp:TextBox>
							</div>
                            <div class="single-register" style="display:none;">
								<label>
                                    Street Name <span>*</span> 
                                    <i class="fa fa-info-circle" data-toggle="tooltip" title="Provide Address Street No and Nearest Landmark for easier and fast delivery"></i>
								</label>
								<asp:RequiredFieldValidator ControlToValidate="textStreet" CssClass="validator"
                                    runat="server" ValidationGroup="memberSignUp" ID="RequiredFieldValidator8" ErrorMessage=""></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidator2" ClientValidationFunction="ClientValidate" ControlToValidate="textStreet" runat="server" Display="Dynamic">
                                    <i class="fa fa-warning" data-toggle="tooltip" title="Enter Street Name"></i>&nbsp;Enter Street Name
                                </asp:CustomValidator>
                                <asp:TextBox ID="textStreet" runat="server"  CssClass="su-st"></asp:TextBox>
							</div>
                            <div class="single-register" style="display:none;">
							    <label>Building / Plot / House <span>*</span><i class="fa fa-info-circle" data-toggle="tooltip" title="Provide Address Plot No."></i></label>
                                <asp:CustomValidator ID="CustomValidator1" ClientValidationFunction="ClientValidate" ControlToValidate="textPlot" runat="server" Display="Dynamic">
                                    <i class="fa fa-warning" data-toggle="tooltip" title="Enter Plot No"></i>&nbsp;Enter Plot No.
                                </asp:CustomValidator>
							    <asp:RequiredFieldValidator ControlToValidate="textPlot" CssClass="validator"
                                    runat="server" ValidationGroup="memberSignUp" ID="RequiredFieldValidator4" ErrorMessage="" />
                                <asp:TextBox ID="textPlot" runat="server" CssClass="su-bp" ></asp:TextBox>
							</div>
                            <div class="single-register" style="display:none;">
							    <label>Address <i class="fa fa-info-circle" data-toggle="tooltip" title="Provide Detailed Address along with Building No, Street and Nearest Landmark for easier and fast delivery."></i></label>
							    <asp:RequiredFieldValidator ControlToValidate="textPlot" CssClass="validator"
                                    runat="server" ValidationGroup="memberSignUp" ID="RequiredFieldValidator9" ErrorMessage="" />
                                <asp:CustomValidator ID="CustomValidator3" ClientValidationFunction="ClientValidate" ControlToValidate="textPlot" runat="server" Display="Dynamic">
                                    <i class="fa fa-warning" data-toggle="tooltip" title="Enter Plot No"></i>&nbsp;Enter Plot No.
                                </asp:CustomValidator>
                                <asp:TextBox ID="textAddress" runat="server" CssClass="su-bp" ></asp:TextBox>
							</div>
                            <div class="single-register" style="display:none;">
                                <label>Password <span>*</span></label>
                                <asp:RequiredFieldValidator ControlToValidate="textPassword" CssClass="validator"
                                    runat="server" ValidationGroup="memberSignUp" ID="RequiredFieldValidator5" ErrorMessage=""></asp:RequiredFieldValidator>
                                <p class="password-strength" id="password_strength"></p>
                                <asp:TextBox ID="textPassword" runat="server" onkeyup="CheckPasswordStrength(this.value);" TextMode="Password" 
                                             data-toggle="tooltip" data-title="Min 8 characters, Must contain an Uppercase(A-Z), a Lowercase(a-z) and a Numeric(0-9) character."
                                             CssClass = "su-pw" placeholder="Password"></asp:TextBox>
                            </div>
                            <div class="single-register" style="display:none;">
                                <label>Confirm Password</label>
                                <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="textConfirmPassword" CssClass="validator"
                                    ControlToCompare="textPassword" ErrorMessage="Password Did not Match" ValidationGroup="memberSignUp"></asp:CompareValidator>  
                                <asp:RequiredFieldValidator ControlToValidate="textConfirmPassword" runat="server" CssClass="validator"
                                    ValidationGroup="memberSignUp" ID="RequiredFieldValidator6" ErrorMessage=""></asp:RequiredFieldValidator>
                                <asp:TextBox ID="textConfirmPassword" runat="server" TextMode="Password"
                                    CssClass="su-cpw" placeholder="Confirm Password"></asp:TextBox>
                            </div>
							<div class="single-register single-register-3" style="display:none;">
								<input id="rememberme" type="checkbox" name="rememberme" value="forever"/>
								<label class="inline">
                                    I agree 
                                    <a href="https://skkatariaandsons.com/topics.aspx?topicid=17">
                                        Terms & Condition
                                    </a>
								</label>
							</div> 
                </asp:Panel>
            </div>
        </div>
    
    <script type="text/javascript">
        function CheckPasswordStrength(password) {
            var textbox = document.getElementById('<%=textPassword.ClientID %>');
            //var password_strength = document.getElementById("password_strength");
            var submitbutton = document.getElementById('<%=btnSignUp.ClientID %>');
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
                    strength = "Good";
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

