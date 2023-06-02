import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrderComponent implements OnInit{

 
  constructor(private api: OrderService, private route: Router) {

  }

  public orders: any = []
  ngOnInit(): void {
    this.api.GetOrders().subscribe((res: any) => {
      this.orders = res;
    })
  }
  

  Edit(order:any): void {
    this.api.updateOrder(order);
    this.route.navigate(['/editorders']);
  }

  
  Delete(order: any): void {
    var answer = window.confirm("Czy chcesz usunąć zlecenie?");
    if (answer) {
      this.api.deleteOrder(order).subscribe((res: any) => {
        order.reload();
      })
    }
    else {

    }

  }
  

}
