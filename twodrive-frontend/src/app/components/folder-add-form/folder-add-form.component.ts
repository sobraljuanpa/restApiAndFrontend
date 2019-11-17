import { Component, OnInit } from '@angular/core';

import { Folder } from "src/app/models/folder";
import { Observable } from 'rxjs';
import { FolderService } from 'src/app/services/folder.service';

@Component({
  selector: 'app-folder-add-form',
  templateUrl: './folder-add-form.component.html',
  styleUrls: ['./folder-add-form.component.css']
})
export class FolderAddFormComponent implements OnInit {
  folderName: string;
  selectedFolder: Folder;
  userFolders: Observable<Folder[]>;

  constructor(
    private folderService: FolderService
  ) { }

  ngOnInit() {
    this.userFolders = this.folderService.getOwnedFolders();
  }

  addFolder() {
    console.log(this.folderName);
    console.log(this.selectedFolder);
  }

}
