import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { TracesComponent } from './traces/traces.component';
import { EditPlannedTracesComponent } from './editplannedtraces/editplannedtraces.component';
import { PlanTraceComponent } from './plantrace/plantrace.component';
import { CarsComponent } from './cars/cars.component';
import { AddusersComponent } from './addusers/addusers.component';
import { ContractorsComponent } from './contractors/contractors.component';
import { LocationComponent } from './location/location.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guard/auth.guard';
import { UsersComponent } from './users/users.component';
import { PlannedTracesComponent } from './planned_traces/planned_traces.component';
import { TokenInterceptor } from './Intercepters/token.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EditUserComponent } from './edituser/edituser.component';
import { EditCarsComponent } from './editcars/editcars.component';
import { EditTraceComponent } from './edittrace/edittrace.component';
import { EditConcractorComponent } from './editcontractor/editcontractor.component';
import { EditLocationComponent } from './editlocation/editlocation.component';
import { AddConcractorComponent } from './addcontractor/addcontractor.component';
import { AddtraceComponent } from './addtrace/addtrace.component';
import { AddCarsComponent } from './addcars/addcars.component';
import { AddLocationComponent } from './addlocation/addlocation.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', },
  { path: 'login', component: LoginComponent, runGuardsAndResolvers: 'always' },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
  { path: 'traces', component: TracesComponent, canActivate: [AuthGuard] },
  { path: 'plantrace', component: PlanTraceComponent, canActivate: [AuthGuard] },
  { path: 'cars', component: CarsComponent, canActivate: [AuthGuard] },
  { path: 'contractors', component: ContractorsComponent, canActivate: [AuthGuard] },
  { path: 'planned_traces', component: PlannedTracesComponent, canActivate: [AuthGuard] },
  { path: 'editplannedtraces', component: EditPlannedTracesComponent, canActivate: [AuthGuard] },
  { path: 'location', component: LocationComponent, canActivate: [AuthGuard] },
  { path: 'adduser', component: AddusersComponent, canActivate: [AuthGuard] },
  { path: 'edituser', component: EditUserComponent, canActivate: [AuthGuard] },
  { path: 'editcars', component: EditCarsComponent, canActivate: [AuthGuard] },
  { path: 'edittrace', component: EditTraceComponent, canActivate: [AuthGuard] },
  { path: 'editlocation', component: EditLocationComponent, canActivate: [AuthGuard] },
  { path: 'editcontractor', component: EditConcractorComponent, canActivate: [AuthGuard] },
  { path: 'addcontractor', component: AddConcractorComponent, canActivate: [AuthGuard] },
  { path: 'addtrace', component: AddtraceComponent, canActivate: [AuthGuard] },
  { path: 'addcars', component: AddCarsComponent, canActivate: [AuthGuard] },
  { path: 'addlocation', component: AddLocationComponent, canActivate: [AuthGuard] }
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
    PlannedTracesComponent,
    EditPlannedTracesComponent,
    PlanTraceComponent,
    AddusersComponent,
    TracesComponent,
    EditUserComponent,
    EditConcractorComponent,
    AddConcractorComponent,
    AddLocationComponent,
    AddtraceComponent,
    AddCarsComponent,
    EditCarsComponent,
    EditTraceComponent,
    AddLocationComponent
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
