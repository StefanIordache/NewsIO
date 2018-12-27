import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserList } from './user-list.model';
import { Subscription } from 'rxjs';
import { FormGroup, FormControl } from '@angular/forms';
import { window } from 'rxjs/operator/window';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  private userSubscription: Subscription;
  users: UserList[];
  changeRoleForm: FormGroup;
  id: string;
  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router) { }

  ngOnInit() {
    if (this.userService.isLoggedIn() === false || this.userService.isAdmin() === false)
      this.router.navigateByUrl('/');
    else {
      this.userSubscription = this.userService.getAllUsers().subscribe(
        //(user: UserList[]) => { this.users = user; });
        (user: UserList[]) => { this.users = user.filter(user => user.roleName !== 'Administrator'); });
      this.userService.show();
      this.initForm()
    }
  }
  initForm() {
    this.changeRoleForm = new FormGroup({
      'roleName': new FormControl('')
    });
  }

  prepareEdit(user: UserList) {
    this.changeRoleForm.patchValue({
      'roleName': user.roleName
    });
    this.id = user.identityId;
    console.log(this.id);
  }
  changeRole() {
    this.userService.changeRole(this.id,this.changeRoleForm.controls['roleName'].value)
      .subscribe(
        () => {
          // this.router.navigateByUrl('/')
          location.reload();
        }
      );
  }
}
