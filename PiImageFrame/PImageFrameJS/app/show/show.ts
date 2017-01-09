import { Component, OnInit } from 'angular2/core';
import { PicturesService } from './pictures.service';
import { CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import { CAROUSEL_DIRECTIVES } from 'ng2-bootstrap/ng2-bootstrap';

@Component({
    selector: 'show',
    templateUrl: '/app/show/show.html',
    directives: [CAROUSEL_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES],
    providers: [PicturesService]
})

export class Show {
    images: string[];
    myInterval: number = 30000;
    noWrapSlides: boolean = true;
    slides: Array<any> = [];
    backgroundImage: string;
    currentSlide: any;
    temperature: number = null;
    lightLevel: number = null;

    constructor(private _service: PicturesService) {
        console.log("Show.ctor")
    }

    ngOnInit() {
        this._service.images$.subscribe(updated => {
            this.images = updated;
            for (var i = 0; i < updated.length; i++) {
                var item = updated[i];                
                this.slides.push({
                    image: item,
                    text: "test",
                    index: i
                })
            }
            this.nextSlide();
        });
        this._service.load();
    }

    nextSlide() {
        if (this.slides && this.slides.length > 0) {
            if (!this.currentSlide) {
                this.currentSlide = this.slides[0];
            } else {
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
            this.backgroundImage = `url("` + this.currentSlide.image + `")`
        }

        var sensorData = this._service.pollData()
        if (sensorData) {
            this.lightLevel = sensorData.lightLevel;
            this.temperature = sensorData.temperature;
        } else {
            this.lightLevel = null;
            this.temperature = null;
        }
        setTimeout(() => this.nextSlide(), this.myInterval);
    }
}