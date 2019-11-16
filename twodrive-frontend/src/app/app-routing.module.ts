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


const routes: Routes = [
  { path: '', component: LoginFormComponent },
  { path: 'files', component: FileListComponent },
  { path: 'files/add', component: FileAddFormComponent },
  { path: 'files/edit/:id', component: FileEditFormComponent },
  { path: 'folders', component: FolderListComponent },
  { path: 'admin/files', component: FileListAdminComponent },
  { path: 'users/add', component: UserAddFormComponent },
  { path: 'users', component: UserListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
