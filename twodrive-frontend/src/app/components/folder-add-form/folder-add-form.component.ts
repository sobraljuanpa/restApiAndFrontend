import { Component, OnInit } from '@angular/core';

import { Folder } from "src/app/models/folder";
import { Observable } from 'rxjs';
import { FolderService } from 'src/app/services/folder.service';
import { Router } from '@angular/router';

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
    private folderService: FolderService,
    private router: Router
  ) { }

  ngOnInit() {
    this.userFolders = this.folderService.getOwnedFolders();
  }

  addFolder() {
    var userId = JSON.parse(localStorage.getItem("credentials")).id;
    this.folderService.addFolder(userId, this.folderName, this.selectedFolder.id).subscribe(
      res => {
        this.router.navigateByUrl('/folders');
      }
    );
  }

}
