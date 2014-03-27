<%@ Page Title="" Language="C#" MasterPageFile="~/admin00100/admin00100_MasterPage.master" AutoEventWireup="true" CodeFile="admin00100_LatestActivities.aspx.cs" Inherits="admin00100_admin00100_LatestActivities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    </p>
    <p>
        Use this page to get any new Activities that have been added to OpenStream. 
        Click the &quot;Reconcile With OpenStream&quot; button to pull new OpenStream activities that have not 
        been updated in the Activity Tracker, and then under the &quot;Latest Activities&quot; tab, click the Select button to complete the 
        record. The &quot;Deleted&quot; tab will show Activities that have been removed from 
        OpenStream that are now flagged as deleted in the Activity Tracker.&nbsp; The Details page will show the fields that need to be completed.</p>
    <p>
        <asp:Label ID="lblInformation" runat="server" Text="Label" ForeColor="#0033CC" 
            Visible="False"></asp:Label>
        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
            Text="Test" Visible="False"></asp:Label>
</p>
    <p>
        <asp:Button ID="btnGetLatestActivities" runat="server" 
            onclick="btnGetLatestActivities_Click" Text="Reconcile With OpenStream" />
    </p>
    
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <asp:TabPanel runat="server" HeaderText="Latest Activities" ID="TabPanel1">
        <ContentTemplate>
        <asp:GridView ID="gdvLatestActivities" runat="server" AllowPaging="True" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" 
        HorizontalAlign="Center" style="text-align: center" 
        DataKeyNames="OldActivityID" OnPageIndexChanging="gdvLatestActivities_PageIndexChanging"
        onselectedindexchanging="gdvLatestActivities_SelectedIndexChanging" 
                AutoGenerateColumns="False" >
           <AlternatingRowStyle BackColor="White" />
           <Columns>
               <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
               <asp:BoundField DataField="OldActivityID" HeaderText="Activity ID" />
               <asp:BoundField DataField="ActivityGroupName" 
                   HeaderText="Activity Group Name" />
               <asp:BoundField DataField="ActivityName" HeaderText="Activity Name" />
               <asp:BoundField DataField="ReconcileDate" HeaderText="Reconcile Date" />
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
        </ContentTemplate>
        </asp:TabPanel>
        <%--<asp:TabPanel runat="server" HeaderText="Incomplete Records" ID="TabPanel2">
        <ContentTemplate>
        <p>
        <asp:Button ID="btnShowIncompleteRecords" runat="server" 
            onclick="btnShowIncompleteRecords_Click" Text="Show Incomplete Records" />
        </p>
        <asp:GridView ID="gdvIncompleteRecords" runat="server" AllowPaging="True" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" 
        HorizontalAlign="Center" style="text-align: center" 
        DataKeyNames="OldActivityID" OnPageIndexChanging="gdvIncompleteRecords_PageIndexChanging"
        onselectedindexchanging="gdvIncompleteRecords_SelectedIndexChanging" >
           <AlternatingRowStyle BackColor="White" />
           <Columns>
               <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
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
        </ContentTemplate>
        </asp:TabPanel>--%>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Deleted Records">
        <ContentTemplate>
        <asp:GridView ID="gdvDeleted" runat="server" AllowPaging="True" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" 
        HorizontalAlign="Center" style="text-align: center" 
        DataKeyNames="OldActivityID" OnPageIndexChanging="gdvDeleted_PageIndexChanging"
        onrowcommand="gdvDeleted_RowCommand" AutoGenerateColumns="false" >
           <AlternatingRowStyle BackColor="White" />
           <Columns>
               <asp:ButtonField CommandName="Reactivate" Text="Reactivate" />
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
        </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <br />
</asp:Content>

