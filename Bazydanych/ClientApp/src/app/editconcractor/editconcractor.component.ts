import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-editconcractor',
  templateUrl: './editconcractor.component.html',
  styleUrls: ['./editconcractor.component.css']
})

export class EditConcractorComponent implements OnInit {
  constructor(private route: Router) {
  }
  ngOnInit(): void { }

  GoToConcractors() {
    this.route.navigate(['/contractors'])
  }

}
