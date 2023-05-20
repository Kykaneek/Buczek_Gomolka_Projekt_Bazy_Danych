import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  carID: any;
  driverID: any;
  readonly ApiUrl = "https://localhost:44449/api/Car/";
  constructor(private http: HttpClient) { }
  getCars() {
    return this.http.get<any>(this.ApiUrl + "Getall");
  }

  addCars(pojazd: any)
  {
    return this.http.post<any>(this.ApiUrl + "addCar", pojazd);
  }
  setCar(carid: any,driverID:any) {
    this.carID = carid;
    this.driverID = driverID;
  }

  deleteCars(pojazd: any) {
    return this.http.post<any>(this.ApiUrl + "DeleteCar", pojazd);
  }
  UnsetCar() {
    this.carID = null;
    this.driverID = null;
  }
  GetCarToUpdate() {
    let queryParams = { "car": this.carID };
    return this.http.get<any>(this.ApiUrl + "getCar", { params: queryParams });

  }
  updateCar(car: any) {
    return this.http.put<any>(this.ApiUrl + "update", car);
  }
}
