import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TraceService } from '../services/trace.service';

@Component({
  selector: 'app-addtrace',
  templateUrl: './addtrace.component.html',
  styleUrls: ['./addtrace.component.css']
})
export class AddtraceComponent implements OnInit {
  constructor(private service: TraceService, private fb: FormBuilder, private route: Router, private toast: ToastrService) {
  }
  traceForm!: FormGroup;
  ngOnInit(): void {
    this.traceForm = this.fb.group({
      Kontraktor: [],
      Lokalizacja_Poczatkowa: [],
      Lokalizacja_Koncowa: [],
      Dystans: [],
      Czas: []
    })
  }

  Resignate() {
    this.traceForm.reset();
    this.route.navigate(['traces'])

  }
  Back() {

    var answer = window.confirm("Czy przerwać dodawanie trasy?");
    if (answer) {
      this.traceForm.reset();
      this.route.navigate(['traces'])
    }
    else {

    }

  }

  OnCreate() {
    if (this.traceForm.valid) {
      this.service.Add(this.traceForm.value).subscribe({
        next: (res) => {
          this.traceForm.reset();
          this.route.navigate(['traces'])
          this.toast.success(res.message);


        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
    } else {
      this.validateAllForm(this.traceForm);
      this.toast.error("Wymagane pola nie są uzupełnione");
    }


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
