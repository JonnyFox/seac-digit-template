import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injector } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {
    MatListModule, MatTableModule, MatButtonModule,
    MatIconModule, MatInputModule, MatSelectModule, MatCardModule, MatDialogModule
} from '@angular/material';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DocumentComponent, DialogOverviewExampleDialogComponent } from './document/document.component';
import { DocumentoService } from './shared/documento.service';
import { AliquotaIvaListResolver } from './shared/aliquota-iva-list.resolver';
import { TitoloInapplicabilitaListResolver } from './shared/titolo-inapplicabilita-list.resolver';
import { TitoloInapplicabilitaService } from './shared/titolo-inapplicabilita.service';
import { AliquotaIvaService } from './shared/aliquota-iva.service';
import { ContoService } from './shared/conto.service';
import { VoceIvaService } from './shared/voce-iva.service';
import { ContoListResolver } from './shared/conto-list.resolver';
import { VoceIvaListResolver } from './shared/voce-iva-list.resolver';
import { EffettoService } from './shared/effetto.service';
import { RigaDigitataService } from './shared/riga-digitata.service';
import { NotificationService } from './shared/notification.service';


@NgModule({
    declarations: [
        AppComponent,
        DashboardComponent,
        DocumentComponent,
        DialogOverviewExampleDialogComponent
    ],
    imports: [
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        BrowserModule,
        BrowserAnimationsModule,
        MatIconModule,
        MatCardModule,
        MatInputModule,
        MatDialogModule,
        MatSelectModule,
        MatButtonModule,
        MatTableModule,
        AppRoutingModule,
        ToastrModule.forRoot()
    ],
    providers: [
        DocumentoService,
        RigaDigitataService,
        EffettoService,
        AliquotaIvaService,
        ContoService,
        TitoloInapplicabilitaService,
        VoceIvaService,
        NotificationService,
        AliquotaIvaListResolver,
        VoceIvaService,
        ContoListResolver,
        TitoloInapplicabilitaListResolver,
        VoceIvaListResolver,
    ],
    bootstrap: [AppComponent],
    entryComponents: [DialogOverviewExampleDialogComponent]
})
export class AppModule { }
