import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs";
import { Category } from "./category.model";
import { HomeService } from "./home.service";
import { ActivatedRoute, Router } from "@angular/router";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { UserService } from '../shared/services/user.service';
import { News } from './news.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{

  private homeSubscription: Subscription;
  categories: Category[];
  add: boolean = false;
  bigNews: News[];

 
  constructor(private route: ActivatedRoute, private homeService: HomeService, private userService: UserService, private router: Router) {
    
 }
  ngOnInit(): void {
    this.homeSubscription = this.homeService.getAllCategories().subscribe(
      (category: Category[]) => { this.categories = category; });
    console.log(this.categories);
    this.userService.show();
    if (this.userService.isAdmin() === true) {
      this.add = true;
    }
    this.userService.getAllNews().subscribe((news: News[]) => { this.bigNews = news; });
  }
  
}
