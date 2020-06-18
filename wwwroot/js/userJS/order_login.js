$(document).ready(function () {
    $('.login-form').on('click',function()
    {
        $('.register-form').removeClass('active');
        $('#register-form').css("display", "none");
        $('.login-form').addClass('active');
        $('#login-form').css("display", "block");    
    });
    $('.register-form').on('click',function()
    {
        $('.login-form').removeClass('active');
        $('#login-form').css("display", "none"); 
        $('.register-form').addClass('active');
        $('#register-form').css("display", "block"); 
    })
})