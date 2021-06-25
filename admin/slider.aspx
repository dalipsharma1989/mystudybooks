<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="slider.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">Slider</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">admin</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Big Slider first</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-lg-12">
                <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Add Slider</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <h1 class="col-lg-12 control-label" style="text-align:center;">Slider Size: 1440 X 480</h1>                                
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Slider Images</label>
                                <div class="col-lg-10">
                                    <asp:FileUpload ID="FileUpload1" class="form-control slider-img" AllowMultiple="false" runat="server" />
                                </div>
                            </div>
                            <div class="form-group" style="display:none;">
                                <label class="col-lg-2 control-label">Background Image</label>
                                <div class="col-lg-10">
                                    <asp:FileUpload ID="FileUpload2" class="form-control slider-img" AllowMultiple="false" runat="server" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" class="btn btn-sm btn-white save-button" Text="Save" />
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="grd_errdt" runat="server"  CssClass="table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found !" AutoGenerateColumns="false">
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
                            <asp:GridView ID="grd_slider" runat="server" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Record Found !"
                                AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnRowCommand="grd_slider_RowCommand" PagerStyle-CssClass="asp-paging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Background Image" ItemStyle-Width="150px" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <img src="<%# Eval("BackImagePath") %>" class="img-responsive" style="height: 50px; border: 1px solid #ddd" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Slider" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <img src="<%# Eval("SliderPath") %>" class="img-responsive" style="height: 50px; border: 1px solid #ddd" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Link and BookCode/ISBN" >
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hf_Sliderpath" Value='<%# Eval("SliderPath") %>' runat="server" />
                                            <asp:HiddenField ID="hf_BackImagePath" Value='<%# Eval("BackImagePath") %>' runat="server" />
                                            <asp:TextBox ID="textLink" Text='<%# Eval("URL") %>' CssClass="form-control text-box-link" style="width:285px !important;" placeholder="Enter Promo link here..." runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtBookCode" Text='<%# Eval("BookCode") %>' CssClass="form-control text-box-link"  style="width:285px !important;" placeholder="Enter BookCode / ISBN here..."  runat="server"></asp:TextBox>
                                            <asp:Button ID="btnUpdateLink" CommandName="update_link" CommandArgument='<%# Eval("SliderID") %>'
                                                CssClass="btn btn-xs btn-success update-btn" runat="server" Text="Update" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("SliderID").ToString()+";"+Eval("SliderPath").ToString() %>' CommandName="delete_record"
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




