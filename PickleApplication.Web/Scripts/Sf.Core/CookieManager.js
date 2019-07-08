// Each object must be extended with the Sf.Core 
$.extend(Sf.Core, {
    CookieManager: {
        set: function (name, value, days) {
            var expires = "";
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toGMTString();
            }
            document.cookie = name + "=" + value + expires + "; path=/";
        },


        get: function (name) {
            var k;
            return (k = new RegExp('(?:^|;\\s*)' + ('' + name).replace(/[-[\]{}()*+?.,\\^$|#\s]/g, '\\$&') + '=([^;]*)')
                .exec(document.cookie)) && k[1];
        },

        remove: function (name) {
            Sf.Core.CookieManager.set(name, "", -1);
        }
    }
});