import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  readonly ApiUrl = "https://localhost:44449/api/Login/users";
  readonly Delete = "https://localhost:44449/api/Login/Delete";
  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<any>(this.ApiUrl);
  }
  DeleteUser(User: any) {
    return this.http.post<any>(this.Delete, User);
  }
  getUser(User: any) {
    return this.http.get<any>(this.ApiUrl,User);
  }
  isLogIn(): boolean {
    //weryfikacja czy token istnieje
    return !!localStorage.getItem('token')
  }
}
