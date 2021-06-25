var NextPageCount = 1;
var PreviousPageCount = 0;
function WEB_load_ItemSpecialOffer(sortby, nextPage, Previouspage) {

    try {

        var queryString = getQueryStrings(); 
        var querytxt = queryString["q"];
        if (querytxt == null || querytxt == "") {
            querytxt = "";
        } 
        //var langid = queryString["langid"];
        //var langnm = queryString["langnm"];
        //if (langid == null || langid == "" || langid == "Nil") {

        //} 

        //if ($("#ContentPlaceHolder1_ddl_Language").val() == langid) {
        //    $("#ContentPlaceHolder1_ddl_Language").val(langid);
        //}

        sortby = $("#sorting").val();
        if (sortby == null || sortby == "" || sortby == "Book Name") {
            sortby = "BookName";
        }/*Added by Ashok Sharma on 31-10-2020*/
        if (sortby == "Clear sort") {
            sortby = "clear";
        }
        var SelectedCategories = $('input[name="checkboxforcategories"]:checkbox:checked:checkbox:checked').map(function () { return this.id; }).get();
        if (SelectedCategories.length > 0) {
            SelectedCategories = SelectedCategories.toString();
        } else {
            SelectedCategories = "";
        }/*Added by Ashok Sharma on 31-10-2020*/

        var SelectedPublisher = $('input[name="checkboxforPublisher"]:checkbox:checked:checkbox:checked').map(function () { return this.id; }).get();
        if (SelectedPublisher.length > 0) {
            SelectedPublisher = SelectedPublisher.toString();
        } else {
            SelectedPublisher = "";
        }/*Added by Ashok Sharma on 31-10-2020*/

        var SelectedAuthor = $('input[name="checkboxforAuthors"]:checkbox:checked:checkbox:checked').map(function () { return this.id; }).get();
        if (SelectedAuthor.length > 0) {
            SelectedAuthor = SelectedAuthor.toString();
        } else {
            SelectedAuthor = "";
        }
        var sortingAcenDesc = $("#sortingAcenDesc").val();
        var Language = $("#ContentPlaceHolder1_ddl_Language").val();

        var msg = "You are Looking for ";
        if (Language != "Nil") {
            msg = msg + "<label style='color:black;font-weight:bold'> Language </label> - " + $("#ContentPlaceHolder1_ddl_Language option:selected").text() + " || ";
        }

        if (querytxt.toString() != "") {
            msg = msg + "<label style='color:black;font-weight:bold'> Text </label> - " + querytxt.toString() + " || ";
        }

        var SelectedCategoriesList = $('input[name="checkboxforcategories"]:checkbox:checked:checkbox:checked').map(function () { return this.id; }).get();
        var SelectedCategoriesText = [];
        for (var i = 0; i < SelectedCategoriesList.length; i++) {
            var lblText = $("#Categorylbl" + SelectedCategoriesList[i].replace("Category", ""))[0].innerText.toString();
            SelectedCategoriesText.push(lblText);
        }
        if (SelectedCategoriesList.length > 0) {
            msg = msg + "<label style='color:black;font-weight:bold'>Categories</label> - " + SelectedCategoriesText.toString()+" || ";
        }

        var SelectedPublisherList = $('input[name="checkboxforPublisher"]:checkbox:checked:checkbox:checked').map(function () { return this.id; }).get();
        var SelectedPublisherListText = [];
        for (var i = 0; i < SelectedPublisherList.length; i++) {
            var lblPublisherText = $("#Publisherlbl" + SelectedPublisherList[i].replace("Publisher", ""))[0].innerText;
            SelectedPublisherListText.push(lblPublisherText);
        }
        if (SelectedPublisherList.length > 0) {
            msg = msg + "<label style='color:black;font-weight:bold'>Publisher</label> - " + SelectedPublisherListText.toString() + " || ";
        }

        var SelectedAuthorList = $('input[name="checkboxforAuthors"]:checkbox:checked:checkbox:checked').map(function () { return this.id; }).get();
        var SelectedAuthorListText = [];
        for (var i = 0; i < SelectedAuthorList.length; i++) {
            var lblAuthorText = $("#Authorlbl" + SelectedAuthorList[i].replace("Author", ""))[0].innerText;
            SelectedAuthorListText.push(lblAuthorText);
        }
        if (SelectedAuthorList.length > 0) {
            msg = msg + "<label style='color:black;font-weight:bold'>Author</label> - " + SelectedAuthorListText.toString() + " || ";
        }

        if (msg == "You are Looking for ") {
            $("#showResultMsgs").html("");
        } else {
            $("#showResultMsgs").html(msg);
        }

        $.ajax({
            type: "POST",
            url: "index.aspx/WEB_load_ItemforShopGrid",
            data: "{'Language': '" + Language.toString() + "','nextPage': '" + nextPage.toString() + "','Previouspage': '" + Previouspage.toString() + "','sortingAcenDesc': '"
                + sortingAcenDesc.toString() + "','sortby': '" + sortby.toString() + "','SelectedCategories': '" + SelectedCategories.toString()
                + "','SelectedPublisher': '" + SelectedPublisher.toString() + "','SelectedAuthor': '" + SelectedAuthor.toString() + "','totPageSize': '25','queryString': '" + querytxt.toString() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            beforeSend: function () {/*Added by Ashok Sharma on 31-10-2020*/
                $("#WaitingMsg").css("display", "block");
            },
            success: function (data) {
                try {
                    var DataList = JSON.parse(data.d);
                    var TotLen = DataList.length;
                    var ShowNextPage = false;
                    if (TotLen > 20) {
                        ShowNextPage = true;
                        TotLen = 20;
                        $("#a_Next").removeAttr("disabled", "disabled");
                        $("#a_Nexttop").removeAttr("disabled", "disabled");
                    } else {
                        $("#a_Next").attr("disabled", "disabled");
                        $("#a_Nexttop").attr("disabled", "disabled");
                    }
                    if (TotLen == 0) {
                        ResetPageNumber();
                    }
                    var Count = 0;
                    $("#ul_ShopList").empty();/*Added by Ashok Sharma on 31-10-2020*/
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
                            var HtmlRow = "<li><div class='col-lg-3 col-md-4 col-sm-6 col-xs-6'> <div style='text-align:center'>"
                                + "<a class='sliderImage-wrapper' class='d-flex' href='view_book.aspx?productid=" + DataList[i].ProductID + "' title='" + DataList[i].BookName + "' id='shopitem" + DataList[i].ProductID + "'>"
                                + "<div class='flip-card'><div class='flip-card-inner'><div class='flip-card-front'><div class='slider-image-wrapper'><div class='" + StockAvl + "'>Out of Stock</div>"
                                + "<img class='sliderImage' src='" + DataList[i].ImgPath + "' alt='" + DataList[i].BookName + "' title='" + DataList[i].BookName + "' onError=this.onerror=null;this.src='../resources/no-image.jpg'; />" + SpecialDiscountLbl + "</div></div>"
                                + "<div class='flip-card-back'><div class='flip-content'><p class='link'><b>" + DataList[i].BookName + "</b></p><b><p style='margin-bottom:0px'>By:</p></b>"
                                + "<p style='font-style: italic;'>" + DataList[i].Author + "</p><b><p style='margin-bottom:0px' >Publisher: </p></b><p>" + DataList[i].Publisher + "</p></div></div></div></div><a class='product_tile' href='view_book.aspx?productid=" + DataList[i].ProductID + "'>" + DataList[i].BookName + "</a>"
                                + "<div style='margin-top: -4px;' class='star-rating'><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i><i class='fa fa-star'></i></div>"
                                + "<h4 style='text-align:center;'><div class='product-price d-flex' style='flex-direction:column;'><ul>" + ShownPrice + "</ul><div class='add-links-wrap-cover'><div class='add-links-wrap'>"
                                + "<a id='ProductID_" + DataList[i].ProductID + "' href='view_book.aspx?productid=" + DataList[i].ProductID + "&title=" + DataList[i].BookName + "' title='" + DataList[i].BookName + "'  class='shopping-card'><div class='show'><i class='fa fa-external-link'></i></div></a>"
                                + "<Button CausesValidation='false'   id='itemID" + DataList[i].ProductID + "' title='Add To Cart'  onclick='return ModalEntry(\"" + DataList[i].ProductID + "\")' class='" + addCartButtonShown + "' ><i class='fa fa-shopping-cart hidden-xs'></i><span class=''>Add to Cart</span></Button>"
                                + "<a href='#' id='itemID" + DataList[i].ProductID + "' title='Add To Wishlist' onclick='return userwishlist(\"" + DataList[i].ProductID + "\")'  ><div class='add-to-wishlist'><i class='fa fa-heart '></i></div></a></div></div></div></h4></a></div></div></li>"
                            $("#ul_ShopList").append(HtmlRow);
                        }

                        if (PreviousPageCount > 0) {
                            $("#a_previous").removeAttr("disabled", "disabled");
                            $("#a_previousTop").removeAttr("disabled", "disabled");
                        } else {
                            $("#a_previous").attr("disabled", "disabled");
                            $("#a_previousTop").attr("disabled", "disabled");
                        } 
                        
                    }
                } catch (err) {
                    $("#WaitingMsg").css("display", "none");
                }
            },
            complete: function (data) {/*Added by Ashok Sharma on 31-10-2020*/
                $("#WaitingMsg").css("display", "none");
            },
            failure: function (response) {
                $("#WaitingMsg").css("display", "none");
                alert("Error coming in data");
            },
            error: function (error) {
                $("#WaitingMsg").css("display", "none");
                alert("Error coming in data");
            }
        });
    } catch (err) {
        $("#WaitingMsg").css("display", "none");
    }
}
 
