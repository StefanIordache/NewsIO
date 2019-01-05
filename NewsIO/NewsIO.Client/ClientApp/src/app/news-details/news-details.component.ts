import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { HomeService } from '../home/home.service';
import { UserService } from '../shared/services/user.service';
import { News } from '../home/news.model';
import { FormGroup, FormControl } from '@angular/forms';
import { Coment } from './comment.model';
import { UserList } from '../users/user-list.model';

@Component({
  selector: 'app-news-details',
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.css']
})
export class NewsDetailsComponent implements OnInit {
  id: number;
  news: News;
  comments: Coment[];
  addCommentForm: FormGroup;
  editCommentForm: FormGroup;
  comId: number;
  delComId: number;
  com: Coment;
  currentUser: UserList;
  isUser: boolean = false;
  adminOrEditor: boolean = false;

  constructor(private route: ActivatedRoute, private homeService: HomeService, private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.id = +params['id'];
    });
    if (this.userService.isUser() === true) {
      this.isUser = true;
    }
    if (this.userService.isAdmin() === true || this.userService.isEditor() === true) {
      this.adminOrEditor = true;
    }
    this.userService.getUserById(localStorage.getItem("id")).subscribe((user: UserList) => { this.currentUser = user; });
    this.userService.getNewsById(this.id).subscribe((n: News) => { this.news = n; });
    this.userService.show();
    this.userService.getCommentsByNewsId(this.id).subscribe((comment:Coment[]) => {
      this.comments = comment;
    });
    this.initForm();
  }
  initForm() {
    this.addCommentForm = new FormGroup({
      'commentText': new FormControl('')
    });
    this.editCommentForm = new FormGroup({
      'commentText': new FormControl('')
    })

  }
  addComment() {
    this.userService.addComment(this.addCommentForm.controls['commentText'].value, this.id).subscribe(() => { location.reload();});
  }
  prepareCommentDelete(comment: Coment) {
    this.delComId = comment.id;
  }
  deleteComment() {
    this.userService.deleteComment(this.delComId).subscribe(
      () => {
        location.reload();
      }
    );
  }
  prepareCommentEdit(comment:Coment) {
    this.editCommentForm.patchValue({
      'commentText': comment.commentText,
    });
    this.comId = comment.id;
    this.com = comment;
  }
  editComment() {
    this.userService.editComment(this.comId, this.editCommentForm.controls['commentText'].value,this.id)
      .subscribe(
        () => {
           location.reload();
        }
      );
  }

}
