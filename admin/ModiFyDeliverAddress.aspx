<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true"
    CodeFile="ModiFyDeliverAddress.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .pay-option-img {
            width: 50px;
            height: auto;
            display: inline;
        }

        .border-right {
            border-right: 1px solid;
        }
        .btn-xs, .btn-group-xs > .btn{
            font-size:20px;
        }

        #myModal .modal-dialog{
            width:850px;
        }

        /*blink {
        -webkit-animation: 2s linear infinite condemned_blink_effect; 
     for android animation: 2s linear infinite condemned_blink_effect;
        }
        */
        .blinking{
                    animation:blinkingText 2.0s infinite;
                    font-size: x-large;
                    text-decoration: solid;
                }
        @keyframes blinkingText{
            0%{     color: #000;    }
            49%{    color: transparent; }
            50%{    color: transparent; }
            99%{    color:transparent;  }
            100%{   color: #000;    }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="kode-inner-banner" style="display:flex;">
        <div class="kode-page-heading">
            <h2>Customers Shipping Address</h2>
        </div>
        <div class="kode-page-heading" style="margin-left: 420px;">
            <h2><a href="view_users.aspx"><i class="fa fa-backward"></i>&nbsp;&nbsp;Back To Customers</a></h2>
        </div>
    </div>

    <div class="kode-content padding-tb-50">
        <div class="container">
            <div class="row"> 
                <asp:HiddenField ID="hf_cartID" runat="server" Value="0" />
                <asp:HiddenField ID="hf_totalitems" runat="server" />
                <asp:HiddenField ID="hf_productinfo" runat="server" />
                <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
                <div class="col-lg-4 col-md-4 " style="border: 1px solid #c57f7e; border-right: 0;">
                    <h4><i class="fa fa-bullseye"></i>&nbsp;Delivery Details</h4>
                    <asp:Literal ID="ltr_ship_alert_msg" runat="server"></asp:Literal>
                    <asp:Repeater ID="rp_ship_addresses" runat="server" OnItemCommand="rp_ship_addresses_ItemCommand">
                        <ItemTemplate>
                            <div class="card card-outline-<%# (Eval("isDefault").ToString().ToUpper()=="TRUE"?"warning":"default") %>" id="div_ship_add<%# Container.ItemIndex+1 %>">
                                <div class="card-block">
                                    <%--<asp:RadioButton ID="rb_ship_here" Checked='<%# Eval("isDefault") %>' runat="server" Text="Ship here"  />--%>
                                    <asp:HiddenField ID="hf_addressID" runat="server" Value='<%# Eval("AddressID") %>' />
                                    <asp:HiddenField ID="hf_ship_address" runat="server" Value='<%# Eval("ShipAddress") %>' />
                                    <asp:HiddenField ID="hf_ship_cityid" runat="server" Value='<%# Eval("CityID") %>' />
                                    <asp:HiddenField ID="hf_ship_postalcode" runat="server" Value='<%# Eval("ShipPostalCode") %>' />
                                    <asp:HiddenField ID="hf_ship_email" runat="server" Value='<%# Eval("EmailID") %>' />
                                    <asp:HiddenField ID="hf_ship_phone" runat="server" Value='<%# Eval("Mobile") %>' />
                                    <h6 class="card-subtitle text-muted"><%# (Eval("isDefault").ToString().ToUpper()=="TRUE"?"Default":"") %></h6>
                                    <p class="card-text">
                                        <%# Eval("ShipAddress") %>
                                    </p>
                                    <%# Eval("CityName") %>, <%# Eval("StateName") %>, <%# Eval("CountryName") %> <br />
                                    <%--ZIP: <%# Eval("ShipPostalCode") %><br />--%>
                                    <span>Phone </span>: <b><%# Eval("Mobile") %></b><br />
                                    <span>EmailID </span>: <b><%# Eval("EmailID") %></b><br />
                                    <br />
                                    <asp:LinkButton ID="lb_delete_ship_address" OnClientClick='javascript:return confirm("Do you really want to Change this Address ?")'
                                        class="btn btn-toolbar btn-primary" CommandName="Change_ship_address" CommandArgument='<%# Eval("AddressID") %>' runat="server">
                                        <i class="fa fa-edit"></i>&nbsp;Change Address
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

<%--                    <a class="btn btn-xs btn-info " data-toggle="modal" data-target="#myModal"><i class="fa fa-plus"></i>&nbsp;Add New Shipping Address</a>
                    <br />
                    <asp:CheckBox ID="cb_copy_bill_to_ship" runat="server" Text="or Same as Billing Address" />
                    <br />--%>
                </div>

                <div class="col-md-6" style="border: 1px solid #5cb85c;padding-bottom: 10px;margin-bottom: 70px;" >
                    <script>
                        function pageLoad(sender, args) {
                            if (args.get_isPartialLoad()) {
                                //Specific code for partial postbacks can go in here.
                                $(document).ready(function () {
                                    $('[data-toggle="tooltip"]').tooltip();


                                    $("#label_rb_onpay").click(function () {
                                        $("#div_online_payment").show(500, 'swing');
                                        $("#div_cod").hide(500, 'swing');
                                        $("#<%=btn_place_order.ClientID%>").val("Pay & Place Order");
                                    });


                                    <%--$("#label_rb_cod").click(function () {
                                        $("#div_online_payment").hide(500, 'swing');
                                        $("#div_cod").show(500, 'swing');
                                        $("#<%=btn_place_order.ClientID%>").val("Place order");
                                    });--%>
                                });
                                //$.getScript("js/tooltip.js", function () {
                                //    //alert("Script loaded and executed.");
                                //});
                            }
                        }

                        function userValid() {
                            // var Description = document.getElementById("<%= bill_textPhone.ClientID %>");
                            var Name = $('#ContentPlaceHolder1_bill_textName').val();
                            var StudentName = $('#ContentPlaceHolder1_bill_textStudent').val();
                            var Address = $('#ContentPlaceHolder1_bill_textAdress').val();
                            var zipcode = $('#ContentPlaceHolder1_bill_textZipCode').val();
                            var Email = $('#ContentPlaceHolder1_bill_textEmailID').val();
                            var Phone = $('#ContentPlaceHolder1_bill_textPhone').val();
       
                            if (Name == "")
                            {
                                    alert("Please Enter Data In Name ");
                                    $('#ContentPlaceHolder1_bill_textName').focus();
                                    return false;
                            }
                            //else if (StudentName == "") {
                            //    alert("Please Enter Data In Student Field");
                            //    $('#ContentPlaceHolder1_bill_textStudent').focus();
                            //    return false;
                            //}
                            else if (Address == "") {
                                alert("Please Enter Data In Address Field");
                                $('#ContentPlaceHolder1_bill_textAdress').focus();
                                return false;
                            }
                            //else if (zipcode == "") {
                            //    alert("Please Enter Valid ZipCode");
                            //    $('#ContentPlaceHolder1_bill_textZipCode').focus();
                            //    return false;
                            //}
                            else if (Email == "") {
                                alert("Please Enter Valid Email Id");
                                $('#ContentPlaceHolder1_bill_textEmailID').focus();
                                return false;
                            }
                            else if (Phone == "") {
                                alert("Please Enter Valid Mobile No");
                                $('#ContentPlaceHolder1_bill_textPhone').focus();
                                return false;
                            }
                            else
                            {
                                return true;
                            }

                            }
                    </script>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div style="position: absolute; top: 1%; height: 97%; width: 94%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center;">
                                <div style="position: absolute; width: 100%; top: 50%;">
                                    <img src="/img/ring-alt.svg" />
                                </div>
                            </div>

                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:Literal ID="ltr_bill_msg" runat="server"></asp:Literal>
                            <div class="billing-fields">
                                <label class="label label-primary pull-right"
                                    data-toggle="tooltip" title="You have to add Full Details accordingly">
                                    <i class="fa fa-info"></i>
                                </label>
                                <h4><i class="fa fa-map-marker"></i>&nbsp;Delivery Address by Full Name
                                </h4>
                                <div class="init-validator">
                                    <asp:HiddenField ID="hf_ShipaddressID" runat="server" />
                                    <label>Customer Name *</label>
                                    <asp:TextBox ID="bill_textName" runat="server" class="form-control" ReadOnly="true" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="place_order"
                                        CssClass="validator" ControlToValidate="bill_textName"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                </div>
                                <div class="init-validator" style="display:none;">
                                    <label>Student Name *</label>
                                    <asp:TextBox ID="bill_textStudent" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="place_order"
                                        CssClass="validator" ControlToValidate="bill_textStudent"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                </div>
                                <div class="clearfix"></div>
                                <div class="init-validator">
                                    <label>Address *</label>
                                    <asp:TextBox ID="bill_textAdress" runat="server" Height="80" placeholder="Full address" class="form-control textarea-vertical" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="place_order"
                                        CssClass="validator" ControlToValidate="bill_textAdress"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                </div>
                                <p>
                                    <label>Country *</label>
                                    <asp:DropDownList ID="bill_dd_Country"
                                        AutoPostBack="true" OnSelectedIndexChanged="bill_dd_Country_SelectedIndexChanged"
                                        class="form-control" runat="server">
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    <label>State *</label>

                                    <asp:DropDownList ID="bill_dd_State"
                                        AutoPostBack="true" OnSelectedIndexChanged="bill_dd_State_SelectedIndexChanged"
                                        class="form-control" runat="server">
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    <label>Town / City *</label>
                                    <asp:DropDownList ID="bill_dd_City" class="form-control" runat="server"></asp:DropDownList>
                                </p>

                                <div class="init-validator">
                                    <label>PinCode *</label>
                                     <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="bill_textZipCode" CssClass="validator" 
                                    ValidationGroup="place_order" > <i class="fa fa-warning"></i>&nbsp;Required </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" CssClass="validator" ValidationGroup="place_order" runat="server" 
                                         ValidationExpression="[0-9]{4,6}" ControlToValidate="bill_textZipCode" ErrorMessage="Invalid Zip Code required(6 digits zip code)"> </asp:RegularExpressionValidator>--%>
                                    
                                    <asp:TextBox ID="bill_textZipCode" runat="server" placeholder="PinCode" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="init-validator">                                
                                    <label>Email Address*</label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="#cc3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        CssClass="validator" ValidationGroup="place_order" ControlToValidate="bill_textEmailID" runat="server" ErrorMessage="Invalid Email ID"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="bill_textEmailID"    class="form-control" runat="server" placeholder="Email ID"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="bill_textEmailID" CssClass="validator" 
                                    ValidationGroup="place_order"> <i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>                                  
                                    </div>
                                <div class="init-validator">
                                    <label>Mobile No *</label>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="validator" ValidationGroup="place_order" runat="server" 
                                         ValidationExpression="[0-9]{10}" ControlToValidate="bill_textPhone" ErrorMessage="Invalid Mobile No"> </asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="bill_textPhone" CssClass="validator" 
                                    ValidationGroup="place_order" > <i class="fa fa-warning"></i>&nbsp;Required </asp:RequiredFieldValidator>                                         
                                    <asp:TextBox ID="bill_textPhone" class="form-control" runat="server"
                                        title="Please Enter Correct Mobile No without including 0, +91 or space of starting of Mobile no."
                                        pattern="[1-9]{1}[0-9]{9}" 
                                        ></asp:TextBox>
                                    <%--<asp:TextBox ID="bill_textPhone" style="display:none;" class="Mobile" runat="server"></asp:TextBox>--%>
                                </div>   
                                <div class ="col-lg-12" style="text-align:center;padding-top:10px;padding-bottom:10px;">
                                    <asp:Button ID="btn_place_order" class="btn btn-lg- btn-success"  OnClick ="btn_place_order_Click" ValidationGroup="place_order"
                                         OnClientClick="return userValid();"  Enabled="true" runat="server" Text="Save" />
                                </div>
                                
                                <br />
                                <br />
                                <div class="row">
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                    <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_place_order" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>

                <div class="col-md-2 pull-right" id="dv_courier" runat="server" style="display:none;">
                    <ul  class="list-group" style="margin-left: 0">
                        <li class="list-group-item">
                            <span>Select Courier Name</span> 
                            <asp:DropDownList ID="drp_couriercompany" AutoPostBack="true" OnSelectedIndexChanged="drp_couriercompany_SelectedIndexChanged" title="Select Courier Name" class="form-control" runat="server"> 
                                <asp:ListItem Value="Nil">Select Courier Name</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                    </ul>
                    <div class="col-lg-12 col-md-12 col-sm-12" >
                        
                    </div>
                </div>

                <div class="col-md-3 pull-right" style="display:none;">
                    <%--<h4><i class="fa fa-shopping-cart"></i>&nbsp;Cart Summary</h4>--%> 
                    <ul class="list-group" style="margin-left: 0">
                        <asp:Repeater ID="rp_cart" runat="server">
                            <ItemTemplate>
                                <li class="list-group-item ">
                                    <span><%# Eval("BookName") %></span><br />
                                    <br />
                                    <div>
                                        <small><%# Eval("Qty") %> X <em><%# Eval("SaleCurrency") %>&nbsp;<%# Eval("Price",CommonCode.AmountFormat()) %></em><span class="text-muted"><%# string.IsNullOrEmpty(Eval("DiscountPer").ToString())?"":"(-"+Eval("DiscountPer")+"%)" %></span></small><b class="pull-right" style="margin-left: 15px"><%# Eval("SaleCurrency") %>&nbsp;<%#Eval("TotalAmt",CommonCode.AmountFormat()) %></b></div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li class="list-group-item">
                            <div>
                                <span>Set Price</span>
                                <b class="pull-right" id="Item_Amt" runat="server"></b>
                            </div>
                        </li>
                        <li class="list-group-item" id="li_shipping" runat="server" >
                            <div>
                                <span>Shipping</span>
                                <b class="pull-right" id="shipping_amt" runat="server"></b>
                            </div>
                        </li>
                        <li class="list-group-item" id="li_handling" runat="server" >
                            <div>
                                <span>Internet Handling charges</span>
                                <b class="pull-right" id="InternetHandling" runat="server"></b>
                            </div>
                        </li>
                        <li class="list-group-item" id="li_roundoff" runat="server" >
                            <div>
                                <span>Round off</span>
                                <b class="pull-right" id="roundoff" runat="server"></b>
                            </div>
                        </li>
                        <li class="list-group-item list-group-item-success">
                            <div>
                                <em>You Pay</em>
                                <asp:HiddenField ID="hf_total_amount" runat="server" />
                                <b class="pull-right" id="b_total_amount" runat="server"></b>
                            </div>
                        </li>
                    </ul>
                </div>
                
                <div class="col-md-3 pull-right" style="display:none;">
                    <br />
                <br />
                    <ul class="list-group" id="ul_impinfo" runat="server">
                        <li class="list-group-item list-group-item-success">
                            <div>
                                <span class="blinking">Important Information!!</span>
                            </div>
                        </li>
                        <li class="list-group-item ">                            
                            <div style="color: darkmagenta;">
                                <asp:Label ID="lblcheckOutNotification" runat="server"></asp:Label>
                            </div>
                            
                        </li>
                    </ul>
                </div>

            </div>
        </div>
    </div>


    <!-- Modal -->
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><i class="fa fa-plus"></i>&nbsp;Add New Shipping Address</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <div style="position: absolute; top: 1%; height: 97%; width: 94%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center;">
                                <div style="position: absolute; width: 100%; top: 50%;">
                                    <img src="/img/ring-alt.svg" />
                                </div>
                            </div>

                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" CssClass="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="email">Address:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:TextBox ID="text_new_shippingAddress"  TextMode="MultiLine" Style="resize: vertical"  class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validator" ControlToValidate="text_new_shippingAddress" ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="email">Country:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:DropDownList ID="dd_new_ship_country"  AutoPostBack="true" OnSelectedIndexChanged="dd_new_ship_country_SelectedIndexChanged" readonly="true" Enabled="false"  class="form-control" runat="server"
                                            style="display: block; width: 100%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; border-radius: 0; -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); -webkit-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;"                                            
                                            title ="you Cann't Change Country"> </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validator" ControlToValidate="dd_new_ship_country" ValueToCompare="Nil" Operator="NotEqual" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:CompareValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="email">State:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:DropDownList ID="dd_new_ship_state" AutoPostBack="true" OnSelectedIndexChanged="dd_new_ship_state_SelectedIndexChanged" Enabled="false" class="form-control" runat="server"
                                            style="display: block; width: 100%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; border-radius: 0; -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); -webkit-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;"
                                          title ="you Cann't Change State"  > </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="validator" ControlToValidate="dd_new_ship_state" ValueToCompare="Nil" Operator="NotEqual" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="email">Town / City:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:DropDownList ID="dd_new_ship_city" class="form-control" runat="server" Enabled="false" 
                                            style="display: block; width: 100%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; border-radius: 0; -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); -webkit-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;"
                                          title ="you Cann't Change City"  ></asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" CssClass="validator" ControlToValidate="dd_new_ship_city" ValueToCompare="Nil" Operator="NotEqual" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:CompareValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="pwd">Postal Code:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:TextBox ID="text_new_ship_PostalCode" class="form-control" runat="server" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="validator" ControlToValidate="text_new_ship_PostalCode" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="pwd">Email ID:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:TextBox ID="text_new_ship_EmailID"
                                            class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            CssClass="validator" ControlToValidate="text_new_ship_EmailID" ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="pwd">Mobile:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:TextBox ID="text_new_ship_Mobile"
                                            class="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            CssClass="validator" ControlToValidate="text_new_ship_Mobile" ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-offset-2 col-sm-10">
                                        <asp:CheckBox ID="cb_make_default" Style="padding: 0" class="checkbox" Text="Make Default" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-2 col-sm-10">
                                        <asp:Button ID="btn_Save_new_shipping_address" runat="server" ValidationGroup="save_new_shipping_address"
                                            OnClick="btn_Save_new_shipping_address_Click" Text="Save" class="btn btn-success" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
     <asp:PlaceHolder ID="ph_scripts" runat="server"></asp:PlaceHolder>

    <script>
        $("[name$='$rb_ship_here']").attr("name", $("[name$='$rb_ship_here']").attr("name"));

        $("[name$='$rb_ship_here]").click(function () {
            //set name for all to name of clicked 
            $("[name$='$rb_ship_here]").attr("name", this.attr("name"));
            $("[name$='$rb_ship_here]").attr("checked", "checked");
        });

        $("#label_rb_onpay").click(function () {
            $("#div_online_payment").show(500, 'swing');
            $("#div_cod").hide(500, 'swing');
            $("#<%=btn_place_order.ClientID%>").val("Pay & Place Order");

            if ($(".Mobile", row).html() == "") {
                alert("Please Enter Data In Mandatory Fields");
                return false;
            }
        });

        $("#label_rb_cod").click(function () {
            $("#div_online_payment").hide(500, 'swing');
            $("#div_cod").show(500, 'swing');
            $("#<%=btn_place_order.ClientID%>").val("Place order");
        });

    </script>
</asp:Content>

