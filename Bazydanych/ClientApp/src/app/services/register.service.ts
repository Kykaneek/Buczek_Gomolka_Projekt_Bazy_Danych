import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  readonly Apiurl = "https://localhost:44449/api/register";
  constructor(private http: HttpClient) { }
  Register(LoginOjb: any) {
    return this.http.post<any>(this.Apiurl, LoginOjb);
  }
  isLogIn(): boolean {
    //weryfikacja czy token istnieje
    return !!localStorage.getItem('token')
  }
}
