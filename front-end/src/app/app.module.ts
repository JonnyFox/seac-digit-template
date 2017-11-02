import { HttpModule } from '@angular/http';
import { TitoloInapplicabilitaListResolver } from './shared/titolo-inapplicabilita-list.resolver';
import { TitoloInapplicabilitaService } from './shared/titolo-inapplicabilita.service';
import { AliquotaIvaService } from './shared/aliquota-iva.service';
import { ContoService } from './shared/conto.service';
import { VoceIvaService } from './shared/voce-iva.service';
import { ContoListResolver } from './shared/conto-list.resolver';
import { VoceIvaListResolver } from './shared/voce-iva-list.resolver';
import { EffettoCalcoloService } from './shared/effetto-calcolo.service';
import { RigaDigitataService } from './shared/riga-digitata.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Http } from '@angular/http';
import { MatListModule, MatTableModule, MatButtonModule, MatIconModule, MatInputModule, MatSelectModule, MatCardModule } from '@angular/material';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DocumentComponent } from './document/document.component';
import { DocumentoService } from './shared/documento.service';
import { AliquotaIvaListResolver } from './shared/aliquota-iva-list.resolver';


@NgModule({
    declarations: [
        AppComponent,
        DashboardComponent,
        DocumentComponent
    ],
    imports: [
        FormsModule,
        HttpClientModule,
        HttpModule,
        BrowserModule,
        BrowserAnimationsModule,
        MatIconModule,
        MatCardModule,
        MatInputModule,
        MatSelectModule,
        MatButtonModule,
        MatTableModule,
        AppRoutingModule
    ],
    providers: [
        DocumentoService,
        RigaDigitataService,
        EffettoCalcoloService,
        AliquotaIvaService,
        ContoService,
        TitoloInapplicabilitaService,
        VoceIvaService,
        AliquotaIvaListResolver,
        VoceIvaService,
        ContoListResolver,
        TitoloInapplicabilitaListResolver,
        VoceIvaListResolver
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
