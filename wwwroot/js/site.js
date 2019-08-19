// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    //button click
    $('#saveSubscription').click(function () {
        setEvents();
    });

    //enter key press
    $('#exampleModal').keypress(function (e) {
        var key = e.which;
        if (key == 13) {
            setEvents();
        }
    });
});

//function to set the event listeners and start the process
function setEvents(){   
    //check if the email address is not empty then call else display a message
    if ($("#EmailAddress").val() != "") {
        makeCall();
        resetValues();
    }
    else {
        $("#noemail").addClass("statusmessage-noentry");
        $("#noemail").text("Please enter an email address.");
    }
}

//function to reset the values
function resetValues() {
    $("#noemail").removeClass("statusmessage-noentry");
    $("#noemail").text("");
    $("#userName").val("");
    $("#EmailAddress").val("");
}

//function to set the values if success
function setValues(data) {
    //add classes/styling to the message
    switch (data) {
        case "Y":
            $("#statusmessage").removeClass("statusmessage-invalid");
            $("#statusmessage").removeClass("statusmessage-success");
            $("#statusmessage").addClass("statusmessage-failure");
            $("#statusmessage").text("This email address is already registered.");
            
            $("#firstButton").text("Subscribe another email address");
            break;
        case "N":
            $("#statusmessage").removeClass("statusmessage-invalid");
            $("#statusmessage").removeClass("statusmessage-failure");
            $("#statusmessage").addClass("statusmessage-success");
            $("#statusmessage").text("Thank you for subscribing!!");
            $("#firstButton").text("Subscribe another email address");
            break;
        default:
            $("#statusmessage").removeClass("statusmessage-failure");
            $("#statusmessage").removeClass("statusmessage-success");
            $("#statusmessage").addClass("statusmessage-invalid");
            $("#statusmessage").text("Please enter a valid email address.");
            break;
    }
    clearText();
}

function clearText() {
    setTimeout(function () {
        $('#statusmessage').text("");;
    }, 2000);
}

//function to call the backend if valid
function makeCall() {
    //hide the pop-up
    $('#exampleModal').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();

    //create object
    var subusers = {};
    subusers.Uname = $("#userName").val();
    subusers.Uemail = $("#EmailAddress").val();

    //make call to the backend and send the parameter
    $.ajax({
        url: '/Home/SubmitDetails',
        type: "POST",
        dataType: "json",
        data: subusers,
        success: function (data) {
            //on success send data to set the values
            setValues(data);
        }
    });
}
