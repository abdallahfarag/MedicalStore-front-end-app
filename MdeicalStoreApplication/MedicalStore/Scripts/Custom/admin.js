$(document).ready(function () {
    $(".admins-add").click(function () {
        $(".catig").css("display", "none");
        $(".cat").css("display", "none");
        $(".main .form").css("display", "block");
        $(".order").css("display", "none");
        $(".mail").css("display", "none");
    });
    $(".categorie-add").click(function () {
        $(".cat").css("display", "block");
        $(".catig").css("display", "none");
        $(".main .form").css("display", "none");
        $(".order").css("display", "none");
        $(".mail").css("display", "none");
    });
    $(".products-add").click(function () {
        $(".catig").css("display", "block");
        $(".cat").css("display", "none");
        $(".order").css("display", "none");
        $(".main .form").css("display", "none");
        $(".mail").css("display", "none");
    });
    $(".order-status").click(function () {
        $(".order").css("display", "block");
        $(".catig").css("display", "none");
        $(".cat").css("display", "none");
        $(".main .form").css("display", "none");
        $(".mail").css("display", "none");
    });

    $(".mail1").click(function () {
        $(".mail").css("display", "block");
        $(".order").css("display", "none");
        $(".catig").css("display", "none");
        $(".cat").css("display", "none");
        $(".main .form").css("display", "none");
    });
});