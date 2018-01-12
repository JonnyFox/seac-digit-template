import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { DataSource } from '@angular/cdk/collections';
import { Feedback } from '../shared/models';
import { FeedbackService } from '../shared/feedback.service';
import { RouterPassCheckService } from '../shared/router-pass-check.service';
import { FeedbackDialogComponent } from '../feedback-dialog/feedback-dialog.component';
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

    public displayedColumns = ['descrizione', 'descrizioneDoc', 'action'];

    constructor(private feedbackservice: FeedbackService,
                private dialog: MatDialog,
                public routerPassCheckService: RouterPassCheckService,
                private router: Router
                ) {
        this.feedbackservice.getAll().subscribe(x => this._Feedback$.next(x));
        this.feedbackList = new Array<Feedback>();
    }

    ngOnInit() {
        this.dataSource = new ExampleDataSource(this.Feedback$);
    }

    public populate() {
        this.Feedback$.subscribe(x => this.feedbackList = x);
    }
    public openDialog(text: string): void {
        const dialogRef = this.dialog.open(FeedbackDialogComponent, {
            width: '800px',
            data: { description : text}
        });
    }
    public goToDocument(effetto: string, idDoc: number) {
        this.routerPassCheckService.setEffect(effetto);
        this.router.navigate(['/document', idDoc]);
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
