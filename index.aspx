<%@ Page EnableEventValidation="false" Title="Home Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="index.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <link href="css/customecss/style.min.css" rel="stylesheet" />
    <link href="css/customecss/homeDCbooks.min.css" rel="stylesheet" />
   <link href="css/customecss/homrPage.min.css" rel="stylesheet" />  
        <style type="text/css"> 
            .owl-carousel .owl-item .owl-lazy {
                opacity: 1;
            } 
            .image-banner {
                width: auto;
                height: 390px;
            }
            @media only screen and (max-width:990px){
                .image-banner {
                   width:auto;
                   height:auto;
                }
            } 

            .school{
                height: 250px !important;
                max-height: 250px !important;
                background: transparent !important;
                padding-top: 75px !important;
                padding-bottom: 75px !important;
                background-image: url(/resources/banners/school.jpg) !important;
                background-repeat: no-repeat !important;
                background-size: 100% 100% !important;
                padding-left:  65px !important;
                padding-right: 65px !important;
            }

            @media only screen and (max-width:580px) {
                .school{
                    padding-left: 30px !important;
                    padding-right: 0px !important;
                    
                }
                .school h6{
                    font-size:20px !important;
                } 

                #hMore7{
                    width:80% !important;
                }
                #hMore6{
                    width:80% !important;
                }
                #hMore5{
                    width:80% !important;
                }
                #hMore4{
                    width:80% !important;
                }
                #hMore3{
                    width:80% !important;
                }
                #hMore2{
                    width:80% !important;
                }
                #hMore1{
                    width:80% !important;
                }
                #hMoreOffer{
                    width:80% !important;
                }
            }

        </style> 
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding:0px">
            <!-- slider-area-start -->
      <div class="slideshow-container slider-area">
         <div  id="TopHeaderSliderDiv"></div>
      </div>
            <!-- slider-area-end -->
   </div>
