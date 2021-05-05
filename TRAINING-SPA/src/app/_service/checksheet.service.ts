import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Dropdown } from '../_model/Dropdown';
import { Employee } from '../_model/Employee';
import { EmployeeId } from '../_model/EmployeeId';
import { Lebaran } from '../_model/Lebaran';
import { PaginatedResult } from '../_model/Pagination';

@Injectable({
  providedIn: 'root'
})
export class ChecksheetService {
  baseUrl = environment.apiUrl + 'checksheet/';

  constructor(private http: HttpClient) { }

  getEmployees(page?, itemPerPage?, prm?): Observable<PaginatedResult<Lebaran[]>> {
    const paginatedResult: PaginatedResult<Lebaran[]> = new PaginatedResult<Lebaran[]>();
    let params = new HttpParams();

    if (page != null && itemPerPage != null) {
      params = params.append('PageNumber', page);
      params = params.append('pageSize', itemPerPage);
    }
    if (prm) {
      if (prm.name) {
        params = params.append('name', prm.name);
      }
      if (prm.dept) {
        params = params.append('dept', prm.dept);
      }
      if (prm.filter) {
        params = params.append('filter', prm.filter);
      }
    }

    return this.http.get<Lebaran[]>(this.baseUrl + 'employee', { observe: 'response', params})
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
  getEmployee(nik: string): Observable<Lebaran> {
    return this.http.get<Lebaran>(this.baseUrl + 'employee/' + nik);
  }
  getSummaryEmployee(prm?): Observable<any> {
    let params = new HttpParams();

    if (prm) {
      if (prm.name) {
        params = params.append('name', prm.name);
      }
    }

    return this.http.get(this.baseUrl + 'employee/summary', { observe: 'response', params})
      .pipe(
        map(reponse => {
          return reponse.body;
        })
      );
  }
  getDetailEmployee(prm?): Observable<any> {
    let params = new HttpParams();

    if (prm) {
      if (prm.name) {
        params = params.append('name', prm.name);
      }
    }

    return this.http.get(this.baseUrl + 'employee/detail', { observe: 'response', params})
      .pipe(
        map(reponse => {
          return reponse.body;
        })
      );
  }
  addQuistioner(data: Lebaran): Observable<any> {
    return this.http.put(this.baseUrl + 'employee/' + data.employeeId, data);
  }
  updateClinic(data: Lebaran): Observable<any> {
    return this.http.put(this.baseUrl + 'employee/' + data.employeeId + '/clinic', data);
  }
  updateSecurity(data: EmployeeId): Observable<Lebaran> {
    return this.http.post(this.baseUrl + 'employee/security', data);
  }
  getDepartment(location: string): Observable<Dropdown[]> {
    let params = new HttpParams();

    if (location) {
        params = params.append('brno', location);
    }

    return this.http.get<Dropdown[]>(this.baseUrl + 'department', { observe: 'response', params})
      .pipe(
        map(reponse => {
          return reponse.body;
        })
      );
  }
}
