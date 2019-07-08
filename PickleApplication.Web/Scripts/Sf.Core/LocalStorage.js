// Each object must be extended with the Sf.Core 
$.extend(Sf.Core,
    {
        LocalStorage: {
            storage: function () { return localStorage; },

            isSupported: function () {
                return (typeof (Storage) !== "undefined");
            },

            get: function (name) {
                if (this.isSupported())
                    return this.storage().getItem(name);
                else
                    console.log("Sorry! No Web Storage support..");
            },
            set: function (name, value) {
                if (this.isSupported())
                    this.storage().setItem(name, value);
                else
                    console.log("Sorry! No Web Storage support..");
            },
            remove: function (name) {
                this.storage().removeItem(name);
            },
            clear: function () {
                this.storage().clear();
            }
        }
    });