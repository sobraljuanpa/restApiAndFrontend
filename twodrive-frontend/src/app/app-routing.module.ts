import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent } from './login-form/login-form.component';
import { UserHomeComponent } from './user-home/user-home.component';


const routes: Routes = [
  { path: '', component: LoginFormComponent},
  { path: 'userhome', component: UserHomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
