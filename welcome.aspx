<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="welcome.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/customecss/style.min.css" rel="stylesheet" />
    <link href="css/customecss/homeDCbooks.min.css" rel="stylesheet" />
   <link href="css/customecss/homrPage.min.css" rel="stylesheet" /> 
    <link href="css/owl.carousel.css" rel="stylesheet" />
    <link href="css/owl.theme.default.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="jumbotron text-center" style="height:150px;width:100%;padding-top: 15px;">
            <h1 style="font-size:45px;">Welcome <%=CustName %></h1>
            <p>to <%=CommonCode.CompanyName%></p>
        </div>
     
        <div class="col-xs-12 jumbotron text-center" style="margin-top: -30px;" >
            <asp:Literal ID="ltr_msg" runat="server" ></asp:Literal>
        </div>
    </div>
    <div class="row" style="display:none;">
        <div class="col-sm-6 col-xs-6" style="padding-right: 4px; padding-left: 0px;">
            <div style="border-top: 5px solid #ecb432; border-bottom: 5px solid #ecb432">
                <div style="padding: 8px 10px;">
                    BEST BOOKS
                </div>
                <div class="row">
                    <div class="owl-inner-demo book-carousel">
                        <asp:Repeater ID="rp_books1" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <div id="product-<%# Eval("ProductID") %>" class="grid-view">
                                        <div class="product-image">
                                            <a id="listItem-image-link_<%# Eval("ProductID") %>" class="product-image-link" href="view_book.aspx?productid=<%# Eval("ProductID") %>">
                                                <span class="picture-overlay picture-overlay-hidden"></span>
                                                <img class="photo" src="<%# Eval("ImagePath") %>" alt="<%# Eval("BookName") %>">
                                            </a>
                                        </div>
                                        <div class="book-details">
                                            <div class="book-title" data-toggle="tooltip" title="<%# Eval("BookName") %> @<%# Eval("SaleCurrency") %> <%# Eval("SalePrice",CommonCode .AmountFormat()) %>">
                                                <a href="view_book.aspx?productid=<%# Eval("ProductID") %>"><%# Eval("BookName") %></a>
                                            </div>
                                            <div class="book-author"><a href="search_results.aspx?author=<%# Eval("Author") %>"><%# Eval("Author") %></a></div>
                                            <div class="book-price"><span class="price"><%# Eval("SaleCurrency") %><%# Eval("SalePrice",CommonCode .AmountFormat()) %></span></div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </div>

        </div>

        <div class="col-sm-6 col-xs-6" style="padding-left: 16px; padding-right: 0px;">
            <div style="border-top: 5px solid #41bb20; border-bottom: 5px solid #41bb20;">
                <div style="padding: 8px 10px;">
                    BEST SELLING
                </div>
                <div class="row">
                    <div class="owl-inner-demo book-carousel">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <div id="product-<%# Eval("ProductID") %>" class="grid-view">
                                        <div class="product-image">
                                            <a id="listItem-image-link_<%# Eval("ProductID") %>" class="product-image-link" href="view_book.aspx?productid=<%# Eval("ProductID") %>">
                                                <span class="picture-overlay picture-overlay-hidden"></span>
                                                <img class="photo" src="<%# Eval("ImagePath") %>" alt="<%# Eval("BookName") %>">
                                            </a>
                                        </div>
                                        <div class="book-details">
                                            <div class="book-title" data-toggle="tooltip" title="<%# Eval("BookName") %> @<%# Eval("SaleCurrency") %> <%# Eval("SalePrice",CommonCode .AmountFormat()) %>">
                                                <a href="view_book.aspx?productid=<%# Eval("ProductID") %>"><%# Eval("BookName") %></a>
                                            </div>
                                            <div class="book-author"><a href="search_results.aspx?author=<%# Eval("Author") %>"><%# Eval("Author") %></a></div>
                                            <div class="book-price"><span class="price"><%# Eval("SaleCurrency") %><%# Eval("SalePrice",CommonCode .AmountFormat()) %></span></div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater> 
                    </div>
                </div>
            </div>

        </div>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_script" runat="server"></asp:Literal>
    <script src="js/owl.carousel.min.js"></script>
    <script>
        $(document).ready(function () {

            $(".owl-demo").owlCarousel({

                autoplay: true, //Set AutoPlay to 2 seconds
                autoplayTimeout: 2500,
                items: 1,
                nav: true,
                navText: ['<i class="fa fa-arrow-left"></i>', '<i class="fa fa-arrow-right"></i>'],
                dots: true,
                loop: true

            });

            $(".owl-inner-demo").owlCarousel({

                autoplay: true, //Set AutoPlay to 2 seconds
                autoplayTimeout: 2500,
                items: 3,
                nav: true,
                navText: ['<i class="fa fa-chevron-left"></i>', '<i class="fa fa-chevron-right"></i>'],
                dots: false,
                loop: true

            });
        });
    </script>
</asp:Content>
