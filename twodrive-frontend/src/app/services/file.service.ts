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
    return this.http.get('http://localhost:57902/api/files/view?fileName=&sortOrder=').pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item))),
    );
  }

  getFilesByName(fileName: string): Observable<File[]> {
    return this.http.get(`http://localhost:57902/api/files/view?fileName=${fileName}&sortOrder=`).pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item))),
    );
  }

  getFilesBySortOrder(sortOrder: string): Observable<File[]> {
    return this.http.get(`http://localhost:57902/api/files/view?fileName=&sortOrder=${sortOrder}`).pipe(
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

  addFile(fileName: string, fileContent: string, parentFolder: number, userId: number) {
    return this.http.post('http://localhost:57902/api/files', {
      name: fileName,
      content: fileContent,
      ownerId: userId,
      parent: { id: parentFolder }
    });
  }
}
