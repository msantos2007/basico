(function (app) {
    'use strict';

    app.controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', '$location', 'membershipService', '$rootScope', '$uibModal', '$templateCache'];

    function loginCtrl($scope, $location, membershipService, $rootScope, $uibModal, $templateCache) {

        $scope.login = login;


        //LIMPAR $templaecahe
        //$templateCache.remove('Scripts/spa/account/loginModal.html');

        function login() {
            $uibModal.open({
                animation: true,
                templateUrl: 'Scripts/spa/account/loginModal.html',
                controller: 'loginModalCtrl',
                scope: $scope,
                backdrop: 'static',
                keyboard: false
            }).result.then(function ($scope) {
            }, function () {
            });           
        }

        //login();
    }
})(angular.module('basicospa'));
