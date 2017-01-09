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
    "use strict";
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
    }());
    exports.App = App;
});
//# sourceMappingURL=app.js.map