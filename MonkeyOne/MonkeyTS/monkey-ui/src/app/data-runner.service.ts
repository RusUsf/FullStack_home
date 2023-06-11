import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataRunnerService {
  
  apiUrl = 'http://localhost:5099/api/MyMonkeyTable/'
  
  constructor(private http: HttpClient) { }

  getValues(): Observable<string[]>{
    return this.http.get<string[]>(this.apiUrl);
  }
}
