<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="subs_newsletters.aspx.cs" Inherits="_Default" %>

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
            <h2>Our Subscribers Emails</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>admin</a></li>
                <li class='active'><strong>Subscribers Emails</strong></li>
            </ol>
        </div>
         <div class="col-lg-2">
            <div class="title-action">
                 <asp:LinkButton ID="btn_ExportToExcel" runat="server" CssClass="btn btn-success"
                                    OnClick="btn_ExportToExcel_Click"><i class="fa fa-download"></i>&nbsp;Export To Excel</asp:LinkButton>
               
            </div>
        </div>
        
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>      

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_Classes" runat="server" CssClass="table table-striped table-bordered table-hover"
                                AutoGenerateColumns="false" PagerStyle-CssClass="asp-paging" OnRowCommand="grd_Classes_RowCommand"
                                OnPageIndexChanging="grd_Classes_PageIndexChanging">
                                <EmptyDataTemplate>
                                    <div class="alert alert-warning">
                                        <strong><i class="fa fa-warning"></i></strong>&nbsp;No record found
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="EmailID">
                                        <ItemTemplate>
                                            
                                                <%# Eval("EmailID") %>
                                             
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Subscribedate">
                                        <ItemTemplate>                                            
                                                <%# Eval("CreatedOn", "{0:dd-MM-yyyy}")%>                                             
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




