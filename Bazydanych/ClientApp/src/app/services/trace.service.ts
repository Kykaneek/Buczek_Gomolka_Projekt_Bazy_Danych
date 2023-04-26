import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TraceService {
  readonly ApiUrl = "https://localhost:44449/api/Trace/";
  constructor(private http: HttpClient) { }
  getTraces() {
    return this.http.get<any>(this.ApiUrl + "Getall");
  }

  Add(trace1: any) {
    return this.http.post<any>(this.ApiUrl + "AddTrace", trace1);
  }
  Delete(trace1: any) {
    return this.http.post<any>(this.ApiUrl + "DeleteDeleteTrace", trace1);
  }

  


}
