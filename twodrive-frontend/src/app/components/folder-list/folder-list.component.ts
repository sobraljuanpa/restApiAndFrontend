import { Component, OnInit } from '@angular/core';

import { Folder } from "../../models/folder";
import { File } from '../../models/file';
import { FolderService } from "../../services/folder.service";
import { Observable } from "rxjs";

@Component({
  selector: 'app-folder-list',
  templateUrl: './folder-list.component.html',
  styleUrls: ['./folder-list.component.css']
})
export class FolderListComponent implements OnInit {
  folders: Observable<Folder[]>;
  files: Observable<File[]>;
  show: Boolean;


  constructor(
    private folderService: FolderService
  ) { }

  ngOnInit() {
    this.folders = this.folderService.getOwnedFolders();
    this.show = true;
  }

  getFiles(idFolder: Number){
    this.files = this.folderService.getFiles(idFolder);
    this.show = false;
  }

  back(){
    this.files = null;
    this.show = true;
  }
}
