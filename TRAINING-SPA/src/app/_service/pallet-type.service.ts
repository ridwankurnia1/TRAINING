import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
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
      /* if (params.name) {
        httpParam = httpParam.append('name', params.name);
      } */

      if (params.searchString) {
        console.info(params.searchString);
        httpParam = httpParam.append('SearchString', params.searchString);
      }
    }

    return this.http
      .get<PalletType[]>(this.baseUrl, { observe: 'response', params:httpParam })
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
}
