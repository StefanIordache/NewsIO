import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { UserService } from '../shared/services/user.service';
import { HomeService } from '../home/home.service';
import { Category } from '../home/category.model';
import { News } from '../home/news.model';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  private id: number;
  private newsSubscription: Subscription;
  category: Category;
  bigNews: News[];
  searchForm: FormGroup;
  image: string;
  constructor(private route: ActivatedRoute, private userService: UserService, private categoryService: HomeService) { }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];
    });
    this.userService.show();
    this.userService.getCategoryById(this.id).subscribe((cat: Category) => { this.category = cat; });
    this.newsSubscription = this.userService.getLatestNewsByCategoryId(this.id).subscribe((news: News[]) => { this.bigNews = news; });
    this.initForm();
  }
  initForm() {
    this.searchForm = new FormGroup({
      'searchValue': new FormControl('')
    });
  }
  getAlpha(id:number) {
    this.userService.getAlphabeticalNewsByCategoryId(id).subscribe((news: News[]) => { this.bigNews = news;  });
  }
  getNonAlpha(id: number) {
    this.userService.getNonAlphabeticalNewsByCategoryId(id).subscribe((news: News[]) => { this.bigNews = news; });
  }
  getAsc(id: number) {
    this.userService.getLatestNewsByCategoryId(id).subscribe((news: News[]) => { this.bigNews = news;  });
  }
  getDesc(id: number) {
    this.userService.getOldestNewsByCategoryId(id).subscribe((news: News[]) => { this.bigNews = news;  });
  }
  fill() {
    this.newsSubscription.unsubscribe();
    this.newsSubscription = this.userService.getSearchedNews(this.searchForm.controls['searchValue'].value)
      .subscribe((news: News[]) => { this.bigNews = news; });
  }
  getImage(id: number) {
    this.userService.getImage(id).subscribe((img: string) => { this.image = img; });
  }
}
