import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { WarehouseGroup } from '../_model/WarehouseGroup';

@Injectable({
  providedIn: 'root',
})
export class WarehouseService {
  baseUrl = environment.apiUrl + 'warehouse/';
  constructor(private http: HttpClient) {}

  allGroup(): Observable<WarehouseGroup[]> {
    return this.http.get(this.baseUrl + 'group', { observe: 'response' }).pipe(
      map((res: HttpResponse<WarehouseGroup[]>) => {
        return res.body;
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

  deleteGroup(code: String): Observable<void> {
    return this.http.delete<void>(this.baseUrl + 'group/' + code);
  }
}
