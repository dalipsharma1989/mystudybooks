<%@ Page Language="C#" AutoEventWireup="true" CodeFile="more_Items.aspx.cs" Inherits="more_Items" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="content" ContentPlaceHolderID="head" runat="server">
    <link href="css/customecss/style.min.css" rel="stylesheet" />
    <link href="css/customecss/homeDCbooks.min.css" rel="stylesheet" />
   <link href="css/customecss/homrPage.min.css" rel="stylesheet" />  
    <style type="text/css">
        .givenmeElipse{
                display: -webkit-box !important;
                overflow: hidden !important;
                text-overflow: ellipsis !important;
                -webkit-box-orient: vertical !important;
                -webkit-line-clamp: 1 !important; 
         } 
    </style>
    <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area"> 
			<div class="row">
				<div class="col-lg-12">
					<div style="margin-left:12px;padding:20px 0 0px 0;" class="breadcrumbs-menu">
						<ul>
							<li><a href="/">Home</a></li>
							<li><a href="#" class="active">More Items</a></li>
                            <li style="color:seagreen" ><span id="showResultMsgs"></span></li>
						</ul>
					</div>
				</div>
			</div> 
		</div>
		<!-- breadcrumbs-area-end -->
      <div class="shop-main-area mb-70">
          <div style="padding:0.5% 2% 2% 2%;">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align:center;">
                        <h1 style="padding:10px 0 50px 0;"><span id="slider_name"> </span></h1> 
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align:center;">
                        <ul id="ul_MoreList"></ul> 
                    </div> 
                </div>
          </div>
      </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="js/1.0.6/moreitems.min.js"></script>
</asp:Content>