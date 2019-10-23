import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { HttpClient } from "@angular/common/http";

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

    return this.http.get(url).pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item))),
    );
  }
}
