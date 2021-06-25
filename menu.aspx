<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="_menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>

        .alert-warning {
            width:100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
    <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area"> 
				<div class="row">
					<div class="col-lg-12">
						<div class="breadcrumbs-menu">
							<ul>
								<li><a href="/">Home</a></li>
								<li><a href="#" class="active" id="tagName" runat="server">Menu Name</a></li>
							</ul>
						</div>
					</div>
				</div> 
		</div>
		<!-- breadcrumbs-area-end -->
		<!-- entry-header-area-start -->
		<div class="entry-header-area" style="padding: 0;">
			 
				<div class="row">
					<div class="col-lg-12"> 
						<div class="entry-header-title" style="text-align: center;">
							<h2><%:HeaderContent %></h2>
						</div>
					</div>
				</div>
			 
		</div>
		<!-- entry-header-area-end -->
    
        <hr/>

    <div class="user-login-area mb-70">
        
            <div class="row"> 
                <asp:PlaceHolder ID="ph_msg" runat="server" ></asp:PlaceHolder> 
                <div class="col-sm-12" id="menutype_page" runat="server">
                    <asp:Repeater ID="rp_page" runat="server">
                        <ItemTemplate>
                            <%--<p class="text-justify">--%>
                                <%--<%# (Eval("HeaderContent").ToString().ToLower() == "contact us")  ?
                                    "<Div class='col-sm-6'><p class='text-justify'>"+Server.HtmlDecode(Eval("MainContent").ToString().Replace("{", "").Replace("}", "").Split('|')[1]) + "</p></Div>"+
                                    "<Div class='col-sm-6'><div id='ROADMAP' class='gmap full m-b-3' ><iframe src='https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3501.5856633575772!2d77.24058831508282!3d28.642177682414037!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x390cfcd8a4430267%3A0xd3d61a0d07b27ec3!2sSS+Publisher+and+Distributor+Pvt.+Ltd!5e0!3m2!1sen!2sin!4v1563522478212!5m2!1sen!2sin' style='width: 100%; height: 400px !important;'></iframe></div></Div>"   :
                                    "<Div class='col-sm-12'><p class='text-justify'>"+ Server.HtmlDecode(Eval("MainContent").ToString().Replace("{", "").Replace("}", "").Split('|')[1])+"</p></Div>"
                                %>--%>
                            
                                <%--<%# Server.HtmlDecode(Eval("MainContent").ToString().Replace("{", "").Replace("}", "").Split('|')[1]) %>--%>
                            <%--</p>--%>
                            <p class="text-justify">
                                <%# Server.HtmlDecode(Eval("MainContent").ToString().Replace("{", "").Replace("}", "").Split('|')[1]) %>
                               <%--<%# Eval("MainContent") %>--%>
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="col-sm-12" id="menutype_grid" runat="server">
                    <ul class="list-unstyled row grid book-carousel">
                        <asp:Repeater ID="rp_products" runat="server">
                            <ItemTemplate>
                                <li class="col-sm-2">
                                    <div id="product-<%# Eval("ProductID") %>" class="grid-view">
                                        <div class="product-image">
                                            <a id="listItem-image-link_<%# Eval("ProductID") %>" class="product-image-link" href="view_book.aspx?productid=<%# Eval("ProductID") %>">
                                                <span class="picture-overlay picture-overlay-hidden"></span>
                                                <img class="photo" src="<%# Eval("ProductImage") %>"  onError="this.onerror=null;this.src='resources/no-image.jpg';" alt="<%# Eval("BookName") %>">
                                            </a>
                                        </div>
                                        <div class="book-details">
                                            <div class="book-title" data-toggle="tooltip" title="<%# Eval("BookName") %> @<%# Eval("SaleCurrency") %> <%# Eval("SalePrice",CommonCode .AmountFormat()) %>">
                                                <a href="view_book.aspx?productid=<%# Eval("ProductID") %>"><%# Eval("BookName") %></a>
                                            </div>
                                            <div class="book-author"><a href="search_results.aspx?author=<%# Eval("Author") %>"><%# Eval("Author") %></a></div>
                                            <%# (string.IsNullOrEmpty(Eval("DiscountPrice").ToString())?"<div class='book-price'><span class='price'>"+Eval("SaleCurrency")+" "+Eval("SalePrice",CommonCode.AmountFormat())+
                                                    "</span></div>":"<div class='book-price-discount'><span class='price main-price'>"+
                                                    Eval("SaleCurrency")+
                                                    " "+Eval("SalePrice",CommonCode.AmountFormat())+"</span></div><div>"+
                                                    Eval("DiscountStatus")+"</div>") %>
                                            <%# (string.IsNullOrEmpty(Eval("DiscountPrice").ToString())?"":"<div class='book-price-discount'><span class='price discount-price'>"+Eval("SaleCurrency")+" "+Eval("DiscountPrice",CommonCode.AmountFormat())+"</span></div>") %>
                                    
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

