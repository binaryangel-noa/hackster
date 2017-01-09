import {Injectable} from 'angular2/core';
import {Observable} from 'rxjs/Observable';
import {Observer} from 'rxjs/Observer';
import 'rxjs/Rx';
import {NgZone} from 'angular2/core';

declare var BllRT;

@Injectable()
export class ImportService {
    images$: Observable<string[]>;
    private _observer: Observer<string[]>;

    constructor(private zone: NgZone) {
        this.images$ = new Observable(observer => this._observer = observer).share();
    }

    load() {
        var bll = new BllRT.SdOperationsBridge();
        var asyncoperation = bll.listFiles();
        
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

    copy() {
        var bll = new BllRT.SdOperationsBridge();
        var asyncoperation = bll.copyFiles();
        var subscriber: Observer<boolean>;
        var obs = new Observable<boolean>(sb => subscriber = sb);

        asyncoperation.done((res) => {
            this.zone.run(() => {
                subscriber.next(res);
            });
        })

        return obs;
    }
}