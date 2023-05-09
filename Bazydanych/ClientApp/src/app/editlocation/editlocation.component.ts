import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocationService } from "../services/location.service";
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr'; import { GetService } from '../services/get.service';


@Component({
  selector: 'app-editlocation',
  templateUrl: './editlocation.component.html',
  styleUrls: ['./editlocation.component.css']
})
export class EditLocationComponent implements OnInit {

  public addlocations: any = [];

  constructor(private route: Router, private service: LocationService, private fb: FormBuilder) {
  }
  contractors: any = [];
  locationForm!: FormGroup;
  ngOnInit(): void {
    
  }


  Back() {
    var answer = window.confirm("Czy przerwać podgląd lokalizacji?");
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
