

import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule, Validators } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { Component } from '@angular/core';
import { FormControl, FormGroup} from '@angular/forms';
import { TipService } from './tip.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  title = 'monkey-ui';
  status: string | undefined;
  errorMessage: string | undefined;
  requestFinished = false;
  requestValid =false;
  newForm = new FormGroup({
    fieldVal: new FormControl('1',{validators: [Validators.required]})
  });
  
  public monkeyTable: any;
// tip: any;
  constructor(private tipService: TipService){

  }

  ngOnInit(){

  }

  onSearch(){
    let enteredId = this.newForm.get('fieldVal')?.value;
    this.tipService.getMonkeyTable(enteredId).subscribe(
      (      data: {
        TipDetail: any; Status: string | undefined; 
}[]) => {
        this.monkeyTable = data[0].TipDetail;
        this.status = data[0].Status;
        if (this.status === "404" || this.status === "Error"){
          this.errorMessage = "Invalid Id" + enteredId + "! Enter a valid one."
          this.requestValid = false;
        }
        else{
          this.errorMessage = "";
          this.requestValid = true;
        }
      },
      () => {
        this.errorMessage = "Unexpected Error Occurred!";
        this.requestValid = false;
        console.log(this.errorMessage);
      }
    )
    
  }
}
