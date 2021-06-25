<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserBookList.aspx.cs" Inherits="Customer_UserBookList" MasterPageFile="~/CustomerMaster.master" %>
<%@ MasterType VirtualPath="~/CustomerMaster.master" %>
 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">

 </asp:Content>
 <asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
     <!-- breadcrumbs-area-start -->
		<div class="breadcrumbs-area "> 
			<div class="row">
				<div class="col-lg-12">
					<div class="breadcrumbs-menu">
						<ul>
							<li><a href="/index.aspx">Home</a></li>
							<li><a href="#" class="active">Upload Your BookList</a></li>
						</ul>
					</div>
				</div>
			</div> 
		</div>
	<!-- breadcrumbs-area-end -->
     <!-- entry-header-area-start -->
	<div class="entry-header-area">
		<div class="row">
			<div class="col-lg-12">
				<div class="entry-header-title" style="text-align: center;padding-bottom:20px">
					<h2>Upload BookList</h2>
				</div>
			</div>
		</div>
	</div>
	<!-- entry-header-area-end -->
     <div  class="user-login-area mb-70">
         
<%--         <asp:UpdatePanel ID="upd_1"  runat="server"  > 
             <ContentTemplate>--%>
                 <div class="col-lg-offset-4 col-lg-8 col-md-offset-2 col-md-8 col-sm-12 col-xs-12">
                     <div class="row">
                        <asp:PlaceHolder ID="ph_Msg" runat="server"></asp:PlaceHolder>
                    </div>
                     <div class="row">
                         <div class="col-md-6 mb-30">
                             <div class="input-group">
                                 <label>Upload BookList<abbr style="color:red;">*</abbr></label>
                                 <asp:FileUpload ID="uploadbooklist" runat="server"/>
                             </div>
                         </div>
                    </div>
                     <div class="row">
                         <div class="col-md-6 mb-30">
                             <div class="input-group">
                                 <label>School Name<abbr style="color:red;">*</abbr></label>
                                 <asp:TextBox ID="txt_SchoolName" runat="server" CssClass="form-control" placeholder="Enter School Name"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-6 mb-30">
                             <div class="input-group">
                                 <label>Class Name<abbr style="color:red;">*</abbr></label>
                                 <asp:TextBox ID="txt_ClassDesc" runat="server" CssClass="form-control" placeholder="Enter Class name"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-6 mb-30">
                             <div class="input-group">
                                 <label>Language Name<abbr style="color:red;">*</abbr></label>
                                 <asp:TextBox ID="txt_Language" runat="server" CssClass="form-control" placeholder="Enter Language name"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                     <div class="row">
                         <div class="col-md-6 mb-30" style="text-align:center;">
                             <div class="input-group">
                                 <asp:Button runat="server" CssClass="btn btn-success" ID="btn_Save" OnClick="btn_Save_Click" Text="Save Book list" />
                             </div>
                         </div>
                     </div> 
                 </div>
                 <div class="row container">
                     <div class="col-lg-12">
                        <div class="ibox ">
                            <div class="ibox-content">
                                <div class="table-responsive">
                                    <asp:GridView ID="grd_slider" runat="server" CssClass="table table-striped table-bordered table-hover" 
                                         EmptyDataText="No Record Found !" AllowPaging="true" PageSize="20" AutoGenerateColumns="false"  
                                                PagerStyle-CssClass="asp-paging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>.
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="90px">
                                                <ItemTemplate>
                                                    <%# Eval("CustomerName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="School Name" ItemStyle-Width="90px">
                                                <ItemTemplate>
                                                    <%# Eval("SchoolName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class Name" ItemStyle-Width="90px">
                                                <ItemTemplate>
                                                    <%# Eval("ClassName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Language Name" ItemStyle-Width="90px">
                                                <ItemTemplate>
                                                    <%# Eval("LanguageName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Book List" ItemStyle-Width="90px">
                                                <ItemTemplate>
                                                    <img src="<%# Eval("BookListPath") %>" class="img-responsive" style="height: 50px; border: 1px solid #ddd" />
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                    </div>
                 </div>
             <%--</ContentTemplate>
         </asp:UpdatePanel>--%>
          

     </div>
 </asp:Content>
 <asp:Content ID="Content3" runat="server" ContentPlaceHolderID="scripts">
     <asp:Literal runat="server" ID="ltr_Msg"></asp:Literal>
 </asp:Content>