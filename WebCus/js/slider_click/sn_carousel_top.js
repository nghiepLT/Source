$(document).ready(function () {

    function Run_top() {
        var item_height = $('#sn_carousel_Top img').outerHeight() + 0;

        var top_indent = parseInt($('#sn_carousel_Top').css('top')) - item_height;

        $('#sn_carousel_Top').animate({ 'top': top_indent }, 450, function () {

            $('#sn_carousel_Top img:last').after($('#sn_carousel_Top img:first'));

            $('#sn_carousel_Top').css({ 'top': '0px' });
        });
    }
    setInterval(Run_top, 3400);

    function Run_top_right() {
        var item_height = $('#sn_carousel_Top_right img').outerHeight() + 0;

        var top_indent = parseInt($('#sn_carousel_Top_right').css('top')) - item_height;

        $('#sn_carousel_Top_right').animate({ 'top': top_indent }, 450, function () {

            $('#sn_carousel_Top_right img:last').after($('#sn_carousel_Top_right img:first'));

            $('#sn_carousel_Top_right').css({ 'top': '0px' });
        });
    }
    setInterval(Run_top_right, 3400);
});