import { Component, OnInit } from '@angular/core';

import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Observable } from 'rxjs';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: Observable<User[]>;

  constructor(
    private userService: UserService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.users = this.userService.getUsers();
  }

  Delete(id:Number){
    this.userService.DeleteUser(id).subscribe(
      res=> {
        this.alertService.success("User delete correctly!");
      },
      err => {
        this.alertService.danger("Sorry, something went wrong.");
      }
    )
  }

}
