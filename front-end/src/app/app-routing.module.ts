import { TitoloInapplicabilitaListResolver } from './shared/titolo-inapplicabilita-list.resolver';
import { AliquotaIvaListResolver } from './shared/aliquota-iva-list.resolver';
import { ContoListResolver } from './shared/conto-list.resolver';
import { DocumentComponent } from './document/document.component';

import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard/dashboard.component';

import { VoceIvaListResolver } from './shared/voce-iva-list.resolver';
import { FeedbackComponent } from './feedback/feedback.component';
import { FeedbackResolver } from './shared/feedback.resolver';

const appRoutes: Routes = [
    { path: 'documentList', component: DashboardComponent },
    { path: 'feedbackList', component: FeedbackComponent },
    {
        path: 'document/:id',
        component: DocumentComponent,
        resolve: {
            aliquotaIvaList: AliquotaIvaListResolver,
            contoList: ContoListResolver,
            titoloInapplicabilitaList: TitoloInapplicabilitaListResolver,
            voceIvaList: VoceIvaListResolver
        }
    },
    {
        path: 'feedback/:id',
        component: DocumentComponent,
        resolve: {
            aliquotaIvaList: AliquotaIvaListResolver,
            contoList: ContoListResolver,
            titoloInapplicabilitaList: TitoloInapplicabilitaListResolver,
            voceIvaList: VoceIvaListResolver,
            feedback: FeedbackResolver
        }
    },
    { path: '', redirectTo: '/documentList', pathMatch: 'full' },
    { path: '**', redirectTo: '/documentList' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
