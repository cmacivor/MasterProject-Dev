<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssc1900021v2.aspx.cs" Inherits="ssc1900021_AJAX_ssc1900021v2" %>
 <%@ Register Src="~/ssc1900021/ssc1900021Utilities.ascx" TagName="Utilities" TagPrefix="util" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<util:Utilities ID="Utilities" runat="server" />

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    

    <link href="../Content/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <!-- jTable style file -->
    <link href="jtable.2.3.1/themes/metro/blue/jtable.css" rel="Stylesheet" type="text/css" />
    
    <script type="text/javascript" src="modernizr-2.6.2.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="jquery-ui-1.10.0.js"></script>

    <script type="text/javascript" src="jtable.2.3.1/external/json2.js"></script>
    
    <script type="text/javascript" src="jtable.2.3.1/jquery.jtable.js"></script>
    <script type="text/javascript" src="jtable.2.3.1/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            
            $('#NumbersContainer').jtable({
                title: 'Dupont TruChoice',
                paging: true, //Enables paging
                pageSize: 10, //Actually this is not needed since default value is 10.
                sorting: true, //Enables sorting
                defaultSorting: 'RSS_Location_Nbr ASC', //Optional. Default sorting on first load.
                actions: {
                    listAction: '/ssc1900021/AJAX/ssc1900021v2.aspx/Numbers'
                },
                fields: {
                    RSS_Location_Nbr: {
                        title: 'RSS Location Nbr',
                        width: '50%'
                    },
                    Merch_Nbr: {
                        title: 'Merch Nbr',
                        width: '50%'
                    }
                }
            });

            $('#NumbersContainer').jtable('load');
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <div id="NumbersContainer"></div>
    </div>
    

    </form>
</body>

</html>
