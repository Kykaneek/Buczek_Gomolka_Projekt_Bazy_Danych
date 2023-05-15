import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocationService } from "../services/location.service";
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';import { GetService } from '../services/get.service';
;

@Component({
  selector: 'app-addlocation',
  templateUrl: './addlocation.component.html',
  styleUrls: ['./addlocation.component.css']
})
export class AddLocationComponent implements OnInit {

  public addlocations: any = [];

  constructor(private route: Router, private service: LocationService, private fb: FormBuilder, private toast: ToastrService,private Get: GetService) {
  }
  contractors: any =[];
  locationForm!: FormGroup;
  ngOnInit(): void {
    this.locationForm = this.fb.group({
      Name: [,Validators.required],
      City: [,Validators.required],
      Street: [,Validators.required],
      Number: [, Validators.required],
      contractorID:[0]
    })
    this.Get.getContractor().subscribe((res: any) => {
      this.contractors = res;
    })
  }


  addLocation() {

    if (this.locationForm.valid) {
      this.service.addLocation(this.locationForm.value).subscribe({
        next: (res) => {
          this.locationForm.reset();
          this.route.navigate(['location'])
          this.toast.success(res.message);


        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
    } else {
      this.validateAllForm(this.locationForm);
      this.toast.error("Wymagane pola nie są uzupełnione");
    }


  }


  Back() {
    var answer = window.confirm("Czy przerwać dodawanie lokalizacji?");
    if (answer) {
      
      this.route.navigate(['location'])
    }
    else {

    }

  }

  Resignate() {
    this.route.navigate(['location']);
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
