<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="select_banner.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .banner {
            display: initial;
        }

        .banner-row {
            margin: 30px 0px;
        }
            .div-border a, .div-border-inner a {
                width: 100%;
                height: 100%;
                font-size: 25px;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2 style="font-weight: 600;font-size: 29px;">Banners</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">admin</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Banners</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Select Banner to Add Images</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div style="box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);" class="row banner-row">
                            <div class="col-sm-6 col-xs-12 div-border">
                                <a href="edit-banner.aspx?bannerid=1" class="btn btn-link" id="banner_1" runat="server">Size :  1011 X 1288 </a>
                            </div>
                            <div class="col-sm-6 col-xs-12 div-border">
                                <a href="edit-banner.aspx?bannerid=3" id="banner_3" runat="server">Size :  1920 X 142 </a>
                            </div>
                        </div>

                        <div class="row text-center banner-row" style="text-align: -webkit-center;box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);">
                            <div class="col-sm-6 col-xs-12 div-border">
                                <a href="edit-banner.aspx?bannerid=2" id="banner_2" runat="server">Size :  960 X 736 </a>
                            </div>
                            
                            <div class="col-sm-6 col-xs-12 div-border">
                                <a href="edit-banner.aspx?bannerid=4" id="banner_4" runat="server">Size :  500 X 281 </a>
                            </div>
                        </div>

                        <div class="row text-center banner-row" style="text-align: -webkit-center;box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);">
                            <div class="col-sm-6 col-xs-12 div-border">
                                <a href="edit-banner.aspx?bannerid=5" id="banner_5" runat="server">Size :   500 X 281 </a>
                            </div>
                            <div class="col-sm-6 col-xs-12 div-border">
                                <a href="edit-banner.aspx?bannerid=6" id="banner_6" runat="server"> Size :   500 X 281 </a>
                            </div>
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



