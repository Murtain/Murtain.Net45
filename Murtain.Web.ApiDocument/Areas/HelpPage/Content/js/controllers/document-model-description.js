'use strict';

define(['app.module', 'app.constants', 'services/document-service'], function (app) {

    app.controller('document_model_controller', fnDocumentModelController)

    fnDocumentModelController.$inject = ['$scope', '$timeout', 'document_service', 'constants', '$stateParams'];

    function fnDocumentModelController($scope, $timeout, document_service, constants, $stateParams) {

        var that = {
            description: {},
        };


        that = angular.extend(this, that);

        active();

        function active() {

            // load model description
            document_service.fnGetModelDescription($stateParams.name).then(function (data) {
                that.description = data;
                console.log(data)
            });
        }

    };


    return fnDocumentModelController;
});