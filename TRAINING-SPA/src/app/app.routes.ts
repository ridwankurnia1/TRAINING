import { Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./lebaran/questionnaire/questionnaire.component').then(
        (c) => c.QuestionnaireComponent
      ),
  },
  {
    path: 'learning',
    loadComponent: () =>
      import('./learning/learning.component').then((c) => c.LearningComponent),
  },
  {
    path: 'warehouse',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./master/warehouse/warehouse.component').then(
        (c) => c.WarehouseComponent
      ),
  },
  {
    path: 'pallet-type',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./master/pallet-type/pallet-type.component').then(
        (c) => c.PalletTypeComponent
      ),
  },
  {
    path: 'tap',
    loadComponent: () =>
      import('./tap/tap.component').then((m) => m.TapComponent),
  },
  {
    path: 'tap/:id',
    loadComponent: () =>
      import('./tap/tap.component').then((m) => m.TapComponent),
  },
  {
    path: 'security',
    loadComponent: () =>
      import('./lebaran/security/security.component').then(
        (c) => c.SecurityComponent
      ),
  },
  {
    path: 'employee',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./master/employee/employee.component').then(
        (c) => c.EmployeeComponent
      ),
  },

  {
    path: 'truck',
    loadComponent: () =>
      import('./master/truck/truck.component').then(
        (c) => c.TruckComponent
      ),
  },

  {
    path: 'register',
    loadComponent: () =>
      import('./lebaran/register/register.component').then(
        (c) => c.RegisterComponent
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
    loadComponent: () =>
      import('./lebaran/summary/summary.component').then(
        (c) => c.SummaryComponent
      ),
  },
  {
    path: 'summary/:id',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./lebaran/detail/detail.component').then(
        (c) => c.DetailComponent
      ),
  },
  // {
  //   path: 'detail',
  //   runGuardsAndResolvers: 'always',
  //   canActivate: [AuthGuard],
  //   loadChildren: () => import('./lebaran/detail/detail.module').then(m => m.DetailModule),
  // },
  {
    path: 'part',
    loadComponent: () =>
      import('./master/part-number/part-number.component').then(
        (c) => c.PartNumberComponent
      ),
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./login/login.component').then((c) => c.LoginComponent),
  },
  {
    path: 'defect',
    loadComponent: () =>
      import('./master/defect-group/defect-group.component').then(
        (c) => c.DefectGroupComponent
      ),
  },
  {
    path: 'defectSecond',
    loadComponent: () =>
      import('./master/defect-group-second/defect-group-second.component').then(
        (c) => c.DefectGroupSecondComponent
      ),
  },
  {
    path: 'defectDetail',
    loadComponent: () =>
      import('./master/defect-detail/defect-detail.component').then(
        (c) => c.DefectDetailComponent
      ),
  },
  {
    path: 'defectMapping',
    loadComponent: () =>
      import('./master/defect-mapping/defect-mapping.component').then(
        (c) => c.DefectMappingComponent
      ),
  },
 
  {
    path: '**',
    redirectTo: '',
  },
];
