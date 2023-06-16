import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataRunnerService {
  
  apiUrl = 'http://localhost:5099/api/MyMonkeyTable'
  
  constructor(private http: HttpClient) { }


  // Method to get all the values
  getValues(): Observable<string[]>{
    return this.http.get<string[]>(this.apiUrl);
  }

  // Method to get a single value by Id
  getValue(id: number): Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/${id}`);  
  }
}
