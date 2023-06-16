import { Component, OnInit } from '@angular/core';
import { DataRunnerService } from '../data-runner.service';
@Component({
  selector: 'app-values',
  templateUrl: './values.component.html',
  styleUrls: ['./values.component.css']
})
export class ValuesComponent implements OnInit {
  values: any[] = []; // Array to hold all values
  searchTerm: number | undefined; // Search term for the search form
  searchedValue: any; // Value found by the search
  constructor(private dataRunnerService: DataRunnerService) { }

  ngOnInit(): void {
    // Get all values on initialization
    this.dataRunnerService.getValues().subscribe(values => {
      this.values = values;
    });
  }

  // Method to perform the search  
    search(): void {
      this.dataRunnerService.getValue(this.searchTerm!).subscribe(value =>{
        this.searchedValue = value;
      });
    }
}
