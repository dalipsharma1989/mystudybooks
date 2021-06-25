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
    <style type="text/css">
            .headtext{
                letter-spacing: 0px;
                text-transform: uppercase;
                font-size: 40px;
                color: #0f2248;
                text-align: center;
                font-style: inherit;
                font-weight: bold; 
                padding: 20px 20px;
                width:100%;
                border-bottom: 5px solid grey;
            }
            .text-height-2{
                height:auto !important;
            }
            .buttonCSS{
                height:100% !important;
                white-space: pre-wrap !important; 
                display:-webkit-box;
            }
            .btn-primary{
                background-color: #d49547 !important;
                border-color: #d49547 !important;
            }

            
            @media only screen and (max-width:767px){
                .headtext{
                    font-size: 20px;
                }
            }

    </style>

</asp:Content>
<asp:Content ID="content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <!-- breadcrumbs-area-start -->
        <div class="breadcrumbs-area"> 
	        <div class="row">
		        <div class="col-lg-12">
			        <div class="breadcrumbs-menu">
				        <ul>
					        <li><a href="/">Home</a></li>
					        <li><a href="/school/school.aspx">School</a></li>
					        <li><a href="#" class="active">Class Selection</a></li>
				        </ul>
			        </div>
		        </div>
	        </div> 
        </div>
		<!-- breadcrumbs-area-end -->
<div id="site-header" class="site-header__v5">
    <section class="space-bottom-3">
        <div class="space-3"  style="padding:0px !important;">
            <div class="container">
                <header class="d-md-flex justify-content-between align-items-center mb-5"  style="box-shadow: 10px 10px 10px 10px #f48221ab;font-family: cursive;">
                    <h2 class="font-size-26 mb-4 mb-md-0 headtext">Select Your Class</h2> 
                    <asp:HiddenField ID="Hf_schoolCode" runat="server" />
                </header>
                <div class="js-slick-carousel products list-unstyled u-slick--gutters-3 my-0" data-arrows-classes="d-none d-lg-block u-slick__arrow u-slick__arrow-centered--y"
                            data-arrow-left-classes="flaticon-back u-slick__arrow-inner u-slick__arrow-inner--left ml-lg-n9" data-arrow-right-classes="flaticon-next u-slick__arrow-inner u-slick__arrow-inner--right mr-lg-n9"
                            data-slides-show="5" data-responsive='[{ "breakpoint": 1500, "settings": { "slidesToShow": 4 } }, { "breakpoint": 1199, "settings": { "slidesToShow": 3 }
                            }, { "breakpoint": 554, "settings": { "slidesToShow": 2 } }]'
                     style="display: flex;flex-wrap: wrap;justify-content: center;"> 
                    <asp:Repeater ID="rp_class" runat="server"  OnItemDataBound="rp_class_ItemDataBound"  >
                        <ItemTemplate> 
                            <div class="product border product__space bg-white" style="width:270px; margin:10px 10px;box-shadow: 0px 0px 10px 10px grey;border-radius:25px;height:fit-content;">
                                <div class="product__inner overflow-hidden p-3 p-md-4d875">
                                    <div class="woocommerce-LoopProduct-link woocommerce-loop-product__link d-block position-relative">
                                        <div class="woocommerce-loop-product__thumbnail">
                                            <asp:HiddenField ID="hf_HiddenClass" runat="server" Value='<%# Eval("ClassCode") %>' /> 
                                            <a href="#" class="d-block">
                                                <img src="../resources/ClassLogo/<%# Eval("SchoolID") %>-<%# Eval("ClassCode") %>.jpg" style="width:150px;"   onError="this.onerror = null; this.src = '../resources/no-image.png';"
                                                    class="d-block mx-auto attachment-shop_catalog size-shop_catalog wp-post-image img-fluid" alt="<%# Eval("ClassName") %>"> 
                                            </a>
                                        </div>
                                        <div class="woocommerce-loop-product__body product__body pt-3 bg-white">
                                            <div class="text-uppercase font-size-1 mb-1 ">
                                                <a style="font-size:14px;font-weight:900;" href="#">
                                                    <%# Eval("ClassName") %> 
                                                </a> 
                                            </div>
                                            <div class="text-uppercase font-size-1 mb-1 ">
                                                <h6 style="font-size:14px;font-weight:900;">Select Set / Language</h6>
                                            </div>
                                            <asp:Repeater ID="rp_Sets" runat="server">
                                                <ItemTemplate>
                                                    <h2 class="woocommerce-loop-product__title product__title h6 text-lh-md mb-1 text-height-2 crop-text-2 h-dark">
                                                        <a class="btn btn-primary buttonCSS" href="SetLanguage.aspx?School=<%# Eval("SchoolID") %>&Class=<%# Eval("ClassID") %>&SetId=<%# Eval("BundleCode") %>&SetName=<%# Eval("BundleDesc") %>">
                                                            <i class="fa fa-book-reader"></i>&nbsp; <%# Eval("BundleDesc") %> 
                                                        </a>  
                                                    </h2>  
                                                </ItemTemplate>
                                            </asp:Repeater> 
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
         //$(document).on('ready', function () {
         //    // initialization of unfold component
         //    $.HSCore.components.HSUnfold.init($('[data-unfold-target]'));

         //    // initialization of slick carousel
         //    $.HSCore.components.HSSlickCarousel.init('.js-slick-carousel');

         //    // initialization of header
         //    $.HSCore.components.HSHeader.init($('#header'));

         //    // initialization of malihu scrollbar
         //    $.HSCore.components.HSMalihuScrollBar.init($('.js-scrollbar'));

         //    // initialization of show animations
         //    $.HSCore.components.HSShowAnimation.init('.js-animation-link');

         //    // init zeynepjs
         //    var zeynep = $('.zeynep').zeynep({
         //        onClosed: function () {
         //            // enable main wrapper element clicks on any its children element
         //            $("body main").attr("style", "");

         //            console.log('the side menu is closed.');
         //        },
         //        onOpened: function () {
         //            // disable main wrapper element clicks on any its children element
         //            $("body main").attr("style", "pointer-events: none;");

         //            console.log('the side menu is opened.');
         //        }
         //    });

         //    // handle zeynep overlay click
         //    $(".zeynep-overlay").click(function () {
         //        zeynep.close();
         //    });

         //    // open side menu if the button is clicked
         //    $(".cat-menu").click(function () {
         //        if ($("html").hasClass("zeynep-opened")) {
         //            zeynep.close();
         //        } else {
         //            zeynep.open();
         //        }
         //    });
         //});
    </script>
</asp:Content>