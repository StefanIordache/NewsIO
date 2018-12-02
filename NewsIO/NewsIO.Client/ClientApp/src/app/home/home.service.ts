import { environment } from "../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { Category } from './category.model';
import { Service } from "../Service";


@Injectable()
export class HomeService extends Service<Category> {
  public basicUrl = environment.resourcesUrl + '';

  constructor(public httpClient: HttpClient) {
    super();
  }
  getAllCategories(){
    return this.httpClient.get<Category[]>('http://localhost:5030/api/categories');
  }

}
