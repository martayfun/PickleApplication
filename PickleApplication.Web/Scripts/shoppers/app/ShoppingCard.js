var ShoppingCard = {
    Prm: {
        Obj: {
            ShoppingCardController: null
        },
        Text: {},
        Message: {},
        OrderLines: "OrderLines",
        scope: null
    },
    controller: Common.angularApp.controller('ShoppingCardController', ["$scope", "$sce", function ($scope, $sce) {
        $scope._ = _;
        $scope.isShoppingCardShow = false;
        $scope.isShoppingCardAlertShow = false;
        $scope.ShoppingCardTotalPrice = 0;
        $scope.OrderLines = [];

        $scope.setShoppingCard = function () {
            var scope = ShoppingCard.Prm.scope;
            var orderLines = JSON.parse(decodeURIComponent(Sf.Core.CookieManager.get(ShoppingCard.Prm.OrderLines)));
            if (orderLines !== null) {
                scope.OrderLines = orderLines;
                scope.isShoppingCardShow = true;
            } else {
                scope.isShoppingCardAlertShow = true;
            }
            scope.calculateOrderLinesTotalPrice();
            scope.$digest();
        };
        $scope.calculateOrderLinesTotalPrice = function () {
            var scope = ShoppingCard.Prm.scope;
            _.each(scope.OrderLines, function (orderLine) {
                scope.ShoppingCardTotalPrice += orderLine.product.unitPrice * orderLine.quantity;
            });
        };
        $scope.calculateOrderLineTotalPrice = function (orderLine) {
            return parseFloat(orderLine.product.unitPrice * orderLine.quantity);
        };
        $scope.recalculate = function (order, status) {
            var scope = ShoppingCard.Prm.scope;
            _.each(scope.OrderLines, function (sOrder) {
                if (sOrder.product.productId === order.product.productId) {
                    if (status) {
                        sOrder.quantity += 1;
                    } else {
                        if (sOrder.quantity === 1) return;
                        sOrder.quantity -= 1;
                    }
                }
            });
            scope.ShoppingCardTotalPrice = 0;
            scope.calculateOrderLinesTotalPrice();
            scope.$digest();
        };
        $scope.updateShoppingCard = function () {
            var scope = ShoppingCard.Prm.scope;
            Sf.Core.CookieManager.remove(ShoppingCard.Prm.OrderLines);
            Sf.Core.CookieManager.set(ShoppingCard.Prm.OrderLines, encodeURIComponent(JSON.stringify(scope.OrderLines)));
            Common.bootstrapNotify(Enumerations.BootstrapNotifyType.Success, "Sepet güncellendi.");
        };
        $scope.removeCardItem = function (productId) {
            var scope = ShoppingCard.Prm.scope;
            var orderLines = _.filter(scope.OrderLines, function (orderLine) { return orderLine.product.productId !== productId; });
            scope.OrderLines = orderLines;
            Sf.Core.CookieManager.remove(ShoppingCard.Prm.OrderLines);
            if (orderLines.length > 0)
                Sf.Core.CookieManager.set(ShoppingCard.Prm.OrderLines, encodeURIComponent(JSON.stringify(orderLines)));
            Common.bootstrapNotify(Enumerations.BootstrapNotifyType.Success, "Ürün sepetten çıkarıldı.");
        };
    }]),
    init: function () {
        ShoppingCard.Prm.Obj.ShoppingCardController = $('[data-ui-id="ShoppingCard"]');
    },
    getScope: function () {
        var scope = angular.element(ShoppingCard.Prm.Obj.ShoppingCardController).scope();
        if (scope !== null) {
            ShoppingCard.Prm.scope = scope;
        }
    },
    setShoppingCard: function () {
        var scope = ShoppingCard.Prm.scope;
        scope.setShoppingCard();
    },
    load: function () {
        ShoppingCard.getScope();
        ShoppingCard.setShoppingCard();
    }
};