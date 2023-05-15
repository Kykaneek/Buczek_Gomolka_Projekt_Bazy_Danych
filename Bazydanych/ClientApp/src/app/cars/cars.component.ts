import { Component, OnInit } from '@angular/core';
import { CarService } from '../services/car.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit{

  constructor(private api : CarService, private route: Router,private toast: ToastrService) {

  }
  public cars: any = []
  ngOnInit() {
    this.api.getCars().subscribe((res: any) => {
      this.cars = res;
    })
    const { search } = window.location;
    const deleteSuccess = (new URLSearchParams(search)).get('deleteSuccess');
    if (deleteSuccess === '1') {
      this.toast.success("Poprawnie usunięto")
    }
  }


  Edit(car:any): void {
    this.api.setCar(car);
    this.route.navigate(['/editcars']);
  }

  Delete(car: any): void {
    var answer = window.confirm("Czy chcesz usunąć pojazd?");
    if (answer) {
      this.api.deleteCars(car).subscribe({
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
