// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#saveSubscription').click(function () {
        var subusers = {};
        subusers.Uname = $("#userName").val();
        subusers.Uemail = $("#EmailAddress").val();

        console.log(subusers);

        //$.ajax({
        //    url: '/Home/SubmitDetails',
        //    type: "POST",
        //    dataType: "application/json",
        //    data: { subusers: userDetails },
        //    success: function (data) {
        //        console.log(data);
        //    }
        //});

        fetch('/Home/SubmitDetails', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(subusers)
        })
            .then(response => {
                return response.json();
            })

            .then(function (data) {
                const myObj = JSON.parse(data);
                console.log(myObj);
            })

            .catch(error => {
                console.error(error);
            });
    });
    
});
