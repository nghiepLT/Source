$(document).ready(function () {
    //move he last list item before the first item. The purpose of this is if the user clicks to slide left he will be able to see the last item.

    //$('#sn_carousel_ul li:first').before($('#sn_carousel_ul li:last'));

    //when user clicks the image for sliding right
    $('#sn_right_scroll').click(function () {

        //get the width of the items ( i like making the jquery part dynamic, so if you change the width in the css you won't have o change it here too ) '
        var item_width = $('#sn_carousel_ul li').outerWidth() + 10;

        //calculae the new left indent of the unordered list
        var left_indent = parseInt($('#sn_carousel_ul').css('left')) - item_width;

        //make the sliding effect using jquery's anumate function '
        $('#sn_carousel_ul:not(:animated)').animate({ 'left': left_indent * 1 }, 300, function () {

            //get the first list item and put it after the last list item (that's how the infinite effects is made) '
            $('#sn_carousel_ul li:last').after($('#sn_carousel_ul li:first'));
            //$('#sn_carousel_ul li:last').after($('#sn_carousel_ul li:first'));
            //and get the left indent to the default -210px
            $('#sn_carousel_ul').css({ 'left': '0px' });
        });
    });

    //when user clicks the image for sliding left
    $('#sn_left_scroll').click(function () {

        var item_width = $('#sn_carousel_ul li').outerWidth() + 10;

        /* same as for sliding right except that it's current left indent + the item width (for the sliding right it's - item_width) */
        var left_indent = parseInt($('#sn_carousel_ul').css('left')) + item_width;

        $('#sn_carousel_ul:not(:animated)').animate({ 'left': left_indent * 1 }, 300, function () {

            /* when sliding to left we are moving the last item before the first list item */
            $('#sn_carousel_ul li:first').before($('#sn_carousel_ul li:last'));
            //$('#sn_carousel_ul li:first').before($('#sn_carousel_ul li:last'));

            /* and again, when we make that change we are setting the left indent of our unordered list to the default -210px */
            $('#sn_carousel_ul').css({ 'left': '0px' });
        });
    });

    setInterval(function () {
        if ($('#sn_carousel_ul').children().length > 1) {
            var item_height = $('#sn_carousel_ul div.css_box_imgHome').outerHeight() + 1;
            var left_indent = parseInt($('#sn_carousel_ul').css('top')) - item_height;
            $('#sn_carousel_ul:not(:animated)').animate({ 'top': left_indent * 1 }, 1000, function () {
                $('#sn_carousel_ul div.css_box_imgHome:last').after($('#sn_carousel_ul div.css_box_imgHome:first'));
                $('#sn_carousel_ul').css({ 'top': '0px' });
            });
        }
    }, 4600);


    setInterval(function () {
        if ($('#sn_carousel_ul2').children().length > 1) {
            var item_height = $('#sn_carousel_ul2 div.css_box_imgHome').outerHeight() + 1;
            var left_indent = parseInt($('#sn_carousel_ul2').css('top')) - item_height;
            $('#sn_carousel_ul2:not(:animated)').animate({ 'top': left_indent * 1 }, 300, function () {
                $('#sn_carousel_ul2 div.css_box_imgHome:last').after($('#sn_carousel_ul2 div.css_box_imgHome:first'));
                $('#sn_carousel_ul2').css({ 'top': '0px' });
            });
        }
    }, 4600);

    setInterval(function () {
        if ($('#sn_carousel_ul3').children().length > 2) {
            var item_height = $('#sn_carousel_ul3 div.css_box_imgHome').outerHeight() + 2;
            var left_indent = parseInt($('#sn_carousel_ul3').css('top')) - item_height;
            $('#sn_carousel_ul3:not(:animated)').animate({ 'top': left_indent * 1 }, 300, function () {
                $('#sn_carousel_ul3 div.css_box_imgHome:last').after($('#sn_carousel_ul3 div.css_box_imgHome:first'));
                $('#sn_carousel_ul3').css({ 'top': '0px' });
            });
        }
    }, 4600);
    /**/
//    $('.sn_carlbtnRight').click(function () {

//        //get the width of the items ( i like making the jquery part dynamic, so if you change the width in the css you won't have o change it here too ) '
//        var item_width = $('#sn_carousel_inner02 li').outerWidth() + 10;

//        //calculae the new left indent of the unordered list
//        var left_indent = parseInt($('#sn_carousel_ul02').css('left')) - item_width;

//        //make the sliding effect using jquery's anumate function '
//        $('#sn_carousel_ul02:not(:animated)').animate({ 'left': left_indent * 1 }, 300, function () {

//            //get the first list item and put it after the last list item (that's how the infinite effects is made) '
//            $('#sn_carousel_ul02 li:last').after($('#sn_carousel_ul02 li:first'));
//            //$('#sn_carousel_ul li:last').after($('#sn_carousel_ul li:first'));
//            //and get the left indent to the default -210px
//            $('#sn_carousel_ul02').css({ 'left': '0px' });
//        });
//    });

//    //when user clicks the image for sliding left
//    $('.sn_carlbtnLeft').click(function () {

//        var item_width = $('#sn_carousel_ul02 li').outerWidth() + 10;

//        /* same as for sliding right except that it's current left indent + the item width (for the sliding right it's - item_width) */
//        var left_indent = parseInt($('#sn_carousel_ul02').css('left')) + item_width;

//        $('#sn_carousel_ul02:not(:animated)').animate({ 'left': left_indent * 1 }, 500, function () {

//            /* when sliding to left we are moving the last item before the first list item */
//            $('#sn_carousel_ul02 li:first').before($('#sn_carousel_ul02 li:last'));
//            //$('#sn_carousel_ul li:first').before($('#sn_carousel_ul li:last'));

//            /* and again, when we make that change we are setting the left indent of our unordered list to the default -210px */
//            $('#sn_carousel_ul02').css({ 'left': '0px' });
//        });
//    });

//    setInterval(function () {
//        var item_width = $('#sn_carousel_ul02 li').outerWidth() + 10;

//        //calculae the new left indent of the unordered list
//        var left_indent = parseInt($('#sn_carousel_ul02').css('left')) - item_width;

//        //make the sliding effect using jquery's anumate function '
//        $('#sn_carousel_ul02:not(:animated)').animate({ 'left': left_indent * 1 }, 1000, function () {

//            //get the first list item and put it after the last list item (that's how the infinite effects is made) '
//            $('#sn_carousel_ul02 li:last').after($('#sn_carousel_ul02 li:first'));
//            //$('#sn_carousel_ul li:last').after($('#sn_carousel_ul li:first'));
//            //and get the left indent to the default -210px
//            $('#sn_carousel_ul02').css({ 'left': '0px' });
//        });
//    }, 4600);

    /**/
    $('#sn_right_scroll_sp').click(function () {

        if ($('#sn_carousel_ul_sp').children().length > 1) {

            var item_height = $('#sn_carousel_ul_sp li').outerHeight() + 2;
            var top_indent = parseInt($('#sn_carousel_ul_sp').css('top')) + item_height;

            $('#sn_carousel_ul_sp:not(:animated)').animate({ 'top': top_indent }, 500, function () {

                //get the first list item and put it after the last list item (that's how the infinite effects is made) '
                $('#sn_carousel_ul_sp li:first').before($('#sn_carousel_ul_sp li:last'));

                //and get the left indent to the default -210px
                $('#sn_carousel_ul_sp').css({ 'top': '-0px' });
            });
        }
    });

    //when user clicks the image for sliding left
    $('#sn_left_scroll_sp').click(function () {

        if ($('#sn_carousel_ul_sp').children().length > 1) {

            var item_height = $('#sn_carousel_ul_sp li').outerHeight() + 2;
            var top_indent = parseInt($('#sn_carousel_ul_sp').css('top')) - item_height;

            $('#sn_carousel_ul_sp:not(:animated)').animate({ 'top': top_indent }, 500, function () {

                //get the first list item and put it after the last list item (that's how the infinite effects is made) '
                $('#sn_carousel_ul_sp li:last').after($('#sn_carousel_ul_sp li:first'));

                //and get the left indent to the default -210px
                $('#sn_carousel_ul_sp').css({ 'top': '+0px' });
            });
        }
    });
    function Run_Up_1() {
        if ($('#sn_carousel_ul_sp').children().length > 1) {
            var item_height = $('#sn_carousel_ul_sp li').outerHeight() + 2;
            var top_indent = parseInt($('#sn_carousel_ul_sp').css('top')) - item_height;

            $('#sn_carousel_ul_sp:not(:animated)').animate({ 'top': top_indent }, 500, function () {

                //get the first list item and put it after the last list item (that's how the infinite effects is made) '
                $('#sn_carousel_ul_sp li:last').after($('#sn_carousel_ul_sp li:first'));

                //and get the left indent to the default -210px
                $('#sn_carousel_ul_sp').css({ 'top': '+0px' });
            });
        }
    }
    setInterval(Run_Up_1, 3000);
});

