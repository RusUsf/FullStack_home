import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ValuesComponent } from './values/values.component';
import { DataRunnerService } from './data-runner.service';

@NgModule({
  declarations: [
    AppComponent,
    ValuesComponent  // Declaring the ValuesComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule, // Adding the HttpClientModule to the imports array
    FormsModule // Adding the FormsModule to the imports array
  ],
  providers: [DataRunnerService], // Adding the DataRunnerService to the providers array
  bootstrap: [AppComponent]
})
export class AppModule { }
