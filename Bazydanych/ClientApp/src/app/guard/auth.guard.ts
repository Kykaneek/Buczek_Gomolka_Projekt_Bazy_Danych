import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from '../services/login.service';
import { RegisterService } from '../services/register.service';
import { ApiService } from '../services/api.service'
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth: LoginService,private router: Router,private auth2: RegisterService,private auth3: ApiService) {

  }
  canActivate():boolean {
    if (this.auth.isLogIn()) {
      return true;
    }
    if (this.auth2.isLogIn()) {
      return true;
    }
    if (this.auth3.isLogIn()) {
      return true;
    }
    this.router.navigate(['']);
    return false;
  }
  
  
}
