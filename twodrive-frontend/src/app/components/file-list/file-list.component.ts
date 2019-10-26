import { Component, OnInit } from '@angular/core';

import { File } from '../../models/file';
import { FileService } from '../../services/file.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})
export class FileListComponent implements OnInit {
  files: Observable<File[]>; 
  

  constructor(private fileService: FileService) { }

  ngOnInit() {
    this.files = this.fileService.getOwnedFiles();
  }

  delete(id: number) {
    this.fileService.deleteFile(id).subscribe(
      res => this.ngOnInit()
    );
  }

}
