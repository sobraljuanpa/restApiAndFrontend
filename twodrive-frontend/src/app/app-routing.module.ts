import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { FileEditFormComponent } from './components/file-edit-form/file-edit-form.component';
import { FileListComponent } from './components/file-list/file-list.component';
import { FileAddFormComponent } from './components/file-add-form/file-add-form.component';
import { FolderListComponent } from './components/folder-list/folder-list.component';
import { FileListAdminComponent } from './components/file-list-admin/file-list-admin.component';
import { UserAddFormComponent } from './components/user-add-form/user-add-form.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserEditFormComponent } from './components/user-edit-form/user-edit-form.component';
import { FolderAddFormComponent } from './components/folder-add-form/folder-add-form.component';
import { UserListTop10Component } from './components/user-list-top10/user-list-top10.component';
import { AdminImportComponent } from './components/admin-import/admin-import.component';
import { UserModificationsComponent } from './components/user-modifications/user-modifications.component';
import { UserFriendComponent } from './components/user-friend/user-friend.component';
import { UserAddFriendComponent } from './components/user-add-friend/user-add-friend.component';


const routes: Routes = [
  { path: '', component: LoginFormComponent },
  { path: 'files', component: FileListComponent },
  { path: 'files/add', component: FileAddFormComponent },
  { path: 'files/edit/:id', component: FileEditFormComponent },
  { path: 'folders', component: FolderListComponent },
  { path: 'folders/add', component: FolderAddFormComponent },
  { path: 'admin/files', component: FileListAdminComponent },
  { path: 'users/add', component: UserAddFormComponent },
  { path: 'users/top10', component: UserListTop10Component },
  { path: 'users/edit/:id', component: UserEditFormComponent },
  { path: 'users/modifications', component: UserModificationsComponent },
  { path: 'users', component: UserListComponent },
  { path: 'import', component: AdminImportComponent },
  { path: 'friends/add', component: UserAddFriendComponent },
  { path: 'user/friend', component: UserFriendComponent }
];

@NgModule({
  
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
