import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from '../services/order.service';


@Component({
  selector: 'app-editorders',
  templateUrl: './editorders.component.html',
  styleUrls: ['./editorders.component.css']
})


export class EditOrderComponent implements OnInit {

  constructor(private route: Router, private api: OrderService, private fb: FormBuilder, private toast: ToastrService) { }
  updateform!: FormGroup;

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  Back() {

    var answer = window.confirm("Czy przerwać podgląd zlecenia?");
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


  EditCar() {
    /*if (this.updateform.valid) {
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


    */
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
