$(document).ready(function () {
    //move he last list item before the first item. The purpose of this is if the user clicks to slide left he will be able to see the last item.
    var sn_imgW = 0;
    var sn_numsp = 5;
    var p_last_right = $('#sn_carousel_ul img:last-child').outerWidth(true);
    $(".scrollableArea").children($(".scrollableArea img")).each(function () {
        sn_imgW = sn_imgW + $(this).outerWidth(true);
        sn_numsp = sn_numsp + 5;
    });
    var p_valuewidth = sn_imgW + sn_numsp;
    $('#sn_carousel_ul').css('width', p_valuewidth);
    //$('#sn_carousel_ul li:first').before($('#sn_carousel_ul li:last'));
    function Run_right() {
        //get the width of the items ( i like making the jquery part dynamic, so if you change the width in the css you won't have o change it here too ) '
        var item_width = $('#sn_carousel_ul img').outerWidth() + 5;
        alert(item_width);
        //calculae the new left indent of the unordered list
        var left_indent = parseInt($('#sn_carousel_ul').css('right')) - item_width;
        alert(left_indent);
        //make the sliding effect using jquery's anumate function '
        var carouel_right = left_indent * 1;
        var p_truWidthright = carouel_right + p_valuewidth;

        alert(p_last_right);
        if (p_truWidthright <= p_last_right + sn_numsp) {
            //alert(p_truWidthright);
            $('#sn_right_scroll img').hide();
        }
        else {
            $('#sn_right_scroll img').show();
            $('#sn_carousel_ul:not(:animated)').animate({ 'right': left_indent * 1 }, 0, function () {

                //get the first list item and put it after the last list item (that's how the infinite effects is made) '

                $('#sn_carousel_ul img:last').after($('#sn_carousel_ul img:first'));

                //and get the left indent to the default -210px
                //$('#sn_carousel_ul').css({ 'right': '0px' });
            });
        }

    }

    function Run_left() {
        var item_width = $('#sn_carousel_ul img').outerWidth() + 10;

        /* same as for sliding right except that it's current left indent + the item width (for the sliding right it's - item_width) */
        for (var i = 0; i <= item_width; i++) {
            var left_indent = parseInt($('#sn_carousel_ul').css('left')) + i;
            $('#sn_carousel_ul:not(:animated)').animate({ 'left': left_indent * 1 }, 335, function () {

                /* when sliding to left we are moving the last item before the first list item */
                $('#sn_carousel_ul img:first').before($('#sn_carousel_ul img:last'));

                /* and again, when we make that change we are setting the left indent of our unordered list to the default -210px */
                $('#sn_carousel_ul').css({ 'left': '0px' });
            });
        }
    }
    //when user clicks the image for sliding right        
    $('#sn_right_scroll img').click(function () {
        Run_right();
    });

    //when user clicks the image for sliding left
    $('#sn_left_scroll img').click(function () {
        Run_left();
    });

    //when user over the image for sliding left

});