/**/

setInterval(function () {
    if ($('#sn_carousel_left_ul').children().length > 5) {
        var item_height = $('#sn_carousel_left_ul div.sn_Item').outerWidth() + 10;
        var left_indent = parseInt($('#sn_carousel_left_ul').css('left')) - item_height;
        $('#sn_carousel_left_ul:not(:animated)').animate({ 'left': left_indent * 1 }, 500, function () {
            $('#sn_carousel_left_ul div.sn_Item:last').after($('#sn_carousel_left_ul div.sn_Item:first'));
            $('#sn_carousel_left_ul').css({ 'left': '0px' });
        });
    }
}, 4600);
/**/
//

setInterval(function () {
    var item_width = $('#sn_coment_ul li').outerWidth() + 10;

    //calculae the new left indent of the unordered list
    var left_indent = parseInt($('#sn_coment_ul').css('left')) - item_width;

    //make the sliding effect using jquery's anumate function '
    $('#sn_coment_ul:not(:animated)').animate({ 'top': left_indent * 0.5 }, 700, function () {

        //get the first list item and put it after the last list item (that's how the infinite effects is made) '
        $('#sn_coment_ul li:last').after($('#sn_coment_ul li:first'));
        //$('#sn_carousel_ul li:last').after($('#sn_carousel_ul li:first'));
        //and get the left indent to the default -210px
        $('#sn_coment_ul').css({ 'top': '0px' });
    });
}, 5800);