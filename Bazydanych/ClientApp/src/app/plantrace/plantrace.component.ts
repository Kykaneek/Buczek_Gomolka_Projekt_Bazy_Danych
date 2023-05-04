import { Component} from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-plantrace',
  templateUrl: './plantrace.component.html',
  styleUrls: ['./plantrace.component.css']
})
export class PlanTraceComponent {

  constructor(private route: Router) { }
  Resignate() {
    this.route.navigate(['planned_traces'])

  }
  Back() {

    var answer = window.confirm("Czy przerwać dodawanie trasy?");
    if (answer) {
   
      this.route.navigate(['planned_traces'])
    }
    else {

    }

  }


}
