<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssc9000004.aspx.cs" Inherits="ssc9000004v2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            color: #009999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" src="Scripts/jquery-2.0.3.min.js"></script>
        <%--<script type="text/javascript" src="../Scripts/ssc9000004.js"></script>--%> 
        <script type="text/javascript">
            $(document).ready(function () {
                $('#ddlEntities').change(function () {
                    //alert('test');
                    var text = $('#ddlEntities option:selected').text();
                    alert(text);
                    return false;
                })
            });
        </script>  
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MarketCostSku %>" 
            
            SelectCommand="select row_id as id, comp_div_descr as descr from dbo.comp_div_descr where ret_whsl = 'RETL'">
        </asp:SqlDataSource>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" Text="Label"></asp:Label>
        <h3>Estimated Writedown Query</h3>
        <p class="style1">Select locations below:</p>
        <table border="1">
            <tr>
                <td>
                    Entity:
                </td>
                <td>
                     District:
                </td>
                <td>
                    Store: 
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:DropDownList ID="ddlEntitiesNoAjax" runat="server" 
                        onselectedindexchanged="ddlEntitiesNoAjax_SelectedIndexChanged" 
                        AutoPostBack="True">
                        <asp:ListItem>Please Select</asp:ListItem>
                        <asp:ListItem>RCOOP</asp:ListItem>
                        <asp:ListItem>Rural America</asp:ListItem>
                        <asp:ListItem>Agronomy</asp:ListItem>
                        <asp:ListItem>Turf</asp:ListItem>
                    </asp:DropDownList>--%>
                    <%--<asp:DropDownList ID="ddlEntitiesNoAjax" runat="server" 
                        onselectedindexchanged="ddlEntitiesNoAjax_SelectedIndexChanged" 
                        AutoPostBack="True">
                        <asp:ListItem>Please Select</asp:ListItem>
                        <asp:ListItem>RCOOP</asp:ListItem>
                        <asp:ListItem>RSERV - Not Agronomy</asp:ListItem>
                        <asp:ListItem>Agronomy</asp:ListItem>
                    </asp:DropDownList>--%>
                    <asp:DropDownList ID="ddlEntitiesNoAjax" runat="server" 
                        onselectedindexchanged="ddlEntitiesNoAjax_SelectedIndexChanged" 
                        AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="descr" 
                        DataValueField="descr" AppendDataBoundItems="true">
                        <asp:ListItem>Please Select</asp:ListItem>
                        <asp:ListItem>Select All</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrictsNoAjax" runat="server" Visible="true"
                        onselectedindexchanged="ddlDistrictsNoAjax_SelectedIndexChanged" 
                        AutoPostBack="True">
                        
                    </asp:DropDownList> 
                </td>
                <td>
                    <asp:DropDownList ID="ddlStores" runat="server" Visible="true">
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSelectEntity" runat="server" Text="Select Entity" OnClick="btnSelectEntity_Click1" />
                </td>
                <td>
                    <asp:Button ID="btnSelectDistrict" runat="server" Text="Select District" OnClick="btnSelectDistrict_Click" />
                </td>
                <td>
                    <asp:Button ID="btnSelectStore" runat="server" Text="Select Store" OnClick="btnSelectStore_Click" />
                </td>
            </tr>
        </table>
        <br />
        <p class="style1">Select products below:</p>
        <table border="1">
            <tr>
                <td>
                     <asp:GridView ID="gdvSelectedEntityParameters" runat="server" CellPadding="4" ForeColor="#333333"
                        GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gdvSelectedEntityParameters_RowDeleting"
                        DataKeyNames="ParameterID">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowCancelButton="False" ShowDeleteButton="True" />
                            <asp:BoundField DataField="GroupType" HeaderText="Group Type" />
                            <asp:BoundField DataField="Item" HeaderText="Item" />
                            <asp:BoundField DataField="ItemDesc" HeaderText="Item Description" />
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table border="1">
            <tr>
                <td>
                    <asp:DropDownList ID="ddlMainProductCategories" runat="server" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlMainProductCategories_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProductGroup" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlProductGroup_SelectedIndexChanged" Width="250px">
                    </asp:DropDownList>  
                </td>
                <td>
                    <asp:DropDownList ID="ddlMarginGroups" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlMarginGroups_SelectedIndexChanged" 
                        Width="250px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSKU" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAddMainCategory" runat="server" Text="Add Category" 
                        onclick="btnAddMainCategory_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAddProductGroup" runat="server" Text="Add Product Group" 
                        onclick="btnAddProductGroup_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAddMarginGroup" runat="server" Text="Add Margin Group" 
                        onclick="btnAddMarginGroup_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAddSKU" runat="server" Text="Add Product" 
                        onclick="btnAddSKU_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table border="1">
            <tr>
                <td>
                    <asp:GridView ID="gdvSelectedProductParameters" runat="server" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" AutoGenerateColumns="False" DataKeyNames="ParameterID" OnRowDeleting="gdvSelectedProductParameters_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" />
                            <asp:BoundField DataField="GroupType" HeaderText="Group Type" />
                            <asp:BoundField DataField="Item" HeaderText="Item" />
                            <asp:BoundField DataField="ItemDesc" HeaderText="Item Description" />
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                </td>
            </tr>
        </table>        
        <br />
        <table border="1">
            <tr>
                <td>
                    Date:
                </td>
                <td>
                    <asp:TextBox ID="txtInventoryDate" runat="server" Width="72px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="reqInventoryDate" runat="server" ControlToValidate="txtInventoryDate" Display="Dynamic"
                        ErrorMessage="Required" Font-Bold="True" ValidationGroup="Search"></asp:RequiredFieldValidator><asp:CompareValidator
                            ID="cmpInventoryDate" runat="server" ControlToValidate="txtInventoryDate" ErrorMessage="Invalid Date"
                            Font-Bold="True" Operator="DataTypeCheck" Type="Date" ValidationGroup="Search"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFreightFactor" runat="server" Style="font-weight: bold; font-size: 12pt"
                        Text="Freight Factor"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Style="font-weight: bold; font-size: 10pt"
                        Text="For weighted items"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFreightPerPound" runat="server" Width="56px">0.00</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="radFrtPer" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Per pound</asp:ListItem>
                        <asp:ListItem>Per Ton</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Style="font-weight: bold; font-size: 10pt"
                        Text="For non-weighted items (EA)"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFreightPerItem" runat="server" Width="56px">0.00</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="cmdSearch" runat="server" OnClick="cmdSearch_Click" Text="Search"
                        ValidationGroup="Search" />
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text="Label" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>

    </div>
    </form>
</body>
</html>
