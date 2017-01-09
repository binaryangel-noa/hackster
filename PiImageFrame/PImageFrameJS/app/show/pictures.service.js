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
    })();
    exports.PicturesService = PicturesService;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiIiwic291cmNlcyI6WyJhcHAvc2hvdy9waWN0dXJlcy5zZXJ2aWNlLmpzIl0sInNvdXJjZXNDb250ZW50IjpbInZhciBfX2RlY29yYXRlID0gKHRoaXMgJiYgdGhpcy5fX2RlY29yYXRlKSB8fCBmdW5jdGlvbiAoZGVjb3JhdG9ycywgdGFyZ2V0LCBrZXksIGRlc2MpIHtcbiAgICB2YXIgYyA9IGFyZ3VtZW50cy5sZW5ndGgsIHIgPSBjIDwgMyA/IHRhcmdldCA6IGRlc2MgPT09IG51bGwgPyBkZXNjID0gT2JqZWN0LmdldE93blByb3BlcnR5RGVzY3JpcHRvcih0YXJnZXQsIGtleSkgOiBkZXNjLCBkO1xuICAgIGlmICh0eXBlb2YgUmVmbGVjdCA9PT0gXCJvYmplY3RcIiAmJiB0eXBlb2YgUmVmbGVjdC5kZWNvcmF0ZSA9PT0gXCJmdW5jdGlvblwiKSByID0gUmVmbGVjdC5kZWNvcmF0ZShkZWNvcmF0b3JzLCB0YXJnZXQsIGtleSwgZGVzYyk7XG4gICAgZWxzZSBmb3IgKHZhciBpID0gZGVjb3JhdG9ycy5sZW5ndGggLSAxOyBpID49IDA7IGktLSkgaWYgKGQgPSBkZWNvcmF0b3JzW2ldKSByID0gKGMgPCAzID8gZChyKSA6IGMgPiAzID8gZCh0YXJnZXQsIGtleSwgcikgOiBkKHRhcmdldCwga2V5KSkgfHwgcjtcbiAgICByZXR1cm4gYyA+IDMgJiYgciAmJiBPYmplY3QuZGVmaW5lUHJvcGVydHkodGFyZ2V0LCBrZXksIHIpLCByO1xufTtcbnZhciBfX21ldGFkYXRhID0gKHRoaXMgJiYgdGhpcy5fX21ldGFkYXRhKSB8fCBmdW5jdGlvbiAoaywgdikge1xuICAgIGlmICh0eXBlb2YgUmVmbGVjdCA9PT0gXCJvYmplY3RcIiAmJiB0eXBlb2YgUmVmbGVjdC5tZXRhZGF0YSA9PT0gXCJmdW5jdGlvblwiKSByZXR1cm4gUmVmbGVjdC5tZXRhZGF0YShrLCB2KTtcbn07XG5kZWZpbmUoW1wicmVxdWlyZVwiLCBcImV4cG9ydHNcIiwgJ2FuZ3VsYXIyL2NvcmUnLCAncnhqcy9PYnNlcnZhYmxlJywgJ2FuZ3VsYXIyL2NvcmUnLCAncnhqcy9SeCddLCBmdW5jdGlvbiAocmVxdWlyZSwgZXhwb3J0cywgY29yZV8xLCBPYnNlcnZhYmxlXzEsIGNvcmVfMikge1xuICAgIHZhciBQaWN0dXJlc1NlcnZpY2UgPSAoZnVuY3Rpb24gKCkge1xuICAgICAgICBmdW5jdGlvbiBQaWN0dXJlc1NlcnZpY2Uoem9uZSkge1xuICAgICAgICAgICAgdmFyIF90aGlzID0gdGhpcztcbiAgICAgICAgICAgIHRoaXMuem9uZSA9IHpvbmU7XG4gICAgICAgICAgICB0aGlzLmltYWdlcyQgPSBuZXcgT2JzZXJ2YWJsZV8xLk9ic2VydmFibGUoZnVuY3Rpb24gKG9ic2VydmVyKSB7IHJldHVybiBfdGhpcy5fb2JzZXJ2ZXIgPSBvYnNlcnZlcjsgfSkuc2hhcmUoKTtcbiAgICAgICAgICAgIHRoaXMuX3NlbnNvckRhdGFTZXJ2aWNlUG9sbEJyaWRnZSA9IG5ldyBCbGxSVC5TZW5zb3JEYXRhU2VydmljZVBvbGxCcmlkZ2UoKTtcbiAgICAgICAgfVxuICAgICAgICBQaWN0dXJlc1NlcnZpY2UucHJvdG90eXBlLnBvbGxEYXRhID0gZnVuY3Rpb24gKCkge1xuICAgICAgICAgICAgcmV0dXJuIHRoaXMuX3NlbnNvckRhdGFTZXJ2aWNlUG9sbEJyaWRnZS5nZXRTZW5zb3JEYXRhKCk7XG4gICAgICAgIH07XG4gICAgICAgIFBpY3R1cmVzU2VydmljZS5wcm90b3R5cGUubG9hZCA9IGZ1bmN0aW9uICgpIHtcbiAgICAgICAgICAgIHZhciBfdGhpcyA9IHRoaXM7XG4gICAgICAgICAgICB2YXIgYmxsID0gbmV3IEJsbFJULlNkT3BlcmF0aW9uc0JyaWRnZSgpO1xuICAgICAgICAgICAgdmFyIGFzeW5jb3BlcmF0aW9uID0gYmxsLmxpc3RDb3BpZWRGaWxlcygpO1xuICAgICAgICAgICAgYXN5bmNvcGVyYXRpb24uZG9uZShmdW5jdGlvbiAoZGF0YSkge1xuICAgICAgICAgICAgICAgIF90aGlzLnpvbmUucnVuKGZ1bmN0aW9uICgpIHtcbiAgICAgICAgICAgICAgICAgICAgaWYgKGRhdGEubGVuZ3RoID4gMCkge1xuICAgICAgICAgICAgICAgICAgICAgICAgX3RoaXMuX29ic2VydmVyLm5leHQoZGF0YSk7XG4gICAgICAgICAgICAgICAgICAgIH1cbiAgICAgICAgICAgICAgICAgICAgZWxzZSB7XG4gICAgICAgICAgICAgICAgICAgICAgICBfdGhpcy5fb2JzZXJ2ZXIubmV4dChbXSk7XG4gICAgICAgICAgICAgICAgICAgIH1cbiAgICAgICAgICAgICAgICB9KTtcbiAgICAgICAgICAgIH0pO1xuICAgICAgICB9O1xuICAgICAgICBQaWN0dXJlc1NlcnZpY2UgPSBfX2RlY29yYXRlKFtcbiAgICAgICAgICAgIGNvcmVfMS5JbmplY3RhYmxlKCksIFxuICAgICAgICAgICAgX19tZXRhZGF0YSgnZGVzaWduOnBhcmFtdHlwZXMnLCBbY29yZV8yLk5nWm9uZV0pXG4gICAgICAgIF0sIFBpY3R1cmVzU2VydmljZSk7XG4gICAgICAgIHJldHVybiBQaWN0dXJlc1NlcnZpY2U7XG4gICAgfSkoKTtcbiAgICBleHBvcnRzLlBpY3R1cmVzU2VydmljZSA9IFBpY3R1cmVzU2VydmljZTtcbn0pO1xuIl0sImZpbGUiOiJhcHAvc2hvdy9waWN0dXJlcy5zZXJ2aWNlLmpzIiwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=
