// app-routing.module.ts
import * as core from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'variable',
    loadChildren: () =>
      import('./variable/variable.module').then(m => m.VariableModule)
  }
];

@core.NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}