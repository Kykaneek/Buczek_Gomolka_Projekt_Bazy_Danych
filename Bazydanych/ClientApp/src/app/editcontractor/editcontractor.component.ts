import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ContractorService } from '../services/contractor.service';


@Component({
  selector: 'app-editcontractor',
  templateUrl: './editcontractor.component.html',
  styleUrls: ['./editcontractor.component.css']
})

export class EditConcractorComponent implements OnInit {

  public contractors: any = [];

  constructor(private route: Router, private api: ContractorService) {
  }
  ngOnInit(): void {

    this.api.getContractor().subscribe((res: any) => {
      this.contractors = res;
    })

  }

  GoToConcractors() {
    this.route.navigate(['/contractors'])
  }

}
