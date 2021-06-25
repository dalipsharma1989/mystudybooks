String.prototype.replaceAll = function (find, replace) {
    var str = this;
    return str.replace(new RegExp(find.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&'), 'g'), replace);
};

function Load_TopSliderContent() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_sliders",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            for (var i = 0; i < TotLen; i++) {
                var LinkText = "";
                var UrlText = "<a href='' title='" + DataList[i].BookName + "' id='ProductID'>";
                if (DataList[i].BookCode != null && DataList[i].BookCode != "") {
                    LinkText = "<a id='Product_ + " + DataList[i].BookCode + "' class='btn btn-primary scale-up delay-1' style='bottom:30px;margin-right: -200px;overflow:hidden;position:absolute;z-index:10000;' href='view_book.aspx?productid=" + DataList[i].BookCode + "' title='See Details'>Shop Now&nbsp;<i class='icon-arrow-right'></i></a>";
                    UrlText = "<a href='view_book.aspx?productid=" + DataList[i].BookCode + "' title='" + DataList[i].BookName + "' id='ProductID_" + DataList[i].BookCode + "'>";
                }
                var HtmlRow = "<div style='margin: auto; width: 100%;' class='row justify-content-center align-items-center'>"                 
                    + UrlText+ "<img class='owl-lazy image-banner' src='data:image/png;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs='  data-src='" + DataList[i].SliderPath + "'  alt='" + DataList[i].BookName + "'></a>"
                +LinkText+"</div>"
                $("#TopHeaderSliderDiv").append(HtmlRow);
            }
            $("#TopHeaderSliderDiv").addClass("slider-active owl-carousel owl-demo");
            $("#TopHeaderSliderDiv").owlCarousel({
                smartSpeed: 1000,
                margin: 0,
                autoplay: true,
                lazyLoad: true,
                nav: true,
                dots: true,
                loop: true,
                autoplayHoverPause: true,
                navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
                responsive: {
                    0: {
                        items: 1
                    },
                    768: {
                        items: 1
                    },
                    1000: {
                        items: 1
                    }
                }
            });
            $('#SecondBlockAftermainSlider').css('opacity', '1');             
        },
        failure: function (response) {
            Console.log("error");
            SecondBlockAftermainSlider
        },
        error: function (error) {
            Console.log("error");
            SecondBlockAftermainSlider
        }
    });
    $('#InDexMainBlock').css('opacity', '1'); 
}

