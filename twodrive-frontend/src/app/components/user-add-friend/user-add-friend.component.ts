import { Component, OnInit } from '@angular/core';

import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-user-add-friend',
  templateUrl: './user-add-friend.component.html',
  styleUrls: ['./user-add-friend.component.css']
})
export class UserAddFriendComponent implements OnInit {
  users: Observable<User[]>;

  constructor(
    private userService: UserService
  ) { }

  ngOnInit() {
    this.users = this.userService.getUsers();
  }

  addFriend(userId: number) {
    this.userService.addFriend(userId).subscribe();
  }

}
