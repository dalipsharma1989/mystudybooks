<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="TodayQuote.aspx.cs" Inherits="_TodayQuote_page" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="/tinymce/tinymce.min.js"></script>
    <script>
        tinymce.init({
            selector: "textarea",
            theme: 'modern',
            plugins: [
              'advlist autolink lists link image charmap print preview hr anchor pagebreak',
              'searchreplace wordcount visualblocks visualchars code fullscreen',
              'insertdatetime media nonbreaking save table contextmenu directionality',
              'emoticons template paste textcolor colorpicker textpattern imagetools'
            ],
            fontsize_formats: '8pt 10pt 12pt 14pt 18pt 24pt 36pt',
            toolbar1: 'insertfile undo redo | styleselect | bold italic fontsizeselect | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            toolbar2: 'print preview media | forecolor backcolor emoticons',
            image_advtab: true,
            relative_urls: false,
            remove_script_host: false,
            convert_urls: true,
            toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image"
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
            .save-button{
                background:#3051a0 !important;
                color:white !important;
                border:1px solid #3051a0 !important;
            }
             .save-button:hover{
                background:white !important;
                color:#3051a0 !important;
                border:1px solid #3051a0 !important;
            }
                .edit-btn{
                background:#3051a0 !important;
                color:white !important;
                border:1px solid #3051a0 !important;
            }
                  .edit-btn:hover{
                background:white !important;
                color:#3051a0 !important;
                border:1px solid #3051a0 !important;
            }
    </style>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">Quote</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong style="color:#3051a0">Quote</strong></li>
            </ol>
        </div>
        <div class="col-lg-2">
            <div class="title-action">
                <a href="TodayQuote.aspx" class="btn btn-info"><i class="fa fa-plus"></i>&nbsp;Add New</a>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
        <div class="row">
            <div class="col-lg-12">
                <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Add Today Quote</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <asp:HiddenField ID="hf_menuID" runat="server" />
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Quote Heading</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="txtquoteHeading" placeholder="Quote Heading" class="form-control" runat="server"></asp:TextBox>                                    
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Name</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textName" placeholder="Name" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validator" ControlToValidate="textName">
                                        <i class="fa fa-warning"></i>&nbsp;Required </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group init-validator" style="display:none;">
                                <label class="col-sm-2 control-label">Type:</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="dd_type" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="dd_type_SelectedIndexChanged">
                                        <asp:ListItem Value="Page">Page</asp:ListItem>
                                        <asp:ListItem Value="Product Grid">Product Grid</asp:ListItem>
                                        <asp:ListItem Value="Link">Link</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">Header Content</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textHeaderContent" placeholder="Header Content"
                                        class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-lg-2 control-label">Main Content</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textmainContent" placeholder="Main Content" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox> 
                                </div>
                            </div>

                            <div class="form-group" style="display:none;">
                                <label class="col-lg-2 control-label">Sort Order</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textSortOrder" placeholder="Sort Order" Text="1" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" CssClass="validator" ControlToValidate="textSortOrder" MinimumValue="0" 
                                        MaximumValue="100" Type="Integer"><i class="fa fa-warning"></i>&nbsp;Value must be in between 0 - 100</asp:RangeValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" class="btn btn-sm btn-success save-button" Text="Save" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_menu" runat="server" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Record Found !" 
                                AutoGenerateColumns="false" PagerStyle-CssClass="asp-paging" OnPageIndexChanging="grd_menu_PageIndexChanging" OnRowCommand="grd_menu_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Quote Heading">
                                        <ItemTemplate>
                                            <%# Eval("QuoteHeading") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Title">
                                        <ItemTemplate>
                                            <a href="TodayQuote.aspx?TodayQuoteid=<%# Eval("TodayQuoteID") %>">
                                                <%# Eval("Name") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Header Content">
                                        <ItemTemplate> 
                                                <%# Eval("HeaderContent") %> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Header Content">
                                        <ItemTemplate> 
                                                <%# Eval("MainContent") %> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <%# Eval("Type") %>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("TodayQuoteID")%>' CommandName="delete_record"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                                runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                            <a href="TodayQuote.aspx?TodayQuoteid=<%# Eval("TodayQuoteID") %>" data-toggle="tootlip" title="Edit" class="btn btn-xs btn-success edit-btn">
                                                <i class="fa fa-edit"></i>
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

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>


