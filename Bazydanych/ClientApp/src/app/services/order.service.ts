import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  readonly Loading = "https://localhost:44449/api/Loading/";
  readonly Unloading = "https://localhost:44449/api/UnLoading/";

  constructor(private http: HttpClient) { }



  getLoadings() {
    return this.http.get<any>(this.Loading + "GetallLoadings");
  }

  getUnloadings() {
    return this.http.get<any>(this.Unloading + "GetallLoadings");
  }
  
  addLoading(loading: any) {
    return this.http.post<any>(this.Loading + "AddLoading", loading);
  }

  addUnLoading(unloading: any) {
    return this.http.post<any>(this.Unloading + "AddUnLoading", unloading);
  }

  deleteLoading(loading: any) {
    return this.http.post<any>(this.Loading + "DeleteLoading", loading);
  }

  deleteUnLoading(unloading: any) {
    return this.http.post<any>(this.Unloading + "DeleteLoading", unloading);
  }

  updateLoading(loading: any) {
    return this.http.put<any>(this.Loading + "UpdateLoading", loading);
  }

  updateUnLoading(unloading: any) {
    return this.http.put<any>(this.Unloading + "UpdateUnloading", unloading);
  }



}

