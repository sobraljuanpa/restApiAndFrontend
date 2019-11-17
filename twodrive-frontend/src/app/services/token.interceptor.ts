import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { Observable } from 'rxjs';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public auth: AuthenticationService) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    if(request.url.includes('/authenticate')){
      return next.handle(request);
    } else {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.auth.getCredentials().token}`
        }
      });
      return next.handle(request);
    }}
}