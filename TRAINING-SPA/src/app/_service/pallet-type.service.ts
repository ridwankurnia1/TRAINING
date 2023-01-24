import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Dropdown2 } from '../_model/Dropdown2';
import { PaginatedResult } from '../_model/Pagination';
import { PalletType } from '../_model/PalletType';

@Injectable({
  providedIn: 'root',
})
export class PalletTypeService {
  pallets: PalletType[] = [];
  baseUrl = environment.apiUrl + 'ipty/';

  constructor(private http: HttpClient) {}

  create(pallet: PalletType) {
    // fix checkbox array value
    let data = pallet;

    if (data.recordStatus) {
      data.recordStatus = 1;
    } else {
      data.recordStatus = 0;
    }

    if (data.flag1) {
      data.flag1 = 1;
    } else {
      data.flag1 = 0;
    }

    if (data.carryInFlag) {
      data.carryInFlag = 1;
    } else {
      data.carryInFlag = 0;
    }

    if (data.carryOutFlag) {
      data.carryOutFlag = 1;
    } else {
      data.carryOutFlag = 0;
    }

    return this.http.post(this.baseUrl, data);
  }

  update(pallet: PalletType) {
    // fix checkbox array value
    let data = pallet;

    if (data.recordStatus) {
      data.recordStatus = 1;
    } else {
      data.recordStatus = 0;
    }

    if (data.flag1) {
      data.flag1 = 1;
    } else {
      data.flag1 = 0;
    }

    if (data.carryInFlag) {
      data.carryInFlag = 1;
    } else {
      data.carryInFlag = 0;
    }

    if (data.carryOutFlag) {
      data.carryOutFlag = 1;
    } else {
      data.carryOutFlag = 0;
    }

    return this.http.put(this.baseUrl + pallet.palletType, data);
  }

  delete(type: string) {
    return this.http.delete(this.baseUrl + type);
  }

  single(palletType: string): Observable<PalletType> {
    return this.http
      .get(this.baseUrl + 'find-type/' + palletType, { observe: 'response' })
      .pipe(
        map((res) => {
          return res.body;
        }),
        catchError((e: HttpErrorResponse) => {
          return throwError(e);
        })
      );
  }

  export(params?): Observable<PalletType[]> {
    let httpParam = new HttpParams();

    if (params) {
      if (params.searchType) {
        httpParam = httpParam.append('ptp', params.searchType);
      }

      if (params.searchApp) {
        httpParam = httpParam.append('atp', params.searchApp);
      }

      if (params.searchMaterial) {
        httpParam = httpParam.append('mtp', params.searchMaterial);
      }

      if (params.searchStatus) {
        let status = params.searchStatus;
        if (status == 2) {
          status = 0;
        }
        httpParam = httpParam.append('stp', status);
      }

      if (params.sortString) {
        httpParam = httpParam.append('srt', params.sortString);
      }
    }

    return this.http
      .get<PalletType[]>(this.baseUrl + 'export', {
        observe: 'response',
        params: httpParam,
      })
      .pipe(
        map((res) => {
          return res.body;
        })
      );
  }

  all(page?, perPage?, params?): Observable<PaginatedResult<PalletType[]>> {
    const paginatedResult: PaginatedResult<PalletType[]> = new PaginatedResult<
      PalletType[]
    >();
    let httpParam = new HttpParams();

    if (page != null && perPage != null) {
      httpParam = httpParam.append('PageNumber', page);
      httpParam = httpParam.append('PageSize', perPage);
    }

    if (params) {
      if (params.searchString) {
        httpParam = httpParam.append('SearchString', params.searchString);
      }

      if (params.searchType) {
        httpParam = httpParam.append('ptp', params.searchType);
      }

      if (params.searchApp) {
        httpParam = httpParam.append('atp', params.searchApp);
      }

      if (params.searchMaterial) {
        httpParam = httpParam.append('mtp', params.searchMaterial);
      }

      if (params.searchStatus) {
        let status = params.searchStatus;
        if (status == 2) {
          status = 0;
        }
        httpParam = httpParam.append('stp', status);
      }

      if (params.sortString) {
        httpParam = httpParam.append('srt', params.sortString);
      }
    }

    return this.http
      .get<PalletType[]>(this.baseUrl, {
        observe: 'response',
        params: httpParam,
      })
      .pipe(
        map((res) => {
          paginatedResult.result = res.body;

          if (res.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(
              res.headers.get('Pagination')
            );
          }

          return paginatedResult;
        })
      );
  }

  getPalletApp(): Observable<Dropdown2[]> {
    let list = <any>[];
    return this.http.get(this.baseUrl + 'plap', { observe: 'response' }).pipe(
      map((res) => {
        list = res.body;
        return list;
      })
    );
  }

  getMeasurements(): Observable<Dropdown2[]> {
    let list = <any>[];
    return this.http
      .get(this.baseUrl + 'measurements', { observe: 'response' })
      .pipe(
        map((res) => {
          list = res.body;
          return list;
        })
      );
  }

  getMaterialType(): Observable<Dropdown2[]> {
    let list = <any>[];
    return this.http
      .get(this.baseUrl + 'gct2?type=mtp', { observe: 'response' })
      .pipe(
        map((res) => {
          list = res.body;
          return list;
        })
      );
  }

  getColorType(): Observable<Dropdown2[]> {
    let list = <any>[];
    return this.http
      .get(this.baseUrl + 'gct2?type=col', { observe: 'response' })
      .pipe(
        map((res) => {
          list = res.body;
          return list;
        })
      );
  }

  getCurrencies(): Observable<Dropdown2[]> {
    let list = <any>[];
    return this.http
      .get(this.baseUrl + 'currencies', { observe: 'response' })
      .pipe(
        map((res) => {
          list = res.body;
          return list;
        })
      );
  }
}
