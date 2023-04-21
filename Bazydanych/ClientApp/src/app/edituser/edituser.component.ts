import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from '../services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edituser',
  templateUrl: './edituser.component.html',
  styleUrls: ['./edituser.component.css']
})

export class EditUserComponent implements OnInit {

  constructor(private route: Router,private user : ApiService) {
    
  }
  ngOnInit(): void {
    const Login = document.getElementById("Login") as HTMLInputElement
    const Phone = document.getElementById("Phone") as HTMLInputElement
    const Licence = document.getElementById("Licence") as HTMLInputElement
    const UserRole = document.getElementById("UserRole") as HTMLInputElement
    const IsDrive = document.getElementById("IsDriver") as HTMLInputElement
    Login.disabled = true
    Phone.disabled = true
    Licence.disabled = true
    UserRole.disabled = true
    IsDrive.disabled = true
    
  }
  BackToList() {
    this.route.navigate(['/users']);
  }
  PasswordChange() {

  }
  EditUser() {
    const Login = document.getElementById("Login") as HTMLInputElement
    const Phone = document.getElementById("Phone") as HTMLInputElement
    const Licence = document.getElementById("Licence") as HTMLInputElement
    const UserRole = document.getElementById("UserRole") as HTMLInputElement
    const IsDrive = document.getElementById("IsDriver") as HTMLInputElement
    
    Login.disabled = false
    Phone.disabled = false
    Licence.disabled = false
    UserRole.disabled = false
    IsDrive.disabled = false
  }
}
