import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {LayoutModule} from './shared/layout/layout.module';
import {HttpClientModule} from '@angular/common/http';
import {SimulationStateService} from "./services/simulation/simulation-state.service";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    HttpClientModule
  ],
  providers: [SimulationStateService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
