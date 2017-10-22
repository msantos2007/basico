/**
 * @author Alvaro Paçó <alvaro@scalasoft.com.br>
 * Modificado Marcelo "Observe"
 * @see {@link http://www.geradorcpf.com/javascript-validar-cpf.htm}
 */
(function (app) {
    'use strict';

    app.directive('validateCpf', validateCpf);
    
    function validateCpf () {
          return {
              require: 'ngModel',
              link: function (scope, element, attrs, ctrl) {

                  attrs.$observe('validateCpf', function (newval) {
                     
                      if (ctrl.$isEmpty(newval) && element[0].required == false) {
                          ctrl.$setValidity('validateCpf', true);
                          element.removeClass('has-error');
                          return true;
                      }

                      if (ctrl.$dirty && ctrl.$invalid) {
                          element.addClass('has-error');
                      } else {
                          element.removeClass('has-error');
                      }

                      ctrl.$setValidity('validateCpf', validaCPF(newval));

                      function validaCPF(str) {
                          if (str) {
                              str = str.replace('.', '');
                              str = str.replace('.', '');
                              str = str.replace('-', '');

                              var cpf = str;
                              var numeros, digitos, soma, i, resultado, digitos_iguais;
                              digitos_iguais = 1;
                              if (cpf.length < 11)
                                  return false;
                              for (i = 0; i < cpf.length - 1; i++)
                                  if (cpf.charAt(i) != cpf.charAt(i + 1)) {
                                      digitos_iguais = 0;
                                      break;
                                  }
                              if (!digitos_iguais) {
                                  numeros = cpf.substring(0, 9);
                                  digitos = cpf.substring(9);
                                  soma = 0;
                                  for (i = 10; i > 1; i--)
                                      soma += numeros.charAt(10 - i) * i;
                                  resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                                  if (resultado != digitos.charAt(0))
                                      return false;
                                  numeros = cpf.substring(0, 10);
                                  soma = 0;
                                  for (i = 11; i > 1; i--)
                                      soma += numeros.charAt(11 - i) * i;
                                  resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                                  if (resultado != digitos.charAt(1))
                                      return false;
                                  return true;
                              }
                              else
                                  return false;
                          }
                      }
                  });
              }
          }
      }
})(angular.module('basicospa'));
