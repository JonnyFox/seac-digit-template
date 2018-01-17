import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { DataSource } from '@angular/cdk/collections';
import { Feedback } from '../shared/models';
import { FeedbackService } from '../shared/feedback.service';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';


@Component({
    selector: 'app-feedback',
    templateUrl: './feedback.component.html',
    styleUrls: ['./feedback.component.scss']
})
export class FeedbackComponent implements OnInit {

    public feedbackList: Feedback[];
    public dataSource: ExampleDataSource | null;

    private _Feedback$: BehaviorSubject<Feedback[]> = new BehaviorSubject(new Array<Feedback>());
    public Feedback$: Observable<Feedback[]> = this._Feedback$.asObservable();

    public displayedColumns = ['documentoId', 'documentoDescrizione', 'descrizione',  'action'];

    constructor(private feedbackservice: FeedbackService, private router: Router) {
        this.feedbackservice.getAll().subscribe(x => this._Feedback$.next(x));
        this.feedbackList = new Array<Feedback>();
    }
    ngOnInit() {
        this.dataSource = new ExampleDataSource(this.Feedback$);
    }
    public populate() {
        this.Feedback$.subscribe(x => this.feedbackList = x);
    }
    public goToDocument(element: Feedback) {
        this.router.navigate(['/feedback', element.id]);
    }

}
export class ExampleDataSource extends DataSource<any> {

    constructor(private Feedback$: Observable<Feedback[]>) {
        super();
    }

    connect(): Observable<Feedback[]> {
        return this.Feedback$;
    }

    disconnect() { }
}
