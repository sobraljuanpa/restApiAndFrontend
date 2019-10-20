import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  logIn(user: string, pass: string) {
    return this.http.post('http://localhost:57902/api/users/authenticate', {
      username: user,
      password: pass
    });
  }
}
