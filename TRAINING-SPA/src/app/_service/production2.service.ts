import { Injectable } from '@angular/core';
import { DefectDetail } from '../_model/DefectDetail';
import { PaginatedResult } from '../_model/Pagination';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class Production2Service {


  baseUrl = environment.apiUrl + 'Production2/';

  constructor(private http: HttpClient) { }

  getMDF1(page?, itemPerPage?, prm?): Observable<PaginatedResult<any>> {
    const paginatedResult: PaginatedResult<DefectDetail[]> = new PaginatedResult<DefectDetail[]>();
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
      if (prm.defName){
        params = params.append('defName', prm.defName);
      }
      if (prm.defType){
        params = params.append('defType', prm.defType);
      }
    }

    return this.http.get<DefectDetail[]>(this.baseUrl + 'DatMDF1', { observe: 'response', params})
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
  addDefectDetail(data: DefectDetail): Observable<any> {
    return this.http.post(this.baseUrl + 'DatMDF1', data);
  }

  deleteDefectDetail(id: string): Observable<any> {
    return this.http.delete(this.baseUrl + 'DatMDF1/' + id);
  }

  editDefectDetail(data: DefectDetail): Observable<any> {
    console.log(data);
    return this.http.put(this.baseUrl + 'DatMdf1/' + data.defectCode, data);
  }
}
