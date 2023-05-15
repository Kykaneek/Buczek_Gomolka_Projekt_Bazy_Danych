import { Component, OnInit } from '@angular/core';
import { Router, } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from '../services/location.service';


@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})

export class LocationComponent implements OnInit {
  public locations: any = [];


  constructor(private route: Router, private api: LocationService,private toast: ToastrService) {
  }
  ngOnInit(): void {
    this.api.getLocation().subscribe((res: any) => {
      this.locations = res;
    })
    const { search } = window.location;
    const deleteSuccess = (new URLSearchParams(search)).get('deleteSuccess');
    if (deleteSuccess === '1') {
      this.toast.success("Poprawnie usunięto")
    }
  } 

  Edit(/*locations: any*/): void {
    /*this.api.setLocation(locations);*/
    this.route.navigate(['/editlocation']);
  }

  Delete(location: any) {
    var answer = window.confirm("Czy chcesz usunąć użytkownika?");
    if (answer) {
      this.api.deleteLocation(location).subscribe({
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





