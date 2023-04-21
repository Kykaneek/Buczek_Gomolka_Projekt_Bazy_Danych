import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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
    this.api.DeleteUser(User).subscribe((res: any) => {
      location.reload();
    })
  }

  ShowUser(): void {
    this.route.navigate(['/edituser']);
  }

}
