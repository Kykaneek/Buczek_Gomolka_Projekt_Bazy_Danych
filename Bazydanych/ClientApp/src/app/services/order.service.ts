import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  readonly Orders = "https://localhost:44449/api/Orders/";

  constructor(private http: HttpClient) { }

   OrderID: any;

  GetOrders() {
    return this.http.get<any>(this.Orders + "Getall");
  }
  
  addOrder(loading: any) {
    return this.http.post<any>(this.Orders + "add", loading);
  }



  deleteOrder(loading: any) {
    return this.http.post<any>(this.Orders + "DeleteLoading", loading);
  }

  updateOrder(loading: any) {
    return this.http.put<any>(this.Orders + "UpdateLoading", loading);
  }


  SetConcrator(contractorid: any) {
    this.OrderID = contractorid;
  }
  GetOrderToUpdate() {
    let queryParams = { "contractor": this.OrderID };
    return this.http.get<any>(this.Orders + "getcontractor", { params: queryParams });

  }
  UpdateOrder(contractor: any) {
    return this.http.put<any>(this.Orders + "update", contractor);

  }
  UnsetOrder() {
    this.OrderID = null;
  }

}

