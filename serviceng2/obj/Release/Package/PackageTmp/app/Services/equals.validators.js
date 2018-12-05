"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var EqualsValidator = /** @class */ (function () {
    function EqualsValidator() {
    }
    /**
     * compares 2 values
     * */
    EqualsValidator.equals = function (valueControlNameA, valueControlNameB) {
        return function (group) {
            // get values
            var valueA = group.get(valueControlNameA).value;
            var valueB = group.get(valueControlNameB).value;
            return valueA === valueB ? null : { equals: true };
        };
    };
    return EqualsValidator;
}());
exports.EqualsValidator = EqualsValidator;
//# sourceMappingURL=equals.validators.js.map