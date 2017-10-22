(function (app) {
    'use strict';

    app.factory('membershipService', membershipService);

    membershipService.$inject = ['apiService', 'notificationService','$http', '$base64', '$cookieStore', '$rootScope'];

    function membershipService(apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {

        var service = {
            login: login,
            register: register,
            saveCredentials: saveCredentials,
            removeCredentials: removeCredentials,
            isUserLoggedIn: isUserLoggedIn,
            verificarExiste: verificarExiste
        }

        function login(user, completed) {
            apiService.post('api/account/authenticate', user,
            completed,
            loginFailed);
        }

        function register(user, completed, failed) {
            apiService.post('api/account/register', user,
            completed,
            failed);
        }

        function saveCredentials(user) {            
            var membershipData = $base64.encode(user.username + ':' + user.password);
            $rootScope.repository = {
                loggedUser: {                   
                    username: user.username,
                    firstname: user.firstname,
                    authdata: membershipData
                }
            };

            $http.defaults.headers.common['Authorization'] = 'Basic ' + membershipData;
            $cookieStore.put('repository', $rootScope.repository);
         
        }

        function removeCredentials() {

            $rootScope.repository = {};
            $cookieStore.remove('repository');
            $http.defaults.headers.common.Authorization = '';
        };

        function loginFailed(response) {
            notificationService.displayError(response.data);
        }

        //?? Não mais utilizado porque não dava chance de uma nova tentativa: Assim
        //?? Function "register" recebeu nova promise (failed), ver "registerModalCtrl".
        function registrationFailed(response) {

            notificationService.displayError('Registro falhou. Tente novamente.');
        }

        function isUserLoggedIn() {
            return $rootScope.repository.loggedUser != null;
        }

        
        function verificarExiste(tipo, valor, completed)
        {
            var config = {
                params: {
                    tipo: tipo,
                    valor: valor
                }
            };
            apiService.get('api/account/existe', config, completed, null);
        }

        function registrationFailed(response) {

            notificationService.displayError('Verificação falhou. Tente novamente.');
        }

        return service;
    }

})(angular.module('common.core'));