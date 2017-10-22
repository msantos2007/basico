(function (app) {
    'use strict';

    app.directive('tableMyisaki', tableMyisaki);

    function tableMyisaki() {
        return {
            restrict: "A",                        
            link: function (scope, element, attrs) {
                //console.log('Do action with data', attrs.tableMyisaki);

                    var headertext = [];
                    //var headers = document.getElementsByName(attrs.tableMyisaki)[0].children[0].children[0].children;
                    //var tablerows = document.getElementsByName(attrs.tableMyisaki)[0].children[0].children[0].children;
                    //var tablebody = document.getElementsByName(attrs.tableMyisaki)[0].children[1];

                    var headers = element.closest('table')[0].children[0].children[0].children;
                    var tablerows = element.closest('table')[0].children[0].children[0].children;
                    var tablebody = element.closest('tbody')[0];

                    for (var i = 0; i < headers.length; i++) {
                        var current = headers[i];
                        headertext.push(current.textContent.replace(/\r?\n|\r/, ""));
                    }

                    for (var i = 0, row; row = tablebody.rows[i]; i++) {
                        for (var j = 0, col; col = row.cells[j]; j++) {
                            col.setAttribute("data-th", headertext[j]);
                        }
                    }
            }
        };
    }
})(angular.module('basicospa'));
