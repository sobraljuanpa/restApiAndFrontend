import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";

import { AuthenticationService } from '../../services/authentication.service';
import { Credentials } from '../../models/credentials';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  username: string;
  password: string;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private alertService: AlertService
  ) { }

  navigate(url: string) {
    this.router.navigateByUrl(url);
  }

  onLogin(): void {
    this.authenticationService.logIn(this.username, this.password).subscribe(
      res => {
        console.log(res);
        let cred = new Credentials();
        cred.id = (res as any).Id;
        cred.username = (res as any).Username;
        cred.token = (res as any).Token;
        cred.admin = (res as any).Role == 'Admin';
        this.authenticationService.setCredentials(cred);
        console.log(this.authenticationService.getCredentials());
        this.alertService.success("Login successfully!")
      },
      err => {
        this.alertService.warning("Username or password incorrectly");
      },
      () => location.reload());
  }

  ngOnInit() {
    if(this.authenticationService.getCredentials()) {
      this.navigate('/files');
    }
  }

}
