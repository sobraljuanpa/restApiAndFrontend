import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class MigrationService {
  private baseUrl = 'http://localhost:57902/api/migration';

  constructor(private http: HttpClient) { }

  addMigration(type:string, path:string[]){
    debugger;
    return this.http.post(`${this.baseUrl}/${type}`, path)
  }
}
