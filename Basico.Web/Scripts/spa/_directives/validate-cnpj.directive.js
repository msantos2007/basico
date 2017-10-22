/**
 * @author Alvaro Paçó <alvaro@scalasoft.com.br>
 * Modificado Marcelo "Observe"
 * @see {@link http://www.geradorcnpj.com/javascript-validar-cnpj.htm}
 */
(function (app) {
    'use strict';

    app.directive('validateCnpj', validateCnpj);

    function validateCnpj() {
          return {
              require: 'ngModel',              
              link: function (scope, element, attrs, ctrl) {

                  attrs.$observe('validateCnpj', function (newval) {
                      
                      if (ctrl.$isEmpty(newval) && element[0].required == false) {
                          //ctrl.$invalid = false;
                          ctrl.$setValidity('validateCnpj', true);
                          element.removeClass('has-error');
                          return true;
                      }

                      if (ctrl.$dirty && ctrl.$invalid) {
                          element.addClass('has-error');
                      } else {
                          element.removeClass('has-error');
                      }

                      ctrl.$setValidity('validateCnpj', validaCNPJ(newval));

                      function validaCNPJ(str) {
                          if (str == null)
                              return false;

                          str = str.replace(/\./g, '');
                          str = str.replace('/', '');
                          str = str.replace('-', '');

                          var cnpj = str;
                          var tamanho;
                          var numeros;
                          var digitos;
                          var soma;
                          var pos;
                          var resultado;
                          var i;

                          if (cnpj == '')
                              return false;

                          if (cnpj.length != 14)
                              return false;

                          // Regex to validate strings with 14 same characters
                          var regex = /([0]{14}|[1]{14}|[2]{14}|[3]{14}|[4]{14}|[5]{14}|[6]{14}|[7]{14}|[8]{14}|[9]{14})/g
                          // Regex builder
                          var patt = new RegExp(regex);
                          if (patt.test(cnpj))
                              return false;

                          // Valida DVs
                          tamanho = cnpj.length - 2
                          numeros = cnpj.substring(0, tamanho);
                          digitos = cnpj.substring(tamanho);
                          soma = 0;
                          pos = tamanho - 7;
                          for (i = tamanho; i >= 1; i--) {
                              soma += numeros.charAt(tamanho - i) * pos--;
                              if (pos < 2)
                                  pos = 9;
                          }
                          resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                          if (resultado != digitos.charAt(0))
                              return false;

                          tamanho = tamanho + 1;
                          numeros = cnpj.substring(0, tamanho);
                          soma = 0;
                          pos = tamanho - 7;
                          for (i = tamanho; i >= 1; i--) {
                              soma += numeros.charAt(tamanho - i) * pos--;
                              if (pos < 2)
                                  pos = 9;
                          }
                          resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                          if (resultado != digitos.charAt(1))
                              return false;

                          return true;
                      }
                  });                 
              }
          }
    }
})(angular.module('basicospa'));
