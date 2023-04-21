import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../services/login.service';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-addcontractor',
  templateUrl: './addcontractor.component.html',
  styleUrls: ['./addcontractor.component.css']
})
export class AddConcractorComponent implements OnInit {
  constructor(private route: Router) {
  }
  ngOnInit(): void { }

  Resignate() {
    //this.registerform.reset();
    this.route.navigate(['contractors'])

  }
  Back() {
    //this.registerform.reset();
    var answer = window.confirm("Czy przerwać dodawanie kontrahenta?");
    if (answer) {
      this.route.navigate(['contractors'])
    }
    else {

    }

  }



}
