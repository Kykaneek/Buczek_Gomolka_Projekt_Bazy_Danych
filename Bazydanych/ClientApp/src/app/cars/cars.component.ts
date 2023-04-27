import { Component, OnInit } from '@angular/core';
import { CarService } from '../services/car.service';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit{

  constructor(private api : CarService) {

  }
  public cars: any = []
  ngOnInit() {
    this.api.getCars().subscribe((res: any) => {
      this.cars = res;
    })
  }
  
  

}
