import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ContractorService {
  readonly ApiUrl = "https://localhost:44449/api/Contractor/"; 
  constructor(private http: HttpClient) { }
  getContractor() {
    return this.http.get<any>(this.ApiUrl+"Getall");
  }
  Add(LoginOjb: any) {
    return this.http.post<any>(this.ApiUrl +"AddContractor", LoginOjb);
  }
  Delete(contractor: any) {
    return this.http.post<any>(this.ApiUrl + "Delete", contractor);
  }
}
