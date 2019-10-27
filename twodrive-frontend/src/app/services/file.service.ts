import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { HttpClient, HttpResponse } from "@angular/common/http";

import { File, FileAdapter } from "../models/file";
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  private baseUrl = 'http://localhost:57902/api/files';

  constructor(private http: HttpClient, private adapter: FileAdapter) { }

  getOwnedFiles(): Observable<File[]> {
    const url = this.baseUrl + '/view?fileName=&sortOrder=';

    return this.http.get('http://localhost:57902/api/files/view?fileName=&sortOrder=').pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item))),
    );
  }

  getFile(id: number): Observable<File> {
    return this.http.get(`http://localhost:57902/api/files/${id}`).pipe(
      map(item => this.adapter.adapt(item))
    );
  }

  deleteFile(id: number) {
    return this.http.delete(`http://localhost:57902/api/files/${id}`);
  }

  updateFile(id: number, fileName: string, fileContent: string){
    return this.http.put(`http://localhost:57902/api/files/${id}`, {
      name: fileName,
      content: fileContent
    });
  }
}
