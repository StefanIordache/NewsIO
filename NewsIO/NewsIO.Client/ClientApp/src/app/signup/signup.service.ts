import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Signup } from "./signup.model";



@Injectable()
export class SignUpService {
  

  constructor(public httpClient: HttpClient) {

  }
  signup(username: String, email: String, password: String): Observable<Signup> {
    return this.httpClient.post<Signup>('http://localhost:5030/api/accounts/register', {Username:username, Email: email, Password: password });
  }


}
