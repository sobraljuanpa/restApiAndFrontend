import { Component, OnInit } from '@angular/core';

import { File } from 'src/app/models/file';
import { FileService } from 'src/app/services/file.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-file-list-admin',
  templateUrl: './file-list-admin.component.html',
  styleUrls: ['./file-list-admin.component.css']
})
export class FileListAdminComponent implements OnInit {
  files: Observable<File[]>;

  constructor(
    private fileService: FileService,
    private router: Router
  ) { }

  ngOnInit() {
    this.files = this.fileService.getAllFiles();
  }

  delete(id: number) {
    this.fileService.deleteFile(id).subscribe(
      res => this.ngOnInit(),
      err => this.router.navigateByUrl('http://localhost:4200/')
    );
  }

}
