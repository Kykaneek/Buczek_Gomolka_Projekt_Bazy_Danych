import { Component,HostListener,NgModule } from '@angular/core';
import { LoginService } from './services/login.service'
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})



export class AppComponent {
  @HostListener("window:beforeunload", ['$event'])
  clearLocalStorage(event: any) {
    localStorage.clear();
  }
  title = 'app';
}
