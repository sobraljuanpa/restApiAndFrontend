import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common';

import { FileService } from 'src/app/services/file.service';
import { File } from '../../models/file';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-file-edit-form',
  templateUrl: './file-edit-form.component.html',
  styleUrls: ['./file-edit-form.component.css']
})
export class FileEditFormComponent implements OnInit {
  @Input() file: File;
  fileName: string;
  fileContent: string;

  constructor(
    private fileService: FileService,
    private route: ActivatedRoute,
    private location: Location
  ) { }

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
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
      res => this.goBack()
    );
  }

}
