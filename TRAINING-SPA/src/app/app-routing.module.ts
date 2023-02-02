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
import { DefectGroupComponent } from './master/defect-group/defect-group.component';
import { EmployeeComponent } from './master/employee/employee.component';
import {DefectGroupSecondComponent} from './master/defect-group-second/defect-group-second.component';
import {DefectDetailComponent} from './master/defect-detail/defect-detail.component';
import { DefectMappingComponent } from './master/defect-mapping/defect-mapping.component';

import { WarehouseComponent } from './master/warehouse/warehouse.component';
import { LearningComponent } from './learning/learning.component';
import { TestingComponent } from './master/testing/testing.component';

const routes: Routes = [
  {
    path: '',
    component: QuestionnaireComponent,
  },
  {
    path: 'learning',
    loadChildren: () =>
      import('./learning/learning.module').then((m) => m.LearningModule),
    component: LearningComponent,
  },
  {
    path: 'testing',
    loadChildren: () =>
      import('./master/testing/testing.module').then((m) => m.TestingModule),
    component: TestingComponent,
  },
  {
    path: 'warehouse',
    // lazy load module
    loadChildren: () =>
      import('./master/warehouse/warehouse.module').then(
        (m) => m.WarehouseModule
      ),
    component: WarehouseComponent,
  },
  {
    path: 'pallet-type',
    /* runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard], */
    loadChildren: () =>
      import('./master/pallet-type/pallet-type.module').then(
        (m) => m.PalletTypeModule
      ),
    component: PalletTypeComponent,
  },
  {
    path: 'tap',
    component: TaplistComponent,
  },
  {
    path: 'tap/:id',
    component: TapComponent,
  },
  {
    path: 'security',
    component: SecurityComponent,
  },
  {
    path: 'emp',
    component: EmployeeComponent,
  },
  {
    path: 'register',
    /* runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard], */
    component: RegisterComponent,
  },
  {
    path: 'clinic',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    component: ClinicComponent,
  },
  {
    path: 'summary',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    component: SummaryComponent,
  },
  {
    path: 'summary/:id',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    component: DetailComponent,
  },
  {
    path: 'detail',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    component: DetailComponent,
  },
  {
    path: 'part',
    component: PartNumberComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'defect',
    component: DefectGroupComponent,
  },
  {
    path: 'defectSecond',
    component: DefectGroupSecondComponent,
  },
  {
    path: 'defectDetail',
    component: DefectDetailComponent,
  },
  {
    path: 'defectMapping',
    component: DefectMappingComponent,
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
