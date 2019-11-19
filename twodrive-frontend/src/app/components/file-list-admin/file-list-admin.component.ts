import { Component, OnInit } from '@angular/core';

import { File } from 'src/app/models/file';
import { FileService } from 'src/app/services/file.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-file-list-admin',
  templateUrl: './file-list-admin.component.html',
  styleUrls: ['./file-list-admin.component.css']
})
export class FileListAdminComponent implements OnInit {
  files: Observable<File[]>;
  fileName: string;

  constructor(
    private fileService: FileService,
    private router: Router,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.files = this.fileService.getAllFiles();
  }

  delete(id: number) {
    this.fileService.deleteFile(id).subscribe(
      res => {
        this.alertService.success("Remove file successfuly!")
        this.ngOnInit();
      },
      err => {
        this.alertService.danger("Sorry, something went wrong.")
        this.router.navigateByUrl('http://localhost:4200/');
      }
    );
  }

  onSearch(fileName: string) {
    this.files = this.fileService.getFilesByNameAdmin(fileName);
  }

}
