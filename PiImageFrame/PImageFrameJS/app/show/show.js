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
    "use strict";
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
    }());
    exports.Show = Show;
});
//# sourceMappingURL=show.js.map