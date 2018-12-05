'use strict';
app.controller('rolesController', ['$scope', 'rolesService', function ($scope, rolesService) {

    $scope.roles = [];

    rolesService.getRoles().then(function (results) {

        $scope.roles = results.data;

    }, function (error) {
        //alert(error.data.message);
    });

}]);