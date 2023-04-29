import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addcars',
  templateUrl: './addcars.component.html',
  styleUrls: ['./addcars.component.css']
})
export class AddCarsComponent implements OnInit {

  constructor(private route: Router) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  Back() {

    var answer = window.confirm("Czy przerwać dodawanie pojazdu?");
    if (answer) {
      //this.carForm.reset();
      this.route.navigate(['cars'])
    }
    else {

    }

  }

  Resignate(): void {
    this.route.navigate(['/cars']);
  }


}
