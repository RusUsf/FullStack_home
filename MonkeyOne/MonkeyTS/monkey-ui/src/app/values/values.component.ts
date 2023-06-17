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
  newValue: any = {}; // New value to create
  constructor(private dataRunnerService: DataRunnerService) { }

  ngOnInit(): void {
    // Get all values on initialization
    this.dataRunnerService.getValues().subscribe(values => {
      this.values = values;
    });
  }

  // Method to perform the search  
  search(): void {
    this.dataRunnerService.getValue(this.searchTerm!).subscribe(value => {
      this.searchedValue = value;
    });
  }

  // Method to create a new value
  create(): void {
    console.log('newValue before sending to server: ', this.newValue);
    // Create a copy of the new value object without the id property
    const valueToCreate = { ...this.newValue };
    delete valueToCreate.id;
    this.dataRunnerService.createValue({ value: this.newValue }).subscribe(value => {
      // Add the new value to the values array
      this.values.push(value);

      // Clear the new value
      this.newValue = {};
    });
  }
}
