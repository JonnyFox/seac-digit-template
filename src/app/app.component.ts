import { DatabaseLists } from './shared/enums';
import { Documento } from './shared/models';
import { Component } from '@angular/core';
import { AngularFirestore } from 'angularfire2/firestore';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    public documents: Observable<Documento[]>;

    constructor(private db: AngularFirestore) {
        this.documents = db.collection<Documento>('documents').valueChanges();
    }
}
