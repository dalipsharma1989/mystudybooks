function WEB_load_IteminSliders() {

    var queryString = getQueryStrings();
    var sliderName = queryString["sliderName"];
    if (sliderName == null || sliderName == "") {
        sliderName = "";
    }
    var sliderDetail = queryString["sliderDetail"];
    if (sliderDetail == null || sliderDetail == "") {
        sliderDetail = "";
    }

    if (sliderDetail == "splOffer") {
        $("#slider_name").html(sliderName);
        $.ajax({
            type: "POST",
            url: "index.aspx/WEB_load_ItemSpecialOffer",
            data: '{ }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {
                $("#WaitingMsg").css("display", "block");
            },
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
                        var HtmlRow = "<li><div class='col-lg-2 col-md-4 col-sm-6 col-xs-6'> <div style='text-align:center'>"
                                        + "<a class='sliderImage-wrapper' class='d-flex' href='view_book.aspx?productid=" + DataList[i].ProductID + "' title='" + DataList[i].BookName + "' id='shopitem" + DataList[i].ProductID + "'>"
                                        + "<div class='flip-card'><div class='flip-card-inner'><div class='flip-card-front'><div class='slider-image-wrapper'><div class='" + StockAvl + "'>Out of Stock</div>"
                                        + "<img class='sliderImage' src='" + DataList[i].ProductImage + "' alt='" + DataList[i].BookName + "' title='" + DataList[i].BookName + "' onError=this.onerror=null;this.src='../resources/no-image.jpg'; />" + SpecialDiscountLbl + "</div></div>"
                                        + "<div class='flip-card-back'><div class='flip-content'><p class='link'><b>" + DataList[i].BookName + "</b></p><b><p style='margin-bottom:0px'>By:</p></b>"
                                        + "<p style='font-style: italic;'>" + DataList[i].Author + "</p><b><p style='margin-bottom:0px' >Publisher: </p></b><p>" + DataList[i].Publisher + "</p></div></div></div></div><a class='product_tile givenmeElipse' href='view_book.aspx?productid=" + DataList[i].ProductID + "'>" + DataList[i].BookName + "</a>"
                                        + "<div style='margin-top: -4px;' class='star-rating'><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i></div>"
                                        + "<h4 style='text-align:center;'><div class='product-price d-flex' style='flex-direction:column;'><ul>" + ShownPrice + "</ul><div class='add-links-wrap-cover'><div class='add-links-wrap'>"
                                        + "<a id='ProductID_" + DataList[i].ProductID + "' href='view_book.aspx?productid=" + DataList[i].ProductID + "&title=" + DataList[i].BookName + "' title='" + DataList[i].BookName + "'  class='shopping-card'><div class='show'><i class='fa fa-external-link'></i></div></a>"
                                        + "<a CausesValidation='false'   id='itemID" + DataList[i].ProductID + "' title='Add To Cart'  onclick='return ModalEntry(\"" + DataList[i].ProductID + "\")' class='" + addCartButtonShown + "' ><i class='fa fa-shopping-cart hidden-xs'></i><span class=''>Add to Cart</span></a>"
                                        + "<a href='#' id='itemID" + DataList[i].ProductID + "' title='Add To Wishlist' onclick='return userwishlist(\"" + DataList[i].ProductID + "\")'  ><div class='add-to-wishlist'><i class='fa fa-heart '></i></div></a></div></div></div></h4></a></div></div></li>"
                        $("#ul_MoreList").append(HtmlRow);
                    } 
                }
            },
            complete: function (data) {
                $("#WaitingMsg").css("display", "none");
            },
            failure: function (response) {
                $("#WaitingMsg").css("display", "none");
                alert("Error");
            },
            error: function (error) {
                $("#WaitingMsg").css("display", "none");
                alert("error");
            }
        });
    } else { 
            $("#slider_name").html(sliderName);
    
            $.ajax({
                type: "POST",
                url: "index.aspx/WEB_load_ItemSliderDetail",
                data: "{'sliderName': '" + sliderDetail.toString() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                beforeSend: function () { 
                    $("#WaitingMsg").css("display", "block");
                },
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
                            var HtmlRow = "<li><div class='col-lg-2 col-md-4 col-sm-6 col-xs-6'> <div style='text-align:center'>"
                                        + "<a class='sliderImage-wrapper' class='d-flex' href='view_book.aspx?productid=" + DataList[i].ProductId + "' title='" + DataList[i].BookName + "' id='shopitem" + DataList[i].ProductId + "'>"
                                        + "<div class='flip-card'><div class='flip-card-inner'><div class='flip-card-front'><div class='slider-image-wrapper'><div class='" + StockAvl + "'>Out of Stock</div>"
                                        + "<img class='sliderImage' src='" + DataList[i].ProductImage + "' alt='" + DataList[i].BookName + "' title='" + DataList[i].BookName + "' onError=this.onerror=null;this.src='../resources/no-image.jpg'; />" + SpecialDiscountLbl + "</div></div>"
                                        + "<div class='flip-card-back'><div class='flip-content'><p class='link'><b>" + DataList[i].BookName + "</b></p><b><p style='margin-bottom:0px'>By:</p></b>"
                                        + "<p style='font-style: italic;'>" + DataList[i].Author + "</p><b><p style='margin-bottom:0px' >Publisher: </p></b><p>" + DataList[i].Publisher + "</p></div></div></div></div><a class='product_tile' href='view_book.aspx?productid=" + DataList[i].ProductId + "'>" + DataList[i].BookName + "</a>"
                                        + "<div style='margin-top: -4px;' class='star-rating'><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i></div>"
                                        + "<h4 style='text-align:center;'><div class='product-price d-flex' style='flex-direction:column;'><ul>" + ShownPrice + "</ul><div class='add-links-wrap-cover'><div class='add-links-wrap'>"
                                        + "<a id='ProductID_" + DataList[i].ProductId + "' href='view_book.aspx?productid=" + DataList[i].ProductId + "&title=" + DataList[i].BookName + "' title='" + DataList[i].BookName + "'  class='shopping-card'><div class='show'><i class='fa fa-external-link'></i></div></a>"
                                        + "<a CausesValidation='false'   id='itemID" + DataList[i].ProductId + "' title='Add To Cart'  onclick='return ModalEntry(\"" + DataList[i].ProductId + "\")' class='" + addCartButtonShown + "' ><i class='fa fa-shopping-cart hidden-xs'></i><span class=''>Add to Cart</span></a>"
                                        + "<a href='#' id='itemID" + DataList[i].ProductId + "' title='Add To Wishlist' onclick='return userwishlist(\"" + DataList[i].ProductId + "\")'  ><div class='add-to-wishlist'><i class='fa fa-heart '></i></div></a></div></div></div></h4></a></div></div></li>"
                            $("#ul_MoreList").append(HtmlRow);
                        } 
                    }
                },
                complete: function (data) { 
                    $("#WaitingMsg").css("display", "none");
                },
                failure: function (response) {
                    $("#WaitingMsg").css("display", "none");
                    alert("Error");
                },
                error: function (error) {
                    $("#WaitingMsg").css("display", "none");
                    alert("error");
                }
            });
    }
}


$(document).ready(function () {
    WEB_load_IteminSliders();
});