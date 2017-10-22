(function (app) {
    'use strict';

    app.directive('sideBar', sideBar);

    function sideBar() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: 'scripts/spa/_layout/sideBar.html'
        }
    }

})(angular.module('common.ui'));