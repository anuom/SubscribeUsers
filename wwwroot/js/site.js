// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#saveSubscription').click(function () {
        $('#exampleModal').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        var subusers = {};
        subusers.Uname = $("#userName").val();
        subusers.Uemail = $("#EmailAddress").val();

        $.ajax({
            url: '/Home/SubmitDetails',
            type: "POST",
            dataType: "json",
            data: subusers,
            success: function (data) {
                switch (data) {
                    case "Y":
                        $("#statusmessage").removeClass("statusmessage-sucess");
                        $("#statusmessage").removeClass("statusmessage-noentry");
                        $("#statusmessage").addClass("statusmessage-failure");
                        $("#statusmessage").text("This email address is already registered.");
                        $("#firstButton").text("Subscribe another email address");
                        break;
                    case "N":
                        $("#statusmessage").removeClass("statusmessage-failure");
                        $("#statusmessage").removeClass("statusmessage-noentry");
                        $("#statusmessage").addClass("statusmessage-sucess");
                        $("#statusmessage").text("Thank you for subscribing!!");
                        $("#firstButton").text("Subscribe another email address");
                        break;
                    case "NA":
                        $("#statusmessage").removeClass("statusmessage-sucess");
                        $("#statusmessage").removeClass("statusmessage-failure");
                        $("#statusmessage").addClass("statusmessage-noentry");
                        $("#statusmessage").text("Please enter an email address.");
                }
            }
        });
       
    });
    
});
