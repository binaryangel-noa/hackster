var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", 'angular2/core', 'angular2/router', './import/import', './show/show'], function (require, exports, core_1, router_1, import_1, show_1) {
    var App = (function () {
        function App(_router) {
            this._router = _router;
            this.skills = ['ASP.NET Core 1.0', 'Angular 2', 'C#', 'SQL'];
            this.title = 'PImage Frame';
            this.mySkill = this.skills[1];
        }
        App = __decorate([
            core_1.Component({
                selector: 'app',
                directives: [router_1.ROUTER_DIRECTIVES],
                templateUrl: '/app/app.html' }),
            router_1.RouteConfig([
                { path: '/', name: 'Show', component: show_1.Show },
                { path: '/default.html', redirectTo: ['Show'] },
                { path: '/import', name: 'Import', component: import_1.Import }
            ]), 
            __metadata('design:paramtypes', [router_1.Router])
        ], App);
        return App;
    })();
    exports.App = App;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiIiwic291cmNlcyI6WyJhcHAvYXBwLmpzIl0sInNvdXJjZXNDb250ZW50IjpbInZhciBfX2RlY29yYXRlID0gKHRoaXMgJiYgdGhpcy5fX2RlY29yYXRlKSB8fCBmdW5jdGlvbiAoZGVjb3JhdG9ycywgdGFyZ2V0LCBrZXksIGRlc2MpIHtcbiAgICB2YXIgYyA9IGFyZ3VtZW50cy5sZW5ndGgsIHIgPSBjIDwgMyA/IHRhcmdldCA6IGRlc2MgPT09IG51bGwgPyBkZXNjID0gT2JqZWN0LmdldE93blByb3BlcnR5RGVzY3JpcHRvcih0YXJnZXQsIGtleSkgOiBkZXNjLCBkO1xuICAgIGlmICh0eXBlb2YgUmVmbGVjdCA9PT0gXCJvYmplY3RcIiAmJiB0eXBlb2YgUmVmbGVjdC5kZWNvcmF0ZSA9PT0gXCJmdW5jdGlvblwiKSByID0gUmVmbGVjdC5kZWNvcmF0ZShkZWNvcmF0b3JzLCB0YXJnZXQsIGtleSwgZGVzYyk7XG4gICAgZWxzZSBmb3IgKHZhciBpID0gZGVjb3JhdG9ycy5sZW5ndGggLSAxOyBpID49IDA7IGktLSkgaWYgKGQgPSBkZWNvcmF0b3JzW2ldKSByID0gKGMgPCAzID8gZChyKSA6IGMgPiAzID8gZCh0YXJnZXQsIGtleSwgcikgOiBkKHRhcmdldCwga2V5KSkgfHwgcjtcbiAgICByZXR1cm4gYyA+IDMgJiYgciAmJiBPYmplY3QuZGVmaW5lUHJvcGVydHkodGFyZ2V0LCBrZXksIHIpLCByO1xufTtcbnZhciBfX21ldGFkYXRhID0gKHRoaXMgJiYgdGhpcy5fX21ldGFkYXRhKSB8fCBmdW5jdGlvbiAoaywgdikge1xuICAgIGlmICh0eXBlb2YgUmVmbGVjdCA9PT0gXCJvYmplY3RcIiAmJiB0eXBlb2YgUmVmbGVjdC5tZXRhZGF0YSA9PT0gXCJmdW5jdGlvblwiKSByZXR1cm4gUmVmbGVjdC5tZXRhZGF0YShrLCB2KTtcbn07XG5kZWZpbmUoW1wicmVxdWlyZVwiLCBcImV4cG9ydHNcIiwgJ2FuZ3VsYXIyL2NvcmUnLCAnYW5ndWxhcjIvcm91dGVyJywgJy4vaW1wb3J0L2ltcG9ydCcsICcuL3Nob3cvc2hvdyddLCBmdW5jdGlvbiAocmVxdWlyZSwgZXhwb3J0cywgY29yZV8xLCByb3V0ZXJfMSwgaW1wb3J0XzEsIHNob3dfMSkge1xuICAgIHZhciBBcHAgPSAoZnVuY3Rpb24gKCkge1xuICAgICAgICBmdW5jdGlvbiBBcHAoX3JvdXRlcikge1xuICAgICAgICAgICAgdGhpcy5fcm91dGVyID0gX3JvdXRlcjtcbiAgICAgICAgICAgIHRoaXMuc2tpbGxzID0gWydBU1AuTkVUIENvcmUgMS4wJywgJ0FuZ3VsYXIgMicsICdDIycsICdTUUwnXTtcbiAgICAgICAgICAgIHRoaXMudGl0bGUgPSAnUEltYWdlIEZyYW1lJztcbiAgICAgICAgICAgIHRoaXMubXlTa2lsbCA9IHRoaXMuc2tpbGxzWzFdO1xuICAgICAgICB9XG4gICAgICAgIEFwcCA9IF9fZGVjb3JhdGUoW1xuICAgICAgICAgICAgY29yZV8xLkNvbXBvbmVudCh7XG4gICAgICAgICAgICAgICAgc2VsZWN0b3I6ICdhcHAnLFxuICAgICAgICAgICAgICAgIGRpcmVjdGl2ZXM6IFtyb3V0ZXJfMS5ST1VURVJfRElSRUNUSVZFU10sXG4gICAgICAgICAgICAgICAgdGVtcGxhdGVVcmw6ICcvYXBwL2FwcC5odG1sJyB9KSxcbiAgICAgICAgICAgIHJvdXRlcl8xLlJvdXRlQ29uZmlnKFtcbiAgICAgICAgICAgICAgICB7IHBhdGg6ICcvJywgbmFtZTogJ1Nob3cnLCBjb21wb25lbnQ6IHNob3dfMS5TaG93IH0sXG4gICAgICAgICAgICAgICAgeyBwYXRoOiAnL2RlZmF1bHQuaHRtbCcsIHJlZGlyZWN0VG86IFsnU2hvdyddIH0sXG4gICAgICAgICAgICAgICAgeyBwYXRoOiAnL2ltcG9ydCcsIG5hbWU6ICdJbXBvcnQnLCBjb21wb25lbnQ6IGltcG9ydF8xLkltcG9ydCB9XG4gICAgICAgICAgICBdKSwgXG4gICAgICAgICAgICBfX21ldGFkYXRhKCdkZXNpZ246cGFyYW10eXBlcycsIFtyb3V0ZXJfMS5Sb3V0ZXJdKVxuICAgICAgICBdLCBBcHApO1xuICAgICAgICByZXR1cm4gQXBwO1xuICAgIH0pKCk7XG4gICAgZXhwb3J0cy5BcHAgPSBBcHA7XG59KTtcbiJdLCJmaWxlIjoiYXBwL2FwcC5qcyIsInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9
