import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoggedUser } from "./loggedUser.model";



@Injectable()
export class NavMenuService {
 

  constructor(public httpClient: HttpClient) {
  }
  checkLoggedUser(): Observable<LoggedUser> {
    return this.httpClient.post<LoggedUser>('http://localhost:5030/api/accounts/identity', {});
  }
  logOut() {
    return this.httpClient.post('http://localhost:5030/api/accounts/logut', {});
  }


}
