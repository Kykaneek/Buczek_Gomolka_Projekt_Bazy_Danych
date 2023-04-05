import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class LoginService {
  readonly Apiurl = "https://localhost:44449/api";
  constructor(private http: HttpClient) { }
  getDeplist(): Observable<any[]> {
    return this.http.get<any>(this.Apiurl + "Login");
  }
  addUser(val: any) {
    return this.http.post(this.Apiurl + "Login", val);
  }
  UpdateUser(val: any) {
    return this.http.put(this.Apiurl + "Login", val);
  }
  DeleteUser(val: any) {
    return this.http.delete(this.Apiurl + "Login", val);
  }
  getAllUser(): Observable<any[]> {
    return this.http.get<any[]>(this.Apiurl + "Login");
  }
  getuserauth(): Observable<any[]> {
    return this.http.get<any[]>(this.Apiurl + "Login");
  }
  GetUserbyCode(id: any) {
    return this.http.get(this.Apiurl + 'Login' + id);
  }
}
