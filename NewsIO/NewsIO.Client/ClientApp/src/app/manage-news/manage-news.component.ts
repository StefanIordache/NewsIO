import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HomeService } from '../home/home.service';
import { UserService } from '../shared/services/user.service';
import { News } from '../home/news.model';
import { Category } from '../home/category.model';
import { FormControl, FormGroup } from '@angular/forms';
import { UserList } from '../users/user-list.model';

@Component({
  selector: 'app-manage-news',
  templateUrl: './manage-news.component.html',
  styleUrls: ['./manage-news.component.css']
})
export class ManageNewsComponent implements OnInit {
  bigNews: News[];
  categories: Category[];
  editNewsForm: FormGroup;
  id: number;
  delId: number;
  n: News;
  currentUser: UserList;
  admin: boolean = false;
  editor: boolean = false;
  
  constructor(private route: ActivatedRoute, private homeService: HomeService, private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.userService.getUserById(localStorage.getItem("id")).subscribe((user: UserList) => { this.currentUser = user;});
    console.log("lalalala");
    console.log(this.currentUser);
    this.userService.show();
    if (this.userService.isAdmin() == true){
      this.userService.getAllNews().subscribe((news: News[]) => {
        this.bigNews = news;
        this.admin = true;
      });
    }
    if (this.userService.isEditor() == true) {
      this.userService.getAllNews().subscribe((news: News[]) => {
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
    })
  }
  prepareEdit(news: News) {
    this.editNewsForm.patchValue({
      'title': news.title,
      'headline': news.headline,
      'content': news.content,
      'category':news.categoryId
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
          this.router.navigateByUrl('/')
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
}
