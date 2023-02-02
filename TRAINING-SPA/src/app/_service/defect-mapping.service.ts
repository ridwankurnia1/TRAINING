import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DefectMapping } from '../_model/DefectMapping';
import { PaginatedResult } from '../_model/Pagination';

@Injectable({
  providedIn: 'root'
})
export class DefectMappingService {

  baseUrl = environment.apiUrl + 'DefectMapping/';

  constructor(private http: HttpClient) { }

  getDefectMapping(page?, itemPerPage?, prm?): Observable<PaginatedResult<any>> {
    const paginatedResult: PaginatedResult<DefectMapping[]> = new PaginatedResult<DefectMapping[]>();
    let params = new HttpParams();

    if (page != null && itemPerPage != null) {
      params = params.append('PageNumber', page);
      params = params.append('pageSize', itemPerPage);
    }

    if (prm) {
      if (prm.name) {
        params = params.append('name', prm.name);
      }
      if (prm.filter) {
        params = params.append('filter', prm.filter);
      }
      if (prm.status){
        params = params.append('status', prm.status);
      }
    }

    return this.http.get<DefectMapping[]>(this.baseUrl + 'MDMP', { observe: 'response', params})
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

  getDropdown(prm?): Observable<any> {
    let params = new HttpParams();

    if (prm) {
      if (prm.name) {
        params = params.append('name', prm.name);
      }
    }

    return this.http.get(this.baseUrl + 'dropdown', { observe: 'response', params})
      .pipe(
        map(reponse => {
          return reponse.body;
        })
      );
  }
}
