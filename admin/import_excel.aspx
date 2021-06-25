<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="import_excel.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper  border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>Import Data via Excel</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">Admin</a>
                </li>
                <li class="active">
                    <strong>Import Excel</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="wrapper wrapper-content">


        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="panel panel-danger ">
                                    <div class="panel-heading">
                                        1 - Create Excel File in Prescribed Format
                                    </div>
                                    <div class="panel-body">
                                        <p>Your Excel File must contain these headers with exact names as provided in the sample file.</p>
                                        <a href="resources\SampleCustomer.xlsx" class="btn btn-success" id="a_donwload_file" runat="server">
                                            <span class="fa fa-download"></span>&nbsp;Download Sample File (10KB)
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        2 - Browse Excel File
                                    </div>
                                    <div class="panel-body">
                                        <p>Browse Excel File with extensions .xls, .xlsx </p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        3 - Import your Excel 
                                    </div>
                                    <div class="panel-body">

                                        <p>Click on Import Button & Wait</p>
                                    </div>

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
                    <div class="ibox-title">
                        <h5>Select An Excel File (only .xls and .xlsx files allowed)</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <asp:Literal ID="ltr_err_msg" runat="server"></asp:Literal>
                        <div class="form-inline">
                            <div class="form-group init-validator">
                                <label for="<%=FileUpload1.ClientID  %>" class="sr-only">Excel File</label>
                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    ControlToValidate="FileUpload1" CssClass="validator"
                                    ErrorMessage="<i class='fa fa-warning'></i>&nbsp;Please Select a file."></asp:RequiredFieldValidator>

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                    ValidationExpression="([a-zA-Z0-9()\s_\\.\-:])+(.xls|.xlsx)$"
                                    ControlToValidate="FileUpload1" runat="server" CssClass="validator "
                                    ErrorMessage="<i class='fa fa-warning'></i>&nbsp;Please select a valid Excel File (.xls or .xlsx)" />

                            </div>
                            <asp:Button ID="btnImport" runat="server" Text="Import Excel"
                                class="btn btn-primary  pull-right " OnClick="btnImport_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
            </div>
        </div>

        <div class="row">


            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5></h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Data inserted ! Try again">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="10" HeaderText="SrNo">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ID" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <%# Eval("ID")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <span class="label <%# ((bool)Eval("isSuccess")) == true ? "label-primary" : "label-danger" %>">
                                                <%# ((bool)Eval("isSuccess")) == true ? "<i class='fa fa-thumbs-o-up'></i> Success" : "<i class='fa fa-thumbs-o-down'></i> Failed" %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reason">
                                        <ItemTemplate>
                                            <%# Eval("Message")%>
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

   
    <asp:Literal ID="ltr_script" runat="server"></asp:Literal>

</asp:Content>

