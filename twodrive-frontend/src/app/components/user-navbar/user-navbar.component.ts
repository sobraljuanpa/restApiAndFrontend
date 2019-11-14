import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from 'src/app/services/authentication.service';
import { Credentials } from 'src/app/models/credentials';

@Component({
  selector: 'app-user-navbar',
  templateUrl: './user-navbar.component.html',
  styleUrls: ['./user-navbar.component.css']
})
export class UserNavbarComponent implements OnInit {
  credentials: Credentials;
  admin: boolean;

  constructor(private authService: AuthenticationService, private router: Router) { }

  navigate(url: string) {
    this.router.navigateByUrl(url);
  }

  logOut() {
    this.authService.logOut();
    this.navigate("");
  }

  ngOnInit() {
    this.credentials = this.authService.getCredentials();
    if(this.credentials){
      this.admin = this.credentials.admin;
    }
  }

}
