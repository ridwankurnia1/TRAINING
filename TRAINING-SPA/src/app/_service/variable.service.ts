import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Variable } from '../_model/Variable';

@Injectable({
  providedIn: 'root'
})

export class VariableService {
  private apiUrl = 'http://your-api-url/api/variables'; // change to your actual API

  constructor(private http: HttpClient) {}

  getVariables(): Observable<Variable[]> {
    return this.http.get<Variable[]>(this.apiUrl);
  }

  deleteVariable(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  updateVariable(variable: Variable): Observable<Variable> {
    return this.http.put<Variable>(`${this.apiUrl}/${variable.variableId}`, variable);
  }
}
