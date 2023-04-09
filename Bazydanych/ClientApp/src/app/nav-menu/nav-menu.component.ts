import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';



@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent {
  


  public IsLogin: boolean = this.LoginService.isLogIn();
  isExpanded = false;
  constructor(private router: Router, private LoginService: LoginService, private toast: ToastrService) { }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  logout() {
    localStorage.clear();
    this.IsLogin = this.LoginService.isLogIn();
    this.toast.info('Wylogowano z systemu');
    this.router.navigate(['/login']);
  }
}
