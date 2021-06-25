<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="notifyme_users.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">Notify me users</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Admin</a></li>
                <li class='active'><strong style="color:#3051a0">Notify me users</strong></li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
        <div class="row">
            <div class="col-lg-12">
                <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Notify me users</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>

                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
                        <div class="table-responsive">
                            <asp:GridView ID="grd_notify_me_users" runat="server" AutoGenerateColumns="false" class="table table-bordered table-hover"
                                PageSize="50" AllowPaging="true" OnPageIndexChanging="grd_notify_me_users_PageIndexChanging"
                                OnRowCommand="grd_notify_me_users_RowCommand">
                                <EmptyDataTemplate>
                                    <div class="alert alert-warning">
                                        <i class="fa fa-warning"></i>&nbsp;No record found
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>                                        
                                              <%# Eval("DateCreated", "{0:dd-MM-yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Time">
                                        <ItemTemplate>                                        
                                              <%# Eval("DateCreated", "{0:hh:mm tt}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="EmailID">
                                        <ItemTemplate>
                                            <%# Eval("EmailID") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Phone No">
                                        <ItemTemplate>
                                            <%# Eval("Phone") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ISBN">
                                        <ItemTemplate>
                                            <%# Eval("ISBN") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Book Name">
                                        <ItemTemplate>
                                            <%# Eval("BookName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("RowID")%>' CommandName="delete_record"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
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


