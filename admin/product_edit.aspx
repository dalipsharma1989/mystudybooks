<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="product_edit.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/admin-assets/css/plugins/summernote/summernote.css" rel="stylesheet">
    <link href="/admin-assets/css/plugins/summernote/summernote-bs3.css" rel="stylesheet">

    <link href="/admin-assets/css/plugins/datapicker/datepicker3.css" rel="stylesheet">

    <style>
        .validation-summary ul {
            font-weight: normal !important;
        }
    </style>

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
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-6">
            <h2 style="font-weight: 600;font-size: 29px;">Product Edit</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="adminhome.aspx">Admin</a>
                </li>
                <li>
                    <a>Products</a>
                </li>
                <li class="active">
                    <strong style="color:#3051a0">Special Offers</strong>
                </li>
            </ol>
        </div>
        <div class="col-sm-6">
            <div class="title-action">
                <asp:Button ID="btn_save_edit" class="btn btn-primary" OnClick="btn_save_edit_Click" runat="server" Text="Save" AccessKey="s" />
                <a href="product_list.aspx" class="btn btn-primary">Back to Products</a>
                <a href="SpecialPriceList.aspx" class="btn btn-primary">Back to Offer List</a>
            </div>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight ecommerce">
        <asp:Literal ID="ltr_alert_msg" runat="server"></asp:Literal>
        <div class="row">
            <div class="col-lg-12">

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="<i class='fa fa-warning'></i>&nbsp;Please check for field validations" Font-Bold="true"
                    DisplayMode="BulletList" ShowSummary="true" CssClass="alert alert-danger validation-summary" />

                <div class="tabs-container">
                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#tab-1">Product info</a></li>
                        <li class="" style="display:none;"><a data-toggle="tab" href="#tab-2">Data</a></li>
                        <li class="" style="display:none;"><a data-toggle="tab" href="#tab-3">Discount</a></li>
                        <li class="" style="display:none;"><a data-toggle="tab" href="#tab-4">Categories</a></li>
                        <li class="" style="display:none;"><a data-toggle="tab" href="#tab-5">Subject</a></li>
                        <li class="" style="display:none;"><a data-toggle="tab" href="#tab-6">Class</a></li>
                    </ul>
                    <div class="tab-content">
                        <div id="tab-1" class="tab-pane active">
                            <div class="panel-body">

                                <fieldset class="form-horizontal">
                                    <div class="alert alert-info">
                                        <b><i class="fa fa-info"></i></b>&nbsp;Add either <b>Discount Price</b> or Discount %. The other field will be automatically calculated. 
                                        If both fields are present then <b>Discount Price</b> will be preferred first 
                                        i.e Discount % will be calculated according to <b>Discount Price</b> despite of whatever Discount % is present in the field.
                                        If both fields are present and want to enter <b>Discount %</b> Need to blank Discount Field.
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">ISBN</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="textISBN" class="form-control"  runat="server" ReadOnly="true"></asp:TextBox> 
                                        </div>
                                        <label class="col-sm-1 control-label">Price</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="textPrice" ReadOnly="true" class="form-control" placeholder="Price" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validator" ControlToValidate="textPrice" ErrorMessage="Price is empty">
                                                    <i class="fa fa-warning"></i>&nbsp;Required</asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="textPrice" CssClass="validator" MinimumValue="0" MaximumValue="999999" 
                                                Type="Double" ErrorMessage="Price must be non-negative decimal number"> <i class="fa fa-warning"></i>&nbsp;Invalid Amount</asp:RangeValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Name</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textProductName" class="form-control" placeholder="Product name" ReadOnly="true"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        
                                        <div class="col-sm-2" style="display:none;">
                                            <asp:DropDownList ID="dd_currency" Enabled="false" runat="server" class="form-control">
                                                <asp:ListItem>UAE</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        
                                        <div class="col-sm-3" style="display:none;" >
                                            <asp:CheckBox ID="IsDeactivate" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Deactivate Title" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group"  style="display:none;">
                                        <label class="col-sm-2 control-label">Description</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textDesc" runat="server" Style="resize: vertical; height: 120px;" TextMode="MultiLine" 
                                                class="form-control" placeholder="Description"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group"  style="display:none;">
                                        <label class="col-sm-2 control-label">Author</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textAuthor" class="form-control" placeholder="Author"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group"  style="display:none;">
                                        <label class="col-sm-2 control-label">Publisher</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textPublisher" class="form-control" placeholder="Publisher"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group"  style="display:none;">
                                        <label class="col-sm-2 control-label">Publish Year</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textPublishYear" class="form-control" placeholder="Publish Year"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div> 
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label"><small>Discount Price</small></label>
                                        <div class="col-sm-4 init-validator">
                                            <asp:TextBox ID="textDiscountPrice" placeholder="Discount Price" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="textDiscountPrice"
                                                CssClass="validator"   MaximumValue="999999" Type="Double" ErrorMessage="Discount Price must be non-negative decimal number">
                                                <i class="fa fa-warning"></i>&nbsp;Invalid Price</asp:RangeValidator>
                                        </div>
                                        <label class="col-sm-2 control-label"><small>Or Discount %</small></label>
                                        <div class="col-sm-4 init-validator">
                                            <asp:TextBox ID="textDiscountPercent" placeholder="Discount %"
                                                class="form-control" runat="server"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="textDiscountPercent"
                                                CssClass="validator" MinimumValue="0" MaximumValue="100" Type="Double"
                                                ErrorMessage="Discount % must be non-negative number">
                                                <i class="fa fa-warning"></i>&nbsp;Percentage must be in between 0 - 100</asp:RangeValidator>
                                        </div>
                                    </div> 

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Start Date</label>
                                        <div class="col-sm-10 init-validator">
                                            <asp:TextBox ID="textDiscountStartDate" placeholder="YYYY/MM/DD"
                                                class="form-control select-date" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">End Date</label>
                                        <div class="col-sm-10 init-validator">
                                            <asp:TextBox ID="textDiscountEndDate" placeholder="YYYY/MM/DD"
                                                class="form-control select-date" runat="server"></asp:TextBox>
                                        </div>
                                    </div>




                                </fieldset>

                            </div>
                        </div>

                        <div id="tab-2" class="tab-pane">
                            <div class="panel-body">

                                <fieldset class="form-horizontal">                                   

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Stock Quantity</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textClBal"
                                                class="form-control"
                                                runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                placeholder="Stock Quantity"
                                                CssClass="validator" ControlToValidate="textClBal" ErrorMessage="Stock Quantity is empty">
                                                <i class="fa fa-warning"></i>&nbsp;Required
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="textClBal"
                                                CssClass="validator" MinimumValue="0" MaximumValue="999999" Type="Integer"
                                                ErrorMessage="Stock Quantity must be a Whole number">
                                                <i class="fa fa-warning"></i>&nbsp;Stock Quantity must be a Whole number</asp:RangeValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Binding</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="dd_binding" runat="server"
                                                class="form-control">
                                                <asp:ListItem>Paperback</asp:ListItem>
                                                <asp:ListItem>Hardcover</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Weight</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textWeight"
                                                class="form-control" placeholder="Weight"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Language</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="dd_language" runat="server"
                                                class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Volume</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textVolume"
                                                class="form-control" placeholder="Volume"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Edition</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textEdition"
                                                class="form-control" placeholder="Edition"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Reprint</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtReprint" class="form-control" placeholder="Re-Print" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Total Pages</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textTotalPages"
                                                class="form-control" placeholder="Total Pages"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                </fieldset>


                            </div>
                        </div>

                        <div id="tab-3" class="tab-pane">
                            <div class="panel-body">
                                <fieldset class="form-horizontal">
                                    
                                </fieldset>
                            </div>
                        </div>

                        <div id="tab-4" class="tab-pane" style="display:none;">
                            <div class="panel-body">

                                <fieldset class="form-horizontal">

                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <div style="position: absolute; top: 1%; height: 97%; width: 94%; background: rgba(255, 255, 255, 0.67); text-align: -webkit-center; z-index: 1">
                                                <div style="position: absolute; width: 100%; top: 50%;">
                                                    <img src="/img/ring-alt.svg" />
                                                </div>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Category</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="dd_categories" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="dd_categories_SelectedIndexChanged"
                                                        class="form-control">
                                                        <asp:ListItem Value="Nil">Select Category</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Sub Categories</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="dd_subcategories" runat="server"
                                                        class="form-control">
                                                        <asp:ListItem Value="Nil">Select Sub Category</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </fieldset>
                            </div>
                        </div>

                        <div id="tab-5" class="tab-pane">
                            <div class="panel-body">

                                <fieldset class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Select Subject</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="dd_subject" runat="server"
                                                class="form-control">
                                                <asp:ListItem Value="Nil">Select Subject</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">New Subject</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textNewSubject" class="form-control"
                                                placeholder="Or Enter New Subject"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>
                        </div>

                        <div id="tab-6" class="tab-pane" style="display:none;">
                            <div class="panel-body">

                                <fieldset class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Select Class</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="dd_class" runat="server"
                                                class="form-control">
                                                <asp:ListItem Value="Nil">Select Class</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">New Class</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="textNewClass" class="form-control"
                                                placeholder="Or Enter New Class"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">

    <asp:Literal ID="ltr_scripts" runat="server"></asp:Literal>

    <!-- Input Mask-->
    <script src="/admin-assets/js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- SUMMERNOTE -->
    <script src="/admin-assets/js/plugins/summernote/summernote.min.js"></script>

    <!-- Data picker -->
    <script src="/admin-assets/js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <script>
        $(document).ready(function () {

            $('.summernote').summernote();

            $('.select-date').datepicker({
                format: 'yyyy/mm/dd',
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true
            });

        });
    </script>

    <script type="text/javascript">
        
    </script>
</asp:Content>

