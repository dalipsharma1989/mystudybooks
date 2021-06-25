<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="categories.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .grid-image {
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Categories</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong>Categories</strong></li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Add Categories</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Category Name</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textCategoryName" placeholder="Category Name" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>



                            <div class="form-group" id="div_form_grp_subcategory" runat="server">
                                <label class="col-lg-2 control-label">Sub Categories (If Any)</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textSubCategories" placeholder="One per line"
                                        TextMode="MultiLine" Style="resize: vertical; height: 180px"
                                        class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" id="div_udpate_sub_cate" runat="server" visible="false">
                                <label class="col-lg-2 control-label">Sub Category Name</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="text_subcat_to_update" placeholder="Sub Category Name"
                                        class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <asp:Button ID="btnSave" runat="server"
                                        OnClick="btnSave_Click"
                                        class="btn btn-sm btn-white" Text="Save" />

                                    <asp:Button ID="btn_update" runat="server"
                                        OnClick="btn_update_Click" Visible="false"
                                        class="btn btn-sm btn-success" Text="Update" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="div_categories" runat="server">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_cats" runat="server"
                                CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found !"
                                AutoGenerateColumns="false"
                                PagerStyle-CssClass="asp-paging"
                                OnRowCommand="grd_cats_RowCommand">

                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo">
                                        <ItemTemplate>
                                            <a href="categories.aspx?catid=<%# Eval("CategoryID") %>"><%# Eval("CategoryID") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <a href="categories.aspx?catid=<%# Eval("CategoryID") %>">
                                                <%# Eval("CategoryDesc") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("CategoryID")%>' CommandName="delete_category"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                                runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                            <%--<%# (Eval("HasSubCats").ToString()=="Y"?"Delete All ":"") %>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="div_sub_categories" runat="server">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Sub Categories</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_sub_categories" runat="server"
                                CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found !"
                                AutoGenerateColumns="false" OnRowCommand="grd_sub_categories_RowCommand"
                                PagerStyle-CssClass="asp-paging">

                                <Columns>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <a href="categories.aspx?catid=<%# Eval("CategoryID") %>">
                                                <%# Eval("CategoryDesc") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sub Category">
                                        <ItemTemplate>
                                            <a href="categories.aspx?catid=<%# Eval("CategoryID") %>&subcatid=<%# Eval("SubCategoryID") %>&subcat=<%# HttpUtility.UrlEncode(Eval("SubCategoryDesc").ToString()) %>">
                                                <%# Eval("SubCategoryDesc") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("SubCategoryID")%>' CommandName="delete_subcategory"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                                runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                            <a class="btn btn-xs btn-success" data-toggle="tooltip" title="Edit"
                                                href="categories.aspx?catid=<%# Eval("CategoryID") %>&subcatid=<%# Eval("SubCategoryID") %>&subcat=<%# HttpUtility.UrlEncode(Eval("SubCategoryDesc").ToString()) %>">
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


