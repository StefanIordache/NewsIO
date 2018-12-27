import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { LoggedUser } from './loggedUser.model';
import { UserService } from '../shared/services/user.service';
import { HomeService } from '../home/home.service';
import { Category } from '../home/category.model';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  show1: boolean = true;
  show2: boolean = false;
  show3: boolean = false;
  show4: boolean = false;
  succesnr: boolean = false;
  categories: Category[];
  loggedUser: LoggedUser;
  addNewsRequestForm: FormGroup;
  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService, private homeService: HomeService) { }
  ngOnInit() {
    this.initForm();
    if (this.userService.isLoggedIn() === true) {
      this.show1 = false;
      this.show2 = true;
    }

    if (this.userService.isAdmin() === true) {
      this.show3 = true;
    }
    if (this.userService.isEditor() === true) {
      this.show4 = true;
    }
     this.homeService.getAllCategories().subscribe(
      (category: Category[]) => { this.categories = category; });
  }
  initForm() {
    this.addNewsRequestForm = new FormGroup({
      'title': new FormControl(''),
      'description': new FormControl(''),
      'category': new FormControl('')
    });
  }
  checkLoggedUser() {
    if (this.userService.isLoggedIn() === true) {
      this.show1 = false;
      this.show2 = true;
    }

    if (this.userService.isAdmin()===true) {
      this.show3 = true;
    }
    if (this.userService.isEditor() === true) {
      this.show4 = true;
    }
    console.log(this.show3);
   
 }
    logout(){
      this.userService.logOut();
      this.router.navigateByUrl('');
  }
  addNewsRequest() {
    this.userService.addNewsRequest(this.addNewsRequestForm.controls['title'].value, this.addNewsRequestForm.controls['description'].value,
      this.addNewsRequestForm.controls['category'].value).subscribe(() => {
        console.log(this.userService.status);
        if (this.userService.status === 1) {
          this.succesnr = true;
        } });
    
  }
  
  }

