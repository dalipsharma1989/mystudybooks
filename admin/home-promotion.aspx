<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="home-promotion.aspx.cs" Inherits="_Defaultds" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .text-ph{
            border:4px double lightgrey !important;
        }
    </style>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-9">
            <h2 style="font-weight: 600;font-size: 29px;">Home Page Promotion</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Admin</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Home Page Promotion</strong>
                </li>
            </ol>
        </div>
        <div class="col-sm-3">
            <div class="title-action">
                <asp:Button ID="btn_save_edit" CssClass="btn btn-primary" OnClick="btn_save_edit_Click" runat="server" Text="Save" />

                <a href="#" onclick="javascript:window.location.assign(window.location.pathname)" class="btn btn-default">Cancel</a>
            </div>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight ecommerce">
        <%--<telerik:RadScriptManager runat="server" ID="RadScriptManager1" />--%>
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" Skin="MetroTouch" />
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
        <asp:HiddenField ID="hf_RowID" runat="server" />
        <div class="row">
            <div class="col-lg-12">
                <asp:HiddenField ID="hf_sliderID" runat="server" />
                <div class="panel-body">
                    <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                        <img src="/img/ring-alt.svg" alt="Loading..." />
                    </telerik:RadAjaxLoadingPanel>

                    <telerik:RadAjaxPanel runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                          </telerik:RadAjaxPanel>--%>
                    <telerik:RadDropDownList ID="dd_rad_Slider" runat="server" Width="100%" CssClass="text-ph" AutoPostBack="true" OnSelectedIndexChanged="dd_rad_Slider_SelectedIndexChanged">
                        <Items>
                            <telerik:DropDownListItem Value="Nil" Text="Select Slider" />
                            <telerik:DropDownListItem Value="Slider1" Text="Slider 1" />
                            <telerik:DropDownListItem Value="Slider2" Text="Slider 2" />
                            <telerik:DropDownListItem Value="Slider3" Text="Slider 3" />
                            <telerik:DropDownListItem Value="Slider4" Text="Slider 4" />
                            <telerik:DropDownListItem Value="Slider5" Text="Slider 5" />
                            <telerik:DropDownListItem Value="Slider6" Text="Slider 6" />
                            <telerik:DropDownListItem Value="Slider7" Text="Slider 7" />
                            <telerik:DropDownListItem Value="Slider8" Text="Slider 8" />
                        </Items>
                    </telerik:RadDropDownList>
                    <br />
                    <br />
                    <telerik:RadTextBox ID="textRad_SliderName" runat="server" CssClass="text-ph" Width="100%" placeholder="Slider Name"></telerik:RadTextBox>
                    <br />
                    <br />
                    <telerik:RadAutoCompleteBox RenderMode="Lightweight" runat="server" ID="text_Rad_search_Books" EmptyMessage="Search books"
                        DataSourceID="SqlDataSource1" DataTextField="BookName" DataValueField="ProductID" InputType="Token" CssClass="text-ph" Width="100%" DropDownWidth="100%">
                    </telerik:RadAutoCompleteBox>
                    <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:ConStr %>" ProviderName="System.Data.SqlClient"
                         SelectCommand="select BookCode ProductID, BookName from MasterTitle where coalesce(IsShowOnWeb,0) = 1 and  (iCompanyID = @iCompanyID)" runat="server">
                        <SelectParameters> <asp:SessionParameter Name="iCompanyID" SessionField="iCompanyID" /> </SelectParameters> 
                    </asp:SqlDataSource>
                </div>
            </div>
            <div class="col-lg-12" style="display : flex; width: 100%; word-break: break-word; flex-wrap: wrap;">
                <asp:HiddenField ID="hf_Slider1_value" runat="server" />
                <asp:HiddenField ID="hf_Slider2_value" runat="server" />
                <asp:HiddenField ID="hf_Slider3_value" runat="server" />
                <asp:HiddenField ID="hf_Slider4_value" runat="server" />
                <asp:HiddenField ID="hf_Slider5_value" runat="server" />
                <asp:HiddenField ID="hf_Slider6_value" runat="server" />
                <asp:HiddenField ID="hf_Slider7_value" runat="server" />
                <asp:HiddenField ID="hf_Slider8_value" runat="server" />
                <asp:Literal ID="ltr_books_selected" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">

    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>

</asp:Content>

