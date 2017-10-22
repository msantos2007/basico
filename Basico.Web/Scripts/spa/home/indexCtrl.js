(function (app) {
    'use strict';
    //http://www.dispatchertimer.com/signalr/signalr-tutorial-a-simple-chat-application-using-signalr-and-angularjs/
    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', '$timeout', '$location'];
    function indexCtrl($scope, $rootScope, apiService, notificationService, $timeout, $location)
    {
        $scope.pageClass = 'page-home';
        $scope.contador = 1;
        $scope.send = null;
        $scope.dt = null;
        $scope.ChatMessage = null;
        $scope.ChatUserame = 'Guest';
        $scope.ChatNewMessage = ChatNewMessage;
        $scope.explode = explode;

        $scope.decrypt = decrypt;
        $scope.decriptado = "";

        $scope.PegaData = PegaData; 
        $scope.dataSQL = null;
        $scope.pegaounao = true;

        $scope.dataNETCrua = null;
        $scope.dataNETNow = null;
        $scope.dataNETNowUTC = null;

        $scope.iniVigencia = null;
        $scope.fimVigencia = null;

        $scope.notif = notif;
        $scope.notif_out = notif_out;
        $scope.zipfy = zipfy;


        var ctrlr = this;
        $scope.meuconteudo = "meu conteudo 2";
        ctrlr.meucont = "eu cc";

        ctrlr.chat = chat;


        function chat()
        {
            $location.path('/chat');
        }
        

        function downloadDataUrlFromJavascript(filename, dataUrl)
        {  
            var link = document.createElement("a");
            link.download = filename;
            link.target = "_blank";
             
            link.href = dataUrl;
            document.body.appendChild(link);
            link.click();
            
            document.body.removeChild(link);
            //delete link;
        }
        function zipfy()
        {
            var baixando = true;
            var datasending = { ID: 'Teste', mensage: 'Mensagem' };

            apiService.post('api/account/zipfy', datasending, zipfy_s, zipfy_f);

            function zipfy_s(result)
            {    
                notificationService.displaySuccess("Download em andamento...");

                $timeout(function ()
                {
                    var url = window.location.href;
                    downloadDataUrlFromJavascript(result.data, url + "_inbox" + result.data);
                    var baixando = false;
                }, 2000);
            }

            function zipfy_f(response)
            {
                notificationService.displayFailed("Download falhou.");
                var baixando = false;
            }
        }

        function notif_out()
        { 
            var http = new XMLHttpRequest();
            var datasending = { ID: 'Teste', mensage: 'Mensagem' };
            var url = "http://www.billete.somee.com/basico/api/account/pagseguronotificacoes";
            url = url + "/";
            http.open("POST", url, true);
            //Send the proper header information along with the request
            http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            //http.setRequestHeader("Origin", document.URL);
            http.onreadystatechange = function ()
            {
                if (http.readyState == 4 && http.status == 200)
                {
                    notificationService.displaySuccess("Enviado");
                }
                //else
                //{
                //    notificationService.displayError("Erro");
                //}
            }
            http.send(datasending);
        }

        function notif()
        {
            var datasending = { ID: 'Teste', mensage: 'Mensagem' };

            apiService.post('api/account/pagseguronotificacoes', datasending, notif_s, notif_f);

            function notif_s(result)
            {
                notificationService.displaySuccess("Enviado e gerado: " + result.data);
            }

            function notif_f(response)
            {
                notificationService.displayError(response.data);
            }
        }
 

        function PegaData()
        { 
            if ($scope.pegaounao)
            {
                $scope.pegaounao = false;
                return;
            }

            //var givenDate = "20170115"; //2017-07-20T04:46:11.051Z
            var givenDate = "2017-07-20T02:46:11.051Z"; //2017-07-20T04:46:11.051Z

            var strUTCnow = moment().toISOString(); //Data-Hora UTC
            var strLclnow = moment().format(); //Data-Hora Local


            var strUTC = moment(givenDate).toISOString(); //Data-Hora UTC
            var strLcl = moment(givenDate).format(); //Data-Hora Local


            var strVig = moment(givenDate).format("YYYY-MM-DDT00:00:00.000"); //Data Local Instatanea

            var dataini = new Date(moment(strVig));

            $scope.iniVigencia = dataini.toISOString();

            var datafim = new Date(moment(dataini).add(-1, 'days'));

            $scope.fimVigencia = datafim.toISOString();




            $scope.dataSQL = moment().toISOString(); //FOI

            var config = { params: { data: $scope.dataSQL } };

            apiService.get('api/account/pegadataSQL', config, PegaData_Success, PegaData_Success);
             
            function PegaData_Success(response)
            {
                $scope.pegaounao = true;  
                $scope.dataNETCrua = response.data.SQL.DateCreated; //VOLTOU


                var strSQLutcISO = response.data.datanowUTC;
                var DateJS_from_strSQLutcISO = new Date(strSQLutcISO);
                var ISOString_fromDateJS = DateJS_from_strSQLutcISO.toISOString();


                //Vigencia
            }
        }

        PegaData();

        function decrypt()
        {

            $scope.decriptado = "";

            apiService.post('api/account/decrypt', null, ssss, ssss);


            function ssss(response)
            {
                //console.log(response.data.decriptado);
                $scope.decriptado = response.data.decriptado;
            }


        }

        function explode(texto)
        {
            $rootScope.ChatHub.server.sendMostar(texto);
        }

        function ChatNewMessage() {
            $rootScope.ChatHub.server.sendMessage($scope.ChatUserame, $scope.ChatMessage + ' ');
            $scope.ChatMessage = null;
        }
        //http://marcelo.linkpc.net:8188/basico/api/account/followzupapi?user=USERID&subs=SUBSCODE
        //http://marcelo.linkpc.net:8188/basico/api/account/followzupapi?
        //http://localhost:56372/basico/api/account/followzupapi
        //http://marcelo.linkpc.net:8188/basico/api/account/followzupapi

        $scope.triggerFolowzup = triggerFolowzup;
        function triggerFolowzup(tru)
        {

            $scope.fzupidchannel = "";
            $scope.fzupresponse = "";
            $scope.dt = null;
            $scope.send = null;

            if (tru)
            {
                var datasending = { fzupidchannel: 'chanel', fzupresponse: 'response' };


                apiService.post('api/account/followzupapi', datasending, dddd, dddd);

                function dddd(result)
                {
                    console.log(result); 
                    $scope.dt = new Date(Date.now());
                    $scope.send = "http://marcelo.linkpc.net:8188/basico/api/account/followzupapi";
                    $scope.sendConfig = datasending;
                } 
            }
            else
            {
                var config = { params: { tipo: 'tipo' } };


                apiService.get('api/account/followzup', config, loginTokenRoute, loginTokenRoute);

                function loginTokenRoute(result)
                {
                    console.log(result);
                    $scope.send = "http://marcelo.linkpc.net:8188/basico/api/account/followzup";
                    $scope.sendConfig = config;
                    $scope.resposta = result.data.resposta;
                } 
            }

        }

        $scope.triggerFolowzupPHP = triggerFolowzupPHP;
        function triggerFolowzupPHP(paramGET)
        {
            paramGET = paramGET || false;

            $rootScope.json = "";
 
                $scope.fzupidchannel = "";
                $scope.fzupresponse = "";
                $scope.dt = null;
                $scope.send = null;
                $scope.texto_enviar = "texto de teste nr" + $scope.contador;
            
                //explode("Clicou?");

            var http = new XMLHttpRequest();
            //var url = "http://marcelo.linkpc.net:8188/basico/api/account/followzupapi";
            //url = "http://marcelo123.url-site.com/api/account/followzupapi";
            //url = "http://marcelo.dynu.net/basico/api/account/followzupapi";
            //var url = "http://www.billete.somee.com/basico/api/account/followzupapi";
            //var url = "http://localhost:56372/basico/api/account/followzupapi";
            //var url = "http://marcelo.linkpc.net:8188/billet/api/followzup/redirectfrom";
            var url = "http://www.billete.somee.com/basico/api/account/redirectfrom";
            var params = "fzupidchannel=canal&fzupresponse=" + $scope.texto_enviar;
            //params = [ "param1_content", "param2_content" ];

            if (!paramGET)
            {
                url = url + "/";
                http.open("POST", url, true);
                //Send the proper header information along with the request
                http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                //http.setRequestHeader("Origin", document.URL);
                http.onreadystatechange = function ()
                {
                    if (http.readyState == 4 && http.status == 200) {
                        //alert(http.responseText, url, params);
                        //alert(http.responseText, url, params);

                        $timeout(function ()
                        {
                            $scope.dt = new Date(Date.now());
                            $scope.send = url;
                            $scope.sendConfig = params;
                            $scope.resposta = http.responseText;

                            //explode($scope.resposta);

                            $scope.contador++;
                        });

                    }
                }
                http.send(params);
            }
            else
            {
                url = url + "get/?" + params;

                http.open("GET", url, true);
                //Send the proper header information along with the request
                http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

                http.onload = function ()
                {
                    if (http.readyState == 4 && http.status == 200) {
           
                        $timeout(function ()
                        {
                            $scope.dt = new Date(Date.now());
                            $scope.send = url;
                            $scope.sendConfig = params;
                            $scope.resposta = http.responseText;
                            $scope.contador++;
                        });

                    }
                }
                http.send();
            }

        }

        return ctrlr;
    }

})(angular.module('basicospa'));
