<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="adminhome.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .pending-review-img {
            width: 50px;
          
        }
        .dashboard{
            background: #cb85dca8 !important;
        }
        .dashboard1{
             background: #7dde7d !important;
        }
        .dashboard2{
              background: #75a0ec !important;
        }
         .dashboard3{
              background: #f5637d99 !important;
        }
         h5{
                 font-size: 18px !important;
         }
         table input{
                 border: 3px double lightgrey;
         } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManger1" runat="server"></asp:ScriptManager>--%>
    <div class="wrapper wrapper-content">
        <div class="row" id="dv_Status" runat="server">
            <div class="col-lg-3">
                <div style="color:white" class="ibox float-e-margins">
                    <div class="ibox-title dashboard">
                        <span class="label label-info pull-right">Monthly</span>
                        <h5>Orders</h5>
                    </div>
                    <div style="border-style:none;"  class="ibox-content  dashboard">
                        <h1 class="no-margins" id="h1_monthly_orders" runat="server">275,800</h1>
                        <!--<div class="stat-percent font-bold text-info">20% <i class="fa fa-level-up"></i></div>-->
                        <small>New orders</small>
                    </div>
                </div>
            </div>

            <div class="col-lg-3">
                <div style="color:white" class="ibox float-e-margins">
                    <div class="ibox-title dashboard1">
                        <span class="label label-success pull-right">Monthly</span>
                        <h5>Total Sale</h5>
                    </div>
                    <div style="border-style:none"  class="ibox-content dashboard1">
                        <h1 class="no-margins" id="h1_monthly_income" runat="server">0</h1>
                        <!--<div class="stat-percent font-bold text-success">98% <i class="fa fa-bolt"></i></div>-->
                        <small>Monthly Sale</small>
                    </div>
                </div>
            </div>

            <div class="col-lg-3">
                <div style="color:white" class="ibox float-e-margins">
                    <div class="ibox-title dashboard2">

                        <h5>Registered customers</h5>
                    </div>
                    <div style="border-style:none" class="ibox-content dashboard2">
                        <h1 class="no-margins" id="h1_registered_users" runat="server">6</h1>
                        <small>Retail</small>
                    </div>
                </div>
            </div>

            <div class="col-lg-3">
                <div style="color:white" class="ibox float-e-margins">
                    <div class="ibox-title dashboard3">
                        <span class="label label-info pull-right">Annually</span>
                        <h5>Orders</h5>
                    </div>
                    <div style="border-style:none"  class="ibox-content dashboard3">
                        <h1 class="no-margins" id="h1_annual_orders" runat="server">275,800</h1>
                        <!--<div class="stat-percent font-bold text-info">20% <i class="fa fa-level-up"></i></div>-->
                        <small>New orders</small>
                    </div>
                </div>
            </div>

        </div>

        <!-- Latest Orders -->
        <div class="row" id="dv_successOrder" runat="server">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Latest Orders</h5>
                                <div style="text-align:center;display:flex;margin-left: 200px;">
                                    <asp:TextBox ID="txtOrderPaymentId" runat="server" class="form-control" placeholder="Enter Transaction ID or Name or Email or Mobile....." Style="border: 1px solid;"  Width="60%"></asp:TextBox>
                                    <asp:Button ID="btn_search" runat="server" class="btn btn-primary" Text="Search" Width="10%" OnClick="btn_search_Click" />
                                    <a href="adminhome.aspx" class="btn btn-primary" style="margin-left:10px;"><i class="fa fa-refresh"></i>&nbsp;Refresh</a>
                                </div>
                                <div class="ibox-tools">
                                    <a class="collapse-link">
                                        <i class="fa fa-chevron-up"></i>
                                    </a>

                                </div>
                            </div>
                            <div style="overflow: scroll;" class="ibox-content"> 
                                <div class="row">
                                    <div class="col-lg-12"> 
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Record Found !" 
                                            OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true" PageSize="50" PagerStyle-CssClass="asp-paging"  AutoGenerateColumns="false" > 
                                             <Columns>
                                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px">
                                        <ItemTemplate>                                            
                                            <a class="btn btn-xs btn-success" data-toggle="tooltip" title="View Detail" 
                                                href="order_details.aspx?orderid=<%# Eval("OrderID") %>&Cur=<%=System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]%>">
                                                <i class="fa fa-search"></i>&nbsp;View
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order ID">
                                        <ItemTemplate>
                                            <%# Eval("OrderID") %> 
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <%# Eval("OrderDocNo") %> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Date">
                                        <ItemTemplate>
                                            <%# Eval("OrderDate","{0:dd MMM yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <%# Eval("OrderStatus") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <%# Eval("TotalNetAmount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <%# Eval("CustName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Email ID">
                                        <ItemTemplate>
                                            <%# Eval("EmailID") %>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <%# Eval("Mobile") %>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                </Columns> 
                                            </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
          </div>

        <!-- Orders Pending Payment -->
        <div class="row" id="dv_PendingOrders" runat="server">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Orders Pending Payment</h5>
                                <div style="text-align:center;display:flex;margin-left: 260px;">
                                    <asp:TextBox ID="txtPendingOrder" runat="server" class="form-control" placeholder="Enter Order ID" Style="border: 1px solid;"  Width="50%"></asp:TextBox>
                                    <asp:Button ID="btn_PendingOrderGet" runat="server" class="btn btn-primary" Text="Search" Width="10%" OnClick="btn_PendingOrderGet_Click" />
                                    <a href="adminhome.aspx?TYPE=PENDINGORDER" class="btn btn-primary" style="margin-left:10px;"><i class="fa fa-refresh"></i>&nbsp;Refresh</a>
                                </div>
                                <div class="ibox-tools">
                                    <a class="collapse-link">
                                        <i class="fa fa-chevron-up"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="ibox-content">

                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:UpdatePanel Id="Panel_pop" runat="server">
                                        <ContentTemplate>
                                        <asp:UpdateProgress id="updateProgress_pop" runat="server">
                                            <ProgressTemplate>
                                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="position:fixed;top:45%;left:45%;" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                            <div  style="overflow: scroll;">
                                                    <asp:GridView   ID = "rp_Pending_order_Payment" runat="server" CssClass="table table-striped table-bordered table-hover margin bottom"
                                                        EmptyDataText="No Record Found !" OnPageIndexChanging="rp_Pending_order_Payment_PageIndexChanging"
                                                        OnRowCommand="rp_Pending_order_Payment_ItemCommand" AutoGenerateColumns="false" DataKeyNames="OrderId"
                                                        AllowPaging="true" PageSize="25" PagerStyle-CssClass="asp-paging" 
                                                        >
                                            <Columns >
                                                <asp:TemplateField HeaderText ="No.">
                                                    <ItemTemplate>
                                                         <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reference ID" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="transaction_id" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                <asp:TemplateField HeaderText="PaymentStatus" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button CssClass="btn btn-primary btn-sm" ID="btn_Payment_received" runat="server" Text="Approve" CommandName="payment_received" CommandArgument="<%#Container.DataItemIndex %>"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:Button CssClass="btn btn-danger btn-sm" ID="btn_delete" runat="server" Text="Delete" CommandName="delete_order" CommandArgument="<%#Container.DataItemIndex %>"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="OrderId" HeaderText="Order Id"/>
                                                <asp:BoundField DataField="EmailID" HeaderText="EMail Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="CustName" HeaderText="Customer"/>
                                                <asp:TemplateField HeaderText ="Date">
                                                    <ItemTemplate>
                                                        <%# Eval("CreatedOn","{0:dd MMM yyyy}") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText ="Total Qty" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate> <!-- Do Not Add any Whitespace or character in Span Tag, otherwise code behind will be affected -->
                                                         <span id="tot_qty" class="label label-primary" runat="server"><%# Eval("TotalQty") %></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText ="Amount" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate><!-- Do Not Add any Whitespace or character in Span Tag, otherwise code behind will be affected -->
                                                         <span class="label label-primary" id="amount" runat="server"><%# Eval("TotalAmount",CommonCode.AmountFormat()) %></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText ="Ship Cost" ItemStyle-HorizontalAlign="Center" visible="false">
                                                    <ItemTemplate> <!-- Do Not Add any Whitespace or character in Span Tag, otherwise code behind will be affected -->
                                                         <span class="label label-primary" id="ship_cost" runat="server"><%# Eval("ShipCost",CommonCode.AmountFormat()) %></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                          
                                                <asp:BoundField DataField="transactionID" HeaderText="Transaction ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="PayMethod" HeaderText="Pay Method" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="ShipMethod" HeaderText="Ship Method" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="Remark" HeaderText="Remark" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="BilltoAccountID" HeaderText="CustID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                
                                                
                                            </Columns>
                                        </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--  Pending Reviews -->
        <div class="row" id="dv_PendingReview" runat="server">
            <div class="col-lg-12" id="div_pending_reviews" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Pending Reviews</h5>
                                <div class="ibox-tools">
                                    <a class="collapse-link">
                                        <i class="fa fa-chevron-up"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-lg-12"> 
                                        <asp:UpdatePanel Id="Panel_pdr" runat="server">
                                        <ContentTemplate>
                                        <asp:UpdateProgress id="updateProgress_pdr" runat="server">
                                            <ProgressTemplate>
                                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="position:fixed;top:45%;left:45%;" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                        
                                        <asp:GridView ID="rp_pending_reviews" runat="server" CssClass="table table-bordered table-hover margin bottom"
                                            EmptyDataText="No Record Found !" OnPageIndexChanging="rp_pending_reviews_PageIndexChanging"
                                            OnRowCommand="rp_pending_reviews_ItemCommand" AutoGenerateColumns="false"
                                            AllowPaging="true" PageSize="25" PagerStyle-CssClass="asp-paging">
                                            
                                            <Columns>
                                                <asp:TemplateField HeaderText ="No.">
                                                    <ItemTemplate>
                                                         <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <img id="imgPicture1" src="<%#Eval("ImgPath") %>" class="img-responsive pending-review-img" onerror="this.onerror=null;this.src='/resources/no-image.png';" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ISBN" HeaderText="ISBN"/>
                                                <asp:BoundField DataField="CustName" HeaderText="Customer"/>
                                                 <asp:TemplateField HeaderText="Review">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="view_review" runat="server"
                                                                        CommandArgument='<%# Eval("ReviewID") %>' CommandName="view_review"
                                                                        CssClass="btn btn-xs btn-default">View review</asp:LinkButton>      
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lb_approve" runat="server"
                                                                        CommandArgument='<%# Eval("ReviewID") %>' CommandName="apporve_review"
                                                                        CssClass="btn btn-xs btn-primary"><i class="fa fa-check"></i></asp:LinkButton>                                                    
                                                        <asp:LinkButton ID="lb_delete" runat="server"
                                                                        CommandArgument='<%# Eval("ReviewID") %>' CommandName="delete_review"
                                                                        CssClass="btn btn-xs btn-danger"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
       <!-- Modal -->
            <div class="modal fade" id="review-modal-<%# Eval("ReviewID") %>" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <asp:Repeater ID="rp_review_modals" runat="server">
                        <ItemTemplate>
                        <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <%--<img src="<%# Eval("ImagePath") %>" class="img-responsive pending-review-img" style="display: inline-block; margin-right: 20px;">--%>
                            <img src="<%#Eval("ImgPath") %>" class="img-responsive pending-review-img" style="display: inline-block; margin-right: 20px;">
                            <span class="modal-title"><%# Eval("BookName") %> - <%# Eval("ISBN") %></span>
                        </div>
                        <div class="modal-body">
                            <p><abbr>Review Heading:       </abbr><%# Eval("ReviewerHeading") %></p>
                            <hr />
                            <p><abbr>Review Description:   </abbr><%# Eval("ReviewDesc") %></p>
                            <hr />
                            <p><abbr>Review Rating:       </abbr><%# Eval("Rating") %></p>
                            <hr />
                            <small>Posted by :- <b><%# Eval("CustName") %></b>  <br />
                                on :- <%# Eval("CreatedOn","{0:dd-MMM-yyyy}") %>
                            </small>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                    </ItemTemplate>
                </asp:Repeater>
                </div>
            </div>

</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>

