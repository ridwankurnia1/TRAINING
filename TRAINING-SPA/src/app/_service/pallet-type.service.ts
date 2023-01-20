import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
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
    this.pallets.push(pallet);
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

      if (params.searchColor) {
        httpParam = httpParam.append('col', params.searchColor);
      }

      if (params.searchLength) {
        httpParam = httpParam.append('plt', params.searchLength);
      }

      if (params.serchLengthDirection) {
        httpParam = httpParam.append('pltd', params.serchLengthDirection);
      }

      if (params.searchWidth) {
        httpParam = httpParam.append('pwt', params.searchWidth);
      }

      if (params.searchWidthDirection) {
        httpParam = httpParam.append('pwtd', params.searchWidthDirection);
      }

      if (params.searchRemark) {
        httpParam = httpParam.append('prm', params.searchRemark);
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
        }),
        catchError((exception: HttpErrorResponse) => {
          let message = exception.error.message;
          /* if (exception.error instanceof ErrorEvent) {
            // client side error
            message = exception.error.message;
          }else{
            // server side error
          } */
          window.alert(message);
          return throwError(exception);
        })
      );
  }

  getPalletApp(): Observable<Dropdown2[]> {
    let list = <any>[];
    return this.http
      .get(this.baseUrl + 'plap', { observe: 'response' })
      .pipe(
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
