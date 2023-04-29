import { Component, OnInit } from '@angular/core';
import { CarService } from '../services/car.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit{

  constructor(private api : CarService, private route: Router) {

  }
  public cars: any = []
  ngOnInit() {
    this.api.getCars().subscribe((res: any) => {
      this.cars = res;
    })
  }


  Edit(): void {
    //this.api.setCar(vehicle);
    this.route.navigate(['/editcars']);
  }

  Remove(): void {
    //this.api.setCar(vehicle);
    
  }


}
