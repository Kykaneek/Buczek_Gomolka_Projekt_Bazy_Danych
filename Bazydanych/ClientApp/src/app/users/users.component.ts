import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public users: any = [];
  constructor(private api: ApiService, private toast: ToastrService) {

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


}
