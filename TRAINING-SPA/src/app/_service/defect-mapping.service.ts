import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DefectMapping } from '../_model/DefectMapping';
import { PaginatedResult } from '../_model/Pagination';

@Injectable({
  providedIn: 'root',
})
export class DefectMappingService {
  baseUrl = environment.apiUrl + 'DefectMapping/';

  constructor(private http: HttpClient) {}

  getDefectMapping(
    page?,
    itemPerPage?,
    prm?
  ): Observable<PaginatedResult<any>> {
    const paginatedResult: PaginatedResult<DefectMapping[]> =
      new PaginatedResult<DefectMapping[]>();
    let httpParams = new HttpParams();

    if (page != null && itemPerPage != null) {
      httpParams = httpParams.append('PageNumber', page);
      httpParams = httpParams.append('pageSize', itemPerPage);
    }

    if (prm) {
      if (prm.name) {
        httpParams = httpParams.append('name', prm.name);
      }
      if (prm.DefectTypeFilter) {
        httpParams = httpParams.append('defT', prm.DefectTypeFilter);
      }
      if (prm.LineProcessFilter) {
        httpParams = httpParams.append('lineP', prm.LineProcessFilter);
      }
    }

    console.log(prm);

    return this.http
      .get<DefectMapping[]>(this.baseUrl + 'MDMP', {
        observe: 'response',
        params: httpParams,
      })
      .pipe(
        map((reponse) => {
          paginatedResult.result = reponse.body;
          if (reponse.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(
              reponse.headers.get('Pagination')
            );
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

    return this.http
      .get(this.baseUrl + 'dropdown', { observe: 'response', params })
      .pipe(
        map((reponse) => {
          return reponse.body;
        })
      );
  }

  addDefectMapping(data: DefectMapping): Observable<any> {
    return this.http.post(this.baseUrl + 'MDMP', data);
  }

  editDefectMapping(data: DefectMapping): Observable<any> {
    console.log(data);
    return this.http.put(this.baseUrl + 'MDMP/' + data.defectCode, data);
  }
}
