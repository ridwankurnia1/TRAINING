import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClinicComponent } from './lebaran/clinic/clinic.component';
import { DownloadComponent } from './lebaran/download/download.component';
import { QuestionnaireComponent } from './lebaran/questionnaire/questionnaire.component';
import { SecurityComponent } from './lebaran/security/security.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: QuestionnaireComponent
  },
  {
    path: 'security',
    component: SecurityComponent
  },
  {
    path: 'clinic',
    component: ClinicComponent
  },
  {
    path: 'download',
    component: DownloadComponent
  },
  {
    path: '**', redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
