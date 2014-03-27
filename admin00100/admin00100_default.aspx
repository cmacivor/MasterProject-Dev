<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/admin00100/admin00100_MasterPage.master" AutoEventWireup="true" CodeFile="admin00100_default.aspx.cs" Inherits="admin00100_admin00100_default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
                    
        .styletext
        {
            width: 40px;
            height: 40px;
            text-align:right;
        }
         .stylecheckbox
        {
            width: 40px;
            height: 40px;
            text-align:left;
        }
    
        .style1
      {
          width: 156px;
          text-align:right;
          padding-right:10px;
      }
      .style2
      {
          width: 157px;
      }
      .style3
      {
          width: 215px;
      }
      .style4
      {
          width: 290px;
      }
    
        </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Label" 
            Visible="False"></asp:Label>
    </p>
    <table>
        <tr>
            <td>
            <strong>Choose your search type:</strong></td>
        </tr>
        <tr>
            <td>
            <asp:RadioButtonList ID="rblSearchChoices" runat="server" 
            AutoPostBack="True" 
            onselectedindexchanged="rblSearchChoices_SelectedIndexChanged">
           <asp:ListItem>Activity</asp:ListItem>
           <asp:ListItem>Programmer</asp:ListItem>
           <asp:ListItem>Language</asp:ListItem>
                <asp:ListItem>IPad Enabled</asp:ListItem>
                <asp:ListItem>Activity Description</asp:ListItem>
            </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <hr />
    <br />
    <table id="tblActivities" runat="server" visible="false" style="height: 205px">
    <tr><td class="style3">
        <asp:Label ID="lblActivityGroupName" runat="server" Text="Activity Group Name:" 
            style="font-weight: 700"></asp:Label>
        </td>
    <td class="style4">
        <asp:Label ID="lblActivityName" runat="server" Text="Activity Name:" 
            style="font-weight: 700"></asp:Label>
        </td>
    <td class="style5">
        &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                <asp:SqlDataSource ID="sqldsActivityGroupName" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:admin00100_ATSwebConnectionString %>" 
                    
                  
                  SelectCommand="select distinct  group_descp from atsweb..web_activities order by  group_descp asc">
                </asp:SqlDataSource>
                <asp:ListBox ID="lbActivityGroupName" runat="server" AutoPostBack="True" 
                    DataSourceID="sqldsActivityGroupName" DataTextField="group_descp" 
                    DataValueField="group_descp" 
                    onselectedindexchanged="lbActivityGroupName_SelectedIndexChanged" 
                    SelectionMode="Multiple" Width="195px" Height="125px" 
                    style="margin-top: 0px"></asp:ListBox>
            </td>
            <td class="style6">
                <asp:ListBox ID="lboxAN" runat="server" SelectionMode="Multiple" DataSourceID="sqldsActivityName" 
                    DataTextField="activityname" DataValueField="activity_id" 
                    Width="244px" Height="125px" 
                    ></asp:ListBox>
                <asp:SqlDataSource ID="sqldsActivityName" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:admin00100_ATSwebConnectionString %>" 
                    
                    
                  
                  SelectCommand="select '0' as activity_id, 'all activities' as activityname
union
select activity_id, activity_id + ', ' + descp activityname from web_activities where group_descp = @group_descp">
                    <SelectParameters>
                        <asp:Parameter Name="group_descp" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <table id="tblLanguages" runat="server" visible="false">
        <tr>
            <td style="font-weight: 700">Language:</td>
            <td>
                   <asp:RadioButtonList ID="rblLanguages" runat="server" 
                       RepeatColumns="2">
                       <asp:ListItem>ASP Classic</asp:ListItem>
                       <asp:ListItem>ASP.NET</asp:ListItem>
                       <asp:ListItem>VB</asp:ListItem>
                       <asp:ListItem>C#</asp:ListItem>
                       <asp:ListItem>.NET</asp:ListItem>
                       <asp:ListItem>AJAX</asp:ListItem>
                       <asp:ListItem>Javascript</asp:ListItem>
                       <asp:ListItem>Other</asp:ListItem>
                   </asp:RadioButtonList>
               </td>
        </tr> 
    </table>
       <br />
    <table id="tblProgrammer" runat="server" visible="false">
    <tr>
        <td class="style1">
            <strong>&nbsp;Original Programmer: </strong> </td>
        <td class="style2">
            <asp:DropDownList ID="ddlProgrammer" runat="server" 
                DataSourceID="sqldsProgrammers" DataTextField="Name" 
                DataValueField="Name">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sqldsProgrammers" runat="server" 
                ConnectionString="<%$ ConnectionStrings:admin00100_ATSwebConnectionString %>" 
                       
                SelectCommand="SELECT [ProgrammerID], [Name] FROM [web_tracker_Programmer]">
            </asp:SqlDataSource>
        </td>
    </tr>
    </table>
    <table id="tblActivityDescription" runat="server" visible="false">
        <tr>
            <td><strong>Activity Description: </strong></td>
            <td>
                &nbsp;&nbsp;
                <asp:TextBox ID="txtActivityDescription" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
     <br />
    <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" 
               Text="Search" Width="136px" Height="36px" />
    &nbsp;
    <asp:Label ID="lblSearchResults" runat="server" ForeColor="Red" Text="Label" 
        Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnExport" runat="server" onclick="btnExport_Click" 
        Text="Export To Excel" Visible="False" />
    <br />
    <br />  
    <br />
       <asp:GridView ID="gdvSearchResults" runat="server" AllowPaging="True" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" 
        HorizontalAlign="Center" style="text-align: center" 
        DataKeyNames="OldActivityID" OnPageIndexChanging="gdvSearchResults_PageIndexChanging"
        onselectedindexchanging="gdvSearchResults_SelectedIndexChanging" 
        AutoGenerateColumns="False" >
           <AlternatingRowStyle BackColor="White" />
           <Columns>
               <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
               <asp:BoundField DataField="OldActivityID" HeaderText="Activity ID" />
               <asp:BoundField DataField="ActivityGroupName" 
                   HeaderText="Activity Group Name" />
               <asp:BoundField DataField="ActivityName" HeaderText="Activity Name" />
               <asp:BoundField DataField="LastModifiedBy" HeaderText="Last Modified By" />
           </Columns>
           <EditRowStyle BackColor="#2461BF" />
           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
           <RowStyle BackColor="#EFF3FB" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
           <SortedAscendingCellStyle BackColor="#F5F7FB" />
           <SortedAscendingHeaderStyle BackColor="#6D95E1" />
           <SortedDescendingCellStyle BackColor="#E9EBEF" />
           <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>


  </asp:Content>

