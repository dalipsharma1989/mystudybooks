<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="subjects.aspx.cs" Inherits="_Default" %>

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
            <h2>Subjects</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong>Subjects</strong></li>
            </ol>
        </div>
        <div class="col-lg-2">
            <div class="title-action">
                <a href="subjects.aspx" class="btn btn-info"><i class="fa fa-plus"></i>&nbsp;Add New Subject</a>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Add/Edit Subjects</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <asp:HiddenField ID="hf_SubjectID" runat="server" />
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Subject Name</label>
                                <div class="col-lg-10 init-validator">
                                    <asp:TextBox ID="textSubjectName" placeholder="Subject Name" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        CssClass="validator" ControlToValidate="textSubjectName">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <asp:Button ID="btnSave" runat="server"
                                        OnClick="btnSave_Click"
                                        class="btn btn-sm btn-success" Text="Save" />
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
                            <asp:GridView ID="grd_subjects" runat="server"
                                CssClass="table table-striped table-bordered table-hover"
                                AutoGenerateColumns="false"
                                PagerStyle-CssClass="asp-paging"
                                OnRowCommand="grd_subjects_RowCommand"
                                OnPageIndexChanging="grd_subjects_PageIndexChanging">
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

                                    <asp:TemplateField HeaderText="Subject">
                                        <ItemTemplate>
                                            <a href="subjects.aspx?subjectid=<%# Eval("SubjectID") %>">
                                                <%# Eval("SubjectName") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <a href="subjects.aspx?subjectid=<%# Eval("SubjectID") %>" class="btn btn-xs btn-primary" data-toggle="tooltip" title="Edit">
                                                <i class="fa fa-edit"></i>
                                            </a>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("SubjectID")%>' CommandName="delete_record"
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



