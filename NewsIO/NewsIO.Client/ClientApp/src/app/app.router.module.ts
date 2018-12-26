import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { LoginFormComponent } from "./login-form/login-form.component";
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { UsersComponent } from './users/users.component';
import { CategoriesListComponent } from './categories-list/categories-list.component';
import { NewsRequestsComponent } from './news-requests/news-requests.component';

const routes: Routes = [
  { path : '', component: HomeComponent},
  { path: 'login', component: LoginFormComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'users', component: UsersComponent },
  { path: 'categories', component: CategoriesListComponent },
  { path: 'newsRequests', component: NewsRequestsComponent }
 ];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { useHash: true })
  ],
  exports: [RouterModule],
  declarations: []
})
export class AppRouterModule {
}
