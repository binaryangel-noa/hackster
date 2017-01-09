import {bootstrap} from 'angular2/platform/browser'
import { ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy } from 'angular2/router';
import {App} from './app'

bootstrap(App, [ROUTER_PROVIDERS]);