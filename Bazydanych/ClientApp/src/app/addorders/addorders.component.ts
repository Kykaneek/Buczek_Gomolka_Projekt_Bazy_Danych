import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GetService } from '../services/get.service';
import { OrderService } from '../services/order.service';


@Component({
  selector: 'app-addorders',
  templateUrl: './addorders.component.html',
  styleUrls: ['./addorders.component.css']
})


export class AddOrderComponent implements OnInit {

  constructor(private route: Router, private api: OrderService, private fb: FormBuilder, private toast: ToastrService, private get: GetService) { }
  order!: FormGroup;
  Traces: any = [];
  Contractors: any = [];
  Cars: any = [];
  ngOnInit(): void {
    this.get.getContractor().subscribe((res: any) => {
      this.Contractors = res;
    })
    this.get.getTraces().subscribe((res: any) => {
      this.Traces = res;
    })
    this.get.GetCars().subscribe((res: any) => {
      this.Cars = res;
    })
    this.order = this.fb.group({
      contractorID: ["", Validators.required],
      TraceId: ["", Validators.required],
      CarId: ["", Validators.required],
      Pickupdate: ["", Validators.required],
      Time_To_Loading: ["", Validators.required],
      Time_To_Unloading: ["", Validators.required],
    })
  }

  Back() {

    var answer = window.confirm("Czy przerwać dodawanie zlecenia?");
    if (answer) {
      //this.updateForm.reset();
      this.route.navigate(['orders'])
    }
    else {

    }

  }

  Resignate(): void {
    this.route.navigate(['/orders']);
  }
  add() {
    if (this.order.valid) {
      this.api.addOrder(this.order.value).subscribe({
        next: (res) => {

          this.order.reset();
          this.route.navigate(['orders'])
          this.toast.success(res.message);


        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
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
