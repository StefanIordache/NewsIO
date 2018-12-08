import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { LoggedUser } from './loggedUser.model';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  show1: boolean = true;
  show2: boolean = false;
  loggedUser: LoggedUser;
  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService) { }
  ngOnInit() { }
  checkLoggedUser() {
    if (this.userService.isLoggedIn() === true) {
      this.show1 = false;
      this.show2 = true;
    }
 }
    logout(){
      this.userService.logOut();
    }
  }

