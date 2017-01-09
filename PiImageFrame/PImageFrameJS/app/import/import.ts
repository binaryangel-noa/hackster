import {Component, OnInit} from 'angular2/core';
import {ImportService} from './import.service';
import {ArrayObservable} from 'rxjs/observable/ArrayObservable';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/fromArray';


@Component({
    selector: 'import',
    templateUrl: '/app/import/import.html',
    providers: [ImportService]
})

export class Import {
    images: string[]; 
    
    constructor(private _service: ImportService) {
        console.log("Import.ctor")        
    }

    copy() {
        this._service.copy().subscribe(updated => {
            var p = updated;
        });
    }

    ngOnInit() {
        this._service.images$.subscribe(updated => {
            this.images = updated;
        });
        this._service.load();
    }
}