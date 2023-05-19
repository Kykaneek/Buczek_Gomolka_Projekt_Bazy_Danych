import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
//import { EditPassService } from '../services/api.service';

@Component({
  selector: 'app-editpassword',
  templateUrl: './editpassword.component.html',
  styleUrls: ['./editpassword.component.css']
})

export class EditPasswordComponent implements OnInit {

  constructor(private route: Router, private fb: FormBuilder, private toast: ToastrService) {

  }
  updateform!: FormGroup;
  public passwordedit: any = [];

  ngOnInit(): void {


  }

  /*BackToList() {
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

}*/

}
