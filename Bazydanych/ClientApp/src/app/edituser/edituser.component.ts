import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-edituser',
  templateUrl: './edituser.component.html',
  styleUrls: ['./edituser.component.css']
})

export class EditUserComponent implements OnInit {

  constructor(private route: Router, private fb: FormBuilder, private api: ApiService, private toast: ToastrService) {

  }
  updateform!: FormGroup;
  public useredit: any = [];
 
  ngOnInit(): void {

    this.api.GetUserToUpdate().subscribe((res: any) => {
      this.useredit = res;
    })
    this.updateform = this.fb.group({
      Id: [this.api.userID,Validators.required],
      Login: ["", Validators.required],
      Phone: [],
      is_driver: ["", Validators.required],
      Licence: ["", Validators.required],
      UserRole: ["", Validators.required],
    },{ initialValueIsDefault: false })

    this.api.UnsetUser();
 
  }
     
  BackToList() {
    this.route.navigate(['/users']);
  }
  PasswordChange() {

  }
  EditUsers() {
    if (this.updateform.valid) {
      this.api.UpdateUser(this.updateform.value).subscribe({
        next: (res) => {

          this.updateform.reset();
          this.route.navigate(['users'])
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
