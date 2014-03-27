<%@ Page Title="" Language="C#" MasterPageFile="~/ssc1900021/Bootstrap.master" AutoEventWireup="true" CodeFile="ssc1900021v3.aspx.cs" Inherits="ssc1900021_ssc1900021v3" %>
<%@ Register Src="~/ssc1900021/ssc1900021OpenStreamSecurity.ascx" TagName="Security" TagPrefix="ss" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title></title>
    
    <link href="Content/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    
    <!-- jTable style file -->
    <link href="AJAX/jtable.2.3.1/themes/lightcolor/blue/jtable.css" rel="Stylesheet" type="text/css" />
    
    <script type="text/javascript" src="AJAX/modernizr-2.6.2.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.10.3.js"></script>

    <script type="text/javascript" src="AJAX/jtable.2.3.1/external/json2.js"></script>
    
    <script type="text/javascript" src="AJAX/jtable.2.3.1/jquery.jtable.js"></script>
    <script type="text/javascript" src="AJAX/jtable.2.3.1/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <script type="text/javascript" src="Scripts/ssc1900021v3.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ss:Security ID="Security1" runat="server" />

<%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                    </asp:ScriptManager>--%>
    <div class="container">
        <br />
        <asp:Panel ID="Panel1" runat="server">
        <div class="row">
            <div class="row">
                <div class="col-md-1">
                    <input id="btnSave" runat="server" class="btn btn-primary" type="button" value="Save" onclick="Update();"  />
                    
                </div>
                <div class="col-md-1">
                    <input id="btnAddNew" class="btn btn-primary" type="button" value="Add New" onclick="Add();" />
                </div>
                <div class="col-md-1">
                    <input id="btnCancel" class="btn btn-primary" type="button" value="Cancel" onclick="ClearFields();" />
                </div>
            </div>
            <br />
            
                <div class="row">
                    <div class="col-md-8">
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                Merch Nbr:
                            </div>
                            <div class="col-md-3">
                                <input id="txtMerchNbr" type="text" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                RSS Location Nbr:
                            </div>
                            <div class="col-md-3">
                                <input id="txtRSSLocationNbr" type="text" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
        </div>
        </asp:Panel>
        <br />
        <br />
        <div class="row">
            <div class="col-md-6">
                <div id="NumbersContainer">
                </div>
            </div>
            <input id="Hidden1" type="hidden" />
        </div>
    </div>
</asp:Content>

