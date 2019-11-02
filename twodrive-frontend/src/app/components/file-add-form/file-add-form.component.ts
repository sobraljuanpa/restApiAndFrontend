import { Component, OnInit, Input } from '@angular/core';

import { FileService } from '../../services/file.service';
import { File } from "../../models/file";

@Component({
  selector: 'app-file-add-form',
  templateUrl: './file-add-form.component.html',
  styleUrls: ['./file-add-form.component.css']
})
export class FileAddFormComponent implements OnInit {
  fileName: string;
  fileContent: string;

  constructor(
    private fileService: FileService
  ) { }

  ngOnInit() {
  }

}
