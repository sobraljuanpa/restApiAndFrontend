import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { UserAdapter, User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
private baseUrl = 'http://localhost:57902/api/users';

  constructor(
    private http: HttpClient,
    private adapter: UserAdapter
  ) { }

  getUsers(): Observable<User[]>{
    return this.http.get(`${this.baseUrl}`).pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item))),
    );
  }

  getUser(id: number): Observable<User> {
    return this.http.get(`${this.baseUrl}/${id}`).pipe(
      map(item => this.adapter.adapt(item))
    );
  }

  updateUser(
    id: number,
    firstName: string,
    lastName: string,
    username: string,
    password: string,
    email: string,
    role: string
  ){
    return this.http.put(`${this.baseUrl}/${id}`, {
      firstName: firstName,
      lastName: lastName,
      username: username,
      password: password,
      email: email,
      role: role
    });
  }

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
