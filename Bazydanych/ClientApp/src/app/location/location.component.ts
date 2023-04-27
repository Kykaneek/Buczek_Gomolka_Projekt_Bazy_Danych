import { Component, OnInit } from '@angular/core';
import { Router, } from '@angular/router';
import { LocationService } from '../services/location.service';


@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})

export class LocationComponent implements OnInit {
  public locations: any = [];


  constructor(private route: Router, private api: LocationService) {
  }
  ngOnInit(): void {
    this.api.getLocation().subscribe((res: any) => {
      this.locations = res;
    })
  } 

  Edit(/*locations: any*/): void {
    /*this.api.setLocation(locations);*/
    this.route.navigate(['/addlocation']);
  }

  Delete(): void {
    

  }

} 



