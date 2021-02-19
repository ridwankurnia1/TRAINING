import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Employee } from '../_model/Employee';
import { PaginatedResult } from '../_model/Pagination';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl = environment.apiUrl + 'user/';

  constructor(private http: HttpClient) { }

  getEmployees(page?, itemPerPage?, prm?): Observable<PaginatedResult<Employee[]>> {
    const paginatedResult: PaginatedResult<Employee[]> = new PaginatedResult<Employee[]>();
    let params = new HttpParams();

    if (page != null && itemPerPage != null) {
      params = params.append('PageNumber', page);
      params = params.append('pageSize', itemPerPage);
    }
    if (prm) {
      if (prm.name) {
        params = params.append('name', prm.name);
      }
    }

    return this.http.get<Employee[]>(this.baseUrl + 'employee', { observe: 'response', params})
      .pipe(
        map(reponse => {
          paginatedResult.result = reponse.body;
          if (reponse.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(reponse.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getEmployee(nama: string) {
    return this.http.get('');
  }
}