function Load_TopBannerContent() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_banners",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            for (var i = 0; i < TotLen; i++) {
                $("#head_dv_banner1").css("display", "none");
                $("#head_dv_Banner3").css("display", "none");
                if (DataList[i].BannerID == "4") {
                    var HtmlRow = "<div class='col-lg-4 col-md-4 col-sm-12 col-xs-12' id='dv_Banner4' runat='server'><img  style='width:100%;' src='" + DataList[i].ImagePath + "' alt='" + DataList[i].BookName + "'>"
                               + "<div class='content-wrapper' > <h3>English Titles</h3> <a href='" + DataList[i].Link + "' class='btn-tertiary btn'>View Collection<i class='fa fa-long-arrow-right' aria-hidden='true'></i></a></div ></div > "
                    $("#BannerBind").append(HtmlRow);
                } else if (DataList[i].BannerID == "5") {
                    var HtmlRow = "<div class='col-lg-4 col-md-4 col-sm-12 col-xs-12' id='dv_Banner4' runat='server'><img  style='width:100%;' src='" + DataList[i].ImagePath + "' alt='" + DataList[i].BookName + "'>"
                               + "<div class='content-wrapper' > <h3>Malayalam Titles</h3> <a href='" + DataList[i].Link + "' class='btn-tertiary btn'>View Collection<i class='fa fa-long-arrow-right' aria-hidden='true'></i></a></div ></div > "
                    $("#BannerBind").append(HtmlRow);

                } else if (DataList[i].BannerID == "6") {
                    var HtmlRow = "<div class='col-lg-4 col-md-4 col-sm-12 col-xs-12' id='dv_Banner4' runat='server'><img  style='width:100%;' src='" + DataList[i].ImagePath + "' alt='" + DataList[i].BookName + "'>"
                               + "<div class='content-wrapper' > <h3>Other Titles</h3> <a href='" + DataList[i].Link + "' class='btn-tertiary btn'>View Collection<i class='fa fa-long-arrow-right' aria-hidden='true'></i></a></div ></div > "
                    $("#BannerBind").append(HtmlRow);
                }
                else if (DataList[i].BannerID == "3") {
                    //var spancheck = "<span style='background:#ff4318;' class='new-item'>" + DataList[i].SpecialDiscount + "% OFF</span>";
                    //if (DataList[i].SpecialDiscount == "0") {
                    //    spancheck = "";
                    //}                    
                    //var HtmlRow = " <a id='prod_" + DataList[i].BookName + "' href='" + DataList[i].Link + "' title='Detail' ><img  style='width:100%;height:142px;' src='" + DataList[i].ImagePath + "' alt='" + DataList[i].BookName + "'>" + spancheck + "</a>";
                    //$("#ShoeFullWidthBanner").append(HtmlRow);
                    //$("#head_dv_Banner3").css("display", "block");

                } else if (DataList[i].BannerID == "2") {
                    var HtmlRow = "";

                } else if (DataList[i].BannerID == "1") {
                    var HtmlRow = "";
                }
                
            }
           
        },
        failure: function (response) {
            Console.log("error");
        },
        error: function (error) {
            Console.log("error");
        }
    });
}

