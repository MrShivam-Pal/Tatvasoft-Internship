import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { FormControl, FormsModule } from '@angular/forms';
import { UserComponent } from './MyComponent/user/user.component';
import { AddUserComponent } from './MyComponent/add-user/add-user.component';
import { AboutComponent } from './MyComponent/about/about.component';
import { UserListComponent } from './MyComponent/user-list/user-list.component';
import { LoginComponent } from './login/login.component';
import { ListComponent } from './list/list.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    UserComponent,
    AddUserComponent,
    AboutComponent,
    UserListComponent,
    LoginComponent,
    ListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
