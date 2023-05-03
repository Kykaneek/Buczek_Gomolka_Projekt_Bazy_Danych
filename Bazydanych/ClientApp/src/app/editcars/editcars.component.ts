import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CarService } from '../services/car.service';


@Component({
  selector: 'app-editcars',
  templateUrl: './editcars.component.html',
  styleUrls: ['./editcars.component.css']
})


export class EditCarsComponent implements OnInit {

  constructor(private route: Router,private api: CarService, private fb: FormBuilder) { }
  updateform!: FormGroup;
  public useredit: any = [];
  ngOnInit(): void {

    this.api.GetCarToUpdate().subscribe((res: any) => {
      this.useredit = res;
    })

    this.updateform = this.fb.group({
      Id: [this.api.carID, Validators.required],
      Driver: ["", Validators.required],
      Registration_Number: ["", Validators.required],
      Mileage: ["", Validators.required],
      Buy_Date: ["", Validators.required],
      Is_Truck: ["", Validators.required],
      Loadingsize: ["", Validators.required],
      IsAvailable: ["", Validators.required],
    }, { initialValueIsDefault: false })
    this.api.UnsetCar();
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
