// variable.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VariableItem } from '../_model/Variable';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_model/Pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class VariableService {
  baseUrl = environment.apiUrl + 'Variable';

  constructor(private http: HttpClient) {}

  getVariables(): Observable<VariableItem[]> {
    return this.http.get<VariableItem[]>(this.baseUrl);
  }

  getVariablePaging(
    page?,
    itemPerPage?,
    prm?
  ): Observable<PaginatedResult<VariableItem[]>> {
    const paginatedResult: PaginatedResult<VariableItem[]> =
      new PaginatedResult<VariableItem[]>();
    let params = new HttpParams();

    if (page != null && itemPerPage != null) {
      params = params.append('PageNumber', page);
      params = params.append('PageSize', itemPerPage);
    }
    if (prm) {
      if (prm.user) {
        params = params.append('user', prm.user);
      }
      if (prm.name) {
        params = params.append('name', prm.name);
      }
      if (prm.code) {
        params = params.append('code', prm.code);
        console.log('service prm code = ', prm.code)
      }
      if (prm.value) {
        params = params.append('value', prm.value);
      }
      if (prm.search) {
        console.log('service prm srch = ', prm.search)
        params = params.append('search', prm.search);
      }
    }

    return this.http
      .get<VariableItem[]>(this.baseUrl, {
        observe: 'response',
        params,
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

  getById(variableId: number): Observable<VariableItem | undefined> {
    return this.http.get<VariableItem>(`${this.baseUrl}/${variableId}`);
  }

  create(variable: VariableItem): Observable<VariableItem> {
    return this.http.post<VariableItem>(this.baseUrl, variable);
  }

  update(variable: VariableItem): Observable<VariableItem> {
    return this.http.put<VariableItem>(
      `${this.baseUrl}/${variable.variableId}`,
      variable
    );
  }

  delete(variableId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${variableId}`);
  }
  checkCodeExist(code: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}/check-code/${code}`);
  }
}
