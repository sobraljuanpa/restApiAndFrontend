import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { AlertModule } from 'ngx-alerts';

import { AppRoutingModule } from './app-routing.module';
import { TokenInterceptor } from "./services/token.interceptor";
import { AppComponent } from './app.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { UserNavbarComponent } from './components/user-navbar/user-navbar.component';
import { FileListComponent } from './components/file-list/file-list.component';
import { FileEditFormComponent } from './components/file-edit-form/file-edit-form.component';
import { FileAddFormComponent } from './components/file-add-form/file-add-form.component';
import { FolderListComponent } from './components/folder-list/folder-list.component';
import { FileListAdminComponent } from './components/file-list-admin/file-list-admin.component';
import { UserAddFormComponent } from './components/user-add-form/user-add-form.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserEditFormComponent } from './components/user-edit-form/user-edit-form.component';
import { FolderAddFormComponent } from './components/folder-add-form/folder-add-form.component';
import { UserListTop10Component } from './components/user-list-top10/user-list-top10.component';
import { AdminImportComponent } from './components/admin-import/admin-import.component';
import { UserFriendComponent } from './components/user-friend/user-friend.component';
import { UserModificationsComponent } from "./components/user-modifications/user-modifications.component";
import { UserAddFriendComponent } from './components/user-add-friend/user-add-friend.component';
import { FileSharedComponent } from './components/file-shared/file-shared.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginFormComponent,
    UserNavbarComponent,
    FileListComponent,
    FileEditFormComponent,
    FileAddFormComponent,
    FolderListComponent,
    FileListAdminComponent,
    UserAddFormComponent,
    UserListComponent,
    UserEditFormComponent,
    FolderAddFormComponent,
    UserListTop10Component,
    AdminImportComponent,
    UserModificationsComponent,
    UserFriendComponent,
    UserAddFriendComponent,
    FileSharedComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    AlertModule.forRoot({maxMessages: 5, timeout: 5000, position: 'left'}),
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
