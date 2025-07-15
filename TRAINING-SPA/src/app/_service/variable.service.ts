import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VariableItem, CreateVariableItem, UpdateVariableItem } from '../_model/Variable';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})

export class VariableService {
  baseUrl = environment.apiUrl + 'Variable/';

  constructor(private http: HttpClient) {}

  getVariables(): Observable<VariableItem[]> {
    return this.http.get<VariableItem[]>(this.baseUrl);
  }

  getById(variableId: number): Observable<VariableItem | undefined> {
    return this.http.get<VariableItem>(`${this.baseUrl}/${variableId}`);
  }

  create(item: CreateVariableItem): Observable<VariableItem> {
    return this.http.post<VariableItem>(this.baseUrl, item);
  }

  update(variable: UpdateVariableItem): Observable<VariableItem> {
    return this.http.put<VariableItem>(`${this.baseUrl}/${variable.variableId}`, variable);
  }

  delete(variableId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${variableId}`);
  }
}
