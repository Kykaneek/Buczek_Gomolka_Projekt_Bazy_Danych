import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ContractorService } from '../services/contractor.service';

@Component({
  selector: 'app-contractors',
  templateUrl: './contractors.component.html',
  styleUrls: ['./contractors.component.css']
})

export class ContractorsComponent implements OnInit {
  public contractors: any = [];


  constructor(private route: Router,private api: ContractorService) {
  }
  ngOnInit(): void {
    this.api.getContractor().subscribe((res: any)=> {
      this.contractors = res;
    })
  } 

  Edit(): void {
    this.route.navigate(['/editcontractor']);
  }
  Delete(contractor: any): void {
    this.api.Delete(contractor).subscribe((res: any) => {
      location.reload();
    })

  }

}
