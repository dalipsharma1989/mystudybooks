<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="view_book.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/customecss/style.css" rel="stylesheet" />
     <link href="css/customecss/homeDCbooks.css" rel="stylesheet" /> 
    <link href="css/customecss/homrPage.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function(){
            $("fa-star").hover(function () {
                {
                    alert('fa-star');
                    this.preAll().css('color','gold');
                }
            });
        });

        //function ModalEntry(ProductID) {
        //    $.ajax({
        //        type: "POST",
        //        url: "index.aspx/AddDataintoCart",
        //        data: "{'ProductID': '" + ProductID + "'}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            if (data.d.Result == "success") {
        //                $("#cart_qty").text(data.d.CartItems);
        //                $("#Span1").text(data.d.CartItems);
        //                $("#ltr_scripts").append(data.d.litmsg);
        //                alert("Item successfully added to in your cart");
        //            } else {
        //                alert(data.d.Result);
        //            }
        //        },
        //        failure: function (response) {
        //            alert('Failed to Add item into cart');
        //            alert(response.d);
        //        } 
        //    });
        //    return false;
        //} 
        //function userwishlist(ProductID) {
        //    $.ajax({
        //        type: "POST",
        //        url: "index.aspx/AddDataInToWishList",
        //        data: "{'ProductID': '" + ProductID + "'}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //            if (data.d.Result == "success") {
        //                alert("Product added to wishlist!");
        //            } else if (data.d.Result == "") {
        //                if (data.d.uriDirect != "") {
        //                    window.location = data.d.uriDirect;
        //                }
        //            } else {
        //                alert(data.d.Result);
        //            }
        //        },
        //        failure: function (response) {
        //            alert('Failed to Add item into cart');
        //            alert(response.d);
        //        } 
        //    });
        //    return false;
        //}

    </script>
     
    <style type="text/css"> 
        .wishlist-button:hover{
            background-color:#162e2d;
        }

        .image-stock-out{
            position: absolute;
            width: 100%;
            height: 100%;
            font-size: 15px;
            left: 0;
            right: 0;
            color: red;
            line-height: 11;
            background: rgba(255,255,255,.75);
            text-shadow: 0 0 12px #000; 
            }
     
        .row{
    width:100%
}

         .product-main {
        margin:0px -14px;
    }

            .product-main-area{
                padding:10px;
            }


         .givenmeElipse{
                display: -webkit-box !important;
                font-size: 16px !important;
                overflow: hidden !important;
                text-overflow: ellipsis !important;
                -webkit-box-orient: vertical !important;
                -webkit-line-clamp: 1 !important;
        }
         #NotifyMe{
                background-color:#96c946 !important;
                border-color:#fff !important;
                font-size: 14px;
                position: relative;
                bottom: 2px;
                font-weight: 800;
                color: deepskyblue;
         }
         #NotifyMe:hover  {                
                color: #3051a0;
         }
         #ContentPlaceHolder1_btn_notifyme{
             background-color:#00abe0;
             color: #fff !important;
             font-size:16px;
         }     
         #ContentPlaceHolder1_btn_notifyme:hover  {                
                background-color: lightseagreen;
         }
  
        #btn_notifymeClose{
            background-color:#00abe0;
             color: #fff !important;
             font-size:16px;
        }
        #btn_notifymeClose:hover  {                
                background-color: lightskyblue;
         }
        table{
            width: 42% !important;
           /* box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);*/
        }
        table tr th{
            border:none !important;
            font-size: 13px;
        }
        table td{
            border:none !important;
        }
  
        
        .tab-total{
      /*    box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);*/
                 margin-right: 20px;
                 margin-bottom:20px;
                 margin-left:20px;


                
        }
          .cart-icon:hover {
            transform: scale(1.5);
        }

        .product-addto-links a:nth-child(3) {
            margin-top: -5px;
        }
        .primary{
            margin-top:44px;
        }
        .notify-btn{
                background-color: #3051a0 !important;
                color: white !important;
                box-shadow: -4px 3px 0px 0px grey;
                border-radius: 4px;
               width: 120px;
               height: 41px;
               margin-top: 40px;
               padding: 9px 43px 11px 7px;
               font-size: 16px;
        }
        .notify-btn:hover{
            background:#3051a0 !important;
             box-shadow: -4px 3px 0px 0px lightgrey;
        }    

         .Star
        {
            background-image: url(img/Star.gif);
            height: 17px;
            width: 17px;
        }
        .WaitingStar
        {
            background-image: url(img/WaitingStar.gif);
            height: 17px;
            width: 17px;
        }
        .FilledStar
        {

            background-image: url(img/FilledStar.png);
            height: 17px;
            width: 17px;
        }
     
        .table{
            box-shadow:none;
        }
     
        @media only screen and (min-width:320px) and (max-width:600px){
           .link-wishlist{
               margin-top:5px;
           } 
         .product-addto-links a:nth-child(3){
             margin-top:2px;
         }
         .primary{
             margin-top: 30px;
         }
         .page-title h1{
             margin-top: -143px !important;
         }
         .tab-total{
             margin:0px;
         }

        }
        @media only screen and (min-width:768px) and (max-width:990px) {
         /*.clone{
                width:213px !important;
            }*/
        }
        @media (max-width:480px) {
            .similar_item_container {
            margin:0 -15px;
            }
        }
        
        ul.flex-direction-nav {
            display: none;
        }

        .new-item{
            background: #FC0;
            color: white;
            position: relative;
            right: 53px;
            top: -216px;
            height: 24px;
            width: 50px;
            font-size: 11px;
            display: inline-block;
            padding-top: 4px;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area"> 
				<div class="row">
					<div class="col-lg-12">
						<div style="margin-left: 32px;" class="breadcrumbs-menu">
							<ul>
								<li><a href="/">Home</a></li>
								<li><a href="#" class="active">Book - Details</a></li>
							</ul>
						</div>
					</div>
				</div> 
		</div>
	<!-- breadcrumbs-area-end -->
		
		<div class="product-main-area mb-70">		
            <div class="row">
                		<div class="col-xs-12 col-lg-12">
						<!-- product-main-area-start -->
						<div class="product-main">
							<div class="row">
                                <div class="col-lg-1 col-md-1"></div>
								<div class="col-lg-3 col-md-3 col-sm-5 col-xs-12">
									<div class="flexslider">
										<ul class="slides">
                                            <asp:Repeater ID="rp_book_detail_side_image" runat="server">
                                                <ItemTemplate>
                                                    <li data-thumb="<%# Eval("ImgPath") %>">
                                                           <%-- <img class="photo" src="/resources/product/<%# Eval("ISBN") %>.png"
                                                                <img class="primary" src="resources/product/<%# Eval("ISBN") %>.png"--%> 
                                                        <img  class="primary" src="<%# Eval("ImgPath") %>" onerror="this.onerror=null;this.src='resources/no-image.jpg';"  alt="<%# Eval("BookName") %>">
											        </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>

								</div>
                                <div style="margin-top:28px" class="col-lg-8 col-md-8 col-sm-7 col-xs-12">
									<div class="product-info-main">
                                        <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
                                        <asp:Literal ID="ltr_book_detail_msg" runat="server"></asp:Literal>
                                        <asp:HiddenField ID="hf_productID" runat="server" />
                                        <asp:Repeater ID="rp_book_detail_main" runat="server" OnItemCommand="rp_book_detail_ItemCommand">
                                            <ItemTemplate>
										        <div class="page-title">
                                                    <h1 > <%# Eval("BookName") %> </h1>
                                                   by<span class="author"> <%# Eval("Author") %></span>
                                                </div>
                                                <div class="product-info-stock-sku pb-10" style="display:none;">
                                                    <div class="product-attribute">
                                                    <%# (Eval("ClosingBalStatus").ToString()=="Out of Stock"?"<span style='color: red;padding-left: 9px;font-size: 14px;display: inline-block;font-weight: 600;'>"+Eval("ClosingBalStatus")+"</span>&nbsp;":"<span style='color: red;'>"+Eval("ClosingBalStatus")+"</span>") %>
											        </div>                                                  
                                                </div>
                                                <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 decoration-border"></div>
                                                <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12 star-rating">
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                </div>
                                                 <div  class="col-md-12 col-lg-12 col-sm-12 col-xs-12 decoration-border-1"></div>
										        <div style="margin-top: 15px;" class="product-info-table">
                                                    <table class="table product-table col-md-6 col-lg-6" style='white-space: normal'>
                                                            <tr> <th> ISBN: </th> <td> <%#Eval("ISBN") %> </td> </tr>    
                                                            <%# (string.IsNullOrEmpty(Eval("BookName").ToString())? "" : "<tr><th>Book Name:</th><td>"+Eval("BookName")+"</td></tr>") %>                                                    
                                                            <%# (string.IsNullOrEmpty(Eval("Author").ToString()) ? "" : "<tr><th>Author:</th><td>"+Eval("Author")+"</td></tr>") %>                                                        
                                                            <%# (string.IsNullOrEmpty(Eval("Publisher").ToString()) ? "" : "<tr><th>Publisher:</th><td>"+Eval("Publisher")+"</td></tr>") %>                                                    
                                                            <%# (string.IsNullOrEmpty(Eval("edition").ToString())? "": "<tr><th>Edition:</th><td>"+Eval("edition")+"</td></tr>") %>
                                                            <%# (string.IsNullOrEmpty(Eval("RePrint").ToString())? "": "<tr><th>Reprint:</th><td>"+Eval("RePrint")+"</td></tr>") %>
                                                            <%# (string.IsNullOrEmpty(Eval("volume").ToString())? "": "<tr><th>Volume:</th><td>"+Eval("volume")+"</td></tr>") %>
                                                            <%# (string.IsNullOrEmpty(Eval("Binding").ToString()) ? "" : "<tr><th>Binding:</th><td>"+Eval("Binding")+"</td></tr>") %>
                                                            <%--<%# (string.IsNullOrEmpty(Eval("PublishYear").ToString()) && Eval("PublishYear").ToString()!="0"  ? "" : "<tr><th>Publish Year:</th><td>"+Eval("PublishYear")+"</td></tr>") %>--%>
                                                            <%# (Eval("PublishYear").ToString() == "0"?"" : "<tr><th>Publish Year:</th><td>"+Eval("PublishYear")+"</td></tr>")%>
                                                            <%# (string.IsNullOrEmpty(Eval("TotalPages").ToString())? "": "<tr><th>Total Pages:</th><td>"+Eval("TotalPages")+"</td></tr>") %>
                                                            <tr><th>Availablity:</th><td>
                                                            <%# (Eval("ClosingBalStatus").ToString()=="Out of Stock"?"<span style='color:red !important' class='text-danger'><b>"+Eval("ClosingBalStatus")+"</b></span>&nbsp;<a ID='NotifyMe' data-toggle='modal' data-target='#modal_notify_me' class='btn btn-xs btn-primary'>Notify me</a>":"<span class='text-success'><b>"+Eval("ClosingBalStatus")+"</b></span>") %>
                                                            </td></tr>
                                                    </table> 
                                                </div>
                                                <div  class="col-md-12 col-lg-12 col-sm-12 col-xs-12 decoration-border-1">
                                                    <div style="font-weight: 800;font-size:20px;"><%# (string.IsNullOrEmpty(Eval("UserDefinedField10").ToString())? "" : "" + Eval("UserDefinedField10") + "") %> </div>
                                                </div>

                                                <div  class="product-info-price" >
											            <div class="price-final">
                                                        <%--<%# (string.IsNullOrEmpty(Eval("DiscountPrice").ToString())? "":
                                                                "<span class=\"old-price\">"+Eval("SaleCurrency")+" "+Eval("SalePrice",CommonCode.AmountFormat())+"</span>") %>  &nbsp;  
                                                        <%# (string.IsNullOrEmpty(Eval("DiscountPrice").ToString()) ?  
                                                                "<span>"+Eval("SaleCurrency")+" "+Eval("DiscountPrice",CommonCode.AmountFormat())+"</span>" 
                                                                : "<span>"+Eval("SaleCurrency")+" "+Eval("DiscountPrice",CommonCode.AmountFormat())+"</span>") %>--%>
                                                        <%# (Eval("SalePrice").ToString()==Eval("DiscountPrice").ToString() 
                                                                    ? "<span>"+Eval("SaleCurrency")+" "+Eval("SalePrice",CommonCode.AmountFormat())+"</span>" 
                                                                    :"<span class=\"old-price\">"+Eval("SaleCurrency")+" "+Eval("SalePrice",CommonCode.AmountFormat())+"</span>&nbsp;<span>"+Eval("SaleCurrency")+" "+Eval("DiscountPrice",CommonCode.AmountFormat())+"</span>"
                                                            )%>
											            </div>
										            </div>
                                             
                                                <div>
                                                    <a href="#" class="link-wishlist wishlist-button" title="Add To Wishlist" style="padding-right: 10px;width:190px;" ID="<%#Eval("ProductID") %>-<%# Container.ItemIndex + 1%>" onclick="return userwishlist('<%#Eval("ProductID") %>');" >
                                                        <i style="font-weight: 600; width: 100%;" class="fa fa-heart">
                                                            <span style="font-family:Arial, Helvetica, sans-serif;">Add To WishList</span>
                                                        </i>
                                                    </a>
                                                     <%--<asp:LinkButton ID="LinkButton4" ToolTip="Add To Wishlist" Width="190px" style="padding-right: 10px;" CommandName="addtowishlist" 
                                                            CommandArgument='<%# Eval("ProductId") %>' runat="server" class="link-wishlist wishlist-button"> 
                                                         <i style="font-weight: 600; width: 100%;" class="fa fa-heart">
                                                                <span style="font-family:Arial, Helvetica, sans-serif;">Add To WishList</span></i> </asp:LinkButton>--%>
                                                    <span class="price-discount">
                                                        <%# (Eval("SpecialDiscount").ToString() == "0.00"?"":"" + Eval("SpecialDiscount") + "% Off")%>
                                                    </span>
                                                </div>
                                               
                                                <%--<div class="product-add-form">
												        <div class="quality-button">                                                            
												        </div>
                                                    CssClass="qty"
										        </div>--%>
                                                <div class="product-social-links">
											        <div style="display:flex" class="product-addto-links">
                                                         
                                                        <%--<asp:LinkButton ID="LinkButton2" ToolTip="Add To Cart"  CommandName="addtocart"  CommandArgument='<%# Eval("ProductId") %>' runat="server" 
                                                            class='<%# (Convert.ToInt32(Eval("ClosingBal"))<=0?"btn btn-primary disabled":"btn btn-primary") %>' >
                                                        <i   class="fa fa-shopping-cart"> </i><%# (Eval("ClosingBalStatus").ToString() == "Out of Stock"?"&nbsp;&nbsp;Add To Cart":"&nbsp;&nbsp;Add To Cart") %>
                                                        </asp:LinkButton> --%>

                                                        <a href="#"  ID="<%# Container.ItemIndex + 1%>" title="Add To Cart" onclick="return ModalEntry('<%#Eval("ProductID") %>');" 
                                                            class='<%# (Convert.ToInt32(Eval("ClosingBal"))<=0?"btn btn-primary disabled addCartButton ":"btn btn-primary addCartButton") %>'
                                                             Style="background-color:#f59744;border-color:#f59744;" >
                                                            <i class="fa fa-shopping-cart hidden-xs"></i>
                                                            <span class="">Add to Cart</span>
                                                        </a>

                                                        <%--<asp:LinkButton ID="LinkButton3" CommandName="buynow" CommandArgument='<%# Eval("ProductId") %>' Width="150px"  ToolTip='<%# string.Concat("Buy ",Eval("BookName")," @",Eval("SaleCurrency")," ",Eval("SalePrice","{0:0.000}")) %>' 
                                                        runat="server" class='<%# (Convert.ToInt32(Eval("ClosingBal"))<=0?"btn btn-buy btn-primary  disabled":"btn btn-primary") %>'  Style="background-color:#96c946;border-color:#96c946;"> 
                                                        <i style="font-weight: 600; width: 100%;" class="fa fa-credit-card"><span style="font-family:Arial, Helvetica, sans-serif;"><%# (Eval("ClosingBalStatus").ToString() == "Out of Stock" ? "&nbsp;&nbsp;Out of Stock" : "&nbsp;&nbsp;Buy Now") %></span></i></asp:LinkButton>--%>
                                                        <asp:LinkButton ID="LinkButton3" CommandName="buynow" CommandArgument='<%# Eval("ProductId") %>'  ToolTip='<%# string.Concat("Buy ",Eval("BookName")," @",Eval("SaleCurrency")," ",Eval("SalePrice","{0:0.000}")) %>' 
                                                        runat="server" class='<%# (Convert.ToInt32(Eval("ClosingBal"))<=0?"btn btn-buy btn-primary  disabled":"btn btn-primary") %>'  Style="background-color:#f59744;border-color:#f59744;"> 
                                                        <i  class="fa fa-credit-card"></i><%# (Eval("ClosingBalStatus").ToString() == "Out of Stock" ? "&nbsp;&nbsp;Buy Now" : "&nbsp;&nbsp;Buy Now") %></asp:LinkButton>
                                                    </div>
											        <div class="product-addto-links-text">
												        <%--<p>
                                                            <%# Eval("AboutProduct") %>
												        </p>--%>
											        </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
						</div>
                         <div style="width:90%;margin:auto;display:none;" class="product-info-area">
							<!-- Nav tabs -->
							<ul class="nav nav-tabs" role="tablist">
								<li class="active"><a style="background:#96c946" href="#Details" data-toggle="tab">Details</a></li>
								<li><a href="#Reviews" data-toggle="tab" id="review_count" runat="server">Reviews</a></li>
							</ul>
							<div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="tab-content">
                                <div class="tab-pane active" id="Details">
                                    <div class="valu">
                                        <asp:Repeater ID="rp_book_details_tabc" runat="server">
                                            <ItemTemplate>
                                                <p>
                                                    <asp:Label ID="lblAboutProduct" runat="server"  Text='<%# Eval("AboutProduct") %>' ToolTip='<%# Eval("AboutProduct") %>' /><br/>
                                                </p>
                                                <ul>
                                                <%#(string.IsNullOrEmpty(Eval("Author").ToString()) ?"": "<li>"+ "<i class=\"fa fa-circle\"></i>Author : " +Eval("Author")+"</li>") %>
                                                <%#(string.IsNullOrEmpty(Eval("Publisher").ToString()) ?"": "<li>"+ "<i class=\"fa fa-circle\"></i>Publisher : " +Eval("Publisher")+"</li>") %>
                                                <%#(string.IsNullOrEmpty(Eval("edition").ToString())?"":"<li>"+"<i class=\"fa fa-circle\"></i>Edition : "+Eval("edition")+"</li>")%>
                                                <%#(string.IsNullOrEmpty(Eval("RePrint").ToString())?"":"<li>"+"<i class=\"fa fa-circle\"></i>Reprint : "+Eval("RePrint")+"</li>")%>
                                                <%#(string.IsNullOrEmpty(Eval("volume").ToString())?"":"<li>"+"<i class=\"fa fa-circle\"></i>Volume : "+Eval("volume")+"</li>")%>
                                                <%# (string.IsNullOrEmpty(Eval("Binding").ToString()) ? "" : "<tr><th>Binding</th><td>"+Eval("Binding")+"</td></tr>") %>
                                                <%#(string.IsNullOrEmpty(Eval("PublishYear").ToString())?"":"<li>"+"<i class=\"fa fa-circle\"></i>Publishing Year : "+Eval("PublishYear")+"</li>")%>    
                                                <%#(string.IsNullOrEmpty(Eval("TotalPages").ToString())?"":"<li>"+"<i class=\"fa fa-circle\"></i>Total Pages : "+Eval("TotalPages")+"</li>")%>
                                                <%#(string.IsNullOrEmpty(Eval("CategoryDesc").ToString()) ?"": "<li>"+ "<i class=\"fa fa-circle\"></i>Category : " +Eval("CategoryDesc")+"</li>") %>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                     </div>
                                </div>
                                <div class="tab-pane" id="Reviews">
                                    <div class="valu valu-2">
                                        <div style="" class="section-title mb-18 mt-16">
                                            <h2 style="position: relative;left: 22px;">Customer Reviews</h2>
                                        </div>
                                        <ul>
                                            <li id="p_blank_review_msg" runat="server">
                                                <div style="position: relative;bottom: 30px;" class="review-title">
                                                    <h3>Be the first one to give review to this Book.</h3>
                                                </div>
                                            </li>
                                            
                                        </ul>
                                        <div style="display:none" class="review-add">
                                            <h3>You're reviewing : </h3>
                                            <h4 ></h4>
                                        </div>
                                        <div class="review-field-ratings">
                                            <span>Your Rating <sup>*</sup></span>
                                            <div class="control">
                                                <div class="single-control">
                                                    <div class="review-control-vote">
                                                        <a href="#"><i class="fa fa-star"></i></a>
                                                        <a href="#"><i class="fa fa-star"></i></a>
                                                        <a href="#"><i class="fa fa-star"></i></a>
                                                        <a href="#"><i class="fa fa-star"></i></a>
                                                        <a href="#"><i class="fa fa-star"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="review-form">
                                            <div class="single-form">
                                                <label>Nickname <sup>*</sup></label>
                                                    <input type="text" />
                                            </div>
                                            <div class="single-form single-form-2">
                                                <label>Summary <sup>*</sup></label>
                                                    <input type="text" />
                                            </div>
                                            <div class="single-form">
                                                <label>Review <sup>*</sup></label>
                                                    <textarea name="massage" cols="10" rows="4"></textarea>
                                            </div>
                                        </div>
                                        <div class="review-form-button">
                                            <a href="#">Submit Review</a>
                                        </div>
                                    </div>
                                </div>
                            </div>	
						</div>
						<!-- product-info-area-end -->
                        <div  class="col-md-12 col-lg-12 col-sm-12 col-xs-12 decorate"></div> 
                         <div class="col-md-12 col-lg-12" id="dv_ItemSummary" runat="server" >
                            <h3 class="book-heading">BOOK SUMMARY</h3>
                        </div>                          
                        <asp:Repeater ID="rp_book_details_tab" runat="server" >
                            <ItemTemplate> 
                                <div class="col-md-12 col-lg-12 book-summary" >
                                    <p>
                                        <asp:Label ID="lblAboutProduct" runat="server"  Text='<%# Eval("AboutProduct") %>' ToolTip='<%# Eval("AboutProduct") %>' /> 
                                    </p>
                                </div> 
                            </ItemTemplate>
                        </asp:Repeater> 
                        <div  class="col-md-12 col-lg-12 col-sm-12 col-xs-12 border-decorate" id="dv_decorateProduct" runat="server"></div>
                        <div class="col-md-12 col-lg-12" id="dv_ItemAboutAuthor" runat="server">
                            <h3 class="book-heading">ABOUT AUTHOR</h3>
                        </div>
                        <asp:Repeater ID="rp_book_details_tab1" runat="server">
                            <ItemTemplate>
                                <div class="col-md-12 col-lg-12 book-summary" >
                                    <p>
                                        <asp:Label ID="Label1" runat="server"  Text='<%# Eval("AboutAuthor") %>' ToolTip='<%# Eval("AboutAuthor") %>'  /> 
                                    </p>
                                </div> 
                            </ItemTemplate>
                        </asp:Repeater> 
                           <div class="col-md-12 col-lg-12 border-decorate" id="dv_decorateAuthor" runat="server" ></div>
                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                            <div class="col-md-1 col-lg-1"></div>
                            <div class="col-md-5 col-lg-5">
                                <div class="col-md-12 col-lg-12" style="padding-bottom:20px;margin-left:-14px;">
                                    <span class="review-heading">WRITE A REVIEW</span>
                                </div>
                                <div class=product-name1">
                                    <span>Product name:</span>
                                    <strong id="review_bookname" runat="server"></strong>
                                </div>
                                <div class="review-title">
                                    <span>Your Name:</span>
                                </div>
                                <div class="">                                    
                                    <asp:TextBox style="width:100%;border:1px solid lightgrey;height:40px" ID="txtName" runat="server" Width="100%" ></asp:TextBox>
                                </div>
                                <div class="review-title">
                                    <span>Review title:</span>
                                </div>
                                <div class="">
                                    <asp:TextBox ID="txtRvwHeading" style="width:100%;border:1px solid lightgrey;height:40px" runat="server" Width="100%"></asp:TextBox>
                                </div>
                                 <div class=" rate-star">
                                    <span>Your Ratings:</span>
                                </div>
                                 <div class=" star-icons">
                                          <div class="row">                                          
                                        <cc1:Rating ID="Rating1"  runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star" FilledStarCssClass="FilledStar"> </cc1:Rating>
                                              
                                         </div>
                                 </div>
                                
                                 <div class=" message">
                                    <span>Your Message:</span>
                                </div>
                                <div class="">                                    
                                    <asp:TextBox style="width: 100%;height: 115px;border: 1px solid lightgrey;" ID="txtBookReview" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="">                                    
                                    <asp:Button style="background-color: #162e2d;color: white;height: 40px; width: 120px;margin-top: 20px; border: none;" ID="btnPostReview" runat="server" Text="Submit Review" CssClass="success" OnClick="btn_post_review_Click" ></asp:Button>
                                </div>
                            </div>
                            <div class="col-md-5 col-lg-5" id="dvReview" runat="server">
                                <div class="col-md-12 col-lg-12">
                                    <span class="review-heading">BOOK REVIEWS</span>
                                </div>
                                <asp:Repeater ID="rp_reviews" runat="server" OnItemDataBound="CreateRatingStars">
                                                <ItemTemplate>
                                                    <div style="padding-top: 10px;" class="col-md-12 col-lg-12 star-icons">
                                                        <div class="rating-result" id="rating_result" runat="server"></div>
                                                        <%--<i style="color:orange;font-size:14px" class="fa fa-star"></i>
                                                        <i  style="color:orange;font-size:14px" class="fa fa-star"></i>
                                                        <i style="color:orange;font-size:14px" class="fa fa-star"></i>
                                                        <i  style="color:orange;font-size:14px" class="fa fa-star"></i>
                                                        <i  style="color:orange;font-size:14px" class="fa fa-star"></i>--%>
                                                    </div>
                                                    <div class="col-md-12 col-lg-12">
                                                        <span style="font-weight: 600;font-size: 15px;"><%# Eval("ReviewerHeading") %></span>
                                                    </div>
                                                    <div class="col-md-12 col-lg-12">
                                                        <span>By:<span style="color:deepskyblue"><%#Eval("CustName") %></span> </span>
                                                        <p class="review-date">Posted on : <span><%# Eval("CreatedOn") %></span></p>
                                                    </div>
                                                     <div style="padding-top: 5px;" class="col-md-12 col-lg-12">
                                                        <span><%# Eval("ReviewDesc") %> </span>
                                                    </div> 
                                                </ItemTemplate>
                                            </asp:Repeater> 
                            </div>
                            <div class="col-md-1 col-lg-1"></div>
                        </div>
                           

						<!-- new-book-area-start -->
						<div class="new-book-area mt-60" id="similarItem" runat="server">
							<div class="section-title text-center mb-30">
								<h3 style="width: 245px;margin: auto;border-bottom:2px solid #f59744;padding-top: 57px; display: inline-block;">SIMILAR PRODUCTS</h3>
							</div>
                            <div class="row">
                                <div class="similar_item_container" >
                                    <div class="tab-active-3 owl-carousel">
                                        <asp:Repeater ID="rpsimilarbooks" runat="server" OnItemCommand="RepeaterCommandArg"> 
                                            <ItemTemplate>
                                                <div class="">
                                                    <a class="sliderImage-wrapper" class="d-flex" href="view_book.aspx?productid=<%# Eval("ProductID") %>&title=<%# Eval("BookName") %>" title="<%# Eval("BookName") %>" id="ProductID_<%# Eval("ProductID") %>_<%# Container.ItemIndex + 1%>">
                                                    <div class="flip-card">                                                        
                                                        <div class="flip-card-inner">
                                                            <div class="flip-card-front">
                                                                <div class="slider-image-wrapper">
                                                                    <div class="<%# (Convert.ToInt32(Eval("ClosingBal"))<=0?"image-stock-out":"hidden") %>">Out of Stock</div>
                                                                    <img class="sliderImage owl-lazy" src="data:image/png;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs=" data-src="<%# Eval("ImgPath") %>" alt="<%# Eval("BookName") %>" title="<%# Eval("BookName") %>" onError="this.onerror = null; this.src = 'resources/no-image.jpg';"/>
                                                                    <%--<%# (Eval("SpecialDiscount").ToString() == "0"?"":"<span style='background:#ff4318;' class='new-item'>" + Eval("SpecialDiscount").ToString().Replace(".00","") + "% OFF</span>" )%>--%>
                                                                    <%# (Eval("SalePrice").ToString()==Eval("DiscountPrice").ToString()?"":"<span style='background:#ff4318;' class='new-item'>" + Eval("SpecialDiscount").ToString().Replace(".00","") + "% OFF</span>" )%>
                                                                    
                                                                </div>
                                                            </div>                                                       
                                                            <div class="flip-card-back">
                                                                <div class="flip-content">                                                
                                                                <p class="link">
                                                                    <b>
                                                                        <%# Eval("BookName") %>
                                                                    </b>
                                                                </p>
                                                                                    <%--<b><p style="margin-bottom:0px">By:</p></b>--%>
                                                                   <p style="font-style: italic;"> <%# Eval("Author") %></p>
                                                                                     <%--<b><p style="margin-bottom:0px" >Publisher: </p></b>--%>
                                                                    <p><%# Eval("Publisher") %></p>
                                                                </div>
                                                            </div>
                                                        </div>                                        
                                                    </div>
                                                    <a class="product_tile" href="view_book.aspx?productid=<%# Eval("ProductID") %>&title=<%# Eval("BookName") %>"><%# Eval("BookName") %></a>
                                                    <div style="margin-top: -4px;" class="star-rating">
                                                        <i class="fa fa-star"></i>
                                                        <i class="fa fa-star"></i>
                                                        <i class="fa fa-star"></i>
                                                        <i class="fa fa-star"></i>
                                                        <i class="fa fa-star"></i>
                                                    </div>
                                                    <a id="ProductIDas_<%# Eval("ProductID") %>" href="view_book.aspx?productid=<%# Eval("ProductID") %>&title=<%# Eval("BookName") %>" title="<%# Eval("BookName") %>" class="shopping-card"></a>
                                                    <h4 style="text-align:center;">
                                                            <div class="product-price d-flex" style="flex-direction:column;">
                                                                <ul >
                                                                <%# (Eval("SalePrice").ToString()==Eval("DiscountPrice").ToString() 
                                                                    ? "<li >" + Eval("SaleCurrency") + " " + Eval("SalePrice",CommonCode.AmountFormat()) + "<li>" 
                                                                    :"<li class=\"old-price\" style='color:#bcac9f !important;'>" + Eval("SaleCurrency") + " " 
                                                                    + Eval("SalePrice",CommonCode.AmountFormat()) + "</li><br/>" 
                                                                    + "<li >" + Eval("SaleCurrency") + " " + Eval("DiscountPrice",CommonCode.AmountFormat()) + "</li>"                                                                    
                                                                    )%>
                                 
                                                                </ul>
                                                                <div class="add-links-wrap-cover">
                                                                        <div class="add-links-wrap"> 
                                                            <a id="ProductID_--<%# Eval("ProductID") %>" href="view_book.aspx?productid=<%# Eval("ProductID") %>&title=<%# Eval("BookName") %>" title="<%# Eval("BookName") %>" class="shopping-card">
                                                                    <div class="show">
                                                                        <i class="fa fa-external-link"></i>
                                                                    </div>
                                                                </a> 
                                                             <a href="#"  ID="<%# Container.ItemIndex + 1%>" title="Add To Cart" onclick="return ModalEntry('<%#Eval("ProductID") %>');" 
                                                                    class='<%# (Convert.ToInt32(Eval("ClosingBal"))<=0?"btn btn-primary disabled addCartButton ":"btn btn-primary addCartButton") %>'>
                                                                    <i class="fa fa-shopping-cart hidden-xs"></i>
                                                                    <span class="">Add to Cart</span>
                                                                </a>
                                                                <a href="#" title="Add To Wishlist" ID="<%#Eval("ProductID") %>-<%# Container.ItemIndex + 1%>" onclick="return userwishlist('<%#Eval("ProductID") %>');" >
                                                                    <div class="add-to-wishlist">
                                                                        <i class="fa fa-heart "></i>
                                                                    </div>
                                                                </a>
                                                            </div>
                                                                    </div>
                                                            </div>
                                                        </h4>
                                                    </a>
                                                    </div> 
                                            </ItemTemplate>
                                        </asp:Repeater> 
							        </div>
                                </div>
                            </div>							
						</div>
						<!-- new-book-area-start -->	
					</div>
            </div>
			
           
		</div>

     <!-- Notify Me Modal Start -->
    <div id="modal_notify_me" class="modal" role="dialog" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 style="float:left;" class="modal-title">Notify me</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span></button>
                </div>
                <div class="modal-body">
                    <p style="font-size:16px !important;font-weight: 800;font-family: Arial, Helvetica, sans-serif !important;">Fill up your details to notify you when this book will be available</p>
                    <div class="form-horizontal">
                        <div class="form-group init-validator">
                            <label style="padding-top: 23px;" class="control-label col-xs-4">Name</label>
                            <div class="col-xs-7">       
                                <asp:RequiredFieldValidator ControlToValidate="txtNameNotify" runat="server" ValidationGroup="notifyme_details" CssClass="validator"  ID="RequiredFieldValidator15" ErrorMessage="***Required" />                         
                                <asp:TextBox ID="txtNameNotify" ValidationGroup="notifyme_details" runat="server" autocomplete="off" class="form-control" placeholder="Type Your Name......."></asp:TextBox>
                            </div>
                        </div>

                        <div style="margin-top: -10px;" class="form-group init-validator">
                            <label style="padding-top: 23px;" class="control-label col-xs-4">Email</label>
                            <div class="col-xs-7">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="#cc3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="validator"
                                    ValidationGroup="notifyme_details" ControlToValidate="textEmail" runat="server" ErrorMessage="Invalid Email ID"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="textEmail" CssClass="validator"  ValidationGroup="notifyme_details" ErrorMessage="***Required"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="textEmail" runat="server" autocomplete="off" class="form-control" placeholder="Email ID"  ValidationGroup="notifyme_details" ></asp:TextBox>
                            </div>
                        </div>

                        <div style="margin-top: 28px;" class="form-group init-validator ">
                            <label class="control-label col-xs-4">Phone No</label>
                            <div class="col-xs-7">
                                <div class="input-group">
                                    <span class="input-group-btn" style="width: 18%;display:none;">
                                        <asp:DropDownList ID="dd_country_code" Style="padding-left: 0;height: 34px;padding-bottom: 0px;" data-toggle="tooltip" title="Select Country Code" class="form-control" runat="server"> 
                                            <asp:ListItem Selected="True">+91</asp:ListItem> </asp:DropDownList>
                                    </span>
                                    <asp:TextBox ID="textPhone" runat="server" autocomplete="off" class="form-control"  ValidationGroup="notifyme_details" title="Please enter valid Mobile No." 
                                        placeholder="Mobile No" pattern="^[0-9]{1,10}$" ></asp:TextBox>
                                </div>
                                <!-- /input-group -->
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="validator" ValidationGroup="notifyme_details" runat="server" 
                                    ValidationExpression="[0-9]{10}" ControlToValidate="textPhone" ErrorMessage="Invalid Mobile No"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ControlToValidate="textPhone" runat="server" ValidationGroup="notifyme_details" CssClass="validator" ID="RequiredFieldValidator3" ErrorMessage="***Required" />
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <div class="col-sm-offset-4 col-sm-10">
                                <asp:Button ID="btn_notifyme" runat="server"  Text="Notify Me" class="btn btn-default notify-btn" OnClick="btn_notifyme_Click" ValidationGroup="notifyme_details" />
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <button ID="btn_notifymeClose" type="button" class="btn btn-default notify-btn" data-dismiss="modal">Close</button>
                            </div>
                            
                        </div>
                    </div>
                </div>
                <%--<div class="modal-footer">
                    
                </div>--%>
            </div>

        </div>
    </div>
     <!-- Notify Me Modal End-->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">


    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>
