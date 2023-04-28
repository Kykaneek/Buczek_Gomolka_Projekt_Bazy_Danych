import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})


export class LocationService {

  readonly ApiUrl = "https://localhost:44449/api/Location/";

  constructor(private http: HttpClient) { }


  LocationID: any;
  getLocation() {
    return this.http.get<any>(this.ApiUrl + "getAllLocations");
  }


  addLocation(location: any) {
    return this.http.post<any>(this.ApiUrl + "addLocation", location);
  }


  deleteLocation(location: any) {
    return this.http.post<any>(this.ApiUrl + "deleteLocation", location);
  }

  setLocation(locationid: any) {
    this.LocationID = locationid;
  }

  getLocationToUpdate() {
    let queryParams = { "location": this.LocationID };
    return this.http.get<any>(this.ApiUrl + "getLocation", { params: queryParams });

  }
}
