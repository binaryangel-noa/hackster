import {Injectable} from 'angular2/core';
import {Observable} from 'rxjs/Observable';
import {Observer} from 'rxjs/Observer';
import 'rxjs/Rx';
import {NgZone} from 'angular2/core';

declare var BllRT;

@Injectable()
export class PicturesService {
    images$: Observable<string[]>;
    private _observer: Observer<string[]>;
    private _sensorDataServicePollBridge : any;

    constructor(private zone: NgZone) {
        this.images$ = new Observable(observer => this._observer = observer).share();
        this._sensorDataServicePollBridge = new BllRT.SensorDataServicePollBridge();
    }

    pollData(): any {
            return this._sensorDataServicePollBridge.getSensorData();
    }

    load() {
        var bll = new BllRT.SdOperationsBridge();
        var asyncoperation = bll.listCopiedFiles();
        
        asyncoperation.done((data) => {
            this.zone.run(() => {
                if (data.length > 0) {
                    this._observer.next(data);
                } else {
                    this._observer.next([]);
                }
            });
        })
    }
}