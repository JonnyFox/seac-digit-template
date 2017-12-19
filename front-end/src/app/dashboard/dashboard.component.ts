import { Component, OnInit } from '@angular/core';
import { DocumentoService } from '../shared/documento.service';
import {
    Documento,
    DocumentoCaratteristicaEnum,
    DocumentoSospensioneEnum,
    DocumentoTipoEnum,
    RegistroTipoEnum,
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
    public newDocument : Documento;

    private _Documento$: Subject<Documento[]> = new Subject();
    public Documento$: Observable<Documento[]> = this._Documento$.asObservable();

    private _documentoEffetti$: Subject<Documento[]> = new Subject();
    public documentoEffetti$: Observable<Documento[]> = this._documentoEffetti$.asObservable();


    public displayedColumns = ['id', 'numero', 'protocollo', 'tipo', 'caratteristica', 'sospeso', 'registro', 'totale', 'action'];

    public dataSource: ExampleDataSource | null;
    public prova:String;

    constructor(
        private documentService: DocumentoService,
        private router: Router
    ) { 
        this.Documento$ = this.documentService.getAll()
        this.documentoList = new Array<Documento>();
    }

    ngOnInit() {
        
        this.dataSource = new ExampleDataSource(this.Documento$);
        
    }

    public editDocument(id: number) {
        this.router.navigate(['/document', id]);
    }
    public populate(){
        this.Documento$.subscribe(x => this.documentoList = x);
    }
    public addDocument(){
        this.newDocument = new Documento;
        this.Documento$.subscribe(x => this.documentoList = x);
        this.newDocument=this.documentoList[0];
        this.documentoList.push(this.newDocument);
        this._Documento$.next(this.documentoList);
    }
    public clear (){
        this._Documento$.next(null);
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

