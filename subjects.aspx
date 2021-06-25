<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="subjects.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area mb-30">
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<div class="breadcrumbs-menu">
							<ul>
								<li><a href="/">Home</a></li>
								<li><a href="#" class="active">Subjects</a></li>
							</ul>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- breadcrumbs-area-end -->
		<!-- entry-header-area-start -->
		<div class="entry-header-area">
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<div class="entry-header-title">
							<h2>Wishlist</h2>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- entry-header-area-end -->

    
        <div class="user-login-area mb-70">
            <div class="container">
                <asp:Literal ID="ltr_errmsg" runat="server"></asp:Literal>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
					<div class="billing-fields" id="subjects">
                            <asp:Repeater ID="dl_subjects" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12" style="padding: 3px;">
                                        <div class="single-register" style="padding: 2%; height: 75px; box-shadow: 1px 1px 1px 1px aqua;">
                                            <label>
                                                <a href="search_results.aspx?subject=<%# Eval("SubjectName") %>&subid=<%# Eval("SubjectID") %>"><%# Eval("SubjectName") %></a>
                                            </label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                    </div>
                    <style>
                        #subjects > div > .single-register:hover{
                            background-color: powderblue;
                        }
                    </style>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

