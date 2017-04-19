"use strict";

define(['app.module', 'app.constants'], function (app) {

    app.factory('document_service', document_service);

    document_service.$inject = ['$http', 'constants'];

    function document_service($http, constants) {

        return {
            documents: fnGetDocuments(),

            fnGetDocument: fnGetDocument,
            fnGetModelDescription:fnGetModelDescription,
            fnGetApiDescription: fnGetApiDescription,
            fnGetApiDescriptions: fnGetApiDescriptions,
        };

        function fnGetDocument(controller) {
            
            return $http.get(constants.rootPath + '/documents/' + controller)
                       .then(function (response) {
                           return response.data;
                       })
                       .catch(function (error) {
                           console.log('XHR Failed for get document.' + error.data);
                       });
        }

        function fnGetDocuments() {

            return $http.get(constants.rootPath + '/documents')
                       .then(function (response) {
                           return response.data;
                       })
                       .catch(function (error) {
                           console.log('XHR Failed for get documents.' + error.data);
                       });
        }
        
        function fnGetModelDescription(name) {

            return $http.get(constants.rootPath + '/documents/model-description/' + name)
                       .then(function (response) {
                           return response.data;
                       })
                       .catch(function (error) {
                           console.log('XHR Failed for get document api descriptions.' + error.data);
                       });
        }
        function fnGetApiDescriptions(controller) {

            return $http.get(constants.rootPath + '/documents/' + controller + '/api-descriptions')
                       .then(function (response) {
                           return response.data;
                       })
                       .catch(function (error) {
                           console.log('XHR Failed for get document api descriptions.' + error.data);
                       });
        }
        function fnGetApiDescription(controller,id) {

            return $http.get(constants.rootPath + '/documents/' + controller + '/api-description/' + id)
                       .then(function (response) {
                           return response.data;
                       })
                       .catch(function (error) {
                           console.log('XHR Failed for get document api description.' + error.data);
                       });
        }
    }

});


