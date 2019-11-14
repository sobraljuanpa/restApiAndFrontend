import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-add-form',
  templateUrl: './user-add-form.component.html',
  styleUrls: ['./user-add-form.component.css']
})
export class UserAddFormComponent implements OnInit {
  role: string;

  constructor() { }

  ngOnInit() {
  }

  addUser() {
    console.log(this.role);
  }

}
