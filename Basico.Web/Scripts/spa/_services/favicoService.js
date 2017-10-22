(function (app) {
    'use strict';

    //Dont' forget! Add Bundles: module and Service!
    app.factory('favicoService', favicoService);

    function favicoService() {

        var service = {
            badge: badge,
            reset: reset
        };

        var favico = new Favico({
            animation: 'fade'
        });

        function badge(num) {
            favico.badge(num);
        }

        function reset() {
            favico.reset();
        }

        return service;
    }

})(angular.module('common.core'));