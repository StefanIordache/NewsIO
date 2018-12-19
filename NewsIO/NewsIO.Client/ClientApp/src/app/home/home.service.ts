import { environment } from "../../environments/environment";
import { Category } from './category.model';
import { Service } from "../Service";
import { HttpClient, HttpParams, HttpErrorResponse } from "@angular/common/http";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Injectable } from "@angular/core";
import { Observable, BehaviorSubject } from "rxjs";
import { map } from 'rxjs/operators';



@Injectable()
export class HomeService {
  public basicUrl = environment.resourcesUrl + '';
  message: String;
  constructor(public httpClient: HttpClient,public http:Http) {
   
  }
  getAllCategories(){
    return this.httpClient.get<Category[]>('http://localhost:5030/api/categories');
  }

  

}
