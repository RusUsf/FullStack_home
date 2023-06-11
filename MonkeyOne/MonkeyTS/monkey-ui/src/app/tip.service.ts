import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import {MonkeyUI} from './tip-model';

@Injectable({
  providedIn: 'root'
})
export class TipService {

  // API URI
  private _url: string = "https://localhost:7295/api/MyMonkeyTable/"
  constructor(private http: HttpClient) { }

  getMonkeyTable(id: any): any {
    return this.http.get<MonkeyUI>(this._url + id);
  }
}
