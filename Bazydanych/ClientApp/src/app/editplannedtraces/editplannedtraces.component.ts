import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-editplannedtraces',
  templateUrl: './editplannedtraces.component.html',
  styleUrls: ['./editplannedtraces.component.css']
})
export class EditPlannedTracesComponent {

  constructor(private route: Router) { }
  Resignate() {
    this.route.navigate(['planned_traces'])

  }
  Back() {

    var answer = window.confirm("Czy przerwać podgląd zaplanowanej trasy?");
    if (answer) {

      this.route.navigate(['planned_traces'])
    }
    else {

    }
   

  }



}
