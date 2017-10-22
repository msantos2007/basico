(function (app) {
    'use strict';

    app.directive('customPager', customPager);
    function customPager() {
        return {
            scope: {
                page: '@',
                pagesCount: '@',
                totalCount: '@',
                searchFunc: '&',
                customPath: '@'
            },
            replace: true,
            restrict: 'E',
            templateUrl: 'scripts/spa/_layout/pager.html',
            controller: ['$scope', function ($scope) {
                $scope.search = function (i) {
                    if ($scope.searchFunc) {
                        $scope.searchFunc({ page: i });
                    }
                };

                $scope.range = function () {
                    if (!$scope.pagesCount) { return []; }
                    var step = 2;
                    var doubleStep = step * 2;
                    var start = Math.max(0, $scope.page - step);
                    var end = start + 1 + doubleStep;
                    if (end > $scope.pagesCount) { end = $scope.pagesCount; }

                    var ret = [];
                    for (var i = start; i != end; ++i) {
                        ret.push(i);
                    }

                    return ret;
                };

                $scope.pagePlus = function (count) {
                    console.log($scope.page);
                    return +$scope.page + count;
                }
            }]
        }
    }


    app.directive('customContent', customContent);
    function customContent()
    {
        return {
            restrict: 'E',
            templateUrl: function (elem, attrs) { return attrs.url }
            //, controllerAs: 'ctrlr'
            //, bindToController: true // because the scope is isolated
        }
    }

    app.component('customComponent', { 
        templateUrl: function ($element, $attrs)
        {
            return $attrs.url;
        }
        , controllerAs: 'ctrlr' 
    });

})(angular.module('basicospa'));
