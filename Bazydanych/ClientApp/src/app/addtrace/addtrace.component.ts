import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ContractorService } from '../services/contractor.service';
import { GetService } from '../services/get.service';
import { TraceService } from '../services/trace.service';

@Component({
  selector: 'app-addtrace',
  templateUrl: './addtrace.component.html',
  styleUrls: ['./addtrace.component.css']
})
export class AddtraceComponent implements OnInit {
  constructor(private service: TraceService, private fb: FormBuilder, private route: Router, private toast: ToastrService,private Get: GetService) {
  }
  traceForm!: FormGroup;
  public contractors: any = [];
  public locations: any= [];
  ngOnInit(): void {
    this.traceForm = this.fb.group({
      ContractorId: [,Validators.required],
      StartLocation: [,Validators.required],
      FinishLocation: [,Validators.required],
      distance: [,Validators.required],
      TravelTime: [,Validators.required]
    })

    this.Get.getContractor().subscribe((res: any) => {
      this.contractors = res;
    })
    this.Get.getLocation().subscribe((res: any) => {
      this.locations = res;
    })
  }

  Resignate() {
    this.traceForm.reset();
    this.route.navigate(['traces'])

  }
  Back() {

    var answer = window.confirm("Czy przerwać dodawanie trasy?");
    if (answer) {
      this.traceForm.reset();
      this.route.navigate(['traces'])
    }
    else {

    }

  }

  OnAdd() {
    if (this.traceForm.valid) {
      this.service.Add(this.traceForm.value).subscribe({
        next: (res) => {
          this.traceForm.reset();
          this.route.navigate(['traces'])
          this.toast.success(res.message);


        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
    } else {
      this.validateAllForm(this.traceForm);
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
