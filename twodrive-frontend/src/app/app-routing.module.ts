import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { FileEditFormComponent } from './components/file-edit-form/file-edit-form.component';


const routes: Routes = [
  { path: '', component: LoginFormComponent },
  { path: 'userhome', component: UserHomeComponent },
  { path: 'files/edit/:id', component: FileEditFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
