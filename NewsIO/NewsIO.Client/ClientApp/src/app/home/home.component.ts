import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs";
import { Category } from "./category.model";
import { HomeService } from "./home.service";
import { ActivatedRoute, Router } from "@angular/router";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{

  private homeSubscription: Subscription;
  categories: Category[];

  constructor(private route: ActivatedRoute, private homeService: HomeService, private router: Router) {
    
 }
  ngOnInit(): void {
    this.homeSubscription = this.homeService.getAllCategories().subscribe(
      (category: Category[]) => { this.categories = category; });
    console.log(this.categories);
  }
 
  
}
