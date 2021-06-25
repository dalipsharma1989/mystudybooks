
/* Cart and wishlisht*/
function ModalEntry(ProductID) {
    $.ajax({
        type: "POST",
        url: "index.aspx/AddDataintoCart",
        data: "{'ProductID': '" + ProductID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d.Result == "success") {
                $("#cart_qty").text(data.d.CartItems);
                $("#Span1").text(data.d.CartItems);
                $("#ltr_scripts").append(data.d.litmsg);
                toastr.success("successfully added to in your cart !", "Cart");
                //alert("Item successfully added to in your cart");
            } else {
                toastr.error(data.d.Result, "Cart");
            }
        },
        failure: function (response) {
            alert('Failed to Add item into cart');
            alert(response.d);
        }

    });
    return false;
}

function userwishlist(ProductID) {
    $.ajax({
        type: "POST",
        url: "index.aspx/AddDataInToWishList",
        data: "{'ProductID': '" + ProductID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d.Result == "success") {
                toastr.success("Item added to wishlist !", "Wishlist");
            } else if (data.d.Result == "") {
                if (data.d.uriDirect != "") {
                    window.location = data.d.uriDirect;
                }
            } else {
                toastr.error(data.d.Result, "Wishlist");
            }
        },
        failure: function (response) {
            alert('Failed to Add item into wishlist');
            alert(response.d);
        }

    });
    return false;
}


function Load_Language() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_Language",
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
                var HtmlRow = "<li><a class='actives' href='../search_results.aspx?langid=" + DataList[i].LanguageID + "&langnm=" + DataList[i].LanguageName + "'>" + DataList[i].LanguageName + "</a>";
                $("#ul_Mobile").append(HtmlRow);
                $("#ul_Language").append(HtmlRow);               
            } 
        },
        failure: function (response) {
            alert("Error coming in Catgeory");
        },
        error: function (error) {
            alert("Error coming in Catgeory");
        }
    });
}

function Load_MainMenu() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_MainMenu",
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
                var HtmlRow = "<li><a class='actives' href='../menu.aspx?menuid=" + DataList[i].MenuID + "&type=" + DataList[i].Type + "'>" + DataList[i].Name + "</a></li>";                
                $("#li_MainMenu").append(HtmlRow);
                $("#li_main_menu_mobile").append(HtmlRow);
            }
        },
        failure: function (response) {
            alert("Error coming in Catgeory");
        },
        error: function (error) {
            alert("Error coming in Catgeory");
        }
    });
}

function Load_FooterContent() {
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_FooterContent",
        data: "{'ParentID': '0','Action': '0'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            for (var i = 0; i < TotLen; i++) {
                var HtmlRow = "<div class='col-lg-3 col-md-12 col-sm-12'><div class='col-lg-12 col-md-12 col-sm-12 footer-heading'>" + DataList[i].Name + "</div>" +
                    "<div class='col-lg-12 col-md-12 col-sm-12 footer-content'><ul style='list-style:none;padding-inline-start: 0px;'>" + Load_FooterChild(DataList[i].TopicID) + "</ul></div></div>";

                $("#dv_Footer").append(HtmlRow);
            }
        },
        failure: function (response) {
            alert("Error coming in Catgeory");
        },
        error: function (error) {
            alert("Error coming in Catgeory");
        }
    });
}

function Load_FooterChild(ParentID) {
    var ResultRow=""; 
    $.ajax({
        type: "POST",
        url: "index.aspx/WEB_load_FooterContent",
        data: "{'ParentID': '" + ParentID + "','Action': '2'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (data) {
            var DataList = JSON.parse(data.d);
            var TotLen = DataList.length;
            var Count = 0;
            for (var i = 0; i < TotLen; i++) {
                var HtmlRow = "<li><a class='anchor-tag' href='../topics.aspx?topicid=" + DataList[i].TopicID + "'>" + DataList[i].Name + "</a></li>";    
                ResultRow=ResultRow+HtmlRow;
            }
        },
        failure: function (response) {
            alert("Error coming in Catgeory");
        },
        error: function (error) {
            alert("Error coming in Catgeory");
        }
    });
    return ResultRow;
}



function getQueryStrings() {
    var assoc = {};
    var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
    var queryString = location.search.substring(1);
    var keyValues = queryString.split('&');

    for (var i in keyValues) {
        var key = keyValues[i].split('=');
        if (key.length > 1) {
            assoc[decode(key[0])] = decode(key[1]);
        }
    }

    return assoc;
}