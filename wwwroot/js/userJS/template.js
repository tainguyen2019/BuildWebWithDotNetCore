$(document).ready(function(){
    // display background color for category list and search function
    $('.hamburger-btn').on('click',function()
    {
        $('.collapse').on('show.bs.collapse',function()
        {
            $('.hamburger').css('background','whitesmoke');            
        })      
        $('.collapse').on('hidden.bs.collapse',function(){
            $('.hamburger').css('background','transparent');
        })
    })
})