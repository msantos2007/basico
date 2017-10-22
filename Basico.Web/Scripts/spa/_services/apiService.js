(function (app) {
    'use strict';

    app.factory('apiService', apiService);

    apiService.$inject = ['$http', '$location', 'notificationService', '$rootScope'];

    function apiService($http, $location, notificationService, $rootScope) {
        var service = {
            get: get,
            post: post
        };

        function get(url, config, success, failure, $scope) {
            return $http.get(url, config)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {

                            $rootScope.previousState = $location.path();
                            if ($rootScope.repository.loggedUser == null) {
                                $location.path('/login');
                            }
                            else {
                                notificationService.displayError('Privilégios insuficientes');
                                $location.path('/');
                            }

                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        function post(url, data, success, failure) {
            return $http.post(url, data)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {

                            $rootScope.previousState = $location.path();

                            if ($rootScope.repository.loggedUser == null) {                                
                                $location.path('/login');
                            }
                            else {
                                notificationService.displayError('Privilégios insuficientes');
                                $location.path('/');
                            }


                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        return service;
    }

})(angular.module('common.core'));