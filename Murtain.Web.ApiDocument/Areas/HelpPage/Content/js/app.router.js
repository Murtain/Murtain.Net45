'use strict';

define(['app.module', 'angular-AMD'], function (app, angularAMD) {

    app.run(function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    });

    app.config(["$stateProvider", "$urlRouterProvider", "$locationProvider", "$uiViewScrollProvider", function ($stateProvider, $urlRouterProvider, $locationProvider, $uiViewScrollProvider) {

        $uiViewScrollProvider.useAnchorScroll();

        $urlRouterProvider.otherwise("/");

        $stateProvider.state('readme', angularAMD.route({
            url: '/readme',
            controllerUrl: '/Areas/HelpPage/content/js/controllers/document-readme.js',
            templateUrl: '/document/readme',
            controllerAs: 'model',
        }));
        $stateProvider.state('document', angularAMD.route({
            url: '/document/:controller',
            controllerUrl: '/Areas/HelpPage/content/js/controllers/document.js',
            templateUrl: '/document/documentation',
            controllerAs: 'model',
        }));
        $stateProvider.state('document-description', angularAMD.route({
            url: '/document-description/:controller_name/:id',
            controllerUrl: '/Areas/HelpPage/content/js/controllers/document-api-description.js',
            templateUrl: '/document/api',
            controllerAs: 'model',
        }));
        $stateProvider.state('model', angularAMD.route({
            url: '/model/:name',
            controllerUrl: '/Areas/HelpPage/content/js/controllers/document-model-description.js',
            templateUrl: '/document/model',
            controllerAs: 'model',
        }));
    }]);
    return app;
})