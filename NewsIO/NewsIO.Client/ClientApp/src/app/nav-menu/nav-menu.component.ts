import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { NavMenuService } from './nav-menu.service';
import { LoggedUser } from './loggedUser.model';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  show1: boolean = true;
  show2: boolean = false;
  loggedUser: LoggedUser;
  constructor(private route: ActivatedRoute, private router: Router, private navMenuService: NavMenuService) { }
  ngOnInit() { }
  checkLoggedUser() {
    this.navMenuService.checkLoggedUser().subscribe(
      //() => {
        //this.show1 = false;
        //this.show2 = true;
     // },
      (loggedUser: LoggedUser) => { this.loggedUser = loggedUser; }
    );
  }
    logout(){
      this.navMenuService.logOut().subscribe(
        () => {
          this.router.navigateByUrl("/login");
        });
    }
  }

