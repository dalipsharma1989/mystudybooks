<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SchoolMaster.master"  CodeFile="Classes.aspx.cs" Inherits="Classes" %>

 <asp:Content ID="content1" runat="server" ContentPlaceHolderID="head">
     <!-- Font -->
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;700&display=swap" rel="stylesheet">

    <!-- CSS Implementing Plugins -->
    <link rel="stylesheet" href="/school/assets/vendor/font-awesome/css/fontawesome-all.min.css">
    <link rel="stylesheet" href="/school/assets/vendor/flaticon/font/flaticon.css">
    <link rel="stylesheet" href="/school/assets/vendor/animate.css/animate.css">
    <link rel="stylesheet" href="/school/assets/vendor/bootstrap-select/dist/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="/school/assets/vendor/slick-carousel/slick/slick.css"/>
    <link rel="stylesheet" href="/school/assets/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css"> 
    <link rel="stylesheet" href="/school/assets/css/theme.css">
    <style>
            .headtext{
                letter-spacing: 0px;
                text-transform: uppercase;
                font-size: 62px;
                color: #0f2248;
                text-align: center;
                font-style: inherit;
                font-weight: bold; 
                padding-bottom: 40px;
                width:100%;
                border-bottom: 5px solid grey;
            }
    </style>

</asp:Content>
<asp:Content ID="content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div id="site-header" class="site-header__v5">
    <section class="space-bottom-3">
        <div class="space-3">
            <div class="container">
                <header class="d-md-flex justify-content-between align-items-center mb-5">
                    <h2 class="font-size-26 mb-4 mb-md-0 headtext">Select Your Class</h2> 
                </header>
                <div class="js-slick-carousel products list-unstyled u-slick--gutters-3 my-0" data-arrows-classes="d-none d-lg-block u-slick__arrow u-slick__arrow-centered--y"
                            data-arrow-left-classes="flaticon-back u-slick__arrow-inner u-slick__arrow-inner--left ml-lg-n9" data-arrow-right-classes="flaticon-next u-slick__arrow-inner u-slick__arrow-inner--right mr-lg-n9"
                            data-slides-show="5" data-responsive='[{ "breakpoint": 1500, "settings": { "slidesToShow": 4 } }, { "breakpoint": 1199, "settings": { "slidesToShow": 3 }
                            }, { "breakpoint": 554, "settings": { "slidesToShow": 2 } }]'
                     style="display: flex;flex-wrap: wrap;justify-content: center;"> 
                        <asp:Repeater ID="rp_class" runat="server">
                            <ItemTemplate>
                        
                                    <div class="product border product__space bg-white" style="width:270px; margin:10px 10px;">
                                        <div class="product__inner overflow-hidden p-3 p-md-4d875">
                                            <div class="woocommerce-LoopProduct-link woocommerce-loop-product__link d-block position-relative">
                                                <div class="woocommerce-loop-product__thumbnail">
                                                    <a href="#" class="d-block">
                                                        <img src="../resources/no-image.png" style="width:150px;"  class="d-block mx-auto attachment-shop_catalog size-shop_catalog wp-post-image img-fluid" alt="image-description"></a>
                                                </div>
                                                <div class="woocommerce-loop-product__body product__body pt-3 bg-white">
                                                    <div class="text-uppercase font-size-1 mb-1 ">
                                                        <a style="font-size:14px;font-weight:900;" href="Classes.aspx?SchoolID=<%# Eval("CustomerCode") %>"><%# Eval("CustomerName") %></a> 
                                                    </div> 
                                                </div> 
                                            </div>
                                        </div>
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater> 
                </div>  
            </div>
        </div>
    </section> 
</div> 
</asp:Content>
<asp:Content ID="content3" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="/school/assets/vendor/jquery/dist/jquery.min.js"></script>
    <script src="/school/assets/vendor/jquery-migrate/dist/jquery-migrate.min.js"></script>
    <script src="/school/assets/vendor/popper.js/dist/umd/popper.min.js"></script>
    <script src="/school/assets/vendor/bootstrap/bootstrap.min.js"></script>
    <script src="/school/assets/vendor/bootstrap-select/dist/js/bootstrap-select.min.js"></script>
    <script src="/school/assets/vendor/slick-carousel/slick/slick.min.js"></script>
    <script src="/school/assets/vendor/multilevel-sliding-mobile-menu/dist/jquery.zeynep.js"></script>
    <script src="/school/assets/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js"></script>


     <!-- JS HS Components -->
    <script src="/school/assets/js/hs.core.js"></script>
    <script src="/school/assets/js/components/hs.unfold.js"></script>
    <script src="/school/assets/js/components/hs.malihu-scrollbar.js"></script>
    <script src="/school/assets/js/components/hs.header.js"></script>
    <script src="/school/assets/js/components/hs.slick-carousel.js"></script>
    <script src="/school/assets/js/components/hs.selectpicker.js"></script>
    <script src="/school/assets/js/components/hs.show-animation.js"></script>

    <!-- JS Bookworm -->
    <!-- <script src="/school/assets/js/bookworm.js"></script> -->
    <script type="text/javascript">
         $(document).on('ready', function () {
             // initialization of unfold component
             $.HSCore.components.HSUnfold.init($('[data-unfold-target]'));

             // initialization of slick carousel
             $.HSCore.components.HSSlickCarousel.init('.js-slick-carousel');

             // initialization of header
             $.HSCore.components.HSHeader.init($('#header'));

             // initialization of malihu scrollbar
             $.HSCore.components.HSMalihuScrollBar.init($('.js-scrollbar'));

             // initialization of show animations
             $.HSCore.components.HSShowAnimation.init('.js-animation-link');

             // init zeynepjs
             var zeynep = $('.zeynep').zeynep({
                 onClosed: function () {
                     // enable main wrapper element clicks on any its children element
                     $("body main").attr("style", "");

                     console.log('the side menu is closed.');
                 },
                 onOpened: function () {
                     // disable main wrapper element clicks on any its children element
                     $("body main").attr("style", "pointer-events: none;");

                     console.log('the side menu is opened.');
                 }
             });

             // handle zeynep overlay click
             $(".zeynep-overlay").click(function () {
                 zeynep.close();
             });

             // open side menu if the button is clicked
             $(".cat-menu").click(function () {
                 if ($("html").hasClass("zeynep-opened")) {
                     zeynep.close();
                 } else {
                     zeynep.open();
                 }
             });
         });
    </script>
</asp:Content>