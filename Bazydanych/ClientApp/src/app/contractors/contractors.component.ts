import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contractors',
  templateUrl: './contractors.component.html',
  styleUrls: ['./contractors.component.css']
})

export class ContractorsComponent implements OnInit {
  constructor(private route: Router) {
  }
  ngOnInit(): void { } 

  Edit(): void {
    this.route.navigate(['/editcontractor']);
  }

}
