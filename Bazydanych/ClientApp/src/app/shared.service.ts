import { importProvidersFrom, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import {Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly Apiurl = "https://localhost:44449/api";
  constructor(private http: HttpClient) { }
  getDeplist():Observable<any[]>{
    return this.http.get<any>(this.Apiurl + "Users");
  }
  addUser(val:any) {
    return this.http.post(this.Apiurl + "Users", val);
  }
  UpdateUser(val: any) {
    return this.http.put(this.Apiurl + "Users", val);
  }
  DeleteUser(val: any) {
    return this.http.delete(this.Apiurl + "Users", val);
  }
  getAllUser(): Observable<any[]> {
    return this.http.get<any[]>(this.Apiurl + "Users");
  }
}
