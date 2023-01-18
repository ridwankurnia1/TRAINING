import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DefectGroup } from '../_model/DefectGroup';
import { Mdf0 } from '../_model/Mdf0';
import { PaginatedResult } from '../_model/Pagination';

@Injectable({
  providedIn: 'root'
})
export class ProductionService {
  baseUrl = environment.apiUrl + 'Production/';

  constructor(private http: HttpClient) { }

  // Tanpa Pagination
  // getMDF0(): Observable<any>  {
  //   return this.http.get(this.baseUrl + 'DatMDF0Map');
  // }
  searchMDF0(prm?): Observable<any> {
    let params = new HttpParams();

    if (prm) {
      if (prm.filter) {
        params = params.append('filter', prm.filter);
      }
    }

    return this.http.get(this.baseUrl + 'DatMDF0Map', { observe: 'response', params})
      .pipe(
        map(reponse => {
          return reponse.body;
        })
      );
  }

  getMDF0(page?, itemPerPage?, prm?): Observable<PaginatedResult<any>> {
    const paginatedResult: PaginatedResult<DefectGroup[]> = new PaginatedResult<DefectGroup[]>();
    let params = new HttpParams();

    if (page != null && itemPerPage != null) {
      params = params.append('PageNumber', page);
      params = params.append('pageSize', itemPerPage);
    }

    if (prm) {
      if (prm.filter) {
        params = params.append('filter', prm.filter);
      }
    }

    return this.http.get<DefectGroup[]>(this.baseUrl + 'DatMDF0Map', { observe: 'response', params})
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

  getMDF0ByDddfgr(data: string): Observable<any> {
    return this.http.get(this.baseUrl + 'GetMDF0ByDddfgr/' + data );
  }

  editMDF0(data: DefectGroup): Observable<any> {
    console.log(data);
    return this.http.put(this.baseUrl + 'DatMDF0Map/' + data.transactionId, data);
  }
  addMDF0(data: DefectGroup): Observable<any> {
    return this.http.post(this.baseUrl + 'DatMDF0Map', data);
  }
  deleteMDF0(id: number): Observable<any> {
    return this.http.delete(this.baseUrl + 'DatMDF0ByDdtridMap/' + id);
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
