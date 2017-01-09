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
    var PicturesService = (function () {
        function PicturesService(zone) {
            var _this = this;
            this.zone = zone;
            this.images$ = new Observable_1.Observable(function (observer) { return _this._observer = observer; }).share();
            this._sensorDataServicePollBridge = new BllRT.SensorDataServicePollBridge();
        }
        PicturesService.prototype.pollData = function () {
            return this._sensorDataServicePollBridge.getSensorData();
        };
        PicturesService.prototype.load = function () {
            var _this = this;
            var bll = new BllRT.SdOperationsBridge();
            var asyncoperation = bll.listCopiedFiles();
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
        PicturesService = __decorate([
            core_1.Injectable(), 
            __metadata('design:paramtypes', [core_2.NgZone])
        ], PicturesService);
        return PicturesService;
    }());
    exports.PicturesService = PicturesService;
});
//# sourceMappingURL=pictures.service.js.map