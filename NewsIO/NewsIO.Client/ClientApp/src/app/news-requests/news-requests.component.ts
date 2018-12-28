import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../shared/services/user.service';
import { NewsRequest } from './newsRequest.model';
import { FormGroup, FormControl } from '@angular/forms';
import { Category } from '../home/category.model';
import { HomeService } from '../home/home.service';

@Component({
  selector: 'app-news-requests',
  templateUrl: './news-requests.component.html',
  styleUrls: ['./news-requests.component.css']
})
export class NewsRequestsComponent implements OnInit {

  newsReq: NewsRequest[];
  successo: boolean = false;
  successc: boolean = false;
  editNRForm: FormGroup;
  categories: Category[];
  delNR: number;
  nrId: number;
  nr: NewsRequest;


  constructor(private route: ActivatedRoute, private userService: UserService, private categoryService: HomeService) { }

  ngOnInit() {
    this.userService.show();
    this.userService.getAllNewsRequests().subscribe((news: NewsRequest[]) => { this.newsReq = news; });
    this.categoryService.getAllCategories().subscribe(
      (category: Category[]) => { this.categories = category; });
    this.initForm();
  }
  initForm() {
    this.editNRForm = new FormGroup({
      'title': new FormControl(''),
      'description': new FormControl(''),
      'status': new FormControl('')
    })
  }
  openNR(id: number) {
    this.userService.openNr(id).subscribe(() => { if (this.userService.statusO===1) this.successo = true; });
  }
  closeNR(id: number) {
    this.userService.closeNr(id).subscribe(() => { if (this.userService.statusC===1) this.successc = true; });
  }
  prepNRDelete(news: NewsRequest) {
    this.delNR = news.id;
  }
  deleteNR() {
    this.userService.deleteNR(this.delNR).subscribe(
      () => {
      location.reload();
      }
    );
  }
  prepNREdit(news: NewsRequest) {
    this.editNRForm.patchValue({
      'title': news.title,
      'description': news.description,
      'status': news.status
    });
    this.nrId = news.id;
    this.nr = news;
    console.log(this.nr);
  }
  editNR() {
    this.userService.editNR(this.nrId, this.editNRForm.controls['title'].value, this.editNRForm.controls['description'].value, this.editNRForm.controls['status'].value,
      this.nr.isClosed, this.nr.requestDate, this.nr.requestedById, this.nr.requestedBy, this.nr.categoryId)
      .subscribe(
        () => {
         // this.router.navigateByUrl('/')
          location.reload();
        }
      );
  }

}
