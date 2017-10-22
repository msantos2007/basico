(function (app) {
    'use strict';

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', '$uibModal', '$templateCache'];

    function registerCtrl($scope, membershipService, notificationService, $rootScope, $location, $uibModal, $templateCache) {
        
        $scope.register = register;

        //$templateCache.remove('Scripts/spa/account/registerModal.html');

        function register() {            
            $uibModal.open({
                animation: true,
                templateUrl: 'Scripts/spa/account/registerModal.html',
                controller: 'registerModalCtrl',
                scope: $scope,
                backdrop: 'static',
                keyboard: false
            }).result.then(function ($scope) {
            }, function () {
            });            
        }

        //register();
    }
})(angular.module('basicospa'));
