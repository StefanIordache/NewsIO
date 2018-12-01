import { environment } from "../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "./user.model";



@Injectable()
export class LoginService {
  public basicUrl = environment.resourcesUrl+'/login';

  constructor(public httpClient: HttpClient) {
    
  }
  login(email: String, password: String): Observable<User>{
    return this.httpClient.post<User>('http://localhost:5030/api/accounts/signIn', { Email: email, Password: password });
  }


}
