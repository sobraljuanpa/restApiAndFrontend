import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { FileEditFormComponent } from './components/file-edit-form/file-edit-form.component';
import { FileListComponent } from './components/file-list/file-list.component';
import { FileAddFormComponent } from './components/file-add-form/file-add-form.component';
import { FolderListComponent } from './components/folder-list/folder-list.component';


const routes: Routes = [
  { path: '', component: LoginFormComponent },
  { path: 'files', component: FileListComponent },
  { path: 'files/add', component: FileAddFormComponent },
  { path: 'files/edit/:id', component: FileEditFormComponent },
  { path: 'folders', component: FolderListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
