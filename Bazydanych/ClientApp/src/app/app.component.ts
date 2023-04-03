import { Component,NgModule } from '@angular/core';
import { LoginService } from './services/login.service'
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})



export class AppComponent {
  title = 'app';
}
