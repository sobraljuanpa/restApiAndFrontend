import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class User {
    constructor(
        public id: number,
        public firstName: string,
        public lastName: string,
        public username: string,
        public password: string,
        public email: string,
        public role: string
    ){}
}

@Injectable({
    providedIn: 'root'
})
export class UserAdapter implements Adapter<User> {
    adapt(item: any): User {
        return new User (
            item.Id,
            item.FirstName,
            item.LastName,
            item.Username,
            item.Password,
            item.Email,
            item.Role
        );
    }
}