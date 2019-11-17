import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-user-edit-form',
  templateUrl: './user-edit-form.component.html',
  styleUrls: ['./user-edit-form.component.css']
})
export class UserEditFormComponent implements OnInit {
  @Input() user: User;
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  email: string;
  role: string;
  
  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private location: Location
  ) { }

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.userService.getUser(id).subscribe(
      user => {
        this.user = user
        this.firstName = this.user.firstName;
        this.lastName = this.user.lastName;
        this.username = this.user.username;
        this.password = this.user.password;
        this.email = this.user.email;
        this.role = this.user.role;
      }
    )
  }

  goBack() {
    this.location.back();
  }

  saveChanges() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.userService.updateUser(
      id,
      this.firstName,
      this.lastName,
      this.username,
      this.password,
      this.email,
      this.role
    ).subscribe(
      res => this.goBack()
    );
    console.log(id);
  }

}
