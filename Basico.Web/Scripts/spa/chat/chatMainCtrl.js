(function (app)
{
    'use strict';
    
    //http://www.dispatchertimer.com/signalr/signalr-tutorial-a-simple-chat-application-using-signalr-and-angularjs/
    app.controller('chatMainCtrl', chatMainCtrl);

    chatMainCtrl.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', '$timeout', 'moment', '$filter', '$mdSidenav'];
    function chatMainCtrl($scope, $rootScope, apiService, notificationService, $timeout, moment, $filter, $mdSidenav)
    {
        $scope.pageClass = 'page-home';
        var ctrlr = this;

        ctrlr.scrollPct = 0;
        ctrlr.lastDate = null;
        ctrlr.contacts = [];
       
        ctrlr.mensagem_atual = {};
        ctrlr.mensagem_nova = null;
        ctrlr.incoming_mesg = false;
        ctrlr.insertMessage = insertMessage;
        ctrlr.glued = true;
        ctrlr.lastcnt = 0; 
        ctrlr.reset = reset; 
        ctrlr.toggle_sidenav = toggle_sidenav; 
        ctrlr.novoChatRoom = novoChatRoom;
        ctrlr.this_anti_ID = null;

        ctrlr.bchat_chatrooms = [];
        ctrlr.bchat_contactos = [];
        ctrlr.bchat_room_curr = {};

        function novoChatRoom(participante)
        {
            var novo_participante = {};
            var nova_sala = {};
            var participantes = [];
            nova_sala.ID = null;
            nova_sala.participantes = participantes;

            novo_participante = {};
            novo_participante.ID = null;
            novo_participante.anti_ID = ctrlr.this_anti_ID;
            novo_participante.username = $rootScope.repository.loggedUser.username;
            novo_participante.condUserRoleAtual = null;

            participantes.push(novo_participante);

            novo_participante = {};
            novo_participante.ID = null;
            novo_participante.anti_ID = participante.anti_ID;
            novo_participante.username = $rootScope.repository.loggedUser.username;
            novo_participante.condUserRoleAtual = null;

            participantes.push(novo_participante);
        } 

        function toggle_sidenav(componente)
        {
            //var contatcs = ctrlr.contacts;
            $mdSidenav(componente).toggle();
        }
        
        function reset(clicked)
        {
            clicked = clicked || false;

            if (clicked) ctrlr.glued = true;

            if (ctrlr.glued)
            {
                ctrlr.lastcnt = ctrlr.bchat_room_curr.mensagens.length;
                ctrlr.bchat_room_curr.mensagens.forEach(function (cada)
                {
                    cada.lida = true;
                });
            }            
        }
        
        function carregarChat(mensagens)
        {
            var existente = {
                autor: "me",
                personal: true,
                dt_utc: new Date(moment().add(-30, 'm')),
                texto: "Existente 1",
                class_new: false,
                incoming: false,
                lida: true
            };

            mensagens.push(existente);

            existente = {
                autor: "ele",
                personal: false,
                dt_utc: new Date(moment().add(-30, 'm')),
                texto: "Resposta 1",
                class_new: false,
                incoming: false,
                lida: true
            };

            mensagens.push(existente);

            existente = {
                autor: "me",
                personal: true,
                dt_utc: new Date(moment().add(-30, 'm')),
                texto: "Existente 2",
                class_new: false,
                incoming: false,
                lida: true
            };

            mensagens.push(existente);

            existente = {
                autor: "ele",
                personal: false,
                dt_utc: new Date(moment().add(-30, 'm')),
                texto: "Resposta 2",
                class_new: false,
                incoming: false,
                lida: true
            };

            mensagens.push(existente);

            existente = {
                autor: "me",
                personal: true,
                dt_utc: new Date(moment().add(-30, 'm')),
                texto: "Existente 3",
                class_new: false,
                incoming: false,
                lida: true
            };

            mensagens.push(existente);

            existente = {
                autor: "ele",
                personal: false,
                dt_utc: new Date(moment().add(-15, 'm')),
                texto: "Resposta 3",
                class_new: false,
                incoming: false,
                lida: true
            };

            mensagens.push(existente);
            ctrlr.lastcnt = mensagens.length;
            fakeMessage();

            ctrlr.this_anti_ID = $rootScope.this_anti_ID;
        }

        function insertMessage(enviei)
        {
            enviei = enviei || false;
            ctrlr.mensagem_nova = ctrlr.mensagem_nova || '';
             
            if (ctrlr.mensagem_nova  == '')
            {
                return false;
            }
            ctrlr.lastDate = ctrlr.lastDate || new Date();

            var nova = {};
            nova.autor = "me";
            nova.personal = enviei;
            nova.dt_utc = new Date();
            nova.texto = ctrlr.mensagem_nova;
            nova.class_new = true;
            nova.incoming = false;
            nova.lida = true;

            $timeout(function ()
            { 
                ctrlr.bchat_room_curr.mensagens.push(nova);                
                ctrlr.mensagem_nova = null;
                ctrlr.glued = true;
                ctrlr.lastcnt = ctrlr.bchat_room_curr.mensagens.length;
                $timeout(function ()
                {
                    fakeMessage();
                }, 1000 + (Math.random() * 20) * 100);
            });
        }
                  
        function fakeMessage()
        { 
            // codepen.io/supah/pen/jqOBqp
            ctrlr.mensagem_nova = ctrlr.mensagem_nova || '';

            if (ctrlr.mensagem_nova !== '')
            {
                return false;
            }



            $timeout(function ()
            {  
                var incoming = { incoming: true };
                incoming.dt_utc = new Date();
                ctrlr.bchat_room_curr.mensagens.push(incoming);
                ctrlr.lastcnt++;

                $timeout(function ()
                {
                    ctrlr.lastcnt--;
                    var nova = {};
                    nova.autor = "ele";
                    nova.personal = false;
                    nova.dt_utc = new Date();
                    nova.texto = Fake[i];
                    nova.class_new = true;
                    nova.incoming = false;
                    nova.lida = ctrlr.glued;

                    var ordinarias = $filter('filter')(ctrlr.bchat_room_curr.mensagens, function (cada) { return !cada.incoming; }); //ctrlr.incoming_mesg = false; //$('.message.loading').remove();
                    ctrlr.bchat_room_curr.mensagens = ordinarias;
                    ctrlr.bchat_room_curr.mensagens.push(nova);
                    if (ctrlr.glued) ctrlr.lastcnt = ctrlr.bchat_room_curr.mensagens.length;
                    i++; 
                }, 1000 + (Math.random() * 20) * 100);


            });

        }
        
        var Fake = [
            'Hi there, I\'m Fabio and you?',
            'Nice to meet you',
            'How are you?',
            'Not too bad, thanks',
            'What do you do?',
            'That\'s awesome',
            'Codepen is a nice place to stay',
            'I think you\'re a nice person',
            'Why do you think that?',
            'Can you explain?',
            'Anyway I\'ve gotta go now',
            'It was a pleasure chat with you',
            'Time to make a new codepen',
            'Bye',
            ':)'
        ];
        var msg = "";
        var d, h, m, i = 0;
        var $messages;



        $(window).on('keydown', function (e)
        {
            if (e.which == 13)
            {
                insertMessage(true);
                return false;
            }
        });

        $timeout(function ()
        { 
            $.getJSON('_inbox/ChatContacs.json').done(function (json)
            {
                ctrlr.bchat_contactos = json;

                var fake_contact = {};
                fake_contact.anti_ID = "11111111-1111-1111-1111-111111111111";
                //fake_contact.anti_ID = "00000000-0000-0000-0000-000000000000";
                fake_contact.ativo = true;
                fake_contact.bllt_ID = null;
                fake_contact.dependencia = null;
                fake_contact.funcao = "Echo";
                fake_contact.usuario = "Supah";
                fake_contact.nome_apelido = "Supah";
                fake_contact.nome_completo = "Fabio Ottaviani";
                fake_contact.unidade = null;
                
                var new_chat_room = {};
                new_chat_room.anti_ID = null;
                new_chat_room.mensagens = [];

                ctrlr.bchat_chatrooms.push(new_chat_room);
                ctrlr.bchat_room_curr = new_chat_room;

                fake_contact.bchat_room = ctrlr.bchat_room_curr;
                ctrlr.bchat_contactos.unshift(fake_contact);

                carregarChat(ctrlr.bchat_room_curr.mensagens); 
            });
        });



        angular.element(document.querySelector('.messages-content')).bind('scroll', function () { ctrlr.reset(); });

        
        return ctrlr;
    }

})(angular.module('basicospa'));


 //ng-style="{'visibility' : item.mensagens.length > 0 ? '' : 'hidden'}"z


        //$scope.mydragg = function ()
        //{
        //    return {
        //        move: function (dividt, xpos, ypos)
        //        {
        //            var divid = dividt.currentTarget;
        //            divid.style.left = xpos + 'px';
        //            divid.style.top = ypos + 'px';
        //        },
        //        startMoving: function (dividt, container, evt)
        //        {
        //            var divid = dividt.currentTarget;
        //            evt = evt || window.event;

        //            var posX = evt.clientX,
        //                posY = evt.clientY,
        //                divTop = divid.style.top,
        //                divLeft = divid.style.left,
        //                eWi = parseInt(divid.style.width),
        //                eHe = parseInt(divid.style.height),
        //                cWi = parseInt(document.getElementById(container).style.width),
        //                cHe = parseInt(document.getElementById(container).style.height);
        //            document.getElementById(container).style.cursor = 'move';
        //            divTop = divTop.replace('px', '');
        //            divLeft = divLeft.replace('px', '');
        //            var diffX = posX - divLeft,
        //                diffY = posY - divTop;
        //            document.onmousemove = function (evt)
        //            {
        //                evt = evt || window.event;
        //                var posX = evt.clientX,
        //                    posY = evt.clientY,
        //                    aX = posX - diffX,
        //                    aY = posY - diffY;
        //                if (aX < 0) aX = 0;
        //                if (aY < 0) aY = 0;
        //                if (aX + eWi > cWi) aX = cWi - eWi;
        //                if (aY + eHe > cHe) aY = cHe - eHe;
        //                $scope.mydragg.move(dividt, aX, aY);
        //            }
        //        },
        //        stopMoving: function (container)
        //        {
        //            var a = document.createElement('script');
        //            document.getElementById(container).style.cursor = 'default';
        //            document.onmousemove = function () { }
        //        },
        //    }
        //}();
        // id = "draggable_javascript" ng- Mousedown="mydragg.startMoving($event,'draggable_javascript_container', event);" ng- mouseup="mydragg.stopMoving('draggable_javascript_container');"




        //$(window).load(function ()
        //{
        //    $messages.mCustomScrollbar();
        //    setTimeout(function ()
        //    {
        //        fakeMessage();
        //    }, 100);
        //});
