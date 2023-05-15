import { Component, OnInit } from '@angular/core';
import { Router, } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ContractorService } from '../services/contractor.service';

@Component({
  selector: 'app-contractors',
  templateUrl: './contractors.component.html',
  styleUrls: ['./contractors.component.css']
})

export class ContractorsComponent implements OnInit {
  public contractors: any = [];


  constructor(private route: Router,private api: ContractorService,private toast: ToastrService) {
  }
  ngOnInit(): void {
    const { search } = window.location;
    const deleteSuccess = (new URLSearchParams(search)).get('deleteSuccess');
    this.api.getContractor().subscribe((res: any)=> {
      this.contractors = res;
    })
    if (deleteSuccess === '1') {
      this.toast.success("Poprawnie usunięto")

    }
  } 

  Edit(contractor: any): void {
    this.api.SetConcrator(contractor);
    this.route.navigate(['/editcontractor']);
  }
  Delete(contractor: any): void {
    var answer = window.confirm("Czy chcesz usunąć kontrahenta?");
    if (answer) {
      this.api.Delete(contractor).subscribe({
        next: (res) => {
          window.location.href = window.location.pathname + '?deleteSuccess=1';
        },
        error: (err) => {
          this.toast.error(err!.error.message);
        }
      })
    }
    else {

    }

  }

}


