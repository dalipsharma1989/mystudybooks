<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="media_backcover.aspx.cs" Inherits="_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .file .icon, .file .image {
            height: 200px !important;
            overflow: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-9">
            <h2>Upload Back Covers</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="dashboard.aspx">Dashboard</a>
                </li>
                <li>
                    <a href="media.aspx">Media Manager</a>
                </li>
                <li class="active">
                    <strong>Upload Back Covers</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="wrapper wrapper-content">
        <div class="row">
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="file-manager">
                            <strong>Rules</strong>
                            <ol>
                                <li>Max <b>1000</b> files allowed </li>
                                <li>Image Size: <b>30 KB to 250 KB</b></li>
                                <li>Image Resolution:  </li>
                                <li>Extensions allowed: <b>.png, .jpg, .bmp</b></li>
                                <li>Preffered Extension: <b>.png</b></li>
                                <li class="text-danger">Process may take time (depending upon physical size and resolution of images), so please wait patiently</li>
                            </ol>

                            <asp:FileUpload ID="FileUpload1" class="form-control" runat="server" AllowMultiple="true" />
                            <br />
                            <div><b>Options</b></div>
                            <asp:RadioButtonList ID="rb_options" runat="server">
                                <asp:ListItem Selected="True" Value="Auto-Resize">Auto-Resize</asp:ListItem>
                                <asp:ListItem Value="Fixed-Resize">Fixed-Resize</asp:ListItem>
                            </asp:RadioButtonList>
                            <p class="help-block text-muted">Auto-Resize Width - 200px</p>
                            <p class="help-block text-muted">Fixed-Resize Width - 200px</p>
                            <p class="help-block text-muted">Fixed-Resize Height - 300px</p>
                            <br />

                            <asp:Button ID="btn_uploadFiles" runat="server"
                                OnClick="btn_uploadFiles_Click"
                                CssClass="btn btn-default" Text="Upload Files" />

                            <div class="hr-line-dashed"></div>

                            <%--<h5>Options</h5>
                                <ul class="folder-list" style="padding: 0">
                                <li>
                                    <asp:CheckBox ID="CheckBox1" Text="Auto Compress Images" runat="server" />&nbsp;
                                    <span class="label label-default" data-toggle="tooltip"
                                        title="Automatically Resize Images"><i class="fa fa-info"></i></span>
                                </li>
                                <li>
                                    <asp:CheckBox ID="CheckBox2" Text="Fixed Compress" runat="server" />&nbsp;<i data-toggle="tooltip"
                                        title="Resize Images to Fixed Height and Width"
                                         class="fa fa-info"></i></li>
                                <li>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem>250 X 300</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                
                            </ul>--%>

                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-9 animated fadeInRight">
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
                        <div class="ibox float-e-margins">
                            <div class="ibox-content">
                                <div class="input-group">
                                    <asp:TextBox ID="txtsearch" runat="server" placeholder="Search Book Image" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="lb_search_isbn_image" runat="server" CssClass="btn btn-success" OnClick="lb_search_isbn_image_Click">
                                        <i class="fa fa-search"></i>&nbsp;Search</asp:LinkButton>
                                    </span>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="grd_img" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" AllowPaging="true" PageSize="100"
                                        PagerStyle-CssClass="asp-paging" AutoPostBack="true" OnPageIndexChanging="grd_img_PageIndexChanging" OnRowCommand="grd_img_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <div class="image">
                                                        <a href="<%# Eval("FilePath") %>" data-title="<%# Eval("FileName") %>" data-lightbox="Slider_listview">
                                                            <img alt="image" class="img-responsive" src="<%# Eval("FilePath") %>">
                                                        </a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FileName" HeaderText="Filename" />

                                            <asp:TemplateField HeaderText="Date Created" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <%# Eval("DateCreated","{0:MMM dd, yyyy}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_delete" runat="server" class="btn btn-xs btn-outline btn-danger"
                                                        OnClientClick="javascript:return confirm('Do you really want to delete this Image ?');"
                                                        CommandName="delete_image" CommandArgument='<%# Eval("FilePath") %>'>
                                                <i class="fa fa-trash"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <%--<asp:Repeater ID="rp_media" runat="server" OnItemCommand="rp_media_ItemCommand">
                            <ItemTemplate>
                                <div class="file-box">
                                    <div class="file">

                                        <span class="corner"></span>
                                        <div class="image">
                                            <a href="<%# Eval("FilePath") %>" data-title="<%# Eval("FileName") %>" data-lightbox="Slider_listview">
                                                <img alt="image" class="img-responsive" src="<%# Eval("FilePath") %>">
                                            </a>
                                        </div>
                                        <div class="file-name">
                                            <a href="<%# Eval("FilePath") %>" data-title="<%# Eval("FileName") %>" data-lightbox="Slider_listview1">
                                                <%# Eval("FileName") %>
                                            </a>
                                            <br>
                                            <small>Added: <%# Eval("DateCreated","{0:MMM dd, yyyy}") %></small><br />
                                            <asp:LinkButton ID="lb_delete" runat="server" class="btn btn-xs btn-outline btn-danger"
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
                        </asp:Repeater>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