function GetAllSliderNameInfo() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_ItemSliders",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            if (DataList.length > 0) {
                for (var i = 0; i < TotLen; i++) {

                    if (DataList[i].IstBookSlider) {
                        var sliderName = DataList[i].IstBookSlider;
                        var slider_arr = sliderName.split("|");
                        SliderName = slider_arr[0].replace("{", "").replace("}", "");
                        slider_csv = slider_arr[1].replace("{", "").replace("}", "");
                        slider_csv = slider_csv.replaceAll("'", "");
                        var SldName = "";
                        SldName = slider_arr[0].replace("{", "").replace("}", "");
                        //var Slider1_Name=DataList[i].
                        WEB_load_IteminSliders(slider_csv, "sliderone");
                        $("#head_slider1_name").html(SldName); 
                        document.getElementById("a_More_item1").href = "../more_Items.aspx?sliderName=" + SldName + "&sliderDetail=" + slider_csv;
                        if (slider_csv == "") {
                            $("#head_dv_Slide1").css("display", "none");
                        } 
                    } else {
                        $("#head_dv_Slide1").css("display", "none");
                    }
                    if (DataList[i].IIndBookSlider) {
                        var sliderName = DataList[i].IIndBookSlider;
                        var slider_arr = sliderName.split("|");
                        SliderName = slider_arr[0].replace("{", "").replace("}", "");
                        slider_csv = slider_arr[1].replace("{", "").replace("}", "");
                        slider_csv = slider_csv.replaceAll("'", "");
                        WEB_load_IteminSliders(slider_csv, "sliderTwo");
                        var SldName = "";
                        SldName = slider_arr[0].replace("{", "").replace("}", "");
                        $("#head_slider2_name").html(SldName);
                        document.getElementById("a_More_item2").href = "../more_Items.aspx?sliderName=" + SldName + "&sliderDetail=" + slider_csv;
                        if (slider_csv == "") {
                            $("#head_dv_Slide2").css("display", "none");
                        }
                    } else {
                        $("#head_dv_Slide2").css("display", "none");
                    }
                    if (DataList[i].IIIrdBookSlider) {
                        var sliderName = DataList[i].IIIrdBookSlider;
                        var slider_arr = sliderName.split("|");
                        SliderName = slider_arr[0].replace("{", "").replace("}", "");
                        slider_csv = slider_arr[1].replace("{", "").replace("}", "");
                        slider_csv = slider_csv.replaceAll("'", "");

                        WEB_load_IteminSliders(slider_csv, "sliderThree");
                        var SldName = "";
                        SldName = slider_arr[0].replace("{", "").replace("}", "");
                        $("#head_slider3_name").html(SldName);
                       // document.getElementById("a_More_item1").href = "../more_Items.aspx?sliderName=" + SldName + "&sliderDetail=" + slider_csv;
                        if (slider_csv == "") {
                            $("#head_dv_Slide3").css("display", "none");
                        }
                    } else {
                        $("#head_dv_Slide3").css("display", "none");
                    }
                    if (DataList[i].IVthBookSlider) {
                        var sliderName = DataList[i].IVthBookSlider;
                        var slider_arr = sliderName.split("|");
                        SliderName = slider_arr[0].replace("{", "").replace("}", "");
                        slider_csv = slider_arr[1].replace("{", "").replace("}", "");
                        slider_csv = slider_csv.replaceAll("'", "");
                        var SldName = "";
                        SldName = slider_arr[0].replace("{", "").replace("}", "");
                        WEB_load_IteminSliders(slider_csv, "sliderFour");
                        $("#head_slider4_name").html(SldName);
                        //document.getElementById("a_More_item1").href = "../more_Items.aspx?sliderName=" + SldName + "&sliderDetail=" + slider_csv;
                        if (slider_csv == "") {
                            $("#head_dv_Slide4").css("display", "none");
                        }
                    } else {
                        $("#head_dv_Slide4").css("display", "none");
                    }
                    if (DataList[i].VthBookSlider) {
                        var sliderName = DataList[i].VthBookSlider;
                        var slider_arr = sliderName.split("|");
                        SliderName = slider_arr[0].replace("{", "").replace("}", "");
                        slider_csv = slider_arr[1].replace("{", "").replace("}", "");
                        slider_csv = slider_csv.replaceAll("'", "");
                        WEB_load_IteminSliders(slider_csv, "sliderfive");
                        var SldName = "";
                        SldName = slider_arr[0].replace("{", "").replace("}", "");
                        $("#head_slider5_name").html(SldName);
                        document.getElementById("a_More_item5").href = "../more_Items.aspx?sliderName=" + SldName + "&sliderDetail=" + slider_csv;
                        if (slider_csv == "") {
                            $("#head_dv_Slide5").css("display", "none");
                        }
                    } else {
                        $("#head_dv_Slide5").css("display", "none");
                    }
                    if (DataList[i].VIthBookSlider) {
                        var sliderName = DataList[i].VIthBookSlider;
                        var slider_arr = sliderName.split("|");
                        SliderName = slider_arr[0].replace("{", "").replace("}", "");
                        slider_csv = slider_arr[1].replace("{", "").replace("}", "");
                        slider_csv = slider_csv.replaceAll("'", "");
                        var SldName = "";
                        SldName = slider_arr[0].replace("{", "").replace("}", "");
                        WEB_load_IteminSliders(slider_csv, "slidersix");
                        $("#head_slider6_name").html(SldName);
                        document.getElementById("a_More_item6").href = "../more_Items.aspx?sliderName=" + SldName + "&sliderDetail=" + slider_csv;
                        if (slider_csv == "") {
                            $("#head_dv_Slide6").css("display", "none");
                        }
                    } else {
                        $("#head_dv_Slide6").css("display", "none");
                    }
                    if (DataList[i].VIIBookSlider) {
                        var sliderName = DataList[i].VIIBookSlider;
                        var slider_arr = sliderName.split("|");
                        SliderName = slider_arr[0].replace("{", "").replace("}", "");
                        slider_csv = slider_arr[1].replace("{", "").replace("}", "");
                        slider_csv = slider_csv.replaceAll("'", "");
                        var SldName = "";
                        SldName = slider_arr[0].replace("{", "").replace("}", "");
                        WEB_load_IteminSliders(slider_csv, "sliderseven");
                        $("#head_slider7_name").html(SldName);
                        document.getElementById("a_More_item7").href = "../more_Items.aspx?sliderName=" + SldName + "&sliderDetail=" + slider_csv;
                        if (slider_csv == "") {
                            $("#head_dv_Slide7").css("display", "none");
                        }
                    } else {
                        $("#head_dv_Slide7").css("display", "none");
                    }
                    if (DataList[i].VIIIBookSlider) {
                        var sliderName = DataList[i].VIIIBookSlider;
                        var slider_arr = sliderName.split("|");
                        SliderName = slider_arr[0].replace("{", "").replace("}", "");
                        slider_csv = slider_arr[1].replace("{", "").replace("}", "");
                        slider_csv = slider_csv.replaceAll("'", "");
                        var SldName = "";
                        SldName = slider_arr[0].replace("{", "").replace("}", "");
                        WEB_load_IteminSliders(slider_csv, "slidereight");
                        $("#head_slider8_name").html(SldName);
                        document.getElementById("a_More_item8").href = "../more_Items.aspx?sliderName=" + SldName + "&sliderDetail=" + slider_csv;
                        if (slider_csv == "") {
                            $("#head_dv_Slide8").css("display", "none");
                        }
                    } else {
                        $("#head_dv_Slide8").css("display", "none");
                    }
                }
            } else {
                $("#head_dv_Slide1").css("display", "none");
                $("#head_dv_Slide2").css("display", "none");
                $("#head_dv_Slide3").css("display", "none");
                $("#head_dv_Slide4").css("display", "none");
                $("#head_dv_Slide5").css("display", "none");
                $("#head_dv_Slide6").css("display", "none");
                $("#head_dv_Slide7").css("display", "none");
                $("#head_dv_Slide8").css("display", "none");
            }
        },
        failure: function (response) {
            alert("Error");
        },
        error: function (error) {
            alert("error");
        }
    })
}

