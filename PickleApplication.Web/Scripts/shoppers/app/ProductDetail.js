var ProductDetail = {
    Prm: {
        Obj: {
            ProductDetailContainer: null,
            BasePrice: null,
            OrderLinesCount: null
        },
        Text: {},
        Message: {},
        Product: null,
        Picture: null,
        OrderLines: "OrderLines",
        scope: null
    },
    controller: Common.angularApp.controller('ProductDetailContainer', ["$scope", "$sce", function ($scope, $sce) {
        $scope._ = _;
        $scope.OrderLines = [];
        $scope.BasePrice = 0;
        $scope.ProductQuantity = 1;
        $scope.btnAddProduct = function () {
            if (ProductDetail.Prm.Product !== null) {
                var scope = ProductDetail.Prm.scope;
                scope.orderLinesAdded(ProductDetail.Prm.Product);
                Sf.Core.CookieManager.set(ProductDetail.Prm.OrderLines, encodeURIComponent(JSON.stringify(scope.OrderLines)), 1);
                $(ProductDetail.Prm.Obj.OrderLinesCount).html(scope.OrderLines.length);
                Common.bootstrapNotify(Enumerations.BootstrapNotifyType.Success, "Ürün sepete eklendi.");
            }
        };
        $scope.orderLinesAdded = function (product) {
            var scope = ProductDetail.Prm.scope;
            var orderLineOfProduct = _.find(scope.OrderLines, function (orderLine) { return orderLine.product.productId === product.productId; }) !== undefined;
            if (orderLineOfProduct) {
                _.each(scope.OrderLines, function (orderLine) {
                    if (orderLine.product.productId === product.productId)
                        orderLine.quantity = scope.ProductQuantity;
                });
            } else {
                scope.OrderLines.push({
                    product: product,
                    quantity: scope.ProductQuantity,
                    picture: ProductDetail.Prm.Picture
                });
            }
        };
        $scope.productQuantityAddingOne = function () {
            var scope = ProductDetail.Prm.scope;
            scope.ProductQuantity += 1;
            scope.priceCalculation();
        };
        $scope.productQuantityExtractionOne = function () {
            var scope = ProductDetail.Prm.scope;
            if (scope.ProductQuantity === 1)
                return;
            scope.ProductQuantity -= 1;
            scope.priceCalculation();
        };
        $scope.priceCalculation = function () {
            var scope = ProductDetail.Prm.scope;
            var basePriceObj = $(ProductDetail.Prm.Obj.ProductDetailContainer).find(ProductDetail.Prm.Obj.BasePrice);
            var basePrice = basePriceObj.attr('data-ui-price');

            $(basePriceObj).html(parseFloat(basePrice) * _.toNumber(scope.ProductQuantity));
        };
    }]),
    init: function () {
        ProductDetail.Prm.Obj.BasePrice = $('[data-ui-id="BasePrice"]');
        ProductDetail.Prm.Obj.ProductDetailContainer = $('[data-ui-id="ProductDetail"]');
        ProductDetail.Prm.Obj.OrderLinesCount = $('[data-ui-id="OrderLinesCount"]');
    },
    getScope: function () {
        var scope = angular.element(ProductDetail.Prm.Obj.ProductDetailContainer).scope();
        if (scope !== null) {
            ProductDetail.Prm.scope = scope;
        }
    },
    setOrderLines: function () {
        var scope = ProductDetail.Prm.scope;
        if (Sf.Core.CookieManager.get(ProductDetail.Prm.OrderLines) !== null)
            scope.OrderLines = JSON.parse(decodeURIComponent(Sf.Core.CookieManager.get(ProductDetail.Prm.OrderLines)));
    },
    loadingOwlCarousel: function() {
        $('.owl-carousel').owlCarousel({
            center: false,
            items: 1,
            loop: false,
            stagePadding: 15,
            margin: 20,
            nav: false
            //nav: true
            //navText: ['<span class="icon-arrow_back">', '<span class="icon-arrow_forward">'],
            //responsive: {
            //    600: {
            //        margin: 20,
            //        items: 2
            //    },
            //    1000: {
            //        margin: 20,
            //        items: 3
            //    },
            //    1200: {
            //        margin: 20,
            //        items: 3
            //    }
            //}
        });
    },
    load: function () {
        ProductDetail.getScope();
        ProductDetail.setOrderLines();
        ProductDetail.loadingOwlCarousel();
    }
};