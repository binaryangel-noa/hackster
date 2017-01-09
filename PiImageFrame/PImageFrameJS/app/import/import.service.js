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
    })();
    exports.ImportService = ImportService;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiIiwic291cmNlcyI6WyJhcHAvaW1wb3J0L2ltcG9ydC5zZXJ2aWNlLmpzIl0sInNvdXJjZXNDb250ZW50IjpbInZhciBfX2RlY29yYXRlID0gKHRoaXMgJiYgdGhpcy5fX2RlY29yYXRlKSB8fCBmdW5jdGlvbiAoZGVjb3JhdG9ycywgdGFyZ2V0LCBrZXksIGRlc2MpIHtcbiAgICB2YXIgYyA9IGFyZ3VtZW50cy5sZW5ndGgsIHIgPSBjIDwgMyA/IHRhcmdldCA6IGRlc2MgPT09IG51bGwgPyBkZXNjID0gT2JqZWN0LmdldE93blByb3BlcnR5RGVzY3JpcHRvcih0YXJnZXQsIGtleSkgOiBkZXNjLCBkO1xuICAgIGlmICh0eXBlb2YgUmVmbGVjdCA9PT0gXCJvYmplY3RcIiAmJiB0eXBlb2YgUmVmbGVjdC5kZWNvcmF0ZSA9PT0gXCJmdW5jdGlvblwiKSByID0gUmVmbGVjdC5kZWNvcmF0ZShkZWNvcmF0b3JzLCB0YXJnZXQsIGtleSwgZGVzYyk7XG4gICAgZWxzZSBmb3IgKHZhciBpID0gZGVjb3JhdG9ycy5sZW5ndGggLSAxOyBpID49IDA7IGktLSkgaWYgKGQgPSBkZWNvcmF0b3JzW2ldKSByID0gKGMgPCAzID8gZChyKSA6IGMgPiAzID8gZCh0YXJnZXQsIGtleSwgcikgOiBkKHRhcmdldCwga2V5KSkgfHwgcjtcbiAgICByZXR1cm4gYyA+IDMgJiYgciAmJiBPYmplY3QuZGVmaW5lUHJvcGVydHkodGFyZ2V0LCBrZXksIHIpLCByO1xufTtcbnZhciBfX21ldGFkYXRhID0gKHRoaXMgJiYgdGhpcy5fX21ldGFkYXRhKSB8fCBmdW5jdGlvbiAoaywgdikge1xuICAgIGlmICh0eXBlb2YgUmVmbGVjdCA9PT0gXCJvYmplY3RcIiAmJiB0eXBlb2YgUmVmbGVjdC5tZXRhZGF0YSA9PT0gXCJmdW5jdGlvblwiKSByZXR1cm4gUmVmbGVjdC5tZXRhZGF0YShrLCB2KTtcbn07XG5kZWZpbmUoW1wicmVxdWlyZVwiLCBcImV4cG9ydHNcIiwgJ2FuZ3VsYXIyL2NvcmUnLCAncnhqcy9PYnNlcnZhYmxlJywgJ2FuZ3VsYXIyL2NvcmUnLCAncnhqcy9SeCddLCBmdW5jdGlvbiAocmVxdWlyZSwgZXhwb3J0cywgY29yZV8xLCBPYnNlcnZhYmxlXzEsIGNvcmVfMikge1xuICAgIHZhciBJbXBvcnRTZXJ2aWNlID0gKGZ1bmN0aW9uICgpIHtcbiAgICAgICAgZnVuY3Rpb24gSW1wb3J0U2VydmljZSh6b25lKSB7XG4gICAgICAgICAgICB2YXIgX3RoaXMgPSB0aGlzO1xuICAgICAgICAgICAgdGhpcy56b25lID0gem9uZTtcbiAgICAgICAgICAgIHRoaXMuaW1hZ2VzJCA9IG5ldyBPYnNlcnZhYmxlXzEuT2JzZXJ2YWJsZShmdW5jdGlvbiAob2JzZXJ2ZXIpIHsgcmV0dXJuIF90aGlzLl9vYnNlcnZlciA9IG9ic2VydmVyOyB9KS5zaGFyZSgpO1xuICAgICAgICB9XG4gICAgICAgIEltcG9ydFNlcnZpY2UucHJvdG90eXBlLmxvYWQgPSBmdW5jdGlvbiAoKSB7XG4gICAgICAgICAgICB2YXIgX3RoaXMgPSB0aGlzO1xuICAgICAgICAgICAgdmFyIGJsbCA9IG5ldyBCbGxSVC5TZE9wZXJhdGlvbnNCcmlkZ2UoKTtcbiAgICAgICAgICAgIHZhciBhc3luY29wZXJhdGlvbiA9IGJsbC5saXN0RmlsZXMoKTtcbiAgICAgICAgICAgIGFzeW5jb3BlcmF0aW9uLmRvbmUoZnVuY3Rpb24gKGRhdGEpIHtcbiAgICAgICAgICAgICAgICBfdGhpcy56b25lLnJ1bihmdW5jdGlvbiAoKSB7XG4gICAgICAgICAgICAgICAgICAgIGlmIChkYXRhLmxlbmd0aCA+IDApIHtcbiAgICAgICAgICAgICAgICAgICAgICAgIF90aGlzLl9vYnNlcnZlci5uZXh0KGRhdGEpO1xuICAgICAgICAgICAgICAgICAgICB9XG4gICAgICAgICAgICAgICAgICAgIGVsc2Uge1xuICAgICAgICAgICAgICAgICAgICAgICAgX3RoaXMuX29ic2VydmVyLm5leHQoW10pO1xuICAgICAgICAgICAgICAgICAgICB9XG4gICAgICAgICAgICAgICAgfSk7XG4gICAgICAgICAgICB9KTtcbiAgICAgICAgfTtcbiAgICAgICAgSW1wb3J0U2VydmljZS5wcm90b3R5cGUuY29weSA9IGZ1bmN0aW9uICgpIHtcbiAgICAgICAgICAgIHZhciBfdGhpcyA9IHRoaXM7XG4gICAgICAgICAgICB2YXIgYmxsID0gbmV3IEJsbFJULlNkT3BlcmF0aW9uc0JyaWRnZSgpO1xuICAgICAgICAgICAgdmFyIGFzeW5jb3BlcmF0aW9uID0gYmxsLmNvcHlGaWxlcygpO1xuICAgICAgICAgICAgdmFyIHN1YnNjcmliZXI7XG4gICAgICAgICAgICB2YXIgb2JzID0gbmV3IE9ic2VydmFibGVfMS5PYnNlcnZhYmxlKGZ1bmN0aW9uIChzYikgeyByZXR1cm4gc3Vic2NyaWJlciA9IHNiOyB9KTtcbiAgICAgICAgICAgIGFzeW5jb3BlcmF0aW9uLmRvbmUoZnVuY3Rpb24gKHJlcykge1xuICAgICAgICAgICAgICAgIF90aGlzLnpvbmUucnVuKGZ1bmN0aW9uICgpIHtcbiAgICAgICAgICAgICAgICAgICAgc3Vic2NyaWJlci5uZXh0KHJlcyk7XG4gICAgICAgICAgICAgICAgfSk7XG4gICAgICAgICAgICB9KTtcbiAgICAgICAgICAgIHJldHVybiBvYnM7XG4gICAgICAgIH07XG4gICAgICAgIEltcG9ydFNlcnZpY2UgPSBfX2RlY29yYXRlKFtcbiAgICAgICAgICAgIGNvcmVfMS5JbmplY3RhYmxlKCksIFxuICAgICAgICAgICAgX19tZXRhZGF0YSgnZGVzaWduOnBhcmFtdHlwZXMnLCBbY29yZV8yLk5nWm9uZV0pXG4gICAgICAgIF0sIEltcG9ydFNlcnZpY2UpO1xuICAgICAgICByZXR1cm4gSW1wb3J0U2VydmljZTtcbiAgICB9KSgpO1xuICAgIGV4cG9ydHMuSW1wb3J0U2VydmljZSA9IEltcG9ydFNlcnZpY2U7XG59KTtcbiJdLCJmaWxlIjoiYXBwL2ltcG9ydC9pbXBvcnQuc2VydmljZS5qcyIsInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9
