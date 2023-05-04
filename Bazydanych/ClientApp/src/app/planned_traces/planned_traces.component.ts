import { Component, OnInit } from '@angular/core';
//import { CarService } from '../services/car.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cars',
  templateUrl: './planned_traces.component.html',
  styleUrls: ['./planned_traces.component.css']
})
export class PlannedTracesComponent implements OnInit {

  constructor(/*private api: CarService, */private route: Router) {

  }
  public cars: any = []
  ngOnInit() {
  /*  this.api.getCars().subscribe((res: any) => {
      this.cars = res;
    })*/
  }


  Edit(): void {
    this.route.navigate(['/editplantraces']);
  }

  Delete(): void {
    var answer = window.confirm("Czy chcesz usunąć pojazd?");
    if (answer) {

    }
    else {

    }

  }


}
