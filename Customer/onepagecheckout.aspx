<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.master" AutoEventWireup="true" CodeFile="onepagecheckout.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="kode-inner-banner">
        <div class="kode-page-heading">
            <h2>Checkout</h2>
        </div>
    </div>

    <div class="kode-content padding-tb-50">
        <div class="container">
            <div class="row">
                <asp:Literal ID="ltr_alert_msg" runat="server"></asp:Literal>
                <h3 class="text-center">Billing Address</h3>
                <hr />
                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Name:</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="text_bill_Name" class="form-control"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Email:</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="text_bill_Email" class="form-control"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="phone">Mobile:</label>
                            <div class="col-sm-10">

                                <asp:TextBox ID="text_bill_Phone" class="form-control"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Address:</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="text_bill_Address" class="form-control"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <div class="col-sm-offset-2 col-sm-10">
                                <asp:CheckBox ID="cb_cod"
                                    Text="Cash On Delivery"
                                    runat="server" />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-sm-6">
                    <div class="form-horizontal">

                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <div style="position: absolute; top: 0%; height: 97%; z-index: 9999; width: 95%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center;">
                                    <div style="position: absolute; width: 100%; top: 17%; left: 6%;">
                                        <img src="/images/ring-alt.svg" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="form-group ">
                                    <label class="control-label col-sm-2" for="email">Country:</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="dd_bill_country"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="dd_bill_country_SelectedIndexChanged"
                                            class="form-control"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label class="control-label col-sm-2" for="phone">State:</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="dd_bill_State"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="dd_bill_State_SelectedIndexChanged"
                                            class="form-control"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label class="control-label col-sm-2" for="phone">City:</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="dd_bill_City"
                                            class="form-control"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label class="control-label col-sm-2" for="phone">Pincode:</label>
                                    <div class="col-sm-10">

                                        <asp:TextBox ID="text_bill_Pincode" class="form-control"
                                            runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>


                        <div class="form-group ">
                            <div class="col-sm-offset-2 col-sm-10">
                                <asp:CheckBox ID="cb_ship_to_bill_addr"
                                    Text="Ship to Same Address"
                                    Checked="true"
                                    runat="server" />
                            </div>
                        </div>

                    </div>

                </div>
            </div>

            <div class="row" id="div_ship_address" runat="server" style="display: none">
                <h3 class="text-center">Shipping Address</h3>
                <hr />
                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Name:</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="text_ship_Name" class="form-control"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Email:</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="text_ship_Email" class="form-control"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="phone">Mobile:</label>
                            <div class="col-sm-10">

                                <asp:TextBox ID="text_ship_Phone" class="form-control"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Address:</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="text_ship_Address" class="form-control"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>


                    </div>

                </div>
                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <div style="position: absolute; top: 0%; height: 97%; z-index: 9999; width: 95%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center;">
                                    <div style="position: absolute; width: 100%; top: 25%; left: 6%;">
                                        <img src="/images/ring-alt.svg" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="form-group ">
                                    <label class="control-label col-sm-2" for="email">Country:</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="dd_ship_country"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="dd_ship_country_SelectedIndexChanged"
                                            class="form-control"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label class="control-label col-sm-2" for="phone">State:</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="dd_ship_State"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="dd_ship_State_SelectedIndexChanged"
                                            class="form-control"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label class="control-label col-sm-2" for="phone">City:</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="dd_ship_City"
                                            class="form-control"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group ">
                                    <label class="control-label col-sm-2" for="phone">Pincode:</label>
                                    <div class="col-sm-10">

                                        <asp:TextBox ID="text_ship_Pincode" class="form-control"
                                            runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>

            <div class="row" id="div_payment_option" runat="server">
                <h3 class="text-center">Payment Option</h3>
                <hr />
                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Credit Card:</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="dd_card_type" class="form-control"
                                    runat="server">
                                    <asp:ListItem>Visa</asp:ListItem>
                                    <asp:ListItem>Master Card</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Card Holder Name:</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="text_CardholderName" runat="server"
                                    placeholder="Card Holder Name" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="phone">Card Number:</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="text_Card_number" runat="server"
                                    placeholder="Card Number" class="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                </div>

                <div class="col-sm-6">
                    <div class="form-horizontal">
                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Exipry Date:</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="DropDownList2" class="form-control"
                                    Style="width: initial; display: inline-block"
                                    runat="server">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                </asp:DropDownList>
                                / 
                            <asp:DropDownList ID="DropDownList3" class="form-control"
                                Style="width: initial; display: inline-block"
                                runat="server">
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label class="control-label col-sm-2" for="email">Card Number:</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="text_CardNo"
                                    class="form-control" Style="width: initial"
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                </div>

            </div>

            <div class="row">
                <div class="col-sm-12 text-center">
                    <asp:Button ID="btn_place_order" runat="server"
                        class="btn btn-success" OnClick="btn_place_order_Click"
                        Text="Place Order" />
                    <asp:Button ID="btnCancel" runat="server"
                        OnClick="btnCancel_Click"
                        class="btn btn-danger" OnClientClick="javascript:return confirm('Are you Sure !');"
                        Text="Cancel Order" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
    <script>
        $("#<%=cb_ship_to_bill_addr.ClientID%>").change(function () {
            if (this.checked)
                $("#<%=div_ship_address.ClientID%>").toggle(500, 'swing');
            else
                $("#<%=div_ship_address.ClientID%>").toggle(500, 'swing');
        });

           $("#<%=cb_cod.ClientID%>").change(function () {
            if (this.checked)
                $("#<%=div_payment_option.ClientID%>").toggle(500, 'swing');
            else
                $("#<%=div_payment_option.ClientID%>").toggle(500, 'swing');
        });
    </script>
</asp:Content>



