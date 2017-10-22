(function () {
    'use strict';

    //https://github.com/asafdav/ng-scrollbar

    angular.module('common.ui', ['ui.bootstrap', 'chieffancypants.loadingBar', 'ngDroplet', 'ngAnimate', 'ui.mask', 'ngMaterial', 'ngMessages', 'ngCacheBuster'])
    .config(function (cfpLoadingBarProvider) {        
        cfpLoadingBarProvider.includeSpinner = false;
        })
    .config(function ($mdThemingProvider) {        
        $mdThemingProvider.theme('default')
            .primaryPalette('yellow')
            .accentPalette('light-green')
            .warnPalette('orange')
            .dark();
    })
    .config(['uiMask.ConfigProvider',function(uiMaskConfigProvider) {  
        uiMaskConfigProvider.clearOnBlur(false);
    }]) 
    .config(function (httpRequestInterceptorCacheBusterProvider)
    {
        httpRequestInterceptorCacheBusterProvider.setMatchlist([/.*[Ss]cripts\/spa.*/], true);
    })
    //.config(function ($mdThemingProvider) {        
    //    $mdThemingProvider.theme('docs-dark', 'default')
    //        .primaryPalette('yellow')
    //        .accentPalette('lime')
    //        .warnPalette('red')
    //        .dark();
    //})
    //.config(function (uiMaskConfigProvider){
    //    uiMaskConfigProvider.uiMaskPlaceholderChar = 'space';
    //})

;})();