<div id="InDexMainBlock" style="opacity:0;"> 
  <label runat="server" id="ltr" ></label>
    <div id="SecondBlockAftermainSlider" style="opacity:0;">
        <div class="feature-box-wrappee">
            <div class="row">
             <div class="col-lg-12 col-md-12 col-sm-12">
                <div style="margin-top: 16px; text-align: left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
                   <h3 style="width:93%;" ><span id="Span1" runat="server">Shop by Categories</span></h3>               
                </div>
             </div>
          </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="container"  id="BooksCategoriesItems"></div> 
                </div>
            </div>   
        </div>
        
        <div class="feature-box-wrappee" style="display:none;">
            <div class="row">
             <div class="col-lg-12 col-md-12 col-sm-12">
                <div style="margin-top: 16px; text-align: left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
                   <h3 style="width:93%;" ><span id="Span3" runat="server">Shop by Subjects</span></h3>               
                </div>
             </div>
          </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="container"  id="BooksCategoriesSubject"></div> 
                </div>
            </div>   
        </div>   

        <div id="dv_SpecialOffer" runat="server">
          <div class="row">
             <div class="col-lg-12 col-md-12 col-sm-12">
                <div style="margin-top: 16px; text-align: left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
                   <h3   style="width:95%;" id="hMoreOffer"><span id="Span2" runat="server">Special Offers</span></h3>
                    <a href="more_Items.aspx" id="a_More_itemSpecial_Offer" class="more-btn">More&nbsp;<i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i></a>
                  <%-- <asp:button Text="More" class="more-btn" UseSubmitBehavior="false" causesvalidation="false" style="display:none;" id="Button1" runat="server" onclick="btnSearchMore1_Click"></asp:button>
                   <i style="margin-top: 10px;margin-left: -5px;display:none;" class="fa fa-chevron-right"></i>--%>
                </div>
             </div>
          </div>
          <div class="row" >
             <div class="col-lg-12 no-background-slider home-slider" id="dv_sliderOffers">       
                 <div id="spl_Offers"></div>               
             </div>
          </div> 
       </div>
       
   <div id="dv_Slide1" runat="server">
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12">
            <div style="margin-top: 16px; text-align: left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
               <h3   style="width:95%;"id="hMore1"><span id="slider1_name" runat="server"></span></h3>
                <a id="a_More_item1" href="#" class="more-btn">More&nbsp;<i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i></a>
               <%--<asp:button Text="More" class="more-btn" UseSubmitBehavior="false" causesvalidation="false" id="btnMore1" runat="server" onclick="btnSearchMore1_Click"></asp:button>--%>
               
            </div>
         </div>
      </div>
      <div class="row" >
         <div class="col-lg-12 no-background-slider home-slider" id="dv_slider1st">  
             <div id="sliderone"></div>
         </div>
      </div>
   </div>

    <div class="image-card" id="dv_BannerCard" runat="server" style="display:none;">
        <div class="row" id="BannerBind"></div>
    </div>

   <div id="dv_Slide2" runat="server">
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12">
            <div style="margin-top: 16px; text-align: left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
               <h3 style="width:95%;"id="hMore2"><span id="slider2_name" runat="server"> </span></h3>
                <a id="a_More_item2" href="#" class="more-btn">More&nbsp;<i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i></a>
               <%--<asp:button Text="More" class="more-btn" id="btnMore2" causesvalidation="false" runat="server" onclick="btnSearchMore2_Click"></asp:button>
               <i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i>--%>
            </div>
         </div>
      </div>
      <div class="row">
         <div class="col-lg-12 no-background-slider"> 
             <div id="sliderTwo"></div>
         </div>
      </div>
   </div>
   <div class="row"  >
      <div   class="col-lg-12 col-md-12 col-sm-12 middleSliderBody">
         <div id="dv_Slide3" runat="server">
            <div class="row ">
               <div class="col-lg-12 col-md-12 col-sm-12">
                  <h3 class="reco5 middleSliderBody-heading" id="slider3_name" runat="server"><span></span></h3>
               </div>
            </div>
            <div class="row">           
               <div class="col-lg-12 col-md-12 col-sm-12"> 
                   <div id="sliderThree"></div>
               </div>
            </div>
         </div>         
         <div id="dv_Slide4"  runat="server">        
            <div class="row">
               <div class="col-lg-12 col-md-12 col-sm-12">
                  <h3 class="middleSliderBody-heading" >
                     <span id="slider4_name" runat="server" ></span>
                  </h3>
               </div>
            </div>
            <div class="row">
               <div class="col-lg-12 col-md-12 col-sm-12" id="dv_sliderfour"> 
                   <div id="sliderFour"></div>
               </div>
            </div>
         </div>
      </div>
   </div>
    <%-- ====  Feature box ============= --%>
      
   <div id="dv_Slide5"  runat="server">
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12">
            <div style="margin-top: 16px; text-align: left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
               <h3 style="width:95%;" id="hMore5"><span id="slider5_name" runat="server"></span></h3>
                <a id="a_More_item5" href="#" class="more-btn">More&nbsp;<i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i></a>
               <%--<asp:button Text="More" class="more-btn" causesvalidation="false" id="btnMore5" runat="server" onclick="btnSearchMore5_Click"> </asp:button>
               <i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i>--%>
            </div>
         </div>
      </div>
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12 no-background-slider"> 
             <div id="sliderfive"></div>
         </div>
      </div>
   </div>

   <div id="dv_Banner3"  runat="server" style="display:none;">
      <div  class="row">
         <div class="col-lg-12 col-md-12 col-sm-12" style="margin-top: 45px;height: 165px;"  id="ShoeFullWidthBanner"></div>
      </div>
   </div>
   <div id="dv_Slide6" runat="server">
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12">
            <div style="margin-top: 16px; text-align: left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
               <h3 style="width:95%;"id="hMore6"><span id="slider6_name" runat="server"></span></h3>
              <%-- <asp:button Text="More" causesvalidation="false" class="more-btn" id="btnMore6" runat="server" onclick="btnSearchMore6_Click"></asp:button>
               <i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i>--%>
                <a id="a_More_item6" href="#" class="more-btn">More&nbsp;<i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i></a>
            </div>
         </div>
      </div>
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12 no-background-slider"> 
             <div id="slidersix"></div>
         </div>
      </div>
   </div>
   <div id="dv_Slide7" runat="server">
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12">
            <div style="margin-top: 16px; text-align: left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
               <h3 style="width:95%;" id="hMore7"><span id="slider7_name" runat="server"></span></h3>
              <%-- <asp:button Text="More" class="more-btn" causesvalidation="false" id="btnMore7" runat="server" onclick="btnSearchMore7_Click"> </asp:button>
               <i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i>--%>
                <a id="a_More_item7" href="#" class="more-btn">More&nbsp;<i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i></a>
            </div>
         </div>
      </div>
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12 no-background-slider"> 
             <div id="sliderseven"></div>
         </div>
      </div>
   </div>
    <div >
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <img  style="width:100%;" src="<%=BannerImage2 %>" alt="<%=BannerBookName2 %>">
                </div>                 
            </div>
        </div>
    </div>
   <div id="dv_Slide8" runat="server">
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12">
            <div style="margin-top: 16px; text-align: left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
               <h3 style="width:95%;"><span id="slider8_name" runat="server"></span></h3>
              <%-- <asp:button Text="More" class="more-btn" id="btnMore8" causesvalidation="false" runat="server" onclick="btnSearchMore8_Click"> </asp:button>
               <i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i>--%>
                <a id="a_More_item8" href="#" class="more-btn">More&nbsp;<i style="margin-top: 10px;margin-left: -5px;" class="fa fa-chevron-right"></i></a>
            </div>
         </div>
      </div>
      <div class="row">
         <div class="col-lg-12 col-md-12 col-sm-12 no-background-slider"> 
             <div id="slidereight"></div>
         </div>
      </div>
   </div>
  
   <!-- coming Up-->      
   <div id="dv_banner1" runat="server">
      <div class="row"  >
         <div class="col-lg-12 col-md-12 col-sm-12">
            <div style=" text-align: left;display: inline-flex;" class="col-md-12 news-heading">
               <h4 style="width:100%;text-align:center;font-weight:700;padding-top:20px;color:#0E345A;font:normal normal bold 20px/1.4em futura-lt-w01-book,sans-serif">
                  <span>Coming Up</span>
               </h4>
            </div>
            <div  class="col-md-12 news-heading">
               <h1 style="width:100%;text-align:center;font-weight:700;color:#0E345A;font:normal normal bold 47px/1.4em georgia,palatino,'book antiqua','palatino linotype',serif;margin-top: -15px;">
                  <span>New Launch</span>
               </h1>
            </div>
         </div>
      </div>
      <div class="row"   >
         <div class="col-lg-12 col-md-12 col-sm-12" style="margin-left:50px; margin-bottom:0px;margin-top:20px;height:405px;" >
            <div class="col-lg-6 col-md-6 col-sm-6">
               <div style="margin-bottom:36px;color:rgb(14, 52, 90);margin-left:150px;">
                  <h1 style="font:normal normal normal 23px/28px futura-lt-w01-book,sans-serif;">
                     Introducing <%=BannerBookName1 %>
                  </h1>
               </div>
               <div style="margin-bottom:42px;margin-left:150px;color:rgb(14, 52, 90);font:normal normal normal 15px/1.4em futura-lt-w01-book,sans-serif;">
                  By <%=BannerBookAuthor1 %>
               </div>
               <div style="margin-bottom:42px;margin-left:150px;color:rgb(14, 52, 90);font:normal normal normal 16px/1.4em futura-lt-w01-book,sans-serif; ">
                  <%--When--%>
                  <div style="margin: 12px 0;">
                     <div style="width: 20px;
                        height: 1px;background-color:rgb(14, 52, 90);"></div>
                  </div>
                  <div style="color:rgb(14, 52, 90);">
                     <%--Jul 12, 2023, 7:00 PM--%>
                  </div>
               </div>
               <div style="margin-bottom:42px;margin-left:150px;color:rgb(14, 52, 90);font:normal normal normal 16px/1.4em futura-lt-w01-book,sans-serif; ">
                  <%--Where--%>
                  <div style="margin: 12px 0;">
                     <div style="width: 20px;
                        height: 1px;background-color:rgb(14, 52, 90);"></div>
                  </div>
                  <div style="color:rgb(14, 52, 90);">
                     <%--New Delhi, India-110001.--%>
                  </div>
               </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6">
               <div class="com2">
                  <div class="com123">
                     <div class="com1">
                        <div style="height: 400px;background-image: url('<%=BannerImage1 %>');background-repeat: no-repeat;background-size: 100% 370px;"></div>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>
   <!-- coming Up-->   
   <div class="row">
      <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 xs-mb">
         <div class="col-lg-12 col-md-12 col-sm-12">
            <div style="margin-top: 16px; text-align:left;display: inline-flex;margin-bottom: 16px;" class="col-md-12 news-heading">
               <h3 style="width:93%;"><span id="todayQuote" runat="server"></span></h3>
               <button class="more-btn"  style="display:none;">
               More
               <i class="fa fa-chevron-right"></i>
               </button>
            </div>
         </div>
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="ourNews owl-inner-1 owl-carousel" id="rpt_BannerNewsDiv"> </div>
        </div>
      </div>
      <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12" style="display:none;">
         <!-- block-newsletter-area-start -->
         <div class="col-lg-12 col-md-12 col-sm-12 hidden-xs" style="height:78px;" >
         </div>
         <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="block-newsletter" > 
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="#cc3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="validator"
                  ValidationGroup="signup_details" ControlToValidate="textEmail" runat="server" ErrorMessage="Invalid Email ID"></asp:RegularExpressionValidator>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="textEmail" CssClass="validator"  ValidationGroup="signup_details" ErrorMessage=""></asp:RequiredFieldValidator>
               <asp:TextBox ID="textEmail" runat="server" autocomplete="off" class="form-control" placeholder=" Enter your email address" />
               <div class="newsletter">
                  <div class="xs-12">
                     <p style="margin-bottom: 0;font-weight: 600;">Sign up for News Letter</p>
                  </div>
                  <div class="xs-12">
                     <asp:Button ID="btn_signup" runat="server"  style="margin-top:12px;color:white;background-color:blue;font-size: 15px;"  ValidationGroup="signup_details" class="btn btn-help"   OnClick="btn_signup_Click" Text="Subscribe!" />
                  </div>
               </div>
            </div>
            <!-- block-newsletter-area-end -->
         </div>
      </div>
   </div>  
