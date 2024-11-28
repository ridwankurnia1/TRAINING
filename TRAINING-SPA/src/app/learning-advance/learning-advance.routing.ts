import { Routes, RouterModule } from '@angular/router';
import { LearningAdvanceComponent } from './learning-advance.component';

const routes: Routes = [
  {
    path: '',
    component: LearningAdvanceComponent,
  },
  {
    path: 'api',
    children: [
      {
        path: 'dragndrop',
        loadComponent: () =>
          import('./api/drag-n-drop/drag-n-drop.component').then(
            (c) => c.DragNDropComponent
          ),
      },
      {
        path: 'content-projections',
        loadComponent: () =>
          import(
            './angular/content-projections/content-projections.component'
          ).then((c) => c.ContentProjectionsComponent),
      },
    ],
  },
  {
    path: 'css',
    children: [],
  },
];

export const LearningAdvanceRoutes = RouterModule.forChild(routes);
