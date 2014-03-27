$(document).ready(function () {

    $('#NumbersContainer').jtable({
        title: 'Dupont TruChoice',
        paging: true, //Enables paging
        pageSize: 10, //Actually this is not needed since default value is 10.
        sorting: true, //Enables sorting
        selecting: true,
        defaultSorting: 'RSS_Location_Nbr ASC', //Optional. Default sorting on first load.
        actions: {
            listAction: '/ssc1900021/ssc1900021v3.aspx/Numbers'
        },
        fields: {
            RSS_Location_Nbr: {
                title: 'RSS Location Nbr',
                width: '50%'

            },
            Merch_Nbr: {
                title: 'Merch Nbr',
                width: '50%',
                key: true
            }
        },
        recordsLoaded: function (event, data) {
            $('.jtable-data-row').click(function () {
                var row_id = $(this).attr('data-record-key');
                //alert('clicked row with id ' + row_id);
                $("#txtMerchNbr").val(row_id);
                $("#Hidden1").val(row_id);
            });
        }
    });

    $('#NumbersContainer').jtable('load');

    //    $('#dialogEdit').dialog({
    //        autoOpen: false,
    //        height: 300,
    //        width: 350,
    //        modal: true
    //    });


    //    $('#btnEdit').click(function () {
    //        $('#dialogEdit').dialog('open');
    //        return false;
    //    });

//    $("#btnSave").click(function (e) {
//        if ($("#txtMerchNbr").val() != '') {
//            sendData();
//            $('#NumbersContainer').jtable('load');
//            e.preventDefault();
//            //__doPostBack('__doPostBack', '');
//            //reloadPage();
//            ClearFields();
//            $('#NumbersContainer').jtable('load');
//        }
//        else {
//            alert('Please select a merchant number to update.');
//        }
//        return false;
//    });

});


function ClearFields() {
    $("#txtMerchNbr").val("");
    $("#txtRSSLocationNbr").val("");
    $("#Hidden1").val("");
}


function testalert() {
    alert("test");
}

function reloadPage() {
    $.ajax({
        url: "",
        context: document.body,
        success: function (s, x) {
            $(this).html(s);
        }
    });
}

function Update() {
    if ($("#txtMerchNbr").val() != '') {
        sendData();
        //$('#NumbersContainer').jtable('load');
        //e.preventDefault();
        //__doPostBack('__doPostBack', '');
        //reloadPage();
        ClearFields();
        $('#NumbersContainer').jtable('load');
    }
    else {
        alert('Please select a merchant number to update.');
    }
    return false;
}


function sendData() {
    
    var storeRecord = {};
    //storeRecord.RSS_Location_Nbr = $("#txtRSSLocationNbr").val();
    storeRecord.Merch_Nbr = $("#txtMerchNbr").val();
    storeRecord.Old_Merch_Nbr = $("#Hidden1").val();
    var pdata = { "p": storeRecord };

    var path = "ssc1900021v3.aspx/Update";
    //var Data = JSON.stringify({ oldNumber: oldNumber, newNumber: newNumber });
    $.ajax
            ({
                type: 'POST',
                url: path,
                //data: "{'oldNumber':'" + oldNumber + "','newNumber':'"+ newNumber +"'}",
                //data: Data,
                data: JSON.stringify(pdata),
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response != null && response.d != null) {
                        if (response.d) {
                            //alert('The record has')
                        }
                        else {
                            alert('Failure')
                        }
                    }
                }
            });
        }


function Add() {
    //alert('test');
    if ($("#txtMerchNbr").val() != '' && $("#txtRSSLocationNbr").val() != '') {
        if ($("#txtRSSLocationNbr").val().length <= 5) {
            AddRecord();
            $('#NumbersContainer').jtable('load');
            ClearFields();
            //e.preventDefault();
            //__doPostBack('__doPostBack', '');
            //reloadPage();

        }
        else {
            alert('The RSS number must be 5 characters.');
        }
    }
    else {
        alert('Please select a merchant number to update.');
    }
    return false;
}


function AddRecord() {
    //var merchNbr = $("#txtMerchNbr").val();
    //var rssNbr = $("#txtRSSLocationNbr").val();

    var storeRecord = {};
    storeRecord.Merch_Nbr = $("#txtMerchNbr").val();
    storeRecord.RSS_Location_Nbr = $("#txtRSSLocationNbr").val();
    var pdata = { "p": storeRecord };

    var path = "ssc1900021v3.aspx/Insert"
    //var Data = JSON.stringify({ rsslocnumber: rssNbr, merchnbr: merchNbr });
    $.ajax
        ({
            type: 'POST',
            url: path,
            //data: "{'oldNumber':'" + oldNumber + "','newNumber':'"+ newNumber +"'}",
            //data: Data,
            data: JSON.stringify(pdata),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response != null && response.d != null) {
                    if (response.d) {
                        //alert('The record has')
                    }
                    else {
                        alert('Failure')
                    }
                }
            }

        });
}