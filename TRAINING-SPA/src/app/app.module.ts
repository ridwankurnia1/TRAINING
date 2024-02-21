import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { JwtModule } from '@auth0/angular-jwt';
import { GraphQLModule } from './graphql.module';
import { RouterModule, TitleStrategy } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptor } from './_service/error.interceptor';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

export function tokenGetter(): string {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    // NavComponent,
    // LoginComponent,
    // SummaryComponent,
    // TapComponent,
    // TaplistComponent,
    // PartNumberComponent,
    // DefectGroupComponent,
    // DefectGroupSecondComponent,
    // DefectDetailComponent,
    // DefectMappingComponent
  ],
  imports: [
    RouterModule,
    GraphQLModule,
    // ConfirmDialogModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right' }),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/api/auth'],
      },
    }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    // { provide: TitleStrategy, useClass:  },
    MessageService,
    ToastrService,
    BsModalService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
