var Common = {
    ApiUrl: null,
    Layout: {
        init: function () {
            Common.Layout.CookieLoadOrderLines.init();
            Common.Layout.PageLoading.init();
        },
        CookieLoadOrderLines: {
            init: function () {
                Common.Layout.CookieLoadOrderLines.set();
            },
            set: function () {
                //Sepetteki ürün miktarını set ediyor.
                var orderLines = Sf.Core.CookieManager.get("OrderLines");
                var orderLinesCount = $('[data-ui-id="OrderLinesCount"]');
                if (orderLines !== undefined && orderLines !== null) {
                    orderLines = decodeURIComponent(orderLines);
                    orderLines = JSON.parse(orderLines);
                    orderLinesCount.html(orderLines.length);
                } else {
                    orderLinesCount.html(0);
                }
            }
        },
        PageLoading: {
            init: function () {
                Common.Layout.PageLoading.set();
            },
            set: function () {
                document.getElementById("loading").style.display = "none";
            }
        }
    },
    angularApp: angular.module('PickleApplication', []),
    callAjax: function (absoluteUri, query, successFnc, errorFnc, completeFnc, async) {
        $.ajax({
            async: async,
            type: "POST",
            url: Common.ApiUrl + absoluteUri,
            data: query,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: eval(successFnc),
            error: eval(errorFnc),
            complete: eval(completeFnc)
        });
    },
    bootstrapNotify: function (type, message) {
        $.notify({
            message: message
        },
            {
                type: type,
                placement: {
                    from: "bottom",
                    align: "right"
                }
            });
    },
    toLazy: function () {
        setTimeout(function () { $.each($('.lazy'), function (i, o) { $(o).lazyload('update'); }); }, 10);
    }
};
var Enumerations = {
    BootstrapNotifyType: {
        Success: "success",
        Danger: "danger",
        Warning: "warning",
        Info: "info"
    }
};