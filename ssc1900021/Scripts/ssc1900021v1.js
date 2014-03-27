//alert('test');
$(document).ready(function () {
    //to work with the gridview directly- gets the value of clicked cell and populates textbox
    //$('#ifrm1').contents().find("#tblDupont").click(function(e) {
    $("#GridView1").click(function (e) {

        //this doesn't work
        //$("#GridView1").filter(":not(:has(table, th))").click(function (e) {

        //get the value of the cell
        var $cell = $(e.target).closest("td");
        //alert($cell.text());
        //get the value of the header column
        var $th = $cell.closest('table').find('th').eq($cell.index());


        //var $prevCell = $cell.closest("td").next('td');
        //alert('You have selected: ' + $cell.text());
        //alert('You have selected: ' + $th.text());
        //alert('test');

        //$("#TextBox1").val($cell.text());

        if ($th.text() == "Merch Nbr") {
            $("#txtMerchNbr").val($cell.text());
            $("#Hidden1").val($cell.text());
            //alert($prevCell.text());
            //alert('You have selected: ' + $cell.text());
            //alert($("#Hidden1").val());
        }
    });

    //to update the value 
    //    $("#Button1").click(function (e) {
    //        if ($("#txtMerchNbr").val() != '') {
    //            sendData();
    //            e.preventDefault();
    //            //__doPostBack('__doPostBack', '');
    //            reloadPage();
    //        }
    //        else {
    //            alert('Please select a merchant number to update.');
    //        }
    //        return false;
    //    });

    //    $("#btnAdd").click(function (e) {
    //        Add();
    //    });

    //    function Add() {
    //        //alert('test');
    //        if ($("#txtMerchNbr").val() != '' &&  $("#txtRSSLocationNbr").val() != '') {
    //            if ($("#txtMerchNbr").val().length <= 5) {
    //                AddRecord();
    //                e.preventDefault();
    //                //__doPostBack('__doPostBack', '');
    //                reloadPage();
    //            }
    //            else {
    //                alert('The RSS number must be 5 characters.');
    //            }
    //        }
    //        else {
    //            alert('Please select a merchant number to update.');
    //        }
    //        return false;
    //    }

    //to load using jquery- paging doesn't work with this method
    //    $(function () {
    //        $('#grid').load("/DupontData");
    //    });


    //for jTable
    //    $('#DupontTableContainer').jtable({
    //        title: 'Dupont Table Maintenance',
    //        paging: true, //Enables paging
    //        pageSize: 10, //Actually this is not needed since default value is 10.
    //        sorting: true, //Enables sorting
    //        defaultSorting: "RSS_Location_Nbr", //Optional. Default sorting on first load.
    //        actions: {
    //            //listAction: '/Test.aspx/ListDupont',

    //            listAction: "Test.aspx/ListDupont",
    //            //listAction: "Test.aspx/ListDupont?jtStartIndex=0&jtPageSize=10&jtSorting='RSS_Location_Nbr ASC'",
    //            //listAction: "Test.aspx/ListDupont?jtStartIndex=0&jtPageSize=10&jtSorting='ASC'",

    //            //createAction: '/PagingAndSorting.aspx/CreateStudent',
    //            //updateAction: '/Test.aspx/UpdateDupont'

    //            updateAction: "Test.aspx/UpdateDupont"

    //            //deleteAction: '/PagingAndSorting.aspx/DeleteStudent'
    //        },
    //        fields: {
    //            RSSLocatioNbr: {
    //                key: false,
    //                create: false,
    //                edit: false,
    //                list: false
    //            },
    //            MerchNbr: {
    //                title: 'MerchNbr',
    //                width: '23%'
    //            },
    //            RSSLocatioNbr: {
    //                title: 'RSSLocatioNbr',
    //                list: true
    //            }
    //        }
    //    });

    //    $('#DupontTableContainer').jtable('load');

});

//function ClearFields() {
//    $("#txtMerchNbr").val() = '';
//    $("#txtRSSLocationNbr").val();
//}



//function reloadPage() {
//    $.ajax({
//        url: "",
//        context: document.body,
//        success: function (s, x) {
//            $(this).html(s);
//        }
//    });
//}

//function AddRecord() {
//    var merchNbr = $("#txtMerchNbr").val();
//    var rssNbr = $("#txtRSSLocationNbr").val();
//        var path = "ssc1900021.aspx/InsertDupont"
//        var Data = JSON.stringify({ rsslocnumber: rssNbr, merchnbr: merchNbr });
//        $.ajax
//                ({
//                    type: 'POST',
//                    url: path,
//                    //data: "{'oldNumber':'" + oldNumber + "','newNumber':'"+ newNumber +"'}",
//                    data: Data,
//                    dataType: 'json',
//                    contentType: "application/json; charset=utf-8",
//                    success: function (response) {
//                        if (response != null && response.d != null) {
//                            if (response.d) {
//                                //alert('The record has')
//                            }
//                            else {
//                                alert('Failure')
//                            }
//                        }
//                    }

//                }); 
//}


//function sendData() {
//var oldNumber = $("#Hidden1").val();
//var newNumber = $("#txtMerchNbr").val();
//var path = "ssc1900021.aspx/UpdateDupont"
//var Data = JSON.stringify({ oldNumber: oldNumber, newNumber: newNumber });
//        $.ajax
//            ({
//                type: 'POST',
//                url: path,
//                //data: "{'oldNumber':'" + oldNumber + "','newNumber':'"+ newNumber +"'}",
//                data: Data,
//                dataType: 'json',
//                contentType: "application/json; charset=utf-8",
//                success: function (response) {
//                    if (response != null && response.d != null) {
//                        if (response.d) {
//                            //alert('The record has')
//                        }
//                        else {
//                            alert('Failure')
//                        }
//                    }
//                }

//            }); 
//        }
