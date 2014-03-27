<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ssc1900016_default.aspx.vb" Inherits="ssc1900016_default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: x-large;
        }
        .descriptionStyle
        {
            width:75px;
            text-align:right;
            padding-right:5px;
        }
        .textBoxWidth
        {
            width:128px;
        }
        .tableStyle
        {
            margin-left:auto;
            margin-right:auto;
        }
    </style>
</head>
<body style="background-color: #FFF8DC">
    <form id="form1" runat="server">
    <br />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        CombineScripts="False"></asp:ToolkitScriptManager>
    <div style="text-align: center">
        <span class="style1">
            <br />
            <asp:Label ID="lblTitle" runat="server"></asp:Label><br />
        </span>
        <hr />
        <br />
        <asp:Button ID="btnExcel" runat="server" Text="Excel" />
        <br />
    </div>
    <br />
    <hr />
    <p>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Label" 
            Visible="False"></asp:Label>
    </p>
    <p>&nbsp;&nbsp;Add or change rates here. Enter as decimal equivalents.&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;
        </p>
    <table>
        <tr>
            <td class="descriptionStyle"></td>
            <td class="textBoxWidth">
                &nbsp;</td>
            <td class="descriptionStyle">&nbsp;</td>
            <td class="textBoxWidth">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="descriptionStyle">Start Date:</td>
            <td><asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="d">
                </asp:CalendarExtender>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate"
                                Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" Type="Date">Invalid
                            </asp:CompareValidator>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999"
                        MaskType="Date"></asp:MaskedEditExtender>
            </td>
            <td class="descriptionStyle">Rate:</td>
            <td>
                <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtRate"
                                Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" Type="Double">Invalid Number</asp:CompareValidator>
            </td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSave" runat="server" Text="Add/Save" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:GridView ID="gdvConfiguration" runat="server" BackColor="White" 
        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Vertical" HorizontalAlign="Left" AutoGenerateColumns="False"
        onselectedindexchanging="gdvConfiguration_SelectedIndexChanging" 
        DataKeyNames="ROWID" >
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="start_date" DataFormatString="{0:MM/dd/yyyy}" 
                HeaderText="Start Date" />
            <asp:BoundField DataField="rate" HeaderText="Rate" />
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" 
                SelectText="Edit" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    <div style="text-align: center">
        <span class="style1">
            <br />
            <br />
        </span>
        <br />
        <br />
        <br />
        
        <br />
    </div>
    <asp:SqlDataSource ID="sql_SDS" runat="server" />
    </form>
</body>
</html>
