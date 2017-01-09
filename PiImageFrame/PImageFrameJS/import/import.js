System.register(['angular2/core', './import.service', 'rxjs/add/observable/fromArray'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, import_service_1;
    var Import;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (import_service_1_1) {
                import_service_1 = import_service_1_1;
            },
            function (_1) {}],
        execute: function() {
            Import = (function () {
                function Import(_service) {
                    this._service = _service;
                    console.log("Import.ctor");
                }
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
            exports_1("Import", Import);
        }
    }
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiIiwic291cmNlcyI6WyJpbXBvcnQvaW1wb3J0LmpzIl0sInNvdXJjZXNDb250ZW50IjpbIlN5c3RlbS5yZWdpc3RlcihbJ2FuZ3VsYXIyL2NvcmUnLCAnLi9pbXBvcnQuc2VydmljZScsICdyeGpzL2FkZC9vYnNlcnZhYmxlL2Zyb21BcnJheSddLCBmdW5jdGlvbihleHBvcnRzXzEpIHtcbiAgICB2YXIgX19kZWNvcmF0ZSA9ICh0aGlzICYmIHRoaXMuX19kZWNvcmF0ZSkgfHwgZnVuY3Rpb24gKGRlY29yYXRvcnMsIHRhcmdldCwga2V5LCBkZXNjKSB7XG4gICAgICAgIHZhciBjID0gYXJndW1lbnRzLmxlbmd0aCwgciA9IGMgPCAzID8gdGFyZ2V0IDogZGVzYyA9PT0gbnVsbCA/IGRlc2MgPSBPYmplY3QuZ2V0T3duUHJvcGVydHlEZXNjcmlwdG9yKHRhcmdldCwga2V5KSA6IGRlc2MsIGQ7XG4gICAgICAgIGlmICh0eXBlb2YgUmVmbGVjdCA9PT0gXCJvYmplY3RcIiAmJiB0eXBlb2YgUmVmbGVjdC5kZWNvcmF0ZSA9PT0gXCJmdW5jdGlvblwiKSByID0gUmVmbGVjdC5kZWNvcmF0ZShkZWNvcmF0b3JzLCB0YXJnZXQsIGtleSwgZGVzYyk7XG4gICAgICAgIGVsc2UgZm9yICh2YXIgaSA9IGRlY29yYXRvcnMubGVuZ3RoIC0gMTsgaSA+PSAwOyBpLS0pIGlmIChkID0gZGVjb3JhdG9yc1tpXSkgciA9IChjIDwgMyA/IGQocikgOiBjID4gMyA/IGQodGFyZ2V0LCBrZXksIHIpIDogZCh0YXJnZXQsIGtleSkpIHx8IHI7XG4gICAgICAgIHJldHVybiBjID4gMyAmJiByICYmIE9iamVjdC5kZWZpbmVQcm9wZXJ0eSh0YXJnZXQsIGtleSwgciksIHI7XG4gICAgfTtcbiAgICB2YXIgX19tZXRhZGF0YSA9ICh0aGlzICYmIHRoaXMuX19tZXRhZGF0YSkgfHwgZnVuY3Rpb24gKGssIHYpIHtcbiAgICAgICAgaWYgKHR5cGVvZiBSZWZsZWN0ID09PSBcIm9iamVjdFwiICYmIHR5cGVvZiBSZWZsZWN0Lm1ldGFkYXRhID09PSBcImZ1bmN0aW9uXCIpIHJldHVybiBSZWZsZWN0Lm1ldGFkYXRhKGssIHYpO1xuICAgIH07XG4gICAgdmFyIGNvcmVfMSwgaW1wb3J0X3NlcnZpY2VfMTtcbiAgICB2YXIgSW1wb3J0O1xuICAgIHJldHVybiB7XG4gICAgICAgIHNldHRlcnM6W1xuICAgICAgICAgICAgZnVuY3Rpb24gKGNvcmVfMV8xKSB7XG4gICAgICAgICAgICAgICAgY29yZV8xID0gY29yZV8xXzE7XG4gICAgICAgICAgICB9LFxuICAgICAgICAgICAgZnVuY3Rpb24gKGltcG9ydF9zZXJ2aWNlXzFfMSkge1xuICAgICAgICAgICAgICAgIGltcG9ydF9zZXJ2aWNlXzEgPSBpbXBvcnRfc2VydmljZV8xXzE7XG4gICAgICAgICAgICB9LFxuICAgICAgICAgICAgZnVuY3Rpb24gKF8xKSB7fV0sXG4gICAgICAgIGV4ZWN1dGU6IGZ1bmN0aW9uKCkge1xuICAgICAgICAgICAgSW1wb3J0ID0gKGZ1bmN0aW9uICgpIHtcbiAgICAgICAgICAgICAgICBmdW5jdGlvbiBJbXBvcnQoX3NlcnZpY2UpIHtcbiAgICAgICAgICAgICAgICAgICAgdGhpcy5fc2VydmljZSA9IF9zZXJ2aWNlO1xuICAgICAgICAgICAgICAgICAgICBjb25zb2xlLmxvZyhcIkltcG9ydC5jdG9yXCIpO1xuICAgICAgICAgICAgICAgIH1cbiAgICAgICAgICAgICAgICBJbXBvcnQucHJvdG90eXBlLm5nT25Jbml0ID0gZnVuY3Rpb24gKCkge1xuICAgICAgICAgICAgICAgICAgICB2YXIgX3RoaXMgPSB0aGlzO1xuICAgICAgICAgICAgICAgICAgICB0aGlzLl9zZXJ2aWNlLmltYWdlcyQuc3Vic2NyaWJlKGZ1bmN0aW9uICh1cGRhdGVkKSB7XG4gICAgICAgICAgICAgICAgICAgICAgICBfdGhpcy5pbWFnZXMgPSB1cGRhdGVkO1xuICAgICAgICAgICAgICAgICAgICB9KTtcbiAgICAgICAgICAgICAgICAgICAgdGhpcy5fc2VydmljZS5sb2FkKCk7XG4gICAgICAgICAgICAgICAgfTtcbiAgICAgICAgICAgICAgICBJbXBvcnQgPSBfX2RlY29yYXRlKFtcbiAgICAgICAgICAgICAgICAgICAgY29yZV8xLkNvbXBvbmVudCh7XG4gICAgICAgICAgICAgICAgICAgICAgICBzZWxlY3RvcjogJ2ltcG9ydCcsXG4gICAgICAgICAgICAgICAgICAgICAgICB0ZW1wbGF0ZVVybDogJy9hcHAvaW1wb3J0L2ltcG9ydC5odG1sJyxcbiAgICAgICAgICAgICAgICAgICAgICAgIHByb3ZpZGVyczogW2ltcG9ydF9zZXJ2aWNlXzEuSW1wb3J0U2VydmljZV1cbiAgICAgICAgICAgICAgICAgICAgfSksIFxuICAgICAgICAgICAgICAgICAgICBfX21ldGFkYXRhKCdkZXNpZ246cGFyYW10eXBlcycsIFtpbXBvcnRfc2VydmljZV8xLkltcG9ydFNlcnZpY2VdKVxuICAgICAgICAgICAgICAgIF0sIEltcG9ydCk7XG4gICAgICAgICAgICAgICAgcmV0dXJuIEltcG9ydDtcbiAgICAgICAgICAgIH0pKCk7XG4gICAgICAgICAgICBleHBvcnRzXzEoXCJJbXBvcnRcIiwgSW1wb3J0KTtcbiAgICAgICAgfVxuICAgIH1cbn0pO1xuIl0sImZpbGUiOiJpbXBvcnQvaW1wb3J0LmpzIiwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=
