import { Component, NgModule, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  
  auth: any;
  type: string = "password";
  loginform!: FormGroup;
  constructor(private LoginService: LoginService,private fb: FormBuilder,private route:Router) {
  }
  ngOnInit(): void {
    this.loginform = this.fb.group({
      Login: ["", Validators.required],
      Pass: ["",Validators.required]
      })
  }
 
  isExpanded = false;
  
  Showpass() {

  }
  checkpass() {

  }
  //logowanie 
  onLogin() {
    if (this.loginform.valid) {
      this.LoginService.Login(this.loginform.value).subscribe({
        next: (res) => {
          alert(res.message);
          this.loginform.reset();
          this.LoginService.storetoken(res.token);
          this.route.navigate(['/users']);
        },
        error: (err) => {
          alert(err!.error.message)
        }
        })
    } else {
      this.validateAllForm(this.loginform);
      alert("Wymagane pola nie są uzupełnione");
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
