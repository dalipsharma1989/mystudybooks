<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="topics.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <link href="css/customecss/style.min.css" rel="stylesheet" />
    <link href="css/customecss/homeDCbooks.min.css" rel="stylesheet" />
   <link href="css/customecss/homrPage.min.css" rel="stylesheet" /> 
     <div class="main" style="padding:1%"> 
    <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area"> 
				<div class="row">
					<div class="col-lg-12 col-md-12 col-sm-12">
						<div class="breadcrumbs-menu">
							<ul>
								<li><a href="/">Home</a></li>
								<li><a href="#" class="active">Information</a></li>
							</ul>
						</div>
					</div>
				</div> 
		</div>		  
        <div class="row">
        <div class="col-sm-12">
            <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
            <asp:Repeater ID="rp_topic" runat="server">
                <ItemTemplate>  
                    <div class="container">
                        <h2 style="text-align:center;"><%# Eval("Name") %></h2>
                        <hr />
                        <p class="text-justify">
                            <%# Eval("TopicContent") %>
                        </p>
                    </div>
                    
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    </div> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

