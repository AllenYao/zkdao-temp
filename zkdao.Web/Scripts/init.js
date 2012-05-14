seajs.config({
    charset: 'utf-8',
    timeout: 5000,
    debug: false
});

define(function (require, exports, module) {
    
    exports._ = require("./zic-js/underscore");
    if (exports._.isUndefined(glo_path)) glo_path = "";
    exports.mc = require('./zic-js/mustache');
    exports.JSON = require("./zic-js/json");
    exports.jsonHelp = require("./zic-js/plus/jsonhelp");

    exports.jq = require('jquery');
    var $ = exports.jq;
    $.ajaxSetup({
        type: "POST",
        dataType: "json", //返回值类型
        contentType: "application/x-www-form-urlencoded; charset=utf-8", //请求参数类型
        timeout: 5000
    });
    require('plus-jq/jq_ui_19')($);
    require('./zic-js/plus-jq/jq_ui_zh')($);
    $.fx.speeds._default = 1000;

    //require('./plus-jq/jq_initbow')($);
    require('./zic-js/plus-jq/jq_mttext')($);
    require('./zic-js/plus-jq/jq_overlabel')($);
    require('./zic-js/plus-jq/jq_countable')($);
    require('./zic-js/plus-jq/jq_focusaft')($);
    require('./zic-js/plus-jq/jq_cookie')($);
    require('./zic-js/plus-jq/jq_validation')($, exports._);
    require('./zic-js/plus-jq/jq_holdinput')($);
    require('./zic-js/plus-jq/jq_serializehelp')($, exports.JSON);
    require('./zic-js/plus-jq/jq_timers')($);
    require('./zic-js/plus-jq/jq_selection')($);
    require('./zic-js/plus-jq/jq_paginate')($);
    require('./zic-js/plus-jq/ms/jq_validate')($);
    require('./zic-js/plus-jq/ms/jq_validate_unobtrusive')($);
    require('./zic-js/plus-jq/ms/jq_ajax_unobtrusive')($);

    require('./zic-js/plus-bs/bootstrap-dropdown')($);
    require('./zic-js/plus-bs/bootstrap-button')($);

    exports.aspx = require('./zic-js/plus/aspx');

    //Function
    var g_notifi = {};
    exports.shownotifi = function (txt, type, key) {
        var o = $('<div class="notifi"><span class="txt"></span><span class="close" title="关闭"></span></div>');
        if (key) {
            if (g_notifi[key]) return;
            g_notifi[key] = o;
        }
        o.addClass(type);
        $('span.txt', o).text(txt);
        $('span.close', o).click(function () { o.fadeOut("slow"); });
        $("#notifi-box").html(o);
    }
    exports.clearnotifi = function (key) {
        if (key) {
            if (!g_notifi[key]) return;
            g_notifi[key].remove();
            g_notifi = exports._.without(g_notifi, key);
        }
        else
            $("#notifi-box").empty();
    }
    exports.checkreturn = function (m) {
        if (m.result == false) {
            exports.shownotifi('操作失败！' + m.msg, 'error');
            return false;
        }
        if (m.attention == true) {
            exports.shownotifi('警告！' + m.msg, 'attention');
            return true;
        }
        exports.clearnotifi();
        return true;
    }

    //Loader
    $(function () {
        var body = $("body")
        exports.ld_logo();
        exports.ld_navact();
        //exports.ld_dialog(body);
        //exports.fix_ajaxload(body);
        exports.fix_vail(body);
    });
    exports.ld_logo = function () {
        var logos = $("a.brand-light, a.brand-dark");
        logos.hover(function () {
            logos.addClass("hover", 1000);
        }, function () {
            logos.removeClass("hover", 1000);
        });
    }
    exports.ld_navact = function () {
        var lis = $("ul.nav>li");
        if (!lis.length) return;
        var stuffToDo = {
            seat_use: function () {
                lis.eq(0).addClass('active');
            },
            seat: function () {
                lis.eq(1).addClass('active');
            },
            role: function () {
                lis.eq(2).addClass('active');
            },
            user: function () {
                lis.eq(2).addClass('active');
            },
            prop: function () {
                lis.eq(3).addClass('active');
            }
        };
        var url3 = exports.aspx.url_segment(3);
        if (url3 && stuffToDo[url3]) stuffToDo[url3]();
        var url2 = exports.aspx.url_segment(2);
        if (url2 && stuffToDo[url2]) {
            var url1 = exports.aspx.url_segment(1);
            if (url2 == "seat" && url1 == "use") {
                stuffToDo["seat_use"]();
                return;
            }
            stuffToDo[url2]();
        }
    }
    exports.ld_dialog = function () {
        $("div.ui_dialog").dialog({
            resizable: false,
            modal: true,
            autoOpen: false,
            width: 425,
            buttons: {
                "确定": function () {
                    $('a.donet', this).click();
                },
                "取消": function () {
                    $('a.undonet', this).click();
                    $(this).dialog("close");
                }
            }
        });
    }
    exports.ld_alert = function () {
        var alert = function (txt) {
            $("#txtAlert").text(txt);
            $('#AlertDialog').dialog('open');
        }
        $('#AlertDialog').dialog({
            resizable: false,
            autoOpen: false,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });

        var confirmUrl;
        var confirm = function (e, o, txt) {
            e.preventDefault();
            confirmUrl = $(o).attr("href");
            $("#txtConfirm").text(txt);
            $('#ConfirmDialog').dialog('open');
        };
        $("#ConfirmDialog").dialog({
            modal: true,
            resizable: false,
            autoOpen: false,
            buttons: {
                "确定": function () {
                    window.location.href = confirmUrl;
                },
                "取消": function () {
                    $(this).dialog("close");
                    return false;
                }
            }
        });
    }
    exports.ld_dynamicwin = function () {
        window.win = $(window);

        var calculateLayout = function () {
            window.winHeight = window.win.height();
            window.winWidth = window.win.width();
        };
        calculateLayout();
        var lazyLayout = exports._.debounce(calculateLayout, 300);
        window.win.resize(lazyLayout);

        var o = $("#fixpos");
        var fixpos = function () {
            o.css("top", window.win.scrollTop());
        }
        window.win.scroll(fixpos);
    }

    //Fix
    exports.fix_submit = function (box) {
        $('a.submit', box).click(function () {
            $(this).closest('form').submit();
        });
        $('input.submit', box).bind("keydown", function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code != 13) return;
            $(this).closest('form').submit();
        });
    }
    exports.fix_pager = function (box) {
        $(".pager", box).each(function () {
            var o = $(this);
            if (o.attr('count') == 0) return;
            o.paginate({
                count: Math.ceil(parseInt(o.attr('count')) / parseInt(o.attr('size'))),
                start: parseInt(o.attr('index')),
                display: 12,
                border: false,
                text_color: '#79B5E3',
                background_color: 'none',
                text_hover_color: '#2573AF',
                background_hover_color: 'none',
                images: false,
                mouse: 'press',
                onChange: function (num) {
                    window.location.href = o.attr('url') + num;
                }
            });
        });
    }
    exports.fix_change_url = function (box) {
        $("select.change_url").bind("change", function () {
            var type = $(this).getSelectedValue();
            window.location.href = $(this).attr('url') + type;
        });
    }
    exports.fix_ajaxload = function (box) {
        $('img#ajaxload', box).ajaxStart(function () {
            $(this).show();
        });
        $('img#ajaxload', box).ajaxStop(function () {
            $(this).hide();
        });
    }
    exports.fix_vail = function (box) {
        $("form:first", box).validationEngine();
    }
    exports.fix_accesskey = function (box) {
        $("input[accesskey]", box).bind("keydown", function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                var btnid = $(this).attr('accesskey');
                $(".access_" + btnid).click();
            }
        });
    }
    exports.fix_mttext = function (box) {
        $("input.text, textarea.text", box).zictext();
        $("input.selall", box).click(function () { $(this).select() });
    }
    exports.fix_overlabel = function (box) {
        $('.overlabel', box).find('label').overlabel();
    }
    exports.fix_holdinput = function (box) {
        $('input.holdinput', box).holdinput();
    }
    exports.fix_txttime = function (box) {
        //exports.async_jqui(function () {
        $("input.txttime.def5k", box)
            .datepicker({ dateFormat: 'yy-mm-dd', changeMonth: true, changeYear: true, defaultDate: -9000, onSelect: function (dateText, inst) { $(this).blur(); } })
            .data("init", true);
        $("input.txttime", box).each(function () {
            if ($(this).data("init")) return true;
            $(this).datepicker({ dateFormat: 'yy-mm-dd', changeMonth: true, changeYear: true, onSelect: function (dateText, inst) { $(this).blur(); } });
        });
        //});
    }
    exports.fix_pressbar = function (box) {
        $(".pressbar", box).each(function () {
            var o = $(this);
            var val = parseInt(o.attr("title"));
            if (o.attr("multi")) {
                val = val * o.attr("multi");
            }
            o.progressbar({ value: parseInt(val) });
        });
    }
    exports.fix_uitips = function (box) {
        $(".uitips", box).tooltip({ position: { my: "center bottom", at: "center top", offset: "0 -5"} });
    }
    exports.fix_uibtn = function (box) {
        $("button.ui_btn, input.ui_btn, a.ui_btn", box).button();
        $("button.ui_btn_newwin", box).button({ icons: { primary: 'ui-icon-newwin'} });
        $("button.ui_btn_plusthick", box).button({ icons: { primary: 'ui-icon-plusthick'} });
        $("button.ui_btn_comment", box).button({ icons: { primary: 'ui-icon-comment'} });
        $("button.ui_btn_plus", box).button({ icons: { primary: 'ui-icon-plus'} });
        $("button.ui_btn_cart", box).button({ icons: { primary: 'ui-icon-cart'} });
        $("button.ui_btn_circle", box).button({ icons: { primary: 'ui-icon-circle-plus'} });
        $("button.ui_btn_check", box).button({ icons: { primary: 'ui-icon-check'} });
    }
    exports.fix_hover = function (box) {
        $(".hov", box).hover(
            function () { $(this).addClass('hover'); },
            function () { $(this).removeClass('hover'); }
        );
    }
    exports.fix_datalazyload = function (box) {
        KISSY.use("datalazyload", function (S, DataLazyload) {
            var dl = DataLazyload(box[0]);
        });
    }
    exports.fix_png = function (box) {
        if (window.bow == "ie6") {
            module.load('./jqplus/jq_pngFix', function () {
                $(".png", box).pngFix();
            });
        }
    }
    exports.fix_countable = function (box) {
        $(".countable", box).each(function () {
            $(this).countable({
                threshold: 0,
                target: $(this).parent().find(".cttarget"),
                appendMethod: "appendTo",
                positiveCopy: "还可输入{n}个字符"
            });
        });
    }

});