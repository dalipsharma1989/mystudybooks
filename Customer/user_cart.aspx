<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.master" AutoEventWireup="true" CodeFile="user_cart.aspx.cs" Inherits="_Default" %>

<%@ MasterType VirtualPath="~/CustomerMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/customecss/homeDCbooks.css" rel="stylesheet" />
    <link href="../css/customecss/user-cart.css" rel="stylesheet" />
    <script type="text/javascript">
        function minmax(value, min, max) 
            {
                if(parseInt(value) < min || isNaN(parseInt(value))) 
                    return min; 
                else if(parseInt(value) > max) 
                    return max; 
                else return value;
        }


        $(document).ready(function () {
            $("#txtInputBox input").blur(function () {
                document.getElementById("ContentPlaceHolder1_btn_update_cart").click();
            })
            $("#txtInputBoxMobile input").blur(function () {
                document.getElementById("ContentPlaceHolder1_btn_Update_Cart_Mobile").click();
            })
        });

    </script>
    <style type="text/css" >
        .rdb{
            width:100%;
        }
        .rdb label{
            margin-right: 10%;
        }
        .rdb input{
            transform: translate(0px, 20%);
        }
        .shipping span{

            font-weight: 600;
     font-size: 15px;
        }
          .cart-subtotal span{
            font-weight: 600;
            font-size: 15px;
            }
            .order-total span{
              color: #96c943;
                font-weight: 700;
                font-size: 20px;
        }
            .cart_totals h2{
                font-family: "Helvetica Neue", Helvetica, Arial, sans-serif !important;
            }
          
        .table-content table {
        border:none;
        box-shadow:0px 0px 5px #dcdcdc;
        }
        
         .table-content table td {
        }
           .table-content table th,
    .table-content table td{
               border-right:none !important;

           }
        .cart_totals table {
            float:none !important;
        }
           
       
           .table-content tr th{
                  font-weight: 700 !important;
                  font-size: 14px;
           }
           @media only screen and (min-width:1200px){
               .card-details{
                       position: fixed;
                      left: 943px;
               }
               .checkout{
                   position:fixed;
                       left: 1078px;
                  top: 445px;
               }
           }
        @media only screen and (min-width:600px){
                #dv_Desktop{
                    display:inline;
                }
                #dv_Mobile{
                    display:none !important;
                }
                #dv_update_Desktop{
                    display:inline;
                }
                #dv_update_Mobile{
                    display:none;
                }
        }
        @media only screen and (max-width:600px) {
            /*.cart_totals {
                text-align: left;
            }
                .cart_totals table {
             float: right;
               margin: -30px;
              text-align: left;
   
                   }*/
                .update-btn{
                    width:100%;
                }
                #dv_update_Desktop{
                    display:none;
                }
                #dv_update_Mobile{
                    display:inline;
                }
                #dv_Desktop{
                    display:none;
                }
                #dv_Mobile{
                    display:inline;
                }
                .cla{
                    margin-left: -20px;
                }
        }
       </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- breadcrumbs-area-start -->
    <div class="breadcrumbs-area "> 
		    <div class="row">
			    <div class="col-lg-12">
				    <div class="breadcrumbs-menu">
					    <ul>
						    <li><a href="/">Home</a></li>
						    <li><a href="#" class="active">User Cart</a></li>
					    </ul>
				    </div>
			    </div>
		    </div> 
    </div>
    <!-- breadcrumbs-area-end -->
    
	<!-- entry-header-area-start -->
	<div class="entry-header-area">
		<div  >
			<div class="row">
				<div class="col-lg-12">
					<div style="margin-top: 20px;" class="entry-header-title">
						<h2 style="text-align:center;">User Cart</h2>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- entry-header-area-end -->

    <!-- cart-main-area-start -->
	<div class="cart-main-area mb-70">
		<div  >
			<div style="margin-top: 20px;" class="row">
            <div class="col-xs-12 col-lg-12">
                <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
            </div>
            <asp:HiddenField ID="hf_cartID" runat="server" />
			<div class="col-lg-12 cart-details" id="div_user_cart" runat="server">
                <div class="table-content table-responsive col-md-12 col-lg-9">
                    <div id="dv_Desktop">
					<table  class="table">
						<thead>
							<tr style="background-color:#d3d3d333;">
								<th class="product-thumbnail" style="text-align: left !important;">Image</th>
								<th class="product-name" style="text-align: left !important;">Product</th>
								<th class="product-price" style="text-align: right !important;">Price</th>
								<th class="product-quantity" style="text-align: center !important;">Quantity</th>
                                <th class="product-discount"  style="text-align: right !important;">Discount</th>
								<th class="product-subtotal"  style="text-align: right !important;">Total</th>
								<th class="product-remove" style="text-align: left !important;">Remove</th>
							</tr>
						</thead>
						<tbody>
                            <asp:Repeater ID="rp_cart" runat="server" OnItemCommand="rp_cart_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td class="product-thumbnail" style="width:93px;vertical-align:middle;text-align:center;">
                                            <a id="listItem-image-link_<%# Eval("ProductID") %>" href="../view_book.aspx?productid=<%# Eval("ProductID") %>&title=<%# Eval("BookName") %>">
                                                <img src="<%# Eval("ImgPath") %>"  onError="this.onerror=null;this.src='/resources/no-image.jpg';"  alt="<%# Eval("BookName") %>"  style="height:100px !important;">
                                            </a>   
                                        </td>  
                                        <td class="product-name" style="width:300px;vertical-align:middle;text-align:center;">
                                            <a class="product-name" id="listItem-image-link-<%# Eval("ProductID") %>" href="../view_book.aspx?productid=<%# Eval("ProductID") %>&title=<%# Eval("BookName") %>">
                                                <%# Eval("BookName") %>
                                            </a>
                                            </td>
                                        <td class="product-price" style="width:70px;vertical-align:middle;font-weight:700; text-align:right !important;">
                                            <span>
                                                <%# Eval("SaleCurrency") %> <%# Eval("Price",CommonCode.AmountFormat()) %>
                                            </span>
                                        </td> 
                                        <td class="product-quantity" style="font-weight:700;text-align:right !important;vertical-align:middle;width:90px;">
                                            <style type="text/css">
                                                .validator{
                                                    left:-20px !important;
                                                    bottom:0px !important;
                                                }
                                            </style>
                                            <div class="init-validator quantity-div" id="txtInputBox">
                                                <asp:TextBox ID="textQuantity" type="Number" min="1" max="9999"  runat="server" Text='<%# Eval("Qty") %>'
                                                     onblur="this.value = minmax(this.value, 1, 9999)" CssClass="quantity"
                                                    ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="textQuantity" CssClass="validator"
                                                    ToolTip="Quantity Required" data-toggle="tooltip" ErrorMessage="* Qty 0 or Netgative not allowed"> <i class="fa fa-times"></i>
                                                </asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="textQuantity" CssClass="validator" MinimumValue="1" 
                                                    MaximumValue="999999" Type="Integer" ToolTip="Quantity must be a natural number" ErrorMessage="* Qty 0 or Netgative not allowed" data-toggle="tooltip">
                                                    <i class="fa fa-warning"></i>&nbsp;* Qty 0 or Netgative not allowed</asp:RangeValidator>
                                            </div>
                                        </td>     
                                        <td class="product-subtotal " style="font-weight:700;text-align:right !important;vertical-align:middle;width:90px;">
                                            <%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%> <%# Eval("DiscountAmt",CommonCode.AmountFormat()) %>
                                        </td>
                                        <td class="product-subtotal" style="font-weight:700;text-align:right !important;vertical-align:middle;width:100px;">
                                            <%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%> <%# Eval("TotalAmt",CommonCode.AmountFormat()) %>
                                        </td>
                                        <td class="product-remove" style="vertical-align: middle;width:50px;">
                                            <asp:HiddenField ID="hf_productID" runat="server" Value='<%# Eval("ProductID") %>' />
                                            <asp:HiddenField ID="hf_ISBN" runat="server" Value='<%# Eval("ISBN") %>' />
                                            <asp:HiddenField ID="hf_BookName" runat="server" Value='<%# Eval("BookName") %>' />
                                            <asp:HiddenField ID="hf_discount" runat="server" Value='<%# Eval("DiscountAmt") %>' />
                                            <asp:HiddenField ID="hf_Clbal" runat="server" Value='<%# Eval("Clbal") %>' />
                                            <asp:LinkButton ID="lb_remove_item_from_cart" CommandName="remove_item_from_cart" CommandArgument='<%# Eval("ProductID") %>' class="remove-item" CausesValidation="false" runat="server">
                                                <i class="fa fa-times-circle"></i>
                                            </asp:LinkButton>
                                        </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>  
                    </div>
                    <div id="dv_Mobile" >
                        <asp:Repeater runat="server" ID="rp_MobileCart" OnItemCommand="rp_MobileCart_ItemCommand">
                            <ItemTemplate>
                                <div class="" style="padding:15px 0;">
                                    <a class="sliderImage-wrapper" href="#" title="<%# Eval("BookName") %>" >
                                        <div class="flip-card">
                                            <div class="flip-card-insner">
                                                <div class="flip-card-front">
                                                    <div style="margin:0px;" class="slider-image-wrapper">
                                                        <div class="hidden">Out of Stock</div>
                                                        <img class="sliderImage owl-lazy" src="<%# Eval("ImgPath") %>" alt="<%# Eval("BookName") %>" title="<%# Eval("BookName") %>"
                                                             onerror="this.onerror=null;this.src='../resources/no-image.jpg';">
                                                    </div>
                                                    <div>
                                                        <a  style="padding-left:10px;font-size:12px;" class="product_tile" href="/view_book.aspx?productid=<%# Eval("ProductID") %>&title=<%# Eval("BookName") %>" >
                                                            <%# Eval("BookName") %> 
                                                        </a>
                                                        <span style="padding-left:10px;font-size:12px;"><%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%> <%# Eval("TotalAmt",CommonCode.AmountFormat()) %></span>
                                                        <div class="init-validator quantity-div"  id="txtInputBoxMobile"  style="padding-left:10px;padding-top:10px;">
                                                            <asp:TextBox ID="textQuantity" type="Number" min="1" max="9999"  runat="server" Text='<%# Eval("Qty") %>'
                                                                 onblur="this.value = minmax(this.value, 1, 9999)" CssClass="quantity" ></asp:TextBox>
                                                            <asp:LinkButton ID="lb_remove_item_from_cart" CommandName="remove_item_from_cart"  class="btn btn-danger" CausesValidation="false"
                                                                CommandArgument='<%# Eval("ProductID") %>'  runat="server"> <i class="fa fa-trash-o"></i>&nbsp;Remove
                                                                </asp:LinkButton>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="textQuantity" CssClass="validator"
                                                                ToolTip="Quantity Required" data-toggle="tooltip" ErrorMessage="* Qty 0 or Netgative not allowed"> <i class="fa fa-times"></i>
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="textQuantity" CssClass="validator" MinimumValue="1" 
                                                                MaximumValue="999999" Type="Integer" ToolTip="Quantity must be a natural number" ErrorMessage="* Qty 0 or Netgative not allowed" data-toggle="tooltip">
                                                                <i class="fa fa-warning"></i>&nbsp;* Qty 0 or Netgative not allowed</asp:RangeValidator>
                                                        </div>
                                                        <div class="add-links-wrap-cover">
                                                            <div class="add-links-wrap">
                                                                <asp:HiddenField ID="hf_productID" runat="server" Value='<%# Eval("ProductID") %>' />
                                                                <asp:HiddenField ID="hf_ISBN" runat="server" Value='<%# Eval("ISBN") %>' />
                                                                <asp:HiddenField ID="hf_BookName" runat="server" Value='<%# Eval("BookName") %>' />
                                                                <asp:HiddenField ID="hf_discount" runat="server" Value='<%# Eval("DiscountAmt") %>' />
                                                                <asp:HiddenField ID="hf_Clbal" runat="server" Value='<%# Eval("Clbal") %>' />
                                                                
                                                            </div> 
                                                        </div>
                                                    </div> 
                                                </div> 
                                            </div> 
                                        </div> 
                                    </a>  
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div> 
                    <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="buttons-cart mb-30" id="dv_update_Desktop">
                                    <ul style="display:flex" class="cla">
                                        <li>
                                            <asp:Button ID="btn_update_cart" runat="server" class="btn  btn-info pull-right btn btn-primary update-btn" OnClick="btn_update_cart_Click" Text="Update Cart" />
                                        </li>
                                        <li>
                                            <a href="../search_results.aspx" class="update-btn btn btn-primary" style="text-decoration: none;">Continue Shopping</a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="buttons-cart mb-30" id="dv_update_Mobile" >
                                    <ul style="display:flex" class="cla">
                                        <li>
                                            <asp:Button ID="btn_Update_Cart_Mobile" runat="server" class="btn  btn-info pull-right btn btn-primary update-btn" OnClick="btn_Update_Cart_Mobile_Click" Text="Update Cart" />
                                        </li>
                                        <li>
                                            <a href="../search_results.aspx" class="update-btn btn btn-primary" style="text-decoration: none;">Continue Shopping</a>
                                        </li>
                                    </ul>
                                </div>
                            </div> 
                        </div> 
                </div>



                   <div  class="col-lg-3 col-md-12 col-sm-12 col-xs-12 ">
                    <asp:UpdatePanel ID="CART_TOTALS" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="cart_totals"> 
                            <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12">
                                <h2 style="white-space:nowrap">Cart Totals</h2> 
                            </div>
                            <div  class="row wc-proceed-to-checkout pb-2" style="text-align: left;">
                                <%--<span  style="font-size: 15px;font-weight: 800;">Free delivery for orders above AED 50.</span>--%>
                            </div>
                            <table class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <tbody>
                                    <tr class="cart-subtotal" style="display: none;">
                                        <th>Total Cost</th>
                                        <td>
                                            <asp:HiddenField ID="hf_total_amount" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="cart-subtotal" id="td_subtotal" runat="server">
                                        <th>Subtotal</th>
                                        <td style="white-space:nowrap">
                                            <span id="span_total_amt" runat="server"></span>
                                        </td>
                                    </tr>
                                    <tr class="shipping" id="td1" runat="server">
                                        <th>Shipping</th>
                                        <td style="white-space:nowrap">
                                            <span id="shipping_amt" runat="server"></span>
                                        </td>
                                    </tr>
                                    <tr class="order-total">
                                        <th>Total</th>
                                        <td style="white-space:nowrap" id="td_total_amount" runat="server">
                                            <strong>
                                                <span id="ttl-amt" class="amount"></span>
                                            </strong>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            
                            <div class="row wc-proceed-to-checkout">                               
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary  update-btn" OnClick="btn_proceed_to_checkout_Click" Text="Proceed To Checkout" />                              
                            </div> 
                        </div>
                        
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                 <%--  <div class="wc-proceed-to-checkout checkout">
                                <asp:Button ID="btn_proceed_to_checkout" runat="server" class="btn btn-primary pull-right update-btn" OnClick="btn_proceed_to_checkout_Click"
                                        Text="Proceed To Checkout" />
                            </div>--%>

            </div>
        </div>
        
		</div>
	</div>
    <!-- cart-main-area-end -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>


