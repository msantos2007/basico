(function (app) {
    app.controller('editAccountCtrl', editAccountCtrl);

    editAccountCtrl.$inject = ['$scope', 'apiService', '$routeParams', 'notificationService', 'membershipService', '$location', '$rootScope'];

    function editAccountCtrl($scope, apiService, $routeParams, notificationService, membershipService, $location, $rootScope) {
        $scope.pageClass = 'page-users';
        $scope.user = {};
        $scope.loadingUser = true;
        $scope.UpdateUser = UpdateUser;
        $scope.closeform = closeform;
        $scope.userRoles = [];
        $scope.isShown = isShown;
        
        function isShown(v1, v2, v3) {            
            if (v1 == '' || v1 == null)
            {
                return false;
            }
            else
            {
                if (v2 == '' || v2 == null)
                {
                    return false;
                }
                else
                {
                    if (v1 == v2) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }

        function loadUserRoles() {
            apiService.get('api/Account/userRoles/' + $scope.username, null,
            userRolesLoadCompleted,
            userRolesLoadFailed);
        }

        function userRolesLoadCompleted(response) {
            $scope.userRoles = response.data;
        }

        function userRolesLoadFailed(response) {
            notificationService.displayError(response.data);
        }
        
        function closeform() {
            $location.path('/');
        }

        function loadUser() {
            $scope.loadingUser = true;
            apiService.get('api/Account/details/' + $scope.username, null, userLoadCompleted, userLoadFailed);            
        }

        function userLoadCompleted(result) {
            $scope.user = result.data;
            //loadUserRoles();
            $scope.loadingUser = false;
        }

        function userLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function UpdateUser() {
            if ((($scope.user.password      == '' || $scope.user.password      == null)  ||
                 ($scope.user.newpassword_n == '' || $scope.user.newpassword_n == null)  ||
                 ($scope.user.newpassword_c == '' || $scope.user.newpassword_c == null)) ||
                 (!isShown($scope.user.newpassword_n, $scope.user.newpassword_c, $scope.user.password)))
            {
                
                $scope.user.newpassword_n = '';
                $scope.user.newpassword_c = '';
                $scope.user.password      = '';
                UpdateUserModel();
            }
            else {                
                loginEdit();
            }
        }

        function loginEdit() {            
            membershipService.login($scope.user, loginEditCompleted);
        }

        function loginEditCompleted(result) {
            if (result.data.success) {
                UpdateUserModel();               
            }
            else {
                notificationService.displayError('Senha Atual incorreta. Tente novamente.');                
            }
        }
        
        function UpdateUserModel() {
            $scope.user.usuario_logado = $rootScope.repository.loggedUser.username;
            apiService.post('api/Account/update/', $scope.user, updateUserSucceed, updateUserFailed);
        }

        function updateUserSucceed(response) {
            notificationService.displaySuccess('Seu acesso foi atualizado com sucesso');
            membershipService.removeCredentials();                       
            $location.path('/');            
            $scope.userData.displayUserInfo();
            $location.path('/login');
        }

        function updateUserFailed(response) {
            notificationService.displayError(response);
        }

        loadUser();       
    }
})(angular.module('basicospa'));
