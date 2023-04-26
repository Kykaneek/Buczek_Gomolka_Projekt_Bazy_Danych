import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TraceService } from '../services/trace.service';

@Component({
  selector: 'app-traces',
  templateUrl: './traces.component.html',
  styleUrls: ['./traces.component.css']
})
export class TracesComponent implements OnInit {
  public traces: any = [];

  constructor(private route: Router, private api: TraceService) {
  }
  ngOnInit(): void {
    this.api.getTraces().subscribe((res: any) => {
      this.traces = res;
    })
  }

  Edit(): void {
    this.route.navigate(['/addtrace']);
  }
  Delete(): void {

  }

}
