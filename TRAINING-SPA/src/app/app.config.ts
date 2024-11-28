import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { JwtModule } from '@auth0/angular-jwt';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';

export function tokenGetter(): string {
  return localStorage.getItem('token');
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    importProvidersFrom([
      ToastrModule.forRoot({ positionClass: 'toast-bottom-right' }),
      JwtModule.forRoot({
        config: {
          tokenGetter,
          allowedDomains: ['localhost:5000'],
          disallowedRoutes: ['localhost:5000/api/auth'],
        },
      }),
    ]),
    provideHttpClient(withInterceptorsFromDi()),
  ],
};
