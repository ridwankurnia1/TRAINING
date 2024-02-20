import { enableProdMode, importProvidersFrom } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { HttpClientModule, provideHttpClient } from '@angular/common/http';
import { AuthService } from './app/_service/auth.service';
import { ErrorInterceptorProvider } from './app/_service/error.interceptor';
import { EmployeeService } from './app/_service/employee.service';
import { AuthGuard } from './app/_guards/auth.guard';
import { UIService } from './app/_service/ui.service';
import { WarehouseService } from './app/_service/warehouse.service';
import { Production2Service } from './app/_service/production2.service';
import { ProductionService } from './app/_service/production.service';
import { ExcelService } from './app/_service/excel.service';
import { DefectMappingService } from './app/_service/defect-mapping.service';
import { ChecksheetService } from './app/_service/checksheet.service';
import { ToastrModule } from 'ngx-toastr';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app/app-routing.module';

if (environment.production) {
  enableProdMode();
}

export function tokenGetter() {
  return localStorage.getItem('token');
}

// platformBrowserDynamic().bootstrapModule(AppModule)
//   .catch(err => console.error(err));

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    AuthService,
    ErrorInterceptorProvider,
    EmployeeService,
    AuthGuard,
    UIService,
    WarehouseService,
    Production2Service,
    ProductionService,
    ExcelService,
    DefectMappingService,
    ChecksheetService,
    importProvidersFrom(
      ToastrModule.forRoot({ positionClass: 'toast-bottom-right'}),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TimepickerModule.forRoot(),
      // TabsModule.forRoot(),
      JwtModule.forRoot({
        config: {
          tokenGetter,
          allowedDomains: ['localhost:5000'],
          disallowedRoutes: ['localhost:5000/api/auth']
       }
      }),
      RouterModule.forRoot([]),
      BrowserModule,
      BrowserAnimationsModule,
      AppRoutingModule,
      HttpClientModule
    )
  ]
})
