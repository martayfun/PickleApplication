// Each object must be extended with the Sf.Core 
$.extend(Sf.Core, {
    QueryString: {

        /* 
         * https://samaxes.com/2011/09/change-url-parameters-with-jquery/
         */
        updateOrInsertParameter: function (queryString, parameterKey, parameterValue) {
            /*
             * queryParameters -> handles the query string parameters
             * queryString -> the query string without the fist '?' character
             * re -> the regular expression
             * m -> holds the string matching the regular expression
             */
            var queryParameters = {}, re = /([^&=]+)=([^&]*)/g, m;

            // Substringing "?" chracter
            queryString = queryString.substring(1);

            // Creates a map with the query string parameters
            while (m = re.exec(queryString)) {
                queryParameters[decodeURIComponent(m[1])] = decodeURIComponent(m[2]);
            }

            // Add new parameters or update existing ones
            queryParameters[parameterKey] = parameterValue;

            /*
             * Replace the query portion of the URL.
             * jQuery.param() -> create a serialized representation of an array or
             *     object, suitable for use in a URL query string or Ajax request.
             */
            return "?" + $.param(queryParameters); // Causes page to reload
        },

        get: function (name) {

            // Dependent get & getParameters
            name = this.parameterNameToLowerCase(name);

            var result = this.getParameters()[name];
            if (result === undefined)
                result = "";
            return result;
        },

        getParameters: function () {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');

                // Dependent get & getParameters
                hash[0] = this.parameterNameToLowerCase(hash[0]);

                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        },

        parameterNameToLowerCase: function (name) {
            return name.toLowerCase();
        }
    }
});