function WEB_load_ItemSpecialOffer() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_ItemSpecialOffer",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            if (TotLen > 0) {
                document.getElementById("a_More_itemSpecial_Offer").href = "../more_Items.aspx?sliderName=Special Offer&sliderDetail=splOffer";
                $("#head_dv_SpecialOffer").css("display", "block");
            } else {
                $("#head_dv_SpecialOffer").css("display", "none");
            }
            if (DataList.length > 0) {
                for (var i = 0; i < TotLen; i++) {
                    var StockAvl = "hidden";
                    var SpecialDiscountLbl = "<span style='background:#ff4318;' class='new-item'>" + DataList[i].SpecialDiscount + " % OFF</span>";
                    var ShownPrice = "<li class='old-price' style='color:#bcac9f !important;'>" + DataList[i].SaleCurrency + " " + DataList[i].SalePrice + "</li><li>" + DataList[i].SaleCurrency + " " + DataList[i].DiscountPrice + "</li>";

                    if (DataList[i].SpecialDiscount == 0) {
                        ShownPrice = "<li> + " + DataList[i].SaleCurrency + " " + DataList[i].SalePrice + "</li>";
                    }
                    if (DataList[i].SpecialDiscount == 0) {
                        SpecialDiscountLbl = "";
                    }
                    var addCartButtonShown = "btn btn-primary addCartButton";
                    if (DataList[i].ClosingBal <= 0) {
                        StockAvl = "image-stock-out";
                        addCartButtonShown = "btn btn-primary disabled addCartButton";
                    }
                    var HtmlRow = "<div class=''><a class='sliderImage-wrapper' href='view_book.aspx?productid=" + DataList[i].ProductID + "&title=" + DataList[i].BookName + "' title='" + DataList[i].BookName + "' id='SpecialOffer" + DataList[i].ProductID + "'>"
                        + "<div class='flip-card'><div class='flip-card-inner'><div class='flip-card-front'>"
                        + "<div class='slider-image-wrapper'>"
                        + "<div class='" + StockAvl + "'>Out of Stock</div>"
                        + "<img class='sliderImage owl-lazy' src='" + DataList[i].ProductImage + "' alt='" + DataList[i].BookName + "' title='" + DataList[i].BookName + "' onError=this.onerror=null;this.src='../resources/no-image.jpg';  />"
                        + SpecialDiscountLbl + "</div></div><div class='flip-card-back'><div class='flip-content'><p class='link'><b>" + DataList[i].BookName + "</b></p>"
                        + "<p style='font-style: italic;'> " + DataList[i].Author + "</p><p>" + DataList[i].Publisher + "</p></div></div></div></div>"
                        + "<a class='product_tile'  href='view_book.aspx?productid=" + DataList[i].ProductID + "&title=" + DataList[i].BookName + "'>" + DataList[i].BookName + "</a>"
                        + "<div style='margin-top: -4px;' class='star-rating'><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i></div>"
                        + "<h4 style='text-align:center;'><div class='product-price d-flex' style='flex-direction:column;'><ul>" + ShownPrice + "</ul>"
                        + "<div class='add-links-wrap-cover'><div class='add-links-wrap'>"
                        + "<a id='ProductID_" + DataList[i].ProductID + "' href='view_book.aspx?productid=" + DataList[i].ProductID + "&title=" + DataList[i].BookName + "' title='" + DataList[i].BookName + "' class='shopping-card'>"
                        + "<div class='show'><i class='fa fa-external-link'></i></div></a>"
                        + "<a href='#' id='itemID" + DataList[i].ProductID + "' title='Add To Cart'  onclick='return ModalEntry(\"" + DataList[i].ProductID + "\")' class='" + addCartButtonShown + "'>"
                        + "<i class='fa fa-shopping-cart hidden-xs'></i><span class=''>Add to Cart</span></a>"
                        + "<a href='#' id='itemID" + DataList[i].ProductID + "' title='Add To Wishlist' onclick='return userwishlist(\"" + DataList[i].ProductID + "\")' >"
                        + "<div class='add-to-wishlist'>"
                        + "<i class='fa fa-heart'></i></div></a></div></div></div></h4></a></div>";
                    //if (Count == 0) {
                    //    $("#spl_Offers").append("<div class='tab-active-3 owl-carousel'>" + HtmlRow);
                    //}      
                    $("#spl_Offers").append(HtmlRow);
                }

                $("#spl_Offers").addClass("tab-active-3 owl-carousel");
                $("#spl_Offers").owlCarousel({
                    smartSpeed: 1000,
                    nav: true,
                    autoplay: true,
                    loop: true,
                    margin: 20,
                    autoplayHoverPause: true,
                    lazyLoad: true,
                    navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
                    responsive: {
                        0: {
                            items: 2
                        },
                        768: {
                            items: 3
                        },
                        992: {
                            items: 4
                        },
                        1170: {
                            items: 5
                        },
                        1300: {
                            items: 6
                        }
                    }
                });
                //$("#spl_Offers").append("</div>");
            }
        },
        failure: function (response) {
            alert("Error");
        },
        error: function (error) {
            alert("error");
        }
    });
}

