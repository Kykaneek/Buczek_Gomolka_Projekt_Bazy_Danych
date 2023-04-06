import { Component, NgModule, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  auth: any;
  type: string = "password";
  loginform!: FormGroup;
  constructor(private LoginService: LoginService, library: FaIconLibrary,private fb: FormBuilder) {
    library.addIconPacks(fas, far)
  }
  ngOnInit(): void {
    this.loginform = this.fb.group({
      username: ['', Validators.required],
      password: ['',Validators.required]
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
          alert(res.message)
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
