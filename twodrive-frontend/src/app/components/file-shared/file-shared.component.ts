import { Component, OnInit } from '@angular/core';

import { File } from '../../models/file';
import { FileService } from '../../services/file.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-file-shared',
  templateUrl: './file-shared.component.html',
  styleUrls: ['./file-shared.component.css']
})
export class FileSharedComponent implements OnInit {

  files: Observable<File[]>; 

  constructor(
    private fileService: FileService,
    private router: Router
  ) { }

  ngOnInit() {
    this.files = this.fileService.getSharedFiles();
  }

  Back(){
    this.router.navigateByUrl('/files');
  }

}
