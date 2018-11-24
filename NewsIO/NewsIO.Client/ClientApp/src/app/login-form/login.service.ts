import { environment } from "../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";



@Injectable()
export class LoginService {
  public basicUrl = environment.resourcesUrl+'/login';

  constructor(public httpClient: HttpClient) {
    
  }


}
