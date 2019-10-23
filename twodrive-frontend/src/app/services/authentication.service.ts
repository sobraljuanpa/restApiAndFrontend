import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Credentials } from '../models/credentials';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  credentials: Credentials;

  constructor(private http: HttpClient) { }

  logIn(user: string, pass: string) {
    return this.http.post('http://localhost:57902/api/users/authenticate', {
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

}
