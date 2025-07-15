import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { SimpleCrudItem, CreateSimpleCrudItem, UpdateSimpleCrudItem } from '../_model/Simplecrud';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SimplecrudService {
  baseUrl = environment.apiUrl + 'Simplecrud/';

  // Mock data for demonstration
  private mockData: SimpleCrudItem[] = [
    { id: 1, name: 'Item 1', description: 'First item', createdDate: new Date(), isActive: true },
    { id: 2, name: 'Item 2', description: 'Second item', createdDate: new Date(), isActive: true },
    { id: 3, name: 'Item 3', description: 'Third item', createdDate: new Date(), isActive: false }
  ];

  constructor(private http: HttpClient) {}

  getAll(): Observable<SimpleCrudItem[]> {
    // return this.http.get<SimpleCrudItem[]>(this.apiUrl);
    return of(this.mockData);
  }

  getById(id: number): Observable<SimpleCrudItem | undefined> {
    // return this.http.get<SimpleCrudItem>(`${this.apiUrl}/${id}`);
    return of(this.mockData.find(item => item.id === id));
  }

  create(item: CreateSimpleCrudItem): Observable<SimpleCrudItem> {
    // return this.http.post<SimpleCrudItem>(this.apiUrl, item);
    
    const newItem: SimpleCrudItem = {
      id: Math.max(...this.mockData.map(i => i.id)) + 1,
      ...item,
      createdDate: new Date()
    };
    this.mockData.push(newItem);
    return of(newItem);
  }
  
  update(item: UpdateSimpleCrudItem): Observable<SimpleCrudItem> {
  // return this.http.put<SimpleCrudItem>(`${this.apiUrl}/${item.id}`, item);
  return this.http.put<SimpleCrudItem>(`${baseUrl}/${item.id}`, item);

    const index = this.mockData.findIndex(i => i.id === item.id);
    if (index !== -1) {
      this.mockData[index] = { ...this.mockData[index], ...item };
      return of(this.mockData[index]);
    }
    throw new Error('Item not found');
  }

  delete(id: number): Observable<boolean> {
    // return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
    
    const index = this.mockData.findIndex(i => i.id === id);
    if (index !== -1) {
      this.mockData.splice(index, 1);
      return of(true);
    }
    return of(false);
  }
}
