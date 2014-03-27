/// <reference path="../Scripts/jquery-1.9.1.js" />
/// <reference path="../ssc1900021/AJAX/jtable.2.3.1/jquery.jtable.js" />
/// <reference path="../ssc1900021/AJAX/jtable.2.3.1/extensions/jquery.jtable.aspnetpagemethods.js" />
/// <reference path="../ssc1900021/AJAX/jtable.2.3.1/external/json2.js" />

$(document).ready(function () {
    $('#NumbersContainer').jtable({
        title: 'Dupont TruChoice',
        paging: true, //Enables paging
        pageSize: 10, //Actually this is not needed since default value is 10.
        sorting: true, //Enables sorting
        defaultSorting: 'RSS_Location_Nbr ASC', //Optional. Default sorting on first load.
        actions: {
            listAction: '/ssc1900021/AJAX/ssc1900021v2.aspx/Numbers'
            //createAction: '/PagingAndSorting.aspx/CreateStudent',
            //updateAction: '/PagingAndSorting.aspx/UpdateStudent',
            //deleteAction: '/PagingAndSorting.aspx/DeleteStudent'
        },
        fields: {
            RSSLocationNumber: {
                title: 'RSS Location Nbr'
            },
            MerchantNumber: {
                title: 'Merch Nbr'
            }
        }
    });

    //load the table
    $('#NumbersContainer').jtable('load');

});