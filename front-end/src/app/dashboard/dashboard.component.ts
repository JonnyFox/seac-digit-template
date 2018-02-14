import { Component, OnInit } from '@angular/core';
import { DocumentoService } from '../shared/documento.service';
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

    private _documento$: BehaviorSubject<Documento[]> = new BehaviorSubject(new Array<Documento>());
    public documento$: Observable<Documento[]> = this._documento$.asObservable();

    public displayedColumns = ['numero', 'protocollo', 'tipo', 'caratteristica', 'sospeso',
        'registro', 'totale', 'action', 'descrizione'];

    public dataSource: ExampleDataSource | null;

    constructor(
        private documentService: DocumentoService,
        private router: Router,
    ) {
        this.documentService.getAll().subscribe(x => this._documento$.next(x));
    }

    ngOnInit() {
        this.dataSource = new ExampleDataSource(this.documento$);
    }

    public editDocument(id: number) {
        this.router.navigate(['/document', id]);
    }

    public deleteDocument(id: number) {
        this.documentService.delete(id).subscribe( any => {
            this.documentService.getAll().subscribe(x => this._documento$.next(x));
        });
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
