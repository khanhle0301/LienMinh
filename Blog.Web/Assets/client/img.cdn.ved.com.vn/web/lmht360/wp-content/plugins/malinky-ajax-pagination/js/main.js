var MalinkyAjaxPaging = function(n) {
		
    function t(n, t, a) {
        var i;
        return function() {
            var e = this,
                o = arguments,
                l = function() {
                    i = null, a || n.apply(e, o)
                },
                r = a && !i;
            clearTimeout(i), i = setTimeout(l, t), r && n.apply(e, o)
        }
    }
    for (var a in malinkySettings)
        if (n(malinkySettings[a].posts_wrapper).length && n(malinkySettings[a].post_wrapper).length && n(malinkySettings[a].pagination_wrapper).length && n(malinkySettings[a].next_page_selector).length) {
            var i = malinkySettings[a].ajax_loader,
                e = (malinkySettings[a].malinky_load_more, malinkySettings[a].malinky_load_more_button, parseInt(malinkySettings[a].infinite_scroll_buffer)),
                o = "",
                l = malinkySettings[a].loading_more_posts_text,
                r = malinkySettings[a].load_more_button_text,
                s = malinkySettings[a].pagination_wrapper,
                m = n(document).height() - n(s).offset().top,
                p = malinkySettings[a].paging_type,
                g = malinkySettings[a].posts_wrapper,
                u = malinkySettings[a].post_wrapper,
                c = parseInt(malinkySettings.max_num_pages),
                y = parseInt(malinkySettings.next_page_number),
                f = (malinkySettings[a].next_page_selector, n(malinkySettings[a].next_page_selector).attr("href") || malinkySettings.next_page_url),
								cc = 1;
							var m = y + 3;
            break
        }
    var d = function() {
            "infinite-scroll" == p ? (_(), n(s).hide(), window.addEventListener("scroll", S)) : "load-more" == p ? (n(s).last().after('<div class="malinky-load-more"><a href="' + f + '" id="malinky-ajax-pagination-button" class="malinky-load-more__button">' + r + "</a></div>"), _(), n(s + ":not(:has(>a#malinky-ajax-pagination-button))").remove(), n("#malinky-ajax-pagination-button").click(function(n) {
							n.preventDefault(), o = setTimeout(h, 750), k();
            })) : "pagination" == p && (_(), n(document).on("click", s, function(n) {
                n.preventDefault(), o = setTimeout(h, 750), f = n.target.href, k()
            }), window.addEventListener("popstate", function(n) {
                f = document.URL, k()
            }))
        },
        k = function() {
            n.ajax({
                type: "GET",
                url: f,
                dataType: "html",
                success: function(t) {
                    var a = n.parseHTML(t),
                        i = n(a).find(u),
                        e = n(g + " " + u).last(),
                        pp = n(a).find(s),
                        o = n(u);
												if (e.after(i), n(s).html(pp.html()), ("infinite-scroll" == p || "load-more" == p) && (y++, (y > m) && (n("#malinky-ajax-pagination-button").parent().remove(), window.removeEventListener("scroll", S)), f = f.replace(/\/page\/[0-9]*/, "/page/" + y)), "pagination" == p) {
													o.remove(), history.pushState(null, null, f);
													var l = n(a).find(s);
													n(s).replaceWith(l)
											}
											v();
											loadimage('100%');
											if(y == m + 1 || y == c + 1) {
												n(s).show();
											}	
                },
                error: function(n, t) {
                    x()
                },
                complete: function() {
                    "pagination" == p && n("body,html").animate({
                        scrollTop: n(g).offset().top - 150
                    }, 400)
                }
            })
        },
        _ = function() {
            n(s).last().before('<div class="malinky-ajax-pagination-loading">' + i + "</div>")
        },
        h = function() {
            n(".malinky-ajax-pagination-loading").show(), ("load-more" == p || "infinite-scroll" == p) && n("#malinky-ajax-pagination-button").text(l)
        },
        v = function() {
            n(".malinky-ajax-pagination-loading").hide(), ("load-more" == p || "infinite-scroll" == p) && n("#malinky-ajax-pagination-button").text(r), clearTimeout(o)
        },
        x = function() {
            n(".malinky-ajax-pagination-loading").hide(), clearTimeout(o)
        },
        S = t(function() {
					var dt = new Date()
					var t1 = dt.getTime();
					var t2 = t1 - cc;
					console.log(cc + '_' + t1 + '_' + t2);
					//if (t2 > 2000) {
						//cc = t1;
						var t = n(document).height() - n(window).scrollTop() - n(window).height();
						m > t - e && (h(), k())
					//} else {
						//alert('nhanh quá');
					//}
        }, 250);
    d()
}(jQuery);