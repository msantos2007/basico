(function () {
    'use strict';

    angular.module('basicospa', ['common.core', 'common.ui'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider','$locationProvider'];
    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/home/index.html"
                , controller: "indexCtrl"
                , controllerAs: "ctrlr"
            }).when("/login", {
                templateUrl: "scripts/spa/account/login.html"
                , controller: "loginCtrl"
            }).when("/account", {
                templateUrl: "scripts/spa/account/editAccount.html"
                , controller: "editAccountCtrl"
                , resolve: { isAuthenticated: isAuthenticated }
            }).when("/chat", {
                templateUrl: "scripts/spa/chat/chatMain.html"
                , controller: "chatMainCtrl"
                , controllerAs: "ctrlr"
            }).when("/register", {
                templateUrl: "scripts/spa/account/register.html"
                , controller: "registerCtrl"
            }).when("/logintoken", {
                templateUrl: "scripts/spa/account/loginToken.html"
                , controller: "loginTokenCtrl"
            }).otherwise({ redirectTo: "/" });

            $locationProvider.html5Mode(true);
    }
    
    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];
    function run($rootScope, $location, $cookieStore, $http) {
        // handle page refreshes
        $rootScope.repository = $cookieStore.get('repository') || {};

        if ($rootScope.repository.loggedUser)
            if (typeof (Storage) !== "undefined") {
                if (localStorage.length !== 0) {
                    //Recuperar do localstorage
                }
            }

        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
        }


        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];
    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/login');    
        }
    }
})();


//Antes de tudo: https://www.formget.com/angularjs-remove-hashtag/
//Depois, talvez use...
//Para prevenir page refresh por conta do HTML5 '#/'. Coloque esta merda no Web.config at <system.webServer>
//<rewrite>
//  <rules>
//    <clear />
//    <rule name="API" stopProcessing="true">
//      <match url="^(api)(.*)$" />
//      <action type="None" />
//    </rule>
//    <rule name="Main Rule" stopProcessing="true">
//      <match url=".*" />
//      <conditions logicalGrouping="MatchAll">
//        <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
//        <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
//      </conditions>
//      <action type="Rewrite" url="/basico/" />
//    </rule>
//  </rules>
//</rewrite>

//<rewrite>
  //<rules>
  //  <clear />
  //  <rule name="API" stopProcessing="true">
  //    <match url="^(api)(.*)$" />
  //    <action type="None" />
  //  </rule>
  //  <rule name="Main Rule" stopProcessing="true">
  //    <match url=".*" />
  //    <conditions logicalGrouping="MatchAll">
  //      <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
  //      <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
  //      <add input="{REQUEST_URI}" matchType="Pattern" pattern="signalr/(.*)" negate="true" />
  //    </conditions>
  //    <action type="Rewrite" url="/basico/" />
  //  </rule>
  //</rules>
//</rewrite>

//https://github.com/Bikeman868/UrlRewrite.Net
//Adicionar <modules></modules> em web.config
// Ou: 
//    <modules>
//      <remove name="FormsAuthentication" />
//    </modules>
//Install-Package UrlRewrite.Net -Version 1.2.7
//Create RewriteRules.config
//copy <rewrite> block into it.]
//Change: <match url="^(api)(.*)$" /> to <match url="api/(.*)$" ignoreCase="true"  />