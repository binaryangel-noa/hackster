var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", 'angular2/core', 'rxjs/Observable', 'angular2/core', 'rxjs/Rx'], function (require, exports, core_1, Observable_1, core_2) {
    "use strict";
    var ImportService = (function () {
        function ImportService(zone) {
            var _this = this;
            this.zone = zone;
            this.images$ = new Observable_1.Observable(function (observer) { return _this._observer = observer; }).share();
        }
        ImportService.prototype.load = function () {
            var _this = this;
            var bll = new BllRT.SdOperationsBridge();
            var asyncoperation = bll.listFiles();
            asyncoperation.done(function (data) {
                _this.zone.run(function () {
                    if (data.length > 0) {
                        _this._observer.next(data);
                    }
                    else {
                        _this._observer.next([]);
                    }
                });
            });
        };
        ImportService.prototype.copy = function () {
            var _this = this;
            var bll = new BllRT.SdOperationsBridge();
            var asyncoperation = bll.copyFiles();
            var subscriber;
            var obs = new Observable_1.Observable(function (sb) { return subscriber = sb; });
            asyncoperation.done(function (res) {
                _this.zone.run(function () {
                    subscriber.next(res);
                });
            });
            return obs;
        };
        ImportService = __decorate([
            core_1.Injectable(), 
            __metadata('design:paramtypes', [core_2.NgZone])
        ], ImportService);
        return ImportService;
    }());
    exports.ImportService = ImportService;
});
//# sourceMappingURL=import.service.js.map