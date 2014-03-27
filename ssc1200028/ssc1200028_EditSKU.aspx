<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssc1200028_EditSKU.aspx.cs"
    Inherits="EditSKU" MaintainScrollPositionOnPostback="true" Title="Assortment Planning SKU Edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]><html class="ie ie6" lang="en"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="en"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="en"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!-->
<html lang="en">
<!--<![endif]-->
<head runat="server">
    <script type="text/javascript" src="../ssc1200028/Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../ssc1200028/Scripts/Default.js"></script>
    <script type="text/javascript" src="../ssc1200028/Scripts/jquery-ui-1.10.3.js"></script>
    <script type="text/javascript">

        function hideDiscontinueMessage() {
            document.getElementById('lblDiscontinueError').style.display = 'none';
        }

        function openAdd() {
            $('#divAddNewProduct').fadeIn();
            $('#div1').fadeIn();
            //$('#divAddProductValidation').show();
            $('#divDiscontinueSKU').hide();
            $('#divbtnSaveFacingStatus').show();
            $('#div1').show();
            //alert('You have chosen to add a new SKU to this Planogram. In order to do so, you will need to increase or reduce the facing of another SKU.');
            //$('#divFacingAdjustment').fadeIn();
            $('#divChangeReport').hide();
            return false;
        }

        function openChangeReport() {
            $('#divChangeReport').fadeIn();
            $('#divDiscontinueSKU').hide();
            $('#divAddNewProduct').hide();
            $('#divFacingAdjustment').hide();
            //$('#divAddProductValidation').hide();
            $('#divbtnSaveFacingStatus').hide();
            $('#div1').hide();
            return false;
        }

        $(document).ready(function () {
            //setValue();
            //$('#btnSaveFacingStatusAjax').click(InsertSKUAdjust);

            function showDialog(id) {
                $('#' + id).dialog("open");
            }

            function closeDialog(id) {
                $('#' + id).dialog("close");
            }

            //$('#divAddNewProduct').hide();

            //            $(".ddlExistingProductStatus").change(function () {
            //                //$("#ddlExistingProductStatus").change(function () {
            //                var selected = $(".ddlExistingProductStatus").val()
            //                if (selected == "DiscontinueSKU") {
            //                    $('#divDiscontinueSKU').fadeIn();
            //                    //$('#divFacingAdjustment').fadeIn();
            //                    $('#divFacingAdjustment').hide();
            //                    $('#divAddNewProduct').hide();
            //                    $('#divbtnSaveFacingStatus').hide();
            //                    $('#div1').hide();
            //                    $('#divChangeReport').hide();
            //                    hideDiscontinueMessage();
            //                    //$('#divAddProductValidation').hide();
            //                    $('#divChangeReport').hide();
            //                    return false;
            //                }
            //                if (selected == "AddSKU") {
            //                    $('#divAddNewProduct').fadeIn();
            //                    //for a modal popup
            //                    //                    $('#divAddNewProduct').dialog({
            //                    //                        autoOpen: false,
            //                    //                        draggable: true,
            //                    //                        title: "Add New Product",
            //                    //                        open: function (type, data) {
            //                    //                            $(this).parent().appendTo("form");
            //                    //                        }
            //                    //                    });
            //                    $('#div1').fadeIn();
            //                    $('#divAddProductValidation').show();
            //                    $('#divDiscontinueSKU').hide();
            //                    $('#divbtnSaveFacingStatus').show();
            //                    $('#div1').show();
            //                    //alert('You have chosen to add a new SKU to this Planogram. In order to do so, you will need to increase or reduce the facing of another SKU.');
            //                    $('#divFacingAdjustment').fadeIn();
            //                    $('#divChangeReport').hide();
            //                    return false;
            //                }
            //                if (selected == "ChangeReport") {
            //                    $('#divChangeReport').fadeIn();
            //                    $('#divDiscontinueSKU').hide();
            //                    $('#divAddNewProduct').hide();
            //                    $('#divFacingAdjustment').hide();
            //                    //$('#divAddProductValidation').hide();
            //                    $('#divbtnSaveFacingStatus').hide();
            //                    $('#div1').hide();
            //                }
            //            });

            $(".ddlChangeReport").change(function () {
                var selected = $(".ddlChangeReport").val()
                if (selected = "DiscontinuedProducts") {

                }
                if (selected = "Compare") {
                    $('#divComparePlanogram').fadeIn();
                }
            });



            $(".ddlDiscontinueSKUStatus").change(function () {
                var selected = $(".ddlDiscontinueSKUStatus").val();
                if (selected == "Replaced") {
                    $('#divReplace').fadeIn();
                    $('#divFacingEdit').fadeIn();
                    $('#div1').fadeIn();
                    $('#divbtnSaveFacingStatus').fadeIn();
                    //$('#divFacingAdjustment').fadeIn();
                    $('#divDiscontinuedBy').hide();
                    //$('#divExitStrategy').hide();
                    hideDiscontinueMessage();
                    return false;
                }
                if (selected == "Discontinued") {
                    $('#divReplace').hide();
                    $('#divDiscontinuedBy').fadeIn();
                    //$('#divExitStrategy').fadeIn();
                    hideDiscontinueMessage();
                    return false;
                }
                if (selected == "IncreaseFacing") {
                    $('#divReduce').fadeIn();
                    return false;
                }
            });
        });
    </script>
    <!-- Basic Page Needs
  ================================================== -->
    <meta charset="utf-8">
    <title>Edit SKUs</title>
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Mobile Specific Metas
  ================================================== -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <!-- CSS
  ================================================== -->
    <link rel="stylesheet" href="../ssc1200028/Skeleton/stylesheets/base.css" />
    <link rel="stylesheet" href="../ssc1200028/Skeleton/stylesheets/skeleton.css" />
    <link rel="stylesheet" href="../ssc1200028/Skeleton/stylesheets/layout.css" />
    <link rel="Stylesheet" href="../ssc1200028/Styles/ssc1200028.css" />
    <!--[if lt IE 9]>
		<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
    <!-- Favicons
	================================================== -->
    <link rel="shortcut icon" href="../ssc1200028/Skeleton/images/favicon.ico">
    <link rel="apple-touch-icon" href="../ssc1200028/Skeleton/images/apple-touch-icon.png">
    <link rel="apple-touch-icon" sizes="72x72" href="../ssc1200028/Skeleton/images/apple-touch-icon-72x72.png">
    <link rel="apple-touch-icon" sizes="114x114" href="../ssc1200028/Skeleton/images/apple-touch-icon-114x114.png">
    <style type="text/css">
         .pagination {
                font-size: 80%;
            }

            .pagination a {
                text-decoration: none;
                border: solid 1px #4f6b72;
                color: #4f6b72;
            }

            .pagination a, .pagination span {
                display: block;
                float: left;
                padding: 0.3em 0.5em;
                margin-right: 5px;
                margin-bottom: 5px;
            }

            .pagination .current {
                background: #4f6b72;
                color: #4f6b72;
                border: solid 3px #4f6b72;
            }

            .pagination .current.prev, .pagination .current.next{
                color:#4f6b72;
                border-color:#2C16BC;
                background:#4f6b72;
            }
        
              .cssPager span  
              {
                  background-color:#4f6b72; 
                  font-size:18px;
                  padding-left:4px;
                  padding-right:4px;
                  padding:8px;
                  list-style:none;
                  text-align:right;
                  display:inline;
                  display:inline-block;
                  margin:1px;
                  color: #FFFFFF;
                  border: 1px solid #006699;
                  -webkit-border-radius: 3px;
                  -moz-border-radius: 3px;
                  border-radius: 3px;
                  background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #006699), color-stop(1, #00557F) );
                  background:-moz-linear-gradient( center top, #006699 5%, #00557F 100% );
                  filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#006699', endColorstr='#00557F');
                  background-color:#006699;
              }
    </style>
</head>
<body style="background-image: url(../ssc1200028/Skeleton/images/bg01.png);">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="Toolkitscriptmanager1" runat="server" EnablePageMethods="true">
        </asp:ToolkitScriptManager>
    <!-- Primary Page Layout
	================================================== -->
    <!-- Delete everything in this .container and get started on your own site! -->
    <div class="container">
        <div class="sixteen columns">
            <br />
            <hr />
            <div class="row">
                <asp:Label ID="lblError" runat="server" Visible="false" Text="Label" ForeColor="Red"></asp:Label>
            </div>
            <div class="row">
                <div class="two column">
                    <asp:Button ID="btnSearch" runat="server" Text="Home" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div class="row">
                <div class="three columns">
                    <strong>Selected Snapshot Description:</strong>
                </div>
                <div class="four columns">
                    <asp:Label ID="lblCurrentlySelectedSnapshot" Font-Bold="true" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="three columns">
                    <strong>Selected Snapshot Number:</strong>
                </div>
                <div class="two columns">
                    <asp:Label ID="lblCurrentlySelectedSnapshotNumber" Font-Bold="true" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="four columns">
                    Search within Snapshot By SKU:
                </div>
                <div class="two columns">
                    <asp:TextBox ID="txtSearchBySKU" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" TargetControlID="txtSearchBySKU"
                        runat="server" ServiceMethod="GetSnapshotSKUS" EnableCaching="true" MinimumPrefixLength="4"
                        UseContextKey="true" CompletionInterval="1000" CompletionSetCount="500" ServicePath="~/ssc1200028/ssc1200028_EditSKU.aspx">
                    </asp:AutoCompleteExtender>
                </div>
                <div class="two column">
                    <asp:Button ID="btnSearchBySKU" runat="server" OnClick="btnSearchBySKU_Click" Text="Search" />
                </div>
                <div class="two columns">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" 
                        onclick="btnReset_Click" />
                </div>
                <div class="two column">
                    <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Text="Excel" />
                </div>
                <div class="two column">
                    <%--<asp:Button ID="btnAddSKU" runat="server" Text="Add SKU" 
                        UseSubmitBehavior="true" OnClientClick="openAdd(); return false;" 
                        onclick="btnAddSKU_Click" />--%>
                        <asp:Button ID="btnAddSKU" runat="server" Text="Add SKU"  
                        onclick="btnAddSKU_Click" />
                </div>
                <div class="two columns">
                    <%--<asp:Button ID="btnChangeReport" runat="server" Text="Change Report" 
                    UseSubmitBehavior="true" OnClientClick="openChangeReport(); return false;" />--%>
                    <asp:Button ID="btnChangeReport" runat="server" Text="Change Report" 
                        onclick="btnChangeReport_Click" />
                </div>
                <div class="two columns">
                    <div id="divAction" runat="server" visible="false" class="row">
                        <%--<div class="one columns">
                            Action:
                        </div>
                        <div class="three columns">
                            <select class="ddlExistingProductStatus" id="ddlExistingProductStatus" name="ddlExistingProductStatus">
                                <option value="PleaseSelect">Please Select</option>
                                <option value="DiscontinueSKU">Discontinue SKU</option>
                                <option value="AddSKU">Add SKU</option>
                                <option value="ChangeReport">Change Report</option>
                            </select>
                        </div>--%>
                        <div class="three columns offset-by-one">
                            <asp:Label ID="lblFacingStatusError" runat="server" Visible="false" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divNoSkusMessage" class="row" style="display: none">
                There are no SKUs associated with this planogram snapshot. Use this panel to begin
                adding products.
            </div>
            <div class="row">
                <asp:GridView ID="gdvProducts" runat="server" BackColor="White" BorderColor="#DEDFDE"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" DataKeyNames="DetailID"
                    GridLines="Vertical" AutoGenerateColumns="False" OnSelectedIndexChanging="gdvProducts_SelectedIndexChanging"
                    OnPageIndexChanging="gdvProducts_PageIndexChanging" AllowPaging="True" AllowSorting="true"
                    OnSorting="gdvProducts_Sorting" PageSize="20" Width="1293px"  EnablePersistedSelection="true">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ProductID" HeaderText="SKU" SortExpression="ProductID" />
                        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Wrap="true"
                            ControlStyle-Font-Size="Small" SortExpression="Description">
                            <ControlStyle Font-Size="Small"></ControlStyle>
                            <ItemStyle Wrap="True"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ControlStyle-Font-Size="Small">
                            <ControlStyle Font-Size="Small"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ExitStrategy" HeaderText="Exit Strategy" SortExpression="ExitStrategy"
                            ControlStyle-Font-Size="Small">
                            <ControlStyle Font-Size="Small"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DiscontinuedBy" HeaderText="Disc By" SortExpression="DiscontinuedBy"
                            ControlStyle-Font-Size="Small">
                            <ControlStyle Font-Size="Small"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DiscontinuedComments" HeaderText="Disc Comments" SortExpression="DiscontinuedComments"
                            ControlStyle-Font-Size="Small">
                            <ControlStyle Font-Size="Small"></ControlStyle>
                            <HeaderStyle VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ReplacementProduct" HeaderText="Rep SKU" SortExpression="ReplacementProduct"
                            ControlStyle-Font-Size="Small">
                            <ControlStyle Font-Size="Small"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ReplacementProductDescription" HeaderText="Rep SKU Desc"
                            SortExpression="ReplacementProductDescription" ControlStyle-Font-Size="Small">
                            <ControlStyle Font-Size="Small"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ReplacementProductStrategy" HeaderText="New SKU Strategy" ControlStyle-Font-Size="Small"
                            SortExpression="ReplacementProductStrategy">
                            <ControlStyle Font-Size="Small"></ControlStyle>
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Button" SelectText="Modify" ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="pagination" BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </div>
            <div class="row">
                <div id="divSKUDetails" runat="server" visible="false" class="eight columns offset-by-one">
                    <div id="divFacingAdjustment" class="row">
                        <div id="divFacingError" runat="server" visible="false" class="row">
                            <div class="five columns">
                                <asp:Label ID="lblFacingError" Visible="false" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <p>
                                Since the planogram has been changed, you may want to adjust the facing status of
                                another SKU. Click "No Change Needed" if this is not the case.
                            </p>
                            <div class="row">
                                <div class="four columns">
                                    <asp:Button ID="btnNoChange" runat="server" Text="No Change Needed" OnClick="btnNoChange_Click" />
                                </div>
                                <div class="two columns">
                                    <asp:Button ID="btnSaveFacing" OnClick="btnSaveFacingStatus_Click" runat="server"
                                    Text="Save Facing" />
                                </div>
                            </div>
                        </div>
                    </div>
                        <div class="row">
                            <div class="three columns">
                                Reduce/Increase Facing:
                            </div>
                            <div class="two columns">
                                <asp:DropDownList ID="ddlFacing" runat="server">
                                    <asp:ListItem Value="PleaseSelect">Please Select</asp:ListItem>
                                    <asp:ListItem Value="Reduce">Reduce Facing</asp:ListItem>
                                    <asp:ListItem Value="Increase">Increase Facing</asp:ListItem>
                                </asp:DropDownList>
                                <%--<select id="Select1">
                                    <option value="PleaseSelect">Please Select</option>
                                    <option value="ReduceFacing">Reduce Facing</option>
                                    <option value="IncreaseFacing">Increase Facing</option>
                                </select>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="three columns">
                                SKU:
                            </div>
                            <div class="two columns">
                                <%--<asp:DropDownList ID="ddlFacingAdjustmentSKU" runat="server">
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtAdjustSKU" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender4" TargetControlID="txtAdjustSKU"
                                    runat="server" ServiceMethod="GetSnapshotSKUS" EnableCaching="true" MinimumPrefixLength="2"
                                    UseContextKey="true" CompletionInterval="500" CompletionSetCount="20" ServicePath="~/ssc1200028/ssc1200028_EditSKU.aspx">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="three columns">
                                Quantity:
                            </div>
                            <div class="two columns">
                                <asp:DropDownList ID="ddlFacingQuantity" runat="server">
                                    <asp:ListItem>Please Select</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                <div id="divAddProductValidation" style="display: none" class="row">
                    <asp:Label ID="lblInformation" ForeColor="Blue" runat="server" Text="Label"></asp:Label>
                </div>
                <%--<div class="row" id="divInformationMessage" style="border: 2px solid; display:none">
                
            </div>--%>
                <%--<div id="divAddNewProduct" style="border: 2px solid; display: none" class="twelve columns offset-by-one">--%>
            <div id="divAddNewProduct" runat="server" visible="false" style="border: 2px solid"
                class="twelve columns offset-by-one">
                <br />
                <div class="row">
                    <div class="three columns">
                        <asp:Button ID="btnSave" runat="server" Text="Save Product" OnClick="btnSave_Click" />
                    </div>
                    <div class="four columns">
                        <asp:Label ID="lblAddSKUError" ForeColor="Red" Visible="false" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="one columns">
                        SKU:
                    </div>
                    <div class="three columns">
                        <asp:TextBox ID="txtSKU" runat="server" OnTextChanged="txtSku_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" TargetControlID="txtSKU" runat="server"
                            ServiceMethod="GetCompletionList" EnableCaching="true" MinimumPrefixLength="4"
                            UseContextKey="true" CompletionInterval="1000" CompletionSetCount="500" ServicePath="~/ssc1200028/ssc1200028_EditSKU.aspx">
                        </asp:AutoCompleteExtender>
                    </div>
                    <div class="three columns offset-by-one">
                        New Item Strategy:
                    </div>
                    <div class="three columns">
                        <asp:TextBox ID="txtNewItemStrategy" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <%--<div class="three columns">
                            Previous Strategy Entries:
                        </div>
                        <div class="five columns">
                            <asp:DropDownList ID="ddlNewItemStrategy" runat="server">
                            </asp:DropDownList>
                        </div>--%>
                </div>
                <div class="row">
                    <div class="two column">
                        UPC:
                    </div>
                    <div class="three columns">
                        <asp:TextBox ID="txtNewUPC" runat="server"></asp:TextBox>
                    </div>
                    <div class="two columns offset-by-one">
                        Description:
                    </div>
                    <div class="three columns">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <%--<div class="one column">
                            <asp:Button ID="btnSaveNewItemStrategy" runat="server" Text="Save Strategy" OnClick="btnSaveNewItemStrategy_Click" />
                        </div>--%>
                </div>
            </div>
            <div id="divDiscontinueSKU" style="border: 2px solid;" class="sixteen columns offset-by-one" runat="server" visible="false">
                <br />
                <div class="row">
                    <div class="row">
                        <div class="three columns">
                            <asp:Button ID="btnSaveSKUChange" runat="server" Text="Save Change" OnClick="btnSaveSKUChange_Click" />
                        </div>
                        <div class="two columns">
                            Current Status:
                        </div>
                        <div class="two columns">
                            <asp:Label ID="lblCurrentStatus" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div class="six columns">
                            <asp:Label ID="lblDiscontinueError" runat="server" ForeColor="Red" Visible="false"
                                Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div class="one columns">
                        SKU:
                    </div>
                    <div class="two columns ">
                        <asp:TextBox ID="txtSkuChange" ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                    <div class="two columns">
                        Choose Action:
                    </div>
                    <div class="four columns">
                        <%--<select id="ddlDiscontinueSKUStatus" runat="server"  name="ddlDiscontinueSKUStatus" class="ddlDiscontinueSKUStatus">
                            <option value="SelectStatus" selected="selected">Select Status</option>
                            <option value="Replaced">Replace this SKU</option>
                            <option value="Discontinued">Discontinue Only</option>
                        </select>--%>
                        <asp:DropDownList ID="ddlDiscontinueSKUStatus" OnSelectedIndexChanged="ddlDiscontinueSKUStatus_onSelectedIndexChanged" runat="server" AutoPostBack="true">
                            <asp:ListItem>Select Status</asp:ListItem>
                            <asp:ListItem>Replace this SKU</asp:ListItem>
                            <asp:ListItem>Discontinue Only</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="divExitStrategy" runat="server" visible="false">
                        <div class="two columns">
                            Exit Strategy:
                        </div>
                        <div class="three columns">
                            <asp:DropDownList ID="ddlDiscontinueExitStrategy" runat="server">
                                <asp:ListItem>Please Select</asp:ListItem>
                                <asp:ListItem>Clearance</asp:ListItem>
                                <asp:ListItem>Sell In Place</asp:ListItem>
                                <asp:ListItem>Rolling Change</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div id="divDiscontinuedBy" class="row" runat="server" visible="false">
                    <div class="two columns">
                        Discontinued By:
                    </div>
                    <div class="four columns">
                        <asp:DropDownList ID="ddlDiscontinuedBy" runat="server">
                            <asp:ListItem>Please Select</asp:ListItem>
                            <asp:ListItem>Buyer</asp:ListItem>
                            <asp:ListItem>Supplier</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="two columns">
                        Comments:
                    </div>
                    <div class="four columns">
                        <asp:TextBox ID="txtDiscontinueComments" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div id="divReplace" runat="server" visible="false" >
                    <div class="row">
                        <div class="two columns">
                            New SKU:
                        </div>
                        <div class="three columns">
                            <asp:TextBox ID="txtNewSKU" runat="server" OnTextChanged="txtNewSku_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtNewSKU"
                                runat="server" ServiceMethod="GetCompletionList" EnableCaching="true" MinimumPrefixLength="4"
                                UseContextKey="true" CompletionInterval="1000" CompletionSetCount="500" ServicePath="~/ssc1200028/ssc1200028_EditSKU.aspx">
                            </asp:AutoCompleteExtender>
                        </div>
                        <div class="two column">
                            New UPC:
                        </div>
                        <div class="two columns">
                            <asp:TextBox ID="txtUPCReplace" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="two columns">
                            Description:
                        </div>
                        <div class="three columns">
                            <asp:TextBox ID="txtDescriptionReplace" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="two columns">
                            New Item Strategy:
                        </div>
                        <div class="two columns">
                            <asp:TextBox ID="txtNewItemStrategyReplace" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        
            <%--<div id="divChangeReport" style="border: 2px solid; display: none" class="twelve columns offset-by-one">--%>
        <div id="divChangeReport" runat="server" style="border: 2px solid" visible="false" class="sixteen columns offset-by-one">
            <br />
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Enabled="true"
                TabStripPlacement="Top">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Discontinued">
                    <ContentTemplate>
                        <asp:GridView ID="gdvDiscontinuedProducts" runat="server" BackColor="White" BorderColor="#DEDFDE"
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" DataKeyNames="detail_id"
                            GridLines="Vertical" AutoGenerateColumns="False" Width="400px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="product_id" HeaderText="SKU" />
                                <asp:BoundField DataField="product_desc" HeaderText="Description" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Discontinued and Replaced">
                    <ContentTemplate>
                        <asp:GridView ID="gdvDiscontinuedReplaced" runat="server" BackColor="White" BorderColor="#DEDFDE"
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" DataKeyNames="detail_id"
                            GridLines="Vertical" AutoGenerateColumns="False" Width="400px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="product_id" HeaderText="SKU" />
                                <asp:BoundField DataField="product_desc" HeaderText="Description" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Replacements">
                    <ContentTemplate>
                        <asp:GridView ID="gdvReplacedOnly" runat="server" BackColor="White" BorderColor="#DEDFDE"
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" DataKeyNames="detail_id"
                            GridLines="Vertical" AutoGenerateColumns="False" Width="400px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="product_repl_prod" HeaderText="SKU" />
                                <asp:BoundField DataField="product_repl_prod_desc" HeaderText="Description" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Comparison">
                    <ContentTemplate>
                        <div class="row">
                            
                        </div>
                        <div class="row">
                            <div class="three columns">
                                Search Options:
                            </div>
                            <div class="four columns">
                                <%--<select id="ddlSearchOptions" name="ddlSearchOptions">
                                        <option value="PleaseSelect" selected="selected">Please Select</option>
                                        <option value="Description">Description</option>
                                        <option value="Year">Year</option>
                                        <option value="Season">Season</option>
                                        <option value="Size">Size</option>
                                        <option value="Location">Location</option>
                                    </select>--%>
                                <asp:DropDownList ID="ddlSearchOptions" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchOptions_SelectedIndexChanged">
                                    <asp:ListItem>Please Select</asp:ListItem>
                                    <asp:ListItem>Description</asp:ListItem>
                                    <asp:ListItem>Year</asp:ListItem>
                                    <asp:ListItem>Season</asp:ListItem>
                                    <asp:ListItem>Size</asp:ListItem>
                                    <asp:ListItem>Location</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div id="divDescriptionSearch" runat="server" visible="false">
                                <div class="two columns">
                                    Description:
                                </div>
                                <div class="two columns">
                                    <asp:TextBox ID="txtPlanogramName" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divYearSearch" runat="server" visible="false">
                                <div class="one column">
                                    Year:
                                </div>
                                <div class="two columns">
                                    <asp:TextBox ID="txtSearchYear" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divSeasonSearch" runat="server" visible="false">
                                <div class="one column">
                                    Season:
                                </div>
                                <div class="four columns">
                                    <asp:DropDownList ID="ddlSeasonSearch" runat="server">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                        <asp:ListItem>Summer</asp:ListItem>
                                        <asp:ListItem>Spring</asp:ListItem>
                                        <asp:ListItem>Fall</asp:ListItem>
                                        <asp:ListItem>Winter</asp:ListItem>
                                        <asp:ListItem>Non-Seasonal</asp:ListItem>
                                        <asp:ListItem>Promotional</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divSizeSearch" runat="server" visible="false">
                                <div class="one column">
                                    Size:
                                </div>
                                <div class="two columns">
                                    <asp:TextBox ID="txtSizeSearch" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div id="divLocationSearch" runat="server" visible="false">
                                <div class="two column">
                                    Location:
                                </div>
                                <div class="four columns">
                                    <asp:DropDownList ID="ddlChangeReportLocation" runat="server">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                        <asp:ListItem>North</asp:ListItem>
                                        <asp:ListItem>South</asp:ListItem>
                                        <asp:ListItem>Neither</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="two columns">
                                <asp:Button ID="btnSearchSnapshotCompare" runat="server" Visible="false" Text="Search"
                                    OnClick="btnSearchSnapshotCompare_Click" />
                            </div>
                            
                            <div class="three columns">
                                <asp:Label ID="lblSearchSnapshotsError" runat="server" Visible="false" ForeColor="Red"
                                    Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                             <div class="five columns">
                                <p runat="server" id="pSearchResults" visible="false">
                                    Search Results
                                 </p>
                                <asp:GridView ID="gdvMatchingSnapshots" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" DataKeyNames="snapshot_id"
                                    GridLines="Vertical" AutoGenerateColumns="False" OnSelectedIndexChanging="gdvMatchingSnapshots_SelectedIndexChanging"
                                    ToolTip="Select a snapshot for comparison.">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="snapshot_nbr" HeaderText="Snapshot ID" />
                                        <asp:BoundField DataField="snapshot_desc" HeaderText="Description">
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle BackColor="#F7F7DE" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                            </div>
                            <div class="seven columns">
                                <p id="pExplaination" runat="server" visible="false">The two grids below show active SKUs only, side by side for comparison.</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="seven columns">
                            <p id="pDifference" runat="server" visible="false"><strong>Intersection</strong></p>
                            <br />
                            <p id="pDifferenceNote" runat="server" visible="false">
                                <asp:Label ID="lblDifferenceNote" runat="server" Text="Label"></asp:Label></p>
                                <asp:GridView ID="gdvDifference" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" DataKeyNames="DetailID"
                                    GridLines="Vertical" AutoGenerateColumns="False" 
                                    OnPageIndexChanging="gdvDifference_PageIndexChanging" Width="400px"  AllowPaging="true" PageSize="20">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="ProductID" HeaderText="SKU" />
                                        <asp:BoundField DataField="Description" HeaderText="Description">
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle BackColor="#F7F7DE" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                            </div>
                            <div class="seven columns">
                            <p id="pReverseDifference" runat="server" visible="false"><strong>Difference</strong></p>
                            <br />
                            <p id="pReverseDifferenceDescription" runat="server" visible="false">
                                <asp:Label ID="lblReverseDifferenceNote" runat="server" Text="Label"></asp:Label>
                             </p>
                                <asp:GridView ID="gdvReverseDifference" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" DataKeyNames="DetailID"
                                    GridLines="Vertical" AutoGenerateColumns="False" 
                                    OnPageIndexChanging="gdvReverseDifference_PageIndexChanging" Width="400px" AllowPaging="true"
                                    PageSize="20">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="ProductID" HeaderText="SKU" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle BackColor="#F7F7DE" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="three columns">
                                <asp:Label ID="lblSearchError" ForeColor="Red" Visible="False" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <br />
        </div>
    </div>
    <div>
        <input id="hidden" name="hidden" type="hidden" value="" />
    </div>

    <!-- container -->
    </form>
    <!-- End Document
================================================== -->
</body>
</html>
<%--<asp:sqldatasource id="sqlPlanagramName0" runat="server" connectionstring="<%$ ConnectionStrings:ssc1200028_ssc_spacemanConnectionString %>"
    selectcommand="select 1000 as Key_id, 'Please Select Planogram' as Planogram_Name union all select Key_id, CAST(Key_id as varchar(40)) + ', ' + Planogram as Planogram_Name from dbo.PLANO_KEY pk where pk.Group1 in ('Promotional', 'Farm Hardware', 'Growing', 'Pilot Stores', 'Seasonal/Impulse') order by Key_id">
            </asp:sqldatasource>--%>