function Load_Categories() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_Categories",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            // var searchBar = "<div class='SearchWraper'><input   ID='txtSearchCategory' type='text' autocomplete='off' onkeyup='filterList(this);' CssClass='form-control' placeholder='Search Category here.....' /><i class='fa fa-search'></i></div>";

            for (var i = 0; i < TotLen; i++) {
                var HtmlRow = "<div class='custom-control custom-checkbox'><input name='checkboxforcategories' class='custom-control-input Category' type='checkbox' id='Category" + DataList[i].BookCategoryID + "'><label id='Categorylbl" + DataList[i].BookCategoryID + "' class='custom-control-label' for='Category" + DataList[i].BookCategoryID + "'>" + DataList[i].BookCategoryDesc + "&nbsp;<span class='text-muted'></span></label></div>";
                $("#dv_Category").append(HtmlRow);
            }
            var queryString = getQueryStrings();
            var Cat = queryString["Cat"];
             
            if (Cat == null || Cat == "Nil") {
                Cat = "";
            }
            if (Cat != "") {
                document.getElementById("Category"+Cat).checked = true;
            }
            // $("#dv_Category").append(searchBar + HtmlRow);
        },
        failure: function (response) {
            alert("Error coming in Catgeory");
        },
        error: function (error) {
            alert("Error coming in Catgeory");
        }
    });
}
function Load_Publishers() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_Publisher",
        data: '{ }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            // var searchBar = "<div class='SearchWraper'><input runat='server' ID='txtSearchCategory' type='text' autocomplete='off' onkeyup='filterList(this);' CssClass='form-control' placeholder='Search Category here.....' /><i class='fa fa-search'></i></div>";

            for (var i = 0; i < TotLen; i++) {
                var HtmlRow = "<div class='custom-control custom-checkbox'><input name='checkboxforPublisher' class='custom-control-input Publisher' type='checkbox' id='Publisher" + DataList[i].PublisherID + "'><label id='Publisherlbl" + DataList[i].PublisherID + "' class='custom-control-label' for='Publisher" + DataList[i].PublisherID + "'>" + DataList[i].PublisherName + "&nbsp;<span class='text-muted'></span></label></div>";
                $("#dv_Publisher").append(HtmlRow);
            }
            // $("#dv_Publisher").append(searchBar + HtmlRow);
        },
        failure: function (response) {
            alert("Error coming in Publisher");
        },
        error: function (error) {
            alert("Error coming in Publisher");
        }
    });
}

