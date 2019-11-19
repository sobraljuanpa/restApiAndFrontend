import { Component, OnInit, Input } from '@angular/core';
import { Observable } from "rxjs";

import { FileService } from '../../services/file.service';
import { FolderService } from "../../services/folder.service";
import { Folder } from "../../models/folder";
import { Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-file-add-form',
  templateUrl: './file-add-form.component.html',
  styleUrls: ['./file-add-form.component.css']
})
export class FileAddFormComponent implements OnInit {
  fileName: string;
  fileContent: string;
  selectedFolder: Folder;
  userFolders: Observable<Folder[]>;

  constructor(
    private fileService: FileService,
    private folderService: FolderService,
    private router: Router,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.userFolders = this.folderService.getOwnedFolders();
  }

  addFile() {
    var userId = JSON.parse(localStorage.getItem("credentials"));
    this.fileService.addFile(this.fileName, this.fileContent, this.selectedFolder.id, userId.id).subscribe(
      res => {
        this.alertService.success("File added successfully!")
        this.router.navigateByUrl('/files');
      },
      err => {
        this.alertService.danger("Sorry, something went wrong.")
      }
    );
  }

}
