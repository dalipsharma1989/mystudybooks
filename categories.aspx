<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="categories.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .sub-cat
        {
                font-size: 14px;
                color:#f00;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <asp:Literal ID="ltr_breadcrumb" runat="server"></asp:Literal>
            <div class="row search-list">
                <div class="col-xs-12">
                    <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
                </div>
            </div>

            <div class="list-cats">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
    <script src="js/owl.carousel.min.js"></script>
</asp:Content>

