$(document).ready(function(){
    $(".admins-add").click(function(){
      $(".catig").css("display", "none");
      $(".cat").css("display", "none");
      $(".main .form").css("display", "block");
    });
    $(".categorie-add").click(function(){
        $(".cat").css("display", "block");
        $(".catig").css("display", "none");
        $(".main .form").css("display", "none");
      });
      $(".products-add").click(function(){
        $(".catig").css("display", "block");
        $(".cat").css("display", "none");
        $(".main .form").css("display", "none");
      });
  });