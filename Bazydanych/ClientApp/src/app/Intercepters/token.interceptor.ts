import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private auth: LoginService, private router: Router,private toast:ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const mytoken = this.auth.gettoken();
    if (mytoken) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${mytoken}` }
      });
    }
    return next.handle(request).pipe(
      catchError((err: any) => {
        if (err instanceof HttpErrorResponse) {
      if (err.status === 401) {
        this.toast.warning("Sesja wygasła zaloguj się ponownie");
        localStorage.clear();
        this.router.navigate(['/login'])
      }
        }
        return throwError(err);
  })
    );
  }
}
