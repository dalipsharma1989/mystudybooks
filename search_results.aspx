<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="search_results.aspx.cs" Inherits="_Default" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/owl.carousel.css" rel="stylesheet" />
    <link href="css/owl.theme.default.min.css" rel="stylesheet" />  
   
    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <link href="css/customecss/style.css" rel="stylesheet" />
    <link href="css/customecss/homrPage.css" rel="stylesheet" />
<%--    <link href="css/Ver_1.0/SearchFormCss.css" rel="stylesheet" />--%>
    <link href="css/Ver_1.0/searchingCSS.css" rel="stylesheet" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">  
            $("#WaitingMsg").css("display", "block");
            $(document).ready(function () {                 
                //Load_Categories();                
                //Load_SubjectAsCategories();
                //Load_BooksClasses();
                //Load_Books_Medium();
                //Load_Publishers();
                WEB_load_ItemSpecialOffer("BookName", 1, 0);
                Load_AuthorList();
               
            });
            

        $(document).ready(function () { 
                $(function () { 
                   $("[id$=txtSearch]").autocomplete({                                
                        source: function (request, response) {
                            $.ajax({
                                url: '<%= ResolveUrl("~/search_results.aspx/GetDataList") %>',
                                data: "{'prefix': '" + request.term + "' , 'cmpOption': '" + $('#ContentPlaceHolder1_cmpOption').val() + "', 'rb_SubjectID': '" + $('#ContentPlaceHolder1_rb_subjects').val() + "'}",
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    response($.map(data.d, function (item) {
                                        return {
                                            label: item.split('|')[0],
                                            val: item.split('|')[1]
                                        }
                                    }))
                                },
                                error: function (response) {
                                    alert(response.responseText);
                                },
                                failure: function (response) {
                                    alert(response.responseText);
                                }
                            });
                        },
                        select: function (e, i) {
                            $("[id$=hftextSearchBooks]").val(i.item.val);
                        },
                        minLength: 1
                    });
                });

                $("#ContentPlaceHolder1_txtSearch").keypress(function (event) {
                    if (event.keyCode == 13) {
                        $("#ContentPlaceHolder1_txtsearchItem").trigger("click");
                    }
                });

                if ($(window).width() > 768 && $(window).width() < 1024 ) {
                    $(".radgrid").items = 4;
                }
                
            }); 

        </script>  
        <script>
            function filterList(ref) {
                var listbox = "";
                var textbox = ref.value;
                <%--if (ref.id == "ContentPlaceHolder1_txtSearchCategory") {
                    listbox = $find("<%= rlb_category.ClientID %>");        
                } else if (ref.id == "ContentPlaceHolder1_txtSearchPublisher") {
                    listbox = $find("<%= rlb_Publisher.ClientID %>");        
                } else if (ref.id == "ContentPlaceHolder1_txtSearchAuthor") {
                    listbox = $find("<%= rlb_Author.ClientID %>");        
                }else if (ref.id == "ContentPlaceHolder1_TextBoxA4") {
                    listbox = $find("<%= RadListBoxA1.ClientID %>");        
                }--%>
                    clearListEmphasis(listbox);
                    createMatchingList(listbox, textbox);
                } 
 
        // Remove emphasis from matching text in ListBox
        function clearListEmphasis(listbox) {
            var re = new RegExp("</{0,1}em>", "gi");
            var items = listbox.get_items();
            var itemText;
 
            items.forEach
            (
                function (item)
                {
                    itemText = item.get_text();
                    item.set_text(itemText.replace(re, ""));
                }
            )
        }
 
        // Emphasize matching text in ListBox and hide non-matching items
        function createMatchingList(listbox, filterText) {
            if (filterText != "")
            {
                filterText = escapeRegExCharacters(filterText);
 
                var items = listbox.get_items();
                var re = new RegExp(filterText, "i");
 
                items.forEach
                (
                    function (item)
                    {
                        var itemText = item.get_text();
 
                        if (itemText.match(re))
                        {
                            item.set_text(itemText.replace(re, "<em>" + itemText.match(re) + "</em>"));
                            item.set_visible(true);
                        }
                        else
                        {
                            item.set_visible(false);
                        }
                    }
                )
            }
            else
            {
                var items = listbox.get_items();
 
                items.forEach
                (
                    function (item)
                    {
                        item.set_visible(true);
                    }
                )  
            }
        }
 
        function rlbAvailable_OnClientTransferring(sender, eventArgs) {
            // Transferred items retain the emphasized text, so it needs to be cleared.
            clearListEmphasis(sender);
            // Clear the list. Optional, but prevents follow up situation.
            clearFilterText();
            createMatchingList(sender, "");
        }
 
      function rbtnClear_OnClientClicking(sender, eventArgs) {
            clearFilterText();
 
            var listbox = $find("<%= txtSearchCategory.ClientID %>");
                 
            clearListEmphasis(listbox);
            createMatchingList(listbox, "");
        }
             
        // Clears the text from the filter.
        function clearFilterText() {
            var textbox = $find('<%= txtSearchCategory.ClientID %>');
            textbox.clear();
        }
 
        // Escapes RegEx character classes and shorthand characters
        function escapeRegExCharacters(text) {
            return text.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
        }


        function slidePanelCategory() {
               var div = "dv_Category";
                    if ($('#' + div).css('display') == 'none') {
                        $('#' + div).slideDown('medium', function () { });
                        var atag =  document.getElementById("a_collapse");
                        atag.innerHTML = "<i class='fa fa-minus-circle infoTag'></i>";// + $('#collapse_info').addClass("fa fa-minus-circle");
                    } else {
                        $('#' + div).slideUp('medium', function () { });
                        //$('#collapse_info').addClass("fa fa-plus-circle");
                        var atag = document.getElementById("a_collapse");
                        atag.innerHTML = "<i class='fa fa-plus-circle infoTag'></i>";// + $('#collapse_info').addClass("fa fa-plus-circle");
                        //$('#a_collapse').innerHTML = "Collapse out";
                        
                    }
                }
        function slidePanelPublisher() {
            var div = "dv_Publisher";
            if ($('#' + div).css('display') == 'none') {
                $('#' + div).slideDown('medium', function () { });
                var atag = document.getElementById("a_dv_Publisher");
                atag.innerHTML = "<i class='fa fa-minus-circle infoTag'></i>";// + $('#collapse_info').addClass("fa fa-minus-circle");
            } else {
                $('#' + div).slideUp('medium', function () { });
                //$('#collapse_info').addClass("fa fa-plus-circle");
                var atag = document.getElementById("a_dv_Publisher");
                atag.innerHTML = "<i class='fa fa-plus-circle infoTag'></i>";// + $('#collapse_info').addClass("fa fa-plus-circle");
                //$('#a_collapse').innerHTML = "Collapse out";

            }
        }
        function slidePanelAuthor() {
            var div = "dv_Authorlist";
            if ($('#' + div).css('display') == 'none') {
                $('#' + div).slideDown('medium', function () { });
                var atag = document.getElementById("a_dv_Authorlist");
                atag.innerHTML = "<i class='fa fa-minus-circle infoTag'></i>";// + $('#collapse_info').addClass("fa fa-minus-circle");
            } else {
                $('#' + div).slideUp('medium', function () { });
                //$('#collapse_info').addClass("fa fa-plus-circle");
                var atag = document.getElementById("a_dv_Authorlist");
                atag.innerHTML = "<i class='fa fa-plus-circle infoTag'></i>";// + $('#collapse_info').addClass("fa fa-plus-circle");
                //$('#a_collapse').innerHTML = "Collapse out";

            }
        }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
		<!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area"> 
			<div class="row">
				<div class="col-lg-12">
					<div style="margin-left:12px;padding:20px 0 0px 0;" class="breadcrumbs-menu">
						<ul>
							<li><a href="/">Home</a></li>
							<li><a href="#" class="active">Search Results</a></li>
                            <li style="color:seagreen" ><span id="showResultMsgs"></span></li>
						</ul>
					</div>
				</div>
			</div> 
		</div>
		<!-- breadcrumbs-area-end -->
		<!-- shop-main-area-start -->
		<div class="shop-main-area mb-70">
			<div class="" style="padding:0.5% 2% 2% 2%;">
				<div class="row">
                    <div class="col-sm-12" style="text-align:center;">
                        <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
                        <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
                    </div>
                    <!-- Left Column -->
                    <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12">
                        <button type="button" class=" m-4 btn btn-primary toggleFilter" style="display:none;">Show Filters</button>
                        <div id="filter" class="filter"> 
						<div class="shop-left">
                            <div class="" style="display:none;">
                                <div class="select-language" style="border-radius:5px;">
                                    <span>Title</span>
                                </div>
                                <div class="side-filter" style="height:auto">
                                    <div>
                                        <asp:DropDownList style="height: 40px;width: 100%;margin: auto" class="form-control medium" ID="ddl_Language" runat="server" 
                                            AutoPostBack="false" DataTextField="LanguageName" DataValueField="LanguageID" DataSourceID="SqlDataSource4"  
                                            OnSelectedIndexChanged="ddl_Language_SelectedIndexChanged" AppendDataBoundItems="true" > 
                                            <asp:ListItem Text="All Titles" Value="Nil"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource4" ConnectionString="<%$ ConnectionStrings:ConStr %>" ProviderName="System.Data.SqlClient" runat="server"
                                                SelectCommand="web_GetMasterLanguage" SelectCommandType="StoredProcedure" >
                                                <SelectParameters> 
                                                    <asp:SessionParameter Name="iCompanyID" SessionField="iCompanyID" />
                                                    <asp:SessionParameter Name="iBranchID" SessionField="iBranchID" />
                                                </SelectParameters> 
                                            </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div> 
                            <section class="widget">
                                <div class="select-language" style="border-radius:5px;">
                                    <span style="padding-right: 171px;font-weight: 700;">Categories</span> 
                                    <a  class="btn btn-primary collapseATag"  id="a_collapse" onclick="slidePanelCategory();">
                                        <i id="collapse_info" class="fa fa-minus-circle infoTag"></i> 
                                    </a>
                                </div>
                                <div class="SearchWraper" style="display:none;">
                                    <asp:TextBox runat="server" ID="txtSearchCategory" Text="" autocomplete="off" onkeyup="filterList(this);" CssClass="form-control" placeholder="Search Category here....." ></asp:TextBox>  
                                    <i class="fa fa-search"></i>
                                </div>
                                <div id="dv_Category"></div> 
                            </section>
                            <section class="widget"  style="display:none;">
                                <div class="select-language" style="border-radius:5px;">
                                    <span>Class</span> 
                                </div>
                                <div class="SearchWraper" style="display:none;">
                                    <asp:TextBox runat="server" ID="TextBox1" Text="" autocomplete="off" onkeyup="filterList(this);" CssClass="form-control" placeholder="Search Category here....." ></asp:TextBox>  
                                    <i class="fa fa-search"></i>
                                </div>
                                <div id="dv_Classes"></div> 
                            </section>
                            <section class="widget" style="display:none;">
                                <div class="select-language" style="border-radius:5px;">
                                    <span>Language</span>
                                </div>
                                <div class="SearchWraper" style="display:none;">
                                    <asp:TextBox runat="server" ID="TextBox2" Text="" autocomplete="off" onkeyup="filterList(this);" CssClass="form-control" placeholder="Search Category here....." ></asp:TextBox>  
                                    <i class="fa fa-search"></i>
                                </div>
                                <div id="dv_Medium"></div> 
                            </section>
                            <section class="widget"  style="display:none;">
                                <div class="select-language" style="border-radius:5px;">
                                    <span>Subjects</span>
                                </div>
                                <div class="SearchWraper" style="display:none;">
                                    <asp:TextBox runat="server" ID="txt_Subject" Text="" autocomplete="off" onkeyup="filterList(this);" CssClass="form-control" placeholder="Search Category here....." ></asp:TextBox>  
                                    <i class="fa fa-search"></i>
                                </div>
                                <div id="dv_Subject"></div> 
                            </section>
                            <section class="widget" >
                                <div class="select-language" style="border-radius:5px;">
                                    <span style="padding-right: 180px; font-weight: 700;">Publisher</span>
                                    <a  class="btn btn-primary collapseATag" id="a_dv_Publisher" onclick="slidePanelPublisher();">
                                        <i class="fa fa-minus-circle infoTag"></i> 
                                    </a>
                                </div>
                                <div class="SearchWraper" style="display:none;">
                                    <asp:TextBox runat="server" ID="txtSearchPublisher" autocomplete="off" onkeyup="filterList(this);"  CssClass="form-control" Text=""  placeholder="Search Publisher here....." ></asp:TextBox> 
                                    <i class="fa fa-search"></i>
                                </div> 
                                <div id="dv_Publisher"></div> 
                            </section>
                            <section class="widget" >
                                <div class="select-language" style="border-radius:5px;">
                                    <span style="padding-right: 199px;font-weight: 700;">Author</span>
                                    <a  class="btn btn-primary collapseATag" id="a_dv_Authorlist" onclick="slidePanelAuthor();">
                                        <i class="fa fa-minus-circle infoTag"></i> 
                                    </a>
                                </div>
                                <div class="SearchWraper" style="display:none;">
                                    <asp:TextBox runat="server" ID="txtSearchAuthor" Text="" autocomplete="off" onkeyup="filterList(this);"  
                                    CssClass="form-control"  placeholder="Search Author here....." ></asp:TextBox>
                                    <i class="fa fa-search"></i>
                                </div>
                                <div id="dv_Authorlist" ></div> 
                            </section> 

                            <div class="" style="position:fixed;bottom:0;left: 50%;transform: translateX(-50%);">
                                <button id="" type="button" class="btn btn-primary toggleFilter" style="display:none">Done</button>
                            </div>

						</div>
                    
                        </div>
                    </div>
                    <!--/Left Column-->
                    <!-- Right Column -->
                    <div class="col-lg-9 col-md-9 col-sm-8 col-xs-12">
                        <%--<div class="col-lg-12 col-md-12">
                            <div class="shop-toolbar padding-bottom-1x mb-2">
                                <div class="column">
                                    <div class="shop-sorting">
                                        <label for="sorting">Sort by:</label>                                    
                                        <select class="form-control" id="sorting" style="height:30px;">
                                        <option>Book Name</option>
                                        <option>Author</option>
                                        <option>Publisher</option>
                                        <option>ISBN</option>
                                        <option>Price</option>
                                        <option>Clear sort</option>
                                        </select>
                                        <select class="form-control" id="sortingAcenDesc" style="height:30px;">
                                            <option>Ascending</option>
                                            <option>Descending</option>
                                        </select>
                                        <a id="a_clear" href="search_results.aspx" class="btn btn-outline-secondary btn-sm">Clear Filter&nbsp;/&nbsp;Reset</a>
                                        <span style="display:none;" class="text-muted">Showing:&nbsp;</span><span style="display:none;">1 - 20 items</span>
                                        <a id="a_Nexttop" data-pageid="1" class="btn btn-outline-secondary btn-sm">Next&nbsp;<i class="fa fa-arrow-right"></i></a>
                                        <a id="a_previousTop" data-pageid="0" class="btn btn-outline-secondary btn-sm"><i class="fa fa-arrow-left"></i>&nbsp;Previous</a>                                        
                                    </div>
                                </div>
                                <div class="column" style="display:none;">
                                    <div class="shop-view">
                                        <a class="grid-view active" href="#">
                                            <span></span>
                                            <span></span>
                                            <span></span> 
                                        </a> 
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <div class="shop-sorting">
                               <label for="sorting">Sort by:</label>                                    
                                        <select class="form-control" id="sorting" style="height:30px;width:135px;">
                                        <option>Book Name</option>
                                        <option>Author</option>
                                        <option>Publisher</option>
                                        <option>ISBN</option>
                                        <option>Price</option>
                                        <option>Clear sort</option>
                                        </select>
                                        <select class="form-control" id="sortingAcenDesc" style="height:30px;width:135px;">
                                            <option>Ascending</option>
                                            <option>Descending</option>
                                        </select> 
                                        <span style="display:none;" class="text-muted">Showing:&nbsp;</span><span style="display:none;">1 - 20 items</span>                            
                                   
                           <div style="display:none;">
                            <input type="text" placeholder="Search Here..." class="form-control" id="inp_Search" /> 
                               <a  id="a_searchhere" onclick="return WEB_load_ItemSpecialOffer('BookName', 1, 0);"   class="btn btn-outline-secondary btn-sm" >
                                   <i class="fa fa-search"></i>
                               </a>  
                           </div>
                            <div class="ml-2 form-check" style="display: flex;flex-direction: row;margin-left: 10px !important;display:none;">
                               <input type="checkbox" class="form-check-input" style="position: absolute;left: 0;width: 20px;height: 20px;" id="stock" name="stock" value="stock">
                                <label class="ml-4 form-check-label" style="font-size: 17px;padding: 0px;" for="vehicle1"> Include out of stock</label><br>
                           </div>
                            
                            
                            <a id="a_clear" href="search_results.aspx" class="btn btn-outline-secondary btn-sm">Clear Filter&nbsp;/&nbsp;Reset</a>
                        </div>                        
                     
                        <div class="d-flex justify-content-between">
                            <a id="a_previousTop" data-pageid="0" class="btn btn-outline-secondary btn-sm"><i class="fa fa-arrow-left"></i>&nbsp;Previous</a>   
                            <a id="a_Nexttop" data-pageid="1" class="btn btn-outline-secondary btn-sm">Next&nbsp;<i class="fa fa-arrow-right"></i></a>
                             
                        </div>
                        <div class="row">
                            <div id="ul_ShopList" style="display: flex;flex-wrap: wrap;justify-content: center;"></div> 
                        </div> 
                        <div class="col-lg-12 col-md-12"> 
                            <nav class="pagination">
                                <div class="column text-left hidden-xs-down">
                                    <a id="a_previous" data-pageid="0" class="btn btn-outline-secondary btn-sm"><i class="fa fa-arrow-left"></i>&nbsp;Previous</a>
                                </div>
                                <div class="column" style="display:none;">
                                    <ul class="pages">
                                        <li class="active">
                                            <a href="#">1</a>
                                        </li>
                                        <li>
                                            <a href="#">2</a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="column text-right hidden-xs-down">
                                    <a id="a_Next" data-pageid="1" class="btn btn-outline-secondary btn-sm">Next&nbsp;<i class="fa fa-arrow-right"></i></a>
                                </div>
                            </nav>
                        </div>  
                    </div>
                </div>
            </div>
        </div>
    <%--</ContentTemplate>
 </asp:UpdatePanel>  --%>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <script src="js/1.0.6/SearchingJs.min.js"></script>
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>