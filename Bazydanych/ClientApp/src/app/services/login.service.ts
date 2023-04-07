import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class LoginService {
  readonly Apiurl = "https://localhost:44449/api/Login";
  constructor(private http: HttpClient) { }
  Login(LoginOjb: any) {
    return this.http.post<any>(this.Apiurl,LoginOjb);
  }

  storetoken(Tokenvalue: string) {
    localStorage.setItem('token', Tokenvalue)
  }

  gettoken() {
    return localStorage.getItem('token')
  }

  isLogIn(): boolean {
    //weryfikacja czy token istnieje
    return !!localStorage.getItem('token')
  }

}
