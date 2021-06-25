<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="view_students.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--PAGE TITLE-->
    <div class="row wrapper  border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>View Registered Students </h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">Admin</a>
                </li>
                <li class="active">
                    <strong>View Students</strong>
                </li>
            </ol>
        </div>
    </div>
    <!--PAGE TITLE-->

    <!--PAGE CONTENT-->
    <div class="row">
        <asp:Literal ID="ltr_alert_msg" runat="server"></asp:Literal>
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-striped table-bordered table-hover"
                            EmptyDataText="No Students Registered yet !"
                            AllowPaging="true" PageSize="20"
                            OnRowCommand="GridView2_RowCommand" PagerStyle-CssClass="asp-paging"
                            OnPageIndexChanging="GridView2_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="10" HeaderText="SrNo">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>.
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ID" ItemStyle-Width="20">
                                    <ItemTemplate>
                                        <%# Eval("CustId")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%# Eval("CustName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Password">
                                    <ItemTemplate>
                                        <%# Eval("Password")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BillingAddress">
                                    <ItemTemplate>
                                        <%# Eval("BillingAddress")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <%# Eval("Mobile")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CreatedOn">
                                    <ItemTemplate>
                                        <%# Eval("CreatedOn","{0:dd-MMM-yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LastLogin">
                                    <ItemTemplate>
                                        <%# Eval("LastLogin","{0:dd-MMM-yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="EmailID">
                                    <ItemTemplate>
                                        <%# Eval("EmailID")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <%--<a class="btn btn-xs btn-success" data-toggle="tooltip" data-placement="left" 
                                            title="Edit Project" href="edit_project.aspx?ProjectID=<%#Eval("CustId")%>"><i class="fa fa-edit"></i></a>--%>
                                        <asp:LinkButton ID="lbdelete" CommandArgument='<%#Eval("CustId")%>' CommandName="delete_Student"
                                            OnClientClick='javascript:return confirm("Do you really want to delete this Student ?")'
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
    <!--PAGE CONTENT-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>


