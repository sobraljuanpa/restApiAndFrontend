import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/services/user.service';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-user-modifications',
  templateUrl: './user-modifications.component.html',
  styleUrls: ['./user-modifications.component.css']
})
export class UserModificationsComponent implements OnInit {
  selectedUser: User;
  users: Observable<User[]>;
  modificationNumber: number;

  constructor(
    private userService: UserService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.users = this.userService.getUsers();
  }

  onUserSelection() {
    this.userService.getModifications(this.selectedUser.id).subscribe(
      res => {
        this.modificationNumber = (res as any);
        this.alertService.success("User delete correctly!");
      },
      err => {
        this.alertService.danger("Sorry, something went wrong.");
      }
    )
  }

}
