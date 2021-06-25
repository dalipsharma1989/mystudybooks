<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="city_wise_shipping.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
         .save-button {
    padding-top: 5px !important;
    font-size: 14px !important;
    background: #3051a0 !important;
    border: 1px solid #3051a0 !important;
}
.save-button:hover {
        background: white !important;
        border: 1px solid #3051a0 !important;
        color: #3051a0 !important;
    }
.city-ph{
    padding-bottom:2px;
}
.label-name{
    font-size:15px !important;
}
    </style>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">City Wise Shipping</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong style="color:#3051a0">City Wise Shipping</strong></li>
            </ol>
        </div>
        <div class="col-lg-2">
            <div class="title-action">
                <a href="city_wise_shipping.aspx" class="btn btn-info"><i class="fa fa-map-marker"></i>&nbsp;City Wise Shipping</a>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: absolute; top: 1%; height: 97%; width: 94%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center; z-index: 1">
                    <div style="position: absolute; width: 100%; top: 50%;">
                        <img src="/img/ring-alt.svg" />
                    </div>
                </div>

            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
                <div class="row">
                    <div class="col-lg-12">
                        <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>City Wise Shipping</h5>
                                <div class="ibox-tools">
                                    <a class="collapse-link">
                                        <i class="fa fa-chevron-up"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="form-horizontal">
                                    <asp:HiddenField ID="hf_CityShipAmountID" runat="server" />

                                    <div class="form-group init-validator ">
                                        <label class="control-label col-lg-2">Country</label>
                                        <div class="col-lg-10">
                                            <asp:DropDownList ID="dd_Country" AutoPostBack="true" OnSelectedIndexChanged="dd_Country_SelectedIndexChanged" class="form-control city-ph" runat="server">
                                                <asp:ListItem Value="Nil">Select Country</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="dd_Country" CssClass="validator"
                                            ValueToCompare="Nil" Operator="NotEqual" ErrorMessage="">
                                                <i class="fa fa-warning"></i>&nbsp;Please Select Country
                                            </asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="form-group init-validator">
                                        <label class="control-label col-lg-2">State</label>
                                        <div class="col-lg-10">
                                            <asp:DropDownList ID="dd_State" AutoPostBack="true" OnSelectedIndexChanged="dd_State_SelectedIndexChanged" class="form-control" runat="server">
                                                <asp:ListItem Value="Nil">Select State</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="dd_State" CssClass="validator"
                                                ValueToCompare="Nil" Operator="NotEqual" ErrorMessage="">
                                                <i class="fa fa-warning"></i>&nbsp;Please Select State
                                            </asp:CompareValidator>
                                        </div>
                                    </div>


                                    <div class="form-group init-validator">
                                        <label class="control-label col-lg-2">City</label>
                                        <div class="col-lg-10">
                                            <asp:DropDownList ID="dd_City" class="form-control city-ph" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="dd_City_SelectedIndexChanged">
                                                <asp:ListItem Value="Nil">Select City</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="dd_City" CssClass="validator"
                                                ValueToCompare="Nil" Operator="NotEqual" ErrorMessage="">
                                                <i class="fa fa-warning"></i>&nbsp;Please Select City
                                            </asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-lg-2 control-label label-name">
                                            <small>From Weight</small>
                                        </label>
                                        <div class="col-lg-10 init-validator">
                                            <asp:TextBox ID="textFromWeight" placeholder="From Weight"
                                                class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                CssClass="validator" ControlToValidate="textFromWeight">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="textFromWeight"
                                                CssClass="validator" MinimumValue="0" MaximumValue="999999" Type="Double">
                                                <i class="fa fa-warning"></i>&nbsp;Invalid Weight</asp:RangeValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-lg-2 control-label label-name">
                                            <small>To Weight</small>
                                        </label>
                                        <div class="col-lg-10 init-validator">
                                            <asp:TextBox ID="textToWeight" placeholder="To Weight"
                                                class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                CssClass="validator" ControlToValidate="textToWeight">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="textToWeight"
                                                CssClass="validator" MinimumValue="0" MaximumValue="999999" Type="Double">
                                                <i class="fa fa-warning"></i>&nbsp;Invalid Weight</asp:RangeValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-lg-2 control-label label-name">
                                            <small>Shipping Amount</small>
                                        </label>
                                        <div class="col-lg-10 init-validator">
                                            <asp:TextBox ID="textShippingAmount" placeholder="Shipping Amount"
                                                class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                CssClass="validator" ControlToValidate="textShippingAmount">
                                        <i class="fa fa-warning"></i>&nbsp;Required
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="textShippingAmount"
                                                CssClass="validator" MinimumValue="0" MaximumValue="999999" Type="Double">
                                                <i class="fa fa-warning"></i>&nbsp;Invalid Amount</asp:RangeValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-lg-offset-2 col-lg-10">
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                                                class="btn btn-sm btn-success save-button" Text="Save" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <div class="row" id="div_grid_shippingamount" runat="server">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_shippingamount" runat="server"
                                CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found !"
                                AutoGenerateColumns="false"
                                PagerStyle-CssClass="asp-paging"
                                OnRowCommand="grd_shippingamount_RowCommand">

                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <%# Eval("CityName") %>, <%# Eval("StateName") %>-<%# Eval("CountryName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weight">
                                        <ItemTemplate>
                                            <%# Eval("FromWeight") %> - <%# Eval("ToWeight") %> grms.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shipping Amount">
                                        <ItemTemplate>
                                            <%# Eval("ShippingAmount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("CityShipAmountID")%>' CommandName="delete_topic"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                                runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>

                                            <a class="btn btn-xs btn-success" data-toggle="tooltip" title="Edit"
                                                href="city_wise_shipping.aspx?CityShipAmountID=<%# Eval("CityShipAmountID") %>">
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
