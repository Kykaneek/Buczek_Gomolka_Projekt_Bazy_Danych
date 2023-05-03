import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CarService } from '../services/car.service';
import { GetService } from '../services/get.service';


@Component({
  selector: 'app-editcars',
  templateUrl: './editcars.component.html',
  styleUrls: ['./editcars.component.css']
})


export class EditCarsComponent implements OnInit {

  constructor(private route: Router, private api: CarService, private fb: FormBuilder, private Get: GetService, private toast: ToastrService) { }
  updateform!: FormGroup;
  public useredit: any = [];
  public Drivers: any = [];
  ngOnInit(): void {

    this.api.GetCarToUpdate().subscribe((res: any) => {
      this.useredit = res;
    })
    this.Get.GetDrivers().subscribe((res: any) => {
      this.Drivers = res;
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

  Resignate(): void {
    this.route.navigate(['/cars']);
  }


  EditCar() {
    if (this.updateform.valid) {
      this.api.updateCar(this.updateform.value).subscribe({
        next: (res) => {

          this.updateform.reset();
          this.route.navigate(['cars'])
          this.toast.success(res.message);


        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
    } else {
      this.validateAllForm(this.updateform);
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
