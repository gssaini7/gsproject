'use strict';
app.factory('rolesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var localServiceFactory = {};

    var _getAll = function () {
        
        return $http.get(serviceBase + 'api/Roles').then(function (results) {
            return results;
        });
    };

    localServiceFactory.getRoles = _getAll;

    return localServiceFactory;

}]);