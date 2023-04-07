import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public users: any = [];
  constructor(private api:ApiService) { }

  ngOnInit(): void {
    this.api.getUsers().subscribe(res => {
      this.users = res;
    })
  }

}
