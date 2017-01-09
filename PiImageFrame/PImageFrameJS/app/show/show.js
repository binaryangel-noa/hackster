var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", 'angular2/core', './pictures.service', 'angular2/common', 'ng2-bootstrap/ng2-bootstrap'], function (require, exports, core_1, pictures_service_1, common_1, ng2_bootstrap_1) {
    var Show = (function () {
        function Show(_service) {
            this._service = _service;
            this.myInterval = 30000;
            this.noWrapSlides = true;
            this.slides = [];
            this.temperature = null;
            this.lightLevel = null;
            console.log("Show.ctor");
        }
        Show.prototype.ngOnInit = function () {
            var _this = this;
            this._service.images$.subscribe(function (updated) {
                _this.images = updated;
                for (var i = 0; i < updated.length; i++) {
                    var item = updated[i];
                    _this.slides.push({
                        image: item,
                        text: "test",
                        index: i
                    });
                }
                _this.nextSlide();
            });
            this._service.load();
        };
        Show.prototype.nextSlide = function () {
            var _this = this;
            if (this.slides && this.slides.length > 0) {
                if (!this.currentSlide) {
                    this.currentSlide = this.slides[0];
                }
                else {
                    var currentIndex = this.currentSlide.index;
                    for (var i = 0; i < this.slides.length; i++) {
                        var citem = this.slides[i];
                        if (citem.index === (currentIndex + 1)) {
                            this.currentSlide = citem;
                            break;
                        }
                    }
                    if (currentIndex === this.currentSlide.index) {
                        //immer noch der gleiche item
                        this.currentSlide = this.slides[0];
                    }
                }
                this.backgroundImage = "url(\"" + this.currentSlide.image + "\")";
            }
            var sensorData = this._service.pollData();
            if (sensorData) {
                this.lightLevel = sensorData.lightLevel;
                this.temperature = sensorData.temperature;
            }
            else {
                this.lightLevel = null;
                this.temperature = null;
            }
            setTimeout(function () { return _this.nextSlide(); }, this.myInterval);
        };
        Show = __decorate([
            core_1.Component({
                selector: 'show',
                templateUrl: '/app/show/show.html',
                directives: [ng2_bootstrap_1.CAROUSEL_DIRECTIVES, common_1.CORE_DIRECTIVES, common_1.FORM_DIRECTIVES],
                providers: [pictures_service_1.PicturesService]
            }), 
            __metadata('design:paramtypes', [pictures_service_1.PicturesService])
        ], Show);
        return Show;
    })();
    exports.Show = Show;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiIiwic291cmNlcyI6WyJhcHAvc2hvdy9zaG93LmpzIl0sInNvdXJjZXNDb250ZW50IjpbInZhciBfX2RlY29yYXRlID0gKHRoaXMgJiYgdGhpcy5fX2RlY29yYXRlKSB8fCBmdW5jdGlvbiAoZGVjb3JhdG9ycywgdGFyZ2V0LCBrZXksIGRlc2MpIHtcbiAgICB2YXIgYyA9IGFyZ3VtZW50cy5sZW5ndGgsIHIgPSBjIDwgMyA/IHRhcmdldCA6IGRlc2MgPT09IG51bGwgPyBkZXNjID0gT2JqZWN0LmdldE93blByb3BlcnR5RGVzY3JpcHRvcih0YXJnZXQsIGtleSkgOiBkZXNjLCBkO1xuICAgIGlmICh0eXBlb2YgUmVmbGVjdCA9PT0gXCJvYmplY3RcIiAmJiB0eXBlb2YgUmVmbGVjdC5kZWNvcmF0ZSA9PT0gXCJmdW5jdGlvblwiKSByID0gUmVmbGVjdC5kZWNvcmF0ZShkZWNvcmF0b3JzLCB0YXJnZXQsIGtleSwgZGVzYyk7XG4gICAgZWxzZSBmb3IgKHZhciBpID0gZGVjb3JhdG9ycy5sZW5ndGggLSAxOyBpID49IDA7IGktLSkgaWYgKGQgPSBkZWNvcmF0b3JzW2ldKSByID0gKGMgPCAzID8gZChyKSA6IGMgPiAzID8gZCh0YXJnZXQsIGtleSwgcikgOiBkKHRhcmdldCwga2V5KSkgfHwgcjtcbiAgICByZXR1cm4gYyA+IDMgJiYgciAmJiBPYmplY3QuZGVmaW5lUHJvcGVydHkodGFyZ2V0LCBrZXksIHIpLCByO1xufTtcbnZhciBfX21ldGFkYXRhID0gKHRoaXMgJiYgdGhpcy5fX21ldGFkYXRhKSB8fCBmdW5jdGlvbiAoaywgdikge1xuICAgIGlmICh0eXBlb2YgUmVmbGVjdCA9PT0gXCJvYmplY3RcIiAmJiB0eXBlb2YgUmVmbGVjdC5tZXRhZGF0YSA9PT0gXCJmdW5jdGlvblwiKSByZXR1cm4gUmVmbGVjdC5tZXRhZGF0YShrLCB2KTtcbn07XG5kZWZpbmUoW1wicmVxdWlyZVwiLCBcImV4cG9ydHNcIiwgJ2FuZ3VsYXIyL2NvcmUnLCAnLi9waWN0dXJlcy5zZXJ2aWNlJywgJ2FuZ3VsYXIyL2NvbW1vbicsICduZzItYm9vdHN0cmFwL25nMi1ib290c3RyYXAnXSwgZnVuY3Rpb24gKHJlcXVpcmUsIGV4cG9ydHMsIGNvcmVfMSwgcGljdHVyZXNfc2VydmljZV8xLCBjb21tb25fMSwgbmcyX2Jvb3RzdHJhcF8xKSB7XG4gICAgdmFyIFNob3cgPSAoZnVuY3Rpb24gKCkge1xuICAgICAgICBmdW5jdGlvbiBTaG93KF9zZXJ2aWNlKSB7XG4gICAgICAgICAgICB0aGlzLl9zZXJ2aWNlID0gX3NlcnZpY2U7XG4gICAgICAgICAgICB0aGlzLm15SW50ZXJ2YWwgPSAzMDAwMDtcbiAgICAgICAgICAgIHRoaXMubm9XcmFwU2xpZGVzID0gdHJ1ZTtcbiAgICAgICAgICAgIHRoaXMuc2xpZGVzID0gW107XG4gICAgICAgICAgICB0aGlzLnRlbXBlcmF0dXJlID0gbnVsbDtcbiAgICAgICAgICAgIHRoaXMubGlnaHRMZXZlbCA9IG51bGw7XG4gICAgICAgICAgICBjb25zb2xlLmxvZyhcIlNob3cuY3RvclwiKTtcbiAgICAgICAgfVxuICAgICAgICBTaG93LnByb3RvdHlwZS5uZ09uSW5pdCA9IGZ1bmN0aW9uICgpIHtcbiAgICAgICAgICAgIHZhciBfdGhpcyA9IHRoaXM7XG4gICAgICAgICAgICB0aGlzLl9zZXJ2aWNlLmltYWdlcyQuc3Vic2NyaWJlKGZ1bmN0aW9uICh1cGRhdGVkKSB7XG4gICAgICAgICAgICAgICAgX3RoaXMuaW1hZ2VzID0gdXBkYXRlZDtcbiAgICAgICAgICAgICAgICBmb3IgKHZhciBpID0gMDsgaSA8IHVwZGF0ZWQubGVuZ3RoOyBpKyspIHtcbiAgICAgICAgICAgICAgICAgICAgdmFyIGl0ZW0gPSB1cGRhdGVkW2ldO1xuICAgICAgICAgICAgICAgICAgICBfdGhpcy5zbGlkZXMucHVzaCh7XG4gICAgICAgICAgICAgICAgICAgICAgICBpbWFnZTogaXRlbSxcbiAgICAgICAgICAgICAgICAgICAgICAgIHRleHQ6IFwidGVzdFwiLFxuICAgICAgICAgICAgICAgICAgICAgICAgaW5kZXg6IGlcbiAgICAgICAgICAgICAgICAgICAgfSk7XG4gICAgICAgICAgICAgICAgfVxuICAgICAgICAgICAgICAgIF90aGlzLm5leHRTbGlkZSgpO1xuICAgICAgICAgICAgfSk7XG4gICAgICAgICAgICB0aGlzLl9zZXJ2aWNlLmxvYWQoKTtcbiAgICAgICAgfTtcbiAgICAgICAgU2hvdy5wcm90b3R5cGUubmV4dFNsaWRlID0gZnVuY3Rpb24gKCkge1xuICAgICAgICAgICAgdmFyIF90aGlzID0gdGhpcztcbiAgICAgICAgICAgIGlmICh0aGlzLnNsaWRlcyAmJiB0aGlzLnNsaWRlcy5sZW5ndGggPiAwKSB7XG4gICAgICAgICAgICAgICAgaWYgKCF0aGlzLmN1cnJlbnRTbGlkZSkge1xuICAgICAgICAgICAgICAgICAgICB0aGlzLmN1cnJlbnRTbGlkZSA9IHRoaXMuc2xpZGVzWzBdO1xuICAgICAgICAgICAgICAgIH1cbiAgICAgICAgICAgICAgICBlbHNlIHtcbiAgICAgICAgICAgICAgICAgICAgdmFyIGN1cnJlbnRJbmRleCA9IHRoaXMuY3VycmVudFNsaWRlLmluZGV4O1xuICAgICAgICAgICAgICAgICAgICBmb3IgKHZhciBpID0gMDsgaSA8IHRoaXMuc2xpZGVzLmxlbmd0aDsgaSsrKSB7XG4gICAgICAgICAgICAgICAgICAgICAgICB2YXIgY2l0ZW0gPSB0aGlzLnNsaWRlc1tpXTtcbiAgICAgICAgICAgICAgICAgICAgICAgIGlmIChjaXRlbS5pbmRleCA9PT0gKGN1cnJlbnRJbmRleCArIDEpKSB7XG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgdGhpcy5jdXJyZW50U2xpZGUgPSBjaXRlbTtcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICBicmVhaztcbiAgICAgICAgICAgICAgICAgICAgICAgIH1cbiAgICAgICAgICAgICAgICAgICAgfVxuICAgICAgICAgICAgICAgICAgICBpZiAoY3VycmVudEluZGV4ID09PSB0aGlzLmN1cnJlbnRTbGlkZS5pbmRleCkge1xuICAgICAgICAgICAgICAgICAgICAgICAgLy9pbW1lciBub2NoIGRlciBnbGVpY2hlIGl0ZW1cbiAgICAgICAgICAgICAgICAgICAgICAgIHRoaXMuY3VycmVudFNsaWRlID0gdGhpcy5zbGlkZXNbMF07XG4gICAgICAgICAgICAgICAgICAgIH1cbiAgICAgICAgICAgICAgICB9XG4gICAgICAgICAgICAgICAgdGhpcy5iYWNrZ3JvdW5kSW1hZ2UgPSBcInVybChcXFwiXCIgKyB0aGlzLmN1cnJlbnRTbGlkZS5pbWFnZSArIFwiXFxcIilcIjtcbiAgICAgICAgICAgIH1cbiAgICAgICAgICAgIHZhciBzZW5zb3JEYXRhID0gdGhpcy5fc2VydmljZS5wb2xsRGF0YSgpO1xuICAgICAgICAgICAgaWYgKHNlbnNvckRhdGEpIHtcbiAgICAgICAgICAgICAgICB0aGlzLmxpZ2h0TGV2ZWwgPSBzZW5zb3JEYXRhLmxpZ2h0TGV2ZWw7XG4gICAgICAgICAgICAgICAgdGhpcy50ZW1wZXJhdHVyZSA9IHNlbnNvckRhdGEudGVtcGVyYXR1cmU7XG4gICAgICAgICAgICB9XG4gICAgICAgICAgICBlbHNlIHtcbiAgICAgICAgICAgICAgICB0aGlzLmxpZ2h0TGV2ZWwgPSBudWxsO1xuICAgICAgICAgICAgICAgIHRoaXMudGVtcGVyYXR1cmUgPSBudWxsO1xuICAgICAgICAgICAgfVxuICAgICAgICAgICAgc2V0VGltZW91dChmdW5jdGlvbiAoKSB7IHJldHVybiBfdGhpcy5uZXh0U2xpZGUoKTsgfSwgdGhpcy5teUludGVydmFsKTtcbiAgICAgICAgfTtcbiAgICAgICAgU2hvdyA9IF9fZGVjb3JhdGUoW1xuICAgICAgICAgICAgY29yZV8xLkNvbXBvbmVudCh7XG4gICAgICAgICAgICAgICAgc2VsZWN0b3I6ICdzaG93JyxcbiAgICAgICAgICAgICAgICB0ZW1wbGF0ZVVybDogJy9hcHAvc2hvdy9zaG93Lmh0bWwnLFxuICAgICAgICAgICAgICAgIGRpcmVjdGl2ZXM6IFtuZzJfYm9vdHN0cmFwXzEuQ0FST1VTRUxfRElSRUNUSVZFUywgY29tbW9uXzEuQ09SRV9ESVJFQ1RJVkVTLCBjb21tb25fMS5GT1JNX0RJUkVDVElWRVNdLFxuICAgICAgICAgICAgICAgIHByb3ZpZGVyczogW3BpY3R1cmVzX3NlcnZpY2VfMS5QaWN0dXJlc1NlcnZpY2VdXG4gICAgICAgICAgICB9KSwgXG4gICAgICAgICAgICBfX21ldGFkYXRhKCdkZXNpZ246cGFyYW10eXBlcycsIFtwaWN0dXJlc19zZXJ2aWNlXzEuUGljdHVyZXNTZXJ2aWNlXSlcbiAgICAgICAgXSwgU2hvdyk7XG4gICAgICAgIHJldHVybiBTaG93O1xuICAgIH0pKCk7XG4gICAgZXhwb3J0cy5TaG93ID0gU2hvdztcbn0pO1xuIl0sImZpbGUiOiJhcHAvc2hvdy9zaG93LmpzIiwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=
