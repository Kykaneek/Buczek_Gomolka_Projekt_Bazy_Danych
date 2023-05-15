import { Component, OnInit } from '@angular/core';
import { Toast, ToastrConfig, ToastrService } from 'ngx-toastr';
import { ApiService } from '../services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public users: any = [];
  constructor(private api: ApiService, private route: Router, private toast: ToastrService) {

}

  ngOnInit(): void {
    this.api.getUsers().subscribe((res: any) => {
      this.users = res;
    })
  }
  DeleteUser(User: any) {
    var answer = window.confirm("Czy chcesz usunąć użytkownika?");
    if (answer) {
      this.api.DeleteUser(User).subscribe({
        next: (res) => {
          location.reload();
        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
        })
    }
  }

  ShowUser(userid: any) {
    this.api.SetUser(userid);
    this.route.navigate(['/edituser']);
  }

}
