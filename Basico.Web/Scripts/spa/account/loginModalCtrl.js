(function (app) {
    'use strict';

    app.controller('loginModalCtrl', loginModalCtrl);

    loginModalCtrl.$inject = ['$scope', '$uibModalInstance', 'membershipService', 'notificationService', '$rootScope', '$location'];
    function loginModalCtrl($scope, $uibModalInstance, membershipService, notificationService, $rootScope, $location) {
        $scope.pageClass = 'page-login';
        $scope.login = login;
        $scope.openRegister = openRegister;
        $scope.user = {};
        $scope.isEnabled = true;
        $scope.sair = sair;

        function sair() {
            //$scope.isEnabled = false;
            $location.path('/');
            $uibModalInstance.dismiss();
        }

        function openRegister() {
            $scope.isEnabled = false;
            $location.path('/register');
            $uibModalInstance.close();
        }

        function login() {
            $scope.isEnabled = false;                
            var usuario_logado = '';
            if (typeof myVar != 'undefined')
                var usuario_logado = $rootScope.repository.loggedUser.username;

            if (usuario_logado == '' || usuario_logado == null)
                $scope.user.usuario_logado = null;
            else
                $scope.user.usuario_logado = usuario_logado;

            membershipService.login($scope.user, loginCompleted);           
        }

        function loginCompleted(result) {
            if (result.data.success) {

                //Destruir persistencia
                if (typeof (Storage) !== "undefined") localStorage.clear();

                //SignalR
                $rootScope.ChatHub = null;
                $rootScope.ChatMessages = [];

                $scope.user.firstname = result.data.user.Firstname;

                membershipService.saveCredentials($scope.user);                
                notificationService.displaySuccess('Bem vindo de volta!');               
                $scope.userData.displayUserInfo();                

                if ($rootScope.previousState)
                    $location.path($rootScope.previousState);
                else
                    $location.path('/');

                $uibModalInstance.dismiss();
            }
            else {
                notificationService.displayError('Login falhou. Tente novamente.');
                $scope.isEnabled = true;
            }
        }
    }

})(angular.module('common.core'));