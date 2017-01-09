import { Component } from 'angular2/core';
import { Router, RouteConfig, ROUTER_DIRECTIVES  } from 'angular2/router';
import { Import } from './import/import';
import { Show } from './show/show';

@Component({
    selector: 'app',
    directives: [ROUTER_DIRECTIVES],
    templateUrl: '/app/app.html' })

@RouteConfig([
    { path: '/', name: 'Show', component: Show },
    { path: '/default.html', redirectTo: ['Show'] },
    { path: '/import', name: 'Import', component: Import }
])

export class App {
    title: string;
    mySkill: string;
    skills = ['ASP.NET Core 1.0', 'Angular 2', 'C#', 'SQL'];
    constructor(private _router: Router) {
        this.title = 'PImage Frame';
        this.mySkill = this.skills[1];
        
    }
}