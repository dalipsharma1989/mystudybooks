<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true"
    CodeFile="proceed_to_checkout.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
     <script type="text/javascript"> 
         function updateText(text) {
             var ctrl = document.getElementsByClassName("pull-rightsingle")[0];
             if (ctrl == null || ctrl == undefined)
             { }
             else
             {
                 ctrl.innerHTML = text;
             }
         }; 
    </script>
    <style type="text/css" >  
        #TableOrder{
            font-size:18px !important;
            font-weight:bold!important;
        } 
        #TableOrder tbody td{
        font-size:15px !important;
        /*font-weight:bold!important;*/       
        }
         .your-order-table table tr.order-total td span,.order-total > th{
        font-size:22px !important;        
        }
                
        #TableOrder tfoot th{        
        }
         #TableOrder tfoot td{
        font-size:18px !important;
        }
        #ContentPlaceHolder1_rbt_CCAvenue {
            height: 20px !important;
            width: 20px !important;
        }
               
         #ContentPlaceHolder1_rbt_paypal{
             height: 40px !important;
             width: 40px !important;
         }
          #ContentPlaceHolder1_rb_ship_method_1 {
            height: 40px !important;
            width: 40px !important;
        }
          #ContentPlaceHolder1_rb_ship_method_0 {
            height: 40px !important;
            width: 40px !important;
        }
          #TableOrder tbody td label{
         color: white !important;    
        }
       #ContentPlaceHolder1_rb_ship_method_1 label{
            color: white !important;          
        }
          #ContentPlaceHolder1_rb_ship_method_0 label{            
            color: white !important;
        }

        .billing-details-container {
        padding-bottom:20px;
        } 
        .card{
                margin-top: 25px;
                padding: 20px;
        }

        .card-text{
               font-weight: 600;
            font-size: 17px;
            padding-top: 6px;
            color:#96c943;
        }
        .card-block{
                font-weight: 600;
                font-size: 15px;
        }
        .shipping-ph{
            border:4px double lightgrey !important;
            background:#d3d3d31f !important;
  
        }
        .save-button:hover{
                box-shadow: -4px 3px 0px 0px lightgrey !important;
        }
        .checkbox label{
            padding-left:0px !important;
        }
        .checkbox-form{
            padding: 10px 0px 45px;
        }
        @media only screen and (max-width:580px) {
            .mobilePadddingMargin{
                padding-left: 10px;
                padding-right: 10px;
                margin-bottom: 10px;
            }
        }
        

    </style>
    <script type="text/javascript">
             function updateTotalAmount(amnt) {
                 var ctrl1 = document.getElementsByClassName("pull-rightsingle")[1];
                 if (ctrl1 == null || ctrl1 == undefined)
                 { }
                 else
                 {
                     ctrl1.innerHTML = amnt;
                 }
             }; 
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area"> 
				<div class="row">
					<div class="col-lg-12">
						<div class="breadcrumbs-menu">
							<ul>
								<li><a href="/">Home</a></li>
								<li><a href="/Customer/user_cart">Cart</a></li>
								<li><a href="#" class="active">Checkout</a></li>
							</ul>
						</div>
					</div>
				</div> 
		</div>
		<!-- breadcrumbs-area-end -->
	<%--	<!-- entry-header-area-start -->
		<div class="entry-header-area"  >
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<div class="entry-header-title checkbox-form">
							<%--<h2>Checkout</h2>
                            
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- entry-header-area-end -->--%>
      
        <!-- checkout-area-start -->
        <div class="checkout-area mb-70">
			<div class="">
				<div class="row">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UD1">
                        <ProgressTemplate>
                            <div style="position: absolute; top: 1%; height: 100%; width: 100%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center;z-index:99999;">
                                <div style="position: absolute; width: 100%; height: 50%; top: 50%;">
                                    <img src="/img/ring-alt.svg" />
                                </div>
                            </div> 
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UD1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:HiddenField ID="hf_cartID" runat="server" />
                            <asp:HiddenField ID="hf_totalitems" runat="server" />
                            <asp:HiddenField ID="hf_productinfo" runat="server" />
                            <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
					        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="checkoutBillDet">
						        <div class="checkbox-form" id="checBillDetail">						
							        <%--<h3>Billing Details</h3>--%>
                                    <asp:Literal ID="ltr_bill_msg" runat="server"></asp:Literal>                            
							                <div class="row billing-details-container boxshadow">								                
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
									                <div class="checkout-form-list">
										               <h3>Billing Details</h3>
									                </div>
								                </div>
                                                  <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
									                <div class="checkout-form-list">
										                <label>Name <span class="required">*</span></label>										
										                <asp:TextBox CssClass="form-control" ID="bill_textName" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="place_order"
                                                                                    CssClass="validator" ControlToValidate="bill_textName">
                                                            <i class="fa fa-warning"></i>&nbsp;Required
                                                        </asp:RequiredFieldValidator>
									                </div>
								                </div>
								                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
									                <div class="checkout-form-list">
										                <label>Address <span class="required">*</span></label>
										                <asp:TextBox CssClass="form-control" ID="bill_textAdress" runat="server" Height="95"
                                                                        placeholder="Full address" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="place_order"
                                                                                    CssClass="validator" ControlToValidate="bill_textAdress">
                                                            <i class="fa fa-warning"></i>&nbsp;Required
                                                        </asp:RequiredFieldValidator>
									                </div>
								                </div>
                                                <div class=" col-lg-12" id="dv_Country" runat="server">
                                                    <div class="country-select">
										                <label>Country <span class="required">*</span></label>
										                <asp:DropDownList ID="bill_dd_Country" AutoPostBack="true"  OnSelectedIndexChanged="bill_dd_Country_SelectedIndexChanged"
                                                            runat="server"> </asp:DropDownList>
                                                        </div>
									                </div>
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="dv_State" runat="server">
									                <div class="country-select">
										                <label>State<span class="required">*</span></label>										
										            <asp:DropDownList ID="bill_dd_State" AutoPostBack="true"  OnSelectedIndexChanged="bill_dd_State_SelectedIndexChanged" runat="server"></asp:DropDownList>
									                </div>
								                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" >
									                <div class="country-select" id="dv_City" runat="server">
										                <label>City <span class="required">*</span></label>										
										                <asp:DropDownList ID="bill_dd_City" AutoPostBack="true" OnSelectedIndexChanged="bill_dd_City_SelectedIndexChanged"  runat="server"></asp:DropDownList>
                                                    </div>
								                </div>
								                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									                <div class="checkout-form-list">
										                <label>Postal/Zip code <span class="required">*</span></label>										
										                <asp:TextBox CssClass="form-control" ID="bill_textZipCode" runat="server" pattern="[1-9]{1}[0-9]{5}" 
                                                            title="Please Enter 6 Digits correct Pin Code Not Start with 0, Postal Code Like (124001)" placeholder="Pin/Zip Code"
                                                            ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="place_order"
                                                                                    CssClass="validator" ControlToValidate="bill_textZipCode">
                                                            <i class="fa fa-warning"></i>&nbsp;Postal/Zip code Required
                                                        </asp:RequiredFieldValidator>
                                                    </div>
								                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									                <div style="white-space:nowrap" class="checkout-form-list">
										                <label>Email Address <span class="required">*</span></label>										
										                <asp:TextBox ReadOnly="true" CssClass="form-control" ID="bill_textEmailID" runat ="server"></asp:TextBox>
									                </div>
								                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
									                <div class="checkout-form-list">
										                <label>Phone  <span class="required">*</span></label>										
											                <asp:TextBox ReadOnly="true" CssClass="form-control" ID="bill_textPhone" title="Please enter valid Mobile No." pattern="^[0-9]{1,10}$" runat="server"></asp:TextBox>
									                </div>
								                </div>								
							                </div>

                                          <div class="different-address different-shipping col-md-12 col-sm-12 col-lg-12 col-xs-12">
								                <div class="ship-different-title ship-title col-md-6 col-lg-6 col-sm-6">
                                                     <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 shipping-address">
                                                         <span> 
                                                             <label>Ship to a different address?</label>
                                                         </span>
                                                     </div>
									               <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 billing-address">
										           <span>
                                                          <asp:CheckBox ID="cb_copy_bill_to_ship" runat="server" Text=" or Same as Billing Address" />
                                                    </span>
                                                  </div>
									               
								                </div> 	
                                              <div class="col-md-6 col-lg-6 col-sm-6">
                                                  <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                                          <a class="btn btn-xs btn-primary btn-info add-shipping" data-toggle="modal" data-target="#myModal" 
                                                            style="color:#fff"   >
                                                                <i class="fa fa-plus"></i>&nbsp;Add New Shipping Address</a>
                                                  </div>
                                              </div>
									        </div> 
                                                                        <!-- // ********************************************************************************  --> 
                                    <asp:Literal ID="ltr_ship_alert_msg" runat="server"></asp:Literal>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="uD2">
                                            <ProgressTemplate>
                                                <div style="position: absolute; top: 1%; height: 100%; width: 100%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center;z-index:99999;">
                                                    <div style="position: absolute; width: 100%; height: 50%; top: 50%;">
                                                        <img src="/img/ring-alt.svg" />
                                                    </div>
                                                </div> 
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdatePanel ID="uD2" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="rp_ship_addresses" runat="server" OnItemCommand="rp_ship_addresses_ItemCommand" >
                                                    <ItemTemplate>
                                                        <div class="card ship-cards boxshadow card-outline-<%# (Eval("isDefault").ToString().ToUpper()=="TRUE"?"warning":"default") %>" id="div_ship_add<%# Container.ItemIndex+1 %>">
                                                            <div class="card-block">
                                                                <asp:RadioButton ID="rb_ship_here" AutoPostBack="true" GroupName="rb_Shiphere" OnCheckedChanged="rb_ship_here_CheckedChanged"  Checked='<%# Eval("isDefault") %>'  runat="server" Text="Ship here" />
                                                                <asp:HiddenField ID="hf_addressID" runat="server" Value='<%# Eval("AddressID") %>' />
                                                                <asp:HiddenField ID="hf_ship_address" runat="server" Value='<%# Eval("ShipAddress") %>' />
                                                                <asp:HiddenField ID="hf_ship_postalcode" runat="server" Value='<%# Eval("ShipPostalCode") %>' />
                                                                <asp:HiddenField ID="hf_ship_email" runat="server" Value='<%# Eval("EmailID") %>' />
                                                                <asp:HiddenField ID="hf_ship_phone" runat="server" Value='<%# Eval("Mobile") %>' />
                                                                <asp:HiddenField ID="hf_ship_cityid" runat="server" Value='<%# Eval("CityID") %>' />
                                                                <h6 class="card-subtitle text-muted"><%# (Eval("isDefault").ToString().ToUpper()=="TRUE"?"Default":"") %></h6>
                                                                <p class="card-text"> <%# Eval("ShipAddress") %> </p>
                                                                <%# Eval("CityName") %>, <%# Eval("StateName") %>, <%# Eval("CountryName") %> <br />
                                                                <%--ZIP: <%# Eval("ShipPostalCode") %><br />--%>
                                                                <span>Phone </span>: <b><%# Eval("Mobile") %></b><br />
                                                                <span>EmailID </span>: <b><%# Eval("EmailID") %></b><br />
                                                                <br />
                                                                <asp:LinkButton ID="lb_delete_ship_address" OnClientClick='javascript:return confirm("Do you really want to delete this Address ?")'
                                                                    class="btn btn-xs btn-danger delete-button" CommandName="delete_ship_address" CommandArgument='<%# Eval("AddressID") %>' runat="server">
                                                                    <i class="fa fa-times"></i>&nbsp;Delete
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ContentTemplate>
                                        </asp:UpdatePanel> 
                                            
                                 <div class="order-notes" style="display:none;">
                                        <br />
								        <div class="checkout-form-list">
									        <label>Order Notes</label>
									        <textarea placeholder="Notes about your order, e.g. special notes for delivery." rows="10" style="height:100px;background:none;font-size:15px" cols="30" id="checkout-mess"></textarea>
								        </div>									
							        </div>
						        </div>													
					        </div>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12" id="orderdetail">
						        <div class="your-order boxshadow mobilePadddingMargin" id="chkorderDet"> 
                                    <div>
                                         <div runat="server" id="dv_impinfo" class="your-order-table table-responsive checkout-order-table mb-3">
                                            <h5 runat="server" id="h2_Notice">Important info :- This Item is newly Launched will Deliver After Few Days.</h5> 
                                            <table id="TableOrderItem">
                                                <thead>
                                                    <tr>
											            <th class="product-name" style="text-align:left" >Product</th>  
											            <th class="product-total" style="text-align:right">Amount</th>
										            </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rp_NewLaunch" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="cart_item">
                                                                <td class="product-name" style="text-align:left">
                                                                    <%# Eval("BookName") %> 
                                                                </td>
                                                                 <td class="product-total" style="text-align:right">
											                        <span class="amount"><%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%>&nbsp;<%#Eval("TotalAmt",CommonCode.AmountFormat()) %></span>
										                        </td> 
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table> 
                                        </div> 
                                        <div>
                                            <h3>Your order</h3>
                                        </div>
                                        <div>
                                            <%--<h5 style="font-weight:100;margin: -17px 0 10px;">(Shipping Amount will be Charged based on Parcel Total Weight and your shipping location)</h5>--%>
                                            <h5 style="font-weight:100;margin: -17px 0 10px;">(Shipping Amount will be Charged based on your location)</h5>
                                            <span class="pb-2"  style="font-size: 15px;font-weight: 800;">(Once The Order is Placed and confirmed Delivery will be done by 5 to 6 days)</span>
                                        </div> 
                                    </div>
							        
							        <div class="your-order-table table-responsive checkout-order-table">
								        <table id="TableOrder">
									        <thead>
										        <tr>
											        <th class="product-name" style="text-align:left" >Product</th> 
                                                    <th class="product-total" style="text-align:right;display:none;">Weight(grms)</th> 
											        <th class="product-total" style="text-align:right">Total</th>
										        </tr>							
									        </thead>
									        <tbody>
                                                <asp:Repeater ID="rp_cart" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="cart_item">
										                    <td class="product-name" style="text-align:left">
											                    <%# Eval("BookName") %> <strong class="product-quantity"><%# Eval("price") %> × <%# Eval("Qty") %>  <%# (Eval("discountper").ToString()!="0"? " " + Eval("discountper") + " % Off" :"") %>
                                                                    </strong>
										                    </td>
                                                            <td class="product-name" style="text-align:center;display:none;">
											                    <asp:label runat="server" ID="lblWeight"><span class="amount"><%# Eval("CartWeight") %></span> grms</asp:label> 
										                    </td>  
										                    <td class="product-total" style="text-align:right">
											                    <span class="amount"><%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%>&nbsp;<%#Eval("TotalAmt",CommonCode.AmountFormat()) %></span>
										                    </td>
									                    </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
									        <tfoot>
									            <tr class="cart-subtotal">
                                                    <th style="text-align:left">Sub Total</th> 
                                                    <td style="text-align:center;display:none;"><span class="amount" id="cart_weight" runat="server"></span></td> 
                                                    <td style="text-align:right"><span class="amount" id="Cart_Subtotal" runat="server"></span></td>
									            </tr>
                                                <tr class="cart-subtotal">
										            <th style="text-align:left">Shipping</th>
                                                    <td style="text-align:center;display:none;"></td>
										            <td style="text-align:right"><span class="amount" id="shipping_amt" runat="server"></span></td>
									            </tr>
                                                <tr class="cart-subtotal">
										            <th style="text-align:left">Discount</th>
                                                    <td style="text-align:center;display:none;"></td>
										            <td style="text-align:right"><span class="amount" id="SPN_discount" runat="server"></span></td>
									            </tr>
									            
                                                <tr class="shipping" style="background-color: #c82128;display:none;" >
										            <th style="padding-left: 50px;text-align:left;color:white !important;">Shipping Options</th>										            
                                                    <td id="rbtshipradiobutton" style="margin-right: 50px; float:right; display:inline-flex;">
                                                        <asp:RadioButtonList ID="rb_ship_method" runat="server" AutoPostBack="true" CssClass="rb-ship-method" OnSelectedIndexChanged="rb_Ship_Method_Changed" ForeColor="White" >
                                                            <asp:ListItem Value="0" Text="Economy Delivery <label id='info' data-toggle='tooltip' title='Registered Postal Service'><i class='fa fa-info-circle'></i></label><br/>(7-10 Working Days)" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Express Delivery <label id='info' data-toggle='tooltip' title='Fedex / SpeedPost'><i class='fa fa-info-circle'></i></label><br/>(5-7 Working Days)"></asp:ListItem>
                                                        </asp:RadioButtonList>
										            </td>
									            </tr>
                                                
                                                <tr class="order-total">
										            <th style="text-align:left">Order Total</th>
                                                    <td style="text-align:center;display:none;"></td>
										            <td style="text-align:right"><asp:HiddenField ID="hf_total_amount" runat="server" /><strong><span class="amount" id="b_total_amount" runat="server"></span></strong></td>
									            </tr>
                                                <tr style="display: none;">
                                                    <th colspan="2" id="label_rb_onpay" >
                                                        <img src="../img/CCAvenue.png"/>
                                                        <asp:RadioButton ID="rb_online_pay" runat="server" Text="Online Pay" GroupName="Custompaymethod" Checked="true" />
                                                    </th>
                                                </tr>
                                               
                                                <tr style="display: none;margin-top: -25px;">
                                                    <th id="div_cod" style="display: none;">
                                                        <img src="../img/cash-on-delivery.png"/>
                                                        <asp:RadioButton ID="rb_cod" runat="server" Text="COD" GroupName="Custompaymethod" />
                                                    </th>
                                                </tr>
                                                <tr style="display: none;">
                                                    <th class="well" colspan="2" style="display: none;">
                                                        <h6 class="text-success"><i class="fa fa-check"></i>&nbsp;You selected Cash On Delivery</h6>
                                                        <span>We`ll deliver it for you.</span>
                                                    </th>
                                                </tr>								
									        </tfoot>
								        </table>
							        </div>
                                    <div class="payment-method">
								        <div class="payment-accordion" >
									        <div class="collapses-group">
										        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true"> 
											        <div class="panel panel-default">
												        <div  role="tab" id="CCAvenue"  style="display:inline-flex;">
                                                            <h4 class="panel-title">
                                                                <a >
                                                                    <asp:RadioButton ID="rbt_CCAvenue" runat="server" GroupName="paymethod" Checked="true" />
													                <img class="title-img" src="/img/CCAvenue.jpg" alt="payment" style="margin-top: -13px !important;" />
                                                                </a>
                                                            </h4>
												        </div>											 
											        </div>
                                                     <div class="panel panel-default" id="div_paypal" runat="server" >
												        <div  role="tab" id="Paypal">
													        <h4 class="panel-title">  
                                                                <%--<a >
                                                                    <asp:RadioButton ID="rbt_paypal" runat="server" GroupName="paymethod"  />
                                                                    <img src="/img/paypal.jpg" alt="payment" style="margin-top: -30px;width: 130px;" />
                                                                </a>--%>
													        </h4>
												        </div>											 
											        </div> 
										        </div>
                                                <div class="panel-group" style="margin-left: 25px;">
                                                    <div class="panel panel-default">
                                                        <input id="chkAcceptTerms" runat="server" type="checkbox" name="" style="height:20px;width:20px;" value="forever"/>
								                            <label class="inline" style="vertical-align: top;font-size: 18px;padding-top: 1px;">
                                                                I accept <a href="../topics.aspx?topicid=9" target="_blank"> Terms & Condition</a>
								                            </label>
                                                    </div>                                                    
                                                </div>                                                
									        </div>
								        </div>
                                        <div class="order-button-payment">
									        <asp:Button ID="btn_place_order" CssClass="btn-primary" OnClick="btn_place_order_Click"  ValidationGroup="place_order" runat="server" Text="Pay & Place Order"/>
								        </div>
							        </div>
                                    <br />
                                   <%-- <label>Currently Site is Under Construction Cann't place Order</label>--%>
						        </div>
					        </div>
				        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
			</div>
		</div>
		<!-- checkout-area-end -->

    <!-- Modal -->
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 style="color: #96c943" class="modal-title"><i class="fa fa-plus"></i>&nbsp;Add New Shipping Address</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button> 
                </div>
                <div class="modal-body">
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <div style="position: absolute; top: 1%; height: 97%; width: 94%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center;z-index:99999999;">
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
                                        <asp:TextBox  ID="text_new_shippingAddress"  TextMode="MultiLine" Style="resize: vertical"  class="form-control shipping-ph" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validator" ControlToValidate="text_new_shippingAddress" ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="email">Country:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:DropDownList ID="dd_new_ship_country"  AutoPostBack="true" OnSelectedIndexChanged="dd_new_ship_country_SelectedIndexChanged" readonly="true"    class="form-control shipping-ph" runat="server"
                                            style="display: block; width: 100%; height: 40px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; border-radius: 0; -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); -webkit-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;"                                            
                                            title ="you Cann't Change Country"> </asp:DropDownList>
                                        <%--<asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validator" ControlToValidate="dd_new_ship_country" ValueToCompare="Nil" Operator="NotEqual" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:CompareValidator>--%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="email">State:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:DropDownList ID="dd_new_ship_state" AutoPostBack="true" OnSelectedIndexChanged="dd_new_ship_state_SelectedIndexChanged"   class="form-control shipping-ph" runat="server"
                                            style="display: block; width: 100%; height: 40px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; border-radius: 0; -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); -webkit-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;"
                                          title ="you Cann't Change State"  > </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="validator" ControlToValidate="dd_new_ship_state" ValueToCompare="Nil" Operator="NotEqual" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="email">Town / City:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:DropDownList ID="dd_new_ship_city" class="form-control shipping-ph" runat="server"  style="display: block; width: 100%; height: 40px; 
                                                    padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; 
                                                    border-radius: 0; -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075); 
                                                    -webkit-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; 
                                                    transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;"
                                          title ="you Cann't Change City"  ></asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" CssClass="validator" ControlToValidate="dd_new_ship_city" ValueToCompare="Nil" Operator="NotEqual" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:CompareValidator>
                                    </div>
                                </div>

                                <div class="form-group" >
                                    <label class="control-label col-sm-2" for="pwd">Postal Code:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:TextBox ID="text_new_ship_PostalCode" class="form-control shipping-ph" runat="server"  placeholder="Pin / Zip Code" ></asp:TextBox>
                                       <%--   pattern="[1-9]{1}[0-9]{5}" title="Please Enter 6 Digits correct Postal Code Not Start with 0, Postal Code Like (124001)" 
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="validator" ControlToValidate="text_new_ship_PostalCode" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="pwd">Email ID:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:TextBox ID="text_new_ship_EmailID" class="form-control shipping-ph" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="validator" ControlToValidate="text_new_ship_EmailID" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-2" for="pwd">Mobile:</label>
                                    <div class="col-sm-10 init-validator">
                                        <asp:TextBox ID="text_new_ship_Mobile" title="Please enter valid Mobile No." pattern="^[0-9]{1,10}$" class="form-control shipping-ph" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="validator" ControlToValidate="text_new_ship_Mobile" 
                                            ValidationGroup="save_new_shipping_address"><i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-offset-2 col-sm-10">
                                        <asp:CheckBox ID="cb_make_default" Style="padding: 0" class="checkbox" Text="Make Default" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-2 col-sm-10" style="text-align:center;">
                                        <asp:Button ID="btn_Save_new_shipping_address" runat="server" ValidationGroup="save_new_shipping_address" OnClick="btn_Save_new_shipping_address_Click" Text="Save" 
                                            class="btn btn-success save-button" style ="width:22%;  box-shadow: -4px 3px 0px 0px grey;font-size:19px;font-weight:700;" />
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

    <script>

        $("[name$='$rb_ship_here']").attr("name", $("[name$='$rb_ship_here']").attr("name"));

        $("[name$='$rb_ship_here]").click(function () {
            //set name for all to name of clicked 
            $("[name$='$rb_ship_here]").attr("name", this.attr("name"));
            $("[name$='$rb_ship_here]").attr("checked", true);
        });

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
        $(document).ready(function () {
            $(".alert").removeClass("fade in");
            $(".alert").fadeIn();
        });
        $("#ship-box").click(function () {
            $(".alert").fadeToggle();
        });


<%--        function showProgress() {
        var updateProgress = $get("<%= UpdateProgress.ClientID %>");
        updateProgress.style.display = "block";
        }--%>

    </script>
</asp:Content>

