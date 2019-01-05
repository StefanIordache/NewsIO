import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HomeService } from '../home/home.service';
import { UserService } from '../shared/services/user.service';
import { News } from '../home/news.model';
import { Category } from '../home/category.model';
import { FormControl, FormGroup } from '@angular/forms';
import { UserList } from '../users/user-list.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-manage-news',
  templateUrl: './manage-news.component.html',
  styleUrls: ['./manage-news.component.css']
})
export class ManageNewsComponent implements OnInit {
  private newsSubscription: Subscription;
  bigNews: News[];
  categories: Category[];
  editNewsForm: FormGroup;
  addNewsForm: FormGroup;
  addExternalNewsForm: FormGroup;
  searchForm: FormGroup;
  id: number;
  delId: number;
  newsId: number;
  n: News;
  currentUser: UserList;
  admin: boolean = false;
  editor: boolean = false;
  selectedFile: File;
  file: File;
  urls = new Array<string>();

  detectFiles(event) {
    this.urls = [];
    let files = event.target.files;
    if (files) {
      for (let file of files) {
        let reader = new FileReader();
        reader.onload = (e: any) => {
          this.urls.push(e.target.result);
        }
        reader.readAsDataURL(file);
      }
    }
  }

  constructor(private route: ActivatedRoute, private homeService: HomeService, private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.userService.getUserById(localStorage.getItem("id")).subscribe((user: UserList) => { this.currentUser = user; });
    console.log("lalalala");
    console.log(this.currentUser);
    this.userService.show();
    if (this.userService.isAdmin() == true) {
      this.newsSubscription = this.userService.getAllNews().subscribe((news: News[]) => {
        this.bigNews = news;
        this.admin = true;
      });
    }
    if (this.userService.isEditor() == true) {
      this.newsSubscription =this.userService.getAllNews().subscribe((news: News[]) => {
        this.bigNews = news.filter(p => p.publishedBy === this.currentUser.userName);
        this.editor = true;
      });
    }
    this.homeService.getAllCategories().subscribe((cat: Category[]) => { this.categories = cat; });
    this.initForm();
  }
  initForm() {
    this.editNewsForm = new FormGroup({
      'title': new FormControl(''),
      'headline': new FormControl(''),
      'content': new FormControl(''),
      'category': new FormControl('')
    });
    this.addNewsForm = new FormGroup({
      'title': new FormControl(''),
      'headline': new FormControl(''),
      'content': new FormControl(''),
      'category': new FormControl('')
    });
    this.addExternalNewsForm = new FormGroup({
      'title': new FormControl(''),
      'headline': new FormControl(''),
      'externalUrl': new FormControl(''),
      'category': new FormControl('')
    });
    this.searchForm = new FormGroup({
      'searchValue': new FormControl('')
    });
  }
  prepareEdit(news: News) {
    this.editNewsForm.patchValue({
      'title': news.title,
      'headline': news.headline,
      'content': news.content,
      'category': news.categoryId
    });
    this.id = news.id;
    this.n = news;
  }
  prepareDelete(news: News) {
    this.delId = news.id;
  }
  editNews() {
    console.log(this.editNewsForm.controls['title'].value);
    console.log(this.editNewsForm.controls['headline'].value);
    console.log(this.editNewsForm.controls['content'].value);
    console.log(this.editNewsForm.controls['category'].value);
    this.userService.editNews(this.id, this.editNewsForm.controls['title'].value, this.editNewsForm.controls['headline'].value, this.editNewsForm.controls['content'].value, this.editNewsForm.controls['category'].value,
      this.n.thumbnailUrl, this.n.externalUrl, this.n.fromRequest, this.n.newsRequestId)
      .subscribe(
        () => {
          location.reload();
        }
      );
  }
  deleteNews() {
    this.userService.deleteNews(this.delId).subscribe(
      () => {
        location.reload();
      }
    );
  }
  prepareAddNews(news: News) {
    this.newsId = news.id;
  }
  onFileChanged(event) {
    this.selectedFile = event.target.files[0];
    this.file = event.target.files[0];
  }
 
  addExternalNews() {
    console.log(this.addExternalNewsForm.controls['title'].value);
    console.log(this.addExternalNewsForm.controls['headline'].value);
    console.log(this.addExternalNewsForm.controls['externalUrl'].value);
    console.log(this.addExternalNewsForm.controls['category'].value);
    this.userService.addExternalNews(this.addExternalNewsForm.controls['title'].value, this.addExternalNewsForm.controls['headline'].value,
      this.addExternalNewsForm.controls['externalUrl'].value, this.addExternalNewsForm.controls['category'].value, this.selectedFile).subscribe(() => { location.reload(); });
  }
  addNews() {
    console.log(this.addNewsForm.controls['title'].value);
    console.log(this.addNewsForm.controls['headline'].value);
    console.log(this.addNewsForm.controls['content'].value);
    console.log(this.addNewsForm.controls['category'].value);
    this.userService.addNews(this.addNewsForm.controls['title'].value, this.addNewsForm.controls['headline'].value,
      this.addNewsForm.controls['content'].value, this.addNewsForm.controls['category'].value, this.file,this.urls).subscribe(() => { location.reload(); });
  }
  fill() {
    this.newsSubscription.unsubscribe();
    this.newsSubscription=this.userService.getSearchedNews(this.searchForm.controls['searchValue'].value)
      .subscribe((news: News[]) => { this.bigNews = news; });
  }
}
