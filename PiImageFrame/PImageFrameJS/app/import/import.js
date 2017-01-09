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
    "use strict";
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
    }());
    exports.Import = Import;
});
//# sourceMappingURL=import.js.map