import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CarService } from '../services/car.service';


@Component({
  selector: 'app-addcars',
  templateUrl: './addcars.component.html',
  styleUrls: ['./addcars.component.css']
})
export class AddCarsComponent implements OnInit {

  constructor(private route: Router, private fb: FormBuilder, private service: CarService, private toast: ToastrService ) { }
  carform!: FormGroup;
  ngOnInit(): void {
    this.carform = this.fb.group({
      Driver: [],
      RegistrationNumber: [],
      Mileage: [],
      BuyDate: [],
      IsTruck: [],
      Loadingsize: [],
      IsAvailable: [],
    })
  }


  Back() {

    var answer = window.confirm("Czy przerwać dodawanie pojazdu?");
    if (answer) {
      this.carform.reset();
      this.route.navigate(['cars'])
    }
    else {

    }

  }

  Resignate(): void {
    this.route.navigate(['/cars']);
  }


  addCars() {
    if (this.carform.valid) {
      this.service.addCars(this.carform.value).subscribe({
        next: (res) => {

          this.carform.reset();
          this.route.navigate(['cars'])
          this.toast.success(res.message);


        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
    } else {
      this.validateAllForm(this.carform);
      this.toast.error("Wymagane pola nie są uzupełnione");
    }
  }


  private validateAllForm(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsDirty({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllForm(control);
      }
    })
  }


}
