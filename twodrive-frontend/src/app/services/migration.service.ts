import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from "@angular/common/http";

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MigrationService {
  private baseUrl = `${environment.apiUrl}/migration`;

  constructor(private http: HttpClient) { }

  addMigration(type:string, path:string[]){
    debugger;
    return this.http.post(`${this.baseUrl}/${type}`, path)
  }
}
