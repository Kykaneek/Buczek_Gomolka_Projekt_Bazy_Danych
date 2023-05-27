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

  public edittrace: any ;
  startloc: any;
  finishloc: any;

  constructor(private route: Router, private service: TraceService, private fb: FormBuilder,private Get: GetService) {
  }
  edittraced: any = [];
  traceForm!: FormGroup;
  ngOnInit(): void {
    this.edittrace = this.fb.group({
      Id: [this.service.traceId, Validators.required],
      ContractorId: [, Validators.required],
      StartLocation: [, Validators.required],
      FinishLocation: [, Validators.required],
      distance: [, Validators.required],
      TravelTime: [, Validators.required]
    }, { initialValueIsDefault: false })

    this.service.GetTraceEdit(this.service.traceId).subscribe((res: any) => {
      this.edittrace = res;
    })


    this.service.UnsetTrace();
  }


  Back() {
    var answer = window.confirm("Czy przerwać podgląd trasy?");
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
