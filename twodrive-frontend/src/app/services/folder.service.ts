import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { HttpClient, HttpResponse } from "@angular/common/http";

import { Folder, FolderAdapter } from "../models/folder";

@Injectable({
  providedIn: 'root'
})
export class FolderService {
  private baseUrl = 'http://localhost:57902/api/folders';

  constructor(private http: HttpClient, private adapter: FolderAdapter) { }

  getOwnedFolders(): Observable<Folder[]> {
    return this.http.get(`${this.baseUrl}`).pipe(
      map((data: any[]) => data.map(item => this.adapter.adapt(item)))
    );
  }

  addFolder(ownerId: number, folderName: string, parentId: number) {
    return this.http.post(`${this.baseUrl}`,{
      name: folderName,
      parent: { id: parentId },
      ownerid: ownerId
    });
  }
}
