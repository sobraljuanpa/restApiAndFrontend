import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { TokenInterceptor } from "./services/token.interceptor";
import { AppComponent } from './app.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { UserNavbarComponent } from './components/user-navbar/user-navbar.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { FileListComponent } from './components/file-list/file-list.component';
import { FileDetailComponent } from './components/file-detail/file-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginFormComponent,
    UserNavbarComponent,
    UserHomeComponent,
    FileListComponent,
    FileDetailComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
