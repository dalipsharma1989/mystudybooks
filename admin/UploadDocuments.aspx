<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="UploadDocuments.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .file .icon, .file .image {
            height: 200px !important;
            overflow: hidden;
        }
        .rules-heading{
                text-align: center;
                 display: block;
                font-size: 25px;
                border-bottom: 4px double #3051a0;
                 margin: auto;
                 width: 107px;
                 margin-bottom: 20px;
        }
        .list-items{
                margin-bottom: 10px;
                font-family: cursive;
        }
        .file-button{
                background: #3051a0 !important;
               color: white !important;
              border: 1px solid #3051a0 !important;
        }
         .file-button:hover{
                background: white !important;
               color: #3051a0 !important;
              border: 1px solid #3051a0 !important;
        }
         .file{
             padding:20px !important;
             box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);
         }
         .f-name{
                 border-top: 1px solid #3051a0 !important;
                 background:none !important;
                 text-align:center;
         }
         .btn-delete{
             margin-top: 10px;
         }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-9">
            <h2 style="font-weight: 600;font-size: 29px;">Document Manager</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="dashboard.aspx">Dashboard</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Document Manager</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="wrapper wrapper-content">
        <div class="row">
            <div class="col-lg-3">
                <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="file-manager">
                            <strong class="rules-heading">Rules</strong>
                            <ol >                                
                                <li class="list-items">Extensions allowed: <b>.pdf, .xls, .xlsx</b></li>
                                <li class="list-items">Documents Name: for add checklist then Name Should be <b>checklist_excel and checklist_pdf</b></li>
                                <li  class="list-items">for add Catalogue then Name Should be <b>Catalogue</b></li>
                                <li  class="list-items">for add Polytechnic Catalogue then Name Should be <b>Polytechnic_Catalogue</b></li>
                            </ol>
                            
                            <asp:FileUpload ID="FileUpload1" class="form-control" style="height: 41px;" runat="server" AllowMultiple="true" />
                            <br />
                            <asp:Button ID="btn_uploadFiles" runat="server" OnClick="btn_uploadFiles_Click" CssClass="btn btn-default file-button" Text="Upload Files" />

                            <div class="hr-line-dashed"></div> 
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-9 animated fadeInRight">
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>

                        <asp:Repeater ID="rp_media" runat="server" OnItemCommand="rp_media_ItemCommand">
                            <ItemTemplate>
                                <div class="file-box">
                                    <div class="file">

                                        <span class="corner"></span>
                                        <div class="image">
                                            <a href="<%# Eval("FilePath") %>" data-title="<%# Eval("FileName") %>" data-lightbox="Slider_listview" target="_blank">
                                                <img alt="image" class="img-responsive" src="<%# Eval("IconPath") %>">
                                            </a>
                                        </div>
                                        <div class="file-name f-name">
                                            <a href="<%# Eval("FilePath") %>" target="_blank" data-title="<%# Eval("FileName") %>" data-lightbox="Slider_listview1"> <%# Eval("FileName") %>
                                            </a>
                                            <br>
                                            <small>Added: <%# Eval("DateCreated","{0:MMM dd, yyyy}") %></small><br />
                                            <asp:LinkButton ID="lb_delete" runat="server" class="btn btn-xs btn-outline btn-danger btn-delete"
                                                OnClientClick="javascript:return confirm('Do you really want to delete this Image ?');"
                                                CommandName="delete_image" CommandArgument='<%# Eval("FilePath") %>'>
                                                <i class="fa fa-trash"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

