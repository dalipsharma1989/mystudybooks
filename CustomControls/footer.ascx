<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="CustomControls_footer" %>

 

<div  class="col-lg-12 col-md-12 col-sm-12 footer">
    <style>
        .a:active, .a:hover{
            color: white;
        } 
    </style>
    <footer id="footer">
			<!-- footer-mid-start -->	
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12">
                    <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
                    <div class="col-lg-6 col-md-12 col-sm-12">
                        <img  src="/img/logo.jpg"  style="max-width: 261px;" /><br />
                        <p>
                            MyStudyBooks has established itself as India's first, exclusive online book store for schools and other educational institutions. 
                            In a short period of time, MyStudyBooks has been recognized by some of the top Educational Institutions across the nation as 
                            "a one-stop portal for all educational needs".
                            <a href="/topics.aspx?topicid=3" class="read-more">Know More</a>
                        </p>
                    </div>
                    <div id="dv_Footer"></div>

                    <asp:Repeater ID="rp_parent_topic" runat="server" OnItemDataBound="rp_parent_topic_ItemDataBound" >
		                    <ItemTemplate>                                                
                                    <div class="col-lg-3 col-md-12 col-sm-12">             
                                        <div class="col-lg-12 col-md-12 col-sm-12 footer-heading">
                                            <%# Eval("Name") %>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 footer-content">
                                            <asp:HiddenField ID="hf_ParentID" runat="server" Value='<%# Eval("TopicID") %>'/>    
                                            <ul style="list-style:none;padding-inline-start: 0px;">
                                                <asp:Repeater ID="rp_child_topics" runat="server">
                                                    <ItemTemplate>
                                                        <li><a class="anchor-tag" href="../topics.aspx?topicid=<%# Eval("TopicID") %>"><%# Eval("Name") %></a></li>
                                                    </ItemTemplate>
                                                </asp:Repeater>                                                        
                                            </ul> 
                                        </div>
                                    </div>                                        
                            </ItemTemplate>
                    </asp:Repeater>
                </div>                
	        </div>     
			<!-- footer-mid-end -->	
            </div>
</footer>

<!-- footer-bottom-start -->
           <%-- <div class="footer-bottom" style="padding-left: 2vw; padding-right:2vw;">
		            <div class="row bt-2">
                        
                </div>               
            </div>--%>

			<!-- footer-bottom-end -->
</div>
<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 footer2">
    <div class="container">
        <div class="row">
	            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
		            <div class="copy-right-area">
                        <p><i class="fa fa-copyright fa-fw"></i>&nbsp;<%:DateTime.Now.Year %> - <%:CommonCode.CompanyName %></p>
		            </div>
	            </div>
                         
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="text-align:center;">
                    <%--<p>
                        <small><i class="fa fa-code"></i>&nbsp;Developed by <a style="color:white;" href="http://springtimesoftware.net">Spring Time Software</a></small><br />
                    </p>--%>
                </div>
                        
	            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12" style="text-align:right;">
		            <div class="payment-img">
                        <a href="#"><img src="/img/payment.jpg" alt="payment" /></a>
			            <%--<a href="#"><img src="/img/CCAvenue.jpg" alt="payment" /></a>--%>
		            </div>
	            </div>
         </div>
        </div>
</div>             