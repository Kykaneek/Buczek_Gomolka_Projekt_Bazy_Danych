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
  constructor(private route: Router) {
  }
    ngOnInit(): void {}
 
  BackToList() {
    this.route.navigate(['/users']);
  }

}
