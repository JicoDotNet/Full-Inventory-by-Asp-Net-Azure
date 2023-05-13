"use strict";
var LangUrl = window.location.origin + '/Content/json/';

$(function () {
    LoadLang();
    $('select[data-lang]').change(function () {
        localStorage.setItem('lang', $('select[data-lang]').val());
        ChangeLang($('select[data-lang]').val());
    });
});
function LoadLang() {
    var defaultLang = 'en';
    if (localStorage.getItem('lang') === undefined || localStorage.getItem('lang') === '') {
        $('select[data-lang]').val(defaultLang);
        $('select[data-lang]').attr('data-lang', defaultLang);
        localStorage.setItem('lang', defaultLang);
    }
    else {
        if (0 === $('select[data-lang]').children('option[value="' + localStorage.getItem('lang') + '"]').length) {
            localStorage.setItem('lang', defaultLang);
        }
        $('select[data-lang]').val(localStorage.getItem('lang'));
        $('select[data-lang]').attr('data-lang', localStorage.getItem('lang'));
    }
    if ($('select[data-lang]').length > 0) {
        ChangeLang($('select[data-lang]').val());
    }
}
function ChangeLang(lang) {
    if (lang === undefined || lang === '') {
        LoadLang();
        lang = localStorage.getItem('lang');
    }
    $('html').attr('lang', lang);
    ajaxLang(lang);
}
function ajaxLang(lang) {
    var arrLang = {};
    var Url = LangUrl + lang + '.json?v=5';
    $.ajax({
        type: "GET",
        url: Url,
        dataType: "text",
        success: function (data) {
            arrLang = JSON.parse(data);
            bindLang(arrLang);
        },
        failure: function (response) {
            console.log(response.responseText);
        },
        error: function (response, jqXHR) {
            console.log(response.responseText);
        }
    });
}
function bindLang(arrLang) {
    var sectors = $('[data-lang-sector]');
    for (var i = 0; i < sectors.length; i++) {
        var sector = $(sectors[i]).attr('data-lang-sector');
        var keies = $(sectors[i]).find('[data-lang-key]');
        for (var j = 0; j < keies.length; j++) {
            var key = $(keies[j]).attr('data-lang-key');
            $(keies[j]).text(arrLang[sector][key]);
        }
    }
}