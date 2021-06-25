<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SchoolMaster.master" CodeFile="SetLanguage.aspx.cs" Inherits="school_SetLanguage" %>

 <asp:Content ID="content1" runat="server" ContentPlaceHolderID="head">
     <!-- Font -->
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;700&display=swap" rel="stylesheet">

    <!-- CSS Implementing Plugins -->
    <link rel="stylesheet" href="/school/assets/vendor/font-awesome/css/fontawesome-all.min.css">
    <link rel="stylesheet" href="/school/assets/vendor/flaticon/font/flaticon.css">
    <link rel="stylesheet" href="/school/assets/vendor/animate.css/animate.css">
    <link rel="stylesheet" href="/school/assets/vendor/bootstrap-select/dist/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="/school/assets/vendor/slick-carousel/slick/slick.css"/>
    <link rel="stylesheet" href="/school/assets/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css"> 
    <link rel="stylesheet" href="/school/assets/css/theme.css">
    <style type="text/css">
            .headtext{
                letter-spacing: 0px;
                text-transform: uppercase;
                font-size: 20px;
                color: #0f2248;
                text-align: center;
                font-style: inherit;
                font-weight: bold; 
                padding: 15px 20px;
                width:100%;
                border-bottom: 5px solid grey;
            }

            .textBoxValue{
                  width: 100%;
                  padding: 0px 20px;
                  margin: 8px 0;
                  display: inline-block;
                  border: 1px solid #333;
                  border-radius: 4px;
                  box-sizing: border-box;
                  font-size: 15px;
                  font-weight: 900;
                }
            .btn-primary{
                background-color: #d49547 !important;
                border-color: #d49547 !important;
            }

            @media only screen and (max-width:767px){ 
                .gridstyl{
                    font-size:10px !important; 
                    padding:30px 5px !important;
                }
            }

            .gridstyl{
                font-size:20px;
                box-shadow: 0px 0px 10px 10px #c77815bf;
                padding:30px 20px;
            }
    </style>

