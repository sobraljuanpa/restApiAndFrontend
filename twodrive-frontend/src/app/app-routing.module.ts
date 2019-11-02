import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { FileEditFormComponent } from './components/file-edit-form/file-edit-form.component';
import { FileListComponent } from './components/file-list/file-list.component';


const routes: Routes = [
  { path: '', component: LoginFormComponent },
  { path: 'files', component: FileListComponent },
  { path: 'files/edit/:id', component: FileEditFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
