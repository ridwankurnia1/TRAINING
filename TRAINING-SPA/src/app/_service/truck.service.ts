import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_model/Pagination';
import { Truck } from '../_model/Truck';

@Injectable({
  providedIn: 'root',
})
export class TruckService {
  baseUrl = environment.apiUrl + 'production/';

  constructor(private http: HttpClient) {}
  getTrucks(page?, itemPerPage?, prm?): Observable<PaginatedResult<Truck[]>> {
    const paginatedResult: PaginatedResult<Truck[]> = new PaginatedResult<
      Truck[]
    >();
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
      if (prm.sortString) {
        params = params.append('srt', prm.sortString);
      }
    }

    return this.http
      .get<Truck[]>(this.baseUrl + 'truck', {
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

  editTruck(data: Truck): Observable<any> {
    return this.http.put(this.baseUrl + 'truck/' + data.truckId, data);
  }

  addTruck(data: Truck): Observable<any> {
    return this.http.post(this.baseUrl + 'truck', data);
  }

  getTruck(truckId: number): Observable<any> {
    return this.http.get(this.baseUrl + 'truck/' + truckId);
  }

  deleteTruck(truckId: number): Observable<any> {
    return this.http.delete(this.baseUrl + 'truck/' + truckId);
  }

  restoreTruck(data: Truck): Observable<any> {
    return this.http.put(
      this.baseUrl + 'truck-restore/' + data.truckId,
      data.truckId
    );
  }

  export(params?): Observable<Truck[]> {
    let httpParams = new HttpParams();

    if (params) {
      if (params) {
        httpParams = httpParams.append('filter', params);
      }
    }
    return this.http
      .get<Truck[]>(this.baseUrl + 'export', {
        params: httpParams,
      })
      .pipe(
        map((res) => {
          return res;
        })
      );
  }
}
