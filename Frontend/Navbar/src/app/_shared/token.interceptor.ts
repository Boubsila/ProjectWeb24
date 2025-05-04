import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { TokenService } from './token.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private tokenService: TokenService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    console.log('From interceptor : '+ request);
    const token = this.tokenService.getToken();
//modifier le token dans le header
    if (token !== null) {
     let clone = request.clone({
      headers: request.headers.set('Authorization', 'Bearer ' + token)
    })
    console.log('From interceptor2 : '+ clone);
//catch error 401
    return next.handle(clone).pipe(
      catchError(error=> {
        if (error.status === 401) {
          
          this.tokenService.clearToken();
        }
        return throwError('Session expired');
      
      })
    )
  }
    return next.handle(request);
  }
}

export const TokenInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: TokenInterceptor,
  multi: true
}
