$(document).ready(function()
{
    $("#btn-login").click(function(){
        $(".signup-form").css("display","none");
        $(".login-form").css("display","block");
    });
    $("#btn-register").click(function(){
        $(".form-wrapped").css(" border","1px solid rgba(0,0,0,0.8)");
        $(".login-form").css("display","none");
        $(".signup-form").css("display","block");
    });
    $("#create-user").click(function(){
        $(".form-wrapped").css(" border","1px solid rgba(0,0,0,0.8)");
        $(".login-form").css("display","none");
        $(".signup-form").css("display","block");
    });
   
})