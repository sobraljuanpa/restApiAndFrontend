import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { environment } from '../../environments/environment';
import { Folder, FolderAdapter } from "../models/folder";
import { File, FileAdapter } from "../models/file";

@Injectable({
  providedIn: 'root'
})
export class FolderService {
  private baseUrl = `${environment.apiUrl}/folders`;

  constructor(private http: HttpClient, private adapter: FolderAdapter, private fileAdapter: FileAdapter) { }

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

  getFiles(idFolder: Number): Observable<File[]> {
    return this.http.get(`${this.baseUrl}/${idFolder}/files`).pipe(
      map((data: any[]) => data.map(item => this.fileAdapter.adapt(item)))
    )
  }
}
