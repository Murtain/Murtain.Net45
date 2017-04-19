"use strict";

define(['angular', 'angular-ui-router', 'angular-loading-bar', 'angular-animate'], function (angular) {

    var app = angular.module('app', ['ui.router', 'angular-loading-bar']);

    app.config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeSpinner = true;
    }]);

    app.config(['$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode({ enable: true, requireBase: false });
    }]);

    return app;

});