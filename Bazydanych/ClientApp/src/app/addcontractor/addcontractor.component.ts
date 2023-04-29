import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ContractorService } from '../services/contractor.service';

@Component({
  selector: 'app-addcontractor',
  templateUrl: './addcontractor.component.html',
  styleUrls: ['./addcontractor.component.css']
})
export class AddConcractorComponent implements OnInit {
  constructor(private service: ContractorService, private fb: FormBuilder, private route: Router, private toast: ToastrService) {
  }
  contractorForm!: FormGroup;
  ngOnInit(): void {
    this.contractorForm = this.fb.group({
      Name: ["", Validators.required],
      Nip: [],
      Pesel: []
    })
}

  Resignate() {
    this.contractorForm.reset();
    this.route.navigate(['contractors'])
  }

  Back() {
    
    var answer = window.confirm("Czy przerwać dodawanie kontrahenta?");
    if (answer) {
      this.contractorForm.reset();
      this.route.navigate(['contractors'])
    }
    else {

    }

  }

  OnCreate() {
    if (this.contractorForm.valid) {
      this.service.Add(this.contractorForm.value).subscribe({
        next: (res) => {
          this.contractorForm.reset();
          this.route.navigate(['contractors'])
          this.toast.success(res.message);


        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
    } else {
      this.validateAllForm(this.contractorForm);
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
