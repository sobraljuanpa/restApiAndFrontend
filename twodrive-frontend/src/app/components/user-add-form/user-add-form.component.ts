import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-user-add-form',
  templateUrl: './user-add-form.component.html',
  styleUrls: ['./user-add-form.component.css']
})
export class UserAddFormComponent implements OnInit {
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  email: string;
  role: string;

  constructor(
    private userService: UserService,
    private router: Router,
    private alertService: AlertService
  ) { }

  ngOnInit() {
  }

  addUser() {
    this.userService.addUser(
      this.firstName,
      this.lastName,
      this.username,
      this.password,
      this.email,
      this.role
    ).subscribe(
      res => {
        this.alertService.success("User added successfully!");
        this.router.navigateByUrl('/files');
      },
      err => {
        this.alertService.danger("Sorry, something went wrong.");
      } 
    );
  }

}
