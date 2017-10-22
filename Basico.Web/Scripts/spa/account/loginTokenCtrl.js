(function (app) {
    'use strict';

    app.controller('loginTokenCtrl', loginTokenCtrl);

    loginTokenCtrl.$inject = ['$scope', '$location', 'membershipService', '$rootScope', '$routeParams', '$window', 'apiService', '$timeout'];

    function loginTokenCtrl($scope, $location, membershipService, $rootScope, $routeParams, $window, apiService, $timeout) {
        $scope.pageClass = 'page-loginToken';
        $rootScope.pageClassAtual = $scope.pageClass;

        $scope.loading_page = false;
        $scope.loading_erro = false;
        $scope.loginToken = loginToken;

        //$scope.counter = 5;
        //$scope.onTimeout = onTimeout; 
        //var mytimeout = $timeout($scope.onTimeout, 1000);

        function loginToken() {

            return;
            //http://marcelo.linkpc.net:8188/basico/logintoken?token=token
            //http://localhost:56372/basico/api/account/authenticateWithToken/token
            apiService.get('api/account/authenticateWithToken/' + 'token', null, loginTokenRoute, loginTokenRoute);

            function loginTokenRoute(result)
            {

            } 

        }

        //function loginTokenRoute(result)
        //{
        //    $scope.loading_page = false;
        //    $scope.loading_erro = false;
        //    var user = {};
        //    user.username = result.data.elevador_usr;
        //    user.password = result.data.elevador_pwd;

        //    membershipService.saveCredentials(user);
        //    $window.location.reload();

        //    // http://localhost:56372/basico/logintoken?token=token
        //}

        //function loginTokenRouteFailed()
        //{
        //    //$scope.loading_erro = true;
        //    //$scope.loading_page = false;
        //    onTimeout();
        //}

        //function onTimeout() {
        //    $scope.counter--;
        //    if ($scope.counter > 0) {
        //        mytimeout = $timeout($scope.onTimeout, 1000);
        //    }
        //    else
        //    {
        //        $timeout.cancel(mytimeout);
        //        //loginToken();
        //    }
                
        //}


        $scope.url = document.URL;
    }
})(angular.module('basicospa'));