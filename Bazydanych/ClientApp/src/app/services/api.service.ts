import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  readonly ApiUrl = "https://localhost:44449/api/Login/";
  readonly Delete = "https://localhost:44449/api/Login/Delete";
  constructor(private http: HttpClient) { }
  userID: any;
  getUsers() {
    return this.http.get<any>(this.ApiUrl+"users");
  }
  DeleteUser(User: any) {
    return this.http.post<any>(this.Delete, User);
  }
  isLogIn(): boolean {
    //weryfikacja czy token istnieje
    return !!localStorage.getItem('token')
  }
  SetUser(userid: any) {
    this.userID = userid;
  }
  GetUserToUpdate() {
    let queryParams = { "user": this.userID };
    return this.http.get<any>(this.ApiUrl + "getuser", { params: queryParams });

  }
  UpdateUser(user: any) {
    return this.http.put<any>(this.ApiUrl + "update", user);

  }
  UnsetUser() {
    this.userID = null;
  }
}
