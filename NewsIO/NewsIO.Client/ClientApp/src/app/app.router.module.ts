import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router";
import { LoginFormComponent } from "./login-form/login-form.component";
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  {path: '/login', component: LoginFormComponent}
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
