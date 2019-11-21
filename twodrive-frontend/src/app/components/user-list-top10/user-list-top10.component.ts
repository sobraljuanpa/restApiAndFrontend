import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-user-list-top10',
  templateUrl: './user-list-top10.component.html',
  styleUrls: ['./user-list-top10.component.css']
})
export class UserListTop10Component implements OnInit {
  users: Observable<User[]>;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.users = this.userService.getTop10Users();
  }

}
