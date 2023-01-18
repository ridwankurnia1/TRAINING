import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClinicComponent } from './lebaran/clinic/clinic.component';
import { DetailComponent } from './lebaran/detail/detail.component';
import { QuestionnaireComponent } from './lebaran/questionnaire/questionnaire.component';
import { RegisterComponent } from './lebaran/register/register.component';
import { SecurityComponent } from './lebaran/security/security.component';
import { SummaryComponent } from './lebaran/summary/summary.component';
import { LoginComponent } from './login/login.component';
import { PalletTypeComponent } from './master/pallet-type/pallet-type.component';
import { PartNumberComponent } from './master/part-number/part-number.component';
import { TapComponent } from './tap/tap.component';
import { TaplistComponent } from './taplist/taplist.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: QuestionnaireComponent
  },
  {
    path: 'pallet-type',
    /* runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard], */
    loadChildren: () => import('./master/pallet-type/pallet-type.module').then(m => m.PalletTypeModule),
    component: PalletTypeComponent
  },
  {
    path: 'tap',
    component: TaplistComponent
  },
  {
    path: 'tap/:id',
    component: TapComponent
  },
  {
    path: 'security',
    component: SecurityComponent
  },
  {
    path: 'register',
    /* runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard], */
    component: RegisterComponent
  },
  {
    path: 'clinic',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    component: ClinicComponent
  },
  {
    path: 'summary',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    component: SummaryComponent
  },
  {
    path: 'summary/:id',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    component: DetailComponent
  },
  {
    path: 'detail',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    component: DetailComponent
  },
  {
    path: 'part',
    component: PartNumberComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '**', redirectTo: ''
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
