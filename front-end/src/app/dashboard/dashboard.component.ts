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

    public displayedColumns = ['id', 'numero', 'protocollo', 'tipo', 'caratteristica', 'sospeso', 'registro', 'totale', 'action'];

    public dataSource: ExampleDataSource | null;

    constructor(
        private documentService: DocumentoService,
        private router: Router
    ) { }

    ngOnInit() {
        this.dataSource = new ExampleDataSource(this.documentService);
    }

    public editDocument(id: number) {
        this.router.navigate(['/document', id]);
    }

}

export class ExampleDataSource extends DataSource<any> {

    constructor(private documentService: DocumentoService) {
        super();
    }

    connect(): Observable<Documento[]> {
        return this.documentService.getAll();
    }

    disconnect() { }
}