function Load_TodaySaying() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_Load_TodaySaying",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            for (var i = 0; i < TotLen; i++) {
                $("#todayQuote").html(DataList[0].QuoteHeading);
                var HtmlRow = "<div style='margin: 15px;' ><div style='box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);margin-bottom:5px;' class='col-lg-12 col-md-12 col-sm-12'>"
                    + "<div class='col-lg-12 col-md-12 col-sm-12 news-heading'>" + DataList[i].Name + "</div>"
                    + "<div class='col-lg-12 col-md-12 col-sm-12 news-sub-heading'>" + DataList[i].HeaderContent + "</div>"
                    + "<div style='padding-top: 0px; color:black' class='col-lg-12 col-md-12 col-sm-12 news-sub-heading'>" + DataList[i].MainContent + "</div></div></div>" 
                $("#rpt_BannerNewsDiv").append(HtmlRow);
            }
            $("#rpt_BannerNewsDiv").addClass("ourNews owl-inner-1 owl-carousel");
            $("#rpt_BannerNewsDiv").owlCarousel({
                smartSpeed: 1000,
                nav: true,
                autoplay: true,
                loop: true,
                margin: 20,
                autoplayHoverPause: true,
                lazyLoad: true,
                navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
                responsive: {
                    0: {
                        items: 1
                    },
                    768: {
                        items: 1
                    },
                    992: {
                        items: 2
                    },
                    1170: {
                        items: 3
                    },
                    1300: {
                        items: 3
                    }
                }
            });
            

        },
        failure: function (response) {
            alert("Error");
        },
        error: function (error) {
            alert("error");
        }
    });
}

