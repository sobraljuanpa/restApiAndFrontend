import { Component, OnInit } from '@angular/core';

import { File } from '../../models/file';
import { FileService } from 'src/app/services/file.service';
import { UserService } from 'src/app/services/user.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})
export class FileListComponent implements OnInit {
  files: Observable<File[]>; 
  fileName: string;
  sortOrder: string;
  friends: Observable<User[]>;
  selectedUser: User;
  option: Boolean;

  constructor(
    private fileService: FileService,
    private router: Router,
    private alertService: AlertService,
    private userService: UserService
  ) { }

  ngOnInit() {
      this.files = this.fileService.getOwnedFiles();
      this.friends = this.userService.getUsersFriends();
      this.option = true;
  }

  delete(id: number) {
    this.fileService.deleteFile(id).subscribe(
      res => {
        this.alertService.success("Remove successfuly!");
        this.ngOnInit();
      },
      err => {
        this.alertService.danger("Sorry, something went wrong.")
        this.router.navigateByUrl('http://localhost:4200/')
      }
    );
  }

  render() {
    this.option = !this.option;
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

  shareFile(fileId: number, userId: number) {
    this.fileService.shareFile(fileId, userId).subscribe(
      res => {
        this.alertService.success("Remove successfuly!");
      },
      err => {
        this.alertService.danger("Sorry, something went wrong.");
      }
    )
  }

}
