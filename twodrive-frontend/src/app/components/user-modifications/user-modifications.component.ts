import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/services/user.service';

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
    private userService: UserService
  ) { }

  ngOnInit() {
    this.users = this.userService.getUsers();
  }

  onUserSelection() {
    this.userService.getModifications(this.selectedUser.id).subscribe(
      res => {
        console.log(res);
        this.modificationNumber = (res as any);
      }
    )
  }

}
