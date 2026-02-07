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

    // courses carousel – 3 boxes like your perfect picture
    $(".course-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        margin: 25,
        dots: true,
        loop: true,
        responsive: {
            0: { items: 1 },
            576: { items: 1 },
            768: { items: 2 },
            992: { items: 3 }
        }
    });
});
var spinnerShow = function () {
    if ($('#spinner').length > 0) {
        $('#spinner').addClass('show');
    }
}
