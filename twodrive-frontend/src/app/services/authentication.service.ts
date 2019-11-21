import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt'

import { Credentials } from '../models/credentials';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  credentials: Credentials;
  helper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  logIn(user: string, pass: string) {
    return this.http.post(`${environment.apiUrl}/users/authenticate`, {
      username: user,
      password: pass
    });
  }

  logOut() {
    this.setCredentials(null);
  }

  getCredentials(): Credentials {
    return JSON.parse(localStorage.getItem('credentials'))
  }

  setCredentials(cred: Credentials) {
    this.credentials = cred;
    localStorage.setItem('credentials', JSON.stringify(this.credentials));
  }

  isAuthenticated(): boolean {
    const cred = this.getCredentials();
    return this.helper.isTokenExpired(cred.token);
  }

}
