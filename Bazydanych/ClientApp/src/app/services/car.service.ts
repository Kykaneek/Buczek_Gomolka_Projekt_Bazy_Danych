import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  readonly ApiUrl = "https://localhost:44449/api/Car/";
  constructor(private http: HttpClient) { }
  getCars() {
    return this.http.get<any>(this.ApiUrl + "Getall");
  }



}
