import { Component, OnInit } from '@angular/core';

import { File } from '../../models/file';
import { FileService } from '../../services/file.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})
export class FileListComponent implements OnInit {
  files: Observable<File[]>; 
  fileName: string;
  sortOrder: string;
  

  constructor(
    private fileService: FileService,
    private router: Router
  ) { }

  ngOnInit() {
      this.files = this.fileService.getOwnedFiles();
  }

  delete(id: number) {
    this.fileService.deleteFile(id).subscribe(
      res => this.ngOnInit(),
      err => this.router.navigateByUrl('http://localhost:4200/')
    );
  }

  onSearch(fileName: string) {
    this.files = this.fileService.getFilesByName(fileName);
  }

  onSortOrderSelection() {
    switch(this.sortOrder) {
      case "Name ascending":
        this.files = this.fileService.getFilesBySortOrder("name_asc");
      case "Name descending":
        this.files = this.fileService.getFilesBySortOrder("name_desc");
      case "Creation date ascending":
        this.files = this.fileService.getFilesBySortOrder("created_asc");
      case "Creation date descending":
        this.files = this.fileService.getFilesBySortOrder("created_desc");
      case "Last modification date ascending":
        this.files = this.fileService.getFilesBySortOrder("modified_asc");
      case "Last modification date descending":
        this.files = this.fileService.getFilesBySortOrder("modified_desc");
    }
  }

}