function Load_AuthorList() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_Author",
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
                var HtmlRow = "<div class='custom-control custom-checkbox'><input name='checkboxforAuthors' class='custom-control-input Author' type='checkbox' id='Author" + DataList[i].AuthorID + "'><label id='Authorlbl" + DataList[i].AuthorID + "' class='custom-control-label' for='Author" + DataList[i].AuthorID + "'>" + DataList[i].AuthorName + "&nbsp;<span class='text-muted'></span></label></div>";
                $("#dv_Authorlist").append(HtmlRow);
            }
        },
        failure: function (response) {
            alert("Error coming in Author");
        },
        error: function (error) {
            alert("Error coming in Author");
        }
    });
}

$(document).ready(function () {/*Added by Ashok Sharma on 31-10-2020*/
    $("#a_previous").attr("disabled", "disabled");
    $("#a_previousTop").attr("disabled", "disabled");    
    $('input[name="checkboxforcategories"]').click(function () {/*Added by Ashok Sharma on 31-10-2020*/
        ResetPageNumber();
        WEB_load_ItemSpecialOffer("BookName", 1, 0);
    });
    $('input[name="checkboxforPublisher"]').click(function () {/*Added by Ashok Sharma on 31-10-2020*/
        ResetPageNumber();
        WEB_load_ItemSpecialOffer("BookName", 1, 0);
    });
    $('input[name="checkboxforAuthors"]').click(function () {/*Added by Ashok Sharma on 31-10-2020*/
        ResetPageNumber();
        WEB_load_ItemSpecialOffer("BookName", 1, 0);
    });
    $("#ContentPlaceHolder1_ddl_Language").change(function () {/*Added by Ashok Sharma on 31-10-2020*/
        ResetPageNumber();
        WEB_load_ItemSpecialOffer("BookName", 1, 0);
    })
    $("#sorting").change(function (e) {
        if (this.value.replace(" ", "") != "Clearsort") {/*Added by Ashok Sharma on 31-10-2020*/
            ResetPageNumber();
            WEB_load_ItemSpecialOffer(this.value.replace(" ", ""), 1, 0);
        }
    })
    $("#sortingAcenDesc").change(function () {/*Added by Ashok Sharma on 31-10-2020*/
        ResetPageNumber();
        WEB_load_ItemSpecialOffer("BookName", 1, 0);
    });
    $("#a_Next").click(function () {
        NextPageCount++;
        PreviousPageCount++;
        $("#a_Next").attr('data-pageid', NextPageCount);
        $("#a_previous").attr('data-pageid', PreviousPageCount);

        $("#a_Nexttop").attr('data-pageid', NextPageCount);
        $("#a_previousTop").attr('data-pageid', PreviousPageCount);

        var PageNo = document.getElementById("a_Next").getAttribute("data-pageid");
        var PreviousNo = document.getElementById("a_previous").getAttribute("data-pageid");
        WEB_load_ItemSpecialOffer("BookName", PageNo, PreviousNo);
    });

    $("#a_Nexttop").click(function () {
        NextPageCount++;
        PreviousPageCount++;
        $("#a_Next").attr('data-pageid', NextPageCount);
        $("#a_previous").attr('data-pageid', PreviousPageCount);

        $("#a_Nexttop").attr('data-pageid', NextPageCount);
        $("#a_previousTop").attr('data-pageid', PreviousPageCount);

        var PageNo = document.getElementById("a_Nexttop").getAttribute("data-pageid");
        var PreviousNo = document.getElementById("a_previous").getAttribute("data-pageid");

        WEB_load_ItemSpecialOffer("BookName", PageNo, PreviousNo);
    });

    $("#a_previousTop").click(function () {
        NextPageCount--;
        PreviousPageCount--;
        $("#a_Next").attr('data-pageid', NextPageCount);
        $("#a_previous").attr('data-pageid', PreviousPageCount);

        $("#a_Nexttop").attr('data-pageid', NextPageCount);
        $("#a_previousTop").attr('data-pageid', PreviousPageCount);

        var PageNo = document.getElementById("a_Nexttop").getAttribute("data-pageid");
        var PreviousNo = document.getElementById("a_previous").getAttribute("data-pageid");

        WEB_load_ItemSpecialOffer("BookName", PageNo, PreviousNo);
    });
    $("#a_previous").click(function () {
        NextPageCount--;
        PreviousPageCount--;
        $("#a_Next").attr('data-pageid', NextPageCount);
        $("#a_previous").attr('data-pageid', PreviousPageCount);

        $("#a_Nexttop").attr('data-pageid', NextPageCount);
        $("#a_previousTop").attr('data-pageid', PreviousPageCount);

        var PageNo = document.getElementById("a_Next").getAttribute("data-pageid");
        var PreviousNo = document.getElementById("a_previous").getAttribute("data-pageid");
        WEB_load_ItemSpecialOffer("BookName", PageNo, PreviousNo);
    });
});

function ResetPageNumber() {
    NextPageCount = 1;
    PreviousPageCount = 0;
    $("#a_Next").attr('data-pageid', NextPageCount);
    $("#a_previous").attr('data-pageid', PreviousPageCount);

    $("#a_Nexttop").attr('data-pageid', NextPageCount);
    $("#a_previousTop").attr('data-pageid', PreviousPageCount);
}

$(window).load(function () {
    var queryString = getQueryStrings();
    var langid = queryString["langid"];
    if (langid == undefined || langid == null) {
        langid = "";
    }
    if (langid == ""||langid == "Nil") {
        $("#ContentPlaceHolder1_ddl_Language").val("Nil");
    } else {
        $("#ContentPlaceHolder1_ddl_Language").val(langid);
    }    
    WEB_load_ItemSpecialOffer("BookName", 1, 0);
});