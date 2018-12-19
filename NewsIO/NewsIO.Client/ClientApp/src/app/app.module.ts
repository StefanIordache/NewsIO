import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { AppRouterModule } from './app.router.module';
import { SignupComponent } from './signup/signup.component';
import { HomeService } from './home/home.service';
import { UserService } from './shared/services/user.service';
import { UsersComponent } from './users/users.component';
import { CategoriesListComponent } from './categories-list/categories-list.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginFormComponent,
    SignupComponent,
    UsersComponent,
    CategoriesListComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule ,
    AppRouterModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ])
  ],
  providers: [ HomeService,UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
