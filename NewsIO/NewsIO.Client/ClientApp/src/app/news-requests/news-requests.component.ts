import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../shared/services/user.service';
import { NewsRequest } from './newsRequest.model';

@Component({
  selector: 'app-news-requests',
  templateUrl: './news-requests.component.html',
  styleUrls: ['./news-requests.component.css']
})
export class NewsRequestsComponent implements OnInit {

  newsReq: NewsRequest[];

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    this.userService.show();
    this.userService.getAllNewsRequests().subscribe((news: NewsRequest[]) => { this.newsReq = news; });
  }

}
