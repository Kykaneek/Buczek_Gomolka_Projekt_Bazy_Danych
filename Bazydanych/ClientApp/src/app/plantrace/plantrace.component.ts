import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PlannedTraceService } from '../services/plantrace.service';


@Component({
  selector: 'app-plantrace',
  templateUrl: './plantrace.component.html',
  styleUrls: ['./plantrace.component.css']
})
export class PlanTraceComponent {

  constructor(private route: Router, private service: PlannedTraceService, private fb: FormBuilder,  private toast: ToastrService) { }
  Resignate() {
    this.route.navigate(['planned_traces'])

  }
  Back() {

    var answer = window.confirm("Czy przerwać dodawanie trasy?");
    if (answer) {
   
      this.route.navigate(['planned_traces'])
    }
    else {

    }

  }


}
