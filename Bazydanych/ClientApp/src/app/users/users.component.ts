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
    const { search } = window.location;
    const deleteSuccess = (new URLSearchParams(search)).get('deleteSuccess');
    this.api.getUsers().subscribe((res: any) => {
      this.users = res;
    })
    if (deleteSuccess === '1') {
      this.toast.success("Poprawnie usunięto")
    }

  }
  DeleteUser(User: any) {
    var answer = window.confirm("Czy chcesz usunąć użytkownika?");
    if (answer) {
      this.api.DeleteUser(User).subscribe({
        next: (res) => {
          window.location.href = window.location.pathname + '?deleteSuccess=1';
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
