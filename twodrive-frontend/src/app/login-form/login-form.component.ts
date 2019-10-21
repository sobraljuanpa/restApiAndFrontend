import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";

import { AuthenticationService } from '../authentication.service';
import { Credentials } from '../credentials';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  username: string;
  password: string;

  constructor(private authenticationService: AuthenticationService, private router: Router) { }

  navigate(url: string) {
    this.router.navigateByUrl(url);
  }

  onLogin(): void {
    this.authenticationService.logIn(this.username, this.password).subscribe(
      res => {
        let cred = new Credentials();
        cred.username = (res as any).username;
        cred.token = (res as any).token;
        cred.admin = (res as any).role == 'Admin';
        this.authenticationService.setCredentials(cred);
        console.log(this.authenticationService.getCredentials());
      },
      err => {
        console.log()
      },
      () => this.navigate('/userhome'));
  }

  ngOnInit() {
    if(this.authenticationService.getCredentials()) {
      this.navigate('/userhome');
    }
  }

}
