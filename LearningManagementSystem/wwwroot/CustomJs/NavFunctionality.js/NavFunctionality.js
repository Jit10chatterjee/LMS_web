$(document).ready(function () {
    debugger
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();
    $.ajax({
        url: "/LandingPage/IsUserLoggedIn",
        type: "GET",
        success: function (result) {
            if (result.loggedIn == true) {
                $("#JoinHere").addClass("d-none").removeClass("d-lg-block");
                $("#UserProfileSection").removeClass("d-none")
            } else {
                $("#JoinHere").removeClass("d-none").addClass("d-lg-block");
                $("#UserProfileSection").addClass("d-none")
            }
        },
        error: function (xhr, status, error) {
            alert("Error : " + error);
        }
    });

    $("#JoinHere").click(function () {
        debugger
        spinnerShow();
        $.ajax({
            url: "/LandingPage/IsUserLoggedIn",
            type: "GET",
            success: function (result) {
                if (result.loggedIn == false) {
                    window.location.href = "/Identity/Account/Login";
                }
            },
            error: function (xhr, status, error) {
                alert("Error : " + error);
            }
        })
    })
});
var spinnerShow = function () {
    if ($('#spinner').length > 0) {
        $('#spinner').addClass('show');
    }
}
