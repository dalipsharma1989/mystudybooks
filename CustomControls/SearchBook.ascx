<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchBook.ascx.cs" Inherits="SearchBook" %>
    
<asp:Panel ID="Panel2" runat="server"  > 
        <style type="text/css">
            .search-div{
                display:flex;
            }
             .ui-autocomplete {
                    max-height: 450px;
                    overflow-y: scroll; /* prevent horizontal scrollbar */
                    overflow-x: hidden; /* add padding to account for vertical scrollbar */
                    z-index: 100000000 !important;
                    max-width:500px;
                    font-size:15px;
                }             
 
           @media only screen and (max-width:600px){
               .DivWaitingClassSearch {
                   left:147px !important;
               }
           }  
           @media only screen and (max-width:575px){
               .search-bar{
                       width: 100%;
                           margin-top: 15px;
               }
               .search-icon{
                   position:relative;
                   right:-9px;
               }
           }

           #SearchBook_btnSearch{
                border: none;
                box-shadow: none;
                    padding: 0 10px;
                height: 0px;
                width: 0px;
                outline:none;
           } 
           .DivWaitingClassSearch {
                position: relative;
                top: 8px;
                bottom: 0;
                height:0;
                left: 320px;
                right: 0;
                /*background: rgba(0, 0, 0, 0.7);*/
                transition: opacity 500ms;
                opacity: 1;
            }
            .loadersearch {
                border: 9px solid #000000;
                border-radius: 50%;
                border-top: 9px solid #3498db;
                width: 25px;
                height: 25px;
                -webkit-animation: spin 1s linear infinite; /* Safari */
                animation: spin 1s linear infinite;
            }
        </style>
        
         <script type="text/javascript"> 
             $(document).ready(function () { 
                 $(function () {
                     $("[id$=textSearchBooks]").autocomplete({
                         source: function (request, response) {
                             $.ajax({
                                 url: '<%= ResolveUrl("~/search_results.aspx/GetDataList") %>',
                                    //data: "{ 'prefix': '" + request.term + "'}",
                                 data: "{'prefix': '" + request.term + "' , 'cmpOption': 'Categories', 'rb_SubjectID': '" + $('#ContentPlaceHolder1_rb_subjects').val() + "', 'cmbcategory': '" + $('#SearchBook_drp_Categories').val() + "'}",
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: true,
                                    cache: false,
                                    beforeSend: function () {
                                        $("#WaitingMsglist").css("display", "block");
                                    },
                                    success: function (data) {
                                        response($.map(data.d, function (item) {
                                            return {
                                                label: item.split('|')[0],
                                                val: item.split('|')[1]
                                            }
                                        }))
                                    },
                                    complete: function (data) {
                                        $("#WaitingMsglist").css("display", "none");
                                    },
                                    error: function (response, url, data, dataType, type, contentType) {
                                        alert(response.responseText);
                                    },
                                    failure: function (response) {
                                        alert(response.responseText);
                                    }
                                });
                            },
                         select: function (e, i) {
                             window.location.href = "../search_results.aspx?inOutOS=1&q=" + $(i.item.val).find("span")[0].innerHTML;
                             e.preventDefault();//$("[id$=hftxtSearchBooks]").val(i.item.val);                                
                             $("#SearchBook_textSearchBooks").val($(i.item.val).find("span")[0].innerHTML);
                         },
                            minLength: 1
                     }).data("ui-autocomplete")._renderItem = function (ul, item) {
                         return $("<li>").append(item.val + "").appendTo(ul);
                     };
                 });


                 $("#SearchBook_textSearchBooks").keyup(function (e) {
                     if (e.keyCode == 13) {
                         if ($("#SearchBook_textSearchBooks").val().trim() == "") {
                             location.href = "../search_results.aspx";
                         } else {
                             __doPostBack('SearchBook_btnSearch', 'OnClick');
                             ResetPage_OnAtagClick();
                         }
                     }
                 });


                 var queryString = getQueryStrings();
                 var querytxt = queryString["q"];
                 if (querytxt == undefined || querytxt == null || querytxt == "") {
                     querytxt = "";
                 }
                 if (querytxt == "") {
                     $("#SearchBook_textSearchBooks").val("");
                 } else {
                     $("#SearchBook_textSearchBooks").val(querytxt);
                 }  
                 var querytxt = queryString["Cat"];
                 if (querytxt == undefined || querytxt == null || querytxt == "") {
                     querytxt = "";
                 }
                 if (querytxt == "" || querytxt == "Nil") {
                     $("#SearchBook_drp_Categories").val("Nil");
                 } else {
                     $("#SearchBook_drp_Categories").val(querytxt);
                 }

                  
             }); 

            </script>  
    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 search-div">
         <div id="WaitingMsglist" class="DivWaitingClassSearch" style="display:none;z-index:90000000;">
            <div style="width:0px;height:0px;border:1px  black;position:relative;top:0px;left:50%;padding:2px;z-index:1000001;color:yellowgreen;">                    
                <div class="loadersearch"></div>
            </div>
        </div>
        <asp:HiddenField ID="hftxtSearchBooks" runat="server" /> 
        <asp:TextBox style="flex-grow: 1" ID="textSearchBooks" autocomplete="off" placeholder="Search..." runat="server" CssClass="search-box form-control" OnTextChanged="textSearchBooks_TextChanged"></asp:TextBox>
        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="textSearchBooks" ValidationGroup ="searchBooks" CssClass="validator search-books" runat="server" ErrorMessage="<i class='fa fa-warning'></i>&nbsp;Required"></asp:RequiredFieldValidator>--%>
         <asp:DropDownList AutoPostBack="false" DataSourceID="SqlDataSource3" DataTextField="BookCategoryDesc" DataValueField="BookCategoryID"
              style="flex-grow: 1" ID="drp_Categories" class="search-iconCateg hidden-xs" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Text="All Categories" Value="Nil"></asp:ListItem>
         </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource3" ConnectionString="<%$ ConnectionStrings:ConStr %>" ProviderName="System.Data.SqlClient" runat="server"
            SelectCommand="web_GetBookCategory" SelectCommandType="StoredProcedure" >
            <SelectParameters> 
                <asp:SessionParameter Name="iCompanyID" SessionField="iCompanyID" />
                <asp:SessionParameter Name="iBranchID" SessionField="iBranchID" />
            </SelectParameters> 
        </asp:SqlDataSource>       
        <div class="search-icons" style="flex-grow: 1"  >
            <asp:LinkButton ID="btnSearch" causesvalidation="false" runat="server"     OnClick="btnSearch_Click"> 
                <i  class="fa fa-search"></i>                            
            </asp:LinkButton>               
        </div>
        </div>
</asp:Panel>
