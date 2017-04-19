'use strict';

define(['app.module', 'app.constants', 'services/document-service'], function (app) {

    app.controller('document_controller', fnDocumentController)

    fnDocumentController.$inject = ['$scope', '$timeout', 'document_service', 'constants', '$stateParams'];

    function fnDocumentController($scope, $timeout, document_service, constants, $stateParams) {

        console.log("document controller running ...");


        var that = {
            documentation: {},
            api_descriptions: [],
        };

        that = angular.extend(this, that);

        active();

        function active() {

            // load document
            document_service.fnGetDocument($stateParams.controller).then(function (data) {
                that.documentation = data;
            });

            // load document api descriptions
            document_service.fnGetApiDescriptions($stateParams.controller).then(function (data) {
                that.api_descriptions = data;
            });
        }

    };


    return fnDocumentController;
});