import { Component, OnInit } from '@angular/core';

import { Folder } from "../../models/folder";
import { FolderService } from "../../services/folder.service";
import { Observable } from "rxjs";

@Component({
  selector: 'app-folder-list',
  templateUrl: './folder-list.component.html',
  styleUrls: ['./folder-list.component.css']
})
export class FolderListComponent implements OnInit {
  folders: Observable<Folder[]>

  constructor(
    private folderService: FolderService
  ) { }

  ngOnInit() {
    this.folders = this.folderService.getOwnedFolders();
  }

}
