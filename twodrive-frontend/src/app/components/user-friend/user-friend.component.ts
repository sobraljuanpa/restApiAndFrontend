import { Component, OnInit } from '@angular/core';

import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Observable } from 'rxjs';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-user-friend',
  templateUrl: './user-friend.component.html',
  styleUrls: ['./user-friend.component.css']
})
export class UserFriendComponent implements OnInit {
  usersFriend: Observable<User[]>;

  constructor(
    private userService: UserService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.usersFriend = this.userService.getUsersFriends();
  }

  RemoveFriend(idUser:Number){
    this.userService.DeleteFriend(idUser).subscribe(
      res => {
          this.alertService.success("User friend delete successfully!")
      },
      err=> {
        this.alertService.danger("Sorry, something went wrong!")
      }
    )
  }

}
