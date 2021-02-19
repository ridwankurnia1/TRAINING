import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeComponent } from './master/employee/employee.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {
    path: 'employee',
    component: EmployeeComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '**', redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
