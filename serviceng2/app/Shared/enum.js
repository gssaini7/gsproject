"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var DBOperation;
(function (DBOperation) {
    DBOperation[DBOperation["create"] = 1] = "create";
    DBOperation[DBOperation["update"] = 2] = "update";
    DBOperation[DBOperation["delete"] = 3] = "delete";
    DBOperation[DBOperation["deleteimage"] = 4] = "deleteimage";
})(DBOperation = exports.DBOperation || (exports.DBOperation = {}));
var MessageSeverity;
(function (MessageSeverity) {
    MessageSeverity["error"] = "error";
    MessageSeverity["success"] = "success";
})(MessageSeverity = exports.MessageSeverity || (exports.MessageSeverity = {}));
var PageOperation;
(function (PageOperation) {
    PageOperation[PageOperation["mainlistpage"] = 1] = "mainlistpage";
    PageOperation[PageOperation["sublistpage"] = 2] = "sublistpage";
    PageOperation[PageOperation["finalorderpage"] = 3] = "finalorderpage";
})(PageOperation = exports.PageOperation || (exports.PageOperation = {}));
var PageContentType;
(function (PageContentType) {
    PageContentType[PageContentType["none"] = 0] = "none";
    PageContentType[PageContentType["simpletext"] = 1] = "simpletext";
    PageContentType[PageContentType["htmlcode"] = 2] = "htmlcode";
    PageContentType[PageContentType["image"] = 3] = "image";
})(PageContentType = exports.PageContentType || (exports.PageContentType = {}));
//# sourceMappingURL=enum.js.map