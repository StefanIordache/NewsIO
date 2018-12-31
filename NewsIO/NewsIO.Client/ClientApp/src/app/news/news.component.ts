import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { UserService } from '../shared/services/user.service';
import { HomeService } from '../home/home.service';
import { Category } from '../home/category.model';
import { News } from '../home/news.model';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  private id: number;
  category: Category;
  bigNews: News[];
  constructor(private route: ActivatedRoute, private userService: UserService, private categoryService: HomeService) { }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];
    });
    this.userService.show();
    this.userService.getCategoryById(this.id).subscribe((cat: Category) => { this.category = cat; });
    this.userService.getLatestNewsByCategoryId(this.id).subscribe((news: News[]) => { this.bigNews = news; });
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
}
