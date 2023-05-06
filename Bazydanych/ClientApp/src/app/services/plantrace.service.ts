import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class PlannedTraceService {
  plannedTraceID: any;
  readonly ApiUrl = "https://localhost:44449/api/PlannedTrace/";
  constructor(private http: HttpClient) { }

  getPlannedTraces() {
    return this.http.get<any>(this.ApiUrl + "Getall");
  }

  addPlannedTrace(trace: any) {
    return this.http.post<any>(this.ApiUrl + "Add", trace);
  }

  deletePlannedTrace(trace: any) {
    return this.http.post<any>(this.ApiUrl + "Delete", trace);
  }

  UnsetPlannedTrace() {
    this.plannedTraceID = null;
  }

  GetPlannedTraceToUpdate() {
    let queryParams = { "trace": this.plannedTraceID };
    return this.http.get<any>(this.ApiUrl + "getTrace", { params: queryParams });
  }

  updatePlannedTrace(trace: any) {
    return this.http.put<any>(this.ApiUrl + "Update", trace);
  }

}
