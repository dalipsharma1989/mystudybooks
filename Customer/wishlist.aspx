<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.master" AutoEventWireup="true" CodeFile="wishlist.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area"> 
				<div class="row">
					<div class="col-lg-12">
						<div class="breadcrumbs-menu">
							<ul>
								<li><a href="/">Home</a></li>
								<li><a href="#" class="active">Wishlist</a></li>
							</ul>
						</div>
					</div>
				</div> 
		</div>
    <style type="text/css">
		.facebook i{
			color:#3b5999;
	      font-size: 22px;
		}
		.twitter i{
			color: #55acee;
			 font-size: 22px;
		}
		.pinterest i{
			color: #bd081c;
			 font-size: 22px;
		}
		.googleplus i{
			color:#dd4b39;
			 font-size: 22px;
		}
		.email i{
			color:#e4405f;
			 font-size: 22px;
		}
	
		.product-name a:hover{
			color:#3051a0;
		}
		.remove-item:hover{
            box-shadow: 0px 0px 10px 0px lightgrey !important;
            background:#162e2d !important;
            color:white !important;
		}
		.wishlist:hover{
             box-shadow: 0px 0px 10px 0px lightgrey;             
		}
        .wishlist-table {
			padding:10px;
        }
    </style>
		<!-- breadcrumbs-area-end -->
		<!-- entry-header-area-start -->
		<div class="entry-header-area">
			 
				<div class="row">
					<div class="col-lg-12">
						<div style="text-align:center" class="entry-header-title">
							<h2 class="wishlist-heading">Wishlist</h2>
						</div>
					</div>
				</div> 
		</div>
		<!-- entry-header-area-end -->

        <!-- cart-main-area-start -->
		<div class="cart-main-area mb-70">
            <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
            <asp:HiddenField ID="hf_wishlist" runat="server" /> 
				<div class="row">
					<div class="col-lg-12">
						<div class="wishlist-content" id="div_user_wishlist" runat="server">
                            <div class="wishlist-table table-responsive">
								<table class="table table-hover" style="    border: none; box-shadow: 0px 0px 5px #dcdcdc">
									<thead>
										<tr>
											<th class="product-remove" style="text-align:left !important;vertical-align:middle;">
												<span class="nobr">Remove</span>
											</th>
											<th class="product-thumbnail" style="text-align:left !important;vertical-align:middle;">Image</th>
											<th class="product-name" style="text-align:left !important;vertical-align:middle;">Product Name</th>
											<th class="product-price" style="text-align:right !important;vertical-align:middle;">Unit Price </th>
											<th class="product-stock-stauts" style="text-align:left !important;vertical-align:middle;">Stock Status </th>
											<th class="product-subtotal" style="text-align:left !important;vertical-align:middle;">Add To Cart </th>
										</tr>
									</thead>
									<tbody>
                                        <asp:Repeater ID="rp_wishlist" runat="server" OnItemCommand="rp_wishlist_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
												    <td class="product-remove" style="vertical-align:middle;">
                                                        <asp:HiddenField ID="hf_productID" runat="server" Value='<%# Eval("ProductID") %>' />
                                                        <asp:LinkButton ID="lb_remove_item_from_wishlist" CommandName="remove_item_from_wishlist" CommandArgument='<%# Eval("ProductID") %>' CausesValidation="false" runat="server">
                                                                <i style="color: red;" class="fa fa-times-circle"></i>
                                                        </asp:LinkButton>
                                                    <td class="product-thumbnail"  style="vertical-align:middle;">
                                                        <a id="listItem-image-link_<%# Eval("ProductID") %>" href="../view_book.aspx?productid=<%# Eval("ProductID") %>&title=<%# Eval("BookName") %>" title="<%# Eval("BookName") %>" >
                                                            <img src="<%# Eval("ImgPath") %>"  style="height: 100px;"  onError="this.onerror=null;this.src='/resources/no-image.jpg';"  alt="<%# Eval("BookName") %>">
                                                        </a>   
                                                    </td>
                                                    <td class="product-name"  style="vertical-align:middle;">
                                                        <a style="color: #162e2d;font-weight:600;" href="../view_book.aspx?productid=<%# Eval("ProductId") %>&title=<%# Eval("BookName") %>"><%# Eval("BookName") %> <a>
                                                    </td>
                                                    <td  style="color: #162e2d;font-weight:600;vertical-align:middle;text-align: right !important;"  class="product-price"><%# Eval("SaleCurrency") %> <%# Eval("SalePrice","{0:0.000}") %></td>
                                                    <td class="product-stock-status"  style="vertical-align:middle;">
                                                        <%--<%# (Convert.ToInt32(Eval("ClBal").ToString())<1?"<span class=\"wishlist-out-of-stock\">"+
                                                                "Out of Stock"+
                                                                "</span>":"<span class=\"wishlist-in-stock\">"+
                                                                "In Stock"+"</span>") %>--%>

                                                        <%# ( Eval("ClosingBalStatus").ToString() == "Out of Stock" ?"<span class=\"wishlist-out-of-stock\">" + "Out of Stock"+ "</span>"
                                                                                                         :"<span class=\"wishlist-in-stock\">" + "In Stock"+"</span>") %> 
                                                    </td>
                                                    <td class="product-add-to-cart" style="vertical-align:middle;">                                                        
                                                        <asp:LinkButton ID="lb_add_to_wishlist" CommandName="addtoCart" CommandArgument='<%# Eval("ProductID") %>' 
                                                            class='<%# (Eval("ClosingBalStatus").ToString() == "Out of Stock"?"btn btn-success btn-primary disabled":"remove-item btn btn-primary btn-success") %>' CausesValidation="false"
                                                            ToolTip='<%# (Eval("ClosingBalStatus").ToString() == "Out of Stock"?"Out of Stock":"Add To Cart") %>'
                                                            runat="server"><i  class="fa fa-shopping-cart"> </i>&nbsp;
                                                                <%# (Eval("ClosingBalStatus").ToString() == "Out of Stock"?"Out of Stock":"Add To Cart") %></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td colspan="2" style="text-align:center;vertical-align:middle;">
                                                <div class="clear-wishlist">
                                                    <asp:Button ID="btn_clear_wishlist" runat="server" style="width: 166px;" CssClass="btn btn-primary" Text="Clear Wishlist" OnClick="btn_clear_wishlist_Click" />
                                                </div>
                                            </td>
                                            <td colspan="3" class="add-to-cart" style="text-align:center;border-bottom:none;display:none;"><div>
                                                <asp:Button ID="btn_add_to_wishlist" runat="server" CssClass="wishlist" Text="Add all to Cart" />
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                   <%-- <tfoot>
											<tr>
												<td colspan="6">
													<div class="wishlist-share">
														<h4 class="wishlist-share-title">Share on:</h4>
														<ul>
															<li><a href="#" class="facebook"><i class="fa fa-facebook"></i></a></li>
															<li><a class="twitter" href="#"><i class="fa fa-twitter"></i></a></li>
															<li><a class="pinterest" href="#"><i class="fa fa-dribbble"></i></a></li>
															<li><a class="googleplus" href="#"><i class="fa fa-google-plus"></i></a></li>
															<li><a class="email" href="#"><i class="fa fa-instagram"></i></a></li>
														</ul>
													</div>
												</td>
											</tr>
										</tfoot>--%>
								</table>
							</div>
						</div>
					</div>
				</div>
			 
		</div>
		<!-- cart-main-area-end -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>