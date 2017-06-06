/// <reference path="jquery-1.10.2.min.js" />

$(document).ready(function () {
    var serviceURL = '/Customer/CreateCustomer';

    $.ajax({
        type: "POST",
        url: serviceURL,
        data: param = "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {
        alert(data);
    }

    function errorFunc() {
        alert('error');
    }
});