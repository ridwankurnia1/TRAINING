import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Dropdown2 } from '../_model/Dropdown2';
import { PaginatedResult } from '../_model/Pagination';
import { Warehouse } from '../_model/Warehouse';
import { WarehouseGroup } from '../_model/WarehouseGroup';

@Injectable()
export class WarehouseService {
  baseUrl = environment.apiUrl + 'warehouse/';
  constructor(private http: HttpClient) {}

  all(page?, perPage?, params?): Observable<PaginatedResult<Warehouse[]>> {
    let httpParams = new HttpParams();

    if (page != null && perPage != null) {
      httpParams = httpParams.append('PageNumber', page);
      httpParams = httpParams.append('PageSize', perPage);
    }

    if (params) {
      if (params.searchGlobal) {
        httpParams = httpParams.append('ws', params.searchGlobal);
      }
    }

    return this.http
      .get<Warehouse[]>(this.baseUrl, {
        observe: 'response',
        params: httpParams,
      })
      .pipe(
        map((res) => {
          let pr = new PaginatedResult<Warehouse[]>();

          pr.result = res.body;
          pr.pagination = JSON.parse(res.headers.get('Pagination'));

          return pr;
        })
      );
  }

  single(code: string): Observable<Warehouse> {
    return this.http.get<Warehouse>(this.baseUrl + code);
  }

  create(data: Warehouse): Observable<any> {
    if (data.fifoFlag) {
      data.fifoFlag = 1;
    } else {
      data.fifoFlag = 0;
    }
    if (!data.fifoDays) {
      data.fifoDays = 0;
    }
    if (data.carryOutFlag) {
      data.carryOutFlag = 1;
    } else {
      data.carryOutFlag = 0;
    }
    if (data.policeNumber) {
      data.policeNumber = 1;
    } else {
      data.policeNumber = 0;
    }
    if (data.stocktakingFlag) {
      data.stocktakingFlag = 1;
    } else {
      data.stocktakingFlag = 0;
    }
    if (data.transferModelFlag) {
      data.transferModelFlag = 1;
    } else {
      data.transferModelFlag = 0;
    }
    if (data.recordStatus) {
      data.recordStatus = 1;
    } else {
      data.recordStatus = 0;
    }
    return this.http.post(this.baseUrl, data);
  }

  update(data: Warehouse): Observable<void> {
    data = this.transformChecks(data);
    return this.http.put<void>(this.baseUrl + data.code, data);
  }

  delete(code: string): Observable<void> {
    return this.http.delete<void>(this.baseUrl + code);
  }

  export(params?): Observable<Warehouse[]> {
    let httpParams = new HttpParams();

    if (params) {
      // search and sort
    }

    return this.http.get<Warehouse[]>(this.baseUrl + 'export', {
      observe: 'body',
      params: httpParams,
    });
  }

  allGroup(search?: string): Observable<WarehouseGroup[]> {
    let httpParams = new HttpParams();

    if (search) {
      httpParams = httpParams.append('gs', search);
    }

    return this.http
      .get<WarehouseGroup[]>(this.baseUrl + 'group', { params: httpParams })
      .pipe(
        map((res) => {
          return res;
        })
      );
  }

  singleGroup(code: string): Observable<WarehouseGroup> {
    return this.http
      .get<WarehouseGroup>(this.baseUrl + 'group/' + code, { observe: 'body' })
      .pipe(
        map((res) => {
          return res;
        })
      );
  }

  createGroup(data: WarehouseGroup): Observable<void> {
    if (data.recordStatus) {
      data.recordStatus = 1;
    } else {
      data.recordStatus = 0;
    }

    return this.http.post<void>(this.baseUrl + 'group', data);
  }

  updateGroup(data: WarehouseGroup): Observable<void> {
    if (data.recordStatus) {
      data.recordStatus = 1;
    } else {
      data.recordStatus = 0;
    }

    return this.http.put<void>(this.baseUrl + 'group/' + data.code, data);
  }

  deleteGroup(code: String): Observable<void> {
    return this.http.delete<void>(this.baseUrl + 'group/' + code);
  }

  private transformChecks(data: any): any {
    let res = data;
    if (res.fifoFlag) {
      res.fifoFlag = 1;
    } else {
      res.fifoFlag = 0;
    }
    if (res.carryOutFlag) {
      res.carryOutFlag = 1;
    } else {
      res.carryOutFlag = 0;
    }
    if (res.policeNumber) {
      res.policeNumber = 1;
    } else {
      res.policeNumber = 0;
    }
    if (res.stocktakingFlag) {
      res.stocktakingFlag = 1;
    } else {
      res.stocktakingFlag = 0;
    }
    if (res.transferModelFlag) {
      res.transferModelFlag = 1;
    } else {
      res.transferModelFlag = 0;
    }
    if (res.recordStatus) {
      res.recordStatus = 1;
    } else {
      res.recordStatus = 0;
    }

    return res;
  }

  allType(): Observable<Dropdown2[]> {
    return this.http.get<Dropdown2[]>(this.baseUrl + 'type');
  }
}
