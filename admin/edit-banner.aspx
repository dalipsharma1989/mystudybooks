<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="edit-banner.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .grid-image {
            width: 300px;
        }

        .text-box-link {
            width: 84% !important;
            display: initial !important;
        }
        .placeholder{
            height:40px;
        }
        .save-btn{
            background: #3051a0 !important;
             color: white !important;
             border: 1px solid #3051a0 !important;
        }
        .save-btn:hover{
            background: white !important;
             color: #3051a0 !important;
             border: 1px solid #3051a0 !important;
        }
        .btn-success{
            background-color:#3051a0 !important;
            color:white !important;
            border:1px solid #3051a0 !important;
        }
         .btn-success:hover{
            background-color:white !important;
            color:#3051a0 !important;
            border:1px solid #3051a0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">Edit Banner</h2>
            <ol class="breadcrumb">
                <li><a href="adminhome.aspx">Home</a></li>
                <li class="active"><strong style="color:#3051a0">Edit Banner</strong></li>
            </ol>
        </div>
        <div class="col-lg-2"></div>
    </div> 
    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <div class="row" id="dv_Upload" runat="server">
            <div class="col-lg-12">
                <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Add Images Banner</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal" >
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Images</label>
                                <div class="col-lg-10">
                                    <asp:FileUpload ID="FileUpload1" class="form-control placeholder" AllowMultiple="false" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Image Background Color (Default is White)</label>
                                <div class="col-lg-10">
                                    <asp:RadioButtonList ID="rb_back_color_sel" runat="server">
                                        <asp:ListItem Selected="True">White</asp:ListItem>
                                        <asp:ListItem>Transparent</asp:ListItem>
                                        <asp:ListItem>Dominant Color</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <asp:Button ID="btnSave" runat="server"
                                        OnClick="btnSave_Click"
                                        class="btn btn-sm btn-white save-btn" Text="Save" />
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="grd_errdt" runat="server"
                            CssClass="table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found !"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="SrNo">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>.
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Filename">
                                    <ItemTemplate>
                                        <%# Eval("FileName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# Eval("Status") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Message">
                                    <ItemTemplate>
                                        <%# Eval("Msg") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_cats" runat="server" CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found !" AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnRowDataBound="grd_cats_RowDataBound" 
                                OnRowCommand="grd_cats_RowCommand" PagerStyle-CssClass="asp-paging" OnPageIndexChanging="grd_cats_PageIndexChanging"> 
                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Image" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <img src="<%# Eval("ImagePath") %>" class="img-responsive" style="height: 50px; border: 1px solid #ddd" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Link">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hf_imagepath" Value='<%# Eval("ImagePath") %>' runat="server" />
                                            <asp:HiddenField ID="hf_active" Value='<%# Eval("Active") %>' runat="server" />
                                            <asp:TextBox ID="textLink" Text='<%# Eval("Link") %>' CssClass="form-control text-box-link" runat="server"  style=" " placeholder="Enter Promo link here..."></asp:TextBox>
                                            <asp:TextBox ID="txtBookCode" Text='<%# Eval("BookCode") %>' CssClass="form-control text-box-link" style="width:200px !important;display:none !important;" placeholder="Enter BookCode / ISBN here..."  runat="server"></asp:TextBox>
                            <asp:Button ID="btnUpdateLink" CommandName="update_link" CommandArgument='<%# Eval("ImageID") %>' CssClass="btn btn-xs btn-success" runat="server" Text="Update" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Active" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_set_active" runat="server"
                                                CssClass='<%# (Eval("Active").ToString().ToLower()=="true"?"btn btn-xs btn-success disabled": "btn btn-xs btn-danger ")%>'
                                                CommandArgument='<%# Eval("ImageID") %>' CommandName="set_active_banner">
                                                <%# (Eval("Active").ToString().ToLower()=="true"?"<i class='fa fa-eye-slash'></i>&nbsp;Active": "<i class='fa fa-eye'></i>&nbsp;Set Active ")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Banner No." ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("BannerID") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("ImageID").ToString()+";"+Eval("ImagePath").ToString() %>' CommandName="delete_record"
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this record ?")'
                                                runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>
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


