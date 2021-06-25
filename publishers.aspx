<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="publishers.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .table td:hover {
            background: #efefef;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <h1><%:mode %></h1>
                <asp:Literal ID="ltr_errmsg" runat="server"></asp:Literal>

                <div class="table-responsive">
                    <asp:DataList ID="dl_Publishers" runat="server" RepeatColumns="4" CssClass="table table-bordered">
                        <ItemTemplate>
                            <a href="search_results.aspx?publisher=<%# Eval("Publisher") %>"><%# Eval("Publisher") %></a>
                        </ItemTemplate>
                    </asp:DataList>
                </div>

                <div class="table-responsive">
                    <asp:DataList ID="dl_authors" runat="server" RepeatColumns="4" CssClass="table table-bordered">
                        <ItemTemplate>
                            <a href="search_results.aspx?author=<%# Eval("Author") %>"><%# Eval("Author") %></a>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

