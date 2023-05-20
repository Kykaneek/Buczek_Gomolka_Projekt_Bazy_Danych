import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GetService {


  readonly User = "https://localhost:44449/api/Login/";
  readonly Contractor = "https://localhost:44449/api/Contractor/"; 
  readonly Trace = "https://localhost:44449/api/Trace/";
  readonly location = "https://localhost:44449/api/Location/";
  constructor(private http: HttpClient) { }
  getUsers() {
    return this.http.get<any>(this.User + "users");
  }
  getTraces() {
    return this.http.get<any>(this.Trace + "Getall");
  }
  getContractor() {
    return this.http.get<any>(this.Contractor + "Getall");
  }
  getLocation() {
    return this.http.get<any>(this.location + "getAllLocations");
  }
  GetDrivers() {
    return this.http.get<any>(this.User + "GetDrivers");
  }
  GetDriversedit(id: any) {
    let queryParams = { "id": id };
    return this.http.get<any>(this.Trace + "GetDriveredited", { params: queryParams });
  }



}
