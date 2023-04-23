import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ContractorService } from '../services/contractor.service';


@Component({
  selector: 'app-editcontractor',
  templateUrl: './editcontractor.component.html',
  styleUrls: ['./editcontractor.component.css']
})

export class EditConcractorComponent implements OnInit {


  constructor(private route: Router, private api: ContractorService, private fb: FormBuilder, private toast: ToastrService) {
  }
  updateform!: FormGroup;
  public concratoredit: any = [];
  ngOnInit(): void {

    this.api.GetContractorToUpdate().subscribe((res: any) => {
      this.concratoredit = res;
    })

    this.updateform = this.fb.group({
      Id: [this.api.ContractorID, Validators.required],
      Name: ["", Validators.required],
      Nip: ["", Validators.required],
      Pesel: ["", Validators.required]
    }, { initialValueIsDefault: false })
    this.api.UnsetConcrator();
  }

  GoToConcractors() {
    this.route.navigate(['/contractors'])
  }


  EditConcrator() {
    if (this.updateform.valid) {
      this.api.UpdateConcrator(this.updateform.value).subscribe({
        next: (res) => {

          this.updateform.reset();
          this.route.navigate(['contractors'])
          this.toast.success(res.message);


        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
    } else {
      this.validateAllForm(this.updateform);
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
