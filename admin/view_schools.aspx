<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="view_schools.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--PAGE TITLE-->
    <div class="row wrapper  border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2 id="h2_title" runat="server">View Schools </h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Admin</a>
                </li>
                <li class="active">
                    <strong id="strong_li_title" runat="server">View Schools</strong>
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
                        <asp:GridView ID="GridView2" runat="server"
                            CssClass="table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found !"
                            AllowPaging="true" PageSize="20" AutoGenerateColumns="false"
                            OnRowDataBound="GridView2_RowDataBound"
                            OnRowCommand="GridView2_RowCommand" PagerStyle-CssClass="asp-paging"
                            OnPageIndexChanging="GridView2_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="10" HeaderText="SrNo">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>.
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <a class="btn btn-xs btn-default"
                                            data-toggle="popover" data-placement="top"
                                            data-title="Address"
                                            data-content="<%# Eval("BillingAddress")%>"><%# Eval("CustName")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Password">
                                    <ItemTemplate>
                                        <%# Eval("Password")%>
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

                                <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px">
                                    <ItemTemplate>

                                        <%--<a class="btn btn-xs btn-success" data-toggle="tooltip" data-placement="left" 
                                            title="Edit Project" href="edit_project.aspx?ProjectID=<%#Eval("CustId")%>"><i class="fa fa-edit"></i></a>--%>
                                        <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("CustId")%>' CommandName="delete_school"
                                            OnClientClick='javascript:return confirm("Do you really want to delete this School?")'
                                            CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                            runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                        <asp:LinkButton ID="lb_approve" CommandArgument='<%# Eval("CustId")+";"+Eval("isApproved") %>' CommandName="approve_school"
                                            CausesValidation="false" data-toggle="tooltip" data-html="true"
                                            OnClientClick='<%# (Eval("isApproved").ToString().ToLower()=="false"?"javascript:return confirm(\"Do you really want to Approve this School?\")": "javascript:return confirm(\"Do you really want to Un-Approve this School?\")") %>'
                                            ToolTip='<%# (Eval("isApproved").ToString().ToLower()=="false"?"<i class=\"fa fa-check-circle\"></i> Approve": "<i class=\"fa fa-ban\"></i> Un-Approve") %>'
                                            CssClass='<%# (Eval("isApproved").ToString().ToLower()=="false"?"btn btn-xs btn-primary": "btn btn-xs btn-warning") %>'
                                            runat="server">
                                            <%# (Eval("isApproved").ToString().ToLower()=="false"?"<i class=\"fa fa-check-circle\"></i>": "<i class=\"fa fa-ban\"></i>") %>
                                        </asp:LinkButton>
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



