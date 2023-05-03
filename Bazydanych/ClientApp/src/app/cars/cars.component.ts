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


  Edit(car:any): void {
    this.api.setCar(car);
    this.route.navigate(['/editcars']);
  }

  Delete(car: any): void {
    var answer = window.confirm("Czy chcesz usunąć pojazd?");
    if (answer) {
      this.api.deleteCars(car).subscribe((res: any) => {
        car.reload();
      })
    }
    else {

    }

  }


}
