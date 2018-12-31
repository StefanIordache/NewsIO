import { HttpClient, HttpParams, HttpErrorResponse } from "@angular/common/http";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Injectable } from "@angular/core";
import { Observable, BehaviorSubject } from "rxjs";
import { User } from "../../login-form/user.model";
import { Signup } from "../../signup/signup.model";
import { map } from 'rxjs/operators';
import { UserList } from "../../users/user-list.model";
import { NewsRequest } from "../../news-requests/newsRequest.model";
import { News } from "../../home/news.model";
import { Category } from "../../home/category.model";



@Injectable()
export class UserService  {
  private loggedIn = false;
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  authNavStatus$ = this._authNavStatusSource.asObservable();
  visible: boolean;
  message: string;
  private admin = false;
  private editor = false;
  identity: string;
  token: string;
  status: number;
  statusO: number;
  statusC: number;

  constructor(public httpClient: Http, public httpi: HttpClient) {
    if (localStorage.getItem('role') == 'Administrator') { this.admin = true; }
    if (localStorage.getItem('role') == 'Moderator') { this.editor = true; }
    this.loggedIn = !!localStorage.getItem('auth_token');
    this._authNavStatusSource.next(this.loggedIn);
    this.visible = false;
    this.token = '';
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
        this.identity = res.user_id;
        if (res.role == 'Administrator') {
          this.admin = true;
        }
        else {
          this.admin = false;
        }
        if (res.role == 'Moderator') {
          this.editor = true;
        }
        else {
          this.editor = false;
        }
        localStorage.setItem('auth_token', res.auth_token);
        console.log(res.auth_token);
        this.token = localStorage.getItem('auth_token');
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
  isEditor() {
    return this.editor;
  }
  getAllUsers(): Observable<UserList[]> {
    return this.httpi.get<UserList[]>('http://localhost:5030/api/Users');
  }
  changeRole(id: string, roleName: string) {
    return this.httpClient.post('http://localhost:5030/api/Users/changeRole/' + id + '/' + roleName, {})
  }
  addCategory(title: string, description: string) {
    let body = JSON.stringify({ title, description});
    let headers = new Headers();
    
    headers.append('Authorization', 'Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Content-Type', 'application/json');
    let options = new RequestOptions({ headers: headers});
    return this.httpClient.post('http://localhost:5030/api/categories/add', body, options);
  }
  editCategory(id: number, title: string, description: string) {
    let body = JSON.stringify({ title, description });
    let headers = new Headers();
    headers.append('Authorization', 'Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Content-Type', 'application/json');
    let options = new RequestOptions({ headers: headers });
    return this.httpClient.put('http://localhost:5030/api/categories/edit/' + id,body,options)
  }
  deleteCategory(id: number) {
    let headers = new Headers();
    console.log('Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Authorization', 'Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Content-Type', 'application/json');
    let options = new RequestOptions({ headers: headers });
    return this.httpClient.delete('http://localhost:5030/api/categories/delete/'+id,options)
  }
  addNewsRequest(title: string, description: string,categoryId:number) {
    let body = JSON.stringify({ title, description,categoryId });
    let headers = new Headers();
    headers.append('Authorization', 'Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Content-Type', 'application/json');
    let options = new RequestOptions({ headers: headers });
    return this.httpClient.post('http://localhost:5030/api/NewsRequests/add', body, options).pipe(map((response: Response) => {
      this.status = response.json()["status"];
      console.log(this.status);
    }));
  }
  getAllNewsRequests(): Observable<NewsRequest[]> {
    return this.httpi.get<NewsRequest[]>('http://localhost:5030/api/NewsRequests');
  }
  openNr(id: number) {
    let body = JSON.stringify({ id });
    let headers = new Headers();
    headers.append('Authorization', 'Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Content-Type', 'application/json');
    let options = new RequestOptions({ headers: headers });
    return this.httpClient.post('http://localhost:5030/api/NewsRequests/open/' + id, body, options).pipe(map((response: Response) => {
      this.statusO = response.json()["status"];
    }));
  }
  closeNr(id: number) {
    let body = JSON.stringify({ id });
    let headers = new Headers();
    headers.append('Authorization', 'Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Content-Type', 'application/json');
    let options = new RequestOptions({ headers: headers });
    return this.httpClient.post('http://localhost:5030/api/NewsRequests/close/' + id, body, options).pipe(map((response: Response) => {
      this.statusC = response.json()["status"];
    }));
  }
  deleteNR(id: number) {
    let headers = new Headers();
    //console.log('Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Authorization', 'Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Content-Type', 'application/json');
    let options = new RequestOptions({ headers: headers });
    return this.httpClient.delete('http://localhost:5030/api/NewsRequests/delete/' + id,options);
  }
  editNR(id: number, title: string, description: string, status: string, isClosed: Boolean, 
    requestDate: Date, requestedById: string, requestedBy: string, categoryId: number) {
    let body = JSON.stringify({id,
      title, description, status, isClosed, requestDate,requestedBy,requestedById,categoryId
    });
    let headers = new Headers();
    console.log('Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Authorization', 'Bearer' + ' ' + localStorage.getItem('auth_token'));
    headers.append('Content-Type', 'application/json');
    let options = new RequestOptions({ headers: headers });
    return this.httpClient.post('http://localhost:5030/api/NewsRequests/edit/' + id, body, options)
  }


  getAllNews(): Observable<News[]> {
    return this.httpi.get<News[]>('http://localhost:5030/api/News');
  }

  getCategoryById(id: number): Observable<Category> {
    return this.httpi.get<Category>('http://localhost:5030/api/categories/' + id);
  }
  getLatestNewsByCategoryId(id: number): Observable<News[]> {
    return this.httpi.get<News[]>('http://localhost:5030/api/news/getLatestByCategory?categoryId=' + id);
  }
  getOldestNewsByCategoryId(id: number): Observable<News[]> {
    return this.httpi.get<News[]>('http://localhost:5030/api/news/getOldestByCategory?categoryId=' + id);
  }
  getAlphabeticalNewsByCategoryId(id: number): Observable<News[]> {
    return this.httpi.get<News[]>('http://localhost:5030/api/news/getAlphabeticalByCategory?categoryId=' + id);
  }
  getNonAlphabeticalNewsByCategoryId(id: number): Observable<News[]> {
    return this.httpi.get<News[]>('http://localhost:5030/api/news/getNonAlphabeticalByCategory?categoryId=' + id);
  }

}
