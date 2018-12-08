import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FormGroup,FormControl, MinLengthValidator, Validators } from '@angular/forms';
import { UserService } from '../shared/services/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  loginForm: FormGroup;
  isInvalid = false;
  invalid: String;

  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router) {
   }

  ngOnInit() {
    this.initForm();
    this.userService.hide();
  }
  initForm() {
    this.loginForm = new FormGroup({
      'username': new FormControl(''),
      'password': new FormControl('',Validators.minLength(8))
    });
  }
  onSubmit() {
    this.userService.login(this.loginForm.controls['username'].value, this.loginForm.controls['password'].value )
      .subscribe(
      () => {
        this.resetErrors();
          this.router.navigateByUrl('/');
          location.reload(); 
      },
      (response: HttpErrorResponse) => {
        this.resetErrors();
        if (response.status == 400) {
          this.isInvalid = true;
          this.invalid = "Invalid username or password";
        }
      }
    );
  }
  private resetErrors() {
    this.isInvalid = false
    this.invalid = '';
  }

}
