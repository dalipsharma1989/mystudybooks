<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="topics.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/admin-assets/css/plugins/datapicker/datepicker3.css" rel="stylesheet">
     <script type="text/javascript" src="/tinymce/tinymce.min.js"></script>
    <script>
        tinymce.init({
            selector: "textarea",
            theme: 'modern',
            plugins: [
              'advlist autolink lists link image charmap print preview hr anchor pagebreak',
              'searchreplace wordcount visualblocks visualchars code fullscreen',
              'insertdatetime media nonbreaking save table contextmenu directionality',
              'emoticons template paste textcolor colorpicker textpattern imagetools'
            ],
            fontsize_formats: '8pt 10pt 12pt 14pt 18pt 24pt 36pt',
            toolbar1: 'insertfile undo redo | styleselect | bold italic fontsizeselect | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            toolbar2: 'print preview media | forecolor backcolor emoticons',
            image_advtab: true,
            relative_urls: false,
            remove_script_host: false,
            convert_urls: true,
            toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image"
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <style>
        .form-group label{
                font-size: 15px !important;
        }
            .btn-success {
             background:#3051a0 !important;
              color:white !important;
             border:1px solid #3051a0 !important;
                      }

            .btn-success:hover {
                background: white !important;
                border: 1px solid #3051a0 !important;
                color: #3051a0 !important;
    }
            .label-name{
                padding-bottom: 2px;
            }
    </style>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-9">
            <h2 style="font-weight: 600;font-size: 29px;">Topic</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Admin</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Topic</strong>
                </li>
            </ol>
        </div>
        <div class="col-sm-3">
            <div class="title-action">
                <asp:Button ID="btn_save_edit"
                    class="btn btn-primary" OnClick="btn_save_edit_Click"
                    runat="server" Text="Save" />
                <a href="#" onclick="javascript:window.location.assign(window.location.pathname)" class="btn btn-default">Cancel</a>
            </div>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight ecommerce">
        <asp:PlaceHolder ID="ph_msg" runat="server"></asp:PlaceHolder>
        <div class="row">
            <div class="col-lg-12">
                <fieldset style="background: white;margin-bottom: 20px;box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16), 0 2px 10px 0 rgba(0,0,0,0.12);padding-top: 34px;padding-bottom: 20px;" class="form-horizontal">
                    <div class="form-group init-validator">
                        <asp:HiddenField ID="hf_TopicID" runat="server" />
                        <label class="col-sm-2 control-label">Name:</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="textName" class="form-control" placeholder="Topic Name"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                data-toggle="tooltip"
                                ToolTip="Topic Name is required"
                                CssClass="validator" ControlToValidate="textName">
                                <i class="fa fa-warning"></i>&nbsp;Required
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group init-validator">
                        <label class="col-sm-2 control-label">Is Parent:</label>
                        <div class="col-sm-9">
                            <asp:CheckBox ID="cb_isParent" runat="server" AutoPostBack="true" OnCheckedChanged="cb_isParent_CheckedChanged" />
                        </div>
                    </div>

                    <div id="div_not_parent" runat="server">
                        <div class="form-group init-validator">
                            <label class="col-sm-2 control-label">Child of:</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="dd_ChildOf" runat="server" CssClass="form-control label-name">
                                    <asp:ListItem Value="Nil">Select Topic</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" data-toggle="tooltip"
                                    ToolTip="Please select a topic"
                                    CssClass="validator" ControlToValidate="dd_ChildOf" ValueToCompare="Nil" Operator="NotEqual"> 
                                <i class="fa fa-warning"></i>&nbsp;Required
                                </asp:CompareValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Content:</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="textDescription" runat="server"
                                    Style="resize: vertical; height: 180px;"
                                    TextMode="MultiLine" class="form-control"
                                    placeholder="Description"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>

        <div class="row" id="div_grid_topics" runat="server">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-content">
                        <div style="padding-top: 25px;" class="table-responsive">
                            <asp:GridView ID="grd_topics" runat="server" CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found !" AutoGenerateColumns="false" PagerStyle-CssClass="asp-paging"
                                OnRowCommand="grd_topics_RowCommand">

                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Child Topic">
                                        <ItemTemplate>
                                            <%# Eval("Name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Parent Topic">
                                        <ItemTemplate>
                                            <a class="btn btn-xs btn-primary" data-toggle="tooltip" title="Edit Parent Topic"
                                                href="topics.aspx?parentopicid=<%# Eval("ParentTopicID") %>"><%# Eval("ParentName") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbdelete" CommandArgument='<%# Eval("TopicID")%>' CommandName="delete_topic" OnClientClick='javascript:return confirm("Do you really want to delete this Record?")'
                                                CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" CssClass="btn btn-xs btn-danger" runat="server"
                                                OnClick="lbdelete_Click" >
                                                <i class="fa fa-trash-o"></i></asp:LinkButton>
                                            <asp:HiddenField ID="hf_RowTopicID" Value='<%# Eval("TopicID")%>'  runat="server" />
                                            <a class="btn btn-xs btn-success" data-toggle="tooltip" title="Edit Topic" href="topics.aspx?topicid=<%# Eval("TopicID") %>">
                                                <i class="fa fa-edit"></i>
                                            </a>
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
    <!-- Data picker -->
    <script src="/admin-assets/js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <script>
        $(document).ready(function () {

            $('.club-dates').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: "yyyy/mm/dd"
            });

        });

    </script>

    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>

</asp:Content>


