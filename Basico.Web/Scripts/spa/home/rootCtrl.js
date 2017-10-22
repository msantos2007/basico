(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$location', 'membershipService', '$rootScope', 'favicoService', '$interval', 'apiService', '$route', '$timeout', 'notificationService', '$templateCache'];

    function rootCtrl($scope, $location, membershipService, $rootScope, favicoService, $interval, apiService, $route, $timeout, notificationService, $templateCache)
    {
        $scope.loading = true;
        $scope.logout = logout;
        $scope.userData = {};        
        $scope.userData.displayUserInfo = displayUserInfo;


        //SignalR
        $rootScope.ChatHub = null;
        $rootScope.ChatMessages = [];
        $rootScope.hubConnect = hubConnect;


        $rootScope.json = "";
        
        function mostarToast(texto)
        {
            //texto = texto || "Toast!";
            $timeout(function () { $rootScope.json = texto; }); 
            notificationService.displaySuccess(texto);
        }

        function broadcastMessage(ChatUserame, ChatMessage) {
            var newMessage = '';
            newMessage = ChatUserame + ' says: ' + ChatMessage;
            $rootScope.ChatMessages.push(newMessage);
            $rootScope.$apply();
        }

        function hubConnect() {
            //if (!$scope.loading && typeof ($.connection.BasicoHub) !== 'undefined') {
            if ( typeof ($.connection.BasicoHub) !== 'undefined') {
                $rootScope.ChatHub = $.connection.BasicoHub;
                $rootScope.ChatHub.client.broadcastMessage = broadcastMessage;
                $rootScope.ChatHub.client.mostarToast = mostarToast;
                //$scope.ChatHub.client = {};
                //$scope.ChatHub.server = {};                
                $.connection.hub.start();
                //  $scope.chatHub.client.broadcastMessage;
                //apiService.get('api/BasicoHub/', null, hubcompleted, null);
                function hubcompleted(response) {
                    // console.log(response);
                }
            }
        }


        function displayUserInfo() {

            $scope.userData.isUserLoggedIn = membershipService.isUserLoggedIn();

            //SignalR
            hubConnect();

            if ($scope.userData.isUserLoggedIn) {                
                $scope.username = $rootScope.repository.loggedUser.username;
                $scope.firstname = $rootScope.repository.loggedUser.firstname;

                ////SignalR
                //hubConnect();

            }
            else
            {
                if (typeof (Storage) !== "undefined") localStorage.clear();
            }

            //?? Na barra de menu estava aparecendo {{}}                
            document.getElementById("navbarEscondido0").removeAttribute("hidden");              
            $scope.loading = false;
        }

        function logout() {

            //Destruir persistencia
            if (typeof (Storage) !== "undefined") localStorage.clear();

            ////SignalR
            //$rootScope.ChatHub = null;
            //$rootScope.ChatMessages = [];
            //$.connection.hub.stop();


            membershipService.removeCredentials();
            $rootScope.previousState = "/";
            $location.path('/');
            $route.reload();
            $scope.userData.displayUserInfo();
        }

        $scope.userData.displayUserInfo();
         
        function limparCache()
        {
            apiService.get('api/account/htmlHash', null, htmlHash_s, htmlHash_f);

            function htmlHash_s(result)
            {
                debugger;
            }

            function htmlHash_f(response)
            {

            }
        }

        limparCache();


    }

})(angular.module('basicospa'));




//initial value
//$scope.count = 0;        
//$scope.plusOne = function () {
//    $scope.count = $scope.count + 1;
//    favicoService.badge($scope.count);
//};
//$scope.minusOne = function () {
//    $scope.count = ($scope.count - 1 < 0) ? 0 : ($scope.count - 1);
//    favicoService.badge($scope.count);
//};
//$scope.reset = function () {
//    favicoService.reset();
//};
//favicoService.badge($scope.count);

//new: marcelo
//    $scope.sobe = 1;
//    $interval(function () {

//        if ($scope.count >= 5)
//            $scope.sobe = 2;

//        if ($scope.count <= 0)
//            $scope.sobe = 1;

//        if ($scope.sobe == 1)
//            $scope.count = $scope.count + 1;
//        else
//            $scope.count = $scope.count - 1;

//        favicoService.badge($scope.count);

//    }, 2000);
