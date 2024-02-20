import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SummaryComponent } from './lebaran/summary/summary.component';
import { LoginComponent } from './login/login.component';
import { PalletTypeComponent } from './master/pallet-type/pallet-type.component';
import { PartNumberComponent } from './master/part-number/part-number.component';
import { TapComponent } from './tap/tap.component';
import { TaplistComponent } from './taplist/taplist.component';
import { AuthGuard } from './_guards/auth.guard';
import { DefectGroupComponent } from './master/defect-group/defect-group.component';
import { EmployeeComponent } from './master/employee/employee.component';
import { DefectGroupSecondComponent } from './master/defect-group-second/defect-group-second.component';
import { DefectDetailComponent } from './master/defect-detail/defect-detail.component';
import { DefectMappingComponent } from './master/defect-mapping/defect-mapping.component';

import { WarehouseComponent } from './master/warehouse/warehouse.component';
import { LearningComponent } from './learning/learning.component';
import { TestingComponent } from './master/testing/testing.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./lebaran/questionnaire/questionnaire.module').then(
        (m) => m.QuestionnaireModule
      ),
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
    loadComponent: () =>
      import('./tap/tap.component').then((m) => m.TapComponent),
    component: TaplistComponent,
  },
  {
    path: 'tap/:id',
    component: TapComponent,
  },
  {
    path: 'security',
    loadChildren: () =>
      import('./lebaran/security/security.module').then(
        (m) => m.SecurityModule
      ),
  },
  {
    path: 'employee',
    component: EmployeeComponent,
    // loadChildren: () => import('./master/employee/employee.module').then(m => m.EmployeeModule),
  },
  {
    path: 'register',
    loadChildren: () =>
      import('./lebaran/register/register.module').then(
        (m) => m.RegisterModule
      ),
  },
  // {
  //   path: 'clinic',
  //   runGuardsAndResolvers: 'always',
  //   canActivate: [AuthGuard],
  //   loadChildren: () => import('./lebaran/clinic/clinic.module').then(m => m.ClinicModule),
  // },
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
    loadChildren: () =>
      import('./lebaran/detail/detail.module').then((m) => m.DetailModule),
  },
  // {
  //   path: 'detail',
  //   runGuardsAndResolvers: 'always',
  //   canActivate: [AuthGuard],
  //   loadChildren: () => import('./lebaran/detail/detail.module').then(m => m.DetailModule),
  // },
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
