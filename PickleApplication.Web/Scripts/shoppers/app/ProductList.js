var ProductList = {
    Prm: {
        Obj: {
            ProductListContainer: null,
            CategoryById: null
        },
        Txt: {},
        Message: {}
    },
    scope: null,
    controller: Common.angularApp.controller('ProductListController', ["$scope", "$sce", function ($scope, $sce) {
        $scope._ = _;
        $scope.Products = [];
        $scope.Categories = [];
        $scope.isCategories = false;
        $scope.isProducts = false;
        $scope.TrustDangerousSnippet = function (snippet) {
            return $sce.trustAsHtml(snippet);
        };
        //post
        $scope.getCategories = function () {
            var request = {};
            Common.callAjax("api/productservice/GetCategories", JSON.stringify(request), $scope.getCategoriesSuccess, $scope.getCategoriesError, $scope.getCategoriesCompleted);
        };
        $scope.getProducts = function () {
            var request = {};
            Common.callAjax("api/productservice/GetProducts", JSON.stringify(request), $scope.getProductsSuccess, $scope.getProductsError, $scope.getProductsCompleted);
        };

        //success
        $scope.getCategoriesSuccess = function (response) {
            console.log(response);
            var scope = ProductList.Prm.scope;
            if (response.success) {
                var categories = _.map(response.data, function (category) {
                    if (category.linkId === _.toNumber(ProductList.Prm.Obj.CategoryById))
                        category.isSelected = true;
                    else
                        category.isSelected = false;
                    return category;
                });
                scope.Categories = categories;
            }
            scope.getProducts();
        };
        $scope.getProductsSuccess = function (response) {
            console.log(response);
            var scope = ProductList.Prm.scope;
            if (response.success)
                scope.filterProductsByCategory(response.data);
        };

        //error
        $scope.getCategoriesError = function (response) {
            console.log(response);
        };
        $scope.getProductsError = function (response) {
            console.log(response);
        };

        //completed
        $scope.getCategoriesCompleted = function () {
            var scope = ProductList.Prm.scope;
            scope.isCategories = true;
            scope.$digest();
        };
        $scope.getProductsCompleted = function () {
            var scope = ProductList.Prm.scope;
            scope.isProducts = true;
            scope.$digest();
        };


        //methods
        $scope.filterProducts = function () {
            var scope = ProductList.Prm.scope;
            scope.filterProductsByCategory(scope.Products);
            setTimeout(function () {
                $("img.lazy").lazyload();
            }, 50);
        };
        $scope.filterProductsByCategory = function (data) {
            var scope = ProductList.Prm.scope;
            var products = _.map(data, function (product) {
                //if not selected category. Get all products
                if (_.find(scope.Categories, { isSelected: true }) === undefined) {
                    product.isActiveCategory = true;
                    return product;
                }
                var category = _.find(scope.Categories, { linkId: product.link.linkId });
                if (category.isSelected)
                    product.isActiveCategory = true;
                else
                    product.isActiveCategory = false;
                return product;
            });
            scope.Products = products;
        };
    }]),
    init: function () {
        ProductList.Prm.Obj.ProductListContainer = $('[data-ui-id="ProductList"]');
    },
    getScope: function () {
        var scope = angular.element("[data-ui-id='ProductList']").scope();
        if (scope !== null) {
            ProductList.Prm.scope = scope;
        }
    },
    load: function () {
        ProductList.getScope();
        var scope = ProductList.Prm.scope;
        scope.getCategories();
    }
};