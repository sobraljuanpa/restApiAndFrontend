import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  username: string;
  password: string;

  constructor(private authenticationService: AuthenticationService) { }

  onLogin(): void {
    this.authenticationService.logIn(this.username, this.password).subscribe(
      res => {console.log(res)}
    );
  }

  ngOnInit() {
  }

}
