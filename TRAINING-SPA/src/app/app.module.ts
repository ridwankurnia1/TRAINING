import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { EmployeeComponent } from './master/employee/employee.component';
import { QuestionnaireComponent } from './lebaran/questionnaire/questionnaire.component';
import { ClinicComponent } from './lebaran/clinic/clinic.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { DropdownModule } from 'primeng/dropdown';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { RadioButtonModule } from 'primeng/radiobutton';
import { LoginComponent } from './login/login.component';
import { SummaryComponent } from './lebaran/summary/summary.component';
import { DetailComponent } from './lebaran/detail/detail.component';
import { SecurityComponent } from './lebaran/security/security.component';
import { RegisterComponent } from './lebaran/register/register.component';
import { TapComponent } from './tap/tap.component';
import { TaplistComponent } from './taplist/taplist.component';

export function tokenGetter(): string {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [		
    AppComponent,
    NavComponent,
    EmployeeComponent,
    QuestionnaireComponent,
    ClinicComponent,
    LoginComponent,
    SummaryComponent,
    DetailComponent,
    SecurityComponent,
    RegisterComponent,
    TapComponent,
      TaplistComponent
   ],
  imports: [
    AutoCompleteModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    TableModule,
    ButtonModule,
    ConfirmDialogModule,
    DropdownModule,
    RadioButtonModule,
    ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right'}),
    BsDatepickerModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/api/auth']
      }
    })
  ],
  providers: [
    ConfirmationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
