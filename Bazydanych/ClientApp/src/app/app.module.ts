import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { TracesComponent } from './traces/traces.component';
import { CarsComponent } from './cars/cars.component';
import { AddusersComponent } from './addusers/addusers.component';
import { ContractorsComponent } from './contractors/contractors.component';
import { LocationComponent } from './location/location.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guard/auth.guard';
import { UsersComponent } from './users/users.component';
import { TokenInterceptor } from './Intercepters/token.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EditUserComponent } from './edituser/edituser.component';
import { EditConcractorComponent } from './editcontractor/editcontractor.component';
import { AddConcractorComponent } from './addcontractor/addcontractor.component';
const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', },
  { path: 'login', component: LoginComponent, runGuardsAndResolvers: 'always' },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
  { path: 'traces', component: TracesComponent, canActivate: [AuthGuard] },
  { path: 'cars', component: CarsComponent, canActivate: [AuthGuard] },
  { path: 'contractors', component: ContractorsComponent, canActivate: [AuthGuard] },
  { path: 'location', component: LocationComponent, canActivate: [AuthGuard] },
  { path: 'adduser', component: AddusersComponent, canActivate: [AuthGuard] },
  { path: 'edituser', component: EditUserComponent, canActivate: [AuthGuard] },
  { path: 'editcontractor', component: EditConcractorComponent, canActivate: [AuthGuard] },
  { path: 'addcontractor', component: AddConcractorComponent, canActivate: [AuthGuard] }
]
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    UsersComponent,
    LoginComponent,
    ContractorsComponent,
    LocationComponent,
    CarsComponent,
    AddusersComponent,
    TracesComponent,
    EditUserComponent,
    EditConcractorComponent,
    AddConcractorComponent
  ],
  imports: [
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes, {
      onSameUrlNavigation: 'reload'
    })
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]


})
export class AppModule { }
