import { Component, OnInit } from '@angular/core';
import { DataRunnerService } from '../data-runner.service';
@Component({
  selector: 'app-values',
  templateUrl: './values.component.html',
  styleUrls: ['./values.component.css']
})
export class ValuesComponent implements OnInit {
values: any[]=[];
constructor(private dataRunnerService: DataRunnerService) {}

ngOnInit(): void {
  this.dataRunnerService.getValues().subscribe(values => {
    this.values=values;
 });
    
}
}
