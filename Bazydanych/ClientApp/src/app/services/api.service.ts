import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  readonly ApiUrl = "https://localhost:44449/api/Login";
  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<any>(this.ApiUrl);
  }


}
