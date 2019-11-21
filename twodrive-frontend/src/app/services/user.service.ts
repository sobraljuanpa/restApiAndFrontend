import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { UserAdapter, User } from '../models/user';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
private baseUrl = `${environment.apiUrl}/users`;

  constructor(
    private http: HttpClient,
    private adapter: UserAdapter
  ) { }

  getUsers(): Observable<User[]>{
    return this.http.get(`${this.baseUrl}`).pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item))),
    );
  }

  getModifications(id: number) {
    return this.http.get(`${this.baseUrl}/${id}/reports?startDate=&finishDate=`);
  }

  getTop10Users(): Observable<User[]>{
    return this.http.get(`${environment.apiUrl}/files/top10`).pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item))),
    )
  }

  getUser(id: number): Observable<User> {
    return this.http.get(`${this.baseUrl}/${id}`).pipe(
      map(item => this.adapter.adapt(item))
    );
  }

  getUsersFriends(): Observable<User[]> {
    return this.http.get(`${this.baseUrl}/friends`).pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item))),
    )
  }

  DeleteFriend(id:Number){
    return this.http.delete(`${this.baseUrl}/friend/${id}`);
  }

  DeleteUser(id: Number){
    return this.http.delete(`${this.baseUrl}/${id}`);
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

  addFriend(id: number) {
    return this.http.post(`${this.baseUrl}/${id}`,{});
  }
}
