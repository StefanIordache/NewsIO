import { HttpClient, HttpParams, HttpErrorResponse } from "@angular/common/http";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Injectable } from "@angular/core";
import { Observable, BehaviorSubject } from "rxjs";
import { User } from "../../login-form/user.model";
import { Signup } from "../../signup/signup.model";
import { map } from 'rxjs/operators';



@Injectable()
export class UserService  {
  private loggedIn = false;
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  authNavStatus$ = this._authNavStatusSource.asObservable();
  visible: boolean;
  message: string;
  private admin=false;

  constructor(public httpClient: Http) {
    if (localStorage.getItem('role') == 'Administrator') { this.admin = true; }
    this.loggedIn = !!localStorage.getItem('auth_token');
    this._authNavStatusSource.next(this.loggedIn);
    this.visible = false;
  }
  hide() { this.visible = false; }

  show() { this.visible = true; }
  signup(email: String, username: String, password: String, firstName: String, lastName: String, location: String){
    let body = JSON.stringify({ email,username, password, firstName, lastName, location });
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    return this.httpClient.post('http://localhost:5030/api/accounts/register', body, options).
      pipe(map((response: Response) => {
        this.message = response.json()["message"];
      }));
  }
  login(username: String, password: String) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.httpClient.post('http://localhost:5030/api/auth/login', JSON.stringify({ Username: username, Password: password }), { headers })
      .pipe(map(res => res.json()))
      .pipe(map(res => {
        if (res.role == 'Administrator') {
          this.admin = true;
        }
        else {
          this.admin = false;
        }
        localStorage.setItem('auth_token', res.auth_token);
        localStorage.setItem('role', res.role);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      }));
   
  }
  logOut() {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('role');
    this.admin = false;
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
    location.reload(); 
  }
  isLoggedIn() {
    return this.loggedIn;
  }
  isAdmin() {
    console.log("-----------");
    console.log(this.admin);
    return this.admin;
    
  }
 

}
