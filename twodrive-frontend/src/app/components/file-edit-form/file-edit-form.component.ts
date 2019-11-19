import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common';
import { Observable } from "rxjs";

import { FileService } from '../../services/file.service';
import { File } from '../../models/file';
import { Folder } from "../../models/folder";
import { ActivatedRoute } from '@angular/router';
import { FolderService } from "../../services/folder.service";
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-file-edit-form',
  templateUrl: './file-edit-form.component.html',
  styleUrls: ['./file-edit-form.component.css']
})
export class FileEditFormComponent implements OnInit {
  @Input() file: File;
  fileName: string;
  fileContent: string;
  userFolders: Observable<Folder[]>;
  selectedFolder: Folder;

  constructor(
    private fileService: FileService,
    private folderService: FolderService,
    private route: ActivatedRoute,
    private location: Location,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.userFolders = this.folderService.getOwnedFolders();
    this.fileService.getFile(id).subscribe(
      file => {
        this.file = file
        this.fileName = this.file.name;
        this.fileContent = this.file.content;
      }
    );
  }

  goBack() {
    this.location.back();
  }

  saveChanges() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.fileService.updateFile(id, this.fileName, this.fileContent).subscribe(
      res => this.fileService.moveFile(id,this.selectedFolder.id).subscribe(
        res => {
          this.alertService.success("File edit successfully!")
          this.goBack();
        },
        err=>{
          this.alertService.danger("Sorry, something went wrong.")
        }
      )
    );
  }

}
