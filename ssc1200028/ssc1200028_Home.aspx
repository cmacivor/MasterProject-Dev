<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssc1200028_Home.aspx.cs"
    Inherits="ssc1200028_Home" EnableViewState="true" Title="Assortment Planning Home" %>

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
    <%--<script type="text/javascript" src="../Scripts/DataTables-1.9.4/media/js/jquery.js"></script>
    <script type="text/javascript" src="../Scripts/DataTables-1.9.4/media/js/jquery.dataTables.js"></script>--%>
    <%--<script type="text/javascript" src="../Scripts/jquery.validate.min.js"></script>--%>
    <script type="text/javascript">
        function openDetails() {
            $('#divAddSnapshot').show();
            return false;
        }


        $(document).ready(function () {
            //validation
            //$('#form1').validate({
            //            onsubmit: false
            ////                rules: {
            ////                    txtSnapshotID: 'required',
            ////                    txtDescription: 'required'
            ////                },
            //            });

            //            $('#tblSnapshots').dataTable({ "oLanguage": { "sSearch": "Search the snapshots:" },
            //                "iDisplayLength": 5,
            //                "aaSorting": [[0, "asc"]]
            //                });



            $("#ddlPlanograms").change(function () {
                //Todo: write your javascript code here.
                //alert('test');
            });

            //            $("input[id$='btnCreateNew']").click(function () {
            //                $('#divAddSnapshot').fadeIn();
            //                return false;
            //            });

            //            $("input[id$='btnSelectSnapshot']").click(function () {
            //                $('#divAddSnapshot').fadeIn();
            //                return false;
            //            });

            $("#btnCreateNew").click(function () {
                $('#divAddSnapshot').fadeIn();
                return false;
            });

            $("#btnCancel").click(function () {
                $('#divAddSnapshot').fadeOut();
                return false;
            });
        });
    </script>
    <!-- Basic Page Needs
  ================================================== -->
    <meta charset="utf-8">
    <title>ssc1</title>
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
    <%--<link rel="Stylesheet" href="../Content/DataTables-1.9.4/media/css/demo_table.css" type="text/css" />--%>
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
    <asp:ToolkitScriptManager ID="Toolkitscriptmanager1" runat="server">
    </asp:ToolkitScriptManager>
    <!-- Primary Page Layout
	================================================== -->
    <!-- Delete everything in this .container and get started on your own site! -->
    <div class="container ">
        <div class="sixteen columns">
            <br />
            <%--<h1 class="remove-bottom" style="margin-top: 40px">--%>
            <%--<asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/Skeleton/images/SSC_Logo.png" />--%>
            <%--</h1>--%>
            <div class="row">
                <div class="nine columns">
                    <asp:Label ID="lblGeneralError" ForeColor="Red" Visible="false" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="two columns">
                    Key ID:
                </div>
                <div class="two columns">
                    <asp:TextBox ID="txtKeyID" runat="server" Width="68px"></asp:TextBox>
                </div>
                <div class="three columns">
                    Planogram Name:
                </div>
                <div class="three columns">
                    <asp:TextBox ID="txtPlanogramName" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtenderPlanogramName" TargetControlID="txtPlanogramName" runat="server"
                    ServiceMethod="GetPlanogramList" EnableCaching="true" MinimumPrefixLength="2"
                    UseContextKey="true" CompletionInterval="1000" CompletionSetCount="500" ServicePath="~/ssc1200028/ssc1200028_Home.aspx">
                    </asp:AutoCompleteExtender>
                </div>
                <div class="two column">
                    <asp:Button ID="btnSearch" runat="server" Text="Search by Key ID and/or Planogram Name" OnClick="btnSearch_Click" />
                </div>
                
                <div class="three columns">
                <asp:CompareValidator ID="CompareValidator1" ForeColor="Red" runat="server"  ControlToValidate="txtKeyID" Operator="DataTypeCheck" Type="Integer"
                     ErrorMessage="Entry must be a number."></asp:CompareValidator>
                </div>
            </div>
            <div class="row">
                <div class="five columns">
                    Or Select from a full list of Planograms:
                </div>
                <div class="six columns">
                    <asp:DropDownList ID="ddlPlanograms" runat="server" Width="500px" ClientIDMode="Static"
                        DataSourceID="sqlPlanagramName" DataTextField="Planogram_Name" DataValueField="Key_id"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlPlanograms_SelectedIndexChanged" EnableViewState="true">
                        <asp:ListItem>Select Item</asp:ListItem>
                        <asp:ListItem>test1</asp:ListItem>
                        <asp:ListItem>test2</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="three columns">
                    Search By SKU:
                </div>
                <div class="three columns">
                    <asp:TextBox ID="txtSearchSKU" runat="server" EnableViewState="true"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtSearchSKU"
                        runat="server" ServiceMethod="GetCompletionList" EnableCaching="true" MinimumPrefixLength="4"
                        UseContextKey="true" CompletionInterval="1000" CompletionSetCount="500" ServicePath="~/ssc1200028/ssc1200028_EditSKU.aspx">
                    </asp:AutoCompleteExtender>
                </div>
                <div class="two columns">
                    <asp:Button ID="btnSearchSKU" EnableViewState="true" runat="server" Text="Search By SKU" OnClick="btnSearchSKU_Click" />
                </div>
                <div class="three columns offset-by-one">
                    <asp:Button ID="btnCreateNewSnapshot" EnableViewState="true" runat="server" Visible="false" 
                        Text="Create New Snapshot" onclick="btnCreateNewSnapshot_Click" />
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lblError" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label>
            </div>
            <div class="row">
                <div class="seven columns">
                    <asp:GridView ID="gdvSnapshots" runat="server" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        DataKeyNames="SnapshotID" OnSelectedIndexChanging="gdvPositions_SelectedIndexChanging"
                        OnPageIndexChanging="gdvSnapshots_PageIndexChanging" AllowPaging="True" PageSize="5"
                        AutoGenerateColumns="False" Width="375px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="SnapshotNumber" HeaderText="Snapshot ID" />
                            <asp:BoundField DataField="SnapshotDescription" HeaderText="Description" />
                            <asp:CommandField ButtonType="Button" ShowSelectButton="True"></asp:CommandField>
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
            </div>
            <div class="row">
                <div id="divAddSnapshot" runat="server" visible="false" class="12 columns">
                    <div class="row">
                        <div class="two columns">
                            <asp:Button ID="btnNewSnapshot" runat="server" Text="Save New" OnClick="btnNewSnapshot_Click"
                                EnableViewState="true" />
                        </div>
                        <div class="three columns">
                            <asp:Button ID="btnEditSkus" EnableViewState="true" runat="server" Visible="false"
                                Text="Edit Snapshot" OnClick="btnEditSkus_Click" />
                        </div>
                        <div class="two column">
                            <asp:Button ID="btnSave" runat="server" EnableViewState="true" Text="Save" OnClick="btnSave_Click"
                                Visible="False" />
                        </div>
                        <div class="two column">
                            <asp:Button ID="btnCopySnapshot" EnableViewState="true" runat="server" Text="Copy Snapshot"
                                OnClick="btnCopySnapshot_Click" Visible="False" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="two columns">
                            Snapshot ID:
                        </div>
                        <div class="two columns">
                            <asp:TextBox ID="txtSnapshotID" runat="server" EnableViewState="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqdtxtSnapshotID" runat="server" ValidationGroup="NewSnapshot"
                                ControlToValidate="txtSnapshotID" ErrorMessage="Please enter a Snapshot ID."
                                Display="None"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" CssClass="customCalloutStyle"
                                Width="50px" runat="server" TargetControlID="rqdtxtSnapshotID" PopupPosition="Right">
                            </asp:ValidatorCalloutExtender>
                        </div>
                        <div class="two columns">
                            Location:
                        </div>
                        <div class="two columns">
                            <asp:DropDownList ID="ddlLocation" EnableViewState="true" runat="server">
                                <asp:ListItem>Please Select</asp:ListItem>
                                <asp:ListItem>North</asp:ListItem>
                                <asp:ListItem>South</asp:ListItem>
                                <asp:ListItem>Both</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="two columns">
                            Size:
                        </div>
                        <div class="two columns">
                            <asp:TextBox ID="txtSize" runat="server" EnableViewState="true"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtSize"
                                Type="Integer" Display="None" ErrorMessage="Value must be a whole number between 0 and 100"
                                MinimumValue="0" MaximumValue="100" ForeColor="Red"></asp:RangeValidator>
                            <%--<asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                             TargetControlID="txtSize" MaskType="Number" Mask="99" MessageValidatorTip="true" ErrorTooltipEnabled="true">
                            </asp:MaskedEditExtender>--%>
                            <asp:RequiredFieldValidator ID="rqdtxtSize" runat="server" ValidationGroup="NewSnapshot"
                                ControlToValidate="txtSize" ErrorMessage="Please enter a Snapshot size." Display="None"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" CssClass="customCalloutStyle"
                                Width="50px" runat="server" TargetControlID="RangeValidator1" PopupPosition="Right">
                            </asp:ValidatorCalloutExtender>
                        </div>
                        <div class="two columns">
                            Season:
                        </div>
                        <div class="two columns">
                            <asp:DropDownList ID="ddlSeason" runat="server" EnableViewState="true">
                                <asp:ListItem>Please Select</asp:ListItem>
                                <asp:ListItem>Winter</asp:ListItem>
                                <asp:ListItem>Spring</asp:ListItem>
                                <asp:ListItem>Summer</asp:ListItem>
                                <asp:ListItem>Fall</asp:ListItem>
                                <asp:ListItem>Non-Seasonal</asp:ListItem>
                                <asp:ListItem>Promotional</asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="rqdddlSeason" runat="server" InitialValue="Please Select"
                                ValidationGroup="NewSnapshot" ControlToValidate="ddlSeason" ErrorMessage="Please select a season."
                                Display="None"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" CssClass="customCalloutStyle"
                                Width="50px" runat="server" TargetControlID="rqdddlSeason" PopupPosition="Right">
                            </asp:ValidatorCalloutExtender>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="two columns">
                            Description:
                        </div>
                        <div class="two columns">
                            <asp:TextBox ID="txtDescription" ReadOnly="true" runat="server" Rows="5" TextMode="MultiLine"
                                Width="150px" EnableViewState="true"></asp:TextBox>
                        </div>
                        <div class="two columns">
                            Status:
                        </div>
                        <div class="two columns">
                            <asp:DropDownList ID="ddlSnapshotStatus" runat="server" EnableViewState="true">
                                <asp:ListItem>Please Select</asp:ListItem>
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rqdddlSnapshotStatus" runat="server" InitialValue="Please Select"
                                ValidationGroup="NewSnapshot" ControlToValidate="ddlSnapshotStatus" ErrorMessage="Please select a status."
                                Display="None"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" CssClass="customCalloutStyle"
                                Width="50px" runat="server" TargetControlID="rqdddlSnapshotStatus" PopupPosition="Right">
                            </asp:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="two columns">
                            Buyer:
                        </div>
                        <div class="two columns">
                            <%--<asp:Label ID="lblBuyer" runat="server" Visible="false" Font-Bold="true" Text="Label"></asp:Label>--%>
                            <asp:DropDownList ID="ddlBuyer" runat="server" EnableViewState="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rqdddlBuyer" runat="server" ValidationGroup="NewSnapshot"
                                ControlToValidate="ddlBuyer" InitialValue="Please Select" ErrorMessage="Please enter a Snapshot size."
                                Display="None"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" CssClass="customCalloutStyle"
                                Width="50px" runat="server" TargetControlID="rqdddlBuyer" PopupPosition="Right">
                            </asp:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <br />
                    <br />
                </div>
            </div>
                <br />
            
            <div>
                <asp:RequiredFieldValidator ID="rqdddlLocation" runat="server" InitialValue="Please Select"
                    ValidationGroup="NewSnapshot" ControlToValidate="ddlLocation" ErrorMessage="Please select a location."
                    Display="None"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" CssClass="customCalloutStyle"
                    Width="30px" runat="server" TargetControlID="rqdddlLocation" PopupPosition="Right">
                </asp:ValidatorCalloutExtender>
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table id="tblSnapshots" cellpadding="0" cellspacing="0" border="0" class="display">
                            <thead>
                                <tr>
                                    <th>
                                        Snapshot ID
                                    </th>
                                    <th>
                                        Snapshot Description
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("snapshot_nbr") %>
                            </td>
                            <td>
                                <%#Eval("snapshot_desc") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <asp:SqlDataSource ID="sqlPlanagramName" runat="server" ConnectionString="<%$ ConnectionStrings:ssc1200028_ssc_spacemanConnectionString %>"
                SelectCommand="select 1000 as Key_id, 'Please Select Planogram' as Planogram_Name union all select Key_id, CAST(Key_id as varchar(40)) + ', ' + Planogram as Planogram_Name from dbo.PLANO_KEY pk where pk.Group1 not in ('Archive') order by Key_id">
            </asp:SqlDataSource>
        </div>
        </div>
        <!-- container -->
    </form>
    <!-- End Document
================================================== -->
</body>
</html>
