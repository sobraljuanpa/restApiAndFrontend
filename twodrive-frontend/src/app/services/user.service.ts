import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map, last } from "rxjs/operators";
import { HttpClient, HttpResponse } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {
private baseUrl = 'http://localhost:57902/api/users';

  constructor(
    private http: HttpClient
  ) { }

  addUser(
    firstName: string,
    lastName: string,
    username: string,
    password: string,
    email: string,
    role: string
  ){
    return this.http.post(`${this.baseUrl}`, {
      firstName: firstName,
      lastName: lastName,
      username: username,
      password: password,
      email: email,
      role: role
    });
  }
}
