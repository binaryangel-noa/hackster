var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", 'angular2/core', './import.service', 'rxjs/add/observable/fromArray'], function (require, exports, core_1, import_service_1) {
    var Import = (function () {
        function Import(_service) {
            this._service = _service;
            console.log("Import.ctor");
        }
        Import.prototype.copy = function () {
            this._service.copy().subscribe(function (updated) {
                var p = updated;
            });
        };
        Import.prototype.ngOnInit = function () {
            var _this = this;
            this._service.images$.subscribe(function (updated) {
                _this.images = updated;
            });
            this._service.load();
        };
        Import = __decorate([
            core_1.Component({
                selector: 'import',
                templateUrl: '/app/import/import.html',
                providers: [import_service_1.ImportService]
            }), 
            __metadata('design:paramtypes', [import_service_1.ImportService])
        ], Import);
        return Import;
    })();
    exports.Import = Import;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiIiwic291cmNlcyI6WyJhcHAvaW1wb3J0L2ltcG9ydC5qcyJdLCJzb3VyY2VzQ29udGVudCI6WyJ2YXIgX19kZWNvcmF0ZSA9ICh0aGlzICYmIHRoaXMuX19kZWNvcmF0ZSkgfHwgZnVuY3Rpb24gKGRlY29yYXRvcnMsIHRhcmdldCwga2V5LCBkZXNjKSB7XG4gICAgdmFyIGMgPSBhcmd1bWVudHMubGVuZ3RoLCByID0gYyA8IDMgPyB0YXJnZXQgOiBkZXNjID09PSBudWxsID8gZGVzYyA9IE9iamVjdC5nZXRPd25Qcm9wZXJ0eURlc2NyaXB0b3IodGFyZ2V0LCBrZXkpIDogZGVzYywgZDtcbiAgICBpZiAodHlwZW9mIFJlZmxlY3QgPT09IFwib2JqZWN0XCIgJiYgdHlwZW9mIFJlZmxlY3QuZGVjb3JhdGUgPT09IFwiZnVuY3Rpb25cIikgciA9IFJlZmxlY3QuZGVjb3JhdGUoZGVjb3JhdG9ycywgdGFyZ2V0LCBrZXksIGRlc2MpO1xuICAgIGVsc2UgZm9yICh2YXIgaSA9IGRlY29yYXRvcnMubGVuZ3RoIC0gMTsgaSA+PSAwOyBpLS0pIGlmIChkID0gZGVjb3JhdG9yc1tpXSkgciA9IChjIDwgMyA/IGQocikgOiBjID4gMyA/IGQodGFyZ2V0LCBrZXksIHIpIDogZCh0YXJnZXQsIGtleSkpIHx8IHI7XG4gICAgcmV0dXJuIGMgPiAzICYmIHIgJiYgT2JqZWN0LmRlZmluZVByb3BlcnR5KHRhcmdldCwga2V5LCByKSwgcjtcbn07XG52YXIgX19tZXRhZGF0YSA9ICh0aGlzICYmIHRoaXMuX19tZXRhZGF0YSkgfHwgZnVuY3Rpb24gKGssIHYpIHtcbiAgICBpZiAodHlwZW9mIFJlZmxlY3QgPT09IFwib2JqZWN0XCIgJiYgdHlwZW9mIFJlZmxlY3QubWV0YWRhdGEgPT09IFwiZnVuY3Rpb25cIikgcmV0dXJuIFJlZmxlY3QubWV0YWRhdGEoaywgdik7XG59O1xuZGVmaW5lKFtcInJlcXVpcmVcIiwgXCJleHBvcnRzXCIsICdhbmd1bGFyMi9jb3JlJywgJy4vaW1wb3J0LnNlcnZpY2UnLCAncnhqcy9hZGQvb2JzZXJ2YWJsZS9mcm9tQXJyYXknXSwgZnVuY3Rpb24gKHJlcXVpcmUsIGV4cG9ydHMsIGNvcmVfMSwgaW1wb3J0X3NlcnZpY2VfMSkge1xuICAgIHZhciBJbXBvcnQgPSAoZnVuY3Rpb24gKCkge1xuICAgICAgICBmdW5jdGlvbiBJbXBvcnQoX3NlcnZpY2UpIHtcbiAgICAgICAgICAgIHRoaXMuX3NlcnZpY2UgPSBfc2VydmljZTtcbiAgICAgICAgICAgIGNvbnNvbGUubG9nKFwiSW1wb3J0LmN0b3JcIik7XG4gICAgICAgIH1cbiAgICAgICAgSW1wb3J0LnByb3RvdHlwZS5jb3B5ID0gZnVuY3Rpb24gKCkge1xuICAgICAgICAgICAgdGhpcy5fc2VydmljZS5jb3B5KCkuc3Vic2NyaWJlKGZ1bmN0aW9uICh1cGRhdGVkKSB7XG4gICAgICAgICAgICAgICAgdmFyIHAgPSB1cGRhdGVkO1xuICAgICAgICAgICAgfSk7XG4gICAgICAgIH07XG4gICAgICAgIEltcG9ydC5wcm90b3R5cGUubmdPbkluaXQgPSBmdW5jdGlvbiAoKSB7XG4gICAgICAgICAgICB2YXIgX3RoaXMgPSB0aGlzO1xuICAgICAgICAgICAgdGhpcy5fc2VydmljZS5pbWFnZXMkLnN1YnNjcmliZShmdW5jdGlvbiAodXBkYXRlZCkge1xuICAgICAgICAgICAgICAgIF90aGlzLmltYWdlcyA9IHVwZGF0ZWQ7XG4gICAgICAgICAgICB9KTtcbiAgICAgICAgICAgIHRoaXMuX3NlcnZpY2UubG9hZCgpO1xuICAgICAgICB9O1xuICAgICAgICBJbXBvcnQgPSBfX2RlY29yYXRlKFtcbiAgICAgICAgICAgIGNvcmVfMS5Db21wb25lbnQoe1xuICAgICAgICAgICAgICAgIHNlbGVjdG9yOiAnaW1wb3J0JyxcbiAgICAgICAgICAgICAgICB0ZW1wbGF0ZVVybDogJy9hcHAvaW1wb3J0L2ltcG9ydC5odG1sJyxcbiAgICAgICAgICAgICAgICBwcm92aWRlcnM6IFtpbXBvcnRfc2VydmljZV8xLkltcG9ydFNlcnZpY2VdXG4gICAgICAgICAgICB9KSwgXG4gICAgICAgICAgICBfX21ldGFkYXRhKCdkZXNpZ246cGFyYW10eXBlcycsIFtpbXBvcnRfc2VydmljZV8xLkltcG9ydFNlcnZpY2VdKVxuICAgICAgICBdLCBJbXBvcnQpO1xuICAgICAgICByZXR1cm4gSW1wb3J0O1xuICAgIH0pKCk7XG4gICAgZXhwb3J0cy5JbXBvcnQgPSBJbXBvcnQ7XG59KTtcbiJdLCJmaWxlIjoiYXBwL2ltcG9ydC9pbXBvcnQuanMiLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==
