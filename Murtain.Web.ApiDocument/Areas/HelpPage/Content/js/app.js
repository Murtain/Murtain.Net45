"use strict";


define(['app.module', 'angular-AMD', 'angular-AMD-ngload', 'app.router', 'app.constants', 'services/document-service', 'jquery', 'jquery-metismenu'], function (app, angularAMD) {

    app.controller('index', index)

    index.$inject = ['$scope', '$timeout', 'document_service', 'constants'];

    function index($scope, $timeout, document_service, constants) {

        console.log("index controller running ...");

        var that = {
            documents: [],
            icon:'https://t.alipayobjects.com/images/rmsweb/T1B9hfXcdvXXXXXXXX.svg',
            brand: 'Ant Design',

        };


        that = angular.extend(this, that);

        active();

        function active() {
            document_service.documents.then(function (data) {

                that.documents = data;

                $timeout(function () {
                    $("#menu").metisMenu();
                });
            });

        }


    };

    return angularAMD.bootstrap(app);
});