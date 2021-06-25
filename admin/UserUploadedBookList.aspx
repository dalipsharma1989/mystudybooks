<%@ Page MasterPageFile="~/AdminMaster.master" Language="C#" AutoEventWireup="true" CodeFile="UserUploadedBookList.aspx.cs" Inherits="admin_UserUploadedBookList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    
     <script type="text/javascript">
         
                    $(document).ready(function () {
                                $(function () {
                                    $("[id$=txtCustomer_Name]").autocomplete({
                                        source: function (request, response) {
                                            $.ajax({
                                                url: '<%=ResolveUrl("~/admin/view_users.aspx/GetDataList") %>',
                                                data: "{ 'prefix': '" + request.term + "'}",
                                                //data: "{'prefix': '" + request.term + "' , 'cmpOption': 'ALL', 'rb_SubjectID': '" + $('#ContentPlaceHolder1_rb_subjects').val() + "'}",
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
                                            $("[id$=hftextCustomer_Name]").val(i.item.val);
                                        },
                                        minLength: 1
                                    });
                                }); 
                        });
        </script>
    <style>
        .ui-autocomplete {
                            max-height: 450px;
                            overflow-y: scroll; /* prevent horizontal scrollbar */
                            overflow-x: hidden; /* add padding to account for vertical scrollbar */
                            z-index: 1000 !important;
                            max-width:750px;
                            font-size:15px;
                        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--PAGE TITLE-->
    <div class="row wrapper  border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2 id="h2_title" runat="server">Users Uploaded BookList </h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Admin</a>
                </li>
                <li class="active">
                    <strong id="strong_li_title" runat="server">Book List with School Class Name</strong>
                </li>
            </ol>
        </div>
    </div>
    <!--PAGE TITLE-->
     <div class="row wrapper  border-bottom white-bg page-heading" style="padding-top: 20px;display:none;">
        <div class="form-group" style="display:block">
            <label class="col-sm-2 control-label">
                <h2 style="margin: 0px;"><strong>Search</strong></h2> 
            </label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtCustomer_Name" class="form-control"  placeholder="Type here Username, Email and Mobile No....................." runat="server" ></asp:TextBox>
                <asp:HiddenField ID="hftextCustomer_Name" runat="server" />                    
            </div>
            <div class="col-sm-4">
                <asp:Button ID="btn_search_edit" class="btn btn-primary" OnClick="btn_search_edit_Click" runat="server" Text="Search" />
            </div>
        </div>        
    </div>
    <!--PAGE CONTENT-->
    <div class="row">
        <asp:PlaceHolder ID="ph_Msg" runat="server"></asp:PlaceHolder>
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView2" runat="server"  CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Record Found !"
                            AllowPaging="true" PageSize="20" OnPageIndexChanging="GridView2_PageIndexChanging"  PagerStyle-CssClass="asp-paging" AutoGenerateColumns="false" >                             
                            <Columns>
                                <asp:BoundField DataField="CustomerID" HeaderText="Customer Id"/>
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name"/>
                                <asp:BoundField DataField="SchoolName" HeaderText="School Name"/>
                                <asp:BoundField DataField="ClassName" HeaderText="Class Name"/>
                                <asp:BoundField DataField="LanguageName" HeaderText="Language Name"/>
                                <asp:BoundField DataField="Mobile" HeaderText="Mobile No"/>
                                <asp:BoundField DataField="EmailID" HeaderText="Email ID"/>                              
                                <asp:TemplateField HeaderText ="BookList" >
                                    <ItemTemplate> 
                                        <a href="#" onclick="Popout(this)"  data-toggle="modal" data-target="#modal_notify_me">
                                            <img src="<%# Eval("BookListPath") %>" class="img-responsive" style="border: 1px solid #ddd" />
                                        </a> 
                                    </ItemTemplate>
                                </asp:TemplateField>   
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <!--PAGE CONTENT-->

     <!-- Notify Me Modal Start -->
    <div id="modal_notify_me" class="modal" role="dialog" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 style="float:left;" class="modal-title">Book List</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">X</span></button>
                </div>
                <div class="modal-body">
                    <p style="font-size:16px !important;font-weight: 800;font-family: Arial, Helvetica, sans-serif !important;">User Book List</p>
                    <div class="form-horizontal" id="img_BookList"></div>
                </div>
                <%--<div class="modal-footer">
                    
                </div>--%>
            </div>

        </div>
    </div>
     <!-- Notify Me Modal End-->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function Popout(element) {
            //window.frames[0].document.images["frame_Image"].src = element.innerHTML;
            $('#img_BookList').empty();
            var ImagePath = element.innerHTML.replace("src=\"", "src=\"" + window.location.origin)
            var html = "<img   src='" + ImagePath + "' alt='BookList' title='BookList' onError=this.onerror=null;this.src='../resources/no-image.jpg';  />"
            $('#img_BookList').append(ImagePath);
            //var newTab = window.open();
            //setTimeout(function () {
            //    newTab.document.body.innerHTML = element.innerHTML.replace("src=\"", "src=\"" + window.location.origin);
            //}, 500); 
        }
    </script>
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>