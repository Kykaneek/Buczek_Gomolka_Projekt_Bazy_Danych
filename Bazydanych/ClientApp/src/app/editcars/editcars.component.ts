import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-editcars',
  templateUrl: './editcars.component.html',
  styleUrls: ['./editcars.component.css']
})


export class EditCarsComponent implements OnInit {

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

  Resignate(): void
  {
    this.route.navigate(['/cars']);
  }


}
