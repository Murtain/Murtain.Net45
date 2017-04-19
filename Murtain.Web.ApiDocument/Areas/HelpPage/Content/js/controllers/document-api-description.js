'use strict';

define(['app.module', 'app.constants', 'services/document-service'], function (app) {

    app.controller('document_api_controller', fnDocumentApisController)

    fnDocumentApisController.$inject = ['$scope', '$timeout', 'document_service', 'constants', '$stateParams'];

    function fnDocumentApisController($scope, $timeout, document_service, constants, $stateParams) {

        console.log("document_api_controller running ...");

        var that = {
            api_description: {},
        };


        that = angular.extend(this, that);

        active();

        function active() {

            // load document
            document_service.fnGetApiDescription($stateParams.controller_name,$stateParams.id).then(function (data) {

                // sample

                if (data.sample_responses['application/json']) {
                    data.sample1 = angular.toJson(data.sample_responses['application/json'], true);
                }
                if (data.sample_responses['application/x-error']) {
                    data.sample2 = angular.toJson(data.sample_responses['application/x-error'], true);
                };

                // http_status_code
                if (data.return_code_model_description && data.return_code_model_description.values) {

                    var codes = data.return_code_model_description.values;

                    for (var i = 0; i < codes.length; i++) {
                        var code = codes[i];
                        var http_status_code;

                        for (var m = 0; m < code.aniotations.length; m++) {
                            if (code.aniotations[m].annotation_attribute.http_status_code != undefined) {
                                http_status_code = code.aniotations[m].annotation_attribute.http_status_code;
                            }
                        }

                        code.http_status_code = http_status_code;
                    }
                }
                that.api_description = data;
            });
        }


    };


    return fnDocumentApisController;
});