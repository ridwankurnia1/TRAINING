import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  decodedToken: any = {};
  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService
  ) { }

  login(model: any): any {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const res = response.token;
        if (res) {
          localStorage.setItem('token', res);
          this.decodedToken = this.jwtHelper.decodeToken(res);
        }
      }
    ));
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    if (token) {
      return !this.jwtHelper.isTokenExpired(token);
    }

    return false;
  }
}
