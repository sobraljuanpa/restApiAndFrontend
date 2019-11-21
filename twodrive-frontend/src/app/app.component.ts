import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';
import { Credentials } from './models/credentials';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'twodrive-frontend';
  credentials: Credentials;

  constructor(private authService: AuthenticationService) {}

  ngOnInit() {
    this.credentials = this.authService.getCredentials();
  }
}
