jQuery.fn.crossSlide = function(opts, plan) {

    var self = this,
			self_width = this.width(),
			self_height = this.height();

    // generic utilities
    function format(str) {
        for (var i = 1; i < arguments.length; i++)
            str = str.replace(new RegExp('\\{' + (i - 1) + '}', 'g'), arguments[i]);
        return str;
    }

    function abort() {
        arguments[0] = 'crossSlide: ' + arguments[0];
        throw format.apply(null, arguments);
    }

    // first preload all the images, while getting their actual width and height
    (function(proceed) {

        var n_loaded = 0;
        function loop(i, img) {
            // for (i = 0; i < plan.length; i++) but with independent var i, img (for the closures)
            img.onload = function(e) {
                n_loaded++;
                plan[i].width = img.width;
                plan[i].height = img.height;
                if (n_loaded == plan.length)
                    proceed();
            }
            img.src = plan[i].src;
            if (i + 1 < plan.length)
                loop(i + 1, new Image());
        }
        loop(0, new Image());

    })(function() {  // then proceed

        // utility to parse "from" and "to" parameters
        function parse_position_param(param) {
            var zoom = 1;
            var tokens = param.replace(/^\s*|\s*$/g, '').split(/\s+/);
            if (tokens.length > 3) throw new Error();
            if (tokens[0] == 'center')
                if (tokens.length == 1)
                tokens = ['center', 'center'];
            else if (tokens.length == 2 && tokens[1].match(/^[\d.]+x$/i))
                tokens = ['center', 'center', tokens[1]];
            if (tokens.length == 3)
                zoom = parseFloat(tokens[2].match(/^([\d.]+)x$/i)[1]);
            var pos = tokens[0] + ' ' + tokens[1];
            if (pos == 'left top' || pos == 'top left') return { xrel: 0, yrel: 0, zoom: zoom };
            if (pos == 'left center' || pos == 'center left') return { xrel: 0, yrel: .5, zoom: zoom };
            if (pos == 'left bottom' || pos == 'bottom left') return { xrel: 0, yrel: 1, zoom: zoom };
            if (pos == 'center top' || pos == 'top center') return { xrel: .5, yrel: 0, zoom: zoom };
            if (pos == 'center center') return { xrel: .5, yrel: .5, zoom: zoom };
            if (pos == 'center bottom' || pos == 'bottom center') return { xrel: .5, yrel: 1, zoom: zoom };
            if (pos == 'right top' || pos == 'top right') return { xrel: 1, yrel: 0, zoom: zoom };
            if (pos == 'right center' || pos == 'center right') return { xrel: 1, yrel: .5, zoom: zoom };
            if (pos == 'right bottom' || pos == 'bottom right') return { xrel: 1, yrel: 1, zoom: zoom };
            return {
                xrel: parseInt(tokens[0].match(/^(\d+)%$/)[1]) / 100,
                yrel: parseInt(tokens[1].match(/^(\d+)%$/)[1]) / 100,
                zoom: zoom
            };
        }

        // utility to compute the css for a given phase between p.from and p.to
        // phase = 1: begin fade-in,  2: end fade-in,  3: begin fade-out,  4: end fade-out
        function position_to_css(p, phase) {

            switch (phase) {
                case 1:
                    var pos = 0;
                    break;
                case 2:
                    var pos = fade_ms / (p.time_ms + 2 * fade_ms);
                    break;
                case 3:
                    var pos = 1 - fade_ms / (p.time_ms + 2 * fade_ms);
                    break;
                case 4:
                    var pos = 1;
                    break;
            }
            return {
                left: Math.round(p.from.left + pos * (p.to.left - p.from.left)),
                top: Math.round(p.from.top + pos * (p.to.top - p.from.top)),
                width: Math.round(p.from.width + pos * (p.to.width - p.from.width)),
                height: Math.round(p.from.height + pos * (p.to.height - p.from.height))
            };
        }

        // check global params
        if (!opts.fade)
            abort('missing fade parameter.');
        if (opts.speed && opts.sleep)
            abort('you cannot set both speed and sleep at the same time.');
        // conversion from sec to ms; from px/sec to px/ms
        var fade_ms = Math.round(opts.fade * 1000);
        if (opts.sleep)
            var sleep = Math.round(opts.sleep * 1000);
        if (opts.speed)
            var speed = opts.speed / 1000,
					fade_px = Math.round(fade_ms * speed);

        // set container css
        self.empty().css({
            overflow: 'hidden',
            padding: 0
        });
        if (!/^(absolute|relative|fixed)$/.test(self.css('position')))
            self.css({ position: 'relative' });
        if (!self.width() || !self.height())
            abort('container element does not have its own width and height');

        // random sorting
        if (opts.shuffle)
            plan.sort(function() {
                return Math.random() - 0.5;
            });

        // prepare each image
        for (var i = 0; i < plan.length; ++i) {

            var p = plan[i];
            if (!p.src)
                abort('missing src parameter in picture {0}.', i + 1);

            if (speed) { // speed/dir mode

                // check parameters and translate speed/dir mode into full mode (from/to/time)
                switch (p.dir) {
                    case 'up':
                        p.from = { xrel: .5, yrel: 0, zoom: 1 };
                        p.to = { xrel: .5, yrel: 1, zoom: 1 };
                        var slide_px = p.height - self_height - 2 * fade_px;
                        break;
                    case 'down':
                        p.from = { xrel: .5, yrel: 1, zoom: 1 };
                        p.to = { xrel: .5, yrel: 0, zoom: 1 };
                        var slide_px = p.height - self_height - 2 * fade_px;
                        break;
                    case 'left':
                        p.from = { xrel: 0, yrel: .5, zoom: 1 };
                        p.to = { xrel: 1, yrel: .5, zoom: 1 };
                        var slide_px = p.width - self_width - 2 * fade_px;
                        break;
                    case 'right':
                        p.from = { xrel: 1, yrel: .5, zoom: 1 };
                        p.to = { xrel: 0, yrel: .5, zoom: 1 };
                        var slide_px = p.width - self_width - 2 * fade_px;
                        break;
                    default:
                        abort('missing or malformed "dir" parameter in picture {0}.', i + 1);
                }
                if (slide_px <= 0)
                    abort('picture number {0} is too short for the desired fade duration.', i + 1);
                p.time_ms = Math.round(slide_px / speed);

            } else if (!sleep) { // full mode

                // check and parse parameters
                if (!p.from || !p.to || !p.time)
                    abort('missing either speed/sleep option, or from/to/time params in picture {0}.', i + 1);
                try {
                    p.from = parse_position_param(p.from)
                } catch (e) {
                    abort('malformed "from" parameter in picture {0}.', i + 1);
                }
                try {
                    p.to = parse_position_param(p.to)
                } catch (e) {
                    abort('malformed "to" parameter in picture {0}.', i + 1);
                }
                if (!p.time)
                    abort('missing "time" parameter in picture {0}.', i + 1);
                p.time_ms = Math.round(p.time * 1000)
            }

            // precalculate left/top/width/height bounding values
            if (p.from)
                jQuery.each([p.from, p.to], function(i, from_to) {
                    from_to.width = Math.round(p.width * from_to.zoom);
                    from_to.height = Math.round(p.height * from_to.zoom);
                    from_to.left = Math.round((self_width - from_to.width) * from_to.xrel);
                    from_to.top = Math.round((self_height - from_to.height) * from_to.yrel);
                });

            // append the image (or anchor) element to the container
            var elm;
            if (p.href) {
                elm = jQuery(format('<a href="{0}"><img src="{1}" width="440px" height="185px"/></a>', p.href, p.src));

            }
            else
                elm = jQuery(format('<img src="{0}" width="440px" height="185px"/>', p.src));
            if (p.onclick)
                elm.click(p.onclick);
            if (p.alt)
                elm.find('img').attr('alt', p.alt);

            elm.find('img').attr('target', '_blank');
            elm.appendTo(self);
        }
        speed = undefined;  // speed mode has now been translated to full mode

        // find images to animate and set initial css attributes
        var imgs = self.find('img').css({
            position: 'absolute',
            visibility: 'hidden',
            top: 0,
            left: 0,
            border: 0
        });

        // show first image
        imgs.eq(0).css({ visibility: 'visible' });
        //$('#lb_imgtitle').text(imgs.eq(0).attr('alt').split('|')[0]);
        // $('#lb_author').text(imgs.eq(0).attr('alt').split('|')[1]);
        changeimginfo(imgs.eq(0).attr('alt').split('|')[2], imgs.eq(0).attr('alt').split('|')[0], imgs.eq(0).attr('alt').split('|')[1]);

        function changeimginfo(imgID, title, author) {
            $('#lb_imgtitle').text(title);
            $('#lb_imgtitle').attr('href', 'http://anhso.net/UserRedirect.aspx?action=image&imgID=' + imgID);

            $('#lb_author').text(author);
            $('#lb_author').attr('href', 'http://' + author + '.pqt.vn');


        }
        if (!sleep) {
            imgs.eq(0).css(position_to_css(plan[0], 2));

        }

        // create animation chain
        var countdown = opts.loop;
        function create_chain(i, chainf) {
            // building the chain backwards, or inside out

            if (i % 2 == 0) {
                if (sleep) {

                    // still image sleep

                    var i_sleep = i / 2,
							i_hide = (i_sleep - 1 + plan.length) % plan.length,
							img_sleep = imgs.eq(i_sleep),
							img_hide = imgs.eq(i_hide);

                    var newf = function() {
                        img_hide.css('visibility', 'hidden');
                        setTimeout(chainf, sleep);

                    };

                } else {

                    // single image slide

                    var i_slide = i / 2,
							i_hide = (i_slide - 1 + plan.length) % plan.length,
							img_slide = imgs.eq(i_slide),
							img_hide = imgs.eq(i_hide),
							time = plan[i_slide].time_ms,
							slide_anim = position_to_css(plan[i_slide], 3);

                    var newf = function() {
                        img_hide.css('visibility', 'hidden');
                        img_slide.animate(slide_anim, time, 'linear', chainf);

                    };

                }
            } else {
                if (sleep) {

                    // still image cross-fade

                    var i_from = Math.floor(i / 2),
							i_to = Math.ceil(i / 2) % plan.length,
							img_from = imgs.eq(i_from),
							img_to = imgs.eq(i_to),
							from_anim = {},
							to_init = { visibility: 'visible' },
							to_anim = {};

                    if (i_to > i_from) {
                        to_init.opacity = 0;
                        to_anim.opacity = 1;
                    } else {
                        from_anim.opacity = 0;
                    }

                    var newf = function() {

                        img_to.css(to_init);
                        if (from_anim.opacity != undefined)
                            img_from.animate(from_anim, fade_ms, 'linear', chainf);
                        else
                            img_to.animate(to_anim, fade_ms, 'linear', chainf);
                        //  $('#lb_imgtitle').text(img_to.attr('alt').split('|')[0]);
                        //$('#lb_author').text(img_to.attr('alt').split('|')[1]);
                        changeimginfo(img_to.attr('alt').split('|')[2], img_to.attr('alt').split('|')[0], img_to.attr('alt').split('|')[1]);
                    };

                } else {

                    // cross-slide + cross-fade

                    var i_from = Math.floor(i / 2),
							i_to = Math.ceil(i / 2) % plan.length,
							img_from = imgs.eq(i_from),
							img_to = imgs.eq(i_to),
							from_anim = position_to_css(plan[i_from], 4),
							to_init = position_to_css(plan[i_to], 1),
							to_anim = position_to_css(plan[i_to], 2);

                    if (i_to > i_from) {
                        to_init.opacity = 0;
                        to_anim.opacity = 1;
                    } else {
                        from_anim.opacity = 0;
                    }
                    to_init.visibility = 'visible';

                    var newf = function() {

                        img_from.animate(from_anim, fade_ms, 'linear');
                        img_to.css(to_init);
                        img_to.animate(to_anim, fade_ms, 'linear', chainf);
                    };

                }
            }

            // if the loop option was requested, push a countdown check
            if (opts.loop && i == plan.length * 2 - 2) {
                var newf_orig = newf;
                newf = function() {
                    if (--countdown) newf_orig();
                }

            }

            if (i > 0)
                return create_chain(i - 1, newf);
            else
                return newf;
        }
        var animation = create_chain(plan.length * 2 - 1, function() {

            return animation();
        });

        // start animation
        animation();

    });

    return self;
};
