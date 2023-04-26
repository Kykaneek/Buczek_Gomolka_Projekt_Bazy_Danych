import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-addusers',
  templateUrl: './addusers.component.html',
  styleUrls: ['./addusers.component.css']
})
export class AddusersComponent implements OnInit {
  constructor(private Register: RegisterService, private fb: FormBuilder, private route: Router, private toast: ToastrService) {
  }
  registerform!: FormGroup;
  ngOnInit(): void {
    this.registerform = this.fb.group({
      Login: ["", Validators.required],
      Pass: ["", Validators.required],
      VerPass: ["", Validators.required],
      Phone: [],
      is_Driver: ["", Validators.required],
      Licence: ["", Validators.required],
      UserRole: ["", Validators.required],
    })
  }
  Resignate() {
    this.registerform.reset();
    this.route.navigate(['users'])
    
  }
  Back() {
    
    var answer = window.confirm("Czy przerwać dodawanie użytkownika?");
    if (answer) {
      this.registerform.reset();
      this.route.navigate(['users'])
    }
    else {
     
    }
    
  }
  onRegister() {
    if (this.registerform.valid) {
      this.Register.Register(this.registerform.value).subscribe({
        next: (res) => {

          this.registerform.reset();
          this.route.navigate(['users'])
          this.toast.success(res.message);


        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
    } else {
      this.validateAllForm(this.registerform);
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
