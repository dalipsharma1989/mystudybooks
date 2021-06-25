<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="locations.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .grid-image {
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Locations</h2>
            <ol class="breadcrumb" id="ol_breadcrumb" runat="server">
                <li><a href='adminhome.aspx'>Home</a></li>
                <li class='active'><strong>Locations</strong></li>
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
                        <h5>Add Locations</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <div class="form-group" id="div_update_country" runat="server">
                                <label class="col-lg-2 control-label">Country</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textCountryName" placeholder="Country Name" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" id="div_form_grp_States" runat="server">
                                <label class="col-lg-2 control-label">States (If Any)</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textStates" placeholder="One per line"
                                        TextMode="MultiLine" Style="resize: vertical; height: 180px"
                                        class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" id="div_udpate_State" runat="server" visible="false">
                                <label class="col-lg-2 control-label">State</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="text_State_to_update" placeholder="State Name"
                                        class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>



                            <div class="form-group" id="div_form_grp_Cities" runat="server">
                                <label class="col-lg-2 control-label">Cities (If Any)</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="textCities" placeholder="One per line"
                                        TextMode="MultiLine" Style="resize: vertical; height: 180px"
                                        class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" id="div_udpate_City" runat="server" visible="false">
                                <label class="col-lg-2 control-label">City</label>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="text_City_to_update" placeholder="City Name"
                                        class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-10">
                                    <asp:Button ID="btnSave" runat="server"
                                        OnClick="btnSave_Click"
                                        class="btn btn-sm btn-white" Text="Save" />

                                    <asp:Button ID="btn_update" runat="server"
                                        OnClick="btn_update_Click" Visible="false"
                                        class="btn btn-sm btn-success" Text="Update" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="div_grd_Countries" runat="server">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_countries" runat="server"
                                CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found !"
                                AutoGenerateColumns="false"
                                PagerStyle-CssClass="asp-paging"
                                OnRowCommand="grd_countries_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo">
                                        <ItemTemplate>
                                            <a href="locations.aspx?countryid=<%# Eval("CountryID") %>&action=edit"><%# Eval("CountryID") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Country">
                                        <ItemTemplate>
                                            <a href="locations.aspx?countryid=<%# Eval("CountryID") %>&action=edit">
                                                <%# Eval("CountryName") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("CountryID")%>' CommandName="delete_Country"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                                runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="div_grd_states" runat="server">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>States</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_states" runat="server"
                                CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found !"
                                AutoGenerateColumns="false" OnRowCommand="grd_states_RowCommand"
                                PagerStyle-CssClass="asp-paging">

                                <Columns>
                                    <asp:TemplateField HeaderText="Country">
                                        <ItemTemplate>
                                            <a href="locations.aspx?countryid=<%# Eval("CountryID") %>&action=edit">
                                                <%# Eval("CountryName") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="States">
                                        <ItemTemplate>
                                            <a href="locations.aspx?stateid=<%# Eval("StateID") %>&action=edit&state=<%# HttpUtility.UrlEncode(Eval("StateName").ToString()) %>&state_countryid=<%# Eval("Countryid") %>">
                                                <%# Eval("StateName") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("StateID")%>' CommandName="delete_State"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                                runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="div_grd_cities" runat="server">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Cities</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="table-responsive">
                            <asp:GridView ID="grd_cities" runat="server"
                                CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found !"
                                AutoGenerateColumns="false" OnRowCommand="grd_cities_RowCommand"
                                PagerStyle-CssClass="asp-paging">

                                <Columns>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <a href="locations.aspx?stateid=<%# Eval("StateID") %>&action=edit&state=<%# HttpUtility.UrlEncode(Eval("StateName").ToString()) %>&state_countryid=<%# Eval("Countryid") %>">
                                                <%# Eval("StateName") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cities">
                                        <ItemTemplate>
                                            <a href="locations.aspx?cityid=<%# Eval("CityID") %>&action=edit&city=<%# HttpUtility.UrlEncode(Eval("CityName").ToString()) %>&city_stateid=<%# Eval("StateID") %>">
                                                <%# Eval("CityName") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("CityID")%>' CommandName="delete_City"
                                                OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger"
                                                runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>
</asp:Content>




