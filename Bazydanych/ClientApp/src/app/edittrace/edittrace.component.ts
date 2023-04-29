import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TraceService } from "../services/trace.service";
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr'; import { GetService } from '../services/get.service';


@Component({
  selector: 'app-edittrace',
  templateUrl: './edittrace.component.html',
  styleUrls: ['./edittrace.component.css']
})

export class EditTraceComponent implements OnInit {

  public edittrace: any = [];

  constructor(private route: Router, private service: TraceService, private fb: FormBuilder) {
  }
  tracess: any = [];
  traceForm!: FormGroup;
  ngOnInit(): void {

  }


  Back() {
    var answer = window.confirm("Czy przerwać dodawanie tras?");
    if (answer) {

      this.route.navigate(['/traces'])
    }
    else {

    }

  }

  Resignate() {
    this.route.navigate(['traces']);
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
