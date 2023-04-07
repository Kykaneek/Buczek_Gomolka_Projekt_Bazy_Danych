import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private auth: LoginService,private router: Router) {

  }
  canActivate():boolean {
    if (this.auth.isLogIn()) {
      return true;
    }
    this.router.navigate(['']);
    return false;
  }
  
  
}
