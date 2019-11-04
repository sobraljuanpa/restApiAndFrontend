import { Component, OnInit, Input } from '@angular/core';
import { Observable } from "rxjs";

import { FileService } from '../../services/file.service';
import { FolderService } from "../../services/folder.service";
import { File } from "../../models/file";
import { Folder } from "../../models/folder";

@Component({
  selector: 'app-file-add-form',
  templateUrl: './file-add-form.component.html',
  styleUrls: ['./file-add-form.component.css']
})
export class FileAddFormComponent implements OnInit {
  fileName: string;
  fileContent: string;
  userFolders: Observable<Folder[]>;

  constructor(
    private fileService: FileService,
    private folderService: FolderService
  ) { }

  ngOnInit() {
    this.userFolders = this.folderService.getOwnedFolders();
  }

}
