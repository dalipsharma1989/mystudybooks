<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadSchoolLogo.aspx.cs" MasterPageFile="~/AdminMaster.master" Inherits="admin_UploadSchoolLogo" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .grid-image {
            width: 300px;
        }

        .text-box-link {
            width: 84% !important;
            display: initial !important;
        }

            .slider-img{
                height:40px;
                   }
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
                .update-btn{
                background:#3051a0 !important;
                color:white !important;
                border:1px solid #3051a0 !important;
            }
                  .update-btn:hover{
                background:white !important;
                color:#3051a0 !important;
                border:1px solid #3051a0 !important;
            }
    </style>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">School</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">admin</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">School Logo</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-md-12">
                <div class="ibox-title">
                    <h5>Upload School Logo</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-2 control-label">Select School</label>
                            <div class="col-lg-6">
                                <asp:DropDownList runat="server" ID="drp_School" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div  class="col-lg-4"></div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-2 control-label">School Logo</label>
                            <div class="col-lg-6">
                                <asp:FileUpload ID="FileUpload1" class="form-control slider-img" AllowMultiple="false" runat="server" />
                            </div>
                            <div  class="col-lg-4"></div>
                        </div>
                        <div class="form-group">
                            <div  class="col-lg-4"></div>
                            <div class="col-lg-2">
                                <asp:Button runat="server" ID="btn_SaveLogo" CssClass="btn btn-primary" Text="Save Logo" OnClick="btn_SaveLogo_Click" />
                            </div>
                            <div  class="col-lg-6"></div>
                        </div>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="table-responsive">
                            <asp:GridView ID="grd_slider" runat="server" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Record Found !"
                                AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnRowCommand="grd_slider_RowCommand" PagerStyle-CssClass="asp-paging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created Date" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                                        <%# Eval("DateCreated") %>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="School Code" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                                        <%# Eval("SchoolCode") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="School Name" ItemStyle-Width="150px" >
                                        <ItemTemplate>
                                                        <%# Eval("SchoolName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Logo Image" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <img src="<%# Eval("FilePath") %>" class="img-responsive" style="height: 50px; border: 1px solid #ddd" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("SchoolCode") %>' CommandName="delete_record"
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
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="scripts" runat="server">

</asp:Content>