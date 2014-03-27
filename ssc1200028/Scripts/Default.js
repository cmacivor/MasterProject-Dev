/// <reference path="jquery-1.4.1.js" />

function saveAdjustment() {
    var sku = $("#txtAdjustSKU").val();
    var status = $("#ddlSKUAdjustStatus option:selected").text();
//    if (sku == "" || status == "Please Select") {
//        alert('Please enter a SKU and/or choose whether to increase or reduce the facing.');
//    }
   // if ((sku != "" || sku!= null) || status != "Please Select") {
        PageMethods.AcceptFacingStatus(sku, status, onSuccess, onError);
        function onSuccess() {
            alert('Successfully changed');
        }

        function onError(result) {
            alert('Something wrong');
        }
   // }
}

$(document).ready(function () {

//    $("#ddlPlanograms").change(function () {
//        alert('test');
//        var selected = $("#ddlPlanograms").val();
//        alert(selected);
//        PageMethods.LoadDataTable(selected);
//    });


    //    $("#btnSaveFacingStatus").click(function (e) {
    //        e.preventDefault();
    //        //var ddlstatus = document.getElementById('<%=ddlStatus.ClientID%>');
    //        var statusvalue = $("#<%=ddlStatus.ClientID%>").val();
    //        saveSKU($("#txtSKU").val(), statusvalue, $("#txtDescription").val(), $("#txtNewItemStrategy").val());
    //    });


    //    $("#btnSaveFacingStatus").click(function (e) {
    //        e.preventDefault();
    //        //var ddlstatus = document.getElementById('<%=ddlStatus.ClientID%>');
    //        //var statusvalue = $("#<%=ddlSKUAdjustStatus.ClientID%>").val();

    //        var statusvalue = $("#ddlSKUAdjustStatus option:selected").text();
    //        //var statusvalue = "test";
    //        saveAdjustment($("#txtAdjustSKU").val(), statusvalue);
    //    });


    //    function saveAdjustment(sku, status) {
    //        //var loc = window.location.href;
    //        //loc = (loc.substr(loc.length - 1, 1) == "/") ? loc +
    //        //"ssc1200028_EditSKU.aspx" : loc;
    //        $.ajax({
    //            type: "POST",
    //            url: "ssc1200028_EditSKU.aspx/AcceptFacingStatus",
    //            data: '{"sku":"' + sku + '","status":"' + status + '"}',
    //            contentType: "json",
    //            success: function (msg) {
    //                //if (msg.d)
    //                //alert("success");
    //                // else
    //                //alert("failure test")
    //                alert(msg);
    //            },
    //            failure: function (response) {
    //                alert(response);
    //                },
    //            error: function () {
    //                alert("An unexpected error has occurred.");
    //            }
    //        });
    //    }

    $("#Result").click(function () {
        $.ajax({
            type: "POST",
            url: "ssc1200028_EditSKU.aspx/GetDate",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                // Replace the div's content with the page method's return.
                $("#Result").text(msg.d);
            }
        });
    });

    //for adding a new SKU
    //    function saveSKU(sku, status, description, strategy) {
    //        var loc = window.location.href;
    //        loc = (loc.substr(loc.length - 1, 1) == "/") ? loc +
    //        "ssc1200028_EditSKU.aspx" : loc;
    //        $.ajax({
    //            type: "POST",
    //            url: loc + "/SaveNewSku",
    //            data: "{'sku': '" + sku + "' ,status: '" + status + "' ,description: '" + description + "' ,strategy: '" + strategy + "'}",
    //            contentType: "json",
    //            success: function (msg) {
    //                if (msg.d)
    //                    alert("success");
    //                else
    //                    alert("failure test")
    //            },
    //            error: function () {
    //                alert("An unexpected error has occurred.");
    //            }
    //        });
    //    }

});