'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers', [])

    // Path: /
    .controller('HomeCtrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.title = 'BurgerShop';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
        toastr.success('Home page loaded!');
    }])

    .controller('storeController', ['$scope', '$routeParams', 'DataService', function ($scope, $routeParams, DataService) {

    // get store and cart from service
    $scope.productDetails = DataService.productDetails;
    $scope.store = DataService.store;
    $scope.cart = DataService.cart;

    if ($routeParams.productCode != null) {
        $scope.product = $scope.store.getProduct($routeParams.productCode);
        $scope.detail = $scope.productDetails.getDetail($routeParams.productCode);
    }
    }])

    .controller('AdminController_Music', ['$scope', '$filter', function ($scope, $filter) {

        var myStore = new store();
        $scope.currentPage = 0;
        $scope.pageSize = 9;
        $scope.numberOfPages = Math.ceil(myStore.products.length / $scope.pageSize);

        $scope.filteredItems = [];
        $scope.groupedItems = [];
        $scope.pagedItems = [];

        var searchMatch = function (haystack, needle) {
            if (!needle) {
                return true;
            }
            return haystack.toLowerCase().indexOf(needle.toLowerCase()) !== -1;
        };
        $scope.search = function (name) {
            $scope.filteredItems = $filter('filter')(myStore.products, function (product) {
                for (var attr in product) {
                    if (searchMatch(product[name], $scope.query))
                        return true;
                }
                return false;
            });
            $scope.currentPage = 0;
            $scope.groupToPages();
        };
        $scope.myFilter = function (column, category) {
            $scope.filteredItems = $filter('filter')(myStore.products, function (product) {
                for (var attr in product) {
                    if (searchMatch(product[column], category))
                        return true;
                }
                return false;
            });
            $scope.currentPage = 0;
            $scope.groupToPages();
        };
        $scope.groupToPages = function () {
            $scope.pagedItems = [];

            for (var i = 0; i < $scope.filteredItems.length; i++) {
                if (i % $scope.pageSize === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.pageSize)] = [$scope.filteredItems[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.pageSize)].push($scope.filteredItems[i]);
                }
            }
        };
        // functions have been describe process the data for display
        $scope.myFilter();
        $scope.search();


function store() {
    this.products = [
          { num: 1, code: '001s', category: 'Speakers', name: 'Sound G.', src: "product/burger.jpg", src_retro: "product/1r.jpg", description: 'Lorem ipsum dolor sit amet, consectetur adipisici elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. ', price: 200, discount: '20%', class: 'show-down' },
          { num: 2, code: '002s', category: 'Watches', name: 'Rhon Doe', src: "product/burger.jpg", src_retro: "product/2r.jpg", description: 'Lorem ipsum dolor sit amet, consectetur adipisici elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. ', price: 110, class: 'show-down' },
          { num: 3, code: '003s', category: 'Speakers', name: 'Patrol SR', src: "product/burger.jpg", src_retro: "product/3r.jpg", description: 'Lorem ipsum dolor sit amet, consectetur adipisici elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. ', price: 68, discount: '10%', class: 'show-up' },
          { num: 4, code: '004s', category: 'Station', name: 'Redo Bag', src: "product/burger.jpg", src_retro: "product/4r.jpg", description: 'Lorem ipsum dolor sit amet, consectetur adipisici elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. ', price: 134, class: 'show-down' },
          { num: 5, code: '005s', category: 'Phone', name: 'Mikore', src: "product/burger.jpg", src_retro: "product/5r.jpg", description: 'Lorem ipsum dolor sit amet, consectetur adipisici elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. ', price: 350, discount: '50%', class: 'show-up' },
          { num: 6, code: '006s', category: 'Station', name: 'Big Hoddie', src: "product/burger.jpg", src_retro: "product/6r.jpg", description: 'Lorem ipsum dolor sit amet, consectetur adipisici elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. ', price: 127, class: 'show-down' },
          { num: 7, code: '007s', category: 'Watches', name: 'Roberto J.', src: "product/burger.jpg", src_retro: "product/7r.jpg", description: 'Lorem ipsum dolor sit amet, consectetur adipisici elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. ', price: 500, discount: '30%', class: 'show-up' },
          { num: 8, code: '008s', category: 'Phone', name: 'Rigo S.', src: "product/burger.jpg", src_retro: "product/8r.jpg", description: 'Lorem ipsum dolor sit amet, consectetur adipisici elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. ', price: 346, class: 'show-down' },
          { num: 9, code: '009s', category: 'Speakers', name: 'Eliteme', src: "product/burger.jpg", src_retro: "product/9r.jpg", description: 'Lorem ipsum dolor sit amet, consectetur adipisici elit, sed eiusmod tempor incidunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquid ex ea commodi consequat. ', price: 234, discount: '30%', class: 'show-down' }];

}

function productDetails() {
    this.details = [
          { id: '001s', material: 'Pelt', power: '10-100W', size: '(LxAxP) 194x296x200 mm', weight: '5,5 kg' },
          { id: '002s', material: 'Wood', power: '30-140W', size: '(LxAxP) 194x296x200 mm', weight: '5,5 kg' },
          { id: '003s', material: 'Pelt', power: '10-150W', size: '(LxAxP) 194x296x200 mm', weight: '5,5 kg' },
          { id: '004s', material: 'Metal', power: '40-200W', size: '(LxAxP) 194x296x200 mm', weight: '5,5 kg' },
          { id: '005s', material: 'Wood', power: '10-100W', size: '(LxAxP) 194x296x200 mm', weight: '5,5 kg' },
          { id: '006s', material: 'Wood', power: '10-150W', size: '(LxAxP) 194x296x200 mm', weight: '5,5 kg' },
          { id: '007s', material: 'Metal', power: '10-180W', size: '(LxAxP) 194x296x200 mm', weight: '5,5 kg' },
          { id: '008s', material: 'Metal', power: '10-100W', size: '(LxAxP) 194x296x200 mm', weight: '5,5 kg' },
          { id: '009s', material: 'Pelt', power: '10-100W', size: '(LxAxP) 194x296x200 mm', weight: '5,5 kg' }];
}



store.prototype.getSize = function (code) {
    var myDetails = new productDetails();

    for (var i = 0; i < myDetails.details.length; i++) {
        if (myDetails.details[i].id == code) {
            return myDetails.details[i].size;
        }
    }

    return null;
}

/*** Get Products ***/
store.prototype.getProduct_sound = function (code) {
    for (var i = 0; i < this.products.length; i++) {
        if (this.products[i].code == code)
            return this.products[i];
    }

    return null;
}

/*** Get Product Details ***/
productDetails.prototype.getDetail_sound = function(code) {
        for (var i = 0; i < this.details.length; i++) {
            if (this.details[i].id == code)
                return this.details[i];

        }
        return null;
    }
    }])
    // Path: /about
    .controller('AboutCtrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.title = 'BurgerShop | About';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

    // Path: /login
    .controller('LoginCtrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.title = 'BurgerShop | Sign In';
        // TODO: Authorize a user
        $scope.login = function () {
            $location.path('/');
            return false;
        };
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])

    // Path: /error/404
    .controller('Error404Ctrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.title = 'Error 404: Page Not Found';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);