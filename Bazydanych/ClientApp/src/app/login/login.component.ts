import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  auth: any;

  constructor(private LoginService: LoginService) { }
  isExpanded = false;
  ngOnInit() {
    this.auth = this.LoginService.getAllUser();
  }
  
}
