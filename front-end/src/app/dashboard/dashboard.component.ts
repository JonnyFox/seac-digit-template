import { Component, OnInit } from '@angular/core';
import { DocumentoService } from '../shared/documento.service';
import { FeedbackService } from '../shared/feedback.service';
import {
    Documento,
    DocumentoCaratteristicaEnum,
    DocumentoSospensioneEnum,
    DocumentoTipoEnum,
    RegistroTipoEnum,
    Clifor,
    RigaDigitata,
} from '../shared/models';
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subject } from 'rxjs/Subject';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

    public caratteristica = DocumentoCaratteristicaEnum;
    public tipo = DocumentoTipoEnum;
    public sospeso = DocumentoSospensioneEnum;
    public registro = RegistroTipoEnum;
    public documentoList: Documento[];
    public newDocument: Documento;

    private _isValidDocument$: BehaviorSubject<boolean> = new BehaviorSubject(true);
    public isValidDocument$: Observable<boolean> = this._isValidDocument$.asObservable();

    private _Documento$: BehaviorSubject<Documento[]> = new BehaviorSubject(new Array<Documento>());
    public Documento$: Observable<Documento[]> = this._Documento$.asObservable();

    private _documentoEffetti$: Subject<Documento[]> = new Subject();
    public documentoEffetti$: Observable<Documento[]> = this._documentoEffetti$.asObservable();


    public displayedColumns = ['numero', 'protocollo', 'tipo', 'caratteristica', 'sospeso',
        'registro', 'totale', 'action', 'descrizione'];

    public dataSource: ExampleDataSource | null;
    public prova: String;

    constructor(
        private documentService: DocumentoService,
        private router: Router,
        private feedbackService: FeedbackService,
    ) {
        this.documentService.getAll().subscribe(x => this._Documento$.next(x));
        this.documentoList = new Array<Documento>();
    }

    ngOnInit() {
        this.dataSource = new ExampleDataSource(this.Documento$);
    }

    public editDocument(id: number) {
        this.router.navigate(['/document', id]);
    }

    public deleteDocument(id: number) {
        this.documentService.delete(id);
    }

    public populate() {
        this.Documento$.subscribe(x => this.documentoList = x);
    }

    public addDocument() {
        this.router.navigateByUrl('/document/0');
    }

}

export class ExampleDataSource extends DataSource<any> {

    constructor(private Documento$: Observable<Documento[]>) {
        super();
    }

    connect(): Observable<Documento[]> {
        return this.Documento$;
    }

    disconnect() { }
}
