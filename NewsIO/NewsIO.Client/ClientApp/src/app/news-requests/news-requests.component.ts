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
  successo: boolean = false;
  successc: boolean = false;

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    this.userService.show();
    this.userService.getAllNewsRequests().subscribe((news: NewsRequest[]) => { this.newsReq = news; });
  }
  openNR(id: number) {
    this.userService.openNr(id).subscribe(() => { if (this.userService.statusO===1) this.successo = true; });
  }
  closeNR(id: number) {
    this.userService.closeNr(id).subscribe(() => { if (this.userService.statusC===1) this.successc = true; });
  }

}