</div>
</div>
</asp:Content>  
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
   <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal> 
    <script  type="text/javascript">
      function changeDefaultCollapseStatus(x) {
      	if (x.matches) {                          // If media query matches
      	    var pan = $("#subjects");             // Search Element with id = Subjects
      	    pan.removeClass("in");                // remove default class = in
      	    pan.attr("aria-expanded", "false");   // change default value of aria-expanded to false
      	    var pan2 = $("#collapsable-panel");   // search Element with id = Collapsed-panel
      	    pan2.addClass("collapsed");           // Add collapsed class
      	    pan2.attr("aria-expanded", "false");  // change default value of aria-expanded to false
      	    } 
      	}
      	
      	var x = window.matchMedia("(max-width: 700px)")
      	changeDefaultCollapseStatus(x)              // Call listener function at run time
      	x.addListener(changeDefaultCollapseStatus)                   // Attach listener function on state changes
             
      	$(document).ready(function () {
      	    ClearSearchedSessionData();
            Load_TopSliderContent();                  
        });

        $(window).load(function () {
            WEB_load_ItemSpecialOffer();
            Load_TopBannerContent();
            GetAllSliderNameInfo();
            LoadIndexCategoriesData();
            //LoadIndexSubjectsData();
            Load_TodaySaying();            
        });        
        function init() {
            var imgDefer = document.getElementsByTagName('img');
            for (var i = 0; i < imgDefer.length; i++) {
                if (imgDefer[i].getAttribute('data-src')) {
                    imgDefer[i].setAttribute('src', imgDefer[i].getAttribute('data-src'));
                }
            }
        }
        window.onload = init;  
    </script>
    <script src="js/1.0.6/IndexJsFIle.min.js"></script>
 
</asp:Content>