</asp:Content>
<asp:Content ID="content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <!-- breadcrumbs-area-start -->
        <div class="breadcrumbs-area"> 
	        <div class="row">
		        <div class="col-lg-12">
			        <div class="breadcrumbs-menu">
				        <ul>
					        <li><a href="/">Home</a></li>
					        <li><a href="/school/school.aspx">School</a></li>
					        <li><a href="#" class="active">Set Information</a></li>
				        </ul>
			        </div>
		        </div>
	        </div> 
        </div>
		<!-- breadcrumbs-area-end -->
    <div class="row">
        <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
        <asp:Literal ID="ltr_msg" runat="server"></asp:Literal>
    </div>
    <div class="row" style="padding:20px 0px;">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <header class="d-md-flex justify-content-between align-items-center mb-5"  style="box-shadow: 0px 0px 10px 10px #f48221ab;font-family: cursive;">
                <h2 class="font-size-26 mb-4 mb-md-0 headtext" id="h_Heading" runat="server"></h2> 
                <asp:HiddenField ID="Hf_schoolCode" runat="server" />
            </header>
            <div class="table-responsive gridstyl" style="">
                <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th class="auto-style1" style="text-align:left !important;">SrNo</th>
                            <th style="display:none;">Set ID</th>
                            <th style="display:none;">ISBN</th>
                            <th class="text-Left" style="text-align:left !important;">Title</th>
                            <th class="text-center" style="display:none;">Currency</th>
                            <th class="text-right"  style="text-align:center !important;">Set Qty</th>
                            <th class="text-right" style="display:none;">Price</th>
                            <th class="text-right" style="display:none;" >Amount</th>
                            <th class="text-right" style="display:none;text-align:right !important;" >Before GST</th>
                            <th class="text-right"  style="display:none;text-align:right !important;">Tax Amount</th>
                            <th class="text-right"  style="text-align:right !important;">Total Amount</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rp_SetInformation" runat="server" >
                            <ItemTemplate> 
                                <asp:Label ID="lblItemCategoryDescription" style="display:none;" runat="server" Text='<%# Eval("BookCategoryDesc") %>'></asp:Label>
                                <asp:HiddenField runat="server" Value='<%# Eval("BookCategoryID") %>' ID = "hf_CategoryID" />
                                <tr>
                                        <td><%# Container.ItemIndex+1 %>.</td>
                                        <td style="display:none;">
                                            <asp:Label ID="lblBundleId" runat="server" Text='<%# Eval("BundleId") %>'></asp:Label></td>
                                        <td style="display:none;">
                                            <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>'></asp:Label>
                                        </td>
                                        <td><small>
                                            <asp:Label ID="lblBookName" runat="server" Text='<%# Eval("BookName") %>'>  </asp:Label></small></td>
                                        <td style="display:none;">
                                            <asp:Label ID="lblSaleCurrency" runat="server" Text='<%# Eval("SaleCurrency") %>'>  </asp:Label></td>
                                        <td class="text-right" style="text-align:center !important;" >
                                            <asp:Label ID="lblqty" runat="server" Text='<%# Eval("Qty") %>'>  </asp:Label></td>
                                        <td class="text-right" style="display:none;">
                                            <asp:Label ID="lblSalePrice" runat="server" Text='<%# Eval("SalePrice") %>'>  </asp:Label></td>
                                        <td class="text-right" style="display:none;">
                                            <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Amount") %>'>  </asp:Label></td>
                                        <td class="text-right" style="text-align:right !important;display:none;">
                                            <asp:Label ID="lbltaxable" runat="server" Text='<%# Eval("TaxableAmount") %>'>  </asp:Label></td>
                                        <td class="text-right" style="text-align:right !important;display:none;">
                                            <asp:Label ID="lbltax" runat="server" Text='<%# Eval("TaxAmount") %>'>  </asp:Label></td>
                                        <td class="text-right" style="text-align:right !important;">
                                            <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'>  </asp:Label></td>
                                    </tr>
                                <asp:Repeater ID="rp_SetDetail" runat="server" Visible="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex+1 %>.</td>
                                            <td style="display:none;">
                                                <asp:Label ID="lblBundleId" runat="server" Text='<%# Eval("BundleId") %>'></asp:Label></td>
                                            <td style="display:none;">
                                                <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>'></asp:Label>
                                            </td>
                                            <td><small>
                                                <asp:Label ID="lblBookName" runat="server" Text='<%# Eval("BookName") %>'>  </asp:Label></small></td>
                                            <td style="display:none;">
                                                <asp:Label ID="lblSaleCurrency" runat="server" Text='<%# Eval("SaleCurrency") %>'>  </asp:Label></td>
                                            <td class="text-right" style="text-align:center !important;" >
                                                <asp:Label ID="lblqty" runat="server" Text='<%# Eval("Qty") %>'>  </asp:Label></td>
                                            <td class="text-right" style="display:none;">
                                                <asp:Label ID="lblSalePrice" runat="server" Text='<%# Eval("SalePrice") %>'>  </asp:Label></td>
                                            <td class="text-right" style="display:none;">
                                                <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Amount") %>'>  </asp:Label></td>
                                            <td class="text-right" style="text-align:right !important;">
                                                <asp:Label ID="lbltaxable" runat="server" Text='<%# Eval("TaxableAmount") %>'>  </asp:Label></td>
                                            <td class="text-right" style="text-align:right !important;">
                                                <asp:Label ID="lbltax" runat="server" Text='<%# Eval("TaxAmount") %>'>  </asp:Label></td>
                                            <td class="text-right" style="text-align:right !important;">
                                                <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'>  </asp:Label></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>  
                            </ItemTemplate>
                        </asp:Repeater> 
                    </tbody> 
                    <tfoot>
                        <tr>
                            <td>
                                <span>Total</span>
                            </td>
                            <td></td>
                            <td></td>
                            <%--<td></td>
                            <td></td>--%>
                            <td  style="text-align:right !important;"><span id="spn_NetTOtl" runat="server"></span></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>
    <div class="row" style="padding:0 40px;">
        <div class="col-md-8"></div>
        <div class="col-md-4" style="box-shadow: 0px -5px 0px 5px #c77815bf;">
            <div class="form-group">
                <label style="font-size:20px;">Enter Student Name</label>
                <asp:TextBox runat="server" ID="txt_StudentName" CssClass="textBoxValue" Placeholder="Enter Student Name"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row" style="padding:0 40px;">
        <div class="col-md-8"></div>
        <div class="col-md-4" style="box-shadow: 0px 5px 0px 5px #c77815bf;">
            <div class="form-group">
                <label style="font-size:20px;">Enter Addmission/Roll Number</label>
                <asp:TextBox runat="server" ID="txt_RollNumber" CssClass="textBoxValue" Placeholder="Enter Addmission/Roll Number"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row" style="padding:40px 40px;">
        <div class="col-md-8" ></div>
        <div class="col-md-4" style="text-align:center;">
             <asp:Button runat="server" style="font-size: 15px;font-weight: 800;" ID="btn_BuyNow" OnClick="btn_BuyNow_Click" CssClass="btn btn-primary" Text="Proceed to Checkout" />
        </div>
    </div>
    <div class="pb-100"></div>

</asp:Content>
<asp:Content ID="content3" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="/school/assets/vendor/jquery/dist/jquery.min.js"></script>
    <script src="/school/assets/vendor/jquery-migrate/dist/jquery-migrate.min.js"></script>
    <script src="/school/assets/vendor/popper.js/dist/umd/popper.min.js"></script>
    <script src="/school/assets/vendor/bootstrap/bootstrap.min.js"></script>
    <script src="/school/assets/vendor/bootstrap-select/dist/js/bootstrap-select.min.js"></script>
    <script src="/school/assets/vendor/slick-carousel/slick/slick.min.js"></script>
    <script src="/school/assets/vendor/multilevel-sliding-mobile-menu/dist/jquery.zeynep.js"></script>
    <script src="/school/assets/vendor/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js"></script>


     <!-- JS HS Components -->
    <script src="/school/assets/js/hs.core.js"></script>
    <script src="/school/assets/js/components/hs.unfold.js"></script>
    <script src="/school/assets/js/components/hs.malihu-scrollbar.js"></script>
    <script src="/school/assets/js/components/hs.header.js"></script>
    <script src="/school/assets/js/components/hs.slick-carousel.js"></script>
    <script src="/school/assets/js/components/hs.selectpicker.js"></script>
    <script src="/school/assets/js/components/hs.show-animation.js"></script>

    <!-- JS Bookworm -->
    <!-- <script src="/school/assets/js/bookworm.js"></script> -->
    <script type="text/javascript">
         
    </script>
</asp:Content>