function LoadIndexCategoriesData() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_BookCategories",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            for (var i = 0; i < TotLen; i++) {
                var HtmlRow = "<div class='col-lg-6 col-lg-6 col-sm-6 col-xs-12'><div class='small feature-box school'>"
                    + "<a href='search_results.aspx?Cat=" + DataList[i].BookCategoryID + "'>"
                    + "<h6 style='color: #f48221;font-size: 30px;text-align: center;'>" + DataList[i].BookCategoryDesc + "</h6></a></div></div>"


                $("#BooksCategoriesItems").append(HtmlRow);
            }

        },
        failure: function (response) {
            alert("Error");
        },
        error: function (error) {
            alert("error");
        }
    });
}

function ClearSearchedSessionData() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_CLearSearchSession",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (data) {
            //var DataList = JSON.parse(data.d);  
        },
        failure: function (response) {
            alert("Error");
        },
        error: function (error) {
            alert("error");
        }
    });
}

function WEB_load_IteminSliders(sliderName, SliderDivName) { 
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_ItemSliderDetail",
        data: "{'sliderName': '" + sliderName.toString() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            if (DataList.length > 0) {
                for (var i = 0; i < TotLen; i++) {
                    var StockAvl = "hidden";
                    var SpecialDiscountLbl = "<span style='background:#ff4318;' class='new-item'>" + DataList[i].SpecialDiscount + " % OFF</span>";
                    var ShownPrice = "<li class='old-price' style='color:#bcac9f !important;'>" + DataList[i].SaleCurrency + " " + DataList[i].SalePrice + "</li><li>" + DataList[i].SaleCurrency + " " + DataList[i].DiscountPrice + "</li>";

                    if (DataList[i].SpecialDiscount == 0) {
                        ShownPrice = "<li>" + DataList[i].SaleCurrency + " " + DataList[i].SalePrice + "</li>";
                    }
                    if (DataList[i].SpecialDiscount == 0) {
                        SpecialDiscountLbl = "";
                    }
                    var addCartButtonShown = "btn btn-primary addCartButton";
                    if (DataList[i].ClosingBal <= 0) {
                        StockAvl = "image-stock-out";
                        addCartButtonShown = "btn btn-primary disabled addCartButton";
                    }
                    var HtmlRow = "<div class=''><a class='sliderImage-wrapper' href='view_book.aspx?productid=" + DataList[i].ProductId + "&title=" + DataList[i].BookName + "' title='" + DataList[i].BookName + "' id='SpecialOffer" + DataList[i].ProductId + "'>"
                        + "<div class='flip-card'><div class='flip-card-inner'><div class='flip-card-front'>"
                        + "<div class='slider-image-wrapper'>"
                        + "<div class='" + StockAvl + "'>Out of Stock</div>"
                        + "<img class='sliderImage owl-lazy' src='" + DataList[i].ProductImage + "' alt='" + DataList[i].BookName + "' title='" + DataList[i].BookName + "' onError=this.onerror=null;this.src='../resources/no-image.jpg';  />"
                        + SpecialDiscountLbl + "</div></div><div class='flip-card-back'><div class='flip-content'><p class='link'><b>" + DataList[i].BookName + "</b></p>"
                        + "<p style='font-style: italic;'> " + DataList[i].Author + "</p><p>" + DataList[i].Publisher + "</p></div></div></div></div>"
                        + "<a class='product_tile'  href='view_book.aspx?productid=" + DataList[i].ProductId + "&title=" + DataList[i].BookName + "'>" + DataList[i].BookName + "</a>"
                        + "<div style='margin-top: -4px;' class='star-rating'><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i></div>"
                        + "<h4 style='text-align:center;'><div class='product-price d-flex' style='flex-direction:column;'><ul>" + ShownPrice + "</ul>"
                        + "<div class='add-links-wrap-cover'><div class='add-links-wrap'>"
                        + "<a id='ProductID_--" + DataList[i].ProductId + "' href='view_book.aspx?productid=" + DataList[i].ProductId + "&title=" + DataList[i].BookName + "' title='" + DataList[i].BookName + "' class='shopping-card'>"
                        + "<div class='show'><i class='fa fa-external-link'></i></div></a>"
                        + "<a href='#' id='itemID" + DataList[i].ProductId + "' title='Add To Cart'  onclick='return ModalEntry(\"" + DataList[i].ProductId + "\")'  class='" + addCartButtonShown + "'>"
                        + "<i class='fa fa-shopping-cart hidden-xs'></i><span class=''>Add to Cart</span></a>"
                        + "<a href='#' id=itemID" + DataList[i].ProductId + " title='Add To Wishlist'  onclick='return userwishlist(\"" + DataList[i].ProductId + "\")' >"
                        + "<div class='add-to-wishlist'>"
                        + "<i class='fa fa-heart'></i></div></a></div></div></div></h4></a></div>";
                    //if (Count == 0) {
                    //    $("#spl_Offers").append("<div class='tab-active-3 owl-carousel'>" + HtmlRow);
                    //}      
                    $("#" + SliderDivName).append(HtmlRow);
                }
                $("#" + SliderDivName).addClass("tab-active-3 owl-carousel");
                $("#" + SliderDivName).owlCarousel({
                    smartSpeed: 1000,
                    nav: true,
                    autoplay: true,
                    loop: true,
                    margin: 20,
                    autoplayHoverPause: true,
                    lazyLoad: true,
                    navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
                    responsive: {
                        0: {
                            items: 2
                        },
                        768: {
                            items: 3
                        },
                        992: {
                            items: 4
                        },
                        1170: {
                            items: 5
                        },
                        1300: {
                            items: 6
                        }
                    }
                });

                //$("#spl_Offers").append("</div>");
            }
        },
        failure: function (response) {
            alert("Error");
        },
        error: function (error) {
            alert("error");
        }
    });
}

function LoadIndexSubjectsData() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_BookSubjectAsCategory",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            for (var i = 0; i < TotLen; i++) {
                var HtmlRow = "<div class='col-lg-3 col-lg-6 col-sm-6 col-xs-12'><div class='small feature-box'>"
                    + "<a href='search_results.aspx?SubjectID=" + DataList[i].TitleSubjectID + "'>"
                    + "<h6>" + DataList[i].SubjectName + "</h6><i class='fa fa-angle-right' aria-hidden='true'></i>"
                    + "</a></div></div>"


                $("#BooksCategoriesSubject").append(HtmlRow);
            }

        },
        failure: function (response) {
            alert("Error");
        },
        error: function (error) {
            alert("error");
        }
    });
}
