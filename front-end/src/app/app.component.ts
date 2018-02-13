import { Component, OnInit, Injector } from '@angular/core';
import { Observable } from 'rxjs/Observable';

export let AppInjector: Injector;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    constructor(injector: Injector) {
        AppInjector = injector;
    }
}
