(function (app) {
    'use strict';

    app.controller('registerModalCtrl', registerModalCtrl);

    registerModalCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location', '$uibModalInstance'];

    function registerModalCtrl($scope, membershipService, notificationService, $rootScope, $location, $uibModalInstance) {
        $scope.pageClass = 'page-login';
        $scope.user = {};
        $scope.isEnabled = true;
        $scope.register = register;

        $scope.sair = sair;
        $scope.verificar = verificar;
        $scope.existingUsername = false;
        $scope.existingEmail = false;

        //setTimeout(function () { }, 2000);

        function verificar(tipo, valor)
        {
            if (valor)            
                membershipService.verificarExiste(tipo, valor, verificarCompleted);
        }

        function verificarCompleted(result)
        {
            if (result.data.tipo == 'username')
                $scope.existingUsername = result.data.success;
                
            if (result.data.tipo == 'email')
                $scope.existingEmail = result.data.success;
        }

        function sair() {                     
            $location.path('/');
            //$location.path($rootScope.previousState);
            //$modalInstance.dismiss();
            $uibModalInstance.dismiss('cancel');
        }

        function register() {
            $scope.isEnabled = false;            
            
            if (typeof ($rootScope.repository.loggedUser) !== 'undefined')
                var usuario_logado = $rootScope.repository.loggedUser.username;

            if (usuario_logado == '' || usuario_logado == null)
                $scope.user.usuario_logado = null;
            else
                $scope.user.usuario_logado = usuario_logado;

            membershipService.register($scope.user, registerCompleted, registerCompleted);

        }

        function registerCompleted(result) {
            if (result.data.success) {

                //Destruir persistencia
                if (typeof (Storage) !== "undefined") localStorage.clear();

                //SignalR
                $rootScope.ChatHub = null;
                $rootScope.ChatMessages = [];

                membershipService.saveCredentials($scope.user);
                notificationService.displaySuccess('Seja bem vindo!');
                $scope.userData.displayUserInfo();
                $location.path('/');

                $uibModalInstance.dismiss();
            }
            else {
                notificationService.displayError('Registro falhou. Tente novamente.');
                $scope.isEnabled = true;
            }
        }
    }

})(angular.module('common.core'));