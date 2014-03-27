<%@ Page Title="" Language="C#" MasterPageFile="~/admin00100/admin00100_MasterPage.master" AutoEventWireup="true" CodeFile="admin00100_details.aspx.cs" Inherits="admin00100_admin00100_details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .tableStyle
        {
            width: 930px;
        }
        .firstRowDropDown
        {
            width:auto;
        }
        .style4
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 60px;
        }
        .style6
         {
            width: 78px;
        }
        .style9
        {
            width: 54px;
        }
        .firstRowText
        {
            width: 123px;
            text-align: right;
            padding-right:10px;
        }
        .style11
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 118px;
        }
        .style12
        {
            width: 36px;
        }
        .style13
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            text-align: center;
        }
        .style14
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 53px;
        }
        .style15
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
        }
        .style16
        {
            border-style: solid;
            border-width: 2px;
            
            width: auto;
        }
        .style17
        {
            border-style: solid;
            border-width: 2px;
            width: 140px;
            text-align:center;
            padding-right:10px
        }
        .style18
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 135px;
        }
        .style19
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            text-align: left;
        }
        .style30
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 129px;
        }
        .style34
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 148px;
        }
        .style36
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 160px;
        }
        .style40
        {
            height: 25px;
        }
        .style41
        {
            width: 148px;
            height: 25px;
        }
        .style43
        {
            width: 160px;
            height: 25px;
        }
        .style44
        {
            font-size: small;
        }
        .style45
        {
            height: 25px;
            width: 128px;
        }
        .style46
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 128px;
        }
        .style50
        {
            height: 25px;
            width: 127px;
        }
        .style51
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 127px;
        }
        .style52
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 448px;
            height: 46px;
        }
        .style53
        {
            border-style: solid;
            border-width: 2px;
            padding: 1px 4px;
            width: 148px;
            height: 46px;
        }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        Activity details</h2>
        <p><strong>Please note that fields highlighted in red below need to be filled out 
            before the Activity can be updated. All fields on this page are required.&nbsp;To 
            make changes to a field, click the Edit button, and then Save to save your 
            changes.</strong></p>
    <asp:Label ID="lblErrorDetails" runat="server" ForeColor="Red" Text="Label" 
        Visible="False"></asp:Label>
    <br />
    <p style="text-align:right">
        <asp:Button ID="btnCancelEdit" runat="server" Enabled="False" 
            Text="Cancel Editing" onclick="btnCancelEdit_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnEdit" runat="server" Text="Edit" onclick="btnEdit_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSave" runat="server" Text="Save " onclick="btnSave_Click"  />
    </p>
        <table class="tableStyle">
           <tr>
               <td class="style17">
                   <strong style="text-align: left">Activity Group Name:</strong></td>
               <td class="style16">
                   <asp:TextBox ID="txtActivityGroupName" runat="server" 
                       Width="244px" Font-Bold="True" ForeColor="Black" ReadOnly="True"></asp:TextBox>
               </td>
               <td class="style17">
                   <strong>Activity Name:</strong></td>
               <td class="style16">
                   <asp:TextBox ID="txtActivityName" runat="server" Width="255px" 
                       Font-Bold="True" ForeColor="Black" ReadOnly="True"></asp:TextBox>
               </td>
              
           </tr>
           </table>
           <br />
           <table class="tableStyle">
           <tr>
               <td class="style18">
                   <strong>Original
                   Programmer: </strong> </td>
               <td class="style15">
               &nbsp;<asp:DropDownList 
                       ID="ddlProgrammer" runat="server" DataSourceID="sqldsProgrammers" 
                       DataTextField="Name" DataValueField="Name" Enabled="False">
                   <asp:ListItem></asp:ListItem>
                   </asp:DropDownList>
                   <asp:Label ID="lblOriginalProgrammer" runat="server" Text="Label" 
                       Visible="False" Enabled="False" Font-Bold="True" ForeColor="Black"></asp:Label>
                   <asp:SqlDataSource ID="sqldsProgrammers" runat="server" 
                       ConnectionString="<%$ ConnectionStrings:admin00100_TrackerLocalDBConnectionString %>" 
                       
                       SelectCommand="SELECT [ProgrammerID], [Name] FROM [web_tracker_Programmer]">
                   </asp:SqlDataSource>
               </td>
               <td class="style6">
                   &nbsp;</td>
               <td class="style4">
                   <strong>VB:</strong></td>
               <td class="style14">
                   <asp:RadioButtonList ID="rblVB" runat="server" Enabled="False" Width="64px">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td class="style4">
                   <strong>.NET:</strong></td>
               <td class="style13">
                   <asp:RadioButtonList ID="rblNET" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
           </tr>
           <tr>
               <td class="style18">
                   <strong>Activity ID:</strong></td>
               <td class="style15">
                   <asp:TextBox ID="txtActivityID" runat="server" Font-Bold="True" ReadOnly="True"></asp:TextBox>
               </td>
               <td class="style6">
                   &nbsp;</td>
               <td class="style4">
                   <strong>C#:</strong></td>
               <td class="style14">
                   <asp:RadioButtonList ID="rblCSharp" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td class="style4">
                   <strong>Javascript:</strong></td>
               <td class="style13">
                   <asp:RadioButtonList ID="rblJavaScript" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
           </tr>
           <tr>
               <td class="style18">
                   <strong>Project Number:</strong></td>
               <td class="style15">
                   <asp:TextBox ID="txtProject" runat="server" Font-Bold="True"></asp:TextBox>
               </td>
               
               <td class="style9">
                   &nbsp;</td>
               <td class="style19">
                   <strong>ASP:</strong></td>
               <td class="style14">
                   <asp:RadioButtonList ID="rblASP" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td class="style4">
                   <strong>AJAX:</strong></td>
               <td class="style13">
                   <asp:RadioButtonList ID="rblAJAX" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
           </tr>
           </table>
           <br />
           <table>
           <tr>
               <td class="style41" >
                   </td>
               <td class="style50" >
                   </td>
               <td class="style40" >
                   </td>
               <td class="style43">
                   <strong><asp:RangeValidator 
                        ID="RangeValidator1" runat="server" ControlToValidate="txtModifiedDate" 
                        Display="Dynamic" ErrorMessage="Please enter a valid date." ForeColor="Red" 
                        MaximumValue="01/01/2100" MinimumValue="01/01/1900" Type="Date"></asp:RangeValidator>
                   </strong></td>
               
               
               <td class="style40" >
                   </td>
               
               
               <td class="style40" >
                   </td>
               
               
               <td class="style45" >
                   <strong>&nbsp;&nbsp;<span class="style44">Location of Code:</span></strong></td>
               
               
               <td class="style40" >
                   </td>
           </tr>
           <tr>
               <td class="style34">
                   <strong>Last Modified By:</strong></td>
               <td class="style51">
                   <asp:DropDownList ID="ddlLastModifiedBy" runat="server" 
                       DataSourceID="sqldsProgrammers" DataTextField="Name" DataValueField="Name" 
                       Font-Bold="True">
                   </asp:DropDownList>
               </td>
               <td class="style30">
                   <strong>Last Modified Date:</strong></td>
               <td class="style36">
                   <asp:TextBox ID="txtModifiedDate" runat="server" Font-Bold="True"></asp:TextBox>
                   <asp:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="txtModifiedDate" Format="d">
                   </asp:CalendarExtender>
          
               </td>
               
               
               <td class="style12">
                   &nbsp;</td>
               
               
               <td class="style12">
                   &nbsp;</td>
               
               
               <td class="style46">
                   <strong>\\hathor\ web\ openss\ activities</strong></td>
               
               
               <td class="style14">
                   <asp:RadioButtonList ID="rblCodeLocation1" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
           </tr>
           
           <tr>
               <td class="style34">
                   <strong>Does Activity Contain<br />
                   Data Controls in OS?</strong></td>
               <td class="style51">
                   <asp:RadioButtonList ID="rblDataControls" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td class="style30">
                   <strong>Is an OS Security
                   <br />
                   Group Available?</strong> </td>
               <td class="style36">
                   <asp:RadioButtonList ID="rblOSSecurityGroup" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td class="style12">
                   &nbsp;</td>
               <td class="style12">
                   &nbsp;</td>
               <td class="style46">
                   <strong>\\hebe\ openssapps</strong></td>
               <td class="style14">
                   <asp:RadioButtonList ID="rblCodeLocation2" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
           </tr>
           <tr>
               <td class="style34">
                   <strong>Is there an OS<br />
                   Security Doc?</strong></td>
               <td class="style51">
                   <asp:RadioButtonList ID="rblOSSecurityDoc" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td class="style30">
                   <strong>IPad Enabled?</strong></td>
               <td class="style36">
                   <asp:RadioButtonList ID="rblIpadEnabled" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               
               
               <td class="style12">
                   &nbsp;</td>
               
               
               <td class="style12">
                   &nbsp;</td>
               
               
               <td class="style46">
                   <strong>\\hebe\ openssapps2</strong></td>
               
               
               <td class="style14">
                   <asp:RadioButtonList ID="rblCodeLocation3" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               
               
           </tr>
           <tr>
               <td class="style34">
                   <strong>Pop-up Window?</strong></td>
               <td class="style51">
                   <asp:RadioButtonList ID="rblPopUpWindow" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td class="style11">
                   <strong>Is Activity Discontinued?</strong></td>
               <td class="style36">
                   <asp:RadioButtonList ID="rblActivityDiscontinued" runat="server" 
                       Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               
               <td class="style12">
                   &nbsp;</td>
               
               <td class="style12">
                   &nbsp;</td>
               
               <td class="style46">
                   <strong>\\hebe\ openssapps4</strong></td>
               
               <td class="style14">
                   <asp:RadioButtonList ID="rblCodeLocation4" runat="server" Enabled="False">
                       <asp:ListItem>Yes</asp:ListItem>
                       <asp:ListItem>No</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
           </tr>
    </table>
    <table>
        <tr>
            <td class="style53"><strong>Notes:</strong></td>
            <td class="style52">
                <asp:TextBox ID="txtNotes" runat="server" Height="39px" Width="431px" 
                    Enabled="False" Font-Bold="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style53"><strong>OSS Documentation:</strong></td>
            <td class="style52">
                <asp:HyperLink ID="HyperLink1" runat="server" Visible="False" Target="_blank">Open Document</asp:HyperLink>
&nbsp;&nbsp;
                <asp:TextBox ID="txtDocLink" runat="server" Enabled="False" Font-Bold="False"></asp:TextBox>
&nbsp;&nbsp;
                <asp:Label ID="lblURL" runat="server" Text="Label"></asp:Label>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

