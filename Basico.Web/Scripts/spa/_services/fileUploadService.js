(function (app) {
    'use strict';

    app.factory('fileUploadService', fileUploadService);

    fileUploadService.$inject = ['$rootScope', '$http', '$timeout', '$upload', 'notificationService'];

    function fileUploadService($rootScope, $http, $timeout, $upload, notificationService) {

        $rootScope.upload = [];        

        var service = {
            uploadImage: uploadImage,
            uploadImageNGDroplet: uploadImageNGDroplet,
            uploadImageNGDropletLogo: uploadImageNGDropletLogo
        }

        function uploadImage($files, mensagemId, callback) {
            //$files: an array of files selected                 
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $rootScope.upload[index] = $upload.upload({
                        url: "./api/mensagem/images/upload?mensagemId=" + mensagemId, // webapi url
                        method: "POST",
                        file: $file
                    }).progress(function (evt) {
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully                        
                        notificationService.displaySuccess(data.FileName + ' uploaded com sucesso!');
                        callback();
                    }).error(function (data, status, headers, config) {
                        notificationService.displayError(data.Message);
                    });
                })(i);
            }
        }

        function uploadImageNGDroplet(files, mensagemId, callback) {
            // For $files: file | For NGDroplet: files.file
            for (var i = 0; i < files.length; i++) {
                var file = files[i].file;
                (function (index) {
                    $rootScope.upload[index] = $upload.upload({
                        url: "./api/mensagem/images/upload?mensagemId=" + mensagemId + "&fileId=" + i + "&usr_criacao=" + $rootScope.repository.loggedUser.username,
                        method: "POST",
                        file: file
                    }).progress(function (evt) {
                    }).success(function (data, status, headers, config) {
                        notificationService.displaySuccess(data.FileName + ' uploaded com sucesso!');
                        callback();
                    }).error(function (data, status, headers, config) {
                        notificationService.displayError(data.Message);
                    });
                })(i);
            }
        }

        function uploadImageNGDropletLogo(files, itemID, tipo, callback) {
            // For $files: file | For NGDroplet: files.file
            for (var i = 0; i < files.length; i++) {
                var file = files[i].file;
                (function (index) {
                    $rootScope.upload[index] = $upload.upload({
                        url: "./api/cond/images/upload?itemID=" + itemID + "&fileId=" + i + "&tipo=" + tipo + "&usr_criacao=" + $rootScope.repository.loggedUser.username,
                        method: "POST",
                        file: file
                    }).progress(function (evt) {
                    }).success(function (data, status, headers, config) {
                        notificationService.displaySuccess(data.FileName + ' uploaded com sucesso!');
                        callback();
                    }).error(function (data, status, headers, config) {
                        notificationService.displayError(data.Message);
                    });
                })(i);
            }
        }

        return service;
    }

})(angular.module('common.